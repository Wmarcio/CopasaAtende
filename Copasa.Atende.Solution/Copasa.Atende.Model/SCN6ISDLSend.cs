using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISDLSend - Calendário faturamento
    /// </summary>
    public class SCN6ISDLSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_NUM-MATRICULA-REC")]
        public string matricula { get; set; }

}
}
