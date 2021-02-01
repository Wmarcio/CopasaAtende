using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;

namespace Copasa.Atende.Model.Digital
{
    /// <summary>
    /// Model representativo da tabela APP_TELA.
    /// </summary>
    [Serializable()]
    public class TelaModel : BaseModel
    {
        /// <summary>
        /// ID do Tela.
        /// </summary>
        public int IdTela { get; set; }

        /// <summary>
        /// Descrição.
        /// </summary>
        public string Descricao { get; set; }
        
        /// <summary>
        /// Ativo
        /// </summary>
        public string IsAtivo { get; set; }
        
        /// <summary>
        /// Detalhe.
        /// </summary>
        public string Detalhe { get; set; }
        

        /// <summary>
        /// Coleção de relacionamentos.
        /// </summary>
        public virtual List<TelaTituloModel> TelaTitulos { get; set; }
    }



}
