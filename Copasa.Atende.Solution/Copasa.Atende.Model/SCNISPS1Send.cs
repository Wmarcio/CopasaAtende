using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCNISPS1Send - Lista hidrômetros de uma matrícula
    /// </summary>
    public class SCNISPS1Send : BaseModelSend
    {
        /// <summary>
        /// Matrícula
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }
    }
}
