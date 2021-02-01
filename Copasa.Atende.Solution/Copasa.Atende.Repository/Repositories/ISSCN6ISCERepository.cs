using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN6ISCERepository - Atualiza status para envio conta por email
    /// </summary>
    public class ISSCN6ISCERepository : ISRepository<SCN6ISCEReceive>, IISSCN6ISCERepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN6ISCERepository(ILog log)
         : base("ContaPorEmail:SCN6ISCE_WSD/ContaPorEmail_SCN6ISCE_WSD_Port", "SCN6ISCE", log)
        {
        }
        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS atualiza status para envio conta por email";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN6ISCEReceive baseModelReceive)
        {
        }
    }
}
