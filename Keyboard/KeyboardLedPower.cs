using System;

namespace Sverto.Clevo.Keyboard
{
    [Flags]
    public enum KeyboardLedPower : uint
    {
        Off = 0xe0000000,
        LeftOn = 0xe0010000,
        MiddleOn = 0xe0020000,
        RightOn = 0xe0040000,
        On = LeftOn | MiddleOn | RightOn
    }
}
