using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace analogout_sine
{
    class analogout_sine
    {
        public static readonly System.IO.TextWriter cout = System.Console.Out;

        public static void Wait(double sec)
        {
            System.Threading.Thread.Sleep((int)(sec * 1000));
        }

        static void Main(string[] args)
        {
            try
            {
                // open automatically the first available device
                var hdwf = Dwf.DeviceOpen(-1);
                // enable first channel
                Dwf.AnalogOutNodeEnableSet(hdwf, 0, ANALOGOUTNODE.Carrier, true);
                // set sine function
                Dwf.AnalogOutNodeFunctionSet(hdwf, 0, ANALOGOUTNODE.Carrier, FUNC.Sine);
                // 10kHz
                Dwf.AnalogOutNodeFrequencySet(hdwf, 0, ANALOGOUTNODE.Carrier, 10000.0);
                // 1.41V amplitude (1Vrms), 2.82V pk2pk
                Dwf.AnalogOutNodeAmplitudeSet(hdwf, 0, ANALOGOUTNODE.Carrier, 1.41);
                // 1.41V offset
                Dwf.AnalogOutNodeOffsetSet(hdwf, 0, ANALOGOUTNODE.Carrier, 1.41);
                // start signal generation
                Dwf.AnalogOutConfigure(hdwf, 0, true);
                // it will run until stopped, reset, parameter changed or device closed
                Wait(2);
                // on close device is stopped and configuration lost
                Dwf.DeviceClose(hdwf);
            }
            catch (Exception ex)
            {
                cout.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
