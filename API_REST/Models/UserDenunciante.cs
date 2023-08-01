using System;
using System.Collections.Generic;

namespace API_REST.Models;

public partial class UserDenunciante
{
    public long IdDenunciante { get; set; }

    public long Folio { get; set; }

    public string PasswordDenuncia { get; set; } = null!;
}
