using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISSVSend - Segunda via fatura
    /// </summary>
    public class SCN6ISSVSend : BaseModel
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_NUMFATUR")]
        public string numeroFatura { get; set; }
    }
}
