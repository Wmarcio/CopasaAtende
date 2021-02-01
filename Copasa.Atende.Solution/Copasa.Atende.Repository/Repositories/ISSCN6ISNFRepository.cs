using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISNFRepository - Nota fiscal fatura
    /// </summary>
    public class ISSCN6ISNFRepository : ISRepository<SCN6ISNFReceive>, IISSCN6ISNFRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISNFRepository(ILog log)
         : base("NotaFiscalFatura:SCN6ISNF_WSD/NotaFiscalFatura_SCN6ISNF_WSD_Port", "SCN6ISNF", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS nota fiscal fatura";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISNFReceive baseModelReceive)
        {
        }
    }
}
