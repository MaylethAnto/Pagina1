using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Pagina1.Servicios
{
    //login admin
    public static class LoginService
    {
        private static readonly string apiUrl = "http://tuapiurl.com";  // Cambia esta URL por la tuya.

        public static async Task<LoginResult> Login(string username, string password, string role)
        {
            using (var client = new HttpClient())
            {
                var loginData = new
                {
                    nombre_usuario = username,
                    contrasena = password,
                    rol_usuario = role
                };

                var content = new StringContent(JsonConvert.SerializeObject(loginData), System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{apiUrl}/login", content);  // Este endpoint debe existir en tu API.

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResult>(jsonResponse);
                    return result;
                }
                else
                {
                    return new LoginResult { IsSuccessful = false };
                }
            }
        }
    }

    public class LoginResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
