using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("t_j_notation_not")]
public class Notation
{
    [Key]
    [Column("utl_id")]
    public int UtilisateurId { get; set; }

    [Key]
    [Column("flm_id")]
    public int FilmId { get; set; }

    [Required]
    [Range(0, 5)]
    [Column("not_note")]
    public int Note { get; set; }

    // Propriétés de navigation
    public Utilisateur UtilisateurNotant { get; set; }
    public Film FilmNote { get; set; }
}
