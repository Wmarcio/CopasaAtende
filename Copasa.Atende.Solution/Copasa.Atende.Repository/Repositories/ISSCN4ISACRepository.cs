using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISACRepository - Altera email e telefone
    /// </summary>
    public class ISSCN4ISACRepository : ISRepository<SCN4ISACReceive>, IISSCN4ISACRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISACRepository(ILog log)
         : base("AlteraEmailTelefone:SCN4ISAC_WSD/AlteraEmailTelefone_SCN4ISAC_WSD_Port", "SCN4ISAC", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS altera email e telefone";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISACReceive baseModelReceive)
        {
        }
    }
}
