using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface Dyn365ProtocoloEncerradoRepository - Inclui e atualiza dados na tabela de protocolo encerrados no Dynamics 365
    /// </summary>
    public interface IDyn365ProtocoloEncerradoRepository : IDynamicsRepository<Dyn365ProtocoloEncerrado>
    {
    }
}
