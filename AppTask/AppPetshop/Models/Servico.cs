using System;
using System.Collections.Generic;

namespace AppPetshop.Models;

public partial class Servico
{
    public int Codigo { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
}
