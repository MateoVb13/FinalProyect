using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using veterinaria.Models;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiService(string baseUrl)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
    }

    public async Task<T> GetTAsync<T>(string endpoint)
    {
        var url = $"{_baseUrl}/{endpoint}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
        else
        {
            throw new Exception($"Error al obtener datos: {response.ReasonPhrase}");
        }
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        var url = $"{_baseUrl}/{endpoint}";
        var jsonData = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseData);
        }
        else
        {
            throw new Exception($"Error al enviar datos: {response.ReasonPhrase}");
        }
    }
    public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        var url = $"{_baseUrl}/{endpoint}";
        var jsonData = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(url, content);

        if (response.IsSuccessStatusCode)
        { 
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseData);
        }
        else
        {
            throw new Exception($"Error al actualizar datos: {response.ReasonPhrase}");
        }
    }

    public async Task<int> DeleteAsync(string endpoint)
    {
        var url = $"{_baseUrl}/{endpoint}";
        var response = await _httpClient.DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    // Método para registrar un usuario
    // Método para registrar un usuario
    public async Task<bool> RegisterAsync(Usuario usuario)
    {
        try
        {
            var endpoint = "api/usuarios/register";
            var response = await PostAsync<Usuario, Usuario>(endpoint, usuario);
            return response != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar el usuario: {ex.Message}");
            return false;
        }
    }



    // Método para iniciar sesión
    public async Task<Usuario> LoginAsync(UsuarioLoginDTO loginDTO)
    {
        try
        {
            var endpoint = "api/usuarios/login";
            return await PostAsync<UsuarioLoginDTO, Usuario>(endpoint, loginDTO);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al iniciar sesión: {ex.Message}");
            return null;
        }
    }
}