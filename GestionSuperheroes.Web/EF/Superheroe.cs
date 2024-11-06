using System;
using System.Collections.Generic;

namespace GestionSuperheroes.Web.EF;

public partial class Superheroe
{
    public int IdSuperheroe { get; set; }

    public string NombreSuperheroe { get; set; } = null!;

    public int IdUniverso { get; set; }

    public bool Eliminado { get; set; }

    public virtual Universo IdUniversoNavigation { get; set; } = null!;
}
