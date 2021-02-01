using Copasa.Atende.Model.Digital;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Infrastructure.Digital;
using Copasa.Atende.Repository.Interfaces.Digital;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories.Digital
{
    /// <summary>
    /// Classe ClienteIdentificadorRepository.
    /// </summary>
    public class ClienteIdentificadorRepository : DigitalBaseRepository<ClienteIdentificadorModel>, IClienteIdentificadorRepository
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public ClienteIdentificadorRepository()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="dbContext"></param>
        public ClienteIdentificadorRepository(CopasaDigitalDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
