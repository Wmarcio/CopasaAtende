using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISORReceive - Busca OS's de uma solicitação de serviço
    /// </summary>
    public class SCN4ISORReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// NumeroSolicitacaoServico.
        /// </summary>
        [XmlElement("_SND-NRO-SOLIC-SERV")]
        public string numeroSolicitacaoServico { get; set; }

        /// <summary>
        /// DataGeracaoSS.
        /// </summary>
        [XmlElement("_SND-DT-GERACAO-SS")]
        public string dataGeracaoSS { get; set; }

        /// <summary>
        /// HoraGeracaoSS.
        /// </summary>
        [XmlElement("_SND-HR-GERACAO-SS")]
        public string horaGeracaoSS { get; set; }

        /// <summary>
        /// DataPrevisaoSS.
        /// </summary>
        [XmlElement("_SND-DT-PREVISAO-SS")]
        public string dataPrevisaoSS { get; set; }

        /// <summary>
        /// HoraPrevisaoSS.
        /// </summary>
        [XmlElement("_SND-HR-PREVISAO-SS")]
        public string horaPrevisaoSS { get; set; }

        /// <summary>
        /// DataBaixaSS.
        /// </summary>
        [XmlElement("_SND-DT-BAIXA-SS")]
        public string dataBaixaSS { get; set; }

        /// <summary>
        /// HoraBaixaSS.
        /// </summary>
        [XmlElement("_SND-HR-BAIXA-SS")]
        public string horaBaixaSS { get; set; }

        /// <summary>
        /// OrdensServicoSicom.
        /// </summary>
        [XmlElement("_TAB-ORDEM-SERVICO")]
        [JsonIgnore]
        public SCN4ISORReceiveOrdemServico[] ordensServicoSicom { get; set; }

        /// <summary>
        /// OrdensServico.
        /// </summary>
        public List<SCN4ISORReceiveOrdemServico> ordensServico { get; set; }
    }
}
