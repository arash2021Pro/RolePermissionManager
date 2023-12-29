using CoreBussiness.BussinessEntity.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreStorage.EntityConfiguration;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsPin).HasDefaultValue(false);
        builder.Property(x => x.PhoneNumber).HasMaxLength(11);
        builder.Property(x => x.NationalCode).HasMaxLength(10);
        builder.Property(x => x.Email).IsRequired(false);
        builder.Property(x => x.Password).IsRequired(true);
        builder.Property(x => x.FullName).IsRequired(true);
        builder.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
        builder.HasOne<User>(x => x.Creator).WithMany(x => x.Users).HasForeignKey(x => x.CreatorId)
            .IsRequired(false);
        
    }
}