using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace digitalout_walking1
{
    class digitalout_walking1
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

                var hzSys = Dwf.DigitalOutInternalClockInfo(hdwf);

                // 10 bit walking 1
                for (int i = 0; i < 10; i++)
                {
                    Dwf.DigitalOutEnableSet(hdwf, i, true);
                    // divide system frequency down to 1khz
                    Dwf.DigitalOutDividerSet(hdwf, i, (uint)(hzSys / 1e3));
                    // all pins will be 9 ticks low and 1 high
                    Dwf.DigitalOutCounterSet(hdwf, i, 9, 1);
                    // first bit will start high others low with increasing phase
                    Dwf.DigitalOutCounterInitSet(hdwf, i, i == 0, (uint)i);
                }

                // start digital out
                Dwf.DigitalOutConfigure(hdwf, true);

                // it will run until stopped, reset, parameter changed or device closed
                Wait(5);
                Dwf.DigitalOutReset(hdwf);

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
