using APIfilms.Controllers;
using APIfilms.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace APIfilmsTests.Controller
{
    [TestClass()]
    public class UtilisateursTests
    {
        private UtilisateurManager usrManager;
        private UtilisateursController usrController;
        private FilmRatingsDBContext context;
        private object expected, actual;

        [TestInitialize]
        public void TestInitialize()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingsDBContext>().UseNpgsql("Host=localhost;Database=DBfilms;Username=postgres;Password=postgres");
            context = new FilmRatingsDBContext(builder.Options);
            usrManager = new UtilisateurManager(context);
            usrController = new UtilisateursController(usrManager);
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            usrController = null;
            expected = null;
            actual = null;
        }

        [TestMethod()]
        public void GetUtilisateurTest_Ok()
        {
            // Act
            var expected = context.Utilisateurs.ToList();
            var actual = usrController.GetUtilisateurs().Result.Value.ToList();
            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest_NotOk_NotFound()
        {
            // Act
            var actual = usrController.GetUtilisateurById(0).Result.Result;
            // Assert
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest_Ok()
        {
            // Act
            Utilisateur expected = context.Utilisateurs.Where(c => c.UtilisateurId == 1).FirstOrDefault();
            Utilisateur actual = usrController.GetUtilisateurById(1).Result.Value;
            // Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_NotOk_NotFound()
        {
            // Act
            var actual = usrController.GetUtilisateurByEmail("moi@gmail.com").Result.Result;
            // Assert
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest_Ok()
        {
            // Act
            Utilisateur expected = context.Utilisateurs.Where(c => c.Mail == "clilleymd@last.fm").FirstOrDefault();
            Utilisateur actual = usrController.GetUtilisateurByEmail("clilleymd@last.fm").Result.Value;
            // Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void PostUtilisateurTest_Ok()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = usrController.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
            // Assert
            Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.Mail.ToUpper() ==
            userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod()]
        public void PutUtilisateurTest_Ok()
        {
            Utilisateur userAtester = new Utilisateur()
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@gmail.com", // Modification de l'adresse mail
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = (float)46.344795,
                Longitude = (float)6.4885845,
                NotesUtilisateur = null
            };

            // Act
            var result = usrController.PutUtilisateur(1, userAtester).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout
            // Assert
            Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.UtilisateurId == userAtester.UtilisateurId).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            if (!(userRecupere is null)) userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");

            userAtester.Mail = "clilleymd@gmail.com";
            result = usrController.PutUtilisateur(1, userAtester).Result;
        }

        [TestMethod()]
        public void DeleteUtilisateurTest_Ok()
        {
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "Fétré",
                Prenom = "Malsontravail",
                Mobile = "0600000000",
                Mail = "simon.fetre@etu.univ-smb.fr",
                Pwd = "Toto12345678!",
                Rue = "Route de verossier",
                CodePostal = "74666",
                Ville = "Larringes",
                Pays = "France",
                Latitude = (float)0,
                Longitude = (float)0,
                NotesUtilisateur = null
            };

            context.Utilisateurs.Add(userAtester);

            var resultAvant = usrController.GetUtilisateurByEmail("simon.fetre@etu.univ-smb.fr");
            var utilisateurAvantSuppression = resultAvant.Result.Value;


            if (utilisateurAvantSuppression != null)
            {
                usrController.DeleteUtilisateur(utilisateurAvantSuppression.UtilisateurId);  
            }

            var resultApres = usrController.GetUtilisateurByEmail("simon.fetre@etu.univ-smb.fr");  
            var utilisateurApresSuppression = resultApres.Result.Value;
            Assert.IsNull(utilisateurApresSuppression, "L'utilisateur n'a pas été supprimé.");
        }
    }
}