using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDesktop.Repositories;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace ApplicationDesktop.Modules.ViewModels;
public class ListaDenunciasViewModel: BindableBase
{
    private readonly ApiService _apiService;
    private IRegionManager _regionManager;
    private ObservableCollection<BindableBase> _listCardObservableCollection;
    private IEventAggregator _ea;
    public ObservableCollection<BindableBase> ListCardObservableCollection
    {
        get => _listCardObservableCollection;
        set => SetProperty(ref _listCardObservableCollection, value);
    }    
    public ListaDenunciasViewModel(IRegionManager regionManager, IEventAggregator ea)
    {
        _regionManager = regionManager;
        _ea = ea;
        _apiService = new ApiService();
        LoadListDenuncias();
    }


    private async void LoadListDenuncias()
    {
        var consult = await _apiService.GetDenuncias();
        if (consult != null)
        {
            var lista = consult.Select(item => new ListaDenunciaCardViewModel(_ea,_regionManager)
            {
                TbIdDenuncia = item.IdDenuncia.ToString(),
                TbIdDenunciante = item.IdDenunciante.ToString(),
                TbEmpresa = item.Empresa,
                TbPais = item.Pais,
                TbEstado = item.Estado,
                TbNumCentro = item.NumCentro.ToString(),
                TbTitulo = item.TituloDenuncia,
                TbFolio = item.Folio.ToString(),
                TbDetalleDenuncia = item.DetalleDenuncia,
                TbEstatus = item.Estatus,
                TbFecha = item.FechaDenuncia.ToString("d")
            }).ToList();
            ListCardObservableCollection = new ObservableCollection<BindableBase>(lista);
        }
    }
}
