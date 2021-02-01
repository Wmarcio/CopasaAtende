using Copasa.Atende.Model.Digital;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Infrastructure.Digital;
using Copasa.Atende.Repository.Interfaces.Digital;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ClienteRepository : DigitalBaseRepository<ClienteModel>, IClienteRepository
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public ClienteRepository()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="dbContext"></param>
        public ClienteRepository(CopasaDigitalDataContext dbContext)
            : base(dbContext)
        { }

        /// <summary>
        /// Método Atualizar.
        /// </summary>
        /// <param name="entity">Entidade.</param>
        public override void Update(ClienteModel entity)
        {
            base.Update(entity);
        }
    }
}
