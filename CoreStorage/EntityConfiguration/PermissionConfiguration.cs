using CoreBussiness.BussinessEntity.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreStorage.EntityConfiguration;

public class PermissionConfiguration:IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(x => x.PermissionName).IsRequired(false);
        builder.Property(x => x.IsActive).HasDefaultValue(true);
        builder.HasOne(x => x.Role).WithMany(x => x.Permissions).HasForeignKey(x => x.RoleId);
        builder.HasKey(x => x.Id);
    }
}