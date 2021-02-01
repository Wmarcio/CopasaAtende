using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCCSend - consiste matrícula centralizadora
    /// </summary>
    public class SCN6ISCCSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// CNPJ.
        /// </summary>
        [XmlElement("_CNPJ")]
        public string CNPJ { get; set; }
    }
}
