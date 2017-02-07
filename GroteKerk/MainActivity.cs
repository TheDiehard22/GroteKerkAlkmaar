using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design;
using Android.Support.V7;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace GroteKerk
{
    [Activity(Label = "Grote Kerk", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout _DrawerLayout;
        NavigationView _NavigtationView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            // Initialize Drawer
            InitDrawer();    
        }

        protected void InitDrawer()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);

            ActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_black_24dp);
            ActionBar.SetDisplayHomeAsUpEnabled(true);

            _DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _NavigtationView = FindViewById<NavigationView>(Resource.Id.left_drawer);

            _NavigtationView.NavigationItemSelected += (sender, e) => {
                e.MenuItem.SetChecked(true);
                _DrawerLayout.CloseDrawers();
            };
        }
    }
}

