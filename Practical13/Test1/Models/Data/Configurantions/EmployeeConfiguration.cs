using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Test1.Models.Entities;

namespace Test1.Models.Data.Configurantions
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employee", "Test1");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");

            Property(e => e.DOB)
                .IsRequired()
                .HasColumnType("date");

            Property(e => e.Age)
                .IsOptional();
        }
    }
}