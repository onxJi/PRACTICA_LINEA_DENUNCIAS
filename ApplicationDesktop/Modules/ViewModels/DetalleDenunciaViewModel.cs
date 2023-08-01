using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ApplicationDesktop.Modules.ViewModels
{
    public class DetalleDenunciaViewModel: BindableBase
    {
        private IEventAggregator _ea;
        private readonly ApiService _apiService;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<BindableBase> _historialCardObservableCollection;
        private string _tbIdDenuncia, _tbIdDenunciante, _tbNombreCompleto,
        _tbCorreo, _tbTelefono, _tbEmpresa, _tbPais, _tbEstado, _tbNumCentro,
            _tbDetalleDenuncia, _tbTitulo, _tbFolio, _tbFecha, _tbEstatus;
        #region Campos
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
        public string TbFecha
        {
            get => _tbFecha;
            set => SetProperty(ref _tbFecha, value);
        }
        public ObservableCollection<BindableBase> HistorialCardObservableCollection
        {
            get => _historialCardObservableCollection;
            set => SetProperty(ref _historialCardObservableCollection, value);
        }
        public string TbEstatus
        {
            get => _tbEstatus;
            set => SetProperty(ref _tbEstatus, value);
        }
        #endregion


        public DelegateCommand<string> NavigateHomeCmd
        {
            get;
        }
        public DetalleDenunciaViewModel(IEventAggregator ea, IRegionManager regionManager)
        {
            _ea = ea;
            _apiService = new ApiService();
            _regionManager = regionManager;
            _ea.GetEvent<MessageSentDenuncia>().Subscribe(DenunciaReceived);
            _ea.GetEvent<MessageSentDatosModel>().Subscribe(DatosPersonales);
            NavigateHomeCmd = new DelegateCommand<string>(ExecuteNavigateHomeCmd);
        }

        private void ExecuteNavigateHomeCmd(string obj)
        {
            _regionManager.RequestNavigate("ContentRegion", obj);
        }

        private void DatosPersonales(DatosPersonal personal)
        {
            TbNombreCompleto = personal.Nombre;
            TbCorreo = personal.Correo;
            TbTelefono = personal.Telefono;
        }

        private void DenunciaReceived(Denuncia denuncia)
        {

            TbIdDenuncia = denuncia.IdDenuncia.ToString();
            TbIdDenunciante = denuncia.IdDenunciante.ToString();
            TbTitulo = denuncia.TituloDenuncia.ToString();
            TbEmpresa = denuncia.Empresa.ToString();    
            TbPais = denuncia.Pais.ToString();
            TbEstado = denuncia.Estado.ToString();
            TbNumCentro = denuncia.NumCentro.ToString();
            TbFolio = denuncia.Folio.ToString();
            TbDetalleDenuncia = denuncia.DetalleDenuncia;
            TbEstatus = denuncia.Estatus;
            TbFecha = denuncia.FechaDenuncia.ToString("d");

            LoadHistorialDenuncia();
        }


        private async void LoadHistorialDenuncia()
        {
            var consult = await _apiService.GetHistorialDenuncia(Convert.ToInt64(TbIdDenuncia));
            if (consult != null)
            {
                var historial = consult.Select(item => new HistorialCardViewModel()
                {
                    TbHistorial = item.Historial +" | " + item.IdDenuncia
                }).ToList();
                HistorialCardObservableCollection = new ObservableCollection<BindableBase>(historial);
            }
        }

    }
}
