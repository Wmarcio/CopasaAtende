using Copasa.Atende.Model;
using System.Data.Entity.ModelConfiguration;

namespace Copasa.Atende.Repository.Map
{
    /// <summary>
    /// Mapa de campos da tabela Oracle Cargo
    /// </summary>
    public class ORACargoMap : EntityTypeConfiguration<ORACargo>
    {
        /// <summary>
        /// Mapeamento
        /// </summary>
        public ORACargoMap()
        {
            ToTable("CORP_CARGO");

            HasKey(x => x.codigo);

            Property(x => x.codigo).HasColumnName("CDCARGO");
            Property(x => x.descricao).HasColumnName("DSCARGO");
        }
    }
}
