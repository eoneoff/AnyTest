using AnyTest.ClientAuthentication;
using AnyTest.MobileClient.Model;
using AnyTest.Model;
using AnyTest.Model.ApiModels;
using Microsoft.AspNetCore.Components;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AnyTest.MobileClient
{
    public static class AppState
    {
        private static readonly string tokenKey = "authToken";

        public static HttpClient HttpClient = new HttpClient(
            new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            }, false
            ) { BaseAddress = new Uri(CrossSettings.Current.GetValueOrDefault("baseAddress", "https://192.168.0.115:44358/api/")) };

        public static Student Student;
        public static ObservableCollection<TestsTreeModel> Subjects = new ObservableCollection<TestsTreeModel>();
        public static ObservableCollection<TestsTreeModel> Courses = new ObservableCollection<TestsTreeModel>();
        public static ObservableCollection<TestsTreeModel> Tests = new ObservableCollection<TestsTreeModel>();

        public static bool IsLoggedIn =>
            App.Current.Properties.ContainsKey(tokenKey)
            && App.Current.Properties[tokenKey] is string token
            && !string.IsNullOrWhiteSpace(token)
            && !ApiAuthenticaionStateProvider.TokenExpired(token);
        public static async Task<LoginResult> Login(ClientAuthentication.LoginModel model)
        {
            var loginAsJson = JsonSerializer.Serialize(model);
            var response = await HttpClient.PostAsync("Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            if(response.IsSuccessStatusCode)
            {
                App.Current.Properties[tokenKey] = loginResult.Token;
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
                var person = await HttpClient.GetJsonAsync<Person>("accounts/person");
                Student = await HttpClient.GetJsonAsync<Student>($"students/{person.Id}");
            }

            return loginResult;
        }

        public static void Logout() => App.Current.Properties.Remove(tokenKey);

        private static async Task GetTestsList()
        {
            try
            {
                //var response = await HttpClient.GetAsync("tests/list");
                //var tests = JsonSerializer.Deserialize<Dictionary<string, List<TestsTreeModel>>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var tests = await HttpClient.GetJsonAsync<Dictionary<string, List<TestsTreeModel>>>("tests/list");
                if (tests.ContainsKey("subjects")) tests["subjects"].ForEach(s => Subjects.Add(s as TestsTreeModel));
                if (tests.ContainsKey("courses")) tests["courses"].ForEach(c => Courses.Add(c as TestsTreeModel));
                if (tests.ContainsKey("tests")) tests["tests"].ForEach(t => Tests.Add(t as TestsTreeModel));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async void LoadTestsList() => await GetTestsList();
    }
}
