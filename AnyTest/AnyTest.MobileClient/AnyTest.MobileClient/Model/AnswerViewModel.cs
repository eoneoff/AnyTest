using AnyTest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AnyTest.MobileClient.Model
{
    public class AnswerViewModel : TestAnswer, INotifyPropertyChanged
    {
        public override string Answer
        {
            get => base.Answer;
            set
            {
                base.Answer = value;
                OnPropertyChanged();
            }
        }

        public override int Percent
        {
            get => base.Percent;
            set
            {
                base.Percent = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
