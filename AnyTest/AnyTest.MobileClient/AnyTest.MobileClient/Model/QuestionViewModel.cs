using AnyTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;

namespace AnyTest.MobileClient.Model
{
    public class QuestionViewModel : TestQuestion, INotifyPropertyChanged
    {
        private ObservableCollection<AnswerViewModel> answers;

        public override string Question
        {
            get => base.Question;
            set
            {
                base.Question = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public override ICollection<TestAnswer> TestAnswers { get => base.TestAnswers; set => base.TestAnswers = value; }

        [JsonPropertyName("testAnswers")]
        public ObservableCollection<AnswerViewModel> Answers
        {
            get => answers;
            set
            {
                answers = value != null ? new ObservableCollection<AnswerViewModel>(value.OrderBy(q => q.OrderNo)) : null;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
