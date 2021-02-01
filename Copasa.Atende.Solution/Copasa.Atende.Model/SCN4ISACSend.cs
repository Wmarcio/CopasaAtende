using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISACSend - altera email e telefone
    /// </summary>
    public class SCN4ISACSend : BaseModelSend
    {

        /// <summary>
        /// Origem.
        /// </summary>
        [XmlElement("_RCV-FLAG-ORIGEM")]
        public string origem { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        [XmlElement("_RCV-IDENTIFICADOR-USUARIO")]
        public string identificador { get; set; }

        /// <summary>
        /// PrimeiroAcesso.
        /// </summary>
        [XmlElement("_RCV-FLAG-PRIMEIRO-ACESSO")]
        public string primeiroAcesso { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        [XmlElement("_RCV-INF-EMAIL")]
        public string email { get; set; }

        /// <summary>
        /// DDDCelular.
        /// </summary>
        [XmlElement("_RCV-DDD-TELEFONE-CEL")]
        public string DDDCelular { get; set; }

        /// <summary>
        /// TelefoneCelular.
        /// </summary>
        [XmlElement("_RCV-NUM-TELEFONE-CEL")]
        public string telefoneCelular { get; set; }

        /// <summary>
        /// DDDResidencial.
        /// </summary>
        [XmlElement("_RCV-DDD-TELEFONE-RES")]
        public string DDDResidencial { get; set; }

        /// <summary>
        /// TelefoneResidencial.
        /// </summary>
        [XmlElement("_RCV-NUM-TELEFONE-RES")]
        public string telefoneResidencial { get; set; }

        /// <summary>
        /// DDDComercial.
        /// </summary>
        [XmlElement("_RCV-DDD-TELEFONE-COM")]
        public string DDDComercial { get; set; }

        /// <summary>
        /// TelefoneComercial.
        /// </summary>
        [XmlElement("_RCV-NUM-TELEFONE-COM")]
        public string telefoneComercial { get; set; }
    }
}
