using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// 
    /// </summary>
   public class DDesassociaIdentificadorSend : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string CpfCnpjId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CpfCnpj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// Empresa(COPASA/COPANOR)
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Definição do sistema de origem da chamada
        /// </summary>
        public string Origem { get; set; }
    }

    /// <summary>
    /// Parametros de entrada fixos para o serviço de desassociação de id
    /// </summary>
    public class DDesassociaIdentificador : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Dyn365Name("statuscode")]
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Dyn365Name("statecode")]
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Dyn365Id("copasa_controledeidentificadorid")]
        public string ControleIdentificadorIdAtualizar { get; set; }
    }
}
