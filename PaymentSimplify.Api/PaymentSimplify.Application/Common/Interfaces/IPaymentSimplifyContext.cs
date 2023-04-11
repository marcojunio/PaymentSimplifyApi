using Microsoft.EntityFrameworkCore;
using PaymentSimplify.Domain.Entities;

namespace PaymentSimplify.Application.Common.Interfaces;

public interface IPaymentSimplifyContext
{
    public DbSet<Auth> Auths { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<AccountBank> AccountBanks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}