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
    public async Task<ActionResult<IEnumerable<Utilisateur>>> GetAllAsync()
    {
        return await filmsDbContext.Utilisateurs.ToListAsync();
    }


    public async Task<ActionResult<Utilisateur>> GetByIdAsync(int id)
    {
        var utilisateur = await filmsDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.UtilisateurId == id);
        return utilisateur == null ? new NotFoundResult() : new ActionResult<Utilisateur>(utilisateur);
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
    public async Task UpdateAsync(Utilisateur utilisateur, Utilisateur entity)
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

    public async Task DeleteAsync(Utilisateur entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "L'utilisateur � supprimer ne peut pas �tre null.");
        }

        filmsDbContext.Utilisateurs.Remove(entity);
        await filmsDbContext.SaveChangesAsync();
    }

}