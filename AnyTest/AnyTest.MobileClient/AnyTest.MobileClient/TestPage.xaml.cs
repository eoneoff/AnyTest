using AnyTest.MobileClient.Model;
using AnyTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib = AnyTest.ResourceLibrary;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnyTest.MobileClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        private TestViewModel test;
        public TestPass Pass { get; set; }

        public TestViewModel Test
        {
            get => test;
            set
            {
                test = value;
                Pass = new TestPass { TestId = Test.Id, Answers = new List<AnswerPass>() }; 
                OnPropertyChanged();
            }
        }

        public TestPage(TestViewModel test)
        {
            InitializeComponent();
            Test = test;
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(Lib.Resources.FinishTest, string.Format(Lib.Resources.FinistTestAndSubmitResult, Test?.Name), Lib.Resources.Yes, Lib.Resources.No)) await Navigation.PushAsync(new TestResultPage(Pass, Test));
        }
    }
}