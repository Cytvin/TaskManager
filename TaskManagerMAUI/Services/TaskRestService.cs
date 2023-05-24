using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagerMAUI.Models;

namespace TaskManagerMAUI.Services
{
    public class TaskRestService
    {
        private HttpClient _client;
        private JsonSerializerOptions _serializerOptions;
        private IHttpsClientHandlerService _httpsClientHandlerService;

        public TaskRestService(IHttpsClientHandlerService service)
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

        public async Task<List<UserTask>> GetUsersTasksAsync(int userId)
        {
            Uri uri = new Uri(string.Format(Constants.RestTaskAssigment, userId));

            List<UserTask> usersTasks = null;

            try
            {
                HttpResponseMessage response = null;

                response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    usersTasks = JsonSerializer.Deserialize<List<UserTask>>(json, _serializerOptions);
                }
            }
            catch
            {
                throw;
            }

            return usersTasks;
        }

        public async Task SaveUserTaskAsync(UserTask item)
        {
            Uri uri = new Uri(Constants.RestTask);

            try
            {
                string json = JsonSerializer.Serialize<UserTask>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTask successfully saved.");
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

        public async Task UpdateUserTaskAsync(UserTask task)
        {
            Uri uri = new Uri(string.Format(Constants.RestTaskAssigment, task.Id));

            TaskAssigment taskAssigment = new TaskAssigment()
            {
                TaskId = task.Id,
                UserId = Constants.CurrentUser.Id,
                IsDone = task.IsDone,
            };

            try
            {
                string json = JsonSerializer.Serialize<TaskAssigment>(taskAssigment, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await _client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTask successfully saved.");
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
