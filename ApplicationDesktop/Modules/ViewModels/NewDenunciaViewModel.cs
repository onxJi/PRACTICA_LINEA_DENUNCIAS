using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationDesktop.Models;
using ApplicationDesktop.Repositories;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace ApplicationDesktop.Modules.ViewModels;
public class NewDenunciaViewModel: BindableBase
{
    private IEnumerable<string> _itemsEmpresa;
    private IEnumerable<string> _itemsPais, _itemsEstado;
    private readonly ApiService _apiService;
    private IEventAggregator _ea;
    private string _selectedEmpresa, _selectedPais, 
        _selectedEstado, _tbNumCentro, _errorMessage;
    private readonly IRegionManager _regionManager;
    public DelegateCommand<string> CapturarDenunciaCmd{ get;}
    public DelegateCommand<string> NavigateDatosPersonales
    {
        get;
    }
    public NewDenunciaViewModel(IRegionManager regionManager, IEventAggregator ea)
    {
        _ea = ea;
        _apiService = new ApiService();
        _regionManager = regionManager;
        CapturarDenunciaCmd = new DelegateCommand<string>(ExecuteCapturarDenunciaCmd, CanExecuteCapturarDenunciaCmd);
        NavigateDatosPersonales = new DelegateCommand<string>(ExecuteNavigateDatosPersonales, 
            CanExecuteNavigateDatosP);
        LoadItemsEmpresa();
        LoadItemsPais();
        LoadItemsEstado();
    }

    private bool CanExecuteNavigateDatosP(string arg)
    {
        bool validData = false;
        if (!string.IsNullOrWhiteSpace(SelectedEmpresa) &&
           !string.IsNullOrWhiteSpace(SelectedPais) &&
           !string.IsNullOrWhiteSpace(SelectedEstado) &&
           !string.IsNullOrWhiteSpace(TbNumCentro))
        {
            validData = true;
        }
        return validData;
    }

    private void ExecuteNavigateDatosPersonales(string obj)
    {
        Catalogo catalogo = new Catalogo();
        catalogo.Empresa = SelectedEmpresa;
        catalogo.Pais = SelectedPais;
        catalogo.Estado = SelectedEstado;
        catalogo.NumCentro = Convert.ToInt32(TbNumCentro);
        _regionManager.RequestNavigate("ContentDatos", obj);
        _ea.GetEvent<MessageSentModel>().Publish(catalogo);
    }

    private bool CanExecuteCapturarDenunciaCmd(string arg)
    {
        bool validData = false;
        if(!string.IsNullOrWhiteSpace(SelectedEmpresa)&&
           !string.IsNullOrWhiteSpace(SelectedPais) &&
           !string.IsNullOrWhiteSpace(SelectedEstado)&&
           !string.IsNullOrWhiteSpace(TbNumCentro))
        {
            validData = true;
        }
        return validData;
    }

    private async void ExecuteCapturarDenunciaCmd(string obj)
    {
        Catalogo catalogo = new Catalogo();
        Random random = new Random();
        long _ramdomCode = (random.Next(9999999));
        catalogo.Empresa = SelectedEmpresa;
        catalogo.Pais = SelectedPais;
        catalogo.Estado = SelectedEstado;
        catalogo.NumCentro = Convert.ToInt32(TbNumCentro);
        catalogo.IdDenuncia = _ramdomCode;
        var resultCatalog = await _apiService.PostCatalogo(catalogo);
        if (resultCatalog)
        {
            ErrorMessage = $"Se ha creado el catalogo con ID:{_ramdomCode} ";
            await Task.Delay(2000);
            ErrorMessage = "";
            DatosPersonal datos = new DatosPersonal();
            datos.IdDenuncia = _ramdomCode;
            datos.Nombre = string.Empty;
            datos.Correo = string.Empty;
            datos.Telefono = string.Empty;
            var resultDatos = await _apiService.PostDatosPersonales(datos);
            if (resultDatos)
            {
                var consult = await _apiService.GetDatosPersonal(_ramdomCode);
                datos.IdDenunciante = consult.IdDenunciante;
                ErrorMessage = $"Usted sera dirigido a otra ventana";
                await Task.Delay(2000);
                ErrorMessage = "";
                _regionManager.RequestNavigate("ContentRegion", obj);
                _ea.GetEvent<MessageSentModel>().Publish(catalogo);
                _ea.GetEvent<MessageSentDatosModel>().Publish(datos);
            }
        }
    }

    private async void LoadItemsEmpresa()
    {

        List<string> empresasList = new List<string>();
        var getEmpresas = await _apiService.GetEmpresas();
        if (getEmpresas != null)
        {
            getEmpresas.ToList().ForEach(item =>
            {
                if (item != null)
                {
                    empresasList.Add(item.Empresas);
                }
            });
            ItemsEmpresa = empresasList;
        }
        
    }
    private async void LoadItemsPais()
    {

        List<string> paisesList = new List<string>();
        var getPaises = await _apiService.GetPaises();
        if (getPaises != null)
        {
            getPaises.ToList().ForEach(item =>
            {
                if (item != null)
                {
                    paisesList.Add(item.Paises);
                }
            });
            ItemsPais = paisesList;
        }


    }
    private async void LoadItemsEstado()
    {

        List<string> estadosList = new List<string>();
        var getEstados = await _apiService.GetEstados();
        if (getEstados != null)
        {
            getEstados.ToList().ForEach(item =>
            {
                if (item != null)
                {
                    estadosList.Add(item.Estados);
                }
            });
            ItemsEstado = estadosList;
        }


    }
    public IEnumerable<string> ItemsEmpresa
    {
        get => _itemsEmpresa;
        set
        {
            SetProperty(ref _itemsEmpresa, value);
            CapturarDenunciaCmd.RaiseCanExecuteChanged();
        }
    }

    public IEnumerable<string> ItemsPais
    {
        get => _itemsPais;
        set
        {
            SetProperty(ref _itemsPais, value);
            CapturarDenunciaCmd.RaiseCanExecuteChanged();
        }
    }
    public IEnumerable<string> ItemsEstado
    {
        get => _itemsEstado;
        set
        {
            SetProperty(ref _itemsEstado, value);
            CapturarDenunciaCmd.RaiseCanExecuteChanged();
        }
    }
    public string SelectedEmpresa
    {
        get => _selectedEmpresa;
        set
        {
            SetProperty(ref _selectedEmpresa, value);
            CapturarDenunciaCmd.RaiseCanExecuteChanged();
            NavigateDatosPersonales.RaiseCanExecuteChanged();
        }
    }
    public string SelectedPais
    {
        get => _selectedPais;
        set
        {
            SetProperty(ref _selectedPais, value);
            CapturarDenunciaCmd.RaiseCanExecuteChanged();
            NavigateDatosPersonales.RaiseCanExecuteChanged();
        }
    }
    public string SelectedEstado
    {
        get => _selectedEstado;
        set
        {
            SetProperty(ref _selectedEstado, value);
            CapturarDenunciaCmd.RaiseCanExecuteChanged();
            NavigateDatosPersonales.RaiseCanExecuteChanged();
        }
    }
    public string TbNumCentro
    {
        get => _tbNumCentro;
        set
        {
            SetProperty(ref _tbNumCentro, value);
            CapturarDenunciaCmd.RaiseCanExecuteChanged();
            NavigateDatosPersonales.RaiseCanExecuteChanged();
        }
    }
    public string ErrorMessage
    {
        get
        {
            return _errorMessage;
        }
        set
        {
            SetProperty(ref _errorMessage, value);
        }
    }
}
