using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace analogout_custom_am
{
    class analogout_custom_am
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
                var rgdSamples = new double[1024];
                int idxChannel = 0;

                // generate custom samples normalized to +-1
                for (int i = 0; i < 1024; i++)
                {
                    rgdSamples[i] = 2.0 * i / 1023 - 1;
                }

                // open automatically the first available device
                var hdwf = Dwf.DeviceOpen(-1);
                // needed for EExplorer, don't care for ADiscovery
                Dwf.AnalogOutCustomAMFMEnableSet(hdwf, idxChannel, true);
                // enable first channel
                Dwf.AnalogOutNodeEnableSet(hdwf, idxChannel, ANALOGOUTNODE.Carrier, true);
                // set sine carrier
                Dwf.AnalogOutNodeFunctionSet(hdwf, idxChannel, ANALOGOUTNODE.Carrier, FUNC.Sine);
                // 1V amplitude, 2V pk2pk
                Dwf.AnalogOutNodeAmplitudeSet(hdwf, idxChannel, ANALOGOUTNODE.Carrier, 1.0);
                // 10kHz carrier frequency
                Dwf.AnalogOutNodeFrequencySet(hdwf, idxChannel, ANALOGOUTNODE.Carrier, 1000.0);
                // enable amplitude modulation
                Dwf.AnalogOutNodeEnableSet(hdwf, idxChannel, ANALOGOUTNODE.AM, true);
                // set custom AM
                Dwf.AnalogOutNodeFunctionSet(hdwf, idxChannel, ANALOGOUTNODE.AM,  FUNC.Custom);
                // +-100% modulation index, will result with 1V amplitude carrier, 0V to 2V
                Dwf.AnalogOutNodeAmplitudeSet(hdwf, idxChannel, ANALOGOUTNODE.AM, 100);
                // 10Hz AM frequency
                Dwf.AnalogOutNodeFrequencySet(hdwf, idxChannel, ANALOGOUTNODE.AM, 10.0);
                // set custom waveform samples
                // normalized to ±1 values
                Dwf.AnalogOutNodeDataSet(hdwf, idxChannel, ANALOGOUTNODE.AM, rgdSamples);
                // by default the offset is 0V
                // start signal generation
                Dwf.AnalogOutConfigure(hdwf, idxChannel, true);
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
