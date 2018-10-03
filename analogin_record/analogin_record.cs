using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace analogin_record
{
    class analogin_record
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
                const int nSamples = 100000;
                double[] rgdSamples = new double[nSamples];
                int cSamples = 0;
                bool fLost = false;
                bool fCorrupted = false;

                cout.WriteLine("Open automatically the first available device\n");
                var hdwf = Dwf.DeviceOpen(-1);

                Dwf.AnalogInChannelEnableSet(hdwf, 0, true);

                Dwf.AnalogInChannelRangeSet(hdwf, 0, 5);

                // recording rate for more samples than the device buffer is limited by device communication
                Dwf.AnalogInAcquisitionModeSet(hdwf, ACQMODE.Record);
                Dwf.AnalogInFrequencySet(hdwf, 50000.0);

                // make sure we calculate record length with the real frequency
                var hzAcq = Dwf.AnalogInFrequencyGet(hdwf);
                Dwf.AnalogInRecordLengthSet(hdwf, 1.0 * nSamples / hzAcq);

                // wait at least 2 seconds with Analog Discovery for the offset to stabilize, before the first reading after device open or offset/range change
                Wait(2);

                // start
                Dwf.AnalogInConfigure(hdwf, false, true);

                cout.WriteLine("Recording...");
                while (cSamples < nSamples)
                {
                    var sts = Dwf.AnalogInStatus(hdwf, true);
                    if (cSamples == 0 && (sts ==  STATE.Config || sts == STATE.Prefill || sts == STATE.Armed))
                    {
                        // Acquisition not yet started.
                        continue;
                    }

                    var stats = Dwf.AnalogInStatusRecord(hdwf);

                    cSamples += stats.Lost;

                    if (stats.Lost > 0)
                    {
                        fLost = true;
                    }
                    if (stats.Corrupt > 0)
                    {
                        fCorrupted = false;
                    }

                    if (stats.Available > 0)
                    {
                        var remaining = nSamples - cSamples;
                        var todo = Math.Min(remaining, stats.Available);
                        // get samples
                        Dwf.AnalogInStatusData(hdwf, 0, rgdSamples, cSamples, todo);
                        cSamples += stats.Available;
                    }
                }

                cout.WriteLine("done");

                if (fLost)
                {
                    cout.WriteLine("Samples were lost! Reduce frequency");
                }
                else if (fCorrupted)
                {
                    cout.WriteLine("Samples could be corrupted! Reduce frequency");
                }

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
