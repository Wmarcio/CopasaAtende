using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// URAHistoricoProtocoloDetalheReceive - Detalhes de um determinado protocolo
    /// </summary>
    public class URAHistoricoProtocoloDetalheReceive : BaseModelReceive
    {
        /// <summary>
        /// NumeroProtocoloSS.
        /// </summary>
        public string numeroProtocoloSS { get; set; }

        /// <summary>
        /// DescricaoServicoSS.
        /// </summary>
        public string descricaoServicoSS { get; set; }

        /// <summary>
        /// DataPrevicaoSSDyn365.
        /// </summary>
        public System.DateTime dataPrevisaoSSDyn365 { get; set; }

        /// <summary>
        /// DataBaixaSSDyn365.
        /// </summary>
        public System.DateTime dataTerminoAtendimentoDyn365 { get; set; }

        /// <summary>
        /// SituacaoSS.
        /// </summary>
        public string situacaoSS { get; set; }

        /// <summary>
        /// IdServicoExpand.
        /// </summary>
        public object descricaoServico { get; set; }

        /// <summary>
        /// IdSolicitacaoRelacionadaBind.
        /// </summary>
        public object descricaoSubTipoServico { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        public System.DateTime dataCriacao { get; set; }

        /// <summary>
        /// RespostaAoSolicitante
        /// </summary>
        public string respostaAoSolicitante { get; set; }
    }
}
