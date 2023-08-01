using System;
using System.Collections.Generic;

namespace API_REST.Models;

public partial class DatosPersonal
{
    public long IdDenuncia { get; set; }

    public long IdDenunciante { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }
}
