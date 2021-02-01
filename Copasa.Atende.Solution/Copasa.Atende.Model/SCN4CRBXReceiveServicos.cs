using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRBXReceiveServicos - Envio de baixas de ordens de serviços
    /// </summary>
    public class SCN4CRBXReceiveServicos : BaseModel 
    {
        /// <summary>
        /// TipoInformacaoSS.
        /// </summary>
        [XmlElement("_SND-TIPO-INF-SS")]
        public string tipoInformacaoSS { get; set; }

        /// <summary>
        /// NumeroProtocoloSS.
        /// </summary>
        [XmlElement("_SND-NUM-PROTOCOLO-SS")]
        public string numeroProtocoloSS { get; set; }

        /// <summary>
        /// NumeroSolicitacaoServico.
        /// </summary>
        [XmlElement("_SND-NUM-SS")]
        public string numeroSolicitacaoServico { get; set; }

        /// <summary>
        /// DataBaixaSS.
        /// </summary>
        [XmlElement("_SND-DATA-BAIXA-SS")]
        public string dataBaixaSS { get; set; }

        /// <summary>
        /// HoraBaixaSS.
        /// </summary>
        [XmlElement("_SND-HORA-BAIXA-SS")]
        public string horaBaixaSS { get; set; }

        /// <summary>
        /// SituacaoSS.
        /// </summary>
        [XmlElement("_SND-SITUACAO-SS")]
        public string situacaoSS { get; set; }

        /// <summary>
        /// DescricaoSituacaoSS.
        /// </summary>
        [XmlElement("_SND-DESC-SITUACAO-SS")]
        public string descricaoSituacaoSS { get; set; }

        /// <summary>
        /// ObservacaoSS.
        /// </summary>
        [XmlElement("_SND-OBS-SS")]
        public string observacaoSS { get; set; }

        /// <summary>
        /// TipoInformacaoOS.
        /// </summary>
        [XmlElement("_SND-TIPO-INF-OS")]
        public string tipoInformacaoOS { get; set; }

        /// <summary>
        /// NumeroProtocoloOS.
        /// </summary>
        [XmlElement("_SND-NUM-PROTOCOLO-OS")]
        public string numeroProtocoloOS { get; set; }

        /// <summary>
        /// NumeroSolitacaoServicoOS.
        /// </summary>
        [XmlElement("_SND-NUM-SS-OS")]
        public string numeroSolitacaoServicoOS { get; set; }

        /// <summary>
        /// NumeroOrdemServico.
        /// </summary>
        [XmlElement("_SND-NUM-OS")]
        public string numeroOrdemServico { get; set; }

        /// <summary>
        /// DataBaixaOS.
        /// </summary>
        [XmlElement("_SND-DATA-BAIXA-OS")]
        public string dataBaixaOS { get; set; }

        /// <summary>
        /// HoraBaixaOS.
        /// </summary>
        [XmlElement("_SND-HORA-BAIXA-OS")]
        public string horaBaixaOS { get; set; }

        /// <summary>
        /// SituacaoOS.
        /// </summary>
        [XmlElement("_SND-SITUACAO-OS")]
        public string situacaoOS { get; set; }

        /// <summary>
        /// DescricaoSituacaoOS.
        /// </summary>
        [XmlElement("_SND-DESC-SITUACAO-OS")]
        public string descricaoSituacaoOS { get; set; }

        /// <summary>
        /// CodigoOcorrenciaBaixaOS.
        /// </summary>
        [XmlElement("_SND-COD-OCORR-BAIXA-OS")]
        public string codigoOcorrenciaBaixaOS { get; set; }

        /// <summary>
        /// DescricaoOcorrenciaBaixaOS.
        /// </summary>
        [XmlElement("_SND-DESC-OCORR-BAIXA-OS")]
        public string descricaoOcorrenciaBaixaOS { get; set; }

        /// <summary>
        /// ObservacaoBaixa.
        /// </summary>
        [XmlElement("_SND-OBS-BAIXA")]
        public string observacaoBaixa { get; set; }
    }
}
