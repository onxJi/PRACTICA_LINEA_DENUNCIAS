using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDesktop.Models;
public class HistorialDenuncia
{
    public long IdHistorial
    {
        get; set;
    }

    public long IdDenuncia
    {
        get; set;
    }

    public long IdDenunciante
    {
        get; set;
    }

    public string? Historial
    {
        get; set;
    }
}
