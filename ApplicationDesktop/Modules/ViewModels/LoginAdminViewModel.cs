using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDesktop.Repositories;
using Microsoft.VisualBasic.ApplicationServices;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace ApplicationDesktop.Modules.ViewModels;
public class LoginAdminViewModel: BindableBase
{
    private string _userName;
    private string _password;
    private string _errorMessage;
    private ApiService apiService;
    private IRegionManager _regionManager;
    public string UserName
    {
        get => _userName;
        set
        {
            SetProperty(ref _userName, value);
            LoginCommand.RaiseCanExecuteChanged();
        }
    }
    public string Password
    {
        get => _password;
        set
        {
            SetProperty(ref _password, value);
            LoginCommand.RaiseCanExecuteChanged();
        }
    }

    public DelegateCommand<string> LoginCommand
    {
    get; set; }
    public DelegateCommand<string> NavigateHome
    {
        get; set;
    }
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public LoginAdminViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        apiService = new ApiService();
        LoginCommand = new DelegateCommand<string>(ExecuteLoginCommand,CanExecuteLoginCommand);
        NavigateHome = new DelegateCommand<string>(ExecuteNavigateHome);
    }

    private void ExecuteNavigateHome(string obj)
    {
        if (obj != null) _regionManager.RequestNavigate("ContentRegion", obj);
    }

    private bool CanExecuteLoginCommand(string obj)
    {
        bool validData;
        if (!string.IsNullOrWhiteSpace(UserName)&&
                !string.IsNullOrWhiteSpace(Password))
            validData = true;
        else validData = false;
        return validData;
    }

    private async void ExecuteLoginCommand(string obj)
    {
       
        bool isValidLogin = await apiService.CheckLogin(UserName,Password);
        if (isValidLogin)
        {
            _regionManager.RequestNavigate("ContentRegion", obj);
        }
        else
        {
            ErrorMessage = "Username or Password incorrects";
            await Task.Delay(1000);
            ErrorMessage = "";
        }
    }
}
