using System.Reflection;
using Microsoft.OpenApi.Models;

namespace InstaManager.ModuleApplication.SwaggerApp;

public static class SwaggerService
{
    public static void AddSwaggerService(this IServiceCollection service)
    {
        service.AddSwaggerGen(c =>
        {
            
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            
      
            // Add security definition for JWT
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            
           
            // Add security requirement for JWT
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        });

    }
}