using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN3ISPSReceivePSAgua - Lista Pontos serviço - Ponto serviço água
    /// </summary>
    public class SCN3ISPSReceivePSAgua : BaseModel
    {
        /// <summary>
        /// Tipo.
        /// </summary>
        [XmlElement("_TIPO-PS-AGUA")]
        public string tipo { get; set; }

        /// <summary>
        /// Numero.
        /// </summary>
        [XmlElement("_NUM-PS-AGUA")]
        public string numero { get; set; }

        /// <summary>
        /// DescricaoSituacao.
        /// </summary>
        [XmlElement("_DESC-SITUACAO-PS-AGUA")]
        public string descricaoSituacao { get; set; }

        /// <summary>
        /// DescricaoMotivo.
        /// </summary>
        [XmlElement("_DESC-MOTIVO-PS-AGUA")]
        public string descricaoMotivo { get; set; }

        /// <summary>
        /// DataInicioSituacao.
        /// </summary>
        [XmlElement("_DATA-SITUACAO-PS-AGUA")]
        public string dataInicioSituacao { get; set; }

        /// <summary>
        /// TipoDocumentoSituacao.
        /// </summary>
        [XmlElement("_TIPO-DOC-SITUACAO-PS-AGUA")]
        public string tipoDocumentoSituacao { get; set; }

        /// <summary>
        /// NumeroDocumentoSituacao.
        /// </summary>
        [XmlElement("_NUM-DOC-SITUACAO-PS-AGUA")]
        public string numeroDocumentoSituacao { get; set; }

        /// <summary>
        /// DescricaoTipoPadrao.
        /// </summary>
        [XmlElement("_DESC-TIPO-PADRAO-PS-AGUA")]
        public string descricaoTipoPadrao { get; set; }

        /// <summary>
        /// DescricaoLocalizacao.
        /// </summary>
        [XmlElement("_DESC-LOCALIZACAO-PS-AGUA")]
        public string descricaoLocalizacao { get; set; }

    }
}
