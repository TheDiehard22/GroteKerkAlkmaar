using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;

namespace GroteKerk
{
    [Activity(Label = "Grote Kerk", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ViewPager viewPager;
        LinearLayout dotsLayout;
        TextView[] dots;
        public int[] layouts;
        Button btnNext, btnSkip;
        RefLayoutManager layoutManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            layoutManager = new RefLayoutManager(this);
            if (!layoutManager.isFirstTimeLaunch())
            {
                //launchHomeScreen();
                Finish();
            }

            SetContentView(Resource.Layout.Main);

            //Layout array for the splash intro slides
            layouts = new int[]
            {
                Resource.Layout.layoutSlide1,
                Resource.Layout.layoutSlide2
            };

            //bind view items to code
            viewPager = (ViewPager)FindViewById(Resource.Id.viewPager);
            dotsLayout = (LinearLayout)FindViewById(Resource.Id.layoutPanel);
            btnNext = (Button)FindViewById(Resource.Id.btn_next);
            btnSkip = (Button)FindViewById(Resource.Id.btn_skip);

            //Sets first dot as active
            //addDots(0);

            //Old code, read https://developer.xamarin.com/guides/android/user_interface/viewpager/part-1-viewpager-and-views/ to create a viewAdapter
            //Adapter adapter = new Adapter(layouts);
        }
    }
}

