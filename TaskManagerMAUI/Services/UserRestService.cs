using System.Diagnostics;
using System.Text;
using System.Text.Json;
using TaskManagerMAUI.Models;

namespace TaskManagerMAUI.Services
{
    public class UserRestService
    {
        private HttpClient _client;
        private JsonSerializerOptions _serializerOptions;
        private IHttpsClientHandlerService _httpsClientHandlerService;

        public UserRestService(IHttpsClientHandlerService service)
        {
#if DEBUG
            _httpsClientHandlerService = service;
            HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            if (handler != null)
                _client = new HttpClient(handler);
            else
                _client = new HttpClient();
#else
            _client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<User> GetUserAsync(string login, string password)
        {
            Uri uri = new Uri(string.Format(Constants.RestUserAuthorization, login, password));

            User user = null;

            try
            {
                HttpResponseMessage response = null;

                response = await _client.GetAsync(uri);
                
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    user = JsonSerializer.Deserialize<User>(json, _serializerOptions);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exception("Пользователь не найден");
                }
            }
            catch
            {
                throw;
            }

            return user;
        }

        public async Task SaveUserAsync(User item)
        {
            Uri uri = new Uri(Constants.RestUserRegistration);

            try
            {
                string json = JsonSerializer.Serialize<User>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tUser successfully saved.");
                }    
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string responseMessage = await response.Content.ReadAsStringAsync();

                    throw new Exception(responseMessage);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
