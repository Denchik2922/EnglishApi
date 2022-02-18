using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class RoleConfig : IEntityTypeConfiguration<CustomRole>
    {
        public void Configure(EntityTypeBuilder<CustomRole> builder)
        {
            builder.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.HasData(
            new CustomRole
            {
                Name = "User",
                NormalizedName = "USER"
            },
            new CustomRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new CustomRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN"
            });

        }
    }
}
