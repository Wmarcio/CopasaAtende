using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using System.Configuration;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Dyn365SuperintendenciaRepository - Inclui e atualiza dados na tabela de superintendências no Dynamics 365
    /// </summary>
    public class Dyn365SuperintendenciaRepository : DynamicsRepository<Dyn365Superintendencia>, IDyn365SuperintendenciaRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365SuperintendenciaRepository()
         : base("copasa_superintendencias", ConfigurationManager.AppSettings["Dyn365Host"].ToString())
        {
        }


        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365Superintendencia baseModel)
        {
        }
    }
}
