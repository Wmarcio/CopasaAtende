using Copasa.Atende.Model.Digital;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Infrastructure.Digital;
using Copasa.Digital.Repository.Interfaces;

namespace Copasa.Digital.Repository.Repositories
{
    /// <summary>
    /// Classe Repository Tela Titulo
    /// </summary>
    public class TelaTituloRepository : DigitalBaseRepository<TelaTituloModel>, ITelaTituloRepository
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public TelaTituloRepository()
        {
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="dbContext"></param>
        public TelaTituloRepository(CopasaDigitalDataContext dbContext)
            : base(dbContext)
        { }

        /// <summary>
        /// Método Atualizar.
        /// </summary>
        /// <param name="entity">Entidade.</param>
        public override void Update(TelaTituloModel entity)
        {
            base.Update(entity);
        }
    }
}
