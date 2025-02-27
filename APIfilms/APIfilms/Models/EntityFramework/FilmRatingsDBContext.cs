using Microsoft.EntityFrameworkCore;

namespace APIfilms.Models
{
    public class FilmRatingsDBContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Notation> Notations { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public FilmRatingsDBContext(DbContextOptions<FilmRatingsDBContext> options): base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=DBfilms;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Film>()
                .HasIndex(f => f.Titre)
                .HasName("IX_flm_titre");

            modelBuilder.Entity<Utilisateur>()
                .HasIndex(u => u.Mail).IsUnique()
                .HasName("IX_utl_mail");

            // Configuration des clés étrangères
            modelBuilder.Entity<Notation>()
                .HasKey(n => new { n.UtilisateurId, n.FilmId });

            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.Pays)
                .HasDefaultValue("France");

            modelBuilder.Entity<Utilisateur>()
                .Property(e => e.DateCreation)
                .HasDefaultValueSql("Now()");

            modelBuilder.Entity<Notation>()
                .HasOne(n => n.UtilisateurNotant)
                .WithMany(u => u.NotesUtilisateur)
                .HasForeignKey(n => n.UtilisateurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notation>()
                .HasOne(n => n.FilmNote)
                .WithMany(f => f.NotesFilm)
                .HasForeignKey(n => n.FilmId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}