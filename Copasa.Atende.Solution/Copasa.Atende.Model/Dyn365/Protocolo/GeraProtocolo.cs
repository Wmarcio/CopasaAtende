using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{
    /// <summary>
    /// 
    /// </summary>
    public class GeraProtocoloSend : BaseModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string CpfCnpjLogin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IdCpfCnpj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TipoProtocolo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Servico { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subtipo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pavimentacao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Origem { get; set; }

        /// <summary>
        /// Localidade - vindo da tabela dynamics (id Guid)
        /// </summary>
        public string LocalidadeId { get; set; }

        /// <summary>
        /// Referencia - vindo do Front - caso não exista deve conter (sem referencia)
        /// </summary>
        public string copasa_referenciaentreruas { get; set; }

        /// <summary>
        /// Descricao da Solicitação - deve conter o titulo da solicitação caso venha vazio
        /// </summary>
        public string DescricaoSolicitacao { get; set; }

        /// <summary>
        /// Titulo
        /// </summary>
        public string Titulo { get; set; }

    }
}
