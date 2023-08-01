using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDesktop.Models;

namespace ApplicationDesktop.Repositories;
public interface IApiService
{
    Task<bool> CheckLogin(string user, string pass);
    Task<bool> CheckLoginCliente(int user, string pass);
    Task<List<Empresa>> GetEmpresas();
    Task<List<Pais>> GetPaises();
    Task<List<Estado>> GetEstados();
    Task<bool> PostCatalogo(Catalogo catalogo);
    Task<bool> PostDenuncia(Denuncia denuncia);
    Task<bool> PostDenunciaByFolio(long id, string status);
    Task<Denuncia> GetDenunciaByFolio(int id);
    Task<List<Denuncia>> GetDenuncias();
    Task<bool> PostDatosPersonales(DatosPersonal datosPersonal);
    Task<DatosPersonal> GetDatosPersonal(long idDenuncia);
    Task<bool> PostHistorialDenuncia(HistorialDenuncia historial);
    Task<List<HistorialDenuncia>> GetHistorialDenuncia(long idDenuncia);
    Task<bool> PostUserDenunciate(UserDenunciante datos);
}
