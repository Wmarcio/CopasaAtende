using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4CRUNRepository - Unidade de Destino
    /// </summary>
    public class ISSCN4CRUNRepository : ISRepository<SCN4CRUNReceive>, IISSCN4CRUNRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4CRUNRepository(ILog log)
         : base("BuscaUnidadeDestino:SCN4CRUN_WSD/BuscaUnidadeDestino_SCN4CRUN_WSD_Port", "SCN4CRUN",log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS Unidade de Destino";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4CRUNReceive baseModelReceive)
        {
        }
    }
}
