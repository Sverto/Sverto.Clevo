namespace Sverto.Clevo.Keyboard
{
    public enum KeyboardLedPatern : uint
    {
        None = 0xf3000000,
        SingleFade = 0x10000000,
        Breath = 0x30000000,
        Random = 0x70000000,
        FastWave = 0x80000000,
        MidOut = 0x90000000,
        Flicker = 0xa0000000,
        LeftRight = 0xb0000000
    }
}
