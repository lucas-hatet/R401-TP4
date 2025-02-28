using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmRatingsApp.Models
{
    [Table("t_e_utilisateur_utl")]
    public class Utilisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("utl_id")]
        public int UtilisateurId { get; set; }

        [StringLength(50)]
        [Column("utl_nom")]
        public string Nom { get; set; }

        [StringLength(50)]
        [Column("utl_prenom")]
        public string Prenom { get; set; }

        [Column("utl_mobile", TypeName = "char(10)")]
        public string Mobile { get; set; }

        [Required]
        [EmailAddress]
        [Column("utl_mail", TypeName = "varchar(100)")]
        public string Mail { get; set; }

        [Required]
        [StringLength(64)]
        [Column("utl_pwd")]
        public string Pwd { get; set; }

        [StringLength(200)]
        [Column("utl_rue")]
        public string Rue { get; set; }

        [StringLength(5)]
        [Column("utl_cp")]
        public string CodePostal { get; set; }

        [StringLength(50)]
        [Column("utl_ville")]
        public string Ville { get; set; }

        [StringLength(50)]
        [Column("utl_pays")]
        public string Pays { get; set; }

        [Column("utl_latitude")]
        public float? Latitude { get; set; }

        [Column("utl_longitude")]
        public float? Longitude { get; set; }

        [Required]
        [Column("utl_datecreation")]
        public DateTime DateCreation { get; set; }

        public ICollection<Notation> NotesUtilisateur { get; set; }
    }
}