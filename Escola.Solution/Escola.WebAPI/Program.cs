using Escola.Application.Interfaces;
using Escola.Application.Services;
using Escola.Domain.Interfaces.Repositories;
using Escola.Infra.Data.Context;
using Escola.Infra.Data.Repositories;
using Escola.WebAPI.Configurations;
using Escola.WebAPI.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure JWT settings
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

// Adicione a autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true
    };
});

// Adicione o Swagger com suporte JWT
builder.Services.AddSwaggerWithJwt();

// Adicione os filtros e serviços
builder.Services.AddControllers(options =>
{
    options.Filters.Add<AuthorizationFilter>(); // Adiciona o filtro de autorização
});

builder.Services.AddControllers();

builder.Services.AddDbContext<EscolaDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Escola.Infra") // Especifica o assembly de migrações
    )
);

builder.Services.AddAutoMapper(typeof(Escola.Application.Mappings.MappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Projeto Gestão Escolar - Documentação Web API",
        Description = "Projeto fictício para gestão de escola com o objetivo de explorar as tecnlogias das quais eu possuo domínio.",
        Contact = new OpenApiContact() { Name = "Levi Natanael", Email = "levi.natanael@gmail.com" },
        License = new OpenApiLicense() { Name = "Linkedin", Url = new Uri("https://www.linkedin.com/in/levinatanael/") }
    });
});

builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // UseAuthentication antes de UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
