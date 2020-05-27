using AnyTest.MobileClient.Model;
using AnyTest.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnyTest.MobileClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestsPage : ContentPage
    {
        public TestsPage()
        {
            InitializeComponent();
        }

        private async void Test_Clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            var testInfo = b.BindingContext as AnyTest.Model.ApiModels.TestsTreeModel;
            var response = await AppState.HttpClient.GetAsync(testInfo.Url);
            var test = JsonSerializer.Deserialize<TestViewModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //var test = await AppState.HttpClient.GetJsonAsync<TestViewModel>(testInfo.Url);
            await Navigation.PushAsync(new TestInfoPage(test));
        }
    }
}