using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISFARepository - Pesquisa interrupção de abastecimento
    /// </summary>
    public class ISSCN4ISFARepository : ISRepository<SCN4ISFAReceive>, IISSCN4ISFARepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISFARepository(ILog log)
         : base("FaltaAgua:SCN4ISFA_WSD/FaltaAgua_SCN4ISFA_WSD_Port", "SCN4ISFA", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS pesquisa interrupção de abastecimento";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISFAReceive baseModelReceive)
        {
        }
    }
}
