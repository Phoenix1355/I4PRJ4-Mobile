using System;
using System.Linq;
using Xamarin.Forms;
using i4prj.SmartCab.Effects;
using Prism.DryIoc;

// Adapted from: https://devblogs.microsoft.com/premier-developer/validate-input-in-xamarin-forms-using-inotifydataerrorinfo-custom-behaviors-effects-and-prism/

namespace i4prj.SmartCab.Validation
{
    /// <summary>
    /// Validation behavior for the input type Entry
    /// </summary>
    public class EntryValidationBehavior : Behavior<Entry>
    {
        private Entry _associatedObject;
        private Color _defaultColor;

        /// <summary>
        /// Perfoms setup
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            // Perform setup       

            _associatedObject = bindable;

            // Setup events
            _associatedObject.TextChanged += _associatedObject_TextChanged;

            // Save default color
            _defaultColor = _associatedObject.TextColor;
        }

        /// <summary>
        /// Updates the graphical aspect of the Entry on the basis of errors and dirty-ness
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void _associatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var source = _associatedObject.BindingContext as ValidationBase;

            if (source != null && !string.IsNullOrEmpty(PropertyName))
            {
                var errors = source.GetErrors(PropertyName).Cast<string>();

                if (errors != null && errors.Any() && source.IsDirty(PropertyName))
                {
                    _associatedObject.TextColor = Color.FromHex("#B00020");
                }
                else
                {
                    _associatedObject.TextColor = _defaultColor;
                }

                // Legacy use of red border effect on validation
                /*var borderEffect = _associatedObject.Effects.FirstOrDefault(eff => eff is BorderEffect);

                if (errors != null && errors.Any() && source.IsDirty(PropertyName))
                {
                    if (borderEffect == null)
                    {
                        _associatedObject.Effects.Add(new BorderEffect());
                    }
                }
                else
                {
                    if (borderEffect != null)
                    {
                        _associatedObject.Effects.Remove(borderEffect);
                    }
                }*/
            }
        }

        /// <summary>
        /// Perfoms clean-up
        /// </summary>
        /// <param name="bindable">Bindable.</param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            // Perform clean up

            _associatedObject.TextChanged -= _associatedObject_TextChanged;

            _associatedObject = null;
        }

        public string PropertyName { get; set; }
    }
}
