using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface Dyn365CriaOSRepository - Inclui e atualiza dados na tabela de ordem de serviço no Dynamics 365
    /// </summary>
    public interface IDyn365OrdemServicoRepository : IDynamicsRepository<Dyn365OrdemServico>
    {
    }
}
