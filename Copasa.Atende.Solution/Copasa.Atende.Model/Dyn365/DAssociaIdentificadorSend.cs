using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// Dados para a Associação de um Identificador no Microsoft Dynamics 365.
    /// </summary>
    public class DAssociaIdentificadorSend : BaseModel
    {

        /// <summary>
        /// Usuário d365
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// senha d365
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Cpf/Cnpj do usuario logado
        /// </summary>
        public string CpfCnpjLogin { get; set; }

        /// <summary>
        /// Id do CpfCnpj
        /// </summary>
        public string IdCpfCnpj { get; set; }


        /// <summary>
        /// Código do identificador
        /// </summary>
        public string Identificador { get; set; }
        
        /// <summary>
        /// Cnpj do identificador para associar identificador com o cpf
        /// </summary>
        public string IdentificadorCnpj { get; set; }

        /// <summary>
        /// Nome para cadastro no Dyn365
        /// </summary>
        public string NomeSolicitante { get; set; }

        /// <summary>
        /// Telefone para cadastro no Dyn365
        /// </summary>
        public string TelefoneSolicitante { get; set; }

        /// <summary>
        /// Email do usuario logado
        /// </summary>
        public string EmailSolicitante { get; set; }

        /// <summary>
        /// Definição do sistema de origem da chamada
        /// </summary>
        public string Origem { get; set; }

        /// <summary>
        /// Empresa(COPASA/COPANOR)
        /// </summary>
        public string Empresa { get; set; }
    }
}
