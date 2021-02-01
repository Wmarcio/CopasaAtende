using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISCBRepository - Código de barras de fatura
    /// </summary>
    public class ISSCN6ISCBRepository : ISRepository<SCN6ISCBReceive>, IISSCN6ISCBRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISCBRepository(ILog log)
         : base("GeraCodigoBarras:SCN6ISCB_WSD/GeraCodigoBarras_SCN6ISCB_WSD_Port", "SCN6ISCB", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS código de barras de fatura";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISCBReceive baseModelReceive)
        {
        }
    }
}
