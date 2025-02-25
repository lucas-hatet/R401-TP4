using Microsoft.EntityFrameworkCore;
using APIfilms.Models;

namespace APIfilms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ajouter les services n�cessaires � Swagger
            builder.Services.AddSwaggerGen();  // Ajouter cette ligne

            // Configuration de la base de donn�es
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();

            var app = builder.Build();

            // Utiliser Swagger uniquement en mode d�veloppement
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();  // Utiliser Swagger
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = "swagger";  // Route pour Swagger UI
                });
            }

            app.MapControllers();

            app.Run();
        }
    }
}
