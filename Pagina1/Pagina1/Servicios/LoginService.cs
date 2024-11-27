using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Pagina1.Servicios
{
    public class LoginResponse
    {
        public string Mensaje { get; set; }
        public string Rol { get; set; }
    }

    public class LoginService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://10.0.2.2:5138/api/Login/Login"; 

        public LoginService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<LoginResponse> LoginUsuarioAsync(string nombreUsuario, string contrasena)
        {
            var loginRequest = new LoginRequest
            {
                NombreUsuario = nombreUsuario,
                Contrasena = contrasena
            };

            try
            {
                var json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new LoginResponse
                    {
                        Mensaje = "Error al iniciar sesión",
                        Rol = ""
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Mensaje = $"Error de conexión: {ex.Message}",
                    Rol = ""
                };
            }
        }
    }

    public class LoginRequest
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
    }
}