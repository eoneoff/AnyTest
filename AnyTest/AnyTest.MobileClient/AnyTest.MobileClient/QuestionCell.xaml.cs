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
    public partial class QuestionCell : ViewCell
    {
        public TestPass Pass
        {
            get => GetValue(PassProperty) as TestPass;
            set
            {
                SetValue(PassProperty, value);
                OnPropertyChanged();
            }
        }

        public QuestionCell()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if(BindingContext is QuestionViewModel question)
            {
                AnswersList.HeightRequest = question.Answers.Count * 30 + 40;
            }
        }

        public static readonly BindableProperty PassProperty = BindableProperty.Create("Pass", typeof(TestPass), typeof(QuestionCell), null);
    }
}