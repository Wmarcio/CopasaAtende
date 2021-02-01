using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISGPRepository - Gera parcelamento de débito
    /// </summary>
    public class ISSCN6ISGPRepository : ISRepository<SCN6ISGPReceive>, IISSCN6ISGPRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISGPRepository(ILog log)
         : base("ParcelamentoDebito:SCN6ISGP_WSD/ParcelamentoDebito_SCN6ISGP_WSD_Port", "SCN6ISGP", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS gera parcelamento de débito";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISGPReceive baseModelReceive)
        {
        }
    }
}
