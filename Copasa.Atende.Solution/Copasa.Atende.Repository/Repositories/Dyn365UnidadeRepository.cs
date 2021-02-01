using Copasa.Atende.Model;
using Copasa.Atende.Repository.Infrastructure;
using Copasa.Atende.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// Dyn365UnidadeRepository - Inclui e atualiza dados na tabela de unidades no Dynamics 365
    /// </summary>
    public class Dyn365UnidadeRepository : DynamicsRepository<Dyn365Unidade>, IDyn365UnidadeRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365UnidadeRepository()
         : base("copasa_unidades", ConfigurationManager.AppSettings["Dyn365Host"].ToString())
        {
        }


        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365Unidade baseModel)
        {
        }
    }
}
