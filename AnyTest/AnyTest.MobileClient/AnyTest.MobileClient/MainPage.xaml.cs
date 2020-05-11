using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AnyTest.MobileClient
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        long lastBackPress;

        public MainPage()
        {
            InitializeComponent();
            AppState.LoadTestsList();
        }

        protected override bool OnBackButtonPressed()
        {
            long currenTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if(currenTime - lastBackPress > 5000)
            {
               
                lastBackPress = currenTime;
                DependencyService.Get<IPopUp>().Long(AnyTest.ResourceLibrary.Resources.TapBackAgainToExit);
                return true;
            }

            AppState.Logout();
            return base.OnBackButtonPressed();
        }
    }
}
