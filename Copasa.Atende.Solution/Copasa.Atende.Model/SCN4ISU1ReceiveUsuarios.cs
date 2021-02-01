using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISU1ReceiveUsuarios Busca dados do cliente
    /// </summary>
    public class SCN4ISU1ReceiveUsuarios : BaseModel
    {
        /// <summary>
        /// CpfCnpj.
        /// </summary>
        [XmlElement("_CRM-CPF-CNPJ-USUARIO-O")]
        public string cpfCnpj { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_CRM-IDENT-USUARIO-O")]
        public string identificador { get; set; }

        /// <summary>
        /// NomeUsuario.
        /// </summary>
        [XmlElement("_CRM-NOME-USUARIO-O")]
        public string nomeUsuario { get; set; }

        /// <summary>
        /// EmailUsuario.
        /// </summary>
        [XmlElement("_CRM-EMAIL-USUARIO-O")]
        public string emailUsuario { get; set; }

        /// <summary>
        /// DDDResidenciaUsuario.
        /// </summary>
        [XmlElement("_CRM-DDD-RES-USU-O")]
        [JsonIgnore]
        public string DDDResidencia { get; set; }

        /// <summary>
        /// TelefoneResidenciaUsuario.
        /// </summary>
        [XmlElement("_CRM-FONE-RES-USU-O")]
        public string telefoneResidencia { get; set; }

        /// <summary>
        /// DDDComercialUsuario.
        /// </summary>
        [XmlElement("_CRM-DDD-COM-USU-O")]
        [JsonIgnore]
        public string DDDComercial { get; set; }

        /// <summary>
        /// TelefoneComercialUsuario.
        /// </summary>
        [XmlElement("_CRM-FONE-COM-USU-O")]
        public string telefoneComercial { get; set; }

        /// <summary>
        /// DDDCelularUsuario.
        /// </summary>
        [XmlElement("_CRM-DDD-CEL-USU-O")]
        [JsonIgnore]
        public string DDDCelular { get; set; }

        /// <summary>
        /// TelefoneCelularUsuario.
        /// </summary>
        [XmlElement("_CRM-FONE-CEL-USU-O")]
        public string telefoneCelular { get; set; }

        /// <summary>
        /// TipoLogradouroUsuario.
        /// </summary>
        [XmlElement("_CRM-TIPO-LOGR-O")]
        public string tipoLogradouroUsuario { get; set; }

        /// <summary>
        /// NomeLogradouro.
        /// </summary>
        [XmlElement("_CRM-NOME-LOGR-O")]
        public string nomeLogradouro { get; set; }

        /// <summary>
        /// NumeroLogradouroUsuario.
        /// </summary>
        [XmlElement("_CRM-NUM-IMOVEL-O")]
        public string numeroLogradouroUsuario { get; set; }

        /// <summary>
        /// TipoComplementoLogradouroUsuario.
        /// </summary>
        [XmlElement("_CRM-COMPL-TIPO-O")]
        public string tipoComplementoLogradouroUsuario { get; set; }

        /// <summary>
        /// ComplementoLogradouroUsuario.
        /// </summary>
        [XmlElement("_CRM-COMPL-INF-O")]
        public string complementoLogradouroUsuario { get; set; }

        /// <summary>
        /// Bairro.
        /// </summary>
        [XmlElement("_CRM-BAIRRO-O")]
        public string bairro { get; set; }

        /// <summary>
        /// Localidade.
        /// </summary>
        [XmlElement("_CRM-LOCALIDADE-O")]
        public string localidade { get; set; }

        /// <summary>
        /// RecebeContaPorEmail.
        /// </summary>
        [XmlElement("_FLAG-CONTA-EMAIL-USU-O")]
        [JsonIgnore]
        public string recebeContaPorEmail { get; set; }

        /// <summary>
        /// Matriculas
        /// </summary>
        [XmlElement("_TAB-MATRICULAS")]
        [JsonIgnore]
        public SCN4ISU1ReceiveMatriculasUsuarios[] matriculas { get; set; }

    }
}
