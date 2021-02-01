using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Copasa.Atende.WebService.Provider
{
    /// <summary>
    /// Provê o token de acesso.
    /// </summary>
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Realiza as validações quando o usuário se autenticar usando o token de acesso gerado;
        /// </summary>
        /// <param name="context"></param>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => {
                context.Validated();
            });
            
        }

        /// <summary>
        /// Recebe um context com as informações passadas pelo usuário.
        /// </summary>
        /// <param name="context"></param>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() => {
                    //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                if (ValidaLogin(context.UserName, context.Password))
                {

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("sub", context.UserName));
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("Acesso inválido.", "As credenciais não conferem.");
                    return;
                }
            });
        }

        /// <summary>
        /// Validacao de login.
        /// ***** Futuramente integracao com o guardian *****
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool ValidaLogin(string userName, string password)
        {
            //string userContext = Copasa.Util.CriptografiaUtil.CriptografarSHA(userName);
            //string passContext = Copasa.Util.CriptografiaUtil.CriptografarSHA(password);
            string userContext = userName;
            string passContext = password;
            string user = Copasa.Util.ConfigurationUtil.GetAppSetting("tokenCopasaUser");
            string pass = Copasa.Util.ConfigurationUtil.GetAppSetting("tokenCopasaPassword"); 
                
            if (userContext.Equals(user) && passContext.Equals(pass))
                return true;

            return false;
        }
    }
}