using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace digitalout_pins
{
    class digitalout_pins
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
    
                //
                var hzSys = Dwf.DigitalOutInternalClockInfo(hdwf);
    
                // 1kHz pulse on IO pin 0
                Dwf.DigitalOutEnableSet(hdwf, 0, true);
                // prescaler to 2kHz, SystemFrequency/1kHz/2
                Dwf.DigitalOutDividerSet(hdwf, 0, (uint)(hzSys/1e3/2));
                // 1 tick low, 1 tick high
                Dwf.DigitalOutCounterSet(hdwf, 0, 1, 1);
    
                // 1kHz 25% duty pulse on IO pin 1
                Dwf.DigitalOutEnableSet(hdwf, 1, true);
                // prescaler to 4kHz SystemFrequency/1kHz/2
                Dwf.DigitalOutDividerSet(hdwf, 1, (uint)(hzSys/1e3/4));
                // 3 ticks low, 1 tick high
                Dwf.DigitalOutCounterSet(hdwf, 1, 3, 1);
    
                // 2kHz random on IO pin 2
                Dwf.DigitalOutEnableSet(hdwf, 2, true);
                Dwf.DigitalOutTypeSet(hdwf, 2, DIGITALOUTTYPE.Random);
                Dwf.DigitalOutDividerSet(hdwf, 2, (uint)(hzSys/2e3));

                var rgcustom = new byte[] {0x00,0xAA,0x66,0xFF};
                // 1kHz custom on IO pin 3
                Dwf.DigitalOutEnableSet(hdwf, 3, true);
                Dwf.DigitalOutTypeSet(hdwf, 3,DIGITALOUTTYPE.Custom);
                Dwf.DigitalOutDividerSet(hdwf, 3, (uint)(hzSys/2e3));
                Dwf.DigitalOutDataSet(hdwf, 3, rgcustom, 4*8);
    
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
