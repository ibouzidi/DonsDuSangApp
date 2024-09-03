using System;
using System.Collections.Generic;

namespace DonsDuSangApp.Context.Models;

public partial class Reponse
{
    public short IdReponse { get; set; }

    public string? Reponse1 { get; set; }

    public string? ComplementTextuel { get; set; }

    public short IdDonneur { get; set; }

    public short IdQuestion { get; set; }

    public virtual Donneur IdDonneurNavigation { get; set; } = null!;

    public virtual Question IdQuestionNavigation { get; set; } = null!;
}
