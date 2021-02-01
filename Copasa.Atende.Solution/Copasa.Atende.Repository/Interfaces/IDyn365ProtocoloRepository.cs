using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface Dyn365AtualizaSSRepository - Inclui e atualiza dados na tabela de protocolo no Dynamics 365
    /// </summary>
    public interface IDyn365ProtocoloRepository : IDynamicsRepository<Dyn365Protocolo>
    {
        /// <summary>
        /// Retorna o código do tipoLogradouro
        /// </summary>
        int getCodigoTipoLogradouro(string tipoLogradouro);
    }
}
