using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDesktop.Models;
public class Catalogo
{
    public long IdDenuncia
    {
        get; set;
    }

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
}
