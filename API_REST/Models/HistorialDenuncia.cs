using System;
using System.Collections.Generic;

namespace API_REST.Models;

public partial class HistorialDenuncia
{
    public long IdHistorial { get; set; }

    public long IdDenuncia { get; set; }

    public long IdDenunciante { get; set; }

    public string? Historial { get; set; }
}
