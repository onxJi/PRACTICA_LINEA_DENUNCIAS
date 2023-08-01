using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ApplicationDesktop.Modules.ViewModels;
public class HomeViewModel: BindableBase
{
    private IRegionManager _regionManager;
    public DelegateCommand<string> NavigateLoginAdmin {get;}
    public DelegateCommand<string> LoginClienteCmd
    {
        get;
    }
    public DelegateCommand<string> NavigateNewDenuncia
    {
        get;
    }
    public HomeViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        NavigateLoginAdmin = new DelegateCommand<string>(ExecuteLoginA);
        NavigateNewDenuncia = new DelegateCommand<string>(ExecuteNavigateNewDenuncia);
        LoginClienteCmd = new DelegateCommand<string>(ExecuteLoginClienteCmd);
    }

    private void ExecuteLoginClienteCmd(string obj)
    {
        _regionManager.RequestNavigate("ContentRegion", obj);
    }

    private void ExecuteNavigateNewDenuncia(string obj)
    {
        if (obj != null)
            _regionManager.RequestNavigate("ContentRegion", obj);
    }

    private void ExecuteLoginA(string obj)
    {
        if (obj != null)
            _regionManager.RequestNavigate("ContentRegion", obj);
    }
}
