using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace analogout_custom
{
    class analogout_custom
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
                var rgdSamples = new double[4096];

                // generate custom samples normalized to +-1
                for (int i = 0; i < 4096; i++)
                {
                    rgdSamples[i] = 2.0 * i / 4095 - 1;
                }

                // open automatically the first available device
                var hdwf = Dwf.DeviceOpen(-1);
                // enable first channel
                Dwf.AnalogOutNodeEnableSet(hdwf, 0, ANALOGOUTNODE.Carrier, true);
                // set custom function
                Dwf.AnalogOutNodeFunctionSet(hdwf, 0, ANALOGOUTNODE.Carrier, FUNC.Custom);
                // set custom waveform samples
                // normalized to ±1 values
                Dwf.AnalogOutNodeDataSet(hdwf, 0, ANALOGOUTNODE.Carrier, rgdSamples);
                // 10kHz waveform frequency
                Dwf.AnalogOutNodeFrequencySet(hdwf, 0, ANALOGOUTNODE.Carrier, 10000.0);
                // 2V amplitude, 4V pk2pk, for sample value -1 will output -2V, for 1 +2V
                Dwf.AnalogOutNodeAmplitudeSet(hdwf, 0, ANALOGOUTNODE.Carrier, 2);
                // by default the offset is 0V
                // start signal generation
                Dwf.AnalogOutConfigure(hdwf, 0, true);
                // it will run until stopped or device closed
                Wait(5);
                // on close device is stopped and configuration lost
                Dwf.DeviceCloseAll();
            }
            catch (Exception ex)
            {
                cout.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
