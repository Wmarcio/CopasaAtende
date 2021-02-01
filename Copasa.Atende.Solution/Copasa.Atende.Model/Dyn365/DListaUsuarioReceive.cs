using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365
{

    /// <summary>
    /// Lista de Identificadores e Matriculas de um Cpf/Cnpj
    /// </summary>
    public class DListaUsuarioReceive : BaseModelReceive
    {
        /// <summary>
        /// Id do cpfcnpj
        /// </summary>
        public string IdCpfCnpj { get; set; }


        /// <summary>
        /// protocolo criado
        /// </summary>
        public string Protocolo { get; set; }

        /// <summary>
        /// id do protocolo criado
        /// </summary>
        public string ProtocoloId { get; set; }

        /// <summary>
        /// Id do Identificador.
        /// </summary>
        public List<DListaUsuario> ListaIdentificadorMatricula { get; set; }     
    }

    /// <summary>
    /// Item da lista de identificadores e matriculas do usuario
    /// </summary>
    public class DListaUsuario : BaseModel
    {
        /// <summary>
        /// identificador
        /// </summary>
        public string Identificador { get; set; }

        /// <summary>
        /// Matricula
        /// </summary>
        public string Matricula { get; set; }

        /// <summary>
        /// Localidade
        /// </summary>
        public string Localidade { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public string Logradouro { get; set; }

        /// <summary>
        /// Numero do Logradouro
        /// </summary>
        public string NumeroLogradouro { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// Data Inicio Vigencia
        /// </summary>
        public DateTime DataInicioVigencia { get; set; }

        /// <summary>
        /// Data Final Vigencia
        /// </summary>
        public DateTime DataFinalVigencia { get; set; }

        /// <summary>
        /// Cpf Cnpj Dono
        /// </summary>
        public String CpfcnpjProprietario { get; set;  }

        /// <summary>
        /// Data Final Vigencia
        /// </summary>
        public string IdProprietario { get; set; }
    }
}
