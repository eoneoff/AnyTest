using AnyTest.Model;
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
    public partial class TestInfoPage : ContentPage
    {
        private Model.TestViewModel test;
        private Model.TestViewModel Test
        {
            get => test;
            set
            {
                test = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TestName));
                OnPropertyChanged(nameof(PassesNumber));
                OnPropertyChanged(nameof(AwerageScore));
                OnPropertyChanged(nameof(MaxScore));
                OnPropertyChanged(nameof(MinScore));
            }
        }

        private IEnumerable<AnswerPass> Answers => AppState.Student?.Passes?.Where(p => p.TestId == test.Id)?.SelectMany(p => p.Answers)
        .Select(a =>
        {
            a.Answer = test?.Questions?.SelectMany(q => q.Answers)?.FirstOrDefault(ans => ans.Id == a.AnswerId);
            return a;
        });

        public string TestName => Test.Name;
        public int PassesNumber => AppState.Student?.Passes?.Where(p => p.TestId == test?.Id)?.Count() ?? 0;
        public decimal AwerageScore => PassesNumber != 0 ? (decimal)Answers?.Sum(a => a.Answer?.Percent) / (PassesNumber * test?.Questions?.Count()) ?? 0 : 0;
        public int MaxScore => AppState.Student?.Passes?.Select(p => p.Answers?.Sum(a => a.Answer?.Percent) / test?.Questions?.Count())?.Max() ?? 0;
        public int MinScore => AppState.Student?.Passes?.Select(p => p.Answers?.Sum(a => a.Answer?.Percent) /    test?.Questions?.Count())?.Min() ?? 0;

        public TestInfoPage(Model.TestViewModel test)
        {
            InitializeComponent();
            Test = test;
            BindingContext = this;
        }

        private async void PassButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TestPage(test));
        }
    }
}