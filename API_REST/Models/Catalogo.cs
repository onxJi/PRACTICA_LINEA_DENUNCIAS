using System;
using System.Collections.Generic;

namespace API_REST.Models;

public partial class Catalogo
{
    public long IdDenuncia { get; set; }

    public string? Empresa { get; set; }

    public string? Pais { get; set; }

    public string? Estado { get; set; }

    public int? NumCentro { get; set; }
}
