using System;
using System.Collections.Generic;

namespace DonsDuSangApp.Context.Models;

public partial class Donneur
{
    public short IdDonneur { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public DateOnly? DateNaissance { get; set; }

    public string? Email { get; set; }

    public string? Motdepasse { get; set; }

    public virtual ICollection<Questionnaire> Questionnaires { get; set; } = new List<Questionnaire>();

    public virtual ICollection<Reponse> Reponses { get; set; } = new List<Reponse>();

}
