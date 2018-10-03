using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace analogin_acquisition
{
    class analogin_acquisition
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
                cout.WriteLine("Open automatically the first available device");
                var hdwf = Dwf.DeviceOpen(-1);

                // get the number of analog in channels
                var cChannel = Dwf.AnalogInChannelCount(hdwf);

                // enable channels
                for (int c = 0; c < cChannel; c++)
                {
                    Dwf.AnalogInChannelEnableSet(hdwf, c, true);
                }
                // set 5V pk2pk input range for all channels
                Dwf.AnalogInChannelRangeSet(hdwf, -1, 5);

                // 20MHz sample rate
                Dwf.AnalogInFrequencySet(hdwf, 20000000.0);

                // get the maximum buffer size
                var cSamples = Dwf.AnalogInBufferSizeInfo(hdwf).Max;
                Dwf.AnalogInBufferSizeSet(hdwf, cSamples);

                // configure trigger
                Dwf.AnalogInTriggerSourceSet(hdwf, TRIGSRC.DetectorAnalogIn);
                Dwf.AnalogInTriggerAutoTimeoutSet(hdwf, 10.0);
                Dwf.AnalogInTriggerChannelSet(hdwf, 0);
                Dwf.AnalogInTriggerTypeSet(hdwf, TRIGTYPE.Edge);
                Dwf.AnalogInTriggerLevelSet(hdwf, 1.0);
                Dwf.AnalogInTriggerConditionSet(hdwf, TRIGCOND.RisingPositive);

                // wait at least 2 seconds with Analog Discovery for the offset to stabilize, before the first reading after device open or offset/range change
                Wait(2);

                // start
                Dwf.AnalogInConfigure(hdwf, false, true);

                cout.WriteLine("Waiting for triggered or auto acquisition");
                STATE sts;
                do
                {
                    sts = Dwf.AnalogInStatus(hdwf, true);
                } while (sts != STATE.Done);

                double[] rgdSamples = null;

                // get the samples for each channel
                for (int c = 0; c < cChannel; c++)
                {
                    rgdSamples = Dwf.AnalogInStatusData(hdwf, c, cSamples);
                    // do something with it
                }

                cout.WriteLine("done");

                // close the device
                Dwf.DeviceClose(hdwf);
            }
            catch (Exception ex)
            {
                cout.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
