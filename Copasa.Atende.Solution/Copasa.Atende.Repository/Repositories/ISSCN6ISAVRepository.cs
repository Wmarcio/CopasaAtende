using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISAVRepository - Altera vencimento fatura
    /// </summary>
    public class ISSCN6ISAVRepository : ISRepository<SCN6ISAVReceive>, IISSCN6ISAVRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISAVRepository(ILog log)
         : base("AlteraVencimentoFatura:SCN6ISAV_WSD/AlteraVencimentoFatura_SCN6ISAV_WSD_Port", "SCN6ISAV", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS altera vencimento fatura";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISAVReceive baseModelReceive)
        {
        }
    }
}
