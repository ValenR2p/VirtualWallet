using Domain.Models;
using Microsoft.EntityFrameworkCore;
using TransferInfrastructure.DataSet;

namespace TransferInfrastructure.Persistence
{
    public class TransferContext : DbContext
    {      
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=.;database=TPVirtualWallet;user id=sa;password=Internacional17;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("Transfer");

                entity.HasKey(t => t.Id);

                // Relación uno-a-muchos con TransferType
                entity.HasOne(t => t.TransferType)
                      .WithMany(tt => tt.Transfers)
                      .HasForeignKey(t => t.TypeId)
                      .OnDelete(DeleteBehavior.Restrict); // No eliminar en cascada si se elimina el TransferType

                // Relación uno-a-uno (cuenta de origen)
                entity.HasOne(t => t.SrcAccount)
                      .WithMany() // Sin navegación inversa desde AccountModel a Transfer
                      .HasForeignKey(t => t.SrcAccountId)
                      .OnDelete(DeleteBehavior.Restrict); // No eliminar en cascada al eliminar la cuenta de origen

                // Relación uno-a-uno (cuenta de destino)
                entity.HasOne(t => t.DestAccount)
                      .WithMany() // Sin navegación inversa desde AccountModel a Transfer
                      .HasForeignKey(t => t.DestAccountId)
                      .OnDelete(DeleteBehavior.Restrict); // No eliminar en cascada al eliminar la cuenta de destino
            });

            // Configuración de TransferType
            modelBuilder.Entity<TransferType>(entity =>
            {
                entity.ToTable("TransferType");

                entity.HasKey(tt => tt.Id);

                entity.Property(tt => tt.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // Configuración de AccountModel
            modelBuilder.Entity<AccountModel>(entity =>
            {
                entity.ToTable("Account");

                entity.HasKey(a => a.AccountId);

                entity.Property(a => a.CBU)
                      .IsRequired()
                      .HasMaxLength(22);

                entity.Property(a => a.Alias)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(a => a.NumberAccount)
                      .IsRequired();

                entity.Property(a => a.Balance)
                      .HasColumnType("decimal(18,2)");

                // Relación con AccountType
                entity.HasOne(a => a.AccountType)
                      .WithMany() // Asume que no hay navegación inversa desde AccountType a AccountModel
                      .HasForeignKey(a => a.AccTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con User
                entity.HasOne(a => a.User)
                      .WithMany() // Asume que no hay navegación inversa desde User a AccountModel
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con TypeCurrency
                entity.HasOne(a => a.TypeCurrency)
                      .WithMany() // Asume que no hay navegación inversa desde TypeCurrency a AccountModel
                      .HasForeignKey(a => a.CurrencyId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con StateAccount
                entity.HasOne(a => a.StateAccount)
                      .WithMany() // Asume que no hay navegación inversa desde StateAccount a AccountModel
                      .HasForeignKey(a => a.StateId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);

            /*base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("Transfer");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                entity.HasOne(t => t.SrcAccount)
                      .WithMany()
                      .HasForeignKey(t => t.SrcAccountId);
                entity.HasOne(t => t.DestAccount)
                      .WithMany()
                      .HasForeignKey(t => t.DestAccountId);
                entity.HasOne(t => t.TransferType)
                      .WithMany(tt => tt.Transfers)                      
                      .HasForeignKey(t => t.TypeId);
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TransferType>(entity =>
            {
                entity.ToTable("TransferType");
                entity.HasKey(tt => tt.Id);
            });
            */

            modelBuilder.ApplyConfiguration(new TransferTypeConfiguration());
        }
    }
}
