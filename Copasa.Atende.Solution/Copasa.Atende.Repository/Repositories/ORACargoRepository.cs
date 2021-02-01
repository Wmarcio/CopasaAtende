using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ORAUnidadeOrganizacional - Tabela de cargos
    /// </summary>
    public class ORACargoRepository : BaseRepository<ORACargo>, IORACargoRepository
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ORACargoRepository(ILog log)
            : base(log)
        {
        }
    }
}
