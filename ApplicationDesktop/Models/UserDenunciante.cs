using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDesktop.Models;
public class UserDenunciante
{
    public long IdDenunciante
    {
        get; set;
    }

    public long Folio
    {
        get; set;
    }

    public string PasswordDenuncia { get; set; } = null!;
}
