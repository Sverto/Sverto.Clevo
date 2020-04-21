using System;
using Sverto.Clevo.Keyboard;
using Sverto.Clevo.Fans;
using Sverto.Clevo.Sensors;

namespace Sverto.Clevo
{
    public class ClevoBridge : IDisposable
    {
        protected const string _WmiPath = "root\\WMI";
        protected const string _WmiQuery = "SELECT * FROM CLEVO_GET";
        protected WmiProvider _Wmi;

        public KeyboardLedControl KeyboardLed { get; }
        public FanControl Fan { get; }
        public SensorControl Sensor { get; }

        public ClevoBridge()
        {
            _Wmi = new WmiProvider(_WmiPath, _WmiQuery);

            KeyboardLed = new KeyboardLedControl(this);
            Fan = new FanControl(this);
            Sensor = new SensorControl(); // this);
        }

        public void RunMethod(string methodName, uint value)
        {
            _Wmi.RunMethod(methodName, new object[] { value });
        }
        public uint RunMethod(string methodName)
        {
            return Convert.ToUInt32(_Wmi.RunMethod(methodName));
        }

        public void Dispose()
        {
            _Wmi?.Dispose();
            Sensor.Dispose();
        }

    }
}
