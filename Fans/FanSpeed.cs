namespace Sverto.Clevo.Fans
{
    public enum FanSpeed : uint
    {
        Full = 0xffffff,
        Off = 0x000000,

        Discrete = 0xbbbb99,

        Level1 = 0x888888,
        Level2 = 0x999999,
        Level3 = 0xaaaaaa,
        Level4 = 0xbbbbbb,
        Level5 = 0xcccccc,
        Level6 = 0xdddddd,
        Level7 = 0xeeeeee
    }
}