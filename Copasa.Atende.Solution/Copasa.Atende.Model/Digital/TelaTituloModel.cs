using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Digital
{
    /// <summary>
    /// Classe Tela Titulo
    /// </summary>
    public class TelaTituloModel : BaseModel
    {   
        
        /// <summary>
        /// ID da Tela.
        /// </summary>
        public int IdTela { get; set; }

        /// <summary>
        /// ID do Título.
        /// </summary>
        public int IdTitulo { get; set; }
        

        /// <summary>
        /// Ordem.
        /// </summary>
        public int Ordem { get; set; }
        
        /// <summary>
        /// Tela.
        /// </summary>
        public virtual TelaModel Tela { get; set; }
        /// <summary>
        /// Titulo.
        /// </summary>
        public virtual TituloModel Titulo { get; set; }

    }
}
