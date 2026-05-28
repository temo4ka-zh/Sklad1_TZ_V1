using Microsoft.EntityFrameworkCore;
using Sklad1.Models;

namespace Sklad1.Data
{
    /// <summary>
    /// Контекст базы данных для работы с PostgreSQL
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// Таблица пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Таблица категорий товаров
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Таблица товаров
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Таблица отгрузок
        /// </summary>
        public DbSet<Shipment> Shipments { get; set; }

        /// <summary>
        /// Таблица позиций отгрузок
        /// </summary>
        public DbSet<ShipmentItem> ShipmentItems { get; set; }

        /// <summary>
        /// Таблица партий товаров (для учёта сроков годности)
        /// </summary>
        public DbSet<ProductBatch> ProductBatches { get; set; }

        /// <summary>
        /// Таблица курсов валют
        /// </summary>
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        /// <summary>
        /// Таблица убытков (просроченные и списанные товары)
        /// </summary>
        public DbSet<Loss> Losses { get; set; }

        /// <summary>
        /// Таблица настроек приложения
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        /// <summary>
        /// Таблица поставок
        /// </summary>
        public DbSet<Supply> Supplies { get; set; }

        /// <summary>
        /// Таблица позиций поставок
        /// </summary>
        public DbSet<SupplyItem> SupplyItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=Sklad_BD;Username=postgres;Password=milanaANDmadina");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>(); 
           
            modelBuilder.Entity<SupplyItem>()
                .HasOne(si => si.Batch)
                .WithMany()
                .HasForeignKey(si => si.BatchId);

            modelBuilder.Entity<ProductBatch>()
                .HasOne(pb => pb.Supply)
                .WithMany()
                .HasForeignKey(pb => pb.SupplyId);
        }
    }
}
