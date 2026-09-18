using System;
using System.Collections.Generic;

namespace AppPetshop.Models;

public partial class Tutor
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public int Idpet { get; set; }

    public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

    public virtual Pet? IdpetNavigation { get; set; } 
}
