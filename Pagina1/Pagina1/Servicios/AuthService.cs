using Newtonsoft.Json;
using Pagina1.Dtos;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pagina1.Servicios
{
    public class AuthService
    {
        private static readonly string ApiUrlRegistrarAdministrador = "http://10.0.2.2:5138/api/auth/registrar-administrador";
        private readonly HttpClient _client;

        public AuthService()
        {
            _client = new HttpClient();
            {
                _client.BaseAddress = new Uri("https://10.0.2.2:5138/api/auth/registrar-usuario");

            };
        }

        //registar admin
        public async Task<bool> RegistrarAdministradorAsync(RegistrarAdministradorDto dto)
        {
            try
            {
                // Serializar el DTO a JSON
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realizar la petición POST
                var response = await _client.PostAsync(ApiUrlRegistrarAdministrador, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Administrador registrado correctamente.");
                    return true;
                }
                else
                {
                    // Mostrar el mensaje de error devuelto por la API
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al registrar administrador: {errorResponse}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al registrar administrador: {ex.Message}");
                return false;
            }
        }

        //registrar dueño y paseador
        public async Task<bool> Register(RegistrarUsuarioDto registerDto)
        {
            try
            {
                // Serializar el objeto RegisterDto a JSON
                var jsonContent = JsonConvert.SerializeObject(registerDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Enviar el POST a la API en el endpoint correspondiente
                var response = await _client.PostAsync("api/auth/registrar-usuario", content);

                // Validar respuesta de la API
                if (response.IsSuccessStatusCode)
                {
                    return true; // Registro exitoso
                }
                else
                {
                    // Manejar errores específicos según la respuesta de la API
                    Console.WriteLine($"Error al registrar: {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Excepción al registrar: {ex.Message}");
                return false;
            }
        }
    }
}
