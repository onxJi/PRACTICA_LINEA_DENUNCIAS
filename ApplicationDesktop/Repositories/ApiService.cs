using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using ApplicationDesktop.Models;
using Newtonsoft.Json;
using Microsoft.VisualBasic.ApplicationServices;

namespace ApplicationDesktop.Repositories;
public class ApiService : IApiService
{
    private static string _baseUrl;
    public ApiService()
    {
        _baseUrl = "http://www.apijax.somee.com/";
        //_baseUrl = "http://localhost:5214/";
    }

    public async Task<bool> CheckLogin(string user, string pass)
    {
        bool isValidLogin=false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/UserAdmins/CheckLogin?user={Uri.EscapeDataString(user)}&pass={Uri.EscapeDataString(pass)}";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            // Si la respuesta es exitosa, leer el contenido de la respuesta como un booleano.
            // Dependiendo de cómo se haya configurado tu API, esto podría ser un JSON o simplemente un valor booleano.
            isValidLogin = await response.Content.ReadFromJsonAsync<bool>();

            return isValidLogin;
        }
        else
        {
            return isValidLogin;
        }
    }

    public async Task<List<Empresa>> GetEmpresas()
    {
        List<Empresa> list = new List<Empresa>();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/Empresas";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Empresa>>(jsonResult);
            list = result;

        }
        return list;

    }
    public async Task<List<Pais>> GetPaises()
    {
        List<Pais> list = new List<Pais>();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/Paises";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Pais>>(jsonResult);
            list = result;

        }
        return list;
    }
    public async Task<List<Estado>> GetEstados()
    {
        List<Estado> list = new List<Estado>();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/Estados";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Estado>>(jsonResult);
            list = result;

        }
        return list;
    }

    public async Task<bool> PostCatalogo(Catalogo catalogo)
    {
        bool result = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/Catalogos";
        var content = new StringContent(JsonConvert.SerializeObject(catalogo), Encoding.UTF8, "application/json");
        var response = await cliente.PostAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            result = true;
        }
        return result;
    }

    public async Task<bool> PostDatosPersonales(DatosPersonal datosPersonal)
    {
        bool result = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/DatosPersonales";
        var content = new StringContent(JsonConvert.SerializeObject(datosPersonal), Encoding.UTF8, "application/json");
        var response = await cliente.PostAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            result = true;
        }
        return result;
    }

    public async Task<bool> PostDenuncia(Denuncia denuncia)
    {
        bool result = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/Denuncias";
        var content = new StringContent(JsonConvert.SerializeObject(denuncia), Encoding.UTF8, "application/json");
        var response = await cliente.PostAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            result = true;
        }
        return result;
    }
    public async Task<Denuncia> GetDenunciaByFolio(int id)
    {
        Denuncia datosPersonal = new Denuncia();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/Denuncias/{id}";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            datosPersonal = JsonConvert.DeserializeObject<Denuncia>(jsonResult);

        }
        return datosPersonal;
    }
    public async Task<List<Denuncia>> GetDenuncias()
    {
        List<Denuncia> list = new List<Denuncia>();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/Denuncias";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Denuncia>>(jsonResult);
            list = result;

        }
        return list;
    }
    public async Task<bool> PostDenunciaByFolio(long id, string status)
    {
        bool result = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/Denuncias/Actualizar/{id}?status={status}";
        var content = new StringContent(JsonConvert.SerializeObject(new { Estatus = status }), Encoding.UTF8, "application/json");
        var response = await cliente.PutAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            result = true;
        }
        return result;
    }
    public async Task<DatosPersonal> GetDatosPersonal(long idDenuncia)
    {
        DatosPersonal datosPersonal = new DatosPersonal();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        //var uri = $"api/DatosPersonales/ObtenerDatos??idDenuncia={Uri.EscapeDataString(idDenuncia.ToString())}";
        var uri = $"api/DatosPersonales/ObtenerDatos/{idDenuncia}";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            datosPersonal = JsonConvert.DeserializeObject<DatosPersonal>(jsonResult);

        }
        return datosPersonal;
    }

    public async Task<bool> PostHistorialDenuncia(HistorialDenuncia historial)
    {
        bool result = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/HistorialDenuncias";
        var content = new StringContent(JsonConvert.SerializeObject(historial), Encoding.UTF8, "application/json");
        var response = await cliente.PostAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            result = true;
        }
        return result;
    }

    public async Task<List<HistorialDenuncia>> GetHistorialDenuncia(long idDenuncia)
    {
        var datosPersonal = new List<HistorialDenuncia>();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        //var uri = $"api/DatosPersonales/ObtenerDatos??idDenuncia={Uri.EscapeDataString(idDenuncia.ToString())}";
        var uri = $"api/HistorialDenuncias/ObtenerHistorial/{idDenuncia}";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HistorialDenuncia>>(jsonResult);
            datosPersonal = result;
        }
        return datosPersonal;
    }

    public async Task<bool> PostUserDenunciate(UserDenunciante datos)
    {
        bool result = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/UserDenunciantes";
        var content = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
        var response = await cliente.PostAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            result = true;
        }
        return result;
    }

    public async Task<bool> CheckLoginCliente(int user, string pass)
    {
        bool isValidLogin = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        var uri = $"api/UserDenunciantes/CheckLogin?user={user}&pass={Uri.EscapeDataString(pass)}";
        var response = await cliente.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            // Si la respuesta es exitosa, leer el contenido de la respuesta como un booleano.
            // Dependiendo de cómo se haya configurado tu API, esto podría ser un JSON o simplemente un valor booleano.
            isValidLogin = await response.Content.ReadFromJsonAsync<bool>();

            return isValidLogin;
        }
        else
        {
            return isValidLogin;
        }
    }
}
