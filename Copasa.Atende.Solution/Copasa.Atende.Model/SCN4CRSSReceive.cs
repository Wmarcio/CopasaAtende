using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRSSReceive - cria solicitação de serviço
    /// </summary>
    public class SCN4CRSSReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// TipoInformacaoSS.
        /// </summary>
        [XmlElement("_SND-TIPO-INF-SS")]
        [JsonIgnore]
        public string tipoInformacaoSS { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_SND-MATRICULA")]
        [JsonIgnore]
        public string matricula { get; set; }

        /// <summary>
        /// NumeroProtocoloSS.
        /// </summary>
        [XmlElement("_SND-NUM-PROTOCOLO-SS")]
        [JsonIgnore]
        public string numeroProtocoloSS { get; set; }

        /// <summary>
        /// NumeroSolicitacaoServico.
        /// </summary>
        [XmlElement("_SND-NUM-SS")]
        public string numeroSolicitacaoServico { get; set; }

        /// <summary>
        /// UnidadeDestino.
        /// </summary>
        [XmlElement("_SND-UNID-DEST-SS")]
        public string unidadeDestino { get; set; }

        /// <summary>
        /// CodigoServicoSS.
        /// </summary>
        [XmlElement("_SND-COD-SERVICO-SS")]
        public string codigoServicoSS { get; set; }

        /// <summary>
        /// DescricaoServicoSS.
        /// </summary>
        [XmlElement("_SND-NOME-LOGRADOURO-SS")]
        public string nomeLogradouroSS { get; set; }

        /// <summary>
        /// DescricaoServicoSS.
        /// </summary>
        [XmlElement("_SND-NOME-BAIRRO-SS")]
        public string nomeBairroSS { get; set; }

        /// <summary>
        /// DescricaoServicoSS.
        /// </summary>
        [XmlElement("_SND-NUM-IMOVEL-SS")]
        public string numeroImovelSS { get; set; }

        /// <summary>
        /// DescricaoServicoSS.
        /// </summary>
        [XmlElement("_SND-DESC-SERVICO-SS")]
        public string descricaoServicoSS { get; set; }

        /// <summary>
        /// DataGeracaoSS.
        /// </summary>
        [XmlElement("_SND-DATA-GERACAO-SS")]
        public string dataGeracaoSS { get; set; }

        /// <summary>
        /// HoraGeracaoSS.
        /// </summary>
        [XmlElement("_SND-HORA-GERACAO-SS")]
        public string horaGeracaoSS { get; set; }

        /// <summary>
        /// DataPrevisaoSS.
        /// </summary>
        [XmlElement("_SND-DATA-PREVISAO-SS")]
        public string dataPrevisaoSS { get; set; }

        /// <summary>
        /// HoraPrevicsoSS.
        /// </summary>
        [XmlElement("_SND-HORA-PREVISAO-SS")]
        public string horaPrevisaoSS { get; set; }

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
        /// ObservacaoSSSicom.
        /// </summary>
        [XmlElement("_SND-OBS-SS")]
        [JsonIgnore]
        public string[] observacaoSSSicom { get; set; }

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
        /// NumeroSolicitacaoServicoOS.
        /// </summary>
        [XmlElement("_SND-NUM-SS-OS")]
        public string numeroSolicitacaoServicoOS { get; set; }

        /// <summary>
        /// NumeroOrdemServico.
        /// </summary>
        [XmlElement("_SND-NUM-OS")]
        public string numeroOrdemServico { get; set; }

        /// <summary>
        /// UnidadeDestinoOS.
        /// </summary>
        [XmlElement("_SND-UNID-DEST-OS")]
        public string unidadeDestinoOS { get; set; }

        /// <summary>
        /// CodigoServicoOS.
        /// </summary>
        [XmlElement("_SND-COD-SERVICO-OS")]
        public string codigoServicoOS { get; set; }

        /// <summary>
        /// DescricaoServicoOS.
        /// </summary>
        [XmlElement("_SND-DESC-SERVICO-OS")]
        public string descricaoServicoOS { get; set; }

        /// <summary>
        /// DataGeracaoOS.
        /// </summary>
        [XmlElement("_SND-DATA-GERACAO-OS")]
        public string dataGeracaoOS { get; set; }

        /// <summary>
        /// HoraGeracaoOS.
        /// </summary>
        [XmlElement("_SND-HORA-GERACAO-OS")]
        public string horaGeracaoOS { get; set; }

        /// <summary>
        /// DataPrevisaoOS.
        /// </summary>
        [XmlElement("_SND-DATA-PREVISAO-OS")]
        public string dataPrevisaoOS { get; set; }

        /// <summary>
        /// HoraPrevisaoOS.
        /// </summary>
        [XmlElement("_SND-HORA-PREVISAO-OS")]
        public string horaPrevisaoOS { get; set; }

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
        /// TipoInformacaoLote.
        /// </summary>
        [XmlElement("_SND-TIPO-INF-LOTE")]
        public string tipoInformacaoLote { get; set; }

        /// <summary>
        /// DataEnvioLote.
        /// </summary>
        [XmlElement("_SND-DATA-ENVIO-LOTE")]
        public string dataEnvioLote { get; set; }

        /// <summary>
        /// HoraEnvioLote.
        /// </summary>
        [XmlElement("_SND-HORA-ENVIO-LOTE")]
        public string horaEnvioLote { get; set; }

        /// <summary>
        /// NumeroLote.
        /// </summary>
        [XmlElement("_SND-NUM-LOTE")]
        public string numeroLote { get; set; }

        /// <summary>
        /// QuantidadeOSEnviada.
        /// </summary>
        [XmlElement("_SND-QTDE-OS-ENV")]
        public string quantidadeOSEnviada { get; set; }

        /// <summary>
        /// EmailUnidadeResponsavel.
        /// </summary>
        public string emailUnidadeResponsavel { get; set; }
    }
}
