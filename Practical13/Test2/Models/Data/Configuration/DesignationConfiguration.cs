using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Test2.Models.Entities;

namespace Test2.Models.Data.Configuration
{
    public class DesignationConfiguration : EntityTypeConfiguration<Designation>
    {
        public DesignationConfiguration()
        {
            ToTable("Designation", "Test2");

            HasKey(d => d.Id);

            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.DesignationName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Designation")
                .HasColumnType("varchar");
        }
    }
}