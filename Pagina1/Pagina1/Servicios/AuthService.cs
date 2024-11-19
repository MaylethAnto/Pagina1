using Pagina1.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Pagina1.Servicios
{
    public class AuthService
    {
        private HttpClient _client;

        public AuthService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://192.168.100.200:5138/api/");
        }

        public async Task<bool> Login(string usuario, string contrasena)
        {
            var loginDto = new LoginDto
            {
                NombreUsuario = usuario,
                Contrasena = contrasena
            };

            var json = JsonSerializer.Serialize(loginDto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<LoginResponse>(resultJson);

                Preferences.Set("AuthToken", result.Token);
                Preferences.Set("TipoPerfil", result.TipoPerfil);
                return true;
            }
            return false;
        }

        public async Task<bool> Register(RegisterDto registro)
        {
            var json = JsonSerializer.Serialize(registro);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("auth/register", content);

            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();
                // Si necesitas deserializar alguna respuesta
                return true;
            }
            return false;
        }
    }

        public class LoginResponse
    {
        public string Token { get; set; }
        public string TipoPerfil { get; set; }
    }
}
