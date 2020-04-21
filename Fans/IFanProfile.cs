namespace Sverto.Clevo.Fans
{
    interface IFanProfile
    {
        byte TempToFanSpeed(double temperature);
    }
}
