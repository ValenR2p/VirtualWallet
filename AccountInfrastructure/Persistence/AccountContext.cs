using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System;

namespace Account.API.Infrastructure
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {

        }

        public DbSet<AccountModel> Account{ get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<TypeCurrency> TypeCurrency { get; set; }
        public DbSet<StateAccount> StateAccount { get; set; }
        public DbSet<PersonalAccount> PersonalAccount { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>(builder =>
            {
                builder.ToTable("Account");
                builder.HasKey(ac => ac.AccountId);

                builder.Property(ac => ac.AccountId)
                    .ValueGeneratedOnAdd();

                builder.Property(ac => ac.Alias)
                    .HasMaxLength(50);

                builder.HasOne(ac => ac.User)
                    .WithOne(u => u.Account)
                    .HasForeignKey<AccountModel>(ac => ac.UserId);

                builder.HasOne(ac => ac.AccountType)
                    .WithMany(at => at.Accounts)
                    .HasForeignKey(ac => ac.AccTypeId);

                builder.HasOne(ac => ac.TypeCurrency)
                    .WithMany(tc => tc.Accounts)
                    .HasForeignKey(ac => ac.CurrencyId);

                builder.HasOne(ac => ac.StateAccount)
                    .WithMany(sa => sa.Accounts)
                    .HasForeignKey(ac => ac.StateId);
            });

            modelBuilder.Entity<PersonalAccount>(builder =>
            {
                builder.HasKey(pa => pa.Id);

                builder.Property(pa => pa.Id)
                    .ValueGeneratedOnAdd();

                builder.HasOne(pa => pa.User)
                    .WithMany(u => u.PersonalAccounts)
                    .HasForeignKey(pa => pa.UserId);
            }); 

            modelBuilder.Entity<AccountType>(builder =>
            {
                //tabla estatica
                builder.HasKey(at => at.Id);

                builder.Property(at => at.Name)
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<TypeCurrency>(builder =>
            {
                //tabla estatica
                builder.HasKey(tc => tc.Id);

                builder.Property(tc => tc.Name)
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<StateAccount>(builder =>
            {
                //tabla estatica
                builder.HasKey(sa => sa.Id);

                builder.Property(sa => sa.Name)
                    .HasMaxLength(80);
            });

            // Data Seeding to static table?
        }
    }
}
