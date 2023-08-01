using System;
using System.Collections.Generic;

namespace API_REST.Models;

public partial class Denuncia
{
    public long IdDenuncia { get; set; }

    public long IdDenunciante { get; set; }

    public string TituloDenuncia { get; set; } = null!;

    public string? Empresa { get; set; }

    public string? Pais { get; set; }

    public string? Estado { get; set; }

    public int? NumCentro { get; set; }

    public long Folio { get; set; }

    public string PasswordDenuncia { get; set; } = null!;

    public string? DetalleDenuncia { get; set; }

    public string? Estatus { get; set; }

    public DateTime FechaDenuncia { get; set; }
}
