using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace analogin_sample
{
    class analogin_sample
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
                // enable first channel FDwfAnalogInChannelEnableSet(hdwf, 0, true)
                // set 0V offset
                Dwf.AnalogInChannelOffsetSet(hdwf, 0, 0);
                // set 5V pk2pk input range, -2.5V to 2.5V
                Dwf.AnalogInChannelRangeSet(hdwf, 0, 5);
                // start signal generation
                Dwf.AnalogInConfigure(hdwf, false, false);
                // wait at least 2 seconds with Analog Discovery for the offset to stabilize, before the first reading after device open or offset/range change
                Wait(2);

                for (int i = 0; i < 10; i++)
                {
                    Wait(1);
                    // fetch analog input information from the device
                    var state = Dwf.AnalogInStatus(hdwf, false);
                    // read voltage input of first channel
                    var voltage = Dwf.AnalogInStatusSample(hdwf, 0);
                    cout.WriteLine(voltage.ToString() + " V", voltage);
                }
                // close all opened devices by this process
                Dwf.DeviceCloseAll();
            }
            catch (Exception ex)
            {
                cout.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
