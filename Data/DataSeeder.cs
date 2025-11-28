using CarePlusApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarePlusApi.Data
{
    public static class DataSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Datas fixas
            var now = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            // -----------------------------
            // 1. Usuários (GUIDs fixos)
            // -----------------------------
            var user1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var user2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var user3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = user1Id, Nome = "Alice Silva", Email = "alice.silva@careplus.com", Pontos = 1500, PasswordHash = "hashed_password_1", CreatedAt = now, UpdatedAt = now },
                new Usuario { Id = user2Id, Nome = "Bruno Costa", Email = "bruno.costa@careplus.com", Pontos = 2500, PasswordHash = "hashed_password_2", CreatedAt = now, UpdatedAt = now },
                new Usuario { Id = user3Id, Nome = "Carla Souza", Email = "carla.souza@careplus.com", Pontos = 800, PasswordHash = "hashed_password_3", CreatedAt = now, UpdatedAt = now }
            );

            // -----------------------------
            // 2. Desafios (GUIDs fixos)
            // -----------------------------
            var challenge1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var challenge2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var challenge3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<Challenge>().HasData(
                new Challenge
                {
                    Id = challenge1Id,
                    Titulo = "Maratona de 10k Passos",
                    Descricao = "Complete 10.000 passos em um único dia.",
                    Icon = "icon_foot",
                    RequiredValue = 10000,
                    RewardPoints = 500,
                    Category = "CARDIO",
                    IsDaily = true,
                    DataLimitePontuacao = now.AddDays(30),
                    Status = ChallengeStatus.Go,
                    CreatedAt = now,
                    UpdatedAt = now,
                    IsDeleted = false
                },
                new Challenge
                {
                    Id = challenge2Id,
                    Titulo = "Meditação de 7 Dias",
                    Descricao = "Medite por 15 minutos todos os dias por uma semana.",
                    Icon = "icon_mind",
                    RequiredValue = 7,
                    RewardPoints = 1000,
                    Category = "MENTE",
                    IsDaily = false,
                    DataLimitePontuacao = now.AddDays(60),
                    Status = ChallengeStatus.Go,
                    CreatedAt = now,
                    UpdatedAt = now,
                    IsDeleted = false
                },
                new Challenge
                {
                    Id = challenge3Id,
                    Titulo = "Desafio de Força",
                    Descricao = "Complete 50 flexões em 3 dias.",
                    Icon = "icon_arm",
                    RequiredValue = 50,
                    RewardPoints = 750,
                    Category = "FORCA",
                    IsDaily = false,
                    DataLimitePontuacao = now.AddDays(15),
                    Status = ChallengeStatus.Waiting,
                    CreatedAt = now,
                    UpdatedAt = now,
                    IsDeleted = false
                }
            );

            // -----------------------------
            // 3. Progresso de usuário
            // -----------------------------
            var userChallenge1Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var userChallenge2Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            modelBuilder.Entity<UserChallenge>().HasData(
                new UserChallenge
                {
                    Id = userChallenge1Id,
                    UserId = user1Id,
                    ChallengeId = challenge1Id,
                    ProgressValue = 100,
                    Completed = true,
                    CompletedAt = now.AddDays(-1),
                    EarnedPoints = 500,
                    LastUpdate = now.AddDays(-1)
                },
                new UserChallenge
                {
                    Id = userChallenge2Id,
                    UserId = user2Id,
                    ChallengeId = challenge2Id,
                    ProgressValue = 50,
                    Completed = false,
                    EarnedPoints = 0,
                    LastUpdate = now
                }
            );

            // -----------------------------
            // 4. Rewards (GUID fixo)
            // -----------------------------
            var reward1Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var reward2Id = Guid.Parse("77777777-7777-7777-7777-777777777777");

            modelBuilder.Entity<Reward>().HasData(
                new Reward
                {
                    Id = reward1Id,
                    Title = "Desconto de 10% em Farmácia",
                    Description = "Desconto em qualquer farmácia credenciada.",
                    CostPoints = 1000,
                    Category = "DESCONTO",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Reward
                {
                    Id = reward2Id,
                    Title = "1 Mês de Aulas de Yoga Grátis",
                    Description = "Acesso gratuito a aulas de yoga online.",
                    CostPoints = 2000,
                    Category = "SERVICO",
                    CreatedAt = now,
                    UpdatedAt = now
                }
            );

            // -----------------------------
            // 5. RewardClaim
            // -----------------------------
            var rewardClaimId = Guid.Parse("88888888-8888-8888-8888-888888888888");

            modelBuilder.Entity<RewardClaim>().HasData(
                new RewardClaim
                {
                    Id = rewardClaimId,
                    UserId = user2Id,
                    RewardId = reward1Id,
                    PointsSpent = 1000,
                    ClaimedAt = now.AddDays(-5),
                    Status = "CLAIMED"
                }
            );
        }
    }
}
