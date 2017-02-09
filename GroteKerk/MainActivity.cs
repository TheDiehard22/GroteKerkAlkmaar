using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Widget;

namespace GroteKerk
{
    [Activity(Label = "Grote Kerk", Theme = "@style/KerkThema", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout _mDrawerLayout;
        NavigationView _mNavigtationView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            #region Toolbar & Drawer Navigation

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Grote Kerk";
            SetSupportActionBar(toolbar);

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _mNavigtationView = FindViewById<NavigationView>(Resource.Id.left_drawer);

            _mNavigtationView.NavigationItemSelected += _mNavigtationView_NavigationItemSelected;

            #endregion

            ImageButton showAppTypes = FindViewById<ImageButton>(Resource.Id.choose_app);
            showAppTypes.Click += (s, arg) =>
            {
                PopupMenu menu = new PopupMenu(this, showAppTypes);
                menu.MenuInflater.Inflate(Resource.Menu.app_types, menu.Menu);
                menu.Show();
            };
        }

        private void _mNavigtationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);

            var ft = FragmentManager.BeginTransaction();

            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.zerken_home):

                    break;
                case (Resource.Id.zerken_map):
                    
                    break;
                case (Resource.Id.zerken_list):
                    
                    break;
            }

            // Close Drawer
            _mDrawerLayout.CloseDrawers();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _mDrawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}

