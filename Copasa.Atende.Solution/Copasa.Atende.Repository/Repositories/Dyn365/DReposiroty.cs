using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces.Dyn365;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories.Dyn365
{

    /// <summary>
    /// Define o serviço e objeto que serão usados no dynamic repository
    /// </summary>
    public class DRepository : DynamicsRepository<BaseModel>, IDRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public DRepository(string servico,string host)
         : base(servico, ConfigurationManager.AppSettings[host].ToString())
        { }

        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(BaseModel baseModel) { }

    }
}
