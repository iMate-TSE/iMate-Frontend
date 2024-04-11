using System.Diagnostics;
using System.Net.Http.Json;
using Exception = System.Exception;

namespace iMate.Services
{
    // Singleton
    public interface IHttpService
    {
        Task<Dictionary<string, string>> GetSettings(string username);
        Task<string> Login(string username, string password);
        void SignUpUser(string username, string password);
        void CreateDefaultSettings(string username);
        Task<string> GetUsername(string token);
        void UpdateSettings(string username, bool soundEffects, bool reducedMotion, bool motivation, bool practice, bool scheduling, string? reminder);
        void LogOut(string username);
    }

    class HttpService : IHttpService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://10.0.2.2:5137/api/v1/")
        };

        public HttpService() { }

        public async Task<string> GetUsername (string token)
        {
            try 
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"Settings/GetUsername?token={token}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadAsStringAsync());

                if (jsonResponse != null)
                {
                    Debug.WriteLine(jsonResponse);
                    return jsonResponse;
                }
                else
                {
                    return "";
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public async Task<Dictionary<string, string>> GetSettings(string username)
        {
            try 
            {
                using HttpResponseMessage response = await _httpClient.GetAsync($"Settings/GetUserSettings?username={username}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadFromJsonAsync<Dictionary<string, string>>());

                
                if (jsonResponse != null) 
                {
                    return jsonResponse;
                } else
                {
                    return new Dictionary<string, string>();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Dictionary<string, string>();
            }
        }

        public async Task<string> Login(string username, string password)
        {
            try
            {
                var content = new { username, password };
                
                using HttpResponseMessage response = 
                    await _httpClient.PostAsJsonAsync("Login/Login", content);

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadAsStringAsync());

                Console.WriteLine("====================" + jsonResponse);
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                else
                {
                    return "404";
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "404";
            }
        }

        public async void SignUpUser(string username, string password)
        {
            try
            {
                StringContent content = new StringContent($"username={username}&password={password}");
                using HttpResponseMessage response = await _httpClient.PostAsync("Login/SignUp", content);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void CreateDefaultSettings(string username)
        {
            try
            {
                StringContent content = new StringContent($"username={username}");
                using HttpResponseMessage response = await _httpClient.PostAsync("Settings/CreateDefaultSettings", content);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void UpdateSettings(string username, bool soundEffects, bool reducedMotion, bool motivation, bool practice, bool scheduling, string? reminder)
        {
            try
            {
                StringContent content = new StringContent($"username={username}&soundEffects={soundEffects}&reducedMotion={reducedMotion}&motivation={motivation}&practice={practice}&scheduling={scheduling}&reminder={reminder}");

                using HttpResponseMessage response = await _httpClient.PostAsync("Settings/UpdateUserSettings", content);
                response.EnsureSuccessStatusCode();

            }catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async void LogOut(string username)
        {
            try
            {
                StringContent content = new StringContent($"username={username}");

                using HttpResponseMessage response = await _httpClient.PostAsync("Settings/LogOut", content);
                response.EnsureSuccessStatusCode();
            }catch(Exception ex) { Console.WriteLine(ex.Message); }
        }

    }
    
}
