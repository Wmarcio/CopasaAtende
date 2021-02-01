using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{
    /// <summary>
    /// parametros de entrada da certidao negativa
    /// </summary>
    public class DCertidaoNegativaSend : BaseModel
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
        /// Definição do sistema de origem da chamada
        /// </summary>
        public string Origem { get; set; }

        /// <summary>
        /// Empresa(COPASA/COPANOR)
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Servico - FAT
        /// </summary>
        public string Servico { get; set; }

        /// <summary>
        /// Subtipo - FAT39
        /// </summary>
        public string Subtipo { get; set; }

        /// <summary>
        /// Pavimentacao
        /// </summary>
        public string Pavimentacao { get; set; }
        
        /// <summary>
        /// Localidade
        /// </summary>
        public string LocalidadeId { get; set; }

        /// <summary>
        /// logradouro
        /// </summary>
        public string Referencia { get; set; }

        /// <summary>
        /// logradouro
        /// </summary>
        public string DescricaoDaSolicitacao { get; set; }

    }
}
