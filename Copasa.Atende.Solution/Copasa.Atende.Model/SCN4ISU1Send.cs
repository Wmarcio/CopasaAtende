using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1Send Busca dados do cliente
    /// </summary>
    public class SCN4ISU1Send : BaseModelSend
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        [XmlElement("_CRM-CPF-CNPJ-I")]
        [IS("N", 14)]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// FlagTipoUsu.
        /// </summary>
        [XmlElement("_CRM-FLAG-CPF-CNPJ-I")]
        [JsonIgnore]
        public string flagTipoUsu { get; set; }

        /// <summary>
        /// CodigoServicoSolicitado.
        /// </summary>
        [XmlElement("_RCV-COD-SERVICO-SOLICITADO")]
        public string codigoServicoSolicitado { get; set; }

        /// <summary>
        /// Identificadores.
        /// </summary>
        [XmlArray("Array")]
        [XmlArrayItem("_CRM-IDENT-USU-I")]
        [IS("N", 11)]
        public string[] identificadores { get; set; }
    }
}
