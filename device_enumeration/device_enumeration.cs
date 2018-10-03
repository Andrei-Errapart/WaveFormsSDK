using System;
using System.Collections.Generic;
using System.Text;

using WaveFormsSDK;

namespace device_enumeration
{
    class device_enumeration
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
                // detect connected all supported devices
                var cDevice = Dwf.Enumerate(ENUMFILTER.All);
                // list information about each device
                cout.WriteLine("found " + cDevice.ToString() + " devices");
                for(int i = 0; i < cDevice; i++)
                {
                    // we use 0 based indexing
                    var szDeviceName = Dwf.EnumDeviceName(i);
                    var szSN = Dwf.EnumSN(i);
                    cout.WriteLine("device: " + (i+1).ToString() + " name: " + szDeviceName + " " + szSN);
                    // before opening, check if the device isn’t already opened by other application, like: WaveForms
                    var fIsInUse = Dwf.EnumDeviceIsOpened(i);
                    if (!fIsInUse)
                    {
                        var hdwf = Dwf.DeviceOpen(i);
                        var cChannel = Dwf.AnalogInChannelCount(hdwf);
                        var frequencyRange = Dwf.AnalogInFrequencyInfo(hdwf);
                        cout.WriteLine("number of analog input channels: " + cChannel.ToString() + " maximum freq.: " + frequencyRange.Max.ToString() + " Hz");
                        Dwf.DeviceClose(hdwf);
                        hdwf = -1;
                    }
                }
                // before application exit make sure to close all opened devices by this process
                Dwf.DeviceCloseAll();
            }
            catch (Exception ex)
            {
                cout.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
