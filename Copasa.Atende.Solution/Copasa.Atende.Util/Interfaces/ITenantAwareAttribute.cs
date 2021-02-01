namespace Copasa.Atende.Util.Interfaces
{

    /// <summary>
    /// Interface ITenantAwareAttribute.
    /// </summary>
    public interface ITenantAwareAttribute
    {
        /// <summary>
        /// Identificador da Unidade Organizacional que o usuário pertence.
        /// </summary>
        string IdUnidadeOrganizacional { get; set; }

        /// <summary>
        /// Identificador da Unidade Operacional que o usuário pertence.
        /// </summary>
        string IdUnidadeOperacional { get; set; }
    }
}
