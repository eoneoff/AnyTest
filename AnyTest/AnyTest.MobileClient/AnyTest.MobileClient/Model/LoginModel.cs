using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AnyTest.MobileClient.Model
{
    public class LoginModel : AnyTest.ClientAuthentication.LoginModel, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public string this[string columnName] => throw new NotImplementedException();

        public override string UserName{
            get => base.UserName;
            set
            {
                base.UserName = value;
                var errors = ValidateProperty(value);
                if (errors.Any()) _errors[nameof(UserName)] = errors;
                else _errors.Remove(nameof(UserName));
                OnPropertyChanged();
            }
        }

        public override string Password
        {
            get => base.Password;
            set
            {
                base.Password = value;
                var errors = ValidateProperty(value);
                if (errors.Any()) _errors[nameof(Password)] = errors;
                else _errors.Remove(nameof(Password));
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string property = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));



        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public bool HasErrors => _errors.Any(props => props.Value.Any());

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public IEnumerable GetErrors(string propertyName)
        {
            if(!string.IsNullOrEmpty(propertyName))
            {
                if(_errors.ContainsKey(propertyName) && _errors[propertyName].Any())
                {
                    return _errors[propertyName].ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
            else
            {
                return _errors.SelectMany(err => err.Value.ToList()).ToList();
            }
        }

        protected List<string> ValidateProperty(object value, [CallerMemberName] string property = null)
        {
            var validationContext = new ValidationContext(this, null) { MemberName = property };
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(value, validationContext, validationResults);
            if (!Validator.TryValidateProperty(value, validationContext, validationResults))
            {
                return validationResults.Select(r => r.ErrorMessage).ToList();
            }

            return new List<string>();
        }
    }
}
