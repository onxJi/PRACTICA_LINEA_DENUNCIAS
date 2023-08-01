using System;
using System.Collections.Generic;

namespace API_REST.Models;

public partial class UserAdmin
{
    public long IdUsuario { get; set; }

    public string Usuario { get; set; } = null!;

    public string Pass { get; set; } = null!;
}
