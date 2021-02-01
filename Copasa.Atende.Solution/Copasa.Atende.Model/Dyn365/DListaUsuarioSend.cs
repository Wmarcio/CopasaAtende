using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{

    /// <summary>
    /// parametros de entrada do ListaUsuarios
    /// </summary>
    public class DListaUsuarioSend : BaseModel
    {
        /// <summary>
        /// Cpf/Cnpj do usuario logado
        /// </summary>
        public string CpfCnpjLogin { get; set; }

        /// <summary>
        /// Id do CpfCnpj
        /// </summary>
        public string IdCpfCnpj { get; set; }

        /// <summary>
        /// Id do CpfCnpj
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// Cnpj vinculado ao cnpj - NAO UTILIZADO
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
