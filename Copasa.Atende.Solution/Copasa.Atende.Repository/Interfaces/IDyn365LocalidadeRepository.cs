using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface Dyn365LocalidadeRepository - Inclui e atualiza dados na tabela de localidades no Dynamics 365
    /// </summary>
    public interface IDyn365LocalidadeRepository : IDynamicsRepository<Dyn365Localidade>
    {
    }
}
