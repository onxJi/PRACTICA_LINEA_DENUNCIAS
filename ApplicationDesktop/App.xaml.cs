using System.Windows;
using ApplicationDesktop.Modules.Views;
using ApplicationDesktop.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;

namespace ApplicationDesktop;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App: PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
        containerRegistry.RegisterForNavigation<Denuncias>();
    }
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog.AddModule<Modules.ModuleRegion>();
       
        //moduleCatalog.AddModule<WpfApp1.ModuleCModule>();
    }
}
