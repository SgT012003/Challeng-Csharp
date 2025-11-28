using CarePlusApi;
using CarePlusApi.AutoMapper;
using CarePlusApi.Data;
using CarePlusApi.Data.Repositories;
using CarePlusApi.Interfaces;
using CarePlusApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure(20, TimeSpan.FromSeconds(10), null)
    )
);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Care Plus API", Version = "v1" });
});

// repos
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IChallengeRepository, ChallengeRepository>();
builder.Services.AddScoped<IUserChallengeRepository, UserChallengeRepository>();
builder.Services.AddScoped<IRewardRepository, RewardRepository>();
builder.Services.AddScoped<IRewardClaimRepository, RewardClaimRepository>();

// services
builder.Services.AddScoped<IRankingService, RankingService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<IBenefitService, BenefitService>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Create all databases and apply migrations if necessary
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Database.EnsureCreated()) app.MigrateWithRetry();
}

app.UseExceptionHandlingMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();