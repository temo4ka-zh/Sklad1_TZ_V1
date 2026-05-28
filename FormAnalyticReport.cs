using Microsoft.EntityFrameworkCore;
using Sklad1.Models;

namespace Sklad1.Data
{
    /// <summary>
    /// Отдельный контекст для новых таблиц ТЗ. Он не используется во входе и регистрации,
    /// чтобы новые модули не могли сломать существующую авторизацию приложения.
    /// </summary>
    public class TzContext : DbContext
    {
        public DbSet<ContractorCheck> ContractorChecks { get; set; }
        public DbSet<BlacklistedInn> BlacklistedInns { get; set; }
        public DbSet<BlockedOperationLog> BlockedOperationLogs { get; set; }
        public DbSet<WeatherCheck> WeatherChecks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=Sklad_BD;Username=postgres;Password=milanaANDmadina");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlacklistedInn>().HasKey(b => b.Inn);

            // Новые таблицы хранят user_id как обычное поле. Навигации к users отключены,
            // чтобы при работе с проверками не перестраивалась модель основных таблиц приложения.
            modelBuilder.Entity<ContractorCheck>().Ignore(c => c.User);
            modelBuilder.Entity<BlockedOperationLog>().Ignore(b => b.User);
            modelBuilder.Entity<WeatherCheck>().Ignore(w => w.User);
        }
    }
}
