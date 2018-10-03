using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace digitalin_acquisition
{
    class digitalin_acquisition
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

                var hzSys = Dwf.DigitalInInternalClockInfo(hdwf);

                // sample rate to 1MHz
                Dwf.DigitalInDividerSet(hdwf, (uint)(hzSys / 1e6));

                // 16bit WORD format
                Dwf.DigitalInSampleFormatSet(hdwf, 16);

                // get the maximum buffer size
                var cSamples = Dwf.DigitalInBufferSizeInfo(hdwf);
                // default buffer size is set to maximum
                //Dwf.DigitalInBufferSizeSet(hdwf, cSamples);

                // set trigger position to the middle 
                Dwf.DigitalInTriggerPositionSet(hdwf, (uint)(cSamples / 2));

                // trigger on any pin transition
                Dwf.DigitalInTriggerSourceSet(hdwf, TRIGSRC.DetectorDigitalIn);
                Dwf.DigitalInTriggerAutoTimeoutSet(hdwf, 10.0);
                Dwf.DigitalInTriggerSet(hdwf, 0, 0, 0xFFFF, 0xFFFF);

                // start
                Dwf.DigitalInConfigure(hdwf, false, true);

                cout.WriteLine("Waiting for triggered or auto acquisition");
                STATE sts;
                do
                {
                    sts = Dwf.DigitalInStatus(hdwf, true);
                } while (sts != STATE.Done);

                // get the samples
                var rgwSamples = Dwf.DigitalInStatusData(hdwf, cSamples);

                cout.WriteLine("done");

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
