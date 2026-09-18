using System;
using System.Collections.Generic;

namespace AppPetshop.Models;

public partial class Agendamento
{
    public int Codigo { get; set; }

    public TimeOnly Datahora { get; set; }

    public int IdTutor { get; set; }

    public int IdServico { get; set; }

    public virtual Servico IdServicoNavigation { get; set; } = null!;

    public virtual Tutor IdTutorNavigation { get; set; } = null!;
}
