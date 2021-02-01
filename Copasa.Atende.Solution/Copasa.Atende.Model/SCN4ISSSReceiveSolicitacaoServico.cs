using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISSSReceiveSolicitacaoServico - Busca solicitações de serviços de uma matrícula
    /// </summary>
    public class SCN4ISSSReceiveSolicitacaoServico : BaseModel
    {
        /// <summary>
        /// NumeroSolicitacaoServico.
        /// </summary>
        [XmlElement("_SND-NRO-SOLIC-SERV")]
        public string numeroSolicitacaoServico { get; set; }

        /// <summary>
        /// CodigoServicoInsumo.
        /// </summary>
        [XmlElement("_SND-COD-SERV-INSUMO")]
        public string codigoServicoInsumo { get; set; }

        /// <summary>
        /// DescricaoServico.
        /// </summary>
        [XmlElement("_SND-DESC-SERVICO")]
        public string DescricaoServico { get; set; }

        /// <summary>
        /// DataGeracao.
        /// </summary>
        [XmlElement("_SND-DT-GERACAO-SS")]
        public string dataGeracao { get; set; }

        /// <summary>
        /// HoraGeracao.
        /// </summary>
        [XmlElement("_SND-HR-GERACAO-SS")]
        public string horaGeracao { get; set; }

        /// <summary>
        /// DataPrevisaoAtendimento.
        /// </summary>
        [XmlElement("_SND-DT-PREVISAO-SS")]
        public string dataPrevisaoAtendimento { get; set; }

        /// <summary>
        /// HoraPrevisaoAtendimento.
        /// </summary>
        [XmlElement("_SND-HR-PREVISAO-SS")]
        public string horaPrevisaoAtendimento { get; set; }

        /// <summary>
        /// DataBaixa.
        /// </summary>
        [XmlElement("_SND-DT-BAIXA-SS")]
        public string dataBaixa { get; set; }

        /// <summary>
        /// HoraBaixa.
        /// </summary>
        [XmlElement("_SND-HR-BAIXA-SS")]
        public string horaBaixa { get; set; }

        /// <summary>
        /// CodigoSituacao.
        /// </summary>
        [XmlElement("_SND-SITUACAO-SS")]
        public string codigoSituacao { get; set; }

        /// <summary>
        /// DescricaoSituacao.
        /// </summary>
        [XmlElement("_SND-DESC-SITUACAO-SS")]
        public string descricaoSituacao { get; set; }

    }
}
