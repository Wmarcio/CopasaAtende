using Copasa.Atende.Model.Digital;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Infrastructure.Digital;
using Copasa.Digital.Repository.Interfaces;

namespace Copasa.Digital.Repository.Repositories
{

    /// <summary>
    /// Classe TelaRepository.
    /// </summary>
    public class TelaRepository : DigitalBaseRepository<TelaModel>, ITelaRepository
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public TelaRepository()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="dbContext"></param>
        public TelaRepository(CopasaDigitalDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
