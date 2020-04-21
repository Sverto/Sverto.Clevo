using System;
using System.Linq;

namespace Sverto.Clevo.Fans
{
    public class CustomFanProfile : IFanProfile
    {
        private SlidingBuffer<double> _LastTemperatures = new SlidingBuffer<double>(15);

        private int _NoFanCounter = 0;
        protected const int NOFAN_THRESSHOLD = 10;

        protected const double TEMP_FOR_MAX_FANSPEED = 80;
        protected const double TEMP_FOR_MIN_FANSPEED = 55;
        protected const double TEMP_SAFE_MAX = 88;

        protected const byte FANSPEED_OFF = 0x00;
        protected const byte FANSPEED_MIN = 0x75;
        protected const byte FANSPEED_MAX = 0xff;

        public byte TempToFanSpeed(double temperature)
        {
            _LastTemperatures.Add(temperature);

            // Calculate average based on some parameters
            double average;
            if (temperature < TEMP_SAFE_MAX)
            {
                average = _LastTemperatures
                    .Where(t => t > 0)   // Ignore possible error readings
                    .DefaultIfEmpty(0.0) // In MSHybrid the dedicated GPU temperature is 0 if not used, so default to this when we're working on an empty list 
                    .Average(t1 => t1);  // Average of last x readings
            }
            else
            {
                // Reset avarage to non safe temperature so we can act accordingly
                _LastTemperatures.Clear();
                _LastTemperatures.Add(temperature);
                _LastTemperatures.Add(temperature); // Increase weight for next average calculation
                average = temperature;
            }

            // Stop fans after x iterations below min fan temperature
            if (average <= TEMP_FOR_MIN_FANSPEED)
            {
                _NoFanCounter += 1;
                if (_NoFanCounter >= NOFAN_THRESSHOLD)
                    return FANSPEED_OFF;
                else
                    return FANSPEED_MIN;
            }
            else
            {
                _NoFanCounter = 0; // Reset
            }

            // Calculate fanspeed
            var fanSpeed = (FANSPEED_MAX - FANSPEED_MIN) / (TEMP_FOR_MAX_FANSPEED - TEMP_FOR_MIN_FANSPEED) * (average - TEMP_FOR_MIN_FANSPEED) + FANSPEED_MIN; // Linear fan formule
            return (byte)Math.Min(Math.Max(fanSpeed, FANSPEED_MIN), FANSPEED_MAX);
        }

    }
}
