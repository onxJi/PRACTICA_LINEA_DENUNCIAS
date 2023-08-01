using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDesktop.Models;
public class DatosPersonal
{
    public long IdDenuncia
    {
        get; set;
    }

    public long IdDenunciante
    {
        get; set;
    }

    public string? Nombre
    {
        get; set;
    }

    public string? Correo
    {
        get; set;
    }

    public string? Telefono
    {
        get; set;
    }
}
