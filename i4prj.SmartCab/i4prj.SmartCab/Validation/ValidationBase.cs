using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Prism.Mvvm;

// Adapted from: https://devblogs.microsoft.com/premier-developer/validate-input-in-xamarin-forms-using-inotifydataerrorinfo-custom-behaviors-effects-and-prism/

namespace i4prj.SmartCab.Validation
{
    public class ValidationBase : BindableBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// Contains a list of errors on a per propertyName basis
        /// </summary>
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        /// <summary>
        /// Contains a list of propertyName which are dirty (has been focused)
        /// </summary>
        private HashSet<string> _dirtyList = new HashSet<string>();

        public ValidationBase()
        {
            ErrorsChanged += ValidationBase_ErrorsChanged;
        }

        private void ValidationBase_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            RaisePropertyChanged("HasErrors");
            RaisePropertyChanged("ErrorsList");
        }

        #region INotifyDataErrorInfo Members

        /// <summary>
        /// Occurs when errors changed.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <returns>The errors.</returns>
        /// <param name="propertyName">Property name.</param>
        public IEnumerable GetErrors(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (_errors.ContainsKey(propertyName) && (_errors[propertyName].Any()))
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:i4prj.SmartCab.Validation.ValidationBase"/> has errors.
        /// </summary>
        /// <value><c>true</c> if has errors; otherwise, <c>false</c>.</value>
        public bool HasErrors
        {
            get
            {
                return _errors.Any(propErrors => propErrors.Value.Any());
            }
        }

        #endregion

        /// <summary>
        /// Validates the property.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        protected virtual void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            var validationContext = new ValidationContext(this, null)
            {
                MemberName = propertyName
            };

            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(value, validationContext, validationResults);

            RemoveErrorsByPropertyName(propertyName);

            HandleValidationResults(validationResults);
        }

        /// <summary>
        /// Removes the name of the errors by property.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        private void RemoveErrorsByPropertyName(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }

            RaiseErrorsChanged(propertyName);
        }

        /// <summary>
        /// Handles the validation results from ValidateProperty
        /// </summary>
        /// <param name="validationResults">Validation results.</param>
        private void HandleValidationResults(List<ValidationResult> validationResults)
        {
            var resultsByPropertyName = from results in validationResults
                                        from memberNames in results.MemberNames
                                        group results by memberNames into groups
                                        select groups;

            foreach (var property in resultsByPropertyName)
            {
                _errors.Add(property.Key, property.Select(r => r.ErrorMessage).ToList());
                RaiseErrorsChanged(property.Key);
            }
        }

        /// <summary>
        /// Raises the errors changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        private void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets the errors list.
        /// </summary>
        /// <value>The errors list.</value>
        public IList<string> ErrorsList
        {
            get
            {
                return GetErrors(string.Empty).Cast<string>().ToList();
            }
        }

        /// <summary>
        /// Sets a property as dirty.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        public void SetDirty(string propertyName)
        {
            _dirtyList.Add(propertyName);
        }

        /// <summary>
        /// Checks to see if property is dirty
        /// </summary>
        /// <returns><c>true</c>, if dirty, <c>false</c> otherwise.</returns>
        /// <param name="propertyName">Property name.</param>
        public bool IsDirty(string propertyName)
        {
            return _dirtyList.Contains(propertyName);
        }
    }
}
