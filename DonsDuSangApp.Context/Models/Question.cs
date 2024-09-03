using System;
using System.Collections.Generic;

namespace DonsDuSangApp.Context.Models;

public partial class Question
{
    public short IdQuestion { get; set; }

    public byte? Numero { get; set; }

    public string? Enonce { get; set; }

    public string? Categorie { get; set; }

    public bool? EstCritique { get; set; }

    public virtual ICollection<Reponse> Reponses { get; set; } = new List<Reponse>();
}
