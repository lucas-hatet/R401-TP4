using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("t_e_film_flm")]
public class Film
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("flm_id")]
    public int FilmId { get; set; }

    [Required]
    [StringLength(100)]
    [Column("flm_titre")]
    public string Titre { get; set; }

    [Column("flm_resume")]
    public string Resume { get; set; }

    [Column("flm_datesortie")]
    public DateTime DateSortie { get; set; }

    [Column("flm_duree")]
    public decimal Duree { get; set; }

    [StringLength(30)]
    [Column("flm_genre")]
    public string Genre { get; set; }

    // Propriété de navigation vers Notation
    public ICollection<Notation> NotesFilm { get; set; }
}
