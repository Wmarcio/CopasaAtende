using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN5IS02Receive Informar leitura - Envia leitura
    /// </summary>
    public class SCN5IS02Receive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_COD-MSG")]
        public string codigoRetornoSicom { get; set; }
    }
}
