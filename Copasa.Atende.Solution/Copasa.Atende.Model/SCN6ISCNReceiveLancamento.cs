using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Lista de Lançamento - Certidão Negativa de Débito
    /// </summary>
    public class SCN6ISCNReceiveLancamento : BaseModel
    {
        /// <summary>
        /// Matricula
        /// </summary>
        [XmlElement("_MATRICULA-PARC")]
        public string matricula { get; set; }

        /// <summary>
        /// Descrição do histórico
        /// </summary>
        [XmlElement("_DESC-HISTORICO")]
        public string descricaoHistorico { get; set; }

        /// <summary>
        /// Primeira descrição do histórico
        /// </summary>
        [XmlElement("_DESC-HIST-1")]
        public string descricaoHistorico1 { get; set; }

        /// <summary>
        /// Segunda descrição do histórico
        /// </summary>
        [XmlElement("_DESC-HIST-2")]
        public string descricaoHistorico2 { get; set; }

        /// <summary>
        /// Data de referência do lançamento
        /// </summary>
        [XmlElement("_DATA-REFERENCIA-LANC")]
        public string dataReferenciaLancamento { get; set; }

        /// <summary>
        /// Número da fatura
        /// </summary>
        [XmlElement("_NUM-FATURA-LANC")]
        public string numeroFatura { get; set; }

        /// <summary>
        /// Valor de lançamento
        /// </summary>
        [XmlElement("_VALOR-LANCAMENTO")]
        public string valorLancamento { get; set; }

        /// <summary>
        /// Data de Vencimento da fatura
        /// </summary>
        [XmlElement("_DATA-VENC-FATURA-LANC")]
        public string dataVencimentoFatura { get; set; }
    }
}
