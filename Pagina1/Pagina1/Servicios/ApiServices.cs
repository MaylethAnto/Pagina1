﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Pagina1.Modelo;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Xamarin.Essentials;
using Pagina1.Dtos;

namespace Pagina1.Servicios
{
    public class ApiService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://192.168.176.39:5138/api/";

        public ApiService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Métodos para Caninos
        public async Task<List<Canino>> GetCaninesAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}caninos");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Canino>>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Canino> AddCanineAsync(Canino canino)
        {
            try
            {
                var json = JsonConvert.SerializeObject(canino);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_baseUrl}caninos", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Canino>(jsonResponse);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> GuardarMascotaAsync(Canino canino)
        {
            try
            {
                var formData = new MultipartFormDataContent();

                // Serializa el objeto Canino a JSON y lo agrega al formulario
                var jsonCanino = JsonConvert.SerializeObject(canino);
                var jsonContent = new StringContent(jsonCanino, Encoding.UTF8, "application/json");
                formData.Add(jsonContent, "canino");

                // Agrega la imagen como parte del formulario
                var fileContent = new ByteArrayContent(canino.foto_canino);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                formData.Add(fileContent, "foto", "foto_canino.jpg");

                // Envía la solicitud POST al servidor
                var response = await _client.PostAsync($"{_baseUrl}caninos", formData);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Retorna true si la solicitud es exitosa
                }
                else
                {
                    Debug.WriteLine("Error al guardar la mascota");
                    return false; // Retorna false si hay algún error
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return false; // Retorna false si ocurre una excepción
            }
        }


        // Métodos para Historial Clínico
        public async Task<List<Historial_Clinico>> GetHistorialClinicoAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}historialClinico");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Historial_Clinico>>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Historial_Clinico> AddHistorialClinicoAsync(Historial_Clinico historial)
        {
            try
            {
                var json = JsonConvert.SerializeObject(historial);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_baseUrl}historialClinico", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Historial_Clinico>(jsonResponse);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Métodos para Dueño
        public async Task<List<Dueno>> GetDuenosAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}duenos");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Dueno>>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Dueno> AddDuenoAsync(Dueno dueno)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dueno);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_baseUrl}duenos", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Dueno>(jsonResponse);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Métodos para Ejercicio
        public async Task<List<Ejercicio>> GetEjerciciosAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}ejercicios");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Ejercicio>>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Ejercicio> AddEjercicioAsync(Ejercicio ejercicio)
        {
            try
            {
                var json = JsonConvert.SerializeObject(ejercicio);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_baseUrl}ejercicios", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Ejercicio>(jsonResponse);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Métodos para GPS
        public async Task<List<Gps>> GetGpsAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}gps");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Gps>>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Gps> AddGpsAsync(Gps gps)
        {
            try
            {
                var json = JsonConvert.SerializeObject(gps);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_baseUrl}gps", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Gps>(jsonResponse);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Métodos para Manejo de Perfiles
        public async Task<List<Manejo_Perfiles>> GetPerfilesAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}perfiles");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Manejo_Perfiles>>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Manejo_Perfiles> AddPerfilAsync(Manejo_Perfiles perfil)
        {
            try
            {
                var json = JsonConvert.SerializeObject(perfil);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_baseUrl}perfiles", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Manejo_Perfiles>(jsonResponse);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Métodos para Paseador
        public async Task<List<Paseador>> GetPaseadoresAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}paseadores");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Paseador>>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Paseador> AddPaseadorAsync(Paseador paseador)
        {
            try
            {
                var json = JsonConvert.SerializeObject(paseador);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_baseUrl}paseadores", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Paseador>(jsonResponse);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Métodos para Receta
        public async Task<List<Receta>> GetRecetasAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}recetas");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Receta>>(json);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<Receta> AddRecetaAsync(Receta receta)
        {
            try
            {
                var json = JsonConvert.SerializeObject(receta);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync($"{_baseUrl}recetas", content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Receta>(jsonResponse);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }

    }


}
