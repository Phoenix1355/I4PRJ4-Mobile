using System;
using Android.Support.V4.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using i4prj.SmartCab.Droid.Effects;

[assembly: ResolutionGroupName("SmartCabEffects")]
[assembly: ExportEffect(typeof(BorderEffect), "BorderEffect")]
namespace i4prj.SmartCab.Droid.Effects
{
    public class BorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var drawable = ContextCompat.GetDrawable(Control.Context, Resource.Drawable.border);
                Control.SetBackground(drawable);
            }
            catch (Exception)
            {
            }
        }

        protected override void OnDetached()
        {
            try
            {
                Control.SetBackground(null);
            }
            catch (Exception)
            {
            }
        }
    }
}
