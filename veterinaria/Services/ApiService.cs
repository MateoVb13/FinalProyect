using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using veterinaria.Models;


namespace veterinaria.Services
{
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

            // Captura el código de estado y el contenido de la respuesta para depuración
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Código de estado: {response.StatusCode}");
            Console.WriteLine($"Contenido de la respuesta: {responseContent}");

            // Maneja el caso cuando el código de estado es exitoso (200, 201)
            if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                try
                {
                    // Intenta deserializar la respuesta si no es string
                    if (typeof(TResponse) == typeof(string))
                    {
                        return (TResponse)(object)responseContent;
                    }
                    return JsonConvert.DeserializeObject<TResponse>(responseContent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al deserializar la respuesta: {ex.Message}");
                    // Si falla la deserialización, devuelve la respuesta como string si es necesario
                    if (typeof(TResponse) == typeof(string))
                    {
                        return (TResponse)(object)responseContent;
                    }
                    throw;  // Vuelve a lanzar la excepción si no es posible manejarla
                }
            }
            else
            {
                throw new Exception($"Error al enviar datos: {response.ReasonPhrase}, Código de estado: {response.StatusCode}, Contenido: {responseContent}");
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
        public async Task<bool> RegisterAsync(Usuario usuario)
        {
            try
            {
                var endpoint = "api/usuarios/register";
                var url = $"{_baseUrl}/{endpoint}";
                var jsonData = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                // Verificar si la respuesta es exitosa (código de estado 200 o 201)
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Registro exitoso.");
                    return true;
                }
                else
                {
                    // Capturar y mostrar el error recibido de la API
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al registrar el usuario: {response.ReasonPhrase}, Contenido del error: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la petición HTTP
                Console.WriteLine($"Error al registrar el usuario: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Detalles del error: {ex.InnerException.Message}");
                }
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
        public async Task<bool> RegistrarMascotaAsync(RegistrarMascotaDTO mascota)
        {
            var endpoint = "api/Mascotas/register";
            var url = $"{_baseUrl}/{endpoint}";

            try
            {
                var jsonData = JsonConvert.SerializeObject(mascota);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Usar _httpClient definido en la clase
                var response = await _httpClient.PostAsync(url, content);

                // Logs para depuración
                Console.WriteLine($"URL: {url}");
                Console.WriteLine($"JSON Enviado: {jsonData}");
                Console.WriteLine($"Código de estado: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Mascota registrada exitosamente.");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al registrar mascota: {response.ReasonPhrase}, Detalles: {errorContent}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción durante el registro: {ex.Message}");
                return false;
            }
        }


    }
}
