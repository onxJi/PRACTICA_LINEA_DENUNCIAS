using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDesktop.Models;
using ApplicationDesktop.Repositories;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace ApplicationDesktop.Modules.ViewModels
{
    public class DatosPersonalesViewModel: BindableBase
    {
        private IEventAggregator _ea;
        private IRegionManager _regionManager;
        private readonly ApiService _apiService;
        private string _tbNombreCompleto, _tbCorreo, _tbTelefono,
            _selectedEmpresa, _selectedPais, _selectedEstado, 
            _tbNmCentro, _errorMessage;


        public string TbNombreCompleto
        {
            get => _tbNombreCompleto;
            set => SetProperty(ref  _tbNombreCompleto, value);
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
        public string SelectedEmpresa
        {
            get => _selectedEmpresa;
            set => SetProperty(ref _selectedEmpresa, value);
        }
        public string SelectedPais
        {
            get => _selectedPais;
            set => SetProperty(ref _selectedPais, value);
        }
        public string SelectedEstado
        {
            get => _selectedEstado;
            set => SetProperty(ref _selectedEstado, value);
        }
        public string TbNumCentro
        {
            get => _tbNmCentro;
            set => SetProperty(ref _tbNmCentro, value);
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public DelegateCommand<string> CapturarDenunciaCmd
        {
            get;
        }
        public DatosPersonalesViewModel(IEventAggregator ea, IRegionManager regionManager)
        {
            _ea = ea;
            _regionManager = regionManager;
            _apiService = new ApiService();
            _ea.GetEvent<MessageSentModel>().Subscribe(GetCatalogo);
            CapturarDenunciaCmd = new DelegateCommand<string>(ExecuteCapturarDenunciaCmd);

        }

        private void GetCatalogo(Catalogo catalogo)
        {
            SelectedEmpresa = catalogo.Empresa;
            SelectedPais = catalogo.Pais;
            SelectedEstado = catalogo.Estado;
            TbNumCentro = catalogo.NumCentro.ToString();
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
                datos.Nombre = TbNombreCompleto;
                datos.Correo = TbCorreo;
                datos.Telefono = TbTelefono;
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
    }
}
