using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonsDuSangApp.Context.Models;

public partial class Donneur
{
    public string FullName => $"{Prenom} {Nom}";
}

