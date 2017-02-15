using Android.App;
using Android.Widget;
using Android.OS;

namespace GroteKerk_Android
{
    [Activity(Label = "Grote Kerk", Theme = "@style/GroteKerkThema", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
        }
    }
}

