using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1_NomesSend Busca dados do cliente
    /// </summary>
    public class SCN4ISU1_NomesSend : BaseModelSend
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        [XmlElement("_CRM-CPF-CNPJ-I")]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// FlagTipoUsu.
        /// </summary>
        [XmlElement("_CRM-FLAG-CPF-CNPJ-I")]
        [JsonIgnore]
        public string flagTipoUsu { get; set; }

        /// <summary>
        /// Identificadores.
        /// </summary>
        [XmlArray("Array")]
        [XmlArrayItem("_CRM-IDENT-USU-I")]
        public string[] identificadores { get; set; }
    }
}
