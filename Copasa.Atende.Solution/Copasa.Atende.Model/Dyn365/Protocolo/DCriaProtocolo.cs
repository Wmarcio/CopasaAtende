using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{
    /// <summary>
    /// classe que cria o json de criação de protocolo
    /// </summary>
    public class DCriaProtocolo : BaseModel
    {

        /// <summary>
        /// titulo 
        /// </summary>
        /// <example>{origem} + -CriaSolicitacao</example>
        [Dyn365Name("title")]
        public string Title { get; set; }

        /// <summary>
        /// Id do CpfCnpj
        /// </summary>
        [Dyn365Name("customerid_contact@odata.bind")]
        public string IdCpfCnpj { get; set; }

        /// <summary>
        /// CpfCnpj
        /// </summary>
        [Dyn365Name("copasa_cpf_cnpj")]
        public string CpfCnpj { get; set; }

        /// <summary>
        /// codigo do tipo de ocorrência que está gerando o protocolo
        /// </summary>
        /// (5)informação(1)solicitação
        [Dyn365Name("casetypecode")]
        public string TipoProtocolo { get; set; }

        /// <summary>
        /// Tipo da empresa
        /// </summary> 
        /// <example>COPASA(1)/COPANOR(2)</example>         
        [Dyn365Name("copasa_empresaid")]
        public string Empresa { get; set; }

        /// <summary>
        /// Tipo da agencia do sistema que está chamando o ws
        /// </summary>        
        [Dyn365Name("copasa_SuaAgencia@odata.bind")]
        public string Agencia { get; set; }

        /// <summary>
        /// nome do solicitante
        /// </summary>
        [Dyn365Name("copasa_nomecompleto")]
        public string Nome { get; set; }
        
        /// <summary>
        /// telefone do solicitante
        /// </summary>
        [Dyn365Name("copasa_telephone1")]
        public string Telefone1 { get; set; }

        /// <summary>
        /// telefone b do do solicitante
        /// </summary>
        [Dyn365Name("copasa_telephone2")]
        public string Telefone2 { get; set; }

        /// <summary>
        /// email do solicitante
        /// </summary>
        [Dyn365Name("copasa_email")]
        public string Email { get; set; }

        /// <summary>
        /// Referencia entre as ruas
        /// </summary>
        [Dyn365Name("copasa_referenciaentreruas")]
        public string Referencia { get; set; }

        /// <summary>
        /// Origem
        /// </summary>
        [Dyn365Name("caseorigincode")]
        public int Origem { get; set; }
    }
}
