using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISPSSend - Lista Pontos serviço
    /// </summary>
    public class SCN3ISPSSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA-I")]
        public string matricula { get; set; }
    }
}
