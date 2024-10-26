using Escola.Application.Interfaces;
using Escola.Application.Services;
using Escola.Domain.Interfaces.Repositories;
using Escola.Infra.Data.Context;
using Escola.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
        License = new OpenApiLicense() { Name = "MIT License", Url = new Uri("https://opensource.org/licenses/MIT") }
    });
});

builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
