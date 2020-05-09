using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnyTest.MobileClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (AppState.IsLoggedIn)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
