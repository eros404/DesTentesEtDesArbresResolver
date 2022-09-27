using DesTentesEtDesArbres.Core;
using Microsoft.EntityFrameworkCore;

namespace DesTentesEtDesArbres.ConsoleApp.Data
{
    public class DesTentesEtDesArbresContext : DbContext
    {
        public DbSet<LevelDefinition> LevelDefinitions { get; set; } = default!;
        public string DbPath { get; }

        public DesTentesEtDesArbresContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "DesTentesEtDesArbres.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LevelDefinition>()
                .HasKey(ld => new { ld.Height, ld.Width, ld.Letter, ld.Number });
        }
    }
}
