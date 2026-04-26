

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Test2.models.Entities;

namespace Test2.models.Data.Configurantions
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasKey(u => u.Id);

            Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}