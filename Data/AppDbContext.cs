using Microsoft.EntityFrameworkCore;
using CarePlusApi.Models;

namespace CarePlusApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<UserChallenge> UserChallenges { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<RewardClaim> RewardClaims { get; set; }
        public DbSet<WearableConnection> WearableConnections { get; set; }
        public DbSet<StepLog> StepLogs { get; set; }
        public DbSet<RankingSnapshot> RankingSnapshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de Chave Única para Email
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configuração 1:1 para WearableConnection
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.WearableConnection)
                .WithOne(wc => wc.Usuario)
                .HasForeignKey<WearableConnection>(wc => wc.UserId);

            // Mapeamento do Enum ChallengeStatus
            modelBuilder.Entity<Challenge>()
                .Property(c => c.Status)
                .HasConversion<int>(); // Armazena o enum como inteiro

            // Exclusão Lógica (IsDeleted)
            modelBuilder.Entity<Challenge>()
                .HasQueryFilter(c => !c.IsDeleted); // Filtra desafios deletados por padrão

            // Configuração para StepLog (DateOnly)
            modelBuilder.Entity<StepLog>()
                .Property(s => s.LogDate)
                .HasColumnType("date"); // Mapeia DateOnly para o tipo date no SQL Server

            // Configuração para RankingSnapshot (DateOnly)
            modelBuilder.Entity<RankingSnapshot>()
                .Property(rs => rs.SnapshotDate)
                .HasColumnType("date"); // Mapeia DateOnly para o tipo date no SQL Server

            modelBuilder.Seed(); // Chamada para o Seeder
            base.OnModelCreating(modelBuilder);
        }
    }
}
