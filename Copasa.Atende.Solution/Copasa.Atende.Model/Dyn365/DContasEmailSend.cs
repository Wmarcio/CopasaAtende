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
   public class DContasEmailSend : BaseModel
    {

        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// Id do CpfCnpj
        /// </summary>
        public string IdCpfCnpj { get; set; }

        /// <summary>
        /// Código do identificador
        /// </summary>
        public string Identificador { get; set; }


        /// <summary>
        /// protocolo(caso de vazamento na rua)
        /// </summary>
        public string NumeroProtocolo { get; set; }

        /// <summary>
        /// protocolo id(caso de vazamento na rua)
        /// </summary>
        public string IdProtocolo { get; set; }

        /// <summary>
        /// Lista de matriculas
        /// </summary>
        public List<EmailMatriculaItem> EmailsMatriculas { get; set; }
        
        /// <summary>
        /// Servico
        /// </summary>
        public string Servico { get; set; }

        /// <summary>
        /// Subtipo
        /// </summary>
        public string SubTipo { get; set; }

        /// <summary>
        /// Pavimentacao
        /// </summary>
        public string Pavimentacao { get; set; }

        /// <summary>
        /// Empresa
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Origem
        /// </summary>
        public string Origem { get; set; }
    }

    /// <summary>
    /// Objeto da lista de matriculas
    /// </summary>
    public class EmailMatriculaItem
    {
        /// <summary>
        /// matriculas vinculadas ao email
        /// </summary>
        public List<MatriculaItem> Matriculas { get; set; }

        /// <summary>
        /// Email da matricula
        /// </summary>
        public string Email { get; set; }

    }

    /// <summary>
    /// Objeto da lista de matriculas
    /// </summary>
    public class MatriculaItem
    {
        /// <summary>
        /// matriculas vinculadas ao email
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// Status R-registro/N-cancelamento
        /// </summary>
        public string Status { get; set; }
    }



}
