using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace analogout_sweep
{
    class analogout_sweep
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
                int idxChannel = 0;
                double hzStart = 1e3;
                double hzStop = 1e5;
                double secSweep = 1;

                // open automatically the first available device
                var hdwf = Dwf.DeviceOpen(-1);
                // enable first channel
                Dwf.AnalogOutNodeEnableSet(hdwf, idxChannel, ANALOGOUTNODE.Carrier, true);
                // set sine carrier
                Dwf.AnalogOutNodeFunctionSet(hdwf, idxChannel, ANALOGOUTNODE.Carrier, FUNC.Sine);
                // 1V amplitude, 2V pk2pk
                Dwf.AnalogOutNodeAmplitudeSet(hdwf, idxChannel, ANALOGOUTNODE.Carrier, 1.0);
                // 10kHz carrier frequency
                Dwf.AnalogOutNodeFrequencySet(hdwf, idxChannel, ANALOGOUTNODE.Carrier, (hzStart + hzStop) / 2);
                // enable frequency modulation
                Dwf.AnalogOutNodeEnableSet(hdwf, idxChannel, ANALOGOUTNODE.FM, true);
                // linear sweep with ramp up symmetry 100%
                Dwf.AnalogOutNodeFunctionSet(hdwf, idxChannel, ANALOGOUTNODE.FM, FUNC.RampUp);
                Dwf.AnalogOutNodeSymmetrySet(hdwf, idxChannel, ANALOGOUTNODE.FM, 100);
                // modulation index
                Dwf.AnalogOutNodeAmplitudeSet(hdwf, idxChannel, ANALOGOUTNODE.FM, 100.0 * (hzStop - hzStart) / (hzStart + hzStop));
                // FM frequency = 1/sweep time
                Dwf.AnalogOutNodeFrequencySet(hdwf, idxChannel, ANALOGOUTNODE.FM, 1.0 / secSweep);

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
