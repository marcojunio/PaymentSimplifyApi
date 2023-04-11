using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentSimplify.Domain.Entities;
using PaymentSimplify.Infra.Persistence.Configurations.Base;

namespace PaymentSimplify.Infra.Persistence.Configurations;

public class AuthConfiguration : EntityTypeConfigurationBase<Auth>
{
    public override void Configure(EntityTypeBuilder<Auth> builder)
    {
        builder.ToTable("AUTH");
        
        base.Configure(builder);

        builder.Property(f => f.Password).HasColumnName("PASSWORD").IsRequired();
        builder.Property(f => f.Salt).HasColumnName("SALT");
        
        builder.OwnsOne(f => f.Email, action =>
        {
            action.Property(f => f.Addreess).HasColumnName("EMAIL").HasMaxLength(80).IsRequired();
            action.HasIndex(f => f.Addreess).IsUnique();
        });

        builder
            .HasOne(f => f.Customer)
            .WithOne()
            .HasForeignKey<Auth>("COSTUMER_ID")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

    }
}