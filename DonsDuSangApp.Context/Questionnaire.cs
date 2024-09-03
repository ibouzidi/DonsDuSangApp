using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonsDuSangApp.Context.Models;

    public partial class Questionnaire
    {
        // Ajoutez cette méthode pour vérifier si un questionnaire est terminé
        public bool EstTermine()
        {
        // Le questionnaire est considéré comme terminé si les deux propriétés sont renseignées
        // et qu'il y a au moins une réponse associée.
        return AccordEnseignement.HasValue && AccordNonTherapeutique.HasValue;
        }

}

