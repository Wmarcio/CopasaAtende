using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISVRSend Vazamento na rua
    /// </summary>
    public class SCN4ISVRSend : BaseModel
    {
        /// <summary>
        /// NumProtocolo.
        /// </summary>
        [XmlElement("_RCV-NUM-PROTOCOLO")]
        public string numProtocolo { get; set; }

        /// <summary>
        /// CodServico.
        /// </summary>
        [XmlElement("_COD-SERVICO")]
        public string codServico { get; set; }

        /// <summary>
        /// Solicitante.
        /// </summary>
        [XmlElement("_SOLICITANTE")]
        public string solicitante { get; set; }

        /// <summary>
        /// EntreRuas.
        /// </summary>
        [XmlElement("_ENTRE-RUAS")]
        public string entreRuas { get; set; }

        /// <summary>
        /// Telefone.
        /// </summary>
        [XmlElement("_TELEFONE")]
        public string telefone { get; set; }

        /// <summary>
        /// CodLocalidade.
        /// </summary>
        [XmlElement("_COD-LOCALIDADE")]
        public string codLocalidade { get; set; }

        /// <summary>
        /// TipoLogradouro.
        /// </summary>
        [XmlElement("_TIPO-LOGRADOURO")]
        public string tipoLogradouro { get; set; }

        /// <summary>
        /// DescLogradouro.
        /// </summary>
        [XmlElement("_DESC-LOGRADOURO")]
        public string descLogradouro { get; set; }

        /// <summary>
        /// NumImovel.
        /// </summary>
        [XmlElement("_NUM-IMOVEL")]
        public string numImovel { get; set; }

        /// <summary>
        /// TipoComplemento.
        /// </summary>
        [XmlElement("_TIPO-COMPLEMENTO")]
        public string tipoComplemento { get; set; }

        /// <summary>
        /// Complemento.
        /// </summary>
        [XmlElement("_COMPLEMENTO")]
        public string complemento { get; set; }

        /// <summary>
        /// DescBairro.
        /// </summary>
        [XmlElement("_DESC-BAIRRO")]
        public string descBairro { get; set; }

        /// <summary>
        /// CodBairro.
        /// </summary>
        [XmlElement("_COD-BAIRRO")]
        public string codBairro { get; set; }

        /// <summary>
        /// CodLogradouro.
        /// </summary>
        [XmlElement("_COD-LOGRADOURO")]
        public string codLogradouro { get; set; }

        /// <summary>
        /// FlagOrigem.
        /// </summary>
        [XmlElement("_FLAG-ORIGEM")]
        public string flagOrigem { get; set; }

        /// <summary>
        /// FlagImagem.
        /// </summary>
        [XmlElement("_FLAG-IMAGEM")]
        public string flagImagem { get; set; }

    }
}
