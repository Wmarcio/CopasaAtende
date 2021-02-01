using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISNFSend - Nota fiscal fatura
    /// </summary>
    public class SCN6ISNFSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_NUM-FATURA")]
        public string numeroFatura { get; set; }

    }
}
