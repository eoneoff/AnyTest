using AnyTest.MobileClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnyTest.MobileClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginModel Model = new LoginModel();

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = Model;
        }

        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            var result = await AppState.Login(Model);
            if (result.Sussessfull) await Navigation.PushAsync(new MainPage());
            else WrongCredentialsMessage.IsVisible = true;
        }
    }
}