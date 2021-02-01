using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCBSend - Código de barras de fatura
    /// </summary>
    public class SCN6ISCBSend : BaseModelSend
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_NUM-FATURA")]
        public string numeroFatura { get; set; }
    }
}
