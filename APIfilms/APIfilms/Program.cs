using Microsoft.EntityFrameworkCore;
using APIfilms.Models;

namespace APIfilms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ajouter les services nécessaires à Swagger
            builder.Services.AddSwaggerGen();  // Ajouter cette ligne

            // Configuration de la base de données
            builder.Services.AddDbContext<FilmRatingsDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();

            var app = builder.Build();

            // Utiliser Swagger uniquement en mode développement
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
