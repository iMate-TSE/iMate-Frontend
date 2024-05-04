using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Exception = System.Exception;
using iMate.Models;
using System.Net.Http.Json;
using System.Text;
using iMate.Models.ApiModels;


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
        void UpdateSettings(string content);
        void LogOut(string username);
        Task<List<DatabaseCard>> GetCards(string mood);
        Task<User> FetchProfile(string token);
        Task<string> FetchProfilePhoto(string token);
        Task<List<FormQuestions>> GetQuestions(string questionCategory);
        Task<string> FetchMood(int P, int A, int D);
        Task UpdateProfile(string data);
        Task SaveMoodEntry(string data);
        Task<List<MoodEntry>> GetJournalData(string token);
        Task<int> FetchPoints(string token);
        Task<int> FetchStreak(string token);
        void UpdatePoints(string content);
    }


    class HttpService : IHttpService
    {
        private static readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("https://imateapi.azurewebsites.net/api/v1/")
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
                var content = new { username, password };
                using HttpResponseMessage response = 
                    await _httpClient.PostAsJsonAsync("Login/SignUp", content);

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
                var content = new { username };
                using HttpResponseMessage response =
                    await _httpClient.PostAsJsonAsync("Settings/CreateDefaultSettings", content);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void UpdateSettings(string content)
        {
            try
            {
                var jsonContent = new StringContent(content, Encoding.UTF8, "application/json");
                
                using HttpResponseMessage response = await _httpClient.PostAsync("Settings/UpdateUserSettings", jsonContent);
                response.EnsureSuccessStatusCode();

            }catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async void LogOut(string token)
        {
            try
            {
                var content = new {token}; 
                
                using HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Settings/LogOut", content);
                response.EnsureSuccessStatusCode();
            }catch(Exception ex) { Console.WriteLine(ex.Message); }
        }
        
        public async Task<User> FetchProfile(string token)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"Profile/?token={token}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadFromJsonAsync<User>());
                
                Console.WriteLine("USEr====================" + jsonResponse.userName);
                
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> FetchProfilePhoto(string token)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"Profile/?token={token}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadFromJsonAsync<User>());
                
                if (jsonResponse != null)
                {
                    return jsonResponse.avatarPath;
                }
                else
                {
                    return "fish.png";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<int> FetchPoints(string token)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"Profile/getCredits?token={token}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadAsStringAsync());
                
                if (jsonResponse != null)
                {
                    return int.Parse(jsonResponse);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async void UpdatePoints(string content)
        {
            try
            {
                var jsonContent = new StringContent(content, Encoding.UTF8, "application/json");
                using HttpResponseMessage res = await _httpClient.PutAsync("Profile/setCredits", jsonContent);
                Console.WriteLine(res);
                res.EnsureSuccessStatusCode();
            } catch (Exception ex) { Console.WriteLine(ex.Message);}
        }
        
        public async Task<int> FetchStreak(string token)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"Profile/getStreak?token={token}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadAsStringAsync());
                
                if (jsonResponse != null)
                {
                    return int.Parse(jsonResponse);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        
        public async Task UpdateProfile(string data)
        {
            Console.WriteLine("CALLING FROM HTTP Profile......");
            try
            {
                var jsonContent = new StringContent(data, Encoding.UTF8, "application/json");
                
                using HttpResponseMessage response = await _httpClient.PutAsync("Profile", jsonContent);
                response.EnsureSuccessStatusCode();
               

            }catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public async Task SaveMoodEntry(string data)
        {
            try
            {
                Console.WriteLine("======= Calling Save Mood");
                var jsonContent = new StringContent(data, Encoding.UTF8, "application/json");
                using HttpResponseMessage res = await _httpClient.PostAsync("Mood/save", jsonContent);
                res.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        public async Task<List<DatabaseCard>> GetCards(string mood)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync($"Card?mood={mood}");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await (response.Content.ReadFromJsonAsync<List<DatabaseCard>>());
                
                
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                else {
                    return new List<DatabaseCard>();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<DatabaseCard>();
            }
        }

        public async Task<string> FetchMood(int P, int A, int D)
        {
            try
            {
                using HttpResponseMessage response =
                    await _httpClient.GetAsync($"Mood?Pleasure={P}&Arousal={A}&Dominance={D}");
                response.EnsureSuccessStatusCode();
                var res = await (response.Content.ReadAsStringAsync());
                if (res != null)
                {
                    return res;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public async Task<List<MoodEntry>> GetJournalData(string token)
        {
            try
            {
                using HttpResponseMessage res = await _httpClient.GetAsync($"Mood/summary?token={token}");
                res.EnsureSuccessStatusCode();

                var JsonResponse = await (res.Content.ReadFromJsonAsync<List<MoodEntry>>());
                if (JsonResponse != null)
                {
                    return JsonResponse;
                }

                return new List<MoodEntry>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        public async Task<List<FormQuestions>> GetQuestions(string questionCategory)
        {
            try
            {
                using HttpResponseMessage response =
                    await _httpClient.GetAsync($"Mood/generateQuestions/?moodCategory={questionCategory}");

                response.EnsureSuccessStatusCode();
                
                var JsonReponse = await (response.Content.ReadFromJsonAsync<List<FormQuestions>>());

                if (JsonReponse != null)
                {
                    return JsonReponse;
                }
                else
                {
                    return new List<FormQuestions>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<FormQuestions>();
            }
        }
    }
}
