using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDesktop.Modules.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ApplicationDesktop.Modules;
public class ModuleRegion : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        var regionManager = containerProvider.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion("ContentRegion", typeof(Home));
        
    }
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<Home>();
        containerRegistry.RegisterForNavigation<LoginAdmin>();
        containerRegistry.RegisterForNavigation<NewDenuncia>();
        containerRegistry.RegisterForNavigation<Denuncias>();
        containerRegistry.RegisterForNavigation<DetalleDenuncia>();
        containerRegistry.RegisterForNavigation<DatosPersonales>();
        containerRegistry.RegisterForNavigation<LoginCliente>();
        containerRegistry.RegisterForNavigation<ListaDenuncias>();
        containerRegistry.RegisterForNavigation<DetalleDenunciaAdmin>();
    }
}
