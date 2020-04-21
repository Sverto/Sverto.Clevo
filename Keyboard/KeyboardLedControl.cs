namespace Sverto.Clevo.Keyboard
{
    public class KeyboardLedControl
    {
        protected const string _MethodName = "SetKBLED";
        protected ClevoBridge _Clevo;

        internal KeyboardLedControl(ClevoBridge clevo)
        {
            _Clevo = clevo;
        }
        

        public void Set(uint value)
        {
            _Clevo.RunMethod(_MethodName, value);
        }

        public void SetPower(KeyboardLedPower power)
        {
            SetPatern(KeyboardLedPatern.None);
            _Clevo.RunMethod(_MethodName, (uint)power);
        }

        public void SetColor(byte r, byte g, byte b)
        {
            _Clevo.RunMethod(_MethodName, ColorToValue(r, g, b, KeyboardLedPosition.Left));
            _Clevo.RunMethod(_MethodName, ColorToValue(r, g, b, KeyboardLedPosition.Middle));
            _Clevo.RunMethod(_MethodName, ColorToValue(r, g, b, KeyboardLedPosition.Right));
        }
        public void SetColor(byte r, byte g, byte b, KeyboardLedPosition position)
        {
            _Clevo.RunMethod(_MethodName, ColorToValue(r, g, b, position));
        }

        public void SetPatern(KeyboardLedPatern patern)
        {
            _Clevo.RunMethod(_MethodName, (uint)patern);
        }

        public void SetBrightness(byte brightness)
        {
            _Clevo.RunMethod(_MethodName, BrightnessToValue(brightness));
        }


        protected uint ColorToValue(byte r, byte g, byte b, KeyboardLedPosition position)
        {
            return (uint)(0xf0000000 + (b << 16) + (r << 8) + g) + (uint)position;
        }
        protected uint BrightnessToValue(byte brightness)
        {
            return 0xf4000000 + brightness;
        }


    }
}
