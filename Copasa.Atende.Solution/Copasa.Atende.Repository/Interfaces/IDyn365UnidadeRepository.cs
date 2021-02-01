using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface Dyn365UnidadeRepository - Inclui e atualiza dados na tabela de unidades no Dynamics 365
    /// </summary>
    public interface IDyn365UnidadeRepository : IDynamicsRepository<Dyn365Unidade>
    {
    }
}
