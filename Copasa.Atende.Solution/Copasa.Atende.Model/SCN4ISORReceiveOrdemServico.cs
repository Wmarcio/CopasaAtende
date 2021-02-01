using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISORReceiveOrdemServico - Busca OS's de uma solicitação de serviço
    /// </summary>
    public class SCN4ISORReceiveOrdemServico : BaseModel
    {
        /// <summary>
        /// NumeroOrdemServico.
        /// </summary>
        [XmlElement("_SND-NRO-ORDEM-SERV")]
        public string numeroOrdemServico { get; set; }

        /// <summary>
        /// CodigoServico.
        /// </summary>
        [XmlElement("_SND-COD-SERV-OS")]
        public string codigoServico { get; set; }

        /// <summary>
        /// DescricaoServico.
        /// </summary>
        [XmlElement("_SND-DESC-SERVICO")]
        public string descricaoServico { get; set; }

        /// <summary>
        /// DataGeracao.
        /// </summary>
        [XmlElement("_SND-DT-GERACAO-OS")]
        public string dataGeracao { get; set; }

        /// <summary>
        /// HoraGeracao.
        /// </summary>
        [XmlElement("_SND-HR-GERACAO-OS")]
        public string horaGeracao { get; set; }

        /// <summary>
        /// DataPrevisaoAtendimento.
        /// </summary>
        [XmlElement("_SND-DT-PREVISAO-OS")]
        public string dataPrevisaoAtendimento { get; set; }

        /// <summary>
        /// HoraPrevisaoAtendimento.
        /// </summary>
        [XmlElement("_SND-HR-PREVISAO-OS")]
        public string horaPrevisaoAtendimento { get; set; }

        /// <summary>
        /// DataExecucao.
        /// </summary>
        [XmlElement("_SND-DT-EXECUCAO-OS")]
        public string dataExecucao { get; set; }

        /// <summary>
        /// HoraExecucao.
        /// </summary>
        [XmlElement("_SND-HR-EXECUCAO-OS")]
        public string horaExecucao { get; set; }

        /// <summary>
        /// DataBaixa.
        /// </summary>
        [XmlElement("_SND-DT-BAIXA-OS")]
        [JsonIgnore]
        public string dataBaixa { get; set; }

        /// <summary>
        /// HoraBaixa.
        /// </summary>
        [XmlElement("_SND-HR-BAIXA-OS")]
        [JsonIgnore]
        public string horaBaixa { get; set; }

        /// <summary>
        /// CodigoSituacao.
        /// </summary>
        [XmlElement("_SND-SITUACAO-OS")]
        [JsonIgnore]
        public string codigoSituacao { get; set; }

        /// <summary>
        /// DescricaoSituacao.
        /// </summary>
        [XmlElement("_SND-DESC-SITUACAO-OS")]
        public string descricaoSituacao { get; set; }

        /// <summary>
        /// CodigoOcorrenciaBaixa.
        /// </summary>
        [XmlElement("_SND-COD-OCORRENCIA-BX")]
        [JsonIgnore]
        public string codigoOcorrenciaBaixa { get; set; }

        /// <summary>
        /// DescricaoOcorrenciaBaixa.
        /// </summary>
        [XmlElement("_SND-DESC-OCORRENCIA-BX")]
        public string descricaoOcorrenciaBaixa { get; set; }

        /// <summary>
        /// CodigoUltimoEvento.
        /// </summary>
        [XmlElement("_SND-COD-ULT-EVENTO-OS")]
        [JsonIgnore]
        public string codigoUltimoEvento { get; set; }

        /// <summary>
        /// DescricaoUltimoEvento.
        /// </summary>
        [XmlElement("_SND-DESC-ULT-EVENTO-OS")]
        public string descricaoUltimoEvento { get; set; }

        /// <summary>
        /// DataUltimoEvento.
        /// </summary>
        [XmlElement("_SND-DT-ULT-EVENTO-OS")]
        public string dataUltimoEvento { get; set; }

        /// <summary>
        /// HoraUltimoEvento.
        /// </summary>
        [XmlElement("_SND-HR-ULT-EVENTO-OS")]
        public string horaUltimoEvento { get; set; }

        /// <summary>
        /// ObservacaoBaixa.
        /// </summary>
        [XmlElement("_SND-OBS-BAIXA-OS")]
        public string observacaoBaixa { get; set; }

    }
}
