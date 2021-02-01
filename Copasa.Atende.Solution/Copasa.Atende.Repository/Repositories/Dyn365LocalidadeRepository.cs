using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using System.Configuration;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Dyn365LocalidadeRepository - Inclui e atualiza dados na tabela de localidades no Dynamics 365
    /// </summary>
    public class Dyn365LocalidadeRepository : DynamicsRepository<Dyn365Localidade>, IDyn365LocalidadeRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365LocalidadeRepository()
         : base("copasa_localidades", ConfigurationManager.AppSettings["Dyn365Host"].ToString())
        {
        }


        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365Localidade baseModel)
        {
        }
    }
}
