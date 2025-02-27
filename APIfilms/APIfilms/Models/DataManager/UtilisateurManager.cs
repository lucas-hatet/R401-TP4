using System.Runtime.CompilerServices;
using APIfilms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UtilisateurManager: IDataRepository<Utilisateur>
{
    readonly FilmRatingsDBContext? filmsDbContext;
    public UtilisateurManager() { }
    public UtilisateurManager(FilmRatingsDBContext context)
    {
        filmsDbContext = context;
    }
    public ActionResult<IEnumerable<Utilisateur>> GetAllAsync()
    {
        return filmsDbContext.Utilisateurs.ToList();
    }
    public ActionResult<Utilisateur> GetByIdAsync(int id)
    {
        return filmsDbContext.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == id);
    }
    public async Task<ActionResult<Utilisateur>> GetByStringAsync(string mail)
    {
        return await filmsDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
    }
    public async Task AddAsync(Utilisateur entity)
    {
        await filmsDbContext.Utilisateurs.AddAsync(entity);
        await filmsDbContext.SaveChangesAsync();
    }
    public async Task Update(Utilisateur utilisateur, Utilisateur entity)
    {
        filmsDbContext.Entry(utilisateur).State = EntityState.Modified;
        utilisateur.UtilisateurId = entity.UtilisateurId;
        utilisateur.Nom = entity.Nom;
        utilisateur.Prenom = entity.Prenom;
        utilisateur.Mail = entity.Mail;
        utilisateur.Rue = entity.Rue;
        utilisateur.CodePostal = entity.CodePostal;
        utilisateur.Ville = entity.Ville;
        utilisateur.Pays = entity.Pays;
        utilisateur.Latitude = entity.Latitude;
        utilisateur.Longitude = entity.Longitude;
        utilisateur.Pwd = entity.Pwd;
        utilisateur.Mobile = entity.Mobile;
        utilisateur.NotesUtilisateur = entity.NotesUtilisateur;
        await filmsDbContext.SaveChangesAsync();
    }
    public async Task Delete(Utilisateur entity)
    {
        filmsDbContext.Utilisateurs.Remove(entity);
        await filmsDbContext.SaveChangesAsync();
    }
}