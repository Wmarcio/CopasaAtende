using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN7ISOPSend - Onde pagar a conta
    /// </summary>
    public class SCN7ISOPSend : BaseModelSend
    {
        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_COD-LOCALIDADE")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// NomeLocalidade.
        /// </summary>
        [XmlElement("_NOME-LOCALIDADE")]
        public string nomeLocalidade { get; set; }
    }
}
