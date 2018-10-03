using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

using HDWF = System.Int32;

namespace WaveFormsSDK
{
    /// <summary>
    /// Device enumeration filters
    /// </summary>
    public enum ENUMFILTER : int
    {
        All = 0,
        EExplorer = 1,
        Discovery = 2,
    }

    /// <summary>
    /// Device ID
    /// </summary>
    public enum DEVID : int
    {
        EExplorer  = 1,
        Discovery  = 2,
    }

    /// <summary>
    /// Device version
    /// </summary>
    public enum DEVVER : int {
        EExplorerC   = 2,
        EExplorerE   = 4,
        EExplorerF   = 5,
        DiscoveryA   = 1,
        DiscoveryB   = 2,
        DiscoveryC   = 3,
    }

    /// <summary>
    /// Trigger source
    /// </summary>
    public enum TRIGSRC : byte {
        /// <summary>
        /// The trigger pin is high impedance, input. This is the default setting.
        /// </summary>
        None               = 0,
        /// <summary>
        /// Trigger from PC, this can be used to synchronously start multiple instruments.
        /// </summary>
        PC = 1,
        /// <summary>
        /// Trigger detector on analog in channels.
        /// </summary>
        DetectorAnalogIn = 2,
        /// <summary>
        /// Trigger on digital input channels.
        /// </summary>
        DetectorDigitalIn = 3,
        /// <summary>
        /// Trigger on device instruments, these output high when running.
        /// </summary>
        AnalogIn = 4,
        /// <summary>
        /// Trigger on device instruments, these output high when running.
        /// </summary>
        DigitalIn = 5,
        /// <summary>
        /// Trigger on device instruments, these output high when running.
        /// </summary>
        DigitalOut = 6,
        /// <summary>
        /// Trigger on device instruments, these output high when running.
        /// </summary>
        AnalogOut1 = 7,
        /// <summary>
        /// Trigger on device instruments, these output high when running.
        /// </summary>
        AnalogOut2 = 8,
        /// <summary>
        /// Trigger on device instruments, these output high when running.
        /// </summary>
        AnalogOut3 = 9,
        /// <summary>
        /// Trigger on device instruments, these output high when running.
        /// </summary>
        AnalogOut4 = 10,
        /// <summary>
        /// External trigger signal.
        /// </summary>
        External1 = 11,
        /// <summary>
        /// External trigger signal.
        /// </summary>
        External2 = 12,
        /// <summary>
        /// External trigger signal.
        /// </summary>
        External3 = 13,
        /// <summary>
        /// External trigger signal.
        /// </summary>
        External4 = 14,
    }

    /// <summary>
    /// Instrument states
    /// </summary>
    public enum STATE : byte {
        /// <summary>
        /// Initial state. After AnalogInConfigure or any AnalogIn*Set function call goes to this state.
        /// With AnalogInConfigure, reconfigure goes to Configure state.
        /// </summary>
        Ready        = 0,
        /// <summary>
        /// The needed configurations are performed and auto trigger is reset.
        /// </summary>
        Config       = 4,
        /// <summary>
        /// Prefills the buffer with samples needed before trigger.
        /// </summary>
        Prefill      = 5,
        /// <summary>
        /// Waits for the trigger.
        /// </summary>
        Armed        = 1,
        Wait         = 7,
        Triggered    = 3,
        /// <summary>
        /// a) Single acquisition mode: remains in this state to acquire samples after trigger according trigger position parameter.
        /// b) Scan screen and shift modes: remains in this state until configure or any set function of this instrument.
        /// c) Record mode: the time period according record length parameter.
        /// </summary>
        Running      = 3,
        /// <summary>
        /// Final state.
        /// </summary>
        Done         = 2,
    }

    /// <summary>
    /// Acquisition modes
    /// </summary>
    public enum ACQMODE : int {
        /// <summary>
        /// Perform a single buffer acquisition. This is the default setting.
        /// </summary>
        Single     = 0,
        /// <summary>
        /// Perform a continuous acquisition in FIFO style. The trigger setting is ignored.
        /// The last sample is at the end of buffer.
        /// The FDwfAnalogInStatusSamplesValid or FDwfDigitalInStatusSamplesValid function is used to show the number of the acquired samples, which will grow until reaching the BufferSize.
        /// Then the waveform “picture” is shifted for every new sample.
        /// </summary>
        ScanShift  = 1,
        /// <summary>
        /// Perform continuous acquisition circularly writing samples into the buffer.
        /// The trigger setting is ignored. The IndexWrite shows the buffer write position. 
        /// This is similar to a heart monitor display.
        /// </summary>
        ScanScreen = 2,
        /// <summary>
        /// Perform acquisition for length of time set by FDwfAnalogInRecordLengthSet.
        /// </summary>
        Record     = 3,
    }

    /// <summary>
    /// Analog acquisition filter
    /// </summary>
    public enum FILTER : int {
        /// <summary>
        /// Store every Nth ADC conversion, where N = ADC frequency /acquisition frequency.
        /// </summary>
        Decimate = 0,
        /// <summary>
        /// Store the average of N ADC conversions.
        /// </summary>
        Average  = 1,
        /// <summary>
        /// Store interleaved, the minimum and maximum values, of 2xN conversions.
        /// </summary>
        MinMax   = 2,
    }

    /// <summary>
    /// Analog input trigger mode.
    /// </summary>
    public enum TRIGTYPE : int {
        /// <summary>
        /// Trigger on rising or falling edge. This is the default setting.
        /// </summary>
        Edge         = 0,
        /// <summary>
        /// Trigger on positive or negative; less, timeout or more pulse lengths
        /// </summary>
        Pulse        = 1,
        /// <summary>
        /// Trigger on rising or falling; less, timeout or more transition times
        /// </summary>
        Transition   = 2,
    }

    /// <summary>
    /// Analog input trigger condition
    /// </summary>
    public enum TRIGCOND : int {
        /// <summary>
        /// For edge and transition trigger on rising edge and for pulse trigger on positive pulse.
        /// </summary>
        RisingPositive   = 0,
        /// <summary>
        /// For edge and transition trigger on falling edge and for pulse trigger on negative pulse.
        /// </summary>
        FallingNegative  = 1,
    }

    /// <summary>
    /// Analog input trigger length condition
    /// </summary>
    public enum TRIGLEN : int {
        /// <summary>
        /// Trigger immediately when a shorter pulse or transition time is detected.
        /// </summary>
        Less       = 0,
        /// <summary>
        /// Trigger immediately as the pulse length or transition time is reached.
        /// </summary>
        Timeout    = 1,
        /// <summary>
        /// Trigger when the length/time is reached and pulse or transition has ended.
        /// </summary>
        More       = 2,
    }

    /// <summary>
    /// error codes for DWF Public API
    /// </summary>
    public enum DWFERC : int {
        /// <summary>
        /// No error occurred
        /// </summary>
        NoErc                  = 0,
        /// <summary>
        /// API waiting on pending API timed out
        /// </summary>
        UnknownError           = 1,
        /// <summary>
        /// API waiting on pending API timed out
        /// </summary>
        ApiLockTimeout = 2,
        /// <summary>
        /// Device already opened
        /// </summary>
        AlreadyOpened = 3,
        /// <summary>
        /// Device not supported
        /// </summary>
        NotSupported = 4,
        /// <summary>
        /// Invalid parameter sent in API call
        /// </summary>
        InvalidParameter0 = 0x10,
        /// <summary>
        /// Invalid parameter sent in API call
        /// </summary>
        InvalidParameter1 = 0x11,
        /// <summary>
        /// Invalid parameter sent in API call
        /// </summary>
        InvalidParameter2 = 0x12,
        /// <summary>
        /// Invalid parameter sent in API call
        /// </summary>
        InvalidParameter3 = 0x13,
        /// <summary>
        /// Invalid parameter sent in API call
        /// </summary>
        InvalidParameter4 = 0x14,
    }

    /// <summary>
    /// analog out signal types
    /// </summary>
    public enum FUNC : byte {
        /// <summary>
        /// Generate DC value set as offset.
        /// </summary>
        DC       = 0,
        /// <summary>
        /// Generate sine waveform.
        /// </summary>
        Sine     = 1,
        /// <summary>
        /// Generate square waveform.
        /// </summary>
        Square   = 2,
        /// <summary>
        /// Generate triangle waveform.
        /// </summary>
        Triangle = 3,
        /// <summary>
        /// Generate a waveform with a ramp-up voltage at the beginning.
        /// </summary>
        RampUp   = 4,
        /// <summary>
        /// Generate a waveform with a ramp-down voltage at the end.
        /// </summary>
        RampDown = 5,
        /// <summary>
        /// Generate noise waveform from random samples.
        /// </summary>
        Noise    = 6,
        /// <summary>
        /// Generate waveform from custom repeated data.
        /// </summary>
        Custom   = 30,
        /// <summary>
        /// Generate waveform from custom data in stream play style.
        /// </summary>
        Play     = 31,
    }

    /// <summary>
    /// analog io channel node types
    /// </summary>
    public enum ANALOGIO : byte {
        /// <summary>
        /// Enable I/O node; used to enable a power supply, reference voltage, etc.
        /// </summary>
        Enable       = 1,
        /// <summary>
        /// Voltage I/O node; used to input/output voltage levels.
        /// </summary>
        Voltage      = 2,
        /// <summary>
        /// Current I/O node; used to input/output current levels.
        /// </summary>
        Current      = 3,
        /// <summary>
        /// FIXME: what is this?
        /// </summary>
        Power        = 4,
        /// <summary>
        /// Temperature I/O node; used to retrieve read values from a temperature sensor.
        /// </summary>
        Temperature  = 5,
    }

    /// <summary>
    /// Analog out node types.
    /// </summary>
    public enum ANALOGOUTNODE : int {
        Carrier  = 0,
        FM       = 1,
        AM       = 2,
    }

    /// <summary>
    /// Digital input clock source.
    /// </summary>
    public enum DIGITALINCLOCKSOURCE : int {
        /// <summary>
        /// Internal clock.
        /// </summary>
        Internal = 0,
        /// <summary>
        /// External clock.
        /// </summary>
        External = 1,
    }

    /// <summary>
    /// Digital input sample mode.
    /// </summary>
    public enum DIGITALINSAMPLEMODE : int {
        /// <summary>
        /// Stores one sample on every divider clock pulse.
        /// </summary>
        Simple   = 0,
        /// <summary>
        /// Stores alternating noise and sample values, where noise is more than one transition between two samples.
        /// This could indicate glitches or ringing. It is available when sample rate is less than maximum clock frequency, divider is greater than one.
        /// Note:
        /// alternate samples: noise|sample|noise|sample|...  
        /// where noise is more than 1 transition between 2 samples
        /// </summary>
        Noise    = 1,
    }

    /// <summary>
    /// Digital output mode.
    /// </summary>
    public enum DIGITALOUTOUTPUT : int {
        /// <summary>
        /// Default setting.
        /// </summary>
        PushPull   = 0,
        /// <summary>
        /// External pull needed.
        /// </summary>
        OpenDrain  = 1,
        /// <summary>
        /// External pull needed.
        /// </summary>
        OpenSource = 2,
        /// <summary>
        /// Available with custom and random types.
        /// </summary>
        ThreeState = 3,
    }

    /// <summary>
    /// Digital output type.
    /// </summary>
    public enum DIGITALOUTTYPE : int {
        /// <summary>
        /// Frequency = internal frequency/divider/(low + high counter).
        /// </summary>
        Pulse      = 0,
        /// <summary>
        /// Sample rate = internal frequency / divider.
        /// </summary>
        Custom     = 1,
        /// <summary>
        /// Random update rate = internal frequency/divider/counter alternating between low and high values.
        /// </summary>
        Random     = 2,
    }

    /// <summary>
    /// Output while not running.
    /// </summary>
    public enum DIGITALOUTIDLE : int {
        /// <summary>
        /// Output initial value.
        /// </summary>
        Init     = 0,
        /// <summary>
        /// Low level.
        /// </summary>
        Low      = 1,
        /// <summary>
        /// High level.
        /// </summary>
        High     = 2,
        /// <summary>
        /// Three state.
        /// </summary>
        Zet      = 3,
    }

    /// <summary>
    /// Device id and version.
    /// </summary>
    public class DeviceIdVersion
    {
        /// <summary>
        /// Device Id.
        /// </summary>
        public DEVID Id;
        /// <summary>
        /// Device version.
        /// </summary>
        public DEVVER Version;
    }

    /// <summary>
    /// Status of (analog) input.
    /// </summary>
    public class DataStatusRecord
    {
        /// <summary>
        /// Available number of samples.
        /// </summary>
        public int Available;
        /// <summary>
        /// Samples lost after the last check
        /// </summary>
        public int Lost;
        /// <summary>
        /// Number of samples that could be corrupt.
        /// </summary>
        public int Corrupt;
    }

    /// <summary>
    /// Range (min,max) and number of steps.
    /// </summary>
    public class RangeSteps<Tminmax, Tsteps>
    {
        /// <summary>
        /// Minimum value.
        /// </summary>
        public Tminmax Min;
        /// <summary>
        /// Maximum value.
        /// </summary>
        public Tminmax Max;
        /// <summary>
        /// Number of steps.
        /// </summary>
        public Tsteps Steps;
    }

    /// <summary>
    /// Range, from minimum to maximum.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Range<T>
    {
        /// <summary>
        /// Minimum value.
        /// </summary>
        public T Min;
        /// <summary>
        /// Maximum value.
        /// </summary>
        public T Max;
    }

    /// <summary>
    /// Whether Master Enable Setting and Master Enable Status are supported.
    /// The Master Enable setting is essentially a software switch that “enables” or “turns on” the AnalogIO channels.
    /// If supported, the status of this Master Enable switch (Enabled/Disabled) can be queried by calling AnalogIOEnableStatus.
    /// </summary>
    public class MasterEnableInfo
    {
        /// <summary>
        /// True when the master enable setting is supported.
        /// </summary>
        public bool Set;
        /// <summary>
        /// True when the status of the master enable can be read.
        /// </summary>
        public bool Status;
    }


    /// <summary>
    /// Channel name and label.
    /// </summary>
    public class ChannelName
    {
        /// <summary>
        /// Channel name (long text).
        /// </summary>
        public string Name;
        /// <summary>
        /// Channel label (short text, printed on the device).
        /// </summary>
        public string Label;
    }

    /// <summary>
    /// Name and units of the node.
    /// </summary>
    public class NodeName
    {
        /// <summary>
        /// Name of a node ("Voltage, "Current").
        /// </summary>
        public string Name;
        /// <summary>
        /// Units of a node ("V", "A").
        /// </summary>
        public string Units;
    }

    /// <summary>
    /// Digital input triggers.
    /// The bits represent pins.
    /// </summary>
    public class DigitalTriggerInfo {
        /// <summary>
        /// Low state triggers.
        /// </summary>
        public uint LevelLow;
        /// <summary>
        /// High state triggers.
        /// </summary>
        public uint LevelHigh;
        /// <summary>
        /// Rising edge triggers.
        /// </summary>
        public uint EdgeRise;
        /// <summary>
        /// Falling edge triggers.
        /// </summary>
        public uint EdgeFall;
    }

    /// <summary>
    /// Initial state of a digital out counter.
    /// </summary>
    public class CounterInitialState
    {
        /// <summary>
        /// Start high.
        /// </summary>
        public bool IsHigh;
        /// <summary>
        /// Divider initial value.
        /// </summary>
        public uint Divider;
    }

    /// <summary>
    /// Waveforms SDK functions, wrapped for automatic checking of the return value and for simplified parameter passing.
    /// </summary>
    public static partial class Dwf
    {
        public const HDWF HdwfNone = 0;

        // ========================================================================================
        /// <summary>
        /// Internal substitution for the tuple of second order.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        class Tuple2<T1, T2>
        {
            public readonly T1 Item1;
            public readonly T2 Item2;
            public Tuple2(T1 item1, T2 item2)
            {
                Item1 = item1;
                Item2 = item2;
            }
        }
        unsafe delegate bool FuncBytePointer(sbyte* stringBuffer);
        unsafe delegate bool FuncIntegerIndexBytePointer(int deviceIndex, sbyte* stringBuffer);
        delegate bool FuncBoolean();
        delegate T FuncObject<T>();
        delegate bool FuncHdwf(HDWF hdwf);

        delegate bool FuncHdwfSetObject<T>(HDWF hdwf, T t);
        delegate bool FuncHdwfGetObject<T>(HDWF hdwf, ref T t) where T : new();
        delegate bool FuncHdwfGetRange<T>(HDWF hdwf, ref T tMin, ref T tMax) where T : new();
        delegate bool FuncHdwfGetRangeSteps<Tminmax, Tsteps>(HDWF hdwf, ref Tminmax min, ref Tminmax max, ref Tsteps steps);

        delegate bool FuncHdwfIndexSetObject<Tindex, Tvalue>(HDWF hdwf, Tindex index, Tvalue t);
        delegate bool FuncHdwfIndexGetObject<Tindex, Treturn>(HDWF hdwf, Tindex index, ref Treturn t) where Treturn : new();
        delegate bool FuncHdwfIndexGetRange<Tindex, Treturn>(HDWF hdwf, Tindex index, ref Treturn tMin, ref Treturn tMax) where Treturn : new();
        delegate bool FuncHdwfIndexGetDouble2x(HDWF hdwf, int index, ref double psecMin, ref double psecMax);
        delegate bool FuncHdwIndexGetRangeSteps<Tminmax, Tsteps>(HDWF hdwf, int index, ref Tminmax min, ref Tminmax max, ref Tsteps steps);

        delegate bool FuncHdwfIndexNodeSetObject<Tnode, Tvalue>(HDWF hdwf, int index, Tnode node, Tvalue t);
        delegate bool FuncHdwfIndexNodeGetObject<Tnode, Treturn>(HDWF hdwf, int index, Tnode node, ref Treturn t) where Treturn : new();
        delegate bool FuncHdwfIndexNodeGetRange<Tnode, Treturn>(HDWF hdwf, int index, Tnode node, ref Treturn tMin, ref Treturn tMax) where Treturn : new();
        delegate bool FuncHdwfIndexNodeGetRangeSteps<Tnode, Tminmax, Tsteps>(HDWF hdwf, int index, Tnode node, ref Tminmax tMin, ref Tminmax tMax, ref Tsteps tSteps)
            where Tminmax : new()
            where Tsteps : new();


        // ========================================================================================
        /// <summary>
        /// Macro used to verify if bit is 1 or 0 in given bit field
        /// </summary>
        /// <param name="fs">Bit field</param>
        /// <param name="bit">Bit number in the range 0..31.</param>
        /// <returns>True iff the given bit is set, false otherwise.</returns>
        public static bool IsBitSet(int fs, int bit)
        {
            return ((fs & (1 << bit)) != 0);
        }

        // ========================================================================================
        /// <summary>
        /// Construct an error message in the form: function name + error message + 
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        static string ExceptionMessage(string functionName)
        {
            var sb = new StringBuilder();
            sb.Append(functionName);
            sb.Append(':');

            DWFERC? error_code = null;
            Exception error_code_exception = null;

            try {
                error_code = GetLastError();
            } catch (Exception ex) {
                error_code_exception = ex;
            }

            try
            {
                sb.Append(GetLastErrorMsg().Trim());
            }
            catch (Exception ex)
            {
                sb.Append("(internal error in GetLastErrorMsg:");
                sb.Append(ex.Message);
                sb.Append(')');
            }

            if (error_code_exception==null)
            {
                if (error_code.HasValue && error_code.Value != DWFERC.UnknownError)
                {
                    sb.Append('(');
                    sb.Append(error_code.Value.ToString());
                    sb.Append(')');
                }
            }
            else
            {
                sb.Append("(internal error in GetLastError:");
                sb.Append(error_code_exception.Message);
                sb.Append(")");
            }

            return sb.ToString();
        }

