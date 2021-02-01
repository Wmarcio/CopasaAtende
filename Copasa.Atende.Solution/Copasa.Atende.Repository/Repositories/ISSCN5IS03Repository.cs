using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// ISSCN5IS03Repository - Informar leitura - Envia leitura CF20
    /// </summary>
    public class ISSCN5IS03Repository : ISRepository<SCN5IS03Receive>, IISSCN5IS03Repository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public ISSCN5IS03Repository(ILog log)
         : base("InformarLeitura:SCN5IS03_WSD/InformarLeitura_SCN5IS03_WSD_Port", "SCN5IS02", log)
        {
        }

        /// <summary>
        /// GetEntidadeNome.
        /// </summary>
        public override string GetEntidadeNome()
        {
            return "Repository IS Informar leitura - Envia leitura CF20";
        }

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected override void TratarRetorno(SCN5IS03Receive baseModelReceive)
        {
        }
    }
}
