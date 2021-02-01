using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN4ISVRRepository - Vazamento na rua
    /// </summary>
    public class ISSCN4ISVRRepository : ISRepository<SCN4ISVRReceive>, IISSCN4ISVRRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN4ISVRRepository(ILog log)
         : base("InformarLeitura:SCN4ISFA_WSD/VazamentoRua_SCN4ISVR_WSD_Port", "SCN4ISVR", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS vazamento na rua";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN4ISVRReceive baseModelReceive)
        {
        }
    }
}
