using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCCReceive - consiste matrícula centralizadora
    /// </summary>
    public class SCN6ISCCReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_COD-MSG")]
        public string codigoRetornoSicom { get; set; }
    }
}
