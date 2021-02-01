using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// URAHistoricoProtocoloReceive - protocolos
    /// </summary>
    public class URAHistoricoProtocoloReceiveProtocolo : BaseModel
    {
        /// <summary>
        /// NumeroProtocoloSS.
        /// </summary>
        public string numeroProtocoloSS { get; set; }

        /// <summary>
        /// CodigoServico.
        /// </summary>
        public string codigoServico { get; set; }

        /// <summary>
        /// NumeroSS.
        /// </summary>
        public string numeroSS { get; set; }

        /// <summary>
        /// SituacaoSS.
        /// </summary>
        public string situacaoSS { get; set; }

        /// <summary>
        /// DescSituacaoSolicitacao.
        /// </summary>
        public string descSituacaoSolicitacao { get; set; }

        /// <summary>
        /// StatusProtocolo.
        /// </summary>
        public int statusProtocolo { get; set; }

        /// <summary>
        /// DescStatusProtocolo.
        /// </summary>
        public string descStatusProtocolo { get; set; }

        /// <summary>
        /// StatusSolicitacao.
        /// </summary>
        public int statusSolicitacao { get; set; }

        /// <summary>
        /// DescStatusSolicitacao.
        /// </summary>
        public string descStatusSolicitacao { get; set; }

        /// <summary>
        /// RespostaAoSolicitante
        /// </summary>
        public string RespostaAoSolicitante { get; set; }

        /// <summary>
        /// DataPrevisaoAtendimento.
        /// </summary>
        public string dataPrevisaoAtendimento { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        public string identificador { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        public string matricula { get; set; }

        /// <summary>
        /// DataCriacao
        /// </summary>
        public string dataCriacao { get; set; }
    }
}
