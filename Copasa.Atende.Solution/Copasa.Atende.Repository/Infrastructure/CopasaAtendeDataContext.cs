namespace Copasa.Atende.Repository.Infrastructure
{
    using Copasa.Atende.Model;
    using Copasa.Atende.Repository.Map;
    using System.Data.Entity;


    // TODO Trocar o nome desta classe para o nome do seu projeto ou do schema.

    /// <summary>
    /// Classe AppDataContext.
    /// </summary>
    public class CopasaAtendeDataContext : DbContext
    {
        /// <summary>
        /// Objeto stático do log.
        /// </summary>
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static CopasaAtendeDataContext()
        {            
            Database.SetInitializer<CopasaAtendeDataContext>(null);
        }

        /// <summary>
        /// Construtor da classe AppDataContext
        /// </summary>
        public CopasaAtendeDataContext()
            : base("Name=CopasaAtendeDataContext")
        {
            Database.Log = (dbLog => log.Debug(dbLog));
        }

        /// <summary>
        /// DbSet Mensagem
        /// </summary>
        public DbSet<ORAMensagem> ORAMensagem { get; set; }

        /// <summary>
        /// DbSet Empregado
        /// </summary>
        public DbSet<ORAEmpregado> ORAEmpregado { get; set; }

        /// <summary>
        /// DbSet Cargo
        /// </summary>
        public DbSet<ORACargo> ORACargo { get; set; }

        /// <summary>
        /// DbSet UnidadeOrganizacional
        /// </summary>
        public DbSet<ORAUnidadeOrganizacional> ORAUnidadeOrganizacional { get; set; }
        
        /// <summary>
        /// Metodo OnModelCreating.
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ORAMensagemMap());
            modelBuilder.Configurations.Add(new ORAEmpregadoMap());
            modelBuilder.Configurations.Add(new ORACargoMap());
            modelBuilder.Configurations.Add(new ORAUnidadeOrganizacionalMap());
        }
    }
}
