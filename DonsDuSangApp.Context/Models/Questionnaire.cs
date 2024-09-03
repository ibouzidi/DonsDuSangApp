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

    public virtual Donneur IdDonneurNavigation { get; set; } = null!;


    // Ajoutez cette méthode pour vérifier si un questionnaire est terminé
    public bool EstTermine()
    {
        // Le questionnaire est considéré comme terminé si les deux propriétés sont renseignées
        // et qu'il y a au moins une réponse associée.
        return AccordEnseignement.HasValue && AccordNonTherapeutique.HasValue;
    }


}
