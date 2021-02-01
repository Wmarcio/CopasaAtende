using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISCCRepository - Consiste matrícula centralizadora
    /// </summary>
    public class ISSCN6ISCCRepository : ISRepository<SCN6ISCCReceive>, IISSCN6ISCCRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISCCRepository(ILog log)
         : base("ConsisteMatriculaCentralizadora:SCN6ISCC_WSD/ConsisteMatriculaCentralizadora_SCN6ISCC_WSD_Port", "SCN6ISCC", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS consiste matrícula centralizadora";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISCCReceive baseModelReceive)
        {
        }
    }
}
