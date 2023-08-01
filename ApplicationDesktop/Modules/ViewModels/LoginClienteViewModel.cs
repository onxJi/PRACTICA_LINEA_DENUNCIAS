using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ApplicationDesktop.Models;
using ApplicationDesktop.Repositories;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace ApplicationDesktop.Modules.ViewModels;
public class LoginClienteViewModel: BindableBase
{
    private string _userName;
    private string _password;
    private string _errorMessage;
    private ApiService _apiService;
    private IRegionManager _regionManager;
    private IEventAggregator _ea;
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
        get; set;
    }
    public DelegateCommand<string> NavigateHome
    {
        get; set;
    }
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }
    

    public LoginClienteViewModel(IRegionManager regionManager, IEventAggregator ea)
    {
        _regionManager = regionManager;
        _ea = ea;
        _apiService = new ApiService();
        NavigateHome = new DelegateCommand<string>(ExecuteNavigateHome);
        LoginCommand = new DelegateCommand<string>(ExecuteLoginCommand, CanExecuteLoginCommand);
    }

    private bool CanExecuteLoginCommand(string arg)
    {
        bool validData;
        if (!string.IsNullOrWhiteSpace(UserName) &&
                !string.IsNullOrWhiteSpace(Password))
            validData = true;
        else validData = false;
        return validData;
    }
    private async void ExecuteLoginCommand(string obj)
    {
        var isValidUser = await _apiService.CheckLoginCliente(Convert.ToInt32(UserName), Password);
        if (isValidUser)
        {
            var getDenuncia = await _apiService.GetDenunciaByFolio(Convert.ToInt32(UserName));
            if (getDenuncia != null)
            {
                Denuncia objDenuncia = new Denuncia();
                objDenuncia.IdDenuncia = getDenuncia.IdDenuncia;
                objDenuncia.IdDenunciante = getDenuncia.IdDenunciante;
                objDenuncia.TituloDenuncia = getDenuncia.TituloDenuncia;
                objDenuncia.Empresa = getDenuncia.Empresa;
                objDenuncia.Pais = getDenuncia.Pais;
                objDenuncia.Estado = getDenuncia.Estado;
                objDenuncia.NumCentro = getDenuncia.NumCentro;
                objDenuncia.Folio = getDenuncia.Folio;
                objDenuncia.DetalleDenuncia = getDenuncia.DetalleDenuncia;
                objDenuncia.Estatus = getDenuncia.Estatus;
                objDenuncia.FechaDenuncia = getDenuncia.FechaDenuncia;
                var getDatos = await _apiService.GetDatosPersonal(getDenuncia.IdDenuncia);
                DatosPersonal datosPersonal = new DatosPersonal();
                datosPersonal.Nombre = getDatos.Nombre;
                datosPersonal.Correo = getDatos.Correo; 
                datosPersonal.Telefono = getDatos.Telefono;
                _regionManager.RequestNavigate("ContentRegion", obj);
                _ea.GetEvent<MessageSentDenuncia>().Publish(objDenuncia);
                _ea.GetEvent<MessageSentDatosModel>().Publish(datosPersonal);

            }
        }
    }

    private void ExecuteNavigateHome(string obj)
    {
        if (obj != null) _regionManager.RequestNavigate("ContentRegion", obj);
    }


}
