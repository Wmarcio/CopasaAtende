namespace Copasa.Atende.Util
{
    using Copasa.Guardian.DTO;
    using System.Collections.Generic;
    using System.Linq;
    using Copasa.Util;

    // TODO Trocar o nome desta classe para o nome do seu projeto ou do schema.
    /// <summary>
    /// Classe que contem as ferramentas para o modelo de Inquilino.
    /// </summary>
    public class MultiInquilinoApp
    {
        /// <summary>
        /// Obter atributo da sessão
        /// </summary>
        /// <returns>String.</returns>
        public static string GetSessionAttribute()
        {
            var acessoUsuario = GetAcessoUsuario();
            List<string> listaIdUnidadeOrganizacional = new List<string>();
            listaIdUnidadeOrganizacional = acessoUsuario.UnidadesUsuario.Select(x => x.IdUnidadeOrganizacional.ToString()).ToList();

            List<string> listaIdUnidadeOperacional = new List<string>();
            foreach (var item in acessoUsuario.UnidadesUsuario)
            {
                listaIdUnidadeOperacional.AddRange(item.UnidadesOperacionais.Select(t => t.Id.ToString()).ToList());
            }

            return string.Format(@"{{""Companhia"": ""{0}"" , ""FiltrarUnidOrganizacional"": ""{1}"", ""IdsUnidadeOrganizacional"": [""{2}""],
                                     ""FiltrarUnidOperacional"": ""{3}"", ""IdsUnidadeOperacional"": [""{4}""] }}",
                acessoUsuario.Companhia.Id, true, string.Join(@""",""", listaIdUnidadeOrganizacional), false, string.Join(@""",""", listaIdUnidadeOperacional));
        }

        /// <summary>
        /// Retorna a lista de unidades organizacionais do usuario logado
        /// </summary>
        /// <returns></returns>
        public static List<UnidadeOrganizacionalDTO> GetUnidadesOrganizacionais()
        {
            var acessoUsuario = GetAcessoUsuario();
            var unidades = acessoUsuario.UnidadesUsuario.ToList();
            return unidades;
        }

        /// <summary>
        /// Método que busca os valores dos filtros obrigatórios na sessão.
        /// </summary>
        private static AcessoUsuarioDTO GetAcessoUsuario()
        {
            try
            {
                return SessionUtil.GetSessionValue<AcessoUsuarioDTO>("UsuarioAutenticado");
            }
            catch
            {
                return null;
            }
        }
    }
}
