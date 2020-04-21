using System;
using System.Collections.Generic;
using System.Linq;
using OpenHardwareMonitor.Hardware;

namespace Sverto.Clevo.Sensors
{
    public class SensorControl : IDisposable
    {
        //protected ClevoBridge _Clevo;
        Computer _HwMon;

        internal SensorControl() //ClevoBridge clevo)
        {
            //_Clevo = clevo;

            _HwMon = new Computer();
            //_HwMon.Open();
            _HwMon.GPUEnabled = true;
            _HwMon.CPUEnabled = true;
            _HwMon.Open();
        }

        public uint GetCpuTemp()
        {
            // Seems off, any other method returns 0...
            //return _Clevo.RunMethod("GetCPUtempThermalIC");
            return HwMon_GetTemperature(HardwareType.CPU);
        }

        public uint GetGpuTemp()
        {
            // Clevo Bug? _Clevo.RunMethod("GetVGA2tempThermalIC") = 0
            return HwMon_GetTemperature(HardwareType.GpuNvidia);
        }


        private Dictionary<HardwareType, ISensor> _SensorMapping = new Dictionary<HardwareType, ISensor>();
        protected uint HwMon_GetTemperature(HardwareType hwType)
        {
            uint value = 0;
            ISensor sensor = null;

            if (!_SensorMapping.ContainsKey(hwType))
            {
                var hwItem = _HwMon.Hardware.FirstOrDefault(x => x.HardwareType == hwType);
                if (hwItem != null)
                {
                    sensor = hwItem.Sensors.LastOrDefault(s => s.SensorType == SensorType.Temperature);
                    if (sensor != null)
                        _SensorMapping.Add(hwType, sensor);
                }
            }
            else
            {
                sensor = _SensorMapping[hwType];
            }
            
            if (sensor != null)
            {
                sensor.Hardware.Update();
                value = (uint)sensor.Value;
            }  

            return sensor == null ? uint.MaxValue : value;
        }


        public void Dispose()
        {
            _HwMon?.Close();
        }

    }
}
