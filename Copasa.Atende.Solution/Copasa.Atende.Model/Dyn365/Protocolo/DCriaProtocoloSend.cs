using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{
    /// <summary>
    /// dados de entrada para geraçaõ do protocolo
    /// </summary>
    public class DCriaProtocoloSend : BaseModel
    {

        /// <summary>
        /// Id do CpfCnpj
        /// </summary>
        public string IdCpfCnpj { get; set; }

        /// <summary>
        /// cpfcnpj
        /// </summary>
        public string CpfCnpj { get; set; }

        /// <summary>
        /// codigo do tipo de ocorrência que está gerando o protocolo
        /// </summary>
        public string TipoProtocolo { get; set; }
                       
        /// <summary>
        /// nome do solicitante
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// telefone do solicitante
        /// </summary>
        public string Telefone1 { get; set; }

        /// <summary>
        /// telefone b do do solicitante
        /// </summary>
        public string Telefone2 { get; set; }

        /// <summary>
        /// email do solicitante
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Tipo da empresa
        /// </summary> 
        /// <example>COPASA/COPANOR</example> 
        public string Empresa { get; set; }

        /// <summary>
        /// Definição do sistema de origem da chamada
        /// </summary>
        public string Origem { get; set; }

        /// <summary>
        /// Referencia entre ruas
        /// </summary>
        public string Referencia { get; set; }
        
        /// <summary>
        /// Localidade
        /// </summary>
        public string LocalidadeId { get; set; }

        /// <summary>
        /// Descricao da Solicitação
        /// </summary>
        public string DescricaoDaSolicitacao { get; set; }

    }
}
