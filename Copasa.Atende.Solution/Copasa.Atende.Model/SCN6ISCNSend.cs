using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCNSend - Certidão negativa de débito
    /// </summary>
    public class SCN6ISCNSend : BaseModel
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        [XmlElement("_NUM-CPF-CNPJ")]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// Empresa.
        /// </summary>
        public string empresa { get; set; }
    }
}
