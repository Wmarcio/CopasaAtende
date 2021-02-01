using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface Dyn365AgenciaAtendimento - Inclui e atualiza dados na tabela de agências de atendimento no Dynamics 365
    /// </summary>
    public interface IDyn365AgenciaAtendimentoRepository : IDynamicsRepository<Dyn365AgenciaFisica>
    {
    }
}
