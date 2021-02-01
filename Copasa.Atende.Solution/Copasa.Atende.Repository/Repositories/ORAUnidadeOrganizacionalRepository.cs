using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ORAUnidadeOrganizacional - Tabela de unidades organizacionais
    /// </summary>
    public class ORAUnidadeOrganizacionalRepository : BaseRepository<ORAUnidadeOrganizacional>, IORAUnidadeOrganizacionalRepository
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ORAUnidadeOrganizacionalRepository(ILog log)
            : base(log)
        {
        }
    }
}
