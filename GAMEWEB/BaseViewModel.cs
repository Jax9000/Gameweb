using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using MvvmValidation;

namespace GAMEWEB {

    public abstract class BaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        protected Dictionary<string, string> _errors;
        protected ValidationHelper validator;
        protected DataErrorInfoAdapter dataErrorInfoAdapter;
        private bool isValid = true;

        public BaseViewModel()
        {
            validator = new ValidationHelper();
            dataErrorInfoAdapter = new DataErrorInfoAdapter(validator);
            validator.ResultChanged += ValidatorOnResultChanged;
        }

        public virtual string this[string columnName]
        {
            get
            {
                var result = dataErrorInfoAdapter[columnName];
                return result;
            }
        }

        public string Error
        {
            get
            {
                return dataErrorInfoAdapter.Error;
            }
        }

        public bool IsValid
        {
            get { return this.isValid; }
            set
            {
                this.isValid = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Get the name of a static or instance property from a property access lambda.
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="propertyLambda">lambda expression of the form: '() => Class.Property' or '() => object.Property'</param>
        /// <returns>The name of the property</returns>
        public string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        protected bool ValidateAll()
        {
            var validationResult = validator.ValidateAll();
            
            return IsValid = validator.GetResult().IsValid;
        }
        protected string ValidateEmptyString([CallerMemberName] string propertyName = null)
        {
            var prop = this.GetType().GetProperty(propertyName);

            _errors[propertyName] = String.IsNullOrEmpty((String)prop.GetValue(this)) ? "Pole " +
                propertyName + " nie może być puste" : null;
            return _errors[propertyName];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
        {
            var memberExpression = (MemberExpression)projection.Body;
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
        }

        private void ValidatorOnResultChanged(object sender, ValidationResultChangedEventArgs eventArgs)
        {
            var propertyName = eventArgs.Target as string;

            if (!string.IsNullOrEmpty(propertyName))
            {
                OnPropertyChanged(propertyName);
                IsValid = validator.GetResult().IsValid;
            }
        }
    }

}
