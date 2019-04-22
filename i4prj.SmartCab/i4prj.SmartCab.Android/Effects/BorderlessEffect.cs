using System;
using Android.Support.V4.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using i4prj.SmartCab.Droid.Effects;

//Only needed for first effect: [assembly: ResolutionGroupName("SmartCabEffects")]
[assembly: ExportEffect(typeof(BorderlessEffect), "BorderlessEffect")]
namespace i4prj.SmartCab.Droid.Effects
{
    public class BorderlessEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var drawable = ContextCompat.GetDrawable(Control.Context, Resource.Drawable.borderless);
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
