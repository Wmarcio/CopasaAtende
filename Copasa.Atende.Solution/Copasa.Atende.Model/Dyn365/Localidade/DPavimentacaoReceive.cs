using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// Retorno da busca da pavimentação
    /// </summary>
    public class DPavimentacaoReceive : BaseModelReceive
    {
        /// <summary>
        /// lista de pavimentações
        /// </summary>
        public List<DPavimentacao> Pavimentacoes { get; set; }
    }

    /// <summary>
    /// item da consulta de pavimentações
    /// </summary>
    public class DPavimentacao : BaseModelReceive
    {
        /// <summary>
        /// nome da pavimentacao.
        /// </summary>
        [Dyn365Id("copasa_name")]
        public string Nome { get; set; }

        /// <summary>
        /// id do tipo da pavimentacao.
        /// </summary>
        [Dyn365Id("copasa_tipodepavimentacaoid")]
        public string TipoId { get; set; }

        /// <summary>
        /// codigo SICOM da pavimentacao.
        /// </summary>
        [Dyn365Id("copasa_codigosicom")]
        public string CodigoSiCom { get; set; }

        /// <summary>
        /// codigo da pavimentacao.
        /// </summary>
        [Dyn365Id("copasa_codigo")]
        public string Codigo { get; set; }
    }
}
