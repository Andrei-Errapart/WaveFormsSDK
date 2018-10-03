using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace analogout_sync
{
    class analogout_sync
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
                // enable two channels
                Dwf.AnalogOutNodeEnableSet(hdwf, 0, ANALOGOUTNODE.Carrier, true);
                Dwf.AnalogOutNodeEnableSet(hdwf, 1, ANALOGOUTNODE.Carrier, true);
                // for second channel set master the first channel
                Dwf.AnalogOutMasterSet(hdwf, 1, 0);
                // slave channel is controlled by the master channel
                // it is enough to set trigger, wait, run and repeat parameters for master channel
                // when using different frequencies it might need periodical resynchronization
                // to do so set limited runtime and repeat infinite

                // configure enabled channels
                Dwf.AnalogOutNodeFunctionSet(hdwf, -1, ANALOGOUTNODE.Carrier, FUNC.Sine);
                Dwf.AnalogOutNodeFrequencySet(hdwf, -1, ANALOGOUTNODE.Carrier, 10000.0);
                Dwf.AnalogOutNodeAmplitudeSet(hdwf, -1, ANALOGOUTNODE.Carrier, 1.0);
                // set phase for second channel
                Dwf.AnalogOutNodePhaseSet(hdwf, 1, ANALOGOUTNODE.Carrier, 180.0);

                // slave channel will only be initialized
                Dwf.AnalogOutConfigure(hdwf, 1, true);
                // starting master will start slave channels too
                Dwf.AnalogOutConfigure(hdwf, 0, true);
                // it will run until stopped, reset, parameter changed or device closed
                Wait(10);
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
