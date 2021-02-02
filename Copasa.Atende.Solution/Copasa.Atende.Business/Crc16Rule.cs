using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Business
{
    internal enum Crc16Mode : ushort { Standard = 0x1021, CcittKermit = 0x8408 }

    /// <summary>
    /// Rule - Crc16Rule.
    /// </summary>
    public class Crc16Rule
    {
        const ushort polynomial = 0xA001;
        static readonly ushort[] table = new ushort[256];

        /// <summary>
        /// Rule - Crc16Rule.
        /// </summary>
        public ushort ComputeChecksum(byte[] bytes)
        {
            ushort crc = 0;
            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ bytes[i]);
                crc = (ushort)((crc >> 8) ^ table[index]);
            }
            return crc;
        }

        /// <summary>
        /// Rule - Crc16Rule.
        /// </summary>
        public Crc16Rule()
        {
            ushort value;
            ushort temp;
            for (ushort i = 0; i < table.Length; ++i)
            {
                value = 0;
                temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                table[i] = value;
            }
        }
    }

    
}
