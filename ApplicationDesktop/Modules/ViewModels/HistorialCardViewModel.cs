using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ApplicationDesktop.Modules.ViewModels;
public class HistorialCardViewModel: BindableBase
{
    private string _tbHistorial;

    public string TbHistorial
    {
        get => _tbHistorial;
        set => SetProperty(ref _tbHistorial, value);
    }

    public HistorialCardViewModel()
    {
    }
}
