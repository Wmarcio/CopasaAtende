using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using System.Configuration;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Dyn365AgenciaAtendimento - Inclui e atualiza dados na tabela de agências de atendimento no Dynamics 365
    /// </summary>
    public class Dyn365AgenciaAtendimentoRepository : DynamicsRepository<Dyn365AgenciaFisica>, IDyn365AgenciaAtendimentoRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365AgenciaAtendimentoRepository()
         : base("copasa_agenciafisicas", ConfigurationManager.AppSettings["Dyn365Host"].ToString())
        {
        }


        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365AgenciaFisica baseModel)
        {
        }
    }
}
