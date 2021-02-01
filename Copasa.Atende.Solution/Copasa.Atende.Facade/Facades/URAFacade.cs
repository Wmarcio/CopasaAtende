using Copasa.Atende.Business.Core;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Facade.Interfaces;
using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Facade.Facades
{
    /// <summary>
    /// Facade - URA.
    /// </summary>
    public class URAFacade : BaseRule, IURAFacade
    {
        private IURARule _uRARule;

        /// <summary>
        /// Construtor URAFacade.
        /// </summary>
        /// <param name="uRARule">IURARule.</param>
        public URAFacade(IURARule uRARule)
        {
            _uRARule = uRARule;
        }

        /// <summary>
        /// Retorna identificadores associados ao cpf/cnpj no D365
        /// </summary>
        public BaseResponse ListaIdentificador(URAIdentificadorListaSend uRAIdentificadorListaSend)
        {
            return _uRARule.ListaIdentificador(uRAIdentificadorListaSend);
        }

        /// <summary>
        /// Histórico de protocolo no Dynamics
        /// </summary>
        public BaseResponse GetHistoricoProtocolo(URAHistoricoProtocoloSend uRAHistoricoProtocoloSend)
        {
            return _uRARule.GetHistoricoProtocolo(uRAHistoricoProtocoloSend);
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Atualiza protocolo no Dynamics
        /// </summary>
        public BaseResponse AtualizaProtocolo(Dyn365ProtocoloURASend dyn365ProtocoloURA)
        {
            return _uRARule.AtualizaProtocolo(dyn365ProtocoloURA);
        }

        /// <summary>
        /// Retorna Detalhes de um protocolo
        /// </summary>
        public BaseResponse GetHistoricoServicoDetalhe(URAHistoricoProtocoloDetalheSend uRAHistoricoProtocoloDetalheSend)
        {
            return _uRARule.GetHistoricoServicoDetalhe(uRAHistoricoProtocoloDetalheSend);
        }
    }
}
