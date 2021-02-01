using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Business.Interfaces
{
    /// <summary>
    /// Interface Rule - URA.
    /// </summary>
    public interface IURARule
    {
        /// <summary>
        /// Retorna identificadores associados ao cpf/cnpj
        /// </summary>
        BaseResponse ListaIdentificador(URAIdentificadorListaSend uRAIdentificadorListaSend);

        /// <summary>
        /// Histórico de protocolo no Dynamics
        /// </summary>
        BaseResponse GetHistoricoProtocolo(URAHistoricoProtocoloSend uRAHistoricoProtocoloSend);

        /// <summary>
        /// Atualiza protocolo no Dynamics
        /// </summary>
        BaseResponse AtualizaProtocolo(Dyn365ProtocoloURASend dyn365ProtocoloURA);

        /// <summary>
        /// Retorna Detalhes de um protocolo
        /// </summary>
        BaseResponse GetHistoricoServicoDetalhe(URAHistoricoProtocoloDetalheSend uRAHistoricoProtocoloDetalheSend);
    }
}
