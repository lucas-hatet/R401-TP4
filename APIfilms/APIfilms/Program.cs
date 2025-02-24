using Microsoft.EntityFrameworkCore;
using APIfilms.Models;

namespace APIfilms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Remplacer ApplicationDbContext par FilmratingDbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();

            app.Run();

        }
    }
}
