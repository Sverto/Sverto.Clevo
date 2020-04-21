namespace Sverto.Clevo.Fans
{
    public class FanControl
    {
        protected const string _SetMethodName = "SetFanDuty";
        protected ClevoBridge _Clevo;

        internal FanControl(ClevoBridge clevo)
        {
            _Clevo = clevo;
        }

        public void Set(uint value)
        {
            _Clevo.RunMethod(_SetMethodName, value);
        }

        public void SetSpeed(byte gpu1, byte gpu2, byte cpu)
        {
            _Clevo.RunMethod(_SetMethodName, SpeedToValue(gpu1, gpu2, cpu));
        }
        public void SetSpeed(FanSpeed fanSpeed)
        {
            _Clevo.RunMethod(_SetMethodName, (uint)fanSpeed);
        }


        protected uint SpeedToValue(byte gpu1, byte gpu2, byte cpu)
        {
            return (uint)((gpu1 << 16) + (gpu2 << 8) + cpu);
        }

    }
}
