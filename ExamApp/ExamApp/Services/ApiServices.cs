using ExamApp.Models;
using ExamApp.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://calendaradhd.azurewebsites.net/api/Account/Register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            var keyvalues = new List<KeyValuePair<string, string>>()
             {
               new KeyValuePair<string, string>("username", email),
               new KeyValuePair<string, string>("password", password),
               new KeyValuePair<string, string>("grant_type", "password"),
             };
            var request = new HttpRequestMessage(HttpMethod.Post, "http://calendaradhd.azurewebsites.net/Token");
            request.Content = new FormUrlEncodedContent(keyvalues);
            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            JObject jObject = JsonConvert.DeserializeObject<dynamic>(content);
            var accesstoken = jObject.Value<string>("access_token");
            Settings.Accesstoken = accesstoken;
            Settings.UserName = email;
            Settings.Password = password;
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterWorkTask(WorkNote worknote)
        {
            var json = JsonConvert.SerializeObject(worknote);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workNoteApiUrl = "http://calendaradhd.azurewebsites.net/api/WorkNotes/PostWorkNote";
            var response = await httpClient.PostAsync(workNoteApiUrl, content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RegisterPlannedWorkshift(PlannedWorkshift plannedWorkshift)
        {
            var json = JsonConvert.SerializeObject(plannedWorkshift);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var plannedApiUrl = "http://calendaradhd.azurewebsites.net/api/PlannedWorkshifts/PostPlannedWorkshift";
            var response = await httpClient.PostAsync(plannedApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterWorkshift(Workshift workshift)
        {
            var json = JsonConvert.SerializeObject(workshift);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workshiftApiUrl = "http://calendaradhd.azurewebsites.net/api/Workshifts/PostWorkshift";
            var response = await httpClient.PostAsync(workshiftApiUrl, content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RegisterSettingsPause(SettingsPause pause)
        {
            var json = JsonConvert.SerializeObject(pause);
            var httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var settingsApiUrl = "http://calendaradhd.azurewebsites.net/api/SettingsPauses/PostSettingsPause";
            var response = await httpClient.PostAsync(settingsApiUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<WorkNote>> FindWorkNoteSubject(string calendarUserEmail, string titleWorkTask)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workNoteApiUrl = "http://calendaradhd.azurewebsites.net/api/WorkNotes/Get";
            var json = await httpClient.GetStringAsync($"{workNoteApiUrl}?titleWorkTask={titleWorkTask}&calendarUserEmail={calendarUserEmail}");
            return JsonConvert.DeserializeObject<List<WorkNote>>(json);
        }


        public async Task<List<WorkTask>> FindWorkTasks()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workTaskApiUrl = "http://calendaradhd.azurewebsites.net/api/WorkTasks/GetWorkTasks";
            var json = await httpClient.GetStringAsync($"{workTaskApiUrl}");
            return JsonConvert.DeserializeObject<List<WorkTask>>(json);
        }
        public async Task<List<SettingsPause>> FindSettingsPauseEmail(string calendarUserEmail)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var pauseApiUrl = "http://calendaradhd.azurewebsites.net/api/SettingsPauses/Get";
            var json = await httpClient.GetStringAsync($"{pauseApiUrl}?calendarUserEmail={calendarUserEmail}");
            return JsonConvert.DeserializeObject<List<SettingsPause>>(json);
        }

        public async Task<List<PlannedWorkshift>> FindDatesPlannedWorkshifts(string calendarUserEmail, bool done, DateTime date)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workshiftApiUrl = "http://calendaradhd.azurewebsites.net/api/PlannedWorkshifts/Get";
            var json = await httpClient.GetStringAsync($"{workshiftApiUrl}?date={date}&calendarUserEmail={calendarUserEmail}&done={done}");
            return JsonConvert.DeserializeObject<List<PlannedWorkshift>>(json);
        }
        public async Task<int> FindAvgUserAndTasks(string calendarUserEmail, int idWorkTask)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workshiftApiUrl = "http://calendaradhd.azurewebsites.net/api/Workshifts/GetAvgWorkshiftTimeOfCalendarUserEmailIdWorkTask";
            var json = await httpClient.GetStringAsync($"{workshiftApiUrl}?calendarUserEmail={calendarUserEmail}&idWorkTask={idWorkTask}");
            int d = JsonConvert.DeserializeObject<int>(json);
            return d;
        }
        public async Task<int> FindAvgTasks(int idWorkTask)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workshiftApiUrl = "http://calendaradhd.azurewebsites.net/api/Workshifts/GetAvgWorkshiftTimeOfIdWorkTask";
            var json = await httpClient.GetStringAsync($"{workshiftApiUrl}?idWorkTask={idWorkTask}");
            int d = JsonConvert.DeserializeObject<int>(json);
            return d;
        }
        public async Task<List<Workshift>> FindWorkshiftsSubjectOfUser(string calendarUserEmail, string titleWorkTask)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workshiftApiUrl = "http://calendaradhd.azurewebsites.net/api/Workshifts/Get";
            var json = await httpClient.GetStringAsync($"{workshiftApiUrl}?calendarUserEmail={calendarUserEmail}&titleWorkTask={titleWorkTask}");
            return JsonConvert.DeserializeObject<List<Workshift>>(json);
        }
        public async Task<List<Workshift>> FindWorkshiftsSubject(string titleWorkTask)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Settings.Accesstoken);
            var workshiftApiUrl = "http://calendaradhd.azurewebsites.net/api/Workshifts/GetWorkshiftOfTask";
            var json = await httpClient.GetStringAsync($"{workshiftApiUrl}?titleWorkTask={titleWorkTask}");
            return JsonConvert.DeserializeObject<List<Workshift>>(json);
        }

    }
}
