using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// Retorno da consulta de bairros de uma localidade
    /// </summary>
    public class DBairroReceive : BaseModelReceive
    {
        /// <summary>
        /// lista retornada
        /// </summary>
       public List<DBairro> Bairros { get; set; }
    }

    /// <summary>
    /// Objeto da lista
    /// </summary>
    public class DBairro : BaseModel
    {
        /// <summary>
        /// nome.
        /// </summary>
        [Dyn365Id("copasa_name")]
        public string BairroNome { get; set; }

        /// <summary>
        /// codigo.
        /// </summary>
        [Dyn365Id("copasa_codigosicom")]
        public string BairroCodigo { get; set; }

        /// <summary>
        /// id.
        /// </summary>
        [Dyn365Id("copasa_bairroid")]
        public string BairroId { get; set; }
    }

}
