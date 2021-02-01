using Copasa.Atende.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Model.Dyn365.Protocolo
{

    /// <summary>
    /// DAtualizaProtocoloSend - Dados para a atualização de um protocolo - funçao private, apenas interno
    /// </summary>
    public class DAtualizaProtocoloSend : BaseModel
    {

        /// <summary>
        /// Título
        /// </summary>
        [Dyn365Name("title")]
        public string Titulo { get; set; }

        /// <summary>
        /// Identificador do solicitante
        /// </summary>
        [Dyn365Name("copasa_identificadorintegracao")]
        public string Identificador { get; set; }

        /// <summary>
        /// Matricula do solicitante
        /// </summary>
        [Dyn365Name("copasa_matriculaintegracao")]
        public string Matricula { get; set; }

        /// <summary>
        /// codigo do serviço no SICOM
        /// </summary>
        [Dyn365Name("copasa_codigoservico")]
        public string Servico { get; set; }

        /// <summary>
        /// codigo do serviço no SICOM
        /// </summary>
        [Dyn365Name("copasa_servicosid@odata.bind")]
        public string ServicoId { get; set; }

        /// <summary>
        /// codigo do subtipo do servico
        /// </summary>
        [Dyn365Name("copasa_subtipoid@odata.bind")]
        public string SubtipoId { get; set; }

        /// <summary>
        /// codigo da pavimentacao
        /// </summary>
        [Dyn365Name("copasa_tipopavimentacaoid@odata.bind")]
        public string PavimentacaoId { get; set; }

        /// <summary>
        /// id da localidade
        /// </summary>
        [Dyn365Name("copasa_localidadeid@odata.bind")]
        public string IdLocalidade { get; set; }

        /// <summary>
        /// Id do bairro
        /// </summary>
        [Dyn365Name("copasa_Bairroid@odata.bind")]
        public string IdBairro { get; set; }

        /// <summary>
        /// Id do logradouro
        /// </summary>
        [Dyn365Name("copasa_Logradouroid@odata.bind")]
        public string IdLogradouro { get; set; }

        /// <summary>
        /// Empresa(COPASA/COPANOR)
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// id para atualização
        /// </summary>
        [Dyn365Id("incidentid")]
        public string ProtocoloId { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        [Dyn365Name("description")]
        public string Descricao { get; set; }

        /// <summary>
        /// Referencia entre ruas
        /// </summary>
        [Dyn365Name("copasa_referenciaentreruas")]
        public string Referencia { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        [Dyn365Name("copasa_cep")]
        public string Cep { get; set; }

        /// <summary>
        /// Tipo de Logradouro
        /// </summary>
        [Dyn365Name("copasa_tipodelogradouro")]
        public string TipoLogradouro { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        [Dyn365Name("copasa_complemento")]
        public string Complemento { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        [Dyn365Name("copasa_tipodecomplemento")]
        public string TipoComplemento { get; set; }
    }
}
