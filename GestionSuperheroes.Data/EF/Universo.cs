using System;
using System.Collections.Generic;

namespace GestionSuperheroes.Data.EF;

public partial class Universo
{
    public int IdUniverso { get; set; }

    public string NombreUniverso { get; set; } = null!;

    public virtual ICollection<Superheroe> Superheroes { get; set; } = new List<Superheroe>();
}
