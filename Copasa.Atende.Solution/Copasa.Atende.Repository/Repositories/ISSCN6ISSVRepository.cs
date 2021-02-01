using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISSVRepository - Segunda via fatura
    /// </summary>
    public class ISSCN6ISSVRepository : ISRepository<SCN6ISSVReceive>, IISSCN6ISSVRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISSVRepository(ILog log)
         : base("SegundaViaFatura:SCN6ISSV_WSD/SegundaViaFatura_SCN6ISSV_WSD_Port", "SCN6ISSV", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS segunda via fatura";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISSVReceive baseModelReceive)
        {
        }
    }
}
