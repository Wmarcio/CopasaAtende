using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4CREXRepository - Gera OS extra
    /// </summary>
    public class ISSCN4CREXRepository : ISRepository<SCN4CREXReceive>, IISSCN4CREXRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4CREXRepository(ILog log)
         : base("GeraOSExtra:SCN4CREX_WSD/GeraOSExtra_SCN4CREX_WSD_Port", "SCN4CREX",log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS gera OS extra";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4CREXReceive baseModelReceive)
        {
        }
    }
}
