using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Test2.Models.Entities;

namespace Test2.Models.Data.Configuration
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employee", "Test2");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");

            Property(e => e.MiddleName)
                .IsOptional()
                .HasMaxLength(50)
                .HasColumnType("varchar");

            Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar");

            Property(e => e.DOB)
                .IsRequired()
                .HasColumnType("date");

            Property(e => e.MobileNumber)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("varchar");

            Property(e => e.Address)
                .IsOptional()
                .HasMaxLength(100)
                .HasColumnType("varchar");

            Property(e => e.Salary)
                .IsRequired()
                .HasColumnType("decimal")
                .HasPrecision(18, 2);

            Property(e => e.DesignationId)
                .IsOptional();

            HasOptional(e => e.Designation)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DesignationId)
                .WillCascadeOnDelete(false);
        }
    }
}