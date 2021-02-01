

namespace Copasa.Atende.Repository.Infrastructure
{

    using Copasa.Atende.Model;
    using Copasa.Atende.Repository.Map;
    using Copasa.Atende.Repository.Map.Digital;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    /// <summary>
    /// 
    /// </summary>
    public class CopasaDigitalDataContext : DbContext
    {

        /// <summary>
        /// Construtor da classe AppDataContext
        /// </summary>
        public CopasaDigitalDataContext()
            : base("Name=DigitalDataContext")
        {          
        }

        /// <summary>
        /// Método OnModelCreating.
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("COPASADIGITAL");

            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new ParametroMap());
            modelBuilder.Configurations.Add(new ClienteIdentificadorMap());
            modelBuilder.Configurations.Add(new TelefoneMap());
            modelBuilder.Configurations.Add(new TipoTelefoneMap());
            modelBuilder.Configurations.Add(new TelaMap());
            modelBuilder.Configurations.Add(new TituloMap());
            modelBuilder.Configurations.Add(new TelaTituloMap());

        }

    }
}
