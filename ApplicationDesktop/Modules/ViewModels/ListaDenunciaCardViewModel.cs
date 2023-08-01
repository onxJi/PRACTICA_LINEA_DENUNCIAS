using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDesktop.Models;
using ApplicationDesktop.Modules.Views;
using ApplicationDesktop.Repositories;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace ApplicationDesktop.Modules.ViewModels;
public class ListaDenunciaCardViewModel: BindableBase
{
    private IEventAggregator _ea;
    private readonly ApiService _apiService;
    private readonly IRegionManager _regionManager;
    private string _tbIdDenuncia, _tbIdDenunciante, _tbNombreCompleto,
    _tbCorreo, _tbTelefono, _tbEmpresa, _tbPais, _tbEstado, _tbNumCentro,
        _tbDetalleDenuncia, _tbTitulo, _tbFolio, _tbFecha, _tbEstatus;
    #region Campos
    public string TbIdDenuncia
    {
        get => _tbIdDenuncia;
        set => SetProperty(ref _tbIdDenuncia, value);
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
    public string TbDetalleDenuncia
    {
        get => _tbDetalleDenuncia;
        set => SetProperty(ref _tbDetalleDenuncia, value);
    }
    public string TbTitulo
    {
        get => _tbTitulo;
        set => SetProperty(ref _tbTitulo, value);
    }
    public string TbFolio
    {
        get => _tbFolio;
        set => SetProperty(ref _tbFolio, value);
    }
    public string TbEstatus
    {
        get => _tbEstatus;
        set => SetProperty(ref _tbEstatus, value);
    }
    public string TbFecha
    {
        get => _tbFecha;
        set => SetProperty(ref _tbFecha, value);
    }
    #endregion
    public DelegateCommand<string> DetalleDenunciaCmd
    {
        get;
    }
    public ListaDenunciaCardViewModel(IEventAggregator ea, IRegionManager regionManager)
    {
        _ea = ea;
        _regionManager = regionManager;
        _apiService = new ApiService();
        DetalleDenunciaCmd = new DelegateCommand<string>(ExecuteDetalleDenunciaCmd);
        
    }

    private async void ExecuteDetalleDenunciaCmd(string obj)
    {
        Denuncia objDenuncia = new Denuncia();
        objDenuncia.IdDenuncia = Convert.ToInt64(TbIdDenuncia);
        objDenuncia.IdDenunciante = Convert.ToInt64(TbIdDenunciante);
        objDenuncia.TituloDenuncia = TbTitulo;
        objDenuncia.Empresa = TbEmpresa;
        objDenuncia.Pais = TbPais;
        objDenuncia.Estado = TbEstado;
        objDenuncia.NumCentro = Convert.ToInt32(TbNumCentro);
        objDenuncia.Folio = Convert.ToInt64(TbFolio);
        objDenuncia.DetalleDenuncia = TbDetalleDenuncia;
        objDenuncia.Estatus = TbEstatus;
        objDenuncia.FechaDenuncia = Convert.ToDateTime(TbFecha);
        var getDatos = await _apiService.GetDatosPersonal(objDenuncia.IdDenuncia);
        DatosPersonal datosPersonal = new DatosPersonal();
        datosPersonal.Nombre = getDatos.Nombre;
        datosPersonal.Correo = getDatos.Correo;
        datosPersonal.Telefono = getDatos.Telefono;
        _regionManager.RequestNavigate("ContentRegion", obj);
        _ea.GetEvent<MessageSentDenuncia>().Publish(objDenuncia);
        _ea.GetEvent<MessageSentDatosModel>().Publish(datosPersonal);

    }
}
