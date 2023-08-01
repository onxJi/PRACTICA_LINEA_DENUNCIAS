using ApplicationDesktop.Repositories;
using Prism.Mvvm;

namespace ApplicationDesktop.ViewModels;
public class MainWindowViewModel : BindableBase
{
    private string _title = "Prism Application";
    private readonly IApiService _apiService;
    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            SetProperty(ref _title, value);
        }
    }

    public MainWindowViewModel()
    {
        
    }





}
