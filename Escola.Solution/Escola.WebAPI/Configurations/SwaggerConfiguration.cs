using Microsoft.OpenApi.Models;

namespace Escola.WebAPI.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Projeto Gestão Escolar - Documentação Web API",
                    Description = "Projeto fictício para gestão de escola com o objetivo de explorar as tecnologias das quais eu possuo domínio.",
                    Version = "v1",
                    Contact = new OpenApiContact() { Name = "Levi Natanael", Email = "levi.natanael@gmail.com" },
                    License = new OpenApiLicense() { Name = "LinkedIn", Url = new Uri("https://www.linkedin.com/in/levinatanael/") }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no campo 'Authorization'. Exemplo: Bearer {seu_token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
