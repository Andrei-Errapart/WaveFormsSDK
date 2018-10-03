using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace digitalout_binarycounter
{
    class digitalout_binarycounter
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

                for (int i = 0; i < 16; i++)
                {
                    Dwf.DigitalOutEnableSet(hdwf, i, true);
                    // increase by 2 the period of successive bits
                    Dwf.DigitalOutDividerSet(hdwf, i, 1u << i);
                    // 100kHz coutner rate, SystemFrequency/100kHz
                    Dwf.DigitalOutCounterSet(hdwf, i, (uint)(hzSys / 1e5), (uint)(hzSys / 1e5));
                }

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
