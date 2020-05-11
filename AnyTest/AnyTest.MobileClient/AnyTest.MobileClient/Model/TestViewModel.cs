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
    public class TestViewModel : Test, INotifyPropertyChanged
    {
        private ObservableCollection<QuestionViewModel> questions;
        public override string Name
        {
            get => base.Name;
            set
            {
                base.Name = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public override ICollection<TestQuestion> TestQuestions { get => base.TestQuestions; set => base.TestQuestions = value; }

        [JsonPropertyName("testQuestions")]
        public ObservableCollection<QuestionViewModel> Questions
        {
            get => questions;
            set
            {
                questions = value != null ? new ObservableCollection<QuestionViewModel>(value.OrderBy(q => q.OrderNo)) : null;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
