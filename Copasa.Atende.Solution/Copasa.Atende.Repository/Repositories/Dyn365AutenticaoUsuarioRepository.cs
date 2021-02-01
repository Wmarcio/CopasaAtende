using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using System.Configuration;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Dyn365AutenticaUsuarioRepository - Autentica usuário do Dynamics 365
    /// </summary>
    public class Dyn365AutenticaoUsuarioRepository : DynamicsRepository<Dyn365User> , IDyn365AutenticaUsuarioRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365AutenticaoUsuarioRepository()
         : base("CopasaUser", ConfigurationManager.AppSettings["Dyn365HostAuthenticate"].ToString())
        {
        }

        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365User baseModel)
        {
        }
    }
}
