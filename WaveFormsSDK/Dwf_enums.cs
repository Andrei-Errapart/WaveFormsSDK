using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace WaveFormsSDK
{
	public static partial class Dwf {
		// ========================================================================================
        static List<TRIGSRC> TrigsrcsOfBitfield(int bf)
        {
            var r = new List<TRIGSRC>();
            var trigger_values = (TRIGSRC[])Enum.GetValues(typeof(TRIGSRC));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<STATE> StatesOfBitfield(int bf)
        {
            var r = new List<STATE>();
            var trigger_values = (STATE[])Enum.GetValues(typeof(STATE));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<ACQMODE> AcqmodesOfBitfield(int bf)
        {
            var r = new List<ACQMODE>();
            var trigger_values = (ACQMODE[])Enum.GetValues(typeof(ACQMODE));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<FILTER> FiltersOfBitfield(int bf)
        {
            var r = new List<FILTER>();
            var trigger_values = (FILTER[])Enum.GetValues(typeof(FILTER));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<TRIGTYPE> TrigtypesOfBitfield(int bf)
        {
            var r = new List<TRIGTYPE>();
            var trigger_values = (TRIGTYPE[])Enum.GetValues(typeof(TRIGTYPE));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<TRIGCOND> TrigcondsOfBitfield(int bf)
        {
            var r = new List<TRIGCOND>();
            var trigger_values = (TRIGCOND[])Enum.GetValues(typeof(TRIGCOND));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<TRIGLEN> TriglensOfBitfield(int bf)
        {
            var r = new List<TRIGLEN>();
            var trigger_values = (TRIGLEN[])Enum.GetValues(typeof(TRIGLEN));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<FUNC> FuncsOfBitfield(int bf)
        {
            var r = new List<FUNC>();
            var trigger_values = (FUNC[])Enum.GetValues(typeof(FUNC));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<ANALOGOUTNODE> AnalogoutnodesOfBitfield(int bf)
        {
            var r = new List<ANALOGOUTNODE>();
            var trigger_values = (ANALOGOUTNODE[])Enum.GetValues(typeof(ANALOGOUTNODE));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<ANALOGIO> AnalogiosOfBitfield(int bf)
        {
            var r = new List<ANALOGIO>();
            var trigger_values = (ANALOGIO[])Enum.GetValues(typeof(ANALOGIO));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<DIGITALINCLOCKSOURCE> DigitalinclocksourcesOfBitfield(int bf)
        {
            var r = new List<DIGITALINCLOCKSOURCE>();
            var trigger_values = (DIGITALINCLOCKSOURCE[])Enum.GetValues(typeof(DIGITALINCLOCKSOURCE));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<DIGITALINSAMPLEMODE> DigitalinsamplemodesOfBitfield(int bf)
        {
            var r = new List<DIGITALINSAMPLEMODE>();
            var trigger_values = (DIGITALINSAMPLEMODE[])Enum.GetValues(typeof(DIGITALINSAMPLEMODE));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<DIGITALOUTOUTPUT> DigitaloutoutputsOfBitfield(int bf)
        {
            var r = new List<DIGITALOUTOUTPUT>();
            var trigger_values = (DIGITALOUTOUTPUT[])Enum.GetValues(typeof(DIGITALOUTOUTPUT));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<DIGITALOUTTYPE> DigitalouttypesOfBitfield(int bf)
        {
            var r = new List<DIGITALOUTTYPE>();
            var trigger_values = (DIGITALOUTTYPE[])Enum.GetValues(typeof(DIGITALOUTTYPE));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		// ========================================================================================
        static List<DIGITALOUTIDLE> DigitaloutidlesOfBitfield(int bf)
        {
            var r = new List<DIGITALOUTIDLE>();
            var trigger_values = (DIGITALOUTIDLE[])Enum.GetValues(typeof(DIGITALOUTIDLE));
            foreach (var tv in trigger_values)
            {
                if ((bf & (1 << (int)tv)) != 0)
                {
                    r.Add(tv);
                }
            }
            return r;
        }

		}
}
