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
    public partial class AnswerCell : ViewCell
    {
        public TestPass Pass
        {
            get => GetValue(PassProperty) as TestPass;
            set => SetValue(PassProperty, value);
        }

        private AnswerViewModel Answer => BindingContext as AnswerViewModel;

        public bool IsSelected
        {
            get => Pass?.Answers?.Any(a => a.AnswerId == Answer?.Id) ?? false;
            set
            {
                if(value)
                {
                    if (!IsSelected) Pass.Answers.Add(new AnswerPass { QuestionId = Answer.TestQuestionId, AnswerId = Answer.Id, Answer = Answer as TestAnswer });
                }
                else
                {
                    Pass.Answers = Pass.Answers.Where(a => a.AnswerId != Answer?.Id).ToList();
                }
                OnPropertyChanged();
            }
        }

        public AnswerCell()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty PassProperty = BindableProperty.Create("Pass", typeof(TestPass), typeof(AnswerCell), null);
    }
}