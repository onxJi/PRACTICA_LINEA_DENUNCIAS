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
public class DenunciasViewModel : BindableBase
{
    private readonly IEventAggregator _ea;
    private readonly IRegionManager _regionManager;
    private readonly ApiService _apiService;
    private string _tbIdDenuncia, _tbIdDenunciante, _tbNombreCompleto,
        _tbCorreo, _tbTelefono, _tbEmpresa, _tbPais, _tbEstado, _tbNumCentro;
    private string _errorMessage, _errorMessage2,_tbDetalleDenuncia,_tbTitulo;
    private DateTime _fecha = DateTime.Now;
    private string _newPassword, _confirmPassword;

    public string TbIdDenuncia
    {
        get => _tbIdDenuncia;
        set => SetProperty(ref  _tbIdDenuncia, value);
    }
    public string TbIdDenunciante
    {
        get => _tbIdDenunciante;
        set => SetProperty(ref _tbIdDenunciante, value);
    }
    public string TbNombreCompleto
    {
        get => _tbNombreCompleto;
        set => SetProperty(ref _tbNombreCompleto, value);
    }
    public string TbCorreo
    {
        get => _tbCorreo;
        set => SetProperty(ref _tbCorreo, value);
    }
    public string TbTelefono
    {
        get => _tbTelefono;
        set => SetProperty(ref _tbTelefono, value);
    }
    public string TbEmpresa
    {
        get => _tbEmpresa;
        set => SetProperty(ref _tbEmpresa, value);
    }
    public string TbPais
    {
        get => _tbPais;
        set => SetProperty(ref _tbPais, value);
    }
    public string TbEstado
    {
        get => _tbEstado;
        set => SetProperty(ref _tbEstado, value);
    }
    public string TbNumCentro
    {
        get => _tbNumCentro;
        set => SetProperty(ref _tbNumCentro, value);
    }
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }
    public string ErrorMessage2
    {
        get => _errorMessage2;
        set => SetProperty(ref _errorMessage2, value);
    }
    public DateTime Fecha
    {
        get => _fecha;
        set => SetProperty(ref _fecha, value);
    }
    public string TbDetalleDenuncia
    {
        get => _tbDetalleDenuncia;
        set => SetProperty(ref _tbDetalleDenuncia, value);   
    }
    public string NewPassword
    {
        get => _newPassword;
        set
        {
            SetProperty(ref _newPassword, value);
            NavigateDetalleDenuncia.RaiseCanExecuteChanged();
            CaracteresMinimos(NewPassword);
        }
    }
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            SetProperty(ref _confirmPassword, value);
            NavigateDetalleDenuncia.RaiseCanExecuteChanged();
        }
    }

    public string TbTitulo
    {
        get => _tbTitulo;
        set => SetProperty(ref _tbTitulo, value);
    }


    public DelegateCommand<string> NavigateDetalleDenuncia {get;}
    public DenunciasViewModel(IEventAggregator ea, IRegionManager regionManager)
    {
        _ea = ea;
        _regionManager = regionManager;
        _apiService = new ApiService();
        _ea.GetEvent<MessageSentModel>().Subscribe(CatalogoModelReceived);
        _ea.GetEvent<MessageSentDatosModel>().Subscribe(DatosPersonalesReceived);
        NavigateDetalleDenuncia = new DelegateCommand<string>(ExecuteNavigateDetalleDenuncia, CanExecuteNavigateDetalleDenuncia);
    }
    private void CaracteresMinimos(string pass)
    {
        if (pass.Length < 8) 
            ErrorMessage = $"Longitud minima 8 caracteres: {8-pass.Length} caracteres faltantes";
        else {
            ErrorMessage = $"Minimo de caracteres suplido";
        }
    }
    private bool CanExecuteNavigateDetalleDenuncia(string arg)
    {
        bool result = false;
        if (NewPassword == ConfirmPassword &&
            !string.IsNullOrWhiteSpace(NewPassword) &&
            !string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            result = true;
            ErrorMessage2 = "Las contraseñas SI coinciden";

        }
        else
        {
            result = false;
            ErrorMessage2 = "Las contraseñas no coinciden";

        }
        return result;
    }

    private async void ExecuteNavigateDetalleDenuncia(string obj)
    {
        Random random = new Random();
        long _ramdomCode = (random.Next(99999));
        Denuncia denuncia = new Denuncia();
        var consult = await _apiService.GetDatosPersonal(Convert.ToInt64(TbIdDenuncia));
        denuncia.IdDenuncia = Convert.ToInt64(TbIdDenuncia);
        denuncia.IdDenunciante = consult.IdDenunciante;
        denuncia.TituloDenuncia = TbTitulo;
        denuncia.Empresa = TbEmpresa;
        denuncia.Pais = TbPais;
        denuncia.Estado = TbEstado;
        denuncia.NumCentro = Convert.ToInt32(TbNumCentro);
        denuncia.Folio = _ramdomCode;
        denuncia.PasswordDenuncia = NewPassword;
        denuncia.DetalleDenuncia = TbDetalleDenuncia;
        denuncia.Estatus = "Iniciado";
        denuncia.FechaDenuncia = Fecha;
        HistorialDenuncia historial = new HistorialDenuncia();
        historial.IdDenuncia = denuncia.IdDenuncia;
        historial.IdDenunciante = denuncia.IdDenunciante;
        historial.Historial = "Inicio de Proceso";
        UserDenunciante userDenunciante = new UserDenunciante();
        userDenunciante.Folio = denuncia.Folio;
        userDenunciante.PasswordDenuncia = denuncia.PasswordDenuncia;
        var resultDatos = await _apiService.PostDenuncia(denuncia);
        await _apiService.PostHistorialDenuncia(historial);
        await _apiService.PostUserDenunciate(userDenunciante);

        DatosPersonal datos = new DatosPersonal();
        datos.Nombre = TbNombreCompleto;
        datos.Correo = TbCorreo;
        datos.Telefono = TbTelefono;
        if (resultDatos)
        {

            ErrorMessage = $"Se ha registrado la denuncia";
            await Task.Delay(2000);
            ErrorMessage = "";
            _regionManager.RequestNavigate("ContentRegion", obj);
            _ea.GetEvent<MessageSentDenuncia>().Publish(denuncia);
            _ea.GetEvent<MessageSentDatosModel>().Publish(datos);
        }
    }

    private void DatosPersonalesReceived(DatosPersonal personal)
    {

        TbNombreCompleto = personal.Nombre.ToString();
        TbCorreo = personal.Correo.ToString();
        TbTelefono = personal.Telefono.ToString();
    }

    private async void CatalogoModelReceived( Catalogo catalogo)
    {
        var consult = await _apiService.GetDatosPersonal(catalogo.IdDenuncia);
        TbIdDenunciante = consult.IdDenunciante.ToString();
        TbIdDenuncia = catalogo.IdDenuncia.ToString();
        TbEmpresa = catalogo.Empresa;
        TbPais = catalogo.Pais;
        TbEstado = catalogo.Estado;
        TbNumCentro = catalogo.NumCentro.ToString();
    }


}
