using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;

namespace Copasa.Atende.Repository.Interfaces
{
    /// <summary>
    /// Interface Dyn365AutenticaUsuarioRepository - Autentica usuário do Dynamics 365
    /// </summary>
    public interface IDyn365AutenticaUsuarioRepository : IDynamicsRepository<Dyn365User>
    {
    }
}
