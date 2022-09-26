using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Myra.Domain.Models;

namespace Myra.Infra.ConfigMigration
{
    public class UserConfig : RegisterConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder
                .Property(user => user.Name)
                .HasMaxLength(60)
                .IsRequired();

            builder
                .Property(user => user.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(u => u.Email)
                .HasMaxLength(40)
                .IsRequired();

            builder
               .Property(u => u.Password)
               .IsRequired();

            builder
                .HasOne(u => u.UserRole)
                .WithMany()
                .HasForeignKey(u => u.IdUserRole)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("getdate()");

            builder
              .Property(p => p.UpdatedAt)
              .ValueGeneratedOnUpdate()
              .HasDefaultValueSql("getdate()")
              .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
        }
    }
}
