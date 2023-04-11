using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PaymentSimplify.Application.Common.Interfaces;
using PaymentSimplify.Domain.Entities;

namespace PaymentSimplify.Infra.Persistence;

public class PaymentSimplifyContext : DbContext , IPaymentSimplifyContext
{
    public PaymentSimplifyContext(DbContextOptions<PaymentSimplifyContext> options) : base(options)
    {
        
    }

    public DbSet<Auth> Auths { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<AccountBank> AccountBanks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}