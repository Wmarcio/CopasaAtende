using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCESendMatricula Atualiza status para envio conta por email
    /// </summary>
    public class SCN6ISCESendMatricula : BaseModel
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_NUM-MATRICULA-I")]
        public string matricula { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        [XmlElement("_FLAG-CONTA-EMAIL-I")]
        public string status { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [XmlElement("_EMAIL-I")]
        public string email { get; set; }
    }
}
