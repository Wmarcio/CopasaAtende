using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISPSReceivePSEsgoto - Lista Pontos serviço - Ponto serviço esgoto
    /// </summary>
    public class SCN3ISPSReceivePSEsgoto : BaseModel
    {
        /// <summary>
        /// Tipo.
        /// </summary>
        [XmlElement("_TIPO-PS-ESGOTO")]
        public string tipo { get; set; }

        /// <summary>
        /// Numero.
        /// </summary>
        [XmlElement("_NUM-PS-ESGOTO")]
        public string numero { get; set; }

        /// <summary>
        /// DescricaoSituacao.
        /// </summary>
        [XmlElement("_DESC-SITUACAO-PS-ESGOTO")]
        public string descricaoSituacao { get; set; }

        /// <summary>
        /// DescricaoMotivo.
        /// </summary>
        [XmlElement("_DESC-MOTIVO-PS-ESGOTO")]
        public string descricaoMotivo { get; set; }

        /// <summary>
        /// DataInicioSituacao.
        /// </summary>
        [XmlElement("_DATA-SITUACAO-PS-ESGOTO")]
        public string dataInicioSituacao { get; set; }

        /// <summary>
        /// TipoDocumentoSituacao.
        /// </summary>
        [XmlElement("_TIPO-DOC-SITUACAO-PS-ESGOTO")]
        public string tipoDocumentoSituacao { get; set; }

        /// <summary>
        /// NumeroDocumentoSituacao.
        /// </summary>
        [XmlElement("_NUM-DOC-SITUACAO-PS-ESGOTO")]
        public string numeroDocumentoSituacao { get; set; }

        /// <summary>
        /// DescricaoProdutoEsgotado.
        /// </summary>
        [XmlElement("_DESC-PRODUTO-ESGOTADO")]
        public string descricaoProdutoEsgotado { get; set; }

    }
}
