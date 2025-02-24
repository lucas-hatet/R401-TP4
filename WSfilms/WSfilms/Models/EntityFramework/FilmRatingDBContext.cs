using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class ApplicationDbContext : DbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<Notation> Notations { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=DBfilms;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        // Configuration de l'index sur flm_titre
        modelBuilder.Entity<Film>()
            .HasIndex(f => f.Titre)
            .HasName("IX_flm_titre"); // Définir le nom de l'index si nécessaire

        // Configuration des clés étrangères
        modelBuilder.Entity<Notation>()
            .HasKey(n => new { n.UtilisateurId, n.FilmId });

        modelBuilder.Entity<Notation>()
            .HasOne(n => n.UtilisateurNotant)
            .WithMany(u => u.NotesUtilisateur)
            .HasForeignKey(n => n.UtilisateurId)
            .OnDelete(DeleteBehavior.Restrict); // Suppression restreinte

        modelBuilder.Entity<Notation>()
            .HasOne(n => n.FilmNote)
            .WithMany(f => f.NotesFilm)
            .HasForeignKey(n => n.FilmId)
            .OnDelete(DeleteBehavior.Restrict); // Suppression restreinte
    }
}
