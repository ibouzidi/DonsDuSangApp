using System;
using System.Collections.Generic;

namespace DonsDuSangApp.Context.Models;

public partial class Questionnaire
{
    public short IdQuestionnaire { get; set; }

    public DateTime? DateCreation { get; set; }

    public bool? AccordEnseignement { get; set; }

    public bool? AccordNonTherapeutique { get; set; }

    public short IdDonneur { get; set; }

    public bool? BesoinEntretient { get; set; }

    public virtual Donneur IdDonneurNavigation { get; set; } = null!;
}
