using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using Copasa.Atende.Util;
using System.Configuration;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Dyn365ProtocoloEncerradoRepository - Inclui e atualiza dados na tabela de protocolo encerrados no Dynamics 365
    /// </summary>
    public class Dyn365ProtocoloEncerradoRepository : DynamicsRepository<Dyn365ProtocoloEncerrado>, IDyn365ProtocoloEncerradoRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365ProtocoloEncerradoRepository()
         : base("CloseIncident", ConfigurationManager.AppSettings["Dyn365Host"].ToString())
        {
        }


        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365ProtocoloEncerrado baseModel)
        {
        }
    }
}
