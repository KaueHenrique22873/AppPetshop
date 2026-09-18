using System;
using System.Collections.Generic;

namespace AppPetshop.Models;

public partial class Pet
{
    public int Codigo { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Tutor> Tutors { get; set; } = new List<Tutor>();
}
