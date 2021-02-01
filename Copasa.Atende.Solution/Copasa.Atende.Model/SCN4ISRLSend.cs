using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISRLSend - Religação
    /// </summary>
    public class SCN4ISRLSend : BaseModelSend
    {
        /// <summary>
        /// NumMatricula.
        /// </summary>
        [XmlElement("_RCV-NUM-MATRICULA")]
        public string numeroMatricula { get; set; }
    }
}
