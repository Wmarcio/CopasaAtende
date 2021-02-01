using Copasa.Atende.Model.Digital;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Infrastructure.Digital;
using Copasa.Atende.Repository.Interfaces.Digital;
using Copasa.Digital.Repository.Interfaces;

namespace Copasa.Digital.Repository.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class TituloRepository : DigitalBaseRepository<TituloModel>, ITituloRepository
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public TituloRepository()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="dbContext"></param>
        public TituloRepository(CopasaDigitalDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