        // ========================================================================================
        /// <summary>
        /// Call an external function expecting a buffer for a string and return the string.
        /// </summary>
        /// <param name="nbytes">Size of the buffer in bytes.</param>
        /// <param name="f">External function to call.</param>
        /// <param name="functionName">Name of the function; this is shown in the error message, if any.</param>
        /// <returns>String returned by the external function.</returns>
        unsafe static string StringFromExternalFunction(int nbytes, FuncBytePointer f, string functionName)
        {
            var sb = new StringBuilder();
            var buffer = stackalloc sbyte[nbytes];
            if (f(buffer))
            {
                return new string(buffer, 0, nbytes, Encoding.ASCII);
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        /// <summary>
        /// Call an external function expecting a device index and a buffer for a string. The string is returned.
        /// </summary>
        /// <param name="nbytes">Size of the buffer, in bytes.</param>
        /// <param name="f">External function to call.</param>
        /// <param name="functionName">Name of the function; this is shown in the error message, if any.</param>
        /// <param name="deviceIndex">Index of the device; this is passed on to the external function.</param>
        unsafe static string StringFromExternalFunctionByDeviceIndex(int nbytes, FuncIntegerIndexBytePointer f, string functionName, int deviceIndex)
        {
            var sb = new StringBuilder();
            var buffer = stackalloc sbyte[nbytes];
            if (f(deviceIndex, buffer))
            {
                return new string(buffer, 0, nbytes, Encoding.ASCII);
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        /// <summary>
        /// Call an external function, check the return value and return the actual return value.
        /// </summary>
        /// <typeparam name="T">The type of the return value of the external function.</typeparam>
        /// <param name="f">External function to call.</param>
        /// <param name="functionName">Name of the function; this is shown in the error message, if any.</param>
        /// <returns>Actual return value of the external function.</returns>
        static T CheckApiFunction<T>(FuncObject<Tuple2<bool, T>> f, string functionName)
        {
            var r = f();
            if (r.Item1)
            {
                return r.Item2;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static void CheckApiProcedureHdwfSet<T>(FuncHdwfSetObject<T> f, string functionName, HDWF hdwf, T t)
        {
            var ok = f(hdwf, t);
            if (!ok)
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static T CheckApiFunctionHdwfGet<T>(FuncHdwfGetObject<T> f, string functionName, HDWF hdwf) where T : new()
        {
            T r = new T();
            var ok = f(hdwf, ref r);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static Range<T> CheckApiFunctionHdwfGetRange<T>(FuncHdwfGetRange<T> f, string functionName, HDWF hdwf) where T : new()
        {
            var r = new Range<T>();
            var ok = f(hdwf, ref r.Min, ref r.Max);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static RangeSteps<Tminmax, Tsteps> CheckApiFunctionHdwfGetRangeSteps<Tminmax, Tsteps>(FuncHdwfGetRangeSteps<Tminmax, Tsteps> f, string functionName, HDWF hdwf)
        {
            var r = new RangeSteps<Tminmax, Tsteps>();
            var ok = f(hdwf, ref r.Min, ref r.Max, ref r.Steps);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static void CheckApiProcedureHdwfIndexSet<Tindex, Tvalue>(FuncHdwfIndexSetObject<Tindex, Tvalue> f, string functionName, HDWF hdwf, Tindex index, Tvalue t)
        {
            var ok = f(hdwf, index, t);
            if (!ok)
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static Treturn CheckApiFunctionHdwfIndexGet<Tindex, Treturn>(FuncHdwfIndexGetObject<Tindex,Treturn> f, string functionName, HDWF hdwf, Tindex index) where Treturn : new()
        {
            Treturn r = new Treturn();
            var ok = f(hdwf, index, ref r);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static Range<Treturn> CheckApiFunctionHdwfIndexGetRange<Tindex, Treturn>(FuncHdwfIndexGetRange<Tindex, Treturn> f, string functionName, HDWF hdwf, Tindex index) where Treturn : new()
        {
            var r = new Range<Treturn>();
            var ok = f(hdwf, index, ref r.Min, ref r.Max);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static RangeSteps<Tminmax, Tsteps> CheckApiFunctionHdwfIndexGetRangeSteps<Tminmax, Tsteps>(FuncHdwIndexGetRangeSteps<Tminmax, Tsteps> f, string functionName, HDWF hdwf, int index)
            where Tminmax : new()
            where Tsteps: new()
        {
            var r = new RangeSteps<Tminmax, Tsteps>();
            var ok = f(hdwf, index, ref r.Min, ref r.Max, ref r.Steps);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static void CheckApiProcedureHdwfIndexNodeSet<Tnode, Tvalue>(FuncHdwfIndexNodeSetObject<Tnode, Tvalue> f, string functionName, HDWF hdwf, int index, Tnode node, Tvalue t)
        {
            var ok = f(hdwf, index, node, t);
            if (!ok)
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static Treturn CheckApiFunctionHdwfIndexNodeGet<Tnode, Treturn>(FuncHdwfIndexNodeGetObject<Tnode, Treturn> f, string functionName, HDWF hdwf, int index, Tnode node) where Treturn : new()
        {
            Treturn r = new Treturn();
            var ok = f(hdwf, index, node, ref r);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static Range<Treturn> CheckApiFunctionHdwfIndexNodeGetRange<Tnode, Treturn>(FuncHdwfIndexNodeGetRange<Tnode, Treturn> f, string functionName, HDWF hdwf, int index, Tnode node) where Treturn : new()
        {
            var r = new Range<Treturn>();
            var ok = f(hdwf, index, node, ref r.Min, ref r.Max);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static RangeSteps<Tminmax, Tsteps> CheckApiFunctionHdwfIndexNodeGetRangeSteps<Tnode, Tminmax, Tsteps>(FuncHdwfIndexNodeGetRangeSteps<Tnode, Tminmax, Tsteps> f, string functionName, HDWF hdwf, int index, Tnode node)
            where Tminmax : new()
            where Tsteps : new()
        {
            var r = new RangeSteps<Tminmax, Tsteps>();
            var ok = f(hdwf, index, node, ref r.Min, ref r.Max, ref r.Steps);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        /// <summary>
        /// Call an external function and check the return value.
        /// </summary>
        /// <param name="f">External function to call.</param>
        /// <param name="functionName">Name of the function; this is shown in the error message, if any.</param>
        static void CheckApiProcedure(FuncBoolean f, string functionName)
        {
            var r = f();
            if (!r)
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        // ========================================================================================
        static void CheckApiProcedureHdwf(FuncHdwf f, string functionName, HDWF hdwf)
        {
            var ok = f(hdwf);
            if (!ok)
            {
                throw new ApplicationException(ExceptionMessage(functionName));
            }
        }

        #region Error and version APIs
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint="FDwfGetLastError")]
        extern static bool FDwfGetLastError(ref DWFERC pdwferc);

        /// <summary>
        /// Retrieve the last error code in the calling process.
        /// The error code is cleared when other API functions are called and is only set when an API function fails during execution.
        /// 
        /// See the enum DWFERC.
        /// </summary>
        /// <returns>Error code</returns>
        public static DWFERC GetLastError()
        {
            var r = DWFERC.NoErc;
            if (FDwfGetLastError(ref r))
            {
                return r;
            }
            else
            {
                throw new ApplicationException("Internal error #1");
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint="FDwfGetLastErrorMsg")]
        extern unsafe static bool FDwfGetLastErrorMsg(byte* szError /* [512] */);

        /// <summary>
        /// retrieve the last error message.
        /// This may consist of a chain of messages, separated by a new line character, that describe the events leading to the failure.
        /// </summary>
        /// <returns>Error string.</returns>
        public static string GetLastErrorMsg()
        {
            // This has to be independent, because it is called by the function "ExceptionMessage"
            unsafe {
                const int nbytes = 512;
                var sb = new StringBuilder();
                var buffer = stackalloc byte[nbytes];
                if (FDwfGetLastErrorMsg(buffer))
                {
                    for (int i = 0; buffer[i] != 0 && i < nbytes; ++i)
                    {
                        sb.Append((char)buffer[i]);
                    }
                    return sb.ToString();
                }
                else
                {
                    throw new ApplicationException("Internal error #2");
                }
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfGetVersion")]
        extern unsafe static bool FDwfGetVersion(sbyte* szVersion/* [32] */);

        /// <summary>
        /// Returns DLL version, for instance: "2.4.3"
        /// </summary>
        /// <returns>DLL version.</returns>
        public static string GetVersion()
        {
            unsafe
            {
                return StringFromExternalFunction(32, FDwfGetVersion, "FDwfGetVersion");
            }
        }
#endregion // Error and version APIs

        #region DEVICE MANAGEMENT FUNCTIONS
        // ========================================================================================
        // Enumeration:
        [DllImport("dwf.dll", EntryPoint = "FDwfEnum")]
        extern static bool FDwfEnum(ENUMFILTER enumfilter, ref int pcDevice);
        /// <summary>
        /// The EnumXXX functions are used to discover all connected, compatible devices.
        /// 
        /// Calling the function Enumerate will build an internal list of detected devices filtered by the enumfilter parameter.
        /// The function Enumerate must be called before using other EnumXXX functions because they obtain information about
        /// enumerated devices from this list identified by the device index.
        /// </summary>
        /// <param name="enumfilter">Filter value to be used for device enumeration. Use the ENUMFILTER.All constant to discover all compatible devices</param>
        /// <returns>Count of found devices</returns>
        public static int Enumerate(ENUMFILTER enumfilter)
        {
            int r = 0;
            if (FDwfEnum(enumfilter, ref r))
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfEnum"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumDeviceType")]
        extern static bool FDwfEnumDeviceType(int idxDevice, ref DEVID pDeviceId, ref DEVVER pDeviceRevision);

        /// <summary>
        /// Return the device ID and version ID.
        /// </summary>
        /// <param name="idxDevice">Index of the enumerated device for which to return the type and revision.</param>
        /// <returns>Tuple of device id and device revision.</returns>
        public static DeviceIdVersion EnumDeviceType(int idxDevice)
        {
            var devid = DEVID.EExplorer;
            var devver = DEVVER.EExplorerC;
            if (FDwfEnumDeviceType(idxDevice, ref devid, ref devver))
            {
                return new DeviceIdVersion() { Id = devid, Version = devver };
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfEnumDeviceType"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumDeviceIsOpened")]
        extern static bool FDwfEnumDeviceIsOpened(int idxDevice, ref bool pfIsUsed);

        /// <summary>
        /// Retrieve a Boolean specifying if a device is already opened by this, or any other process.
        /// </summary>
        /// <param name="idxDevice">Index of the enumerated device.</param>
        /// <returns>Boolean indicating if the device is in use.</returns>
        public static bool EnumDeviceIsOpened(int idxDevice)
        {
            bool r = false;
            if (FDwfEnumDeviceIsOpened(idxDevice, ref r))
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfEnumDeviceIsOpened"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumUserName")]
        static extern unsafe bool FDwfEnumUserName(int idxDevice, sbyte* szUserName /* [32] */);

        /// <summary>
        /// Retrieve the user name of the enumerated device.
        /// </summary>
        /// <param name="idxDevice">Index of the enumerated device.</param>
        /// <returns>The user name string</returns>
        public static string EnumUserName(int idxDevice)
        {
            unsafe
            {
                return StringFromExternalFunctionByDeviceIndex(32, FDwfEnumUserName, "FDwfEnumUserName", idxDevice);
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumUserName")]
        static unsafe extern bool FDwfEnumDeviceName(int idxDevice, sbyte* szDeviceName /* [32] */);

        /// <summary>
        /// Retrieve the device name of the enumerated device.
        /// </summary>
        /// <param name="idxDevice">Index of the enumerated device.</param>
        /// <returns>Device name</returns>
        public static string EnumDeviceName(int idxDevice)
        {
            unsafe
            {
                return StringFromExternalFunctionByDeviceIndex(32, FDwfEnumDeviceName, "FDwfEnumDeviceName", idxDevice);
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumSN")]
        static unsafe extern bool FDwfEnumSN(int idxDevice, sbyte* szSN /* [32] */);

        /// <summary>
        /// Retrieve the 12-digit, unique serial number of the enumerated device.
        /// </summary>
        /// <param name="idxDevice">Index of the enumerated device.</param>
        /// <returns>Serial number</returns>
        public static string EnumSN(int idxDevice)
        {
            unsafe
            {
                return StringFromExternalFunctionByDeviceIndex(32, FDwfEnumSN, "FDwfEnumSN", idxDevice);
            }
        }

        #endregion // DEVICE MANAGEMENT FUNCTIONS

        #region OPEN/CLOSE
        // Open/Close:
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceOpen")]
        static extern bool FDwfDeviceOpen(int idxDevice, ref HDWF phdwf);

        /// <summary>
        /// Opens a device identified by the enumeration index and retrieves a handle.
        /// To automatically enumerate all connected devices and open the first discovered device, use index -1.
        /// </summary>
        /// <param name="idxDevice">Index of the enumerated device.</param>
        /// <returns>Opened interface handle</returns>
        public static HDWF DeviceOpen(int idxDevice)
        {
            return CheckApiFunction<HDWF>(() =>
            {
                HDWF r = HdwfNone;
                var ok = FDwfDeviceOpen(idxDevice, ref r);
                return new Tuple2<bool, HDWF>(ok, r);
            }, "FDwfDeviceOpen");
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceClose")]
        static extern bool FDwfDeviceClose(HDWF hdwf);

        /// <summary>
        /// close an interface handle when access to the device is no longer needed.
        /// Once the function above has returned, the specified interface handle can no longer be used to access the device.
        /// </summary>
        /// <param name="hdwf">Interface handle to be closed.</param>
        public static void DeviceClose(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfDeviceClose, "FDwfDeviceClose", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceCloseAll")]
        static extern bool FDwfDeviceCloseAll();

        /// <summary>
        /// Close all opened devices by the calling process. It does not close all devices across all processes.
        /// </summary>
        public static void DeviceCloseAll()
        {
            CheckApiProcedure(FDwfDeviceCloseAll, "FDwfDeviceCloseAll");
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceAutoConfigureSet")]
        static extern bool FDwfDeviceAutoConfigureSet(HDWF hdwf, bool fAutoConfigure);

        /// <summary>
        /// Enables or disables the AutoConfig setting for a specific device.
        /// When this setting is enabled, the device is automatically configured every time an instrument parameter is set.
        /// For example, when AutoConfigure is enabled, FDwfAnalogOutConfigure does not need to be called after FDwfAnalogOutRunSet.
        /// This adds latency to every Set function; just as much latency as calling the corresponding Configure function directly afterward.
        /// </summary>
        /// <param name="hdwf">Interface handle</param>
        /// <param name="fAutoConfigure">TRUE if enabled, FALSE if disabled.</param>
        public static void DeviceAutoConfigureSet(HDWF hdwf, bool fAutoConfigure)
        {
            CheckApiProcedureHdwfSet<bool>(FDwfDeviceAutoConfigureSet, "FDwfDeviceAutoConfigureSet", hdwf, fAutoConfigure);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceAutoConfigureGet")]
        static extern bool FDwfDeviceAutoConfigureGet(HDWF hdwf, ref bool pfAutoConfigure);

        /// <summary>
        /// Returns the AutoConfig setting in the device. See the function description for FDwfDeviceAutoConfigureSet for details on this setting.
        /// </summary>
        /// <param name="hdwf">Interface handle</param>
        /// <returns>Current value of this option.</returns>
        public static bool DeviceAutoConfigureGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<bool>(FDwfDeviceAutoConfigureGet, "FDwfDeviceAutoConfigureGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceReset")]
        static extern bool FDwfDeviceReset(HDWF hdwf);

        /// <summary>
        /// Resets and configures (by default, having auto configure enabled) all device and instrument parameters to default values.
        /// </summary>
        /// <param name="hdwf">Interface handle</param>
        public static void DeviceReset(HDWF hdwf)
        {
            CheckApiProcedure(() => FDwfDeviceReset(hdwf), "FDwfDeviceReset");
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceTriggerInfo")]
        static extern bool FDwfDeviceTriggerInfo(HDWF hdwf, ref int pfstrigsrc); // use IsBitSet

        /// <summary>
        /// Returns the supported trigger source options for the global trigger bus. They are returned (by reference) as a bit field.
        /// This bit field can be parsed using the IsBitSet Macro. Individual bits are defined using the TRIGSRC constants in dwf.h.
        /// </summary>
        /// <param name="hdwf">Interface handle</param>
        /// <returns>List of supported trigger sources</returns>
        public static List<TRIGSRC> DeviceTriggerInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfDeviceTriggerInfo, "FDwfDeviceTriggerInfo", hdwf);
            return TrigsrcsOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceTriggerSet")]
        static extern bool FDwfDeviceTriggerSet(HDWF hdwf, int idxPin, TRIGSRC trigsrc);

        /// <summary>
        /// Configure the trigger I/O pin with a specific TRIGSRC option.
        /// </summary>
        /// <param name="hdwf">Interface handle</param>
        /// <param name="idxPin">External trigger, I/O pin index.</param>
        /// <param name="trigsrc">Trigger source to set.</param>
        public static void DeviceTriggerSet(HDWF hdwf, int idxPin, TRIGSRC trigsrc)
        {
            CheckApiProcedure(() => FDwfDeviceTriggerSet(hdwf, idxPin, trigsrc), "FDwfDeviceTriggerSet");
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceTriggerGet")]
        static extern bool FDwfDeviceTriggerGet(HDWF hdwf, int idxPin, ref TRIGSRC ptrigsrc);

        /// <summary>
        /// Returns the configured trigger setting for a trigger I/O pin.
        /// The trigger source can be â€œnoneâ€, an internal instrument, or an external trigger.
        /// </summary>
        /// <param name="hdwf">Interface handle</param>
        /// <param name="idxPin">External trigger, I/O pin index</param>
        /// <returns>current trigger source</returns>
        public static TRIGSRC DeviceTriggerGet(HDWF hdwf, int idxPin)
        {
            return CheckApiFunctionHdwfIndexGet<int, TRIGSRC>(FDwfDeviceTriggerGet, "FDwfDeviceTriggerGet", hdwf, idxPin);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDeviceTriggerPC")]
        static extern bool FDwfDeviceTriggerPC(HDWF hdwf);

        /// <summary>
        /// Generates one pulse on the PC trigger line.
        /// </summary>
        /// <param name="hdwf">Interface handle</param>
        public static void DeviceTriggerPC(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfDeviceTriggerPC, "FDwfDeviceTriggerPC", hdwf);
        }
        #endregion OPEN/CLOSE

        #region ANALOG IN INSTRUMENT FUNCTIONS
        // Control and status: 
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInReset")]
        static extern bool FDwfAnalogInReset(HDWF hdwf);

        /// <summary>
        /// Resets and configures (by default, having auto configure enabled) all AnalogIn instrument parameters to default values.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        public static void AnalogInReset(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfAnalogInReset, "FDwfAnalogInReset", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInConfigure")]
        static extern bool FDwfAnalogInConfigure(HDWF hdwf, bool fReconfigure, bool fStart);

        /// <summary>
        /// Configure the instrument and start or stop the acquisition. To reset the Auto trigger timeout, set fReconfigure to TRUE.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="fReconfigure">Configure the device.</param>
        /// <param name="fStart">Start the acquisition.</param>
        public static void AnalogInConfigure(HDWF hdwf, bool fReconfigure, bool fStart)
        {
            CheckApiProcedure(() => FDwfAnalogInConfigure(hdwf, fReconfigure, fStart), "FDwfAnalogInConfigure");
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInStatus")]
        static extern bool FDwfAnalogInStatus(HDWF hdwf, bool fReadData, ref STATE psts);


        /// <summary>
        /// Check the state of the acquisition. To read the data from the device, set fReadData to TRUE.
        /// For single acquisition mode, the data will be read only when the acquisition is finished.
        /// 
        /// Note: To ensure simultaneity of information and data, all of the following AnalogInStatus*** functions do not
        /// communicate with the device. These functions only return information and data from the last
        /// FDwfAnalogInStatus call.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="fReadData">TRUE if data should be read.</param>
        /// <returns>Acquisition state</returns>
        public static STATE AnalogInStatus(HDWF hdwf, bool fReadData)
        {
            return CheckApiFunctionHdwfIndexGet<bool, STATE>(FDwfAnalogInStatus, "FDwfAnalogInStatus", hdwf, fReadData);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInStatusSamplesLeft")]
        static extern bool FDwfAnalogInStatusSamplesLeft(HDWF hdwf, ref int pcSamplesLeft);

        /// <summary>
        /// Retrieve the number of samples left in the acquisition.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Remaining samples to acquire.</returns>
        public static int AnalogInStatusSamplesLeft(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogInStatusSamplesLeft, "FDwfAnalogInStatusSamplesLeft", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInStatusSamplesValid")]
        static extern bool FDwfAnalogInStatusSamplesValid(HDWF hdwf, ref int pcSamplesValid);

        /// <summary>
        /// Retrieve the number of valid/acquired data samples.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Number of valid samples</returns>
        public static int AnalogInStatusSamplesValid(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogInStatusSamplesValid, "FDwfAnalogInStatusSamplesValid", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInStatusIndexWrite")]
        static extern bool FDwfAnalogInStatusIndexWrite(HDWF hdwf, ref int pidxWrite);

        /// <summary>
        /// Retrieve the buffer write pointer. This is needed in ScanScreen acquisition mode to display the scan bar.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Position of the acquisition.</returns>
        public static int AnalogInStatusIndexWrite(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogInStatusIndexWrite, "FDwfAnalogInStatusIndexWrite", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInStatusAutoTriggered")]
        static extern bool FDwfAnalogInStatusAutoTriggered(HDWF hdwf, ref bool pfAuto);

        /// <summary>
        /// Verify if the acquisition is auto triggered.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>TRUE if the acquisition was auto triggered.</returns>
        public static bool AnalogInStatusAutoTriggered(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<bool>(FDwfAnalogInStatusAutoTriggered, "FDwfAnalogInStatusAutoTriggered", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInStatusData")]
        static extern unsafe bool FDwfAnalogInStatusData(HDWF hdwf, int idxChannel, double* rgdVoltData, int cdData);

        /// <summary>
        /// Retrieve the acquired data samples from the specified idxChannel on the AnalogIn instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="cdData">Number of samples to retrieve.</param>
        /// <returns>Samples retrieved.</returns>
        public static double[] AnalogInStatusData(HDWF hdwf, int idxChannel, int cdData)
        {
            unsafe
            {
                var samples = new double[cdData];
                fixed (double* ptr = samples)
                {
                    var ok = FDwfAnalogInStatusData(hdwf, idxChannel, ptr, cdData);
                    if (ok)
                    {
                        return samples;
                    }
                    else
                    {
                        throw new ApplicationException(ExceptionMessage("FDwfAnalogInStatusData"));
                    }
                }
            }
        }

        /// <summary>
        /// Retrieve the acquired data samples from the specified idxChannel on the AnalogIn instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="rgdVoltData">Buffer to copy the acquisition data.</param>
        /// <param name="offset">Offset to the buffer.</param>
        /// <param name="cdData">Number of samples to retrieve.</param>
        public static void AnalogInStatusData(HDWF hdwf, int idxChannel, double[] rgdVoltData, int offset, int cdData)
        {
            if (offset >= 0 && cdData > 0 && offset + cdData <= rgdVoltData.Length)
            {
                unsafe
                {
                    fixed (double* ptr = rgdVoltData)
                    {
                        var ok = FDwfAnalogInStatusData(hdwf, idxChannel, ptr + offset, cdData);
                        if (!ok)
                        {
                            throw new ApplicationException(ExceptionMessage("FDwfAnalogInStatusData"));
                        }
                    }
                }
            }
            else
            {
                throw new ApplicationException("AnalogInStatusData: invalid parameters!");
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInStatusSample")]
        static extern bool FDwfAnalogInStatusSample(HDWF hdwf, int idxChannel, ref double pdVoltSample);

        /// <summary>
        /// Gets the last ADC conversion sample from the specified idxChannel on the AnalogIn instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Sample value</returns>
        public static double AnalogInStatusSample(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, double>(FDwfAnalogInStatusSample, "FDwfAnalogInStatusSample", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInStatusRecord")]
        static extern bool FDwfAnalogInStatusRecord(HDWF hdwf, ref int pcdDataAvailable, ref int pcdDataLost, ref int pcdDataCorrupt);

        /// <summary>
        /// Retrieve information about the recording process. The data loss occurs when the
        /// device acquisition is faster than the read process to PC. In this case, the device recording buffer is filled and data
        /// samples are overwritten. Corrupt samples indicate that the samples have been overwritten by the acquisition
        /// process during the previous read. In this case, try optimizing the loop process for faster execution or reduce the
        /// acquisition frequency or record length to be less than or equal to the device buffer size (record length <= buffer
        /// size/frequency
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Status of the recording process.</returns>
        public static DataStatusRecord AnalogInStatusRecord(HDWF hdwf)
        {
            var r = new DataStatusRecord() { Available = Int32.MinValue, Lost = Int32.MinValue, Corrupt = Int32.MinValue };
            var ok = FDwfAnalogInStatusRecord(hdwf, ref r.Available, ref r.Lost, ref r.Corrupt);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfAnalogInStatusRecord"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInRecordLengthSet")]
        static extern bool FDwfAnalogInRecordLengthSet(HDWF hdwf, double sLength);

        /// <summary>
        /// Set the Record length in seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="sLength">Record length to set expressed in seconds.</param>
        public static void AnalogInRecordLengthSet(HDWF hdwf, double sLength)
        {
            CheckApiProcedureHdwfSet<double>(FDwfAnalogInRecordLengthSet, "FDwfAnalogInRecordLengthSet", hdwf, sLength);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInRecordLengthGet")]
        static extern bool FDwfAnalogInRecordLengthGet(HDWF hdwf, ref double psLegth);

        /// <summary>
        /// Get the current Record length in seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>The record length</returns>
        public static double AnalogInRecordLengthGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInRecordLengthGet, "FDwfAnalogInRecordLengthGet", hdwf);
        }
        #endregion // ANALOG IN INSTRUMENT FUNCTIONS

        #region ACQUISTION CONFIGURATION

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInFrequencyInfo")]
        static extern bool FDwfAnalogInFrequencyInfo(HDWF hdwf, ref double phzMin, ref double phzMax);

        /// <summary>
        /// Retrieve the minimum and maximum (ADC frequency) settable sample frequency.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Frequency range.</returns>
        public static Range<double> AnalogInFrequencyInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRange<double>(FDwfAnalogInFrequencyInfo, "FDwfAnalogInFrequencyInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInFrequencySet")]
        static extern bool FDwfAnalogInFrequencySet(HDWF hdwf, double hzFrequency);

        /// <summary>
        /// Set the sample frequency for the instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="hzFrequency">Acquisition frequency to set.</param>
        public static void AnalogInFrequencySet(HDWF hdwf, double hzFrequency)
        {
            CheckApiProcedureHdwfSet<double>(FDwfAnalogInFrequencySet, "FDwfAnalogInFrequencySet", hdwf, hzFrequency);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInFrequencyGet")]
        static extern bool FDwfAnalogInFrequencyGet(HDWF hdwf, ref double phzFrequency);

        /// <summary>
        /// Read the configured sample frequency. The AnalogIn ADC always runs at maximum frequency,
        /// but the method in which the samples are stored in the buffer can be individually configured for each channel with FDwfAnalogInChannelFilterSet function.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Acquisition frequency.</returns>
        public static double AnalogInFrequencyGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInFrequencyGet, "FDwfAnalogInFrequencyGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInBitsInfo")]
        static extern bool FDwfAnalogInBitsInfo(HDWF hdwf, ref int pnBits); // Returns the number of ADC bits 

        /// <summary>
        /// Retrieve the number bits used by the AnalogIn ADC.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>The number of ADC bits.</returns>
        public static int AnalogInBitsInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogInBitsInfo, "FDwfAnalogInBitsInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInBufferSizeInfo")]
        static extern bool FDwfAnalogInBufferSizeInfo(HDWF hdwf, ref int pnSizeMin, ref int pnSizeMax);

        /// <summary>
        /// Retrieve the minimum and maximum allowable buffer sizes for the instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Buffer size range.</returns>
        public static Range<int> AnalogInBufferSizeInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRange<int>(FDwfAnalogInBufferSizeInfo, "FDwfAnalogInBufferSizeInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInBufferSizeSet")]
        static extern bool FDwfAnalogInBufferSizeSet(HDWF hdwf, int nSize);

        /// <summary>
        /// Adjust the AnalogIn instrument buffer size.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="nSize">Buffer size to set.</param>
        public static void AnalogInBufferSizeSet(HDWF hdwf, int nSize)
        {
            CheckApiProcedureHdwfSet<int>(FDwfAnalogInBufferSizeSet, "FDwfAnalogInBufferSizeSet", hdwf, nSize);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInBufferSizeGet")]
        static extern bool FDwfAnalogInBufferSizeGet(HDWF hdwf, ref int pnSize);

        /// <summary>
        /// Retrieve the used AnalogIn instrument buffer size.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current buffer size.</returns>
        public static int AnalogInBufferSizeGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogInBufferSizeGet, "FDwfAnalogInBufferSizeGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInAcquisitionModeInfo")]
        static extern bool FDwfAnalogInAcquisitionModeInfo(HDWF hdwf, ref int pfsacqmode); // use IsBitSet

        /// <summary>
        /// Receive the supported AnalogIn acquisition modes.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>List of the supported acquisition modes</returns>
        public static List<ACQMODE> AnalogInAcquisitionModeInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfAnalogInAcquisitionModeInfo, "FDwfAnalogInAcquisitionModeInfo", hdwf);
            return AcqmodesOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInAcquisitionModeSet")]
        static extern bool FDwfAnalogInAcquisitionModeSet(HDWF hdwf, ACQMODE acqmode);

        /// <summary>
        /// Set the acquisition mode.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="acqmode">Acquisition mode to set.</param>
        public static void AnalogInAcquisitionModeSet(HDWF hdwf, ACQMODE acqmode)
        {
            CheckApiProcedureHdwfSet<ACQMODE>(FDwfAnalogInAcquisitionModeSet, "FDwfAnalogInAcquisitionModeSet", hdwf, acqmode);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInAcquisitionModeGet")]
        static extern bool FDwfAnalogInAcquisitionModeGet(HDWF hdwf, ref ACQMODE pacqmode);

        /// <summary>
        /// Retrieve the acquisition mode.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>The current acquisition mode.</returns>
        public static ACQMODE AnalogInAcquisitionModeGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<ACQMODE>(FDwfAnalogInAcquisitionModeGet, "FDwfAnalogInAcquisitionModeGet", hdwf);
        }
        #endregion // ACQUISTION CONFIGURATION

        #region CHANNEL CONFIGURATION

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelCount")]
        static extern bool FDwfAnalogInChannelCount(HDWF hdwf, ref int pcChannel);

        /// <summary>
        /// Read the number of AnalogIn channels of the device.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>The number of channels.</returns>
        public static int AnalogInChannelCount(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogInChannelCount, "FDwfAnalogInChannelCount", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelEnableSet")]
        static extern bool FDwfAnalogInChannelEnableSet(HDWF hdwf, int idxChannel, bool fEnable);

        /// <summary>
        /// Enable or disable the specified AnalogIn channel.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Index of channel to enable/disable.</param>
        /// <param name="fEnable">Set TRUE to enable, FALSE to disable</param>
        public static void AnalogInChannelEnableSet(HDWF hdwf, int idxChannel, bool fEnable)
        {
            CheckApiProcedureHdwfIndexSet<int, bool>(FDwfAnalogInChannelEnableSet, "FDwfAnalogInChannelEnableSet", hdwf, idxChannel, fEnable);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelEnableGet")]
        static extern bool FDwfAnalogInChannelEnableGet(HDWF hdwf, int idxChannel, ref bool pfEnable);

        /// <summary>
        /// Get the current enable/disable status of the specified AnalogIn channel.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Index of channel.</param>
        /// <returns>Enable/disable status of channel.</returns>
        public static bool AnalogInChannelEnableGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, bool>(FDwfAnalogInChannelEnableGet, "FDwfAnalogInChannelEnableGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelFilterInfo")]
        static extern bool FDwfAnalogInChannelFilterInfo(HDWF hdwf, ref int pfsfilter); // use IsBitSet

        /// <summary>
        /// Retrieve the supported acquisition filters.
        /// 
        /// The filter can be used when the acquisition frequency (FDwfAnalogInFrequencySet)
        /// is less than the ADC frequency (maximum acquisition frequency).
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>List of supported filters.</returns>
        public static List<FILTER> AnalogInChannelFilterInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfAnalogInChannelFilterInfo, "FDwfAnalogInChannelFilterInfo", hdwf);
            return FiltersOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelFilterSet")]
        static extern bool FDwfAnalogInChannelFilterSet(HDWF hdwf, int idxChannel, FILTER filter);

        /// <summary>
        /// Set the acquisition filter for each AnalogIn channel. With channel index -1, each
        /// enabled AnalogIn channel filter will be configured to use the same, new option.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="filter">Acquisition sample filter to set</param>
        public static void AnalogInChannelFilterSet(HDWF hdwf, int idxChannel, FILTER filter)
        {
            CheckApiProcedureHdwfIndexSet<int, FILTER>(FDwfAnalogInChannelFilterSet, "FDwfAnalogInChannelFilterSet", hdwf, idxChannel, filter);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelFilterGet")]
        static extern bool FDwfAnalogInChannelFilterGet(HDWF hdwf, int idxChannel, ref FILTER pfilter);

        /// <summary>
        /// Returns the configured acquisition filter.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Current sample filter.</returns>
        public static FILTER AnalogInChannelFilterGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, FILTER>(FDwfAnalogInChannelFilterGet, "FDwfAnalogInChannelFilterGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelRangeInfo")]
        static extern bool FDwfAnalogInChannelRangeInfo(HDWF hdwf, ref double pvoltsMin, ref double pvoltsMax, ref double pnSteps);

        /// <summary>
        /// Returns the minimum and maximum range, peak to peak values, and the number of adjustable steps.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Voltage range information.</returns>
        public static RangeSteps<double, double> AnalogInChannelRangeInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double, double>(FDwfAnalogInChannelRangeInfo, "FDwfAnalogInChannelRangeInfo", hdwf);
        }


        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelRangeSteps")]
        static extern unsafe bool FDwfAnalogInChannelRangeSteps(HDWF hdwf, double* rgVoltsStep /*[32]*/, ref int pnSteps);

        /// <summary>
        /// Read the range of steps supported by the device. For instance: 1, 2, 5, 10, etc.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>List of the range steps.</returns>
        public static List<double> AnalogInChannelRangeSteps(HDWF hdwf)
        {
            const int max_steps = 32;
            unsafe
            {
                int nsteps = max_steps;
                var buffer = new double[max_steps];
                fixed (double* ptr = buffer)
                {
                    var ok = FDwfAnalogInChannelRangeSteps(hdwf, ptr, ref nsteps);
                    if (ok)
                    {
                        var r = new List<double>();
                        for (int i = 0; i < nsteps; ++i)
                        {
                            r.Add(buffer[i]);
                        }
                        return r;
                    }
                    else
                    {
                        throw new ApplicationException(ExceptionMessage("FDwfAnalogInChannelRangeSteps"));
                    }
                }
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelRangeSet")]
        static extern bool FDwfAnalogInChannelRangeSet(HDWF hdwf, int idxChannel, double voltsRange);

        /// <summary>
        /// Configure the range for each channel. With channel index -1, each enabled Analog In channel range will be configured to the same, new value.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="voltsRange">Voltage range to set.</param>
        public static void AnalogInChannelRangeSet(HDWF hdwf, int idxChannel, double voltsRange)
        {
            CheckApiProcedureHdwfIndexSet<int, double>(FDwfAnalogInChannelRangeSet, "FDwfAnalogInChannelRangeSet", hdwf, idxChannel, voltsRange);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelRangeGet")]
        static extern bool FDwfAnalogInChannelRangeGet(HDWF hdwf, int idxChannel, ref double pvoltsRange);

        /// <summary>
        /// Returns the real range value for the given channel.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Current voltage range.</returns>
        public static double AnalogInChannelRangeGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, double>(FDwfAnalogInChannelRangeGet, "FDwfAnalogInChannelRangeGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelOffsetInfo")]
        static extern bool FDwfAnalogInChannelOffsetInfo(HDWF hdwf, ref double pvoltsMin, ref double pvoltsMax, ref double pnSteps);

        /// <summary>
        /// Returns the minimum and maximum offset levels supported, and the number of adjustable steps.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Range of the offset voltage.</returns>
        public static RangeSteps<double, double> AnalogInChannelOffsetInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double,double>(FDwfAnalogInChannelOffsetInfo, "FDwfAnalogInChannelOffsetInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelOffsetSet")]
        static extern bool FDwfAnalogInChannelOffsetSet(HDWF hdwf, int idxChannel, double voltOffset);

        /// <summary>
        /// Configure the offset for each channel. When channel index is specified as -1, each enabled AnalogIn channel offset will be configured to the same level.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="voltOffset">Channel offset voltage to set.</param>
        public static void AnalogInChannelOffsetSet(HDWF hdwf, int idxChannel, double voltOffset)
        {
            CheckApiProcedureHdwfIndexSet<int, double>(FDwfAnalogInChannelOffsetSet, "FDwfAnalogInChannelOffsetSet", hdwf, idxChannel, voltOffset);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInChannelOffsetGet")]
        static extern bool FDwfAnalogInChannelOffsetGet(HDWF hdwf, int idxChannel, ref double pvoltOffset);

        /// <summary>
        /// Returns for each AnalogIn channel the real offset level.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Channel offset voltage</returns>
        public static double AnalogInChannelOffsetGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, double>(FDwfAnalogInChannelOffsetGet, "FDwfAnalogInChannelOffsetGet", hdwf, idxChannel);
        }
        #endregion // CHANNEL CONFIGURATION

        #region TRIGGER CONFIGURATION

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerSourceInfo")]
        static extern bool FDwfAnalogInTriggerSourceInfo(HDWF hdwf, ref int pfstrigsrc); // use IsBitSet

        /// <summary>
        /// Returns the supported trigger source options for the AnalogIn instrument
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns></returns>
        public static List<TRIGSRC> AnalogInTriggerSourceInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfAnalogInTriggerSourceInfo, "FDwfAnalogInTriggerSourceInfo", hdwf);
            return TrigsrcsOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerSourceSet")]
        static extern bool FDwfAnalogInTriggerSourceSet(HDWF hdwf, TRIGSRC trigsrc);

        /// <summary>
        /// Configure the AnalogIn acquisition trigger source.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="trigsrc">Trigger source to set.</param>
        public static void AnalogInTriggerSourceSet(HDWF hdwf, TRIGSRC trigsrc)
        {
            CheckApiProcedureHdwfSet<TRIGSRC>(FDwfAnalogInTriggerSourceSet, "FDwfAnalogInTriggerSourceSet", hdwf, trigsrc);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerSourceGet")]
        static extern bool FDwfAnalogInTriggerSourceGet(HDWF hdwf, ref TRIGSRC ptrigsrc);

        /// <summary>
        /// Returns the configured trigger source. The trigger source can be â€œnoneâ€ or an internal
        /// instrument or external trigger. To use the trigger on AnalogIn channels (edge, pulse, etc.),
        /// use trigsrcDetectorAnalogIn.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger source.</returns>
        public static TRIGSRC AnalogInTriggerSourceGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<TRIGSRC>(FDwfAnalogInTriggerSourceGet, "FDwfAnalogInTriggerSourceGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerPositionInfo")]
        static extern bool FDwfAnalogInTriggerPositionInfo(HDWF hdwf, ref double psecMin, ref double psecMax, ref double pnSteps);

        /// <summary>
        /// Returns the minimum and maximum values of the trigger position in seconds.
        /// The horizontal trigger position is used for Single acquisition mode and it is relative to the buffer middle point.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Trigger range.</returns>
        public static RangeSteps<double,double> AnalogInTriggerPositionInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double, double>(FDwfAnalogInTriggerPositionInfo, "FDwfAnalogInTriggerPositionInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerPositionSet")]
        static extern bool FDwfAnalogInTriggerPositionSet(HDWF hdwf, double secPosition);

        /// <summary>
        /// Configure the horizontal trigger position in seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="secPosition">Trigger position to set.</param>
        public static void AnalogInTriggerPositionSet(HDWF hdwf, double secPosition)
        {
            CheckApiProcedureHdwfSet<double>(FDwfAnalogInTriggerPositionSet, "FDwfAnalogInTriggerPositionSet", hdwf, secPosition);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerPositionGet")]
        static extern bool FDwfAnalogInTriggerPositionGet(HDWF hdwf, ref double psecPosition);

        /// <summary>
        /// Returns the configured trigger position in seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger position.</returns>
        public static double AnalogInTriggerPositionGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInTriggerPositionGet, "FDwfAnalogInTriggerPositionGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerPositionStatus")]
        static extern bool FDwfAnalogInTriggerPositionStatus(HDWF hdwf, ref double psecPosition);

        public static double AnalogInTriggerPositionStatus(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInTriggerPositionStatus, "FDwfAnalogInTriggerPositionStatus", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerAutoTimeoutInfo")]
        static extern bool FDwfAnalogInTriggerAutoTimeoutInfo(HDWF hdwf, ref double psecMin, ref double psecMax, ref double pnSteps);

        /// <summary>
        /// Returns the minimum and maximum auto trigger timeout values, and the number of adjustable steps.
        /// The acquisition is auto triggered when the specified time elapses. With zero value the timeout is disabled, performing â€œNormalâ€ acquisitions.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Minimum and maximum timeout values.</returns>
        public static RangeSteps<double,double> AnalogInTriggerAutoTimeoutInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double, double>(FDwfAnalogInTriggerAutoTimeoutInfo, "FDwfAnalogInTriggerAutoTimeoutInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerAutoTimeoutSet")]
        static extern bool FDwfAnalogInTriggerAutoTimeoutSet(HDWF hdwf, double secTimeout);

        /// <summary>
        /// Configure the auto trigger timeout value in seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="secTimeout">Timeout to set.</param>
        public static void AnalogInTriggerAutoTimeoutSet(HDWF hdwf, double secTimeout)
        {
            CheckApiProcedureHdwfSet<double>(FDwfAnalogInTriggerAutoTimeoutSet, "AnalogInTriggerAutoTimeoutSet", hdwf, secTimeout);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerAutoTimeoutGet")]
        static extern bool FDwfAnalogInTriggerAutoTimeoutGet(HDWF hdwf, ref double psecTimeout);

        /// <summary>
        /// Returns the configured auto trigger timeout value in seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current timeout.</returns>
        public static double AnalogInTriggerAutoTimeoutGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInTriggerAutoTimeoutGet, "FDwfAnalogInTriggerAutoTimeoutGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerHoldOffInfo")]
        static extern bool FDwfAnalogInTriggerHoldOffInfo(HDWF hdwf, ref double psecMin, ref double psecMax, ref double pnStep);

        /// <summary>
        /// Returns the supported range of the trigger Hold-Off time in Seconds. The trigger hold-off is an
        /// adjustable period of time during which the acquisition will not trigger. This feature is used when you are triggering
        /// on burst waveform shapes, so the oscilloscope triggers only on the first eligible trigger point.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Supported range of the trigger hold-off.</returns>
        public static RangeSteps<double,double> AnalogInTriggerHoldOffInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double,double>(FDwfAnalogInTriggerHoldOffInfo, "FDwfAnalogInTriggerHoldOffInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerHoldOffSet")]
        static extern bool FDwfAnalogInTriggerHoldOffSet(HDWF hdwf, double secHoldOff);

        /// <summary>
        /// Set the trigger hold-off for the AnalongIn instrument in Seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="secHoldOff">Holdoff to set.</param>
        public static void AnalogInTriggerHoldOffSet(HDWF hdwf, double secHoldOff)
        {
            CheckApiProcedureHdwfSet<double>(FDwfAnalogInTriggerHoldOffSet, "FDwfAnalogInTriggerHoldOffSet", hdwf, secHoldOff);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerHoldOffGet")]
        static extern bool FDwfAnalogInTriggerHoldOffGet(HDWF hdwf, ref double psecHoldOff);

        /// <summary>
        /// Get the current trigger hold-off for the AnalongIn instrument in Seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current holdoff value.</returns>
        public static double AnalogInTriggerHoldOffGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInTriggerHoldOffGet, "FDwfAnalogInTriggerHoldOffGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerTypeInfo")]
        static extern bool FDwfAnalogInTriggerTypeInfo(HDWF hdwf, ref int pfstrigtype); // use IsBitSet

        /// <summary>
        /// Returns the supported trigger type options for the instrument
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>List of supported trigger types.</returns>
        public static List<TRIGTYPE> AnalogInTriggerTypeInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfAnalogInTriggerTypeInfo, "FDwfAnalogInTriggerTypeInfo", hdwf);
            var r = new List<TRIGTYPE>();
            var enum_values = (TRIGTYPE[])Enum.GetValues(typeof(TRIGTYPE));
            foreach (var ev in enum_values)
            {
                if ((bf & (1 << (int)ev)) != 0)
                {
                    r.Add(ev);
                }
            }
            return r;
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerTypeSet")]
        static extern bool FDwfAnalogInTriggerTypeSet(HDWF hdwf, TRIGTYPE trigtype);

        /// <summary>
        /// Set the trigger type for the instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="trigtype">Trigger type to set.</param>
        public static void AnalogInTriggerTypeSet(HDWF hdwf, TRIGTYPE trigtype)
        {
            CheckApiProcedureHdwfSet<TRIGTYPE>(FDwfAnalogInTriggerTypeSet, "FDwfAnalogInTriggerTypeSet", hdwf, trigtype);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerTypeGet")]
        static extern bool FDwfAnalogInTriggerTypeGet(HDWF hdwf, ref TRIGTYPE ptrigtype);

        /// <summary>
        /// Get the current trigger type for the instrument
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger type.</returns>
        public static TRIGTYPE AnalogInTriggerTypeGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<TRIGTYPE>(FDwfAnalogInTriggerTypeGet, "FDwfAnalogInTriggerTypeGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerChannelInfo")]
        static extern bool FDwfAnalogInTriggerChannelInfo(HDWF hdwf, ref int pidxMin, ref int pidxMax);

        /// <summary>
        /// Returns the range of channels that can be triggered on.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Channel index range.</returns>
        public static Range<int> AnalogInTriggerChannelInfo(HDWF hdwf)
        {
            var r = new Range<int>() { Min = Int32.MinValue, Max = Int32.MinValue };
            var ok = FDwfAnalogInTriggerChannelInfo(hdwf, ref r.Min, ref r.Max);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfAnalogInTriggerChannelInfo"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerChannelSet")]
        static extern bool FDwfAnalogInTriggerChannelSet(HDWF hdwf, int idxChannel);

        /// <summary>
        /// Set the trigger channel.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Trigger channel index to set.</param>
        public static void AnalogInTriggerChannelSet(HDWF hdwf, int idxChannel)
        {
            CheckApiProcedureHdwfSet<int>(FDwfAnalogInTriggerChannelSet, "FDwfAnalogInTriggerChannelSet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerChannelGet")]
        static extern bool FDwfAnalogInTriggerChannelGet(HDWF hdwf, ref int pidxChannel);

        /// <summary>
        /// Retrieve the current trigger channel index.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger channel index.</returns>
        public static int AnalogInTriggerChannelGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogInTriggerChannelGet, "FDwfAnalogInTriggerChannelGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerFilterInfo")]
        static extern bool FDwfAnalogInTriggerFilterInfo(HDWF hdwf, ref int pfsfilter); // use IsBitSet

        /// <summary>
        /// Returns a list of the supported trigger filters.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>A list of the supported trigger filters.</returns>
        public static List<FILTER> AnalogInTriggerFilterInfo(HDWF hdwf)
        {
            int bf = CheckApiFunctionHdwfGet<int>(FDwfAnalogInTriggerFilterInfo, "FDwfAnalogInTriggerFilterInfo", hdwf);
            return FiltersOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerFilterSet")]
        static extern bool FDwfAnalogInTriggerFilterSet(HDWF hdwf, FILTER filter);

        /// <summary>
        /// Set the trigger filter.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="filter">Trigger filter to set.</param>
        public static void AnalogInTriggerFilterSet(HDWF hdwf, FILTER filter)
        {
            CheckApiProcedureHdwfSet<FILTER>(FDwfAnalogInTriggerFilterSet, "FDwfAnalogInTriggerFilterSet", hdwf, filter);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerFilterGet")]
        static extern bool FDwfAnalogInTriggerFilterGet(HDWF hdwf, ref FILTER pfilter);

        /// <summary>
        /// Get the trigger filter.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger filter.</returns>
        public static FILTER AnalogInTriggerFilterGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<FILTER>(FDwfAnalogInTriggerFilterGet, "FDwfAnalogInTriggerFilterGet", hdwf);
        }


        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLevelInfo")]
        static extern bool FDwfAnalogInTriggerLevelInfo(HDWF hdwf, ref double pvoltsMin, ref double pvoltsMax, ref double pnSteps);

        /// <summary>
        /// Retrieve the range of valid trigger voltage levels for the AnalogIn instrument in Volts.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Valid trigger voltage levels.</returns>
        public static RangeSteps<double,double> AnalogInTriggerLevelInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double,double>(FDwfAnalogInTriggerLevelInfo, "FDwfAnalogInTriggerLevelInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLevelSet")]
        static extern bool FDwfAnalogInTriggerLevelSet(HDWF hdwf, double voltsLevel);

        /// <summary>
        /// Set the trigger voltage level in Volts.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="voltsLevel">Trigger voltage level to set.</param>
        public static void AnalogInTriggerLevelSet(HDWF hdwf, double voltsLevel)
        {
            CheckApiProcedureHdwfSet<double>(FDwfAnalogInTriggerLevelSet, "FDwfAnalogInTriggerLevelSet", hdwf, voltsLevel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLevelGet")]
        static extern bool FDwfAnalogInTriggerLevelGet(HDWF hdwf, ref double pvoltsLevel);

        /// <summary>
        /// Get the current trigger voltage level in Volts.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger voltage level.</returns>
        public static double AnalogInTriggerLevelGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInTriggerLevelGet, "FDwfAnalogInTriggerLevelGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerHysteresisInfo")]
        static extern bool FDwfAnalogInTriggerHysteresisInfo(HDWF hdwf, ref double pvoltsMin, ref double pvoltsMax, ref double pnSteps);

        /// <summary>
        /// Retrieve the range of valid trigger hysteresis voltage levels for the AnalogIn instrument in Volts.
        /// The trigger detector uses two levels: low level (TriggerLevel - Hysteresis) and high level (TriggerLevel + Hysteresis).
        /// Trigger hysteresis can be used to filter noise for Edge or Pulse trigger. The low and high levels are used in transition time triggering.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Trigger hysteresis levels.</returns>
        public static RangeSteps<double,double> AnalogInTriggerHysteresisInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double,double>(FDwfAnalogInTriggerHysteresisInfo, "FDwfAnalogInTriggerHysteresisInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerHysteresisSet")]
        static extern bool FDwfAnalogInTriggerHysteresisSet(HDWF hdwf, double voltsLevel);

        /// <summary>
        /// Set the trigger hysteresis level in Volts.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="voltsLevel">Trigger hysteresis level to set.</param>
        public static void AnalogInTriggerHysteresisSet(HDWF hdwf, double voltsLevel)
        {
            CheckApiProcedureHdwfSet<double>(FDwfAnalogInTriggerHysteresisSet, "FDwfAnalogInTriggerHysteresisSet", hdwf, voltsLevel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerHysteresisGet")]
        static extern bool FDwfAnalogInTriggerHysteresisGet(HDWF hdwf, ref double pvoltsHysteresis);

        /// <summary>
        /// Get the current trigger hysteresis level in Volts.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger hysteresis level.</returns>
        public static double AnalogInTriggerHysteresisGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInTriggerHysteresisGet, "FDwfAnalogInTriggerHysteresisGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerConditionInfo")]
        static extern bool FDwfAnalogInTriggerConditionInfo(HDWF hdwf, ref int pfstrigcond); // use IsBitSet

        /// <summary>
        /// Returns list of the supported trigger conditions for the instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>List of the supported trigger conditions.</returns>
        public static List<TRIGCOND> AnalogInTriggerConditionInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfAnalogInTriggerConditionInfo, "FDwfAnalogInTriggerConditionInfo", hdwf);
            var r = new List<TRIGCOND>();
            var enum_values = (TRIGCOND[])Enum.GetValues(typeof(TRIGCOND));
            foreach (var ev in enum_values)
            {
                if ((bf & (1 << (int)ev)) != 0)
                {
                    r.Add(ev);
                }
            }
            return r;
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerConditionSet")]
        static extern bool FDwfAnalogInTriggerConditionSet(HDWF hdwf, TRIGCOND trigcond);

        /// <summary>
        /// Set the trigger condition for the instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="trigcond">Trigger condition to set</param>
        public static void AnalogInTriggerConditionSet(HDWF hdwf, TRIGCOND trigcond)
        {
            CheckApiProcedureHdwfSet<TRIGCOND>(FDwfAnalogInTriggerConditionSet, "FDwfAnalogInTriggerConditionSet", hdwf, trigcond);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerConditionGet")]
        static extern bool FDwfAnalogInTriggerConditionGet(HDWF hdwf, ref TRIGCOND ptrigcond);

        /// <summary>
        /// Get the trigger condition for the instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger condition.</returns>
        public static TRIGCOND AnalogInTriggerConditionGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<TRIGCOND>(FDwfAnalogInTriggerConditionGet, "FDwfAnalogInTriggerConditionGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLengthInfo")]
        static extern bool FDwfAnalogInTriggerLengthInfo(HDWF hdwf, ref double psecMin, ref double psecMax, ref double pnSteps);

        /// <summary>
        /// Returns the supported range of trigger length for the instrument.
        /// The trigger length specifies the minimal or maximal pulse length or transition time.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Supported range of trigger length, in seconds.</returns>
        public static RangeSteps<double,double> AnalogInTriggerLengthInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double,double>(FDwfAnalogInTriggerLengthInfo, "FDwfAnalogInTriggerLengthInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLengthSet")]
        static extern bool FDwfAnalogInTriggerLengthSet(HDWF hdwf, double secLength);

        /// <summary>
        /// Set the trigger length in seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="secLength">Trigger length to set.</param>
        public static void AnalogInTriggerLengthSet(HDWF hdwf, double secLength)
        {
            CheckApiProcedureHdwfSet<double>(FDwfAnalogInTriggerLengthSet, "AnalogInTriggerLengthSet", hdwf, secLength);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLengthGet")]
        static extern bool FDwfAnalogInTriggerLengthGet(HDWF hdwf, ref double psecLength);

        /// <summary>
        /// Get the current trigger length in Seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>Current trigger length.</returns>
        public static double AnalogInTriggerLengthGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfAnalogInTriggerLengthGet, "FDwfAnalogInTriggerLengthGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLengthConditionInfo")]
        static extern bool FDwfAnalogInTriggerLengthConditionInfo(HDWF hdwf, ref int pfstriglen); // use IsBitSet

        /// <summary>
        /// Returns the supported trigger length condition options for the AnalogIn instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>List of the supported trigger length condition options.</returns>
        public static List<TRIGLEN> AnalogInTriggerLengthConditionInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfAnalogInTriggerLengthConditionInfo, "FDwfAnalogInTriggerLengthConditionInfo", hdwf);
            var r = new List<TRIGLEN>();
            var enum_values = (TRIGLEN[])Enum.GetValues(typeof(TRIGLEN));
            foreach (var ev in enum_values)
            {
                if ((bf & (1 << (int)ev)) != 0)
                {
                    r.Add(ev);
                }
            }
            return r;
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLengthConditionSet")]
        static extern bool FDwfAnalogInTriggerLengthConditionSet(HDWF hdwf, TRIGLEN triglen);

        /// <summary>
        /// Set the trigger length condition for the AnalongIn instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="triglen">Trigger length condition to set.</param>
        public static void AnalogInTriggerLengthConditionSet(HDWF hdwf, TRIGLEN triglen)
        {
            CheckApiProcedureHdwfSet<TRIGLEN>(FDwfAnalogInTriggerLengthConditionSet, "AnalogInTriggerLengthConditionSet", hdwf, triglen);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogInTriggerLengthConditionGet")]
        static extern bool FDwfAnalogInTriggerLengthConditionGet(HDWF hdwf, ref TRIGLEN ptriglen);

        /// <summary>
        /// Get the current trigger length condition for the AnalongIn instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns></returns>
        public static TRIGLEN AnalogInTriggerLengthConditionGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<TRIGLEN>(FDwfAnalogInTriggerLengthConditionGet, "FDwfAnalogInTriggerLengthConditionGet", hdwf);
        }
        #endregion // TRIGGER CONFIGURATION


        #region ANALOG OUT INSTRUMENT FUNCTIONS - CONFIGURATION

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutCount")]
        static extern bool FDwfAnalogOutCount(HDWF hdwf, ref int pcChannel);

        /// <summary>
        /// Returns the number of Analog Out channels by the device specified by hdwf.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <returns>The number of channels in the instrument.</returns>
        public static int AnalogOutCount(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogOutCount, "FDwfAnalogOutCount", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutMasterSet")]
        static extern bool FDwfAnalogOutMasterSet(HDWF hdwf, int idxChannel, int idxMaster);

        /// <summary>
        /// Sets the state machine master of the channel generator.
        /// With channel index -1, each enabled Analog Out channel will be configured to use the same, new option.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="idxMaster">Node index.</param>
        public static void AnalogOutMasterSet(HDWF hdwf, int idxChannel, int idxMaster)
        {
            CheckApiProcedureHdwfIndexSet<int, int>(FDwfAnalogOutMasterSet, "FDwfAnalogOutMasterSet", hdwf, idxChannel, idxMaster);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutMasterGet")]
        static extern bool FDwfAnalogOutMasterGet(HDWF hdwf, int idxChannel, ref int pidxMaster);

        /// <summary>
        /// Get the state machine master..
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>State machine master.</returns>
        public static int AnalogOutMasterGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, int>(FDwfAnalogOutMasterGet, "FDwfAnalogOutMasterGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutTriggerSourceInfo")]
        static extern bool FDwfAnalogOutTriggerSourceInfo(HDWF hdwf, int idxChannel, ref int pfstrigsrc); // use IsBitSet

        /// <summary>
        /// Returns list of the supported trigger source options for the instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>List of the supported trigger source options.</returns>
        public static List<TRIGSRC> AnalogOutTriggerSourceInfo(HDWF hdwf, int idxChannel)
        {
            var bf = CheckApiFunctionHdwfIndexGet<int, int>(FDwfAnalogOutTriggerSourceInfo, "FDwfAnalogOutTriggerSourceInfo", hdwf, idxChannel);
            return TrigsrcsOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutTriggerSourceSet")]
        static extern bool FDwfAnalogOutTriggerSourceSet(HDWF hdwf, int idxChannel, TRIGSRC trigsrc);

        /// <summary>
        /// Set the trigger source for the channel on instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="trigsrc">Trigger source to set.</param>
        public static void AnalogOutTriggerSourceSet(HDWF hdwf, int idxChannel, TRIGSRC trigsrc)
        {
            CheckApiProcedureHdwfIndexSet<int, TRIGSRC>(FDwfAnalogOutTriggerSourceSet, "FDwfAnalogOutTriggerSourceSet", hdwf, idxChannel, trigsrc);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutTriggerSourceGet")]
        static extern bool FDwfAnalogOutTriggerSourceGet(HDWF hdwf, int idxChannel, ref TRIGSRC ptrigsrc);

        /// <summary>
        /// Get the current trigger source setting for the channel on instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>The trigger source.</returns>
        public static TRIGSRC AnalogOutTriggerSourceGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, TRIGSRC>(FDwfAnalogOutTriggerSourceGet, "FDwfAnalogOutTriggerSourceGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRunInfo")]
        static extern bool FDwfAnalogOutRunInfo(HDWF hdwf, int idxChannel, ref double psecMin, ref double psecMax);

        /// <summary>
        /// Return the supported run length range for the instrument in Seconds.
        /// Zero values represent an infinite (or continuous) run. Default value is zero.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Supported minimum and maximum run lengths in seconds.</returns>
        public static Range<double> AnalogOutRunInfo(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGetRange<int, double>(FDwfAnalogOutRunInfo, "FDwfAnalogOutRunInfo", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRunSet")]
        static extern bool FDwfAnalogOutRunSet(HDWF hdwf, int idxChannel, double secRun);

        /// <summary>
        /// Set the run length for the instrument in Seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="secRun">Run length to set expressed in seconds.</param>
        public static void AnalogOutRunSet(HDWF hdwf, int idxChannel, double secRun)
        {
            CheckApiProcedureHdwfIndexSet<int, double>(FDwfAnalogOutRunSet, "FDwfAnalogOutRunSet", hdwf, idxChannel, secRun);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRunGet")]
        static extern bool FDwfAnalogOutRunGet(HDWF hdwf, int idxChannel, ref double psecRun);

        /// <summary>
        /// Read the configured run length for the instrument in Seconds.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Run length.</returns>
        public static double AnalogOutRunGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, double>(FDwfAnalogOutRunGet, "FDwfAnalogOutRunGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRunStatus")]
        static extern bool FDwfAnalogOutRunStatus(HDWF hdwf, int idxChannel, ref double psecRun);

        /// <summary>
        /// Read the remaining run length. It returns data from the last FDwfAnalogOutStatus call.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Remaining run length.</returns>
        public static double AnalogOutRunStatus(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, double>(FDwfAnalogOutRunStatus, "FDwfAnalogOutRunStatus", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutWaitInfo")]
        static extern bool FDwfAnalogOutWaitInfo(HDWF hdwf, int idxChannel, ref double psecMin, ref double psecMax);

        /// <summary>
        /// Return the supported wait length range in Seconds.
        /// The wait length is how long the instrument waits after being triggered to generate the signal.
        /// Default value is zero.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Supported minimum and maximum wait lengths in seconds.</returns>
        public static Range<double> AnalogOutWaitInfo(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGetRange<int,double>(FDwfAnalogOutWaitInfo, "FDwfAnalogOutWaitInfo", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutWaitSet")]
        static extern bool FDwfAnalogOutWaitSet(HDWF hdwf, int idxChannel, double secWait);

        /// <summary>
        /// Set the wait length for the channel on instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="secWait">Wait length to set expressed in seconds.</param>
        public static void AnalogOutWaitSet(HDWF hdwf, int idxChannel, double secWait)
        {
            CheckApiProcedureHdwfIndexSet<int, double>(FDwfAnalogOutWaitSet, "FDwfAnalogOutWaitSet", hdwf, idxChannel, secWait);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutWaitGet")]
        static extern bool FDwfAnalogOutWaitGet(HDWF hdwf, int idxChannel, ref double psecWait);

        /// <summary>
        /// Get the current wait length for the channel on instrument.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Wait length, in seconds.</returns>
        public static double AnalogOutWaitGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, double>(FDwfAnalogOutWaitGet, "FDwfAnalogOutWaitGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRepeatInfo")]
        static extern bool FDwfAnalogOutRepeatInfo(HDWF hdwf, int idxChannel, ref int pnMin, ref int pnMax);

        /// <summary>
        /// Return the supported repeat count range.
        /// This is how many times the generated signal will be repeated upon.
        /// Zero value represents infinite repeat.
        /// Default value is one.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Minimum and maximum repeat counts.</returns>
        public static Range<int> AnalogOutRepeatInfo(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGetRange<int, int>(FDwfAnalogOutRepeatInfo, "FDwfAnalogOutRepeatInfo", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRepeatSet")]
        static extern bool FDwfAnalogOutRepeatSet(HDWF hdwf, int idxChannel, int cRepeat);

        /// <summary>
        /// Set the repeat count.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="cRepeat">Repeat count to set.</param>
        public static void AnalogOutRepeatSet(HDWF hdwf, int idxChannel, int cRepeat)
        {
            CheckApiProcedureHdwfIndexSet<int, int>(FDwfAnalogOutRepeatSet, "FDwfAnalogOutRepeatSet", hdwf, idxChannel, cRepeat);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRepeatGet")]
        static extern bool FDwfAnalogOutRepeatGet(HDWF hdwf, int idxChannel, ref int pcRepeat);

        /// <summary>
        /// Read the current repeat count.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Repeat count.</returns>
        public static int AnalogOutRepeatGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, int>(FDwfAnalogOutRepeatGet, "FDwfAnalogOutRepeatGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRepeatStatus")]
        static extern bool FDwfAnalogOutRepeatStatus(HDWF hdwf, int idxChannel, ref int pcRepeat);

        /// <summary>
        /// Read the remaining repeat counts.
        /// It only returns information from the last FDwfAnalogOutStatus function call, it does not read from the device.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Remaining repeat counts.</returns>
        public static int AnalogOutRepeatStatus(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, int>(FDwfAnalogOutRepeatStatus, "FDwfAnalogOutRepeatStatus", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRepeatTriggerSet")]
        static extern bool FDwfAnalogOutRepeatTriggerSet(HDWF hdwf, int idxChannel, bool fRepeatTrigger);

        /// <summary>
        /// set the repeat trigger option.
        /// To include the trigger in wait-run repeat cycles, set fRepeatTrigger to TRUE. It is disabled by default.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="fRepeatTrigger">Specify if the trigger should be included in a repeat cycle.</param>
        public static void AnalogOutRepeatTriggerSet(HDWF hdwf, int idxChannel, bool fRepeatTrigger)
        {
            CheckApiProcedureHdwfIndexSet<int, bool>(FDwfAnalogOutRepeatTriggerSet, "FDwfAnalogOutRepeatTriggerSet", hdwf, idxChannel, fRepeatTrigger);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutRepeatTriggerGet")]
        static extern bool FDwfAnalogOutRepeatTriggerGet(HDWF hdwf, int idxChannel, ref bool pfRepeatTrigger);

        /// <summary>
        /// Verify if the trigger has been included in wait-run repeat cycles.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Repeat trigger option.</returns>
        public static bool AnalogOutRepeatTriggerGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, bool>(FDwfAnalogOutRepeatTriggerGet, "FDwfAnalogOutRepeatTriggerGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeInfo")]
        static extern bool FDwfAnalogOutNodeInfo(HDWF hdwf, int idxChannel, ref int pfsnode); // use IsBitSet

        /// <summary>
        /// Returns a list of the supported AnalogOut nodes of the AnalogOut channel.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>List of the supported AnalogOut nodes.</returns>
        public static List<ANALOGOUTNODE> AnalogOutNodeInfo(HDWF hdwf, int idxChannel)
        {
            var bf = CheckApiFunctionHdwfIndexGet<int, int>(FDwfAnalogOutNodeInfo, "FDwfAnalogOutNodeInfo", hdwf, idxChannel);
            return AnalogoutnodesOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeEnableSet")]
        static extern bool FDwfAnalogOutNodeEnableSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, bool fEnable);

        /// <summary>
        /// Enables or disables the channel node specified by idxChannel and node. The Carrier node
        /// enables or disables the channel and AM/FM the modulation.
        /// With channel index -1, each Analog Out channel enable will be configured to use the same, new option.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <param name="fEnable">TRUE to enable, FALSE to disable</param>
        public static void AnalogOutNodeEnableSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, bool fEnable)
        {
            CheckApiProcedureHdwfIndexNodeSet<ANALOGOUTNODE, bool>(FDwfAnalogOutNodeEnableSet, "FDwfAnalogOutNodeEnableSet", hdwf, idxChannel, node, fEnable);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeEnableGet")]
        static extern bool FDwfAnalogOutNodeEnableGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref bool pfEnable);

        /// <summary>
        /// Verify if a specific channel and node is enabled or disabled
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Enabled state</returns>
        public static bool AnalogOutNodeEnableGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGet<ANALOGOUTNODE, bool>(FDwfAnalogOutNodeEnableGet, "FDwfAnalogOutNodeEnableGet", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeFunctionInfo")]
        static extern bool FDwfAnalogOutNodeFunctionInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref int pfsfunc); // use IsBitSet

        /// <summary>
        /// Returns the supported generator function options.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>List of function options</returns>
        public static List<FUNC> AnalogOutNodeFunctionInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            var bf = CheckApiFunctionHdwfIndexNodeGet<ANALOGOUTNODE, int>(FDwfAnalogOutNodeFunctionInfo, "FDwfAnalogOutNodeFunctionInfo", hdwf, idxChannel, node);
            return FuncsOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeFunctionSet")]
        static extern bool FDwfAnalogOutNodeFunctionSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, FUNC func);

        /// <summary>
        /// the generator output function for the specified instrument channel.
        /// With channel index -1, each enabled Analog Out channel function will be configured to use the same, new option.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <param name="func">Generator function option to set.</param>
        public static void AnalogOutNodeFunctionSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, FUNC func)
        {
            CheckApiProcedureHdwfIndexNodeSet<ANALOGOUTNODE, FUNC>(FDwfAnalogOutNodeFunctionSet, "FDwfAnalogOutNodeFunctionSet", hdwf, idxChannel, node, func);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeFunctionGet")]
        static extern bool FDwfAnalogOutNodeFunctionGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref FUNC pfunc);

        /// <summary>
        /// Retrieve the current generator function option for the specified instrument channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Generator function option</returns>
        public static FUNC AnalogOutNodeFunctionGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGet<ANALOGOUTNODE, FUNC>(FDwfAnalogOutNodeFunctionGet, "FDwfAnalogOutNodeFunctionGet", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeFrequencyInfo")]
        static extern bool FDwfAnalogOutNodeFrequencyInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double phzMin, ref double phzMax);

        /// <summary>
        /// Return the supported frequency range for the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Range of supported frequencies.</returns>
        public static Range<double> AnalogOutNodeFrequencyInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGetRange<ANALOGOUTNODE, double>(FDwfAnalogOutNodeFrequencyInfo, "FDwfAnalogOutNodeFrequencyInfo", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeFrequencySet")]
        static extern bool FDwfAnalogOutNodeFrequencySet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double hzFrequency);

        /// <summary>
        /// Set the frequency.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <param name="hzFrequency">Frequency value to set expressed in Hz.</param>
        public static void AnalogOutNodeFrequencySet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double hzFrequency)
        {
            CheckApiProcedureHdwfIndexNodeSet<ANALOGOUTNODE, double>(FDwfAnalogOutNodeFrequencySet, "FDwfAnalogOutNodeFrequencySet", hdwf, idxChannel, node, hzFrequency);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeFrequencyGet")]
        static extern bool FDwfAnalogOutNodeFrequencyGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double phzFrequency);

        // Carrier Amplitude or Modulation Index

        /// <summary>
        /// Get the currently set frequency for the specified channel-node on the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Frequency value in Hz.</returns>
        public static double AnalogOutNodeFrequencyGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGet<ANALOGOUTNODE, double>(FDwfAnalogOutNodeFrequencyGet, "FDwfAnalogOutNodeFrequencyGet", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeAmplitudeInfo")]
        static extern bool FDwfAnalogOutNodeAmplitudeInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double pMin, ref double pMax);

        /// <summary>
        /// Retrieve the amplitude range for the specified channel-node on the instrument.
        /// The amplitude is expressed in Volts units for carrier and in percentage units (modulation index) for AM/FM.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Range of the amplitude level or modulation index.</returns>
        public static Range<double> AnalogOutNodeAmplitudeInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGetRange<ANALOGOUTNODE, double>(FDwfAnalogOutNodeAmplitudeInfo, "FDwfAnalogOutNodeAmplitudeInfo", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeAmplitudeSet")]
        static extern bool FDwfAnalogOutNodeAmplitudeSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double vAmplitude);

        /// <summary>
        /// Set the amplitude or modulation index for the specified channel-node on the instrument.
        /// With channel index -1, each enabled Analog Out channel amplitude (or modulation index) will be configured to use the same, new option.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <param name="vAmplitude">Amplitude of channel in Volts or modulation index in percentage.</param>
        public static void AnalogOutNodeAmplitudeSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double vAmplitude)
        {
            CheckApiProcedureHdwfIndexNodeSet<ANALOGOUTNODE, double>(FDwfAnalogOutNodeAmplitudeSet, "FDwfAnalogOutNodeAmplitudeSet", hdwf, idxChannel, node, vAmplitude);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeAmplitudeGet")]
        static extern bool FDwfAnalogOutNodeAmplitudeGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double pvAmplitude);

        /// <summary>
        /// Get the currently set amplitude or modulation index for the specified channel-node on the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>amplitude value in Volts or modulation index in percentage.</returns>
        public static double AnalogOutNodeAmplitudeGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGet<ANALOGOUTNODE, double>(FDwfAnalogOutNodeAmplitudeGet, "FDwfAnalogOutNodeAmplitudeGet", hdwf, idxChannel, node); 
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeOffsetInfo")]
        static extern bool FDwfAnalogOutNodeOffsetInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double pMin, ref double pMax);

        /// <summary>
        /// retrieve available the offset range. For carrier node in units of volts, and in percentage units for AM/FM nodes.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Range of the offset voltage or modulation offset percentage.</returns>
        public static Range<double> AnalogOutNodeOffsetInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGetRange<ANALOGOUTNODE, double>(FDwfAnalogOutNodeOffsetInfo, "FDwfAnalogOutNodeOffsetInfo", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeOffsetSet")]
        static extern bool FDwfAnalogOutNodeOffsetSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double vOffset);

        /// <summary>
        /// Set the offset value for the specified channel-node on the instrument.
        /// With channel index -1, each enabled Analog Out channel offset will be configured to use the same, new option.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <param name="vOffset">Value to set voltage offset in Volts or modulation offset percentage.</param>
        public static void AnalogOutNodeOffsetSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double vOffset)
        {
            CheckApiProcedureHdwfIndexNodeSet<ANALOGOUTNODE, double>(FDwfAnalogOutNodeOffsetSet, "FDwfAnalogOutNodeOffsetSet", hdwf, idxChannel, node, vOffset);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeOffsetGet")]
        static extern bool FDwfAnalogOutNodeOffsetGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double pvOffset);

        /// <summary>
        /// Get the current offset value for the specified channel-node on the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Offset value in Volts or modulation offset percentage.</returns>
        public static double AnalogOutNodeOffsetGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGet<ANALOGOUTNODE, double>(FDwfAnalogOutNodeOffsetGet, "FDwfAnalogOutNodeOffsetGet", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeSymmetryInfo")]
        static extern bool FDwfAnalogOutNodeSymmetryInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double ppercentageMin, ref double ppercentageMax);

        /// <summary>
        /// Obtain the symmetry (or duty cycle) range (0..100). This symmetry is supported for standard signal types.
        /// It the pulse duration divided by the pulse period.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Range of symmetry percentages.</returns>
        public static Range<double> AnalogOutNodeSymmetryInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGetRange<ANALOGOUTNODE, double>(FDwfAnalogOutNodeSymmetryInfo, "FDwfAnalogOutNodeSymmetryInfo", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeSymmetrySet")]
        static extern bool FDwfAnalogOutNodeSymmetrySet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double percentageSymmetry);

        /// <summary>
        /// Set the symmetry (or duty cycle) for the specified channel-node on the instrument.
        /// With channel index -1, each enabled Analog Out channel symmetry will be configured to use the same, new option.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <param name="percentageSymmetry">Value of percentage of Symmetry (duty cycle).</param>
        public static void AnalogOutNodeSymmetrySet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double percentageSymmetry)
        {
            CheckApiProcedureHdwfIndexNodeSet<ANALOGOUTNODE, double>(FDwfAnalogOutNodeSymmetrySet, "FDwfAnalogOutNodeSymmetrySet", hdwf, idxChannel, node, percentageSymmetry);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeSymmetryGet")]
        static extern bool FDwfAnalogOutNodeSymmetryGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double ppercentageSymmetry);

        /// <summary>
        /// Get the currently set symmetry (or duty cycle) for the specified channel-node of the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device</param>
        /// <param name="idxChannel">Channel index</param>
        /// <param name="node">Node index</param>
        /// <returns>Value of Symmetry (duty cycle).</returns>
        public static double AnalogOutNodeSymmetryGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGet<ANALOGOUTNODE, double>(FDwfAnalogOutNodeSymmetryGet, "FDwfAnalogOutNodeSymmetryGet", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodePhaseInfo")]
        static extern bool FDwfAnalogOutNodePhaseInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double pdegreeMin, ref double pdegreeMax);

        /// <summary>
        /// Retrieve the phase range (in degrees 0...360) for the specified channel-node of the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <returns>Range of Phase (in degrees).</returns>
        public static Range<double> AnalogOutNodePhaseInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGetRange<ANALOGOUTNODE, double>(FDwfAnalogOutNodePhaseInfo, "FDwfAnalogOutNodePhaseInfo", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodePhaseSet")]
        static extern bool FDwfAnalogOutNodePhaseSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double degreePhase);

        /// <summary>
        /// Set the phase for the specified channel-node on the instrument. With channel index - 1,
        /// each enabled Analog Out channel phase will be configured to use the same, new option.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <param name="degreePhase">Value of Phase in degrees.</param>
        public static void AnalogOutNodePhaseSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double degreePhase)
        {
            CheckApiProcedureHdwfIndexNodeSet<ANALOGOUTNODE, double>(FDwfAnalogOutNodePhaseSet, "FDwfAnalogOutNodePhaseSet", hdwf, idxChannel, node, degreePhase);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodePhaseGet")]
        static extern bool FDwfAnalogOutNodePhaseGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref double pdegreePhase);

        /// <summary>
        /// Get the current phase for the specified channel-node on the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <returns>Phase value (in degrees).</returns>
        public static double AnalogOutNodePhaseGet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGet<ANALOGOUTNODE, double>(FDwfAnalogOutNodePhaseGet, "FDwfAnalogOutNodePhaseGet", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeDataInfo")]
        static extern bool FDwfAnalogOutNodeDataInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref int pnSamplesMin, ref int pnSamplesMax);

        /// <summary>
        /// Retrieve the minimum and maximum number of samples allowed for custom data generation.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <returns>Range of samples available for custom data.</returns>
        public static Range<int> AnalogOutNodeDataInfo(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            return CheckApiFunctionHdwfIndexNodeGetRange<ANALOGOUTNODE, int>(FDwfAnalogOutNodeDataInfo, "FDwfAnalogOutNodeDataInfo", hdwf, idxChannel, node);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodeDataSet")]
        static unsafe extern bool FDwfAnalogOutNodeDataSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double* rgdData, int cdData);

        /// <summary>
        /// The function above is used to set the custom data or to prefill the buffer with play samples.
        /// The samples are double precision floating point values (rgdData) normalized to ±1.
        /// With the custom function option, the data samples (cdData) will be interpolated to the device buffer size.
        /// The output value will be Offset + Sample*Amplitude, for instance:
        /// - 0 value sample will output: Offset.
        /// - +1 value sample will output: Offset + Amplitude.
        /// - -1 value sample will output: Offset – Amplitude.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <param name="data">Samples to set.</param>
        public static void AnalogOutNodeDataSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double[] data)
        {
            unsafe
            {
                fixed (double* ptr = data)
                {
                    bool ok = FDwfAnalogOutNodeDataSet(hdwf, idxChannel, node, ptr, data.Length);
                    if (!ok)
                    {
                        throw new ApplicationException(ExceptionMessage("FDwfAnalogOutNodeDataSet"));
                    }
                }
            }
        }

        /// <summary>
        /// The function above is used to set the custom data or to prefill the buffer with play samples.
        /// The samples are double precision floating point values (rgdData) normalized to ±1.
        /// With the custom function option, the data samples (cdData) will be interpolated to the device buffer size.
        /// The output value will be Offset + Sample*Amplitude, for instance:
        /// - 0 value sample will output: Offset.
        /// - +1 value sample will output: Offset + Amplitude.
        /// - -1 value sample will output: Offset – Amplitude.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <param name="data">Samples to set.</param>
        /// <param name="offset">Offset into the data.</param>
        /// <param name="length">Number of samples.</param>
        public static void AnalogOutNodeDataSet(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double[] data, int offset, int length)
        {
            if (offset < 0 || length <= 0 || offset + length > data.Length)
            {
                throw new ApplicationException("AnalogOutNodeDataSet: invalid parameters");
            }
            else
            {
                unsafe
                {
                    fixed (double* ptr = data)
                    {
                        bool ok = FDwfAnalogOutNodeDataSet(hdwf, idxChannel, node, ptr + offset, length);
                        if (!ok)
                        {
                            throw new ApplicationException(ExceptionMessage("FDwfAnalogOutNodeDataSet"));
                        }
                    }
                }
            }
        }

        // ========================================================================================
        // needed for EExplorer, don't care for ADiscovery
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutCustomAMFMEnableSet")]
        static extern bool FDwfAnalogOutCustomAMFMEnableSet(HDWF hdwf, int idxChannel, bool fEnable);

        public static void AnalogOutCustomAMFMEnableSet(HDWF hdwf, int idxChannel, bool fEnable)
        {
            CheckApiProcedureHdwfIndexSet<int, bool>(FDwfAnalogOutCustomAMFMEnableSet, "FDwfAnalogOutCustomAMFMEnableSet", hdwf, idxChannel, fEnable);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutCustomAMFMEnableGet")]
        static extern bool FDwfAnalogOutCustomAMFMEnableGet(HDWF hdwf, int idxChannel, ref bool pfEnable);

        public static bool AnalogOutCustomAMFMEnableGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, bool>(FDwfAnalogOutCustomAMFMEnableGet, "FDwfAnalogOutCustomAMFMEnableGet", hdwf, idxChannel);
        }
        #endregion // ANALOG OUT INSTRUMENT FUNCTIONS - CONFIGURATION

        #region ANALOG OUT INSTRUMENT FUNCTIONS - CONTROL
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutReset")]
        static extern bool FDwfAnalogOutReset(HDWF hdwf, int idxChannel);

        /// <summary>
        /// Resets and configures (by default, having auto configure enabled) all AnalogOut instrument parameters to default values for the specified channel.
        /// To reset instrument parameters across all channels, set idxChannel to -1.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        public static void AnalogOutReset(HDWF hdwf, int idxChannel)
        {
            CheckApiProcedureHdwfSet<int>(FDwfAnalogOutReset, "FDwfAnalogOutReset", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutConfigure")]
        static extern bool FDwfAnalogOutConfigure(HDWF hdwf, int idxChannel, bool fStart);

        /// <summary>
        /// Start or stop the instrument. With channel index -1, each enabled Analog Out channel will be configured.
        /// </summary>
        /// <param name="hdwf">Interface handle.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="fStart">Start the acquisition. To stop, set to FALSE.</param>
        public static void AnalogOutConfigure(HDWF hdwf, int idxChannel, bool fStart)
        {
            CheckApiProcedureHdwfIndexSet<int, bool>(FDwfAnalogOutConfigure, "FDwfAnalogOutConfigure", hdwf, idxChannel, fStart);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutStatus")]
        static extern bool FDwfAnalogOutStatus(HDWF hdwf, int idxChannel, ref STATE psts);

        /// <summary>
        /// Check the state of the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>State.</returns>
        public static STATE AnalogOutStatus(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, STATE>(FDwfAnalogOutStatus, "FDwfAnalogOutStatus", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodePlayStatus")]
        static extern bool FDwfAnalogOutNodePlayStatus(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, ref int cdDataFree, ref int cdDataLost, ref int cdDataCorrupted);

        /// <summary>
        /// Retrieve information about the play process.
        /// The data lost occurs when the device generator is faster than the sample send process from the PC.
        /// In this case, the device buffer gets emptied and generated samples are repeated.
        /// Corrupt samples are a warning that the buffer might have been emptied while samples were sent to the device.
        /// In this case, try optimizing the loop for faster execution; or reduce the frequency
        /// or run time to be less or equal to the device buffer size (run time <= buffer size/frequency).
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <returns>Number of new samples that can be sent, number of lost samples and number of samples that could be corrupted.</returns>
        public static DataStatusRecord AnalogOutNodePlayStatus(HDWF hdwf, int idxChannel, ANALOGOUTNODE node)
        {
            var r = new DataStatusRecord() { Available = Int32.MinValue, Lost = Int32.MinValue, Corrupt = Int32.MinValue };
            var ok = FDwfAnalogOutNodePlayStatus(hdwf, idxChannel, node, ref r.Available, ref r.Lost, ref r.Corrupt);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfAnalogOutNodePlayStatus"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutNodePlayData")]
        static extern unsafe bool FDwfAnalogOutNodePlayData(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double* rgdData, int cdData);

        /// <summary>
        /// Send new data samples for play mode. Before starting the Analog Out instrument,
        /// prefill the device buffer with the first set of samples using the AnalogOutNodeDataSet function.
        /// In the loop of sending the following samples, first call AnalogOutStatus to read the information from the device,
        /// then AnalogOutPlayStatus to find out how many new samples can be sent,
        /// then send the samples with AnalogOutPlayData.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <param name="data">Samples array to be sent to the device.</param>
        public static void AnalogOutNodePlayData(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double[] data)
        {
            unsafe
            {
                fixed (double* ptr = data)
                {
                    var ok = FDwfAnalogOutNodePlayData(hdwf, idxChannel, node, ptr, data.Length);
                    if (!ok)
                    {
                        throw new ApplicationException(ExceptionMessage("FDwfAnalogOutNodePlayData"));
                    }
                }
            }
        }

        /// <summary>
        /// Send new data samples for play mode. Before starting the Analog Out instrument,
        /// prefill the device buffer with the first set of samples using the AnalogOutNodeDataSet function.
        /// In the loop of sending the following samples, first call AnalogOutStatus to read the information from the device,
        /// then AnalogOutPlayStatus to find out how many new samples can be sent,
        /// then send the samples with AnalogOutPlayData.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="node">Node index.</param>
        /// <param name="data">Samples array to be sent to the device.</param>
        /// <param name="offset">Offset into the data.</param>
        /// <param name="length">Number of samples.</param>
        public static void AnalogOutNodePlayData(HDWF hdwf, int idxChannel, ANALOGOUTNODE node, double[] data, int offset, int length)
        {
            if (offset < 0 || length <= 0 || offset + length > data.Length)
            {
                throw new ApplicationException("AnalogOutNodePlayData: invalid parameters");
            }
            else
            {
                unsafe
                {
                    fixed (double* ptr = data)
                    {
                        var ok = FDwfAnalogOutNodePlayData(hdwf, idxChannel, node, ptr + offset, length);
                        if (!ok)
                        {
                            throw new ApplicationException(ExceptionMessage("FDwfAnalogOutNodePlayData"));
                        }
                    }
                }
            }
        }
        #endregion // ANALOG OUT INSTRUMENT FUNCTIONS - CONTROL

        #region ANALOG IO INSTRUMENT FUNCTIONS - CONTROL

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOReset")]
        static extern bool FDwfAnalogIOReset(HDWF hdwf);

        /// <summary>
        /// Resets and configures (by default, having auto configure enabled) all AnalogIO instrument parameters to default values.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static void AnalogIOReset(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfAnalogIOReset, "FDwfAnalogIOReset", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOConfigure")]
        static extern bool FDwfAnalogIOConfigure(HDWF hdwf);

        /// <summary>
        /// Configure the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static void AnalogIOConfigure(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfAnalogIOConfigure, "FDwfAnalogIOConfigure", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOStatus")]
        static extern bool FDwfAnalogIOStatus(HDWF hdwf);

        /// <summary>
        /// Reads the status of the device and stores it internally.
        /// The following status functions will return the information that was read from the device when the function above was called.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static void AnalogIOStatus(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfAnalogIOStatus, "FDwfAnalogIOStatus", hdwf);
        }
        #endregion // ANALOG IO INSTRUMENT FUNCTIONS - CONTROL

        #region ANALOG IO INSTRUMENT FUNCTIONS - CONFIGURE

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOEnableInfo")]
        static extern bool FDwfAnalogIOEnableInfo(HDWF hdwf, ref bool pfSet, ref bool pfStatus);

        /// <summary>
        /// Verify if Master Enable Setting and/or Master Enable Status are supported for the AnalogIO instrument.
        /// The Master Enable setting is essentially a software switch that “enables” or “turns on” the AnalogIO channels.
        /// If supported, the status of this Master Enable switch (Enabled/Disabled) can be queried by calling AnalogIOEnableStatus.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Master Enable Setting and Master Enable Status.</returns>
        public static MasterEnableInfo AnalogIOEnableInfo(HDWF hdwf)
        {
            var r = new MasterEnableInfo() { Set = false, Status = false };
            var ok = FDwfAnalogIOEnableInfo(hdwf, ref r.Set, ref r.Status);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfAnalogIOEnableInfo"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOEnableSet")]
        static extern bool FDwfAnalogIOEnableSet(HDWF hdwf, bool fMasterEnable);

        /// <summary>
        /// Set the master enable switch.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="fMasterEnable">Set TRUE to enable the master switch; FALSE to disable the master switch.</param>
        public static void AnalogIOEnableSet(HDWF hdwf, bool fMasterEnable)
        {
            CheckApiProcedureHdwfSet<bool>(FDwfAnalogIOEnableSet, "FDwfAnalogIOEnableSet", hdwf, fMasterEnable);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOEnableGet")]
        static extern bool FDwfAnalogIOEnableGet(HDWF hdwf, ref bool pfMasterEnable);

        /// <summary>
        /// Returns the current state of the master enable switch. This is not obtained from the device.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>The enabled configuration.</returns>
        public static bool AnalogIOEnableGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<bool>(FDwfAnalogIOEnableGet, "FDwfAnalogIOEnableGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOEnableStatus")]
        static extern bool FDwfAnalogIOEnableStatus(HDWF hdwf, ref bool pfMasterEnable);

        /// <summary>
        /// Returns the master enable status (if the device supports it).
        /// This can be a switch on the board or an overcurrent protection circuit state.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Active status.</returns>
        public static bool AnalogIOEnableStatus(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<bool>(FDwfAnalogIOEnableStatus, "FDwfAnalogIOEnableStatus", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelCount")]
        static extern bool FDwfAnalogIOChannelCount(HDWF hdwf, ref int pnChannel);

        /// <summary>
        /// Returns the number of AnalogIO channels available on the device.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>The number of channels.</returns>
        public static int AnalogIOChannelCount(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfAnalogIOChannelCount, "FDwfAnalogIOChannelCount", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelName")]
        static extern unsafe bool FDwfAnalogIOChannelName(HDWF hdwf, int idxChannel, sbyte* szName /*[32]*/, sbyte* szLabel/*16*/);

        /// <summary>
        /// Returns the name (long text) and label (short text, printed on the device) for a channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Name and label of a channel.</returns>
        public static ChannelName AnalogIOChannelName(HDWF hdwf, int idxChannel)
        {
            unsafe
            {
                const int name_length = 32;
                var name_buffer = stackalloc sbyte[name_length];
                const int label_length = 16;
                var label_buffer = stackalloc sbyte[label_length];
                var ok = FDwfAnalogIOChannelName(hdwf, idxChannel, name_buffer, label_buffer);
                if (ok)
                {
                    var r = new ChannelName()
                    {
                        Name = new string(name_buffer, 0, name_length, Encoding.ASCII),
                        Label = new string(label_buffer, 0, label_length, Encoding.ASCII),
                    };
                    return r;
                }
                else
                {
                    throw new ApplicationException(ExceptionMessage("FDwfAnalogIOChannelName"));
                }
            }
        }


        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelInfo")]
        static extern bool FDwfAnalogIOChannelInfo(HDWF hdwf, int idxChannel, ref int pnNodes);

        /// <summary>
        /// Returns the number of nodes associated with the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Number of nodes.</returns>
        static int AnalogIOChannelInfo(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, int>(FDwfAnalogIOChannelInfo, "FDwfAnalogIOChannelInfo", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelNodeName")]
        static extern unsafe bool FDwfAnalogIOChannelNodeName(HDWF hdwf, int idxChannel, int idxNode, sbyte* szNodeName/*32*/, sbyte* szNodeUnits/*16*/);

        /// <summary>
        /// Returns the node name (“Voltage”, “Current”…) and units (“V”, “A”) for an Analog I/O node.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="idxNode">Node index.</param>
        /// <returns>Node name and units.</returns>
        public static NodeName AnalogIOChannelNodeName(HDWF hdwf, int idxChannel, int idxNode)
        {
            unsafe
            {
                const int name_length = 32;
                var name_buffer = stackalloc sbyte[name_length];
                const int units_length = 16;
                var units_buffer = stackalloc sbyte[units_length];
                var ok = FDwfAnalogIOChannelNodeName(hdwf, idxChannel, idxNode, name_buffer, units_buffer);
                if (ok)
                {
                    var r = new NodeName()
                    {
                        Name = new string(name_buffer, 0, name_length, Encoding.ASCII),
                        Units = new string(units_buffer, 0, units_length, Encoding.ASCII),
                    };
                    return r;
                }
                else
                {
                    throw new ApplicationException(ExceptionMessage("FDwfAnalogIOChannelName"));
                }
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelNodeInfo")]
        static extern bool FDwfAnalogIOChannelNodeInfo(HDWF hdwf, int idxChannel, int idxNode, ref int panalogio);

        /// <summary>
        /// Returns the supported channel nodes.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="idxNode">Node index.</param>
        /// <returns>List of node types.</returns>
        public static List<ANALOGIO> AnalogIOChannelNodeInfo(HDWF hdwf, int idxChannel, int idxNode)
        {
            var bf = CheckApiFunctionHdwfIndexNodeGet<int, int>(FDwfAnalogIOChannelNodeInfo, "FDwfAnalogIOChannelNodeInfo", hdwf, idxChannel, idxNode);
            return AnalogiosOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelNodeSetInfo")]
        static extern bool FDwfAnalogIOChannelNodeSetInfo(HDWF hdwf, int idxChannel, int idxNode, ref double pmin, ref double pmax, ref int pnSteps);

        /// <summary>
        /// Return node value limits. Since a Node can represent many things (Power supply, Temperature sensor, etc.),
        /// the Minimum, Maximum, and Steps parameters also represent different types of values.
        /// In broad terms, the (Maximum – Minimum)/Steps = the number of specific input/output values.
        /// FDwfAnalogIOChannelNodeInfo returns the type of values to expect and
        /// FDwfAnalogIOChannelNodeName returns the units of these values.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="idxNode">Node index.</param>
        /// <returns>Node value limits and number of steps.</returns>
        public static RangeSteps<double,int> AnalogIOChannelNodeSetInfo(HDWF hdwf, int idxChannel, int idxNode)
        {
            return CheckApiFunctionHdwfIndexNodeGetRangeSteps<int, double, int>(FDwfAnalogIOChannelNodeSetInfo, "FDwfAnalogIOChannelNodeSetInfo", hdwf, idxChannel, idxNode);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelNodeSet")]
        static extern bool FDwfAnalogIOChannelNodeSet(HDWF hdwf, int idxChannel, int idxNode, double value);

        /// <summary>
        /// Set the node value for the specified node on the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="idxNode">Node index.</param>
        /// <param name="value">Value to set.</param>
        public static void AnalogIOChannelNodeSet(HDWF hdwf, int idxChannel, int idxNode, double value)
        {
            CheckApiProcedureHdwfIndexNodeSet<int, double>(FDwfAnalogIOChannelNodeSet, "FDwfAnalogIOChannelNodeSet", hdwf, idxChannel, idxNode, value);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelNodeGet")]
        static extern bool FDwfAnalogIOChannelNodeGet(HDWF hdwf, int idxChannel, int idxNode, ref double pvalue);

        /// <summary>
        /// Returns the currently set value of the node on the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="idxNode">Node index.</param>
        /// <returns>Configured value.</returns>
        public static double AnalogIOChannelNodeGet(HDWF hdwf, int idxChannel, int idxNode)
        {
            return CheckApiFunctionHdwfIndexNodeGet<int, double>(FDwfAnalogIOChannelNodeGet, "FDwfAnalogIOChannelNodeGet", hdwf, idxChannel, idxNode);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelNodeStatusInfo")]
        static extern bool FDwfAnalogIOChannelNodeStatusInfo(HDWF hdwf, int idxChannel, int idxNode, ref double pmin, ref double pmax, ref int pnSteps);

        /// <summary>
        /// Returns the range of reading values available for the specified node on the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="idxNode">Node index.</param>
        /// <returns>Range of reading values and the number of steps.</returns>
        public static RangeSteps<double, int> AnalogIOChannelNodeStatusInfo(HDWF hdwf, int idxChannel, int idxNode)
        {
            return CheckApiFunctionHdwfIndexNodeGetRangeSteps<int, double, int>(FDwfAnalogIOChannelNodeStatusInfo, "FDwfAnalogIOChannelNodeStatusInfo", hdwf, idxChannel, idxNode);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogIOChannelNodeStatus")]
        static extern bool FDwfAnalogIOChannelNodeStatus(HDWF hdwf, int idxChannel, int idxNode, ref double pvalue);

        /// <summary>
        /// Returns the value reading of the node.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="idxNode">Node index.</param>
        /// <returns>Value reading.</returns>
        public static double AnalogIOChannelNodeStatus(HDWF hdwf, int idxChannel, int idxNode)
        {
            return CheckApiFunctionHdwfIndexNodeGet<int, double>(FDwfAnalogIOChannelNodeStatus, "FDwfAnalogIOChannelNodeStatus", hdwf, idxChannel, idxNode);
        }
        #endregion // ANALOG IO INSTRUMENT FUNCTIONS - CONFIGURE


        #region DIGITAL IO INSTRUMENT FUNCTIONS - CONTROL

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOReset")]
        static extern bool FDwfDigitalIOReset(HDWF hdwf);

        /// <summary>
        /// Resets and configures (by default, having auto configure enabled) all DigitalIO instrument parameters to default values.
        /// It sets the output enables to zero (tri-state), output value to zero, and configures the DigitalIO instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static void DigitalIOReset(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfDigitalIOReset, "FDwfDigitalIOReset", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOConfigure")]
        static extern bool FDwfDigitalIOConfigure(HDWF hdwf);

        /// <summary>
        /// Configure the DigitalIO instrument. This doesn’t have to be used if AutoConfiguration is enabled.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static void DigitalIOConfigure(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfDigitalIOConfigure, "FDwfDigitalIOConfigure", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOStatus")]
        static extern bool FDwfDigitalIOStatus(HDWF hdwf);

        /// <summary>
        /// Reads the status and input values, of the device DigitalIO to the PC.
        /// The status and values are accessed from the DigitalIOInputStatus function.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static void DigitalIOStatus(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfDigitalIOStatus, "FDwfDigitalIOStatus", hdwf);
        }
        #endregion // DIGITAL IO INSTRUMENT FUNCTIONS - CONTROL

        #region DIGITAL IO INSTRUMENT FUNCTIONS - CONFIGURE
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOOutputEnableInfo")]
        static extern bool FDwfDigitalIOOutputEnableInfo(HDWF hdwf, ref uint pfsOutputEnableMask);

        /// <summary>
        /// Returns the output enable mask (bit set) that can be used on this device.
        /// These are the pins that can be used as outputs on the device.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Output enable mask bit field.</returns>
        public static uint DigitalIOOutputEnableInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalIOOutputEnableInfo, "FDwfDigitalIOOutputEnableInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOOutputEnableSet")]
        static extern bool FDwfDigitalIOOutputEnableSet(HDWF hdwf, uint fsOutputEnable);

        /// <summary>
        /// Enable specific pins for output.
        /// This is done by setting bits in the fsOutEnable bit field (1 for enabled, 0 for disabled).
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="fsOutputEnable">Output enable bit set.</param>
        public static void DigitalIOOutputEnableSet(HDWF hdwf, uint fsOutputEnable)
        {
            CheckApiProcedureHdwfSet<uint>(FDwfDigitalIOOutputEnableSet, "FDwfDigitalIOOutputEnableSet", hdwf, fsOutputEnable);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOOutputEnableGet")]
        static extern bool FDwfDigitalIOOutputEnableGet(HDWF hdwf, ref uint pfsOutputEnable);

        /// <summary>
        /// Returns a bit field that specifies which output pins have been enabled.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Output enable bit set.</returns>
        public static uint DigitalIOOutputEnableGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalIOOutputEnableGet, "FDwfDigitalIOOutputEnableGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOOutputInfo")]
        static extern bool FDwfDigitalIOOutputInfo(HDWF hdwf, ref uint pfsOutputMask);

        /// <summary>
        /// Returns the settable output value mask (bit set) that can be used on this device.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Output value mask.</returns>
        public static uint DigitalIOOutputInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalIOOutputInfo, "FDwfDigitalIOOutputInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOOutputSet")]
        static extern bool FDwfDigitalIOOutputSet(HDWF hdwf, uint fsOutput);

        /// <summary>
        /// Set the output logic value on all output pins.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="fsOutput">Output bit set.</param>
        public static void DigitalIOOutputSet(HDWF hdwf, uint fsOutput)
        {
            CheckApiProcedureHdwfSet<uint>(FDwfDigitalIOOutputSet, "FDwfDigitalIOOutputSet", hdwf, fsOutput);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOOutputGet")]
        static extern bool FDwfDigitalIOOutputGet(HDWF hdwf, ref uint pfsOutput);

        /// <summary>
        /// Returns the currently set output values across all output pins.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Output bit set.</returns>
        public static uint DigitalIOOutputGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalIOOutputGet, "FDwfDigitalIOOutputGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOInputInfo")]
        static extern bool FDwfDigitalIOInputInfo(HDWF hdwf, ref uint pfsInputMask);

        /// <summary>
        /// Returns the readable input value mask (bit set) that can be used on the device.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Input value mask.</returns>
        public static uint DigitalIOInputInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalIOInputInfo, "FDwfDigitalIOInputInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalIOInputStatus")]
        static extern bool FDwfDigitalIOInputStatus(HDWF hdwf, ref uint pfsInput);

        /// <summary>
        /// Returns the input states of all I/O pins. Before calling the function above,
        /// call the FDwfDigitalIOStatus function to read the Digital I/O states from the device.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Input value.</returns>
        public static uint DigitalIOInputStatus(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalIOInputStatus, "FDwfDigitalIOInputStatus", hdwf);
        }
        #endregion // DIGITAL IO INSTRUMENT FUNCTIONS - CONFIGURE

        #region DIGITAL IN INSTRUMENT FUNCTIONS - CONTROL AND STATUS
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInReset")]
        static extern bool FDwfDigitalInReset(HDWF hdwf);

        /// <summary>
        /// Resets and configures (by default, having auto configure enabled) all DigitalIn instrument
        /// parameters to default values.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static void DigitalInReset(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfDigitalInReset, "FDwfDigitalInReset", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInConfigure")]
        static extern bool FDwfDigitalInConfigure(HDWF hdwf, bool fReconfigure, bool fStart);

        /// <summary>
        /// Configure the instrument and start or stop the acquisition.
        /// To reset the Auto trigger timeout, set fReconfigure to TRUE.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="fReconfigure">Configure the device.</param>
        /// <param name="fStart">Start the acquisition.</param>
        public static void DigitalInConfigure(HDWF hdwf, bool fReconfigure, bool fStart)
        {
            CheckApiProcedureHdwfIndexSet<bool, bool>(FDwfDigitalInConfigure, "FDwfDigitalInConfigure", hdwf, fReconfigure, fStart);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInStatus")]
        static extern bool FDwfDigitalInStatus(HDWF hdwf, bool fReadData, ref STATE psts);

        /// <summary>
        /// Check the state of the instrument.
        /// To read the data from the device, set fReadData to TRUE.
        /// For single acquisition mode, the data will be read only when the acquisition is finished.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="fReadData">TRUE if data should be read.</param>
        /// <returns>Acquisition state.</returns>
        public static STATE DigitalInStatus(HDWF hdwf, bool fReadData)
        {
            return CheckApiFunctionHdwfIndexGet<bool, STATE>(FDwfDigitalInStatus, "FDwfDigitalInStatus", hdwf, fReadData);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInStatusSamplesLeft")]
        static extern bool FDwfDigitalInStatusSamplesLeft(HDWF hdwf, ref int pcSamplesLeft);

        /// <summary>
        /// Retrieve the number of samples left in the acquisition.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Remaining samples to acquire.</returns>
        public static int DigitalInStatusSamplesLeft(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfDigitalInStatusSamplesLeft, "FDwfDigitalInStatusSamplesLeft", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInStatusSamplesValid")]
        static extern bool FDwfDigitalInStatusSamplesValid(HDWF hdwf, ref int pcSamplesValid);

        /// <summary>
        /// Retrieve the number of valid/acquired data samples.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Number of valid samples.</returns>
        public static int DigitalInStatusSamplesValid(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfDigitalInStatusSamplesValid, "FDwfDigitalInStatusSamplesValid", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInStatusIndexWrite")]
        static extern bool FDwfDigitalInStatusIndexWrite(HDWF hdwf, ref int pidxWrite);

        /// <summary>
        /// Retrieve the buffer write pointer.
        /// This is needed in ScanScreen acquisition mode to display the scan bar.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Position of the acquisition.</returns>
        public static int DigitalInStatusIndexWrite(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfDigitalInStatusIndexWrite, "FDwfDigitalInStatusIndexWrite", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInStatusAutoTriggered")]
        static extern bool FDwfDigitalInStatusAutoTriggered(HDWF hdwf, ref bool pfAuto);

        /// <summary>
        /// Verify if the acquisition is auto triggered.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>TRUE if the acquisition was auto triggered.</returns>
        public static bool DigitalInStatusAutoTriggered(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<bool>(FDwfDigitalInStatusAutoTriggered, "FDwfDigitalInStatusAutoTriggered", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInStatusData")]
        static extern unsafe bool FDwfDigitalInStatusData(HDWF hdwf, byte* rgData, int countOfDataBytes);

        /// <summary>
        /// Retrieve the acquired data samples from the instrument.
        /// The sample format is specified by FDwfDigitalInSampleFormatSetfunction.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="countOfDataBytes">Number of samples to copy.</param>
        /// <returns>Acquisition data.</returns>
        public static byte[] DigitalInStatusData(HDWF hdwf, int countOfDataBytes)
        {
            unsafe
            {
                var bytes = new byte[countOfDataBytes];
                fixed (byte* ptr = bytes)
                {
                    var ok = FDwfDigitalInStatusData(hdwf, ptr, countOfDataBytes);
                    if (ok)
                    {
                        return bytes;
                    }
                    else
                    {
                        throw new ApplicationException(ExceptionMessage("FDwfDigitalInStatusData"));
                    }
                }
            }
        }
        #endregion // DIGITAL IN INSTRUMENT FUNCTIONS - CONTROL AND STATUS

        #region DIGITAL IN INSTRUMENT FUNCTIONS - ACQUISITION CONFIGURATION

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInInternalClockInfo")]
        static extern bool FDwfDigitalInInternalClockInfo(HDWF hdwf, ref double phzFreq);

        /// <summary>
        /// Retrieve the internal clock frequency.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Internal clock frequency.</returns>
        public static double DigitalInInternalClockInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfDigitalInInternalClockInfo, "FDwfDigitalInInternalClockInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInClockSourceInfo")]
        static extern bool FDwfDigitalInClockSourceInfo(HDWF hdwf, ref int pfsDwfDigitalInClockSource); // use IsBitSet

        /// <summary>
        /// Returns the supported clock sources for Digital In instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>List of supported clock sources.</returns>
        public static List<DIGITALINCLOCKSOURCE> DigitalInClockSourceInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfDigitalInClockSourceInfo, "FDwfDigitalInClockSourceInfo", hdwf);
            return DigitalinclocksourcesOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInClockSourceSet")]      
        static extern bool FDwfDigitalInClockSourceSet(HDWF hdwf, DIGITALINCLOCKSOURCE v);

        /// <summary>
        /// Set the clock source of instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="v">Clock source.</param>
        public static void DigitalInClockSourceSet(HDWF hdwf, DIGITALINCLOCKSOURCE v)
        {
            CheckApiProcedureHdwfSet<DIGITALINCLOCKSOURCE>(FDwfDigitalInClockSourceSet, "FDwfDigitalInClockSourceSet", hdwf, v);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInClockSourceGet")]
        static extern bool FDwfDigitalInClockSourceGet(HDWF hdwf, ref DIGITALINCLOCKSOURCE pv);

        /// <summary>
        /// Get the clock source of instrument
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Configured clock source</returns>
        public static DIGITALINCLOCKSOURCE DigitalInClockSourceGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<DIGITALINCLOCKSOURCE>(FDwfDigitalInClockSourceGet, "FDwfDigitalInClockSourceGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInDividerInfo")]
        static extern bool FDwfDigitalInDividerInfo(HDWF hdwf, ref uint pdivMax);

        /// <summary>
        /// Returns the maximum supported clock divider value. This specifies the sample rate.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>available maximum divider value</returns>
        public static uint  DigitalInDividerInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalInDividerInfo, "FDwfDigitalInDividerInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInDividerSet")]
        static extern bool FDwfDigitalInDividerSet(HDWF hdwf, uint div);

        /// <summary>
        /// Set the clock divider value.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="div">Divider value.</param>
        public static void DigitalInDividerSet(HDWF hdwf, uint div)
        {
            CheckApiProcedureHdwfSet<uint>(FDwfDigitalInDividerSet, "FDwfDigitalInDividerSet", hdwf, div);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInDividerGet")]
        static extern bool FDwfDigitalInDividerGet(HDWF hdwf, ref uint pdiv);

        /// <summary>
        /// Get the configured clock divider value.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Configured clock divider.</returns>
        public static uint DigitalInDividerGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalInDividerGet, "FDwfDigitalInDividerGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInBitsInfo")]
        static extern bool FDwfDigitalInBitsInfo(HDWF hdwf, ref int pnBits); // Returns the number of Digital In bits

        /// <summary>
        /// Returns the number of Digital In bits.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Number of bits.</returns>
        public static int DigitalInBitsInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfDigitalInBitsInfo, "FDwfDigitalInBitsInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInSampleFormatSet")]
        static extern bool FDwfDigitalInSampleFormatSet(HDWF hdwf, int nBits);  // valid options 8/16/32

        /// <summary>
        /// Set the sample format, the number of bits starting from least significant bit.
        /// Valid options are 8, 16, and 32.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="nBits">Sample format.</param>
        public static void DigitalInSampleFormatSet(HDWF hdwf, int nBits)
        {
            CheckApiProcedureHdwfSet<int>(FDwfDigitalInSampleFormatSet, "FDwfDigitalInSampleFormatSet", hdwf, nBits);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInSampleFormatGet")]
        static extern bool FDwfDigitalInSampleFormatGet(HDWF hdwf, ref int pnBits);

        /// <summary>
        /// Return the configured sample format.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Configured sample format</returns>
        public static int DigitalInSampleFormatGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfDigitalInSampleFormatGet, "FDwfDigitalInSampleFormatGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInBufferSizeInfo")]
        static extern bool FDwfDigitalInBufferSizeInfo(HDWF hdwf, ref int pnSizeMax);

        /// <summary>
        /// Returns the Digital In maximum buffer size.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Maximum buffer size.</returns>
        public static int DigitalInBufferSizeInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfDigitalInBufferSizeInfo, "FDwfDigitalInBufferSizeInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInBufferSizeSet")]
        static extern bool FDwfDigitalInBufferSizeSet(HDWF hdwf, int nSize);

        /// <summary>
        /// Set the buffer size.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="nSize">Buffer size.</param>
        public static void DigitalInBufferSizeSet(HDWF hdwf, int nSize)
        {
            CheckApiProcedureHdwfSet<int>(FDwfDigitalInBufferSizeSet, "FDwfDigitalInBufferSizeSet", hdwf, nSize);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInBufferSizeGet")]
        static extern bool FDwfDigitalInBufferSizeGet(HDWF hdwf, ref int pnSize);

        /// <summary>
        /// Return the configured buffer size.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>configured buffer size.</returns>
        public static int DigitalInBufferSizeGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfDigitalInBufferSizeGet, "FDwfDigitalInBufferSizeGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInSampleModeInfo")]
        static extern bool FDwfDigitalInSampleModeInfo(HDWF hdwf, ref int pfsDwfDigitalInSampleMode); // use IsBitSet

        /// <summary>
        /// Returns the supported sample modes.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>List of the supported sample modes.</returns>
        public static List<DIGITALINSAMPLEMODE> DigitalInSampleModeInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfDigitalInSampleModeInfo, "FDwfDigitalInSampleModeInfo", hdwf);
            return DigitalinsamplemodesOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInSampleModeSet")]
        static extern bool FDwfDigitalInSampleModeSet(HDWF hdwf, DIGITALINSAMPLEMODE v);  // 

        /// <summary>
        /// Set the sample mode.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="v">Sample mode.</param>
        public static void DigitalInSampleModeSet(HDWF hdwf, DIGITALINSAMPLEMODE v)
        {
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInSampleModeGet")]
        static extern bool FDwfDigitalInSampleModeGet(HDWF hdwf, ref DIGITALINSAMPLEMODE pv);

        /// <summary>
        /// Return the configured sample mode.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Configured sample mode.</returns>
        public static DIGITALINSAMPLEMODE DigitalInSampleModeGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<DIGITALINSAMPLEMODE>(FDwfDigitalInSampleModeGet, "FDwfDigitalInSampleModeGet", hdwf);            
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInAcquisitionModeInfo")]
        static extern bool FDwfDigitalInAcquisitionModeInfo(HDWF hdwf, ref int pfsacqmode); // use IsBitSet

        /// <summary>
        /// Returns the supported AnalogIn acquisition modes.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>List of acquisition modes.</returns>
        public static List<ACQMODE> DigitalInAcquisitionModeInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfDigitalInAcquisitionModeInfo, "FDwfDigitalInAcquisitionModeInfo", hdwf);
            return AcqmodesOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInAcquisitionModeSet")]
        static extern bool FDwfDigitalInAcquisitionModeSet(HDWF hdwf, ACQMODE acqmode);

        /// <summary>
        /// Set the acquisition mode.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="acqmode"></param>
        public static void DigitalInAcquisitionModeSet(HDWF hdwf, ACQMODE acqmode)
        {
            CheckApiProcedureHdwfSet<ACQMODE>(FDwfDigitalInAcquisitionModeSet, "FDwfDigitalInAcquisitionModeSet", hdwf, acqmode);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInAcquisitionModeGet")]
        static extern bool FDwfDigitalInAcquisitionModeGet(HDWF hdwf, ref ACQMODE pacqmode);

        /// <summary>
        /// Retrieve the acquisition mode.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Current acquisition mode.</returns>
        public static ACQMODE DigitalInAcquisitionModeGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<ACQMODE>(FDwfDigitalInAcquisitionModeGet, "FDwfDigitalInAcquisitionModeGet", hdwf);
        }
        #endregion // DIGITAL IN INSTRUMENT FUNCTIONS - ACQUISITION CONFIGURATION

        #region DIGITAL IN INSTRUMENT FUNCTIONS - TRIGGER CONFIGURATION
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerSourceInfo")]
        static extern bool FDwfDigitalInTriggerSourceInfo(HDWF hdwf, ref int pfstrigsrc); // use IsBitSet

        /// <summary>
        /// Returns the supported trigger source options for the instrument
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>List of supported trigger sources.</returns>
        public static List<TRIGSRC> DigitalInTriggerSourceInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfDigitalInTriggerSourceInfo, "FDwfDigitalInTriggerSourceInfo", hdwf);
            return TrigsrcsOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerSourceSet")]
        static extern bool FDwfDigitalInTriggerSourceSet(HDWF hdwf, TRIGSRC trigsrc);

        /// <summary>
        /// Set the trigger source for the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="trigsrc">Trigger source to set.</param>
        public static void DigitalInTriggerSourceSet(HDWF hdwf, TRIGSRC trigsrc)
        {
            CheckApiProcedureHdwfSet<TRIGSRC>(FDwfDigitalInTriggerSourceSet, "FDwfDigitalInTriggerSourceSet", hdwf, trigsrc);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerSourceGet")]
        static extern bool FDwfDigitalInTriggerSourceGet(HDWF hdwf, ref TRIGSRC ptrigsrc);

        /// <summary>
        /// Get the current trigger source setting for the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Trigger source.</returns>
        public static TRIGSRC DigitalInTriggerSourceGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<TRIGSRC>(FDwfDigitalInTriggerSourceGet, "FDwfDigitalInTriggerSourceGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerPositionInfo")]
        static extern bool FDwfDigitalInTriggerPositionInfo(HDWF hdwf, ref uint pnSamplesAfterTriggerMax);

        /// <summary>
        /// Returns maximum values of the trigger position in samples.
        /// This can be greater than the specified buffer size.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Maximum trigger position.</returns>
        public static uint DigitalInTriggerPositionInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalInTriggerPositionInfo, "FDwfDigitalInTriggerPositionInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerPositionSet")]
        static extern bool FDwfDigitalInTriggerPositionSet(HDWF hdwf, uint cSamplesAfterTrigger);

        /// <summary>
        /// Set the number of samples to acquire after trigger
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="cSamplesAfterTrigger">Samples after trigger.</param>
        public static void DigitalInTriggerPositionSet(HDWF hdwf, uint cSamplesAfterTrigger)
        {
            CheckApiProcedureHdwfSet<uint>(FDwfDigitalInTriggerPositionSet, "FDwfDigitalInTriggerPositionSet", hdwf, cSamplesAfterTrigger);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerPositionGet")]
        static extern bool FDwfDigitalInTriggerPositionGet(HDWF hdwf, ref uint pcSamplesAfterTrigger);

        /// <summary>
        /// Get the configured trigger position.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Configured trigger position.</returns>
        public static uint DigitalInTriggerPositionGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalInTriggerPositionGet, "FDwfDigitalInTriggerPositionGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerAutoTimeoutInfo")]
        static extern bool FDwfDigitalInTriggerAutoTimeoutInfo(HDWF hdwf, ref double psecMin, ref double psecMax, ref double pnSteps);

        /// <summary>
        /// Returns the minimum and maximum auto trigger timeout values, and the number of adjustable steps.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Trigger timeout range and number of steps.</returns>
        public static RangeSteps<double, double> DigitalInTriggerAutoTimeoutInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRangeSteps<double, double>(FDwfDigitalInTriggerAutoTimeoutInfo, "FDwfDigitalInTriggerAutoTimeoutInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerAutoTimeoutSet")]
        static extern bool FDwfDigitalInTriggerAutoTimeoutSet(HDWF hdwf, double secTimeout);

        /// <summary>
        /// Configure the auto trigger timeout value in seconds.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="secTimeout">Timeout to set, in seconds.</param>
        public static void DigitalInTriggerAutoTimeoutSet(HDWF hdwf, double secTimeout)
        {
            CheckApiProcedureHdwfSet<double>(FDwfDigitalInTriggerAutoTimeoutSet, "FDwfDigitalInTriggerAutoTimeoutSet", hdwf, secTimeout);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerAutoTimeoutGet")]
        static extern bool FDwfDigitalInTriggerAutoTimeoutGet(HDWF hdwf, ref double psecTimeout);

        /// <summary>
        /// Returns the configured auto trigger timeout value in seconds.
        /// The acquisition is auto triggered when the specified time elapses.
        /// With zero value the timeout is disabled, performing “Normal” acquisitions.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static double DigitalInTriggerAutoTimeoutGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfDigitalInTriggerAutoTimeoutGet, "FDwfDigitalInTriggerAutoTimeoutGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerInfo")]
        static extern bool FDwfDigitalInTriggerInfo(HDWF hdwf, ref uint pfsLevelLow, ref uint pfsLevelHigh, ref uint pfsEdgeRise, ref uint pfsEdgeFall);

        /// <summary>
        /// Returns the supported digital in triggers. The bits of the arguments represent pins.
        /// 
        /// The logic for the trigger bits is: Low and High and (Rise or Fall).
        /// Setting a bit in both rise and fall will trigger on any edge, any transition.
        /// For instance FDwfDigitalInTriggerInfo(hdwf, 1, 2, 4, 8) will generate trigger when DIO-0 is low and DIO-1 is high and DIO-2 is rising or DIO-3 is falling.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Supported digital in triggers.</returns>
        public static DigitalTriggerInfo DigitalInTriggerInfo(HDWF hdwf)
        {
            var r = new DigitalTriggerInfo();
            var ok = FDwfDigitalInTriggerInfo(hdwf, ref r.LevelLow, ref r.LevelHigh, ref r.EdgeRise, ref r.EdgeFall);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfDigitalInTriggerInfo"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerSet")]
        static extern bool FDwfDigitalInTriggerSet(HDWF hdwf, uint fsLevelLow, uint fsLevelHigh, uint fsEdgeRise, uint fsEdgeFall);

        /// <summary>
        /// Configure the digital in trigger detector.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="fsLevelLow">Set low state condition.</param>
        /// <param name="fsLevelHigh">Set high state condition.</param>
        /// <param name="fsEdgeRise">Set rising edge condition.</param>
        /// <param name="fsEdgeFall">Set falling edge condition.</param>
        public static void DigitalInTriggerSet(HDWF hdwf, uint fsLevelLow, uint fsLevelHigh, uint fsEdgeRise, uint fsEdgeFall)
        {
            var ok = FDwfDigitalInTriggerSet(hdwf, fsLevelLow, fsLevelHigh, fsEdgeRise, fsEdgeFall);
            if (!ok)
            {
                throw new ApplicationException(ExceptionMessage("FDwfDigitalInTriggerSet"));
            }
        }

        /// <summary>
        /// Configure the digital in trigger detector.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="trigger">Trigger information.</param>
        public static void DigitalInTriggerSet(HDWF hdwf, DigitalTriggerInfo trigger)
        {
            var ok = FDwfDigitalInTriggerSet(hdwf, trigger.LevelLow, trigger.LevelHigh, trigger.EdgeRise, trigger.EdgeFall);
            if (!ok)
            {
                throw new ApplicationException(ExceptionMessage("FDwfDigitalInTriggerSet"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalInTriggerGet")]
        static extern bool FDwfDigitalInTriggerGet(HDWF hdwf, ref uint pfsLevelLow, ref uint pfsLevelHigh, ref uint pfsEdgeRise, ref uint pfsEdgeFall);
        // the logic for trigger bits: Low and High and (Rise or Fall)
        // bits set in Rise and Fall means any edge

        /// <summary>
        /// Returns the configured digital in trigger detector option.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="trigger">Trigger information.</param>
        public static DigitalTriggerInfo DigitalInTriggerGet(HDWF hdwf)
        {
            var r = new DigitalTriggerInfo();
            var ok = FDwfDigitalInTriggerGet(hdwf, ref r.LevelLow, ref r.LevelHigh, ref r.EdgeRise, ref r.EdgeFall);
            if (ok)
            {
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfDigitalInTriggerGet"));
            }
        }
        #endregion DIGITAL IN INSTRUMENT FUNCTIONS - TRIGGER CONFIGURATION

        #region DIGITAL OUT INSTRUMENT FUNCTIONS - CONTROL
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutReset")]
        static extern bool FDwfDigitalOutReset(HDWF hdwf);

        /// <summary>
        /// Resets and configures (by default, having auto configure enabled) all of the instrument parameters to default values.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        public static void DigitalOutReset(HDWF hdwf)
        {
            CheckApiProcedureHdwf(FDwfDigitalOutReset, "FDwfDigitalOutReset", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutConfigure")]
        static extern bool FDwfDigitalOutConfigure(HDWF hdwf, bool fStart);

        /// <summary>
        /// Start or stop the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="fStart">Start the acquisition. To stop, set to FALSE.</param>
        public static void DigitalOutConfigure(HDWF hdwf, bool fStart)
        {
            CheckApiProcedureHdwfSet<bool>(FDwfDigitalOutConfigure, "FDwfDigitalOutConfigure", hdwf, fStart);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutStatus")]
        static extern bool FDwfDigitalOutStatus(HDWF hdwf, ref STATE psts);

        /// <summary>
        /// Check the state of the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>State of the instrument.</returns>
        public static STATE DigitalOutStatus(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<STATE>(FDwfDigitalOutStatus, "FDwfDigitalOutStatus", hdwf);
        }
        #endregion // DIGITAL OUT INSTRUMENT FUNCTIONS - CONTROL

        #region DIGITAL OUT INSTRUMENT FUNCTIONS - CONFIGURATION

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutInternalClockInfo")]
        static extern bool FDwfDigitalOutInternalClockInfo(HDWF hdwf, ref double phzFreq);

        /// <summary>
        /// Retrieve the internal clock frequency.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Internal clock frequency.</returns>
        public static double DigitalOutInternalClockInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfDigitalOutInternalClockInfo, "FDwfDigitalOutInternalClockInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutTriggerSourceInfo")]
        static extern bool FDwfDigitalOutTriggerSourceInfo(HDWF hdwf, ref int pfstrigsrc); // use IsBitSet

        /// <summary>
        /// Returns the supported trigger source options for the instrument. See the description of DeviceTriggerInfo.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>List of supported trigger sources.</returns>
        public static List<TRIGSRC> DigitalOutTriggerSourceInfo(HDWF hdwf)
        {
            var bf = CheckApiFunctionHdwfGet<int>(FDwfDigitalOutTriggerSourceInfo, "FDwfDigitalOutTriggerSourceInfo", hdwf);
            return TrigsrcsOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutTriggerSourceSet")]
        static extern bool FDwfDigitalOutTriggerSourceSet(HDWF hdwf, TRIGSRC trigsrc);

        /// <summary>
        /// Set the trigger source for the instrument. Default setting is TRIGSRC.None.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="trigsrc">Trigger source to set.</param>
        public static void DigitalOutTriggerSourceSet(HDWF hdwf, TRIGSRC trigsrc)
        {
            CheckApiProcedureHdwfSet<TRIGSRC>(FDwfDigitalOutTriggerSourceSet, "FDwfDigitalOutTriggerSourceSet", hdwf, trigsrc);
        }
        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutTriggerSourceGet")]
        static extern bool FDwfDigitalOutTriggerSourceGet(HDWF hdwf, ref TRIGSRC ptrigsrc);

        /// <summary>
        /// Get the current trigger source setting for the instrument.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Trigger source.</returns>
        public static TRIGSRC DigitalOutTriggerSourceGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<TRIGSRC>(FDwfDigitalOutTriggerSourceGet, "FDwfDigitalOutTriggerSourceGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRunInfo")]
        static extern bool FDwfDigitalOutRunInfo(HDWF hdwf, ref double psecMin, ref double psecMax);

        /// <summary>
        /// Return the supported run length range for the instrument in seconds.
        /// Zero value (default) represent an infinite (or continuous) run.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Range of the supported run length.</returns>
        public static Range<double> DigitalOutRunInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRange<double>(FDwfDigitalOutRunInfo, "FDwfDigitalOutRunInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRunSet")]
        static extern bool FDwfDigitalOutRunSet(HDWF hdwf, double secRun);

        /// <summary>
        /// Set the run length for the instrument in Seconds.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="secRun">Run length to set expressed in seconds.</param>
        public static void DigitalOutRunSet(HDWF hdwf, double secRun)
        {
            CheckApiProcedureHdwfSet<double>(FDwfDigitalOutRunSet, "FDwfDigitalOutRunSet", hdwf, secRun);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRunGet")]
        static extern bool FDwfDigitalOutRunGet(HDWF hdwf, ref double psecRun);

        /// <summary>
        /// Read the configured run length for the instrument in seconds.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Run length.</returns>
        public static double DigitalOutRunGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfDigitalOutRunGet, "FDwfDigitalOutRunGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRunStatus")]
        static extern bool FDwfDigitalOutRunStatus(HDWF hdwf, ref double psecRun);

        /// <summary>
        /// Read the remaining run length. It returns data from the last DigitalOutStatus call.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Remaining run length.</returns>
        public static double DigitalOutRunStatus(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfDigitalOutRunStatus, "FDwfDigitalOutRunStatus", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutWaitInfo")]
        static extern bool FDwfDigitalOutWaitInfo(HDWF hdwf, ref double psecMin, ref double psecMax);

        /// <summary>
        /// Return the supported wait length range in seconds.
        /// The wait length is how long the instrument waits after being triggered to generate the signal.
        /// Default value is zero.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Range of the supported wait length.</returns>
        public static Range<double> DigitalOutWaitInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRange<double>(FDwfDigitalOutWaitInfo, "FDwfDigitalOutWaitInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutWaitSet")]
        static extern bool FDwfDigitalOutWaitSet(HDWF hdwf, double secWait);

        /// <summary>
        /// Set the wait length.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="secWait">Wait length to set expressed in seconds.</param>
        public static void DigitalOutWaitSet(HDWF hdwf, double secWait)
        {
            CheckApiProcedureHdwfSet<double>(FDwfDigitalOutWaitSet, "FDwfDigitalOutWaitSet", hdwf, secWait);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutWaitGet")]
        static extern bool FDwfDigitalOutWaitGet(HDWF hdwf, ref double psecWait);

        /// <summary>
        /// Get the current wait length.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Wait length.</returns>
        public static double DigitalOutWaitGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<double>(FDwfDigitalOutWaitGet, "FDwfDigitalOutWaitGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRepeatInfo")]
        static extern bool FDwfDigitalOutRepeatInfo(HDWF hdwf, ref uint pnMin, ref uint pnMax);

        /// <summary>
        /// Return the supported repeat count range.
        /// This is how many times the generated signal will be repeated.
        /// Zero value represents infinite repeats.
        /// Default value is one.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Range of supported repeat counts.</returns>
        public static Range<uint> DigitalOutRepeatInfo(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGetRange<uint>(FDwfDigitalOutRepeatInfo, "FDwfDigitalOutRepeatInfo", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRepeatSet")]
        static extern bool FDwfDigitalOutRepeatSet(HDWF hdwf, uint cRepeat);

        /// <summary>
        /// Set the repeat count.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="cRepeat">Repeat count to set.</param>
        public static void DigitalOutRepeatSet(HDWF hdwf, uint cRepeat)
        {
            CheckApiProcedureHdwfSet<uint>(FDwfDigitalOutRepeatSet, "FDwfDigitalOutRepeatSet", hdwf, cRepeat);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRepeatGet")]
        static extern bool FDwfDigitalOutRepeatGet(HDWF hdwf, ref uint pcRepeat);

        /// <summary>
        /// Read the current repeat count.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Repeat count.</returns>
        public static uint DigitalOutRepeatGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalOutRepeatGet, "FDwfDigitalOutRepeatGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRepeatStatus")]
        static extern bool FDwfDigitalOutRepeatStatus(HDWF hdwf, ref uint pcRepeat);

        /// <summary>
        /// Read the remaining repeat counts.
        /// It only returns information from the last DigitalOutStatus function call, it does not read from the device.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Remaining repeat counts.</returns>
        public static uint DigitalOutRepeatStatus(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<uint>(FDwfDigitalOutRepeatStatus, "FDwfDigitalOutRepeatStatus", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRepeatTriggerSet")]
        static extern bool FDwfDigitalOutRepeatTriggerSet(HDWF hdwf, bool fRepeatTrigger);

        /// <summary>
        /// Set the repeat trigger option.
        /// To include the trigger in wait-run repeat cycles, set fRepeatTrigger to TRUE. It is disabled by default.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="fRepeatTrigger">Boolean used to specify if the trigger should be included in a repeat cycle.</param>
        public static void DigitalOutRepeatTriggerSet(HDWF hdwf, bool fRepeatTrigger)
        {
            CheckApiProcedureHdwfSet<bool>(FDwfDigitalOutRepeatTriggerSet, "FDwfDigitalOutRepeatTriggerSet", hdwf, fRepeatTrigger);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutRepeatTriggerGet")]
        static extern bool FDwfDigitalOutRepeatTriggerGet(HDWF hdwf, ref bool pfRepeatTrigger);

        /// <summary>
        /// Verify if the trigger has been included in wait-run repeat cycles.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Repeat trigger option.</returns>
        public static bool DigitalOutRepeatTriggerGet(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<bool>(FDwfDigitalOutRepeatTriggerGet, "FDwfDigitalOutRepeatTriggerGet", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutCount")]
        static extern bool FDwfDigitalOutCount(HDWF hdwf, ref int pcChannel);

        /// <summary>
        /// Returns the number of Digital Out channels by the device specified by hdwf.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <returns>Number of channels in the instrument.</returns>
        public static int DigitalOutCount(HDWF hdwf)
        {
            return CheckApiFunctionHdwfGet<int>(FDwfDigitalOutCount, "FDwfDigitalOutCount", hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutEnableSet")]
        static extern bool FDwfDigitalOutEnableSet(HDWF hdwf, int idxChannel, bool fEnable);

        /// <summary>
        /// Enables or disables the channel specified by idxChannel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="fEnable">TRUE to enable, FALSE to disable.</param>
        public static void DigitalOutEnableSet(HDWF hdwf, int idxChannel, bool fEnable)
        {
            CheckApiProcedureHdwfIndexSet<int, bool>(FDwfDigitalOutEnableSet, "FDwfDigitalOutEnableSet", hdwf, idxChannel, fEnable);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutEnableGet")]
        static extern bool FDwfDigitalOutEnableGet(HDWF hdwf, int idxChannel, ref bool pfEnable);

        /// <summary>
        /// Verify if a specific channel is enabled or disabled.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Enabled state.</returns>
        public static bool DigitalOutEnableGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, bool>(FDwfDigitalOutEnableGet, "FDwfDigitalOutEnableGet", idxChannel, hdwf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutOutputInfo")]
        static extern bool FDwfDigitalOutOutputInfo(HDWF hdwf, int idxChannel, ref int pfsDwfDigitalOutOutput); // use IsBitSet

        /// <summary>
        /// Returns the supported output modes of the channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns></returns>
        public static List<DIGITALOUTOUTPUT> DigitalOutOutputInfo(HDWF hdwf, int idxChannel)
        {
            var bf = CheckApiFunctionHdwfIndexGet<int, int>(FDwfDigitalOutOutputInfo, "FDwfDigitalOutOutputInfo", idxChannel, hdwf);
            return DigitaloutoutputsOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutOutputSet")]
        static extern bool FDwfDigitalOutOutputSet(HDWF hdwf, int idxChannel, DIGITALOUTOUTPUT v);

        /// <summary>
        /// Specifies output mode of the channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="v">Output mode.</param>
        static void DigitalOutOutputSet(HDWF hdwf, int idxChannel, DIGITALOUTOUTPUT v)
        {
            CheckApiProcedureHdwfIndexSet<int, DIGITALOUTOUTPUT>(FDwfDigitalOutOutputSet, "FDwfDigitalOutOutputSet", hdwf, idxChannel, v);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutOutputGet")]
        static extern bool FDwfDigitalOutOutputGet(HDWF hdwf, int idxChannel, ref DIGITALOUTOUTPUT pv);

        /// <summary>
        /// Verify if a specific channel output mode.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Enabled state.</returns>
        public static DIGITALOUTOUTPUT DigitalOutOutputGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, DIGITALOUTOUTPUT>(FDwfDigitalOutOutputGet, "FDwfDigitalOutOutputGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutTypeInfo")]
        static extern bool FDwfDigitalOutTypeInfo(HDWF hdwf, int idxChannel, ref int pfsDwfDigitalOutType); // use IsBitSet

        /// <summary>
        /// Returns the supported types of the channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>List of supported output types.</returns>
        public static List<DIGITALOUTTYPE> DigitalOutTypeInfo(HDWF hdwf, int idxChannel)
        {
            var bf = CheckApiFunctionHdwfIndexGet<int, int>(FDwfDigitalOutTypeInfo, "FDwfDigitalOutTypeInfo", hdwf, idxChannel);
            return DigitalouttypesOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutTypeSet")]
        static extern bool FDwfDigitalOutTypeSet(HDWF hdwf, int idxChannel, DIGITALOUTTYPE v);

        /// <summary>
        /// Sets the output type of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="v">Output mode.</param>
        public static void DigitalOutTypeSet(HDWF hdwf, int idxChannel, DIGITALOUTTYPE v)
        {
            CheckApiProcedureHdwfIndexSet<int, DIGITALOUTTYPE>(FDwfDigitalOutTypeSet, "FDwfDigitalOutTypeSet", hdwf, idxChannel, v);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutTypeGet")]
        static extern bool FDwfDigitalOutTypeGet(HDWF hdwf, int idxChannel, ref DIGITALOUTTYPE pv);

        /// <summary>
        /// Verify the type of a specific channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Enabled state.</returns>
        public static DIGITALOUTTYPE DigitalOutTypeGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, DIGITALOUTTYPE>(FDwfDigitalOutTypeGet, "FDwfDigitalOutTypeGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutIdleInfo")]
        static extern bool FDwfDigitalOutIdleInfo(HDWF hdwf, int idxChannel, ref int pfsDwfDigitalOutIdle); // use IsBitSet

        /// <summary>
        /// Returns the supported idle output types of the channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>List of supported idle output types.</returns>
        public static List<DIGITALOUTIDLE> DigitalOutIdleInfo(HDWF hdwf, int idxChannel)
        {
            var bf = CheckApiFunctionHdwfIndexGet<int, int>(FDwfDigitalOutIdleInfo, "FDwfDigitalOutIdleInfo", hdwf, idxChannel);
            return DigitaloutidlesOfBitfield(bf);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutIdleSet")]
        static extern bool FDwfDigitalOutIdleSet(HDWF hdwf, int idxChannel, DIGITALOUTIDLE v);

        /// <summary>
        /// Sets the idle output of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="v">Value to set idle output.</param>
        public static void DigitalOutIdleSet(HDWF hdwf, int idxChannel, DIGITALOUTIDLE v)
        {
            CheckApiProcedureHdwfIndexSet<int, DIGITALOUTIDLE>(FDwfDigitalOutIdleSet, "FDwfDigitalOutIdleSet", hdwf, idxChannel, v);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutIdleGet")]
        static extern bool FDwfDigitalOutIdleGet(HDWF hdwf, int idxChannel, ref DIGITALOUTIDLE pv);

        /// <summary>
        /// Verify the idle output of a specific channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Configured value of the idle output.</returns>
        public static DIGITALOUTIDLE DigitalOutIdleGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, DIGITALOUTIDLE>(FDwfDigitalOutIdleGet, "FDwfDigitalOutIdleGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutDividerInfo")]
        static extern bool FDwfDigitalOutDividerInfo(HDWF hdwf, int idxChannel, ref uint vMin, ref uint vMax);

        /// <summary>
        /// Return the supported clock divider value range.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Range of supported divider values.</returns>
        public static Range<uint> DigitalOutDividerInfo(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGetRange<int, uint>(FDwfDigitalOutDividerInfo, "FDwfDigitalOutDividerInfo", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutDividerInitSet")]
        static extern bool FDwfDigitalOutDividerInitSet(HDWF hdwf, int idxChannel, uint v);

        /// <summary>
        /// Sets the initial divider value of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="v">Divider initial value.</param>
        public static void DigitalOutDividerInitSet(HDWF hdwf, int idxChannel, uint v)
        {
            CheckApiProcedureHdwfIndexSet<int, uint>(FDwfDigitalOutDividerInitSet, "FDwfDigitalOutDividerInitSet", hdwf, idxChannel, v);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutDividerInitGet")]
        static extern bool FDwfDigitalOutDividerInitGet(HDWF hdwf, int idxChannel, ref uint pv);

        /// <summary>
        /// Verify the initial divider value of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Configured value of the initial divider.</returns>
        public static uint DigitalOutDividerInitGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, uint>(FDwfDigitalOutDividerInitGet, "FDwfDigitalOutDividerInitGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutDividerSet")]
        static extern bool FDwfDigitalOutDividerSet(HDWF hdwf, int idxChannel, uint v);

        /// <summary>
        /// Sets the divider value of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="v">Divider value.</param>
        public static void DigitalOutDividerSet(HDWF hdwf, int idxChannel, uint v)
        {
            CheckApiProcedureHdwfIndexSet<int, uint>(FDwfDigitalOutDividerSet, "FDwfDigitalOutDividerSet", hdwf, idxChannel, v);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutDividerGet")]
        static extern bool FDwfDigitalOutDividerGet(HDWF hdwf, int idxChannel, ref uint pv);

        /// <summary>
        /// Verify the divider value of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Configured value of the divider.</returns>
        public static uint DigitalOutDividerGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, uint>(FDwfDigitalOutDividerGet, "FDwfDigitalOutDividerGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutCounterInfo")]
        static extern bool FDwfDigitalOutCounterInfo(HDWF hdwf, int idxChannel, ref uint vMin, ref uint vMax);

        /// <summary>
        /// Return the supported counter value range.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Range of supported counter values.</returns>
        public static Range<uint> DigitalOutCounterInfo(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGetRange<int, uint>(FDwfDigitalOutCounterInfo, "FDwfDigitalOutCounterInfo", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutCounterInitSet")]
        static extern bool FDwfDigitalOutCounterInitSet(HDWF hdwf, int idxChannel, bool fHigh, uint v);

        /// <summary>
        /// Sets the initial state and counter value of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="fHigh">Start high.</param>
        /// <param name="v">Divider initial value.</param>
        public static void DigitalOutCounterInitSet(HDWF hdwf, int idxChannel, bool fHigh, uint v)
        {
            CheckApiProcedureHdwfIndexNodeSet<bool, uint>(FDwfDigitalOutCounterInitSet, "FDwfDigitalOutCounterInitSet", hdwf, idxChannel, fHigh, v);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutCounterInitGet")]
        static extern bool FDwfDigitalOutCounterInitGet(HDWF hdwf, int idxChannel, ref int pfHigh, ref uint pv);

        /// <summary>
        /// Verify the initial state and counter value of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Configured value of initial state and counter value.</returns>
        public static CounterInitialState DigitalOutCounterInitGet(HDWF hdwf, int idxChannel)
        {
            var r = new CounterInitialState();
            int is_high = 0;
            var ok = FDwfDigitalOutCounterInitGet(hdwf, idxChannel, ref is_high, ref r.Divider);
            if (ok)
            {
                r.IsHigh = is_high != 0;
                return r;
            }
            else
            {
                throw new ApplicationException(ExceptionMessage("FDwfDigitalOutCounterInitGet"));
            }
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutCounterSet")]
        static extern bool FDwfDigitalOutCounterSet(HDWF hdwf, int idxChannel, uint vLow, uint vHigh);

        /// <summary>
        /// Sets the counter low and high values of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="vLow">Counter low value.</param>
        /// <param name="vHigh">Counter high value.</param>
        public static void DigitalOutCounterSet(HDWF hdwf, int idxChannel, uint vLow, uint vHigh)
        {
            CheckApiProcedureHdwfIndexNodeSet<uint, uint>(FDwfDigitalOutCounterSet, "FDwfDigitalOutCounterSet", hdwf, idxChannel, vLow, vHigh);
        }

        /// <summary>
        /// Sets the counter low and high values of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="values">Low and high values of the counter.</param>
        public static void DigitalOutCounterSet(HDWF hdwf, int idxChannel, Range<uint> values)
        {
            CheckApiProcedureHdwfIndexNodeSet<uint, uint>(FDwfDigitalOutCounterSet, "FDwfDigitalOutCounterSet", hdwf, idxChannel, values.Min, values.Max);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutCounterGet")]
        static extern bool FDwfDigitalOutCounterGet(HDWF hdwf, int idxChannel, ref uint pvLow, ref uint pvHigh);

        /// <summary>
        /// Verify the low and high counter value of the specified channel.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Configured counter high and low values.</returns>
        public static Range<uint> DigitalOutCounterGet(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGetRange<int, uint>(FDwfDigitalOutCounterGet, "FDwfDigitalOutCounterGet", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutDataInfo")]
        static extern bool FDwfDigitalOutDataInfo(HDWF hdwf, int idxChannel, ref uint pcountOfBitsMax);

        /// <summary>
        /// Return the maximum buffers size, the number of custom data bits.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <returns>Maximum number of bits.</returns>
        public static uint DigitalOutDataInfo(HDWF hdwf, int idxChannel)
        {
            return CheckApiFunctionHdwfIndexGet<int, uint>(FDwfDigitalOutDataInfo, "FDwfDigitalOutDataInfo", hdwf, idxChannel);
        }

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfDigitalOutDataSet")]
        static extern unsafe bool FDwfDigitalOutDataSet(HDWF hdwf, int idxChannel, byte* rgBits, uint countOfBits);

        /// <summary>
        /// Set the custom data bits. The function also sets the counter initial, low and high value, according the number of bits.
        /// The data bits are sent out in LSB first order.
        /// For TS output, the count of bits is the total number of output value (I/O) and output enable (OE) bits, which should be an even number.
        /// </summary>
        /// <param name="hdwf">Open interface handle on a device.</param>
        /// <param name="idxChannel">Channel index.</param>
        /// <param name="rgBits">Custom data array.</param>
        /// <param name="countOfBits">Number of bits.</param>
        public static void DigitalOutDataSet(HDWF hdwf, int idxChannel, byte[] rgBits, uint countOfBits)
        {
            if (rgBits.Length * 8 >= countOfBits)
            {
                unsafe
                {
                    fixed (byte* ptr = rgBits)
                    {
                        var ok = FDwfDigitalOutDataSet(hdwf, idxChannel, ptr, countOfBits);
                        if (!ok)
                        {
                            throw new ApplicationException(ExceptionMessage("FDwfDigitalOutDataSet"));
                        }
                    }
                }
            }
            else
            {
                throw new ApplicationException("DigitalOutDataSet: Too few bytes provided for the given number of bits!");
            }
        }
        #endregion  // DIGITAL OUT INSTRUMENT FUNCTIONS - CONFIGURATION

#if (USE_DEPRECATED_FUNCTIONS)
        #region ANALOG OUTPUT - DEPRECATED!
        // bits order is lsb first
        // for TS output the count of bits its the total number of IO|OE bits, it should be an even number
        // BYTE:   0                 |1     ...
        // bit:    0 |1 |2 |3 |...|7 |0 |1 |...
        // sample: IO|OE|IO|OE|...|OE|IO|OE|...


        /*
        // OBSOLETE, do not use them:
        typedef BYTE STS;
        const STS stsRdy        = 0;
        const STS stsArm        = 1;
        const STS stsDone       = 2;
        const STS stsTrig       = 3;
        const STS stsCfg        = 4;
        const STS stsPrefill    = 5;
        const STS stsNotDone    = 6;
        const STS stsTrigDly    = 7;
        const STS stsError      = 8;
        const STS stsBusy       = 9;
        const STS stsStop       = 10;
        */


        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutEnableSet")]
        static extern bool FDwfAnalogOutEnableSet(HDWF hdwf, int idxChannel, bool fEnable);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutEnableGet")]
        static extern bool FDwfAnalogOutEnableGet(HDWF hdwf, int idxChannel, ref bool pfEnable);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutFunctionInfo")]
        static extern bool FDwfAnalogOutFunctionInfo(HDWF hdwf, int idxChannel, ref int pfsfunc); // use IsBitSet

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutFunctionSet")]
        static extern bool FDwfAnalogOutFunctionSet(HDWF hdwf, int idxChannel, FUNC func);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutFunctionGet")]
        static extern bool FDwfAnalogOutFunctionGet(HDWF hdwf, int idxChannel, ref FUNC pfunc);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutFrequencyInfo")]
        static extern bool FDwfAnalogOutFrequencyInfo(HDWF hdwf, int idxChannel, ref double phzMin, ref double phzMax);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutFrequencySet")]
        static extern bool FDwfAnalogOutFrequencySet(HDWF hdwf, int idxChannel, double hzFrequency);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutFrequencyGet")]
        static extern bool FDwfAnalogOutFrequencyGet(HDWF hdwf, int idxChannel, ref double phzFrequency);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutAmplitudeInfo")]
        static extern bool FDwfAnalogOutAmplitudeInfo(HDWF hdwf, int idxChannel, ref double pvoltsMin, ref double pvoltsMax);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutAmplitudeSet")]
        static extern bool FDwfAnalogOutAmplitudeSet(HDWF hdwf, int idxChannel, double voltsAmplitude);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutAmplitudeGet")]
        static extern bool FDwfAnalogOutAmplitudeGet(HDWF hdwf, int idxChannel, ref double pvoltsAmplitude);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutOffsetInfo")]
        static extern bool FDwfAnalogOutOffsetInfo(HDWF hdwf, int idxChannel, ref double pvoltsMin, ref double pvoltsMax);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutOffsetSet")]
        static extern bool FDwfAnalogOutOffsetSet(HDWF hdwf, int idxChannel, double voltsOffset);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutOffsetGet")]
        static extern bool FDwfAnalogOutOffsetGet(HDWF hdwf, int idxChannel, ref double pvoltsOffset);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutSymmetryInfo")]
        static extern bool FDwfAnalogOutSymmetryInfo(HDWF hdwf, int idxChannel, ref double ppercentageMin, ref double ppercentageMax);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutSymmetrySet")]
        static extern bool FDwfAnalogOutSymmetrySet(HDWF hdwf, int idxChannel, double percentageSymmetry);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutSymmetryGet")]
        static extern bool FDwfAnalogOutSymmetryGet(HDWF hdwf, int idxChannel, ref double ppercentageSymmetry);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutPhaseInfo")]
        static extern bool FDwfAnalogOutPhaseInfo(HDWF hdwf, int idxChannel, ref double pdegreeMin, ref double pdegreeMax);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutPhaseSet")]
        static extern bool FDwfAnalogOutPhaseSet(HDWF hdwf, int idxChannel, double degreePhase);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutPhaseGet")]
        static extern bool FDwfAnalogOutPhaseGet(HDWF hdwf, int idxChannel, ref double pdegreePhase);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutDataInfo")]
        static extern bool FDwfAnalogOutDataInfo(HDWF hdwf, int idxChannel, ref int pnSamplesMin, ref int pnSamplesMax);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutDataSet")]
        static extern bool FDwfAnalogOutDataSet(HDWF hdwf, int idxChannel, ref double rgdData, int cdData);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutPlayStatus")]
        static extern bool FDwfAnalogOutPlayStatus(HDWF hdwf, int idxChannel, ref int cdDataFree, ref int cdDataLost, ref int cdDataCorrupted);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfAnalogOutPlayData")]
        static extern bool FDwfAnalogOutPlayData(HDWF hdwf, int idxChannel, ref double rgdData, int cdData);


        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumAnalogInChannels")]
        static extern bool FDwfEnumAnalogInChannels(int idxDevice, ref int pnChannels);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumAnalogInBufferSize")]
        static extern bool FDwfEnumAnalogInBufferSize(int idxDevice, ref int pnBufferSize);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumAnalogInBits")]
        static extern bool FDwfEnumAnalogInBits(int idxDevice, ref int pnBits);

        // ========================================================================================
        [DllImport("dwf.dll", EntryPoint = "FDwfEnumAnalogInFrequency")]
        static extern bool FDwfEnumAnalogInFrequency(int idxDevice, ref double phzFrequency);
        #endregion // ANALOG OUTPUT - DEPRECATED
#endif // USE_DEPRECATED_FUNCTIONS
    }
}
