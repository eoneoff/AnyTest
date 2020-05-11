using AnyTest.MobileClient.Model;
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
    public partial class TestResultPage : ContentPage
    {
        private TestPass pass;
        private TestViewModel test;
        public TestPass Pass
        {
            get => pass;
            set
            {
                pass = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Progress));
                OnPropertyChanged(nameof(ProgressColor));
                OnPropertyChanged(nameof(ProgressLabel));
            }
        }

        public TestViewModel Test
        {
            get => test;
            set
            {
                test = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Progress));
                OnPropertyChanged(nameof(ProgressColor));
                OnPropertyChanged(nameof(ProgressLabel));
            }
        }

        public double Progress => (double)(Pass?.Answers?.Sum(a => a.Answer.Percent) ?? 0) / (double)(Test?.Questions?.Count() ?? 1) / 100.0;
        public Color ProgressColor => Progress switch
        {
            double progress when progress >= 0.9 => Color.Green,
            double progress when progress >= 0.6 => Color.Orange,
            _ => Color.Red
        };

        public string ProgressLabel => $"{Progress * 100}%";

        public TestResultPage(TestPass pass, TestViewModel test)
        {
            InitializeComponent();
            Pass = pass;
            Test = test;
        }
    }
}