using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ORAMensagemRepository - Mensagens de retorno do sistema
    /// </summary>
    public class ORAMensagemRepository : BaseRepository<ORAMensagem>, IORAMensagemRepository
    {

        /// <summary>
        /// Construtor
        /// </summary>
        public ORAMensagemRepository(ILog log)
            : base(log)
        {
        }
    }
}
