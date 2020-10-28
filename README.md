# Sverto.Clevo
A Library to control Clevo laptop Fans and Keyboard lights.  
Created for Clevo P670RS-G but may work on other models.

## Requires
The `OpenHardwareMonitor.Hardware` library (.\Libraries\OpenHardwareMonitorLib.dll) for `Clevo.Sensor` to work.  
The Clevo `Control Center` software installed for `ClevoBridge` to work.

## Keyboard Lights Example
```csharp
using Sverto.Clevo;
using Sverto.Clevo.Keyboard;

namespace ExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init bridge to Clevo WMI
            clevo = new ClevoBridge();

            // Turn left part of keyboard lights on
            clevo.KeyboardLed.SetPower(KeyboardLedPower.LeftOn);
            // Or turn all keyboard lights on
            clevo.KeyboardLed.SetPower(KeyboardLedPower.On);

            // Set brightness to max (byte)
            clevo.KeyboardLed.SetBrightness(0xff);

            // Set RGB color (in bytes) of middle keyboard part to red
            clevo.KeyboardLed.SetColor(0xff, 0x20, 0x20, KeyboardLedPosition.Middle);

            // ...
        }
    }
}
```

## Fanspeed Example
```csharp
using Sverto.Clevo;
using Sverto.Clevo.Fans;

namespace ExampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init bridge to Clevo WMI
            clevo = new ClevoBridge();

            // Use the library predefined profile
            var cpuFanProfile = new CustomFanProfile();
            var gpuFanProfile = new CustomFanProfile();

            // Get temperature
            var cpuTemp = clevo.Sensor.GetCpuTemp();
            var gpuTemp = clevo.Sensor.GetGpuTemp();

            // Get fanspeed for temperature using CustomFanProfile
            var cpuFanSpeed = cpuFanProfile.TempToFanSpeed(cpuTemp);
            var gpuFanSpeed = gpuFanProfile.TempToFanSpeed(gpuTemp);

            // Apply fanspeed (Clevo P670RS-G has 2 gpu and 1 cpu fans)
            clevo.Fan.SetSpeed(gpuFanSpeed, gpuFanSpeed, cpuFanSpeed);
            // Or set all fans to max speed
            clevo.Fan.SetSpeed(FanSpeed.Full);

            // ...
        }
    }
}
```
