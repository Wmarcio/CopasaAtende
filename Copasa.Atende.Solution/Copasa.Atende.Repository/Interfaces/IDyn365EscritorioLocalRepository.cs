using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface Dyn365EscritorioLocalRepository - Inclui e atualiza dados na tabela de escritórios locais no Dynamics 365
    /// </summary>
    public interface IDyn365EscritorioLocalRepository : IDynamicsRepository<Dyn365EscritorioLocal>
    {
    }
}
