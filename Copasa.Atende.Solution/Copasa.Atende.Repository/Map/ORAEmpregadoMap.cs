using Copasa.Atende.Model;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map
{
    /// <summary>
    /// Mapa de campos da tabela Oracle Empregados
    /// </summary>
    class ORAEmpregadoMap : EntityTypeConfiguration<ORAEmpregado>
    {
        /// <summary>
        /// Mapeamento
        /// </summary>
        public ORAEmpregadoMap()
        {
            ToTable("CORP_EMPREGADO");

            HasKey(x => x.matricula);

            Property(x => x.matricula).HasColumnName("NUMATRICULAEMPREGADO");
            Property(x => x.nome).HasColumnName("NMEMPREGADO");
            Property(x => x.codigoCargo).HasColumnName("CDCARGO");
            Property(x => x.codigoUnidadeOrganizacional).HasColumnName("CDUNIDADEORGANIZ");

            HasRequired(x => x.cargo)
                .WithMany()
                .HasForeignKey(x => x.codigoCargo);


            HasRequired(x => x.unidadeOrganizacional)
                .WithMany()
                .HasForeignKey(x => x.codigoUnidadeOrganizacional);
        }
    }
}
