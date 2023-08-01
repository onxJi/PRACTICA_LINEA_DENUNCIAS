using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDesktop.Models;
public class Denuncia
{
    public long IdDenuncia
    {
        get; set;
    }

    public long IdDenunciante
    {
        get; set;
    }

    public string TituloDenuncia { get; set; } = null!;

    public string? Empresa
    {
        get; set;
    }

    public string? Pais
    {
        get; set;
    }

    public string? Estado
    {
        get; set;
    }

    public int? NumCentro
    {
        get; set;
    }

    public long Folio
    {
        get; set;
    }

    public string? PasswordDenuncia
    {
        get; set;
    }

    public string DetalleDenuncia { get; set; } = null!;

    public string? Estatus
    {
        get; set;
    }

    public DateTime FechaDenuncia
    {
        get; set;
    }
}
