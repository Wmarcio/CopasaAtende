using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Localidade
{
    /// <summary>
    /// Retorno da consulta de logradouros de um bairro
    /// </summary>
    public class DLogradouroReceive : BaseModelReceive
    {
        /// <summary>
        /// retorna lista
        /// </summary>
        public List<DLogradouro> Logradouros { get; set; }
    }

    /// <summary>
    /// item da lista
    /// </summary>
    public class DLogradouro : BaseModel
    {
        /// <summary>
        /// nome.
        /// </summary>
        [Dyn365Id("copasa_name")]
        public string LogradouroNome { get; set; }

        /// <summary>
        /// codigo.
        /// </summary>
        [Dyn365Id("copasa_codigosicom")]
        public string LogradouroCodigo { get; set; }

        /// <summary>
        /// id.
        /// </summary>
        [Dyn365Id("copasa_logradouroid")]
        public string LogradouroId { get; set; }
    }
}
