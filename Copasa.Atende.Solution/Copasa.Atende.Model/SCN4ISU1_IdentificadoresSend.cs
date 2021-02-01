using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1_IdentificadoresSend Busca dados do cliente sem array de matrículas
    /// </summary>
    public class SCN4ISU1_IdentificadoresSend : BaseModelSend
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        [XmlElement("_CRM-CPF-CNPJ-I")]
        [JsonIgnore]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// Identificadores.
        /// </summary>
        [XmlArray("Array")]
        [XmlArrayItem("_CRM-IDENT-USU-I")]
        public string[] identificadores { get; set; }
    }
}
