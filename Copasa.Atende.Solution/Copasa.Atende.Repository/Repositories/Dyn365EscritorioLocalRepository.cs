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
    /// Dyn365EscritorioLocalRepository - Inclui e atualiza dados na tabela de escritórios locais no Dynamics 365
    /// </summary>
    public class Dyn365EscritorioLocalRepository : DynamicsRepository<Dyn365EscritorioLocal>, IDyn365EscritorioLocalRepository
    {
        /// <summary>
        /// Contrutor.
        /// </summary>
        public Dyn365EscritorioLocalRepository()
         : base("copasa_escritoriolocals", ConfigurationManager.AppSettings["Dyn365Host"].ToString())
        {
        }


        /// <summary>
        /// Trata envio para Dynamics
        /// </summary>
        protected override void TratarEnvio(Dyn365EscritorioLocal baseModel)
        {
        }
    }
}
