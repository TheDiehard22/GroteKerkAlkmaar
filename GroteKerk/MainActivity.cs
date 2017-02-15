using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Support.V4.Widget;
using Android.Widget;
using Android.Content;
using Android.Preferences;
using GroteKerk.Fragments.Algemeen;
using GroteKerk.Fragments.Kids;
using GroteKerk.Fragments.Zerken;

namespace GroteKerk
{
    [Activity(Label = "Grote Kerk", Theme = "@style/KerkThema", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        #region Properties

        DrawerLayout _mDrawerLayout;
        NavigationView _mNavigtationView;
        ISharedPreferences _Preferences;
        ISharedPreferencesEditor _PreferencesEditor;

        ImageButton _ChooseApplicationType;
        NavigationView _NavigationView;
        View _NavigationHeaderView;

        #region Fragments

        Fragment _mCurrentFragment;
        AlgemeenHome _mAlgemeenHome;
        KidsHome _mKidsHome;
        ZerkenHome _mZerkenHome;

        #endregion

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            #region Properties

            _Preferences = PreferenceManager.GetDefaultSharedPreferences(this);
            _PreferencesEditor = _Preferences.Edit();

            _mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _mNavigtationView = FindViewById<NavigationView>(Resource.Id.left_drawer);

            #region Fragments

            _mAlgemeenHome = new AlgemeenHome();
            _mZerkenHome = new ZerkenHome();
            _mKidsHome = new KidsHome();

            Fragment FragmentToLoad = new Fragment();

            #endregion

            #endregion

            #region Fragment Handling

            switch (_Preferences.GetInt("appType", 0))
            {
                case 0:
                    FragmentToLoad = _mAlgemeenHome;
                    break;
                case 1:
                    FragmentToLoad = _mZerkenHome;
                    break;
                case 2:
                    FragmentToLoad = _mKidsHome;
                    break;
            }

            var trans = FragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragment_container, FragmentToLoad, FragmentToLoad.ToString());
            trans.Commit();

            _mCurrentFragment = FragmentToLoad;

            #endregion

            /// Initialize Toolbar
            InitToolbar();

            /// Initliaze Navigation
            InitNavigation();


            #region Events

            _ChooseApplicationType.Click += ChooseApplicationType_Click;
            _mNavigtationView.NavigationItemSelected += _mNavigtationView_NavigationItemSelected;

            #endregion
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (_Preferences.GetBoolean("firstRun", false) != true)
            {
                _PreferencesEditor.PutInt("appType", 0);
                _PreferencesEditor.Apply();
            }
        }

        #region Methods

        private void _mNavigtationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            e.MenuItem.SetChecked(true);

            switch (e.MenuItem.ItemId)
            {

            }

            // Close Drawer
            _mDrawerLayout.CloseDrawers();
        }

        private void ChooseApplicationType_Click(object sender, System.EventArgs e)
        {
            PopupMenu menu = new PopupMenu(this, _ChooseApplicationType);
            menu.MenuInflater.Inflate(Resource.Menu.app_types, menu.Menu);
            menu.Show();

            menu.MenuItemClick += (s, args) =>
            {
                switch (args.Item.ItemId)
                {
                    case Resource.Id.algemeen_app:
                        SwitchApp(0, _mAlgemeenHome);
                        return;
                    case Resource.Id.zerken_app:
                        SwitchApp(1, _mZerkenHome);
                        return;
                    case Resource.Id.kids_app:
                        SwitchApp(2, _mKidsHome);
                        return;
                }
            };
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _mDrawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
                case Resource.Id.algemeen_app:
                    _PreferencesEditor.PutInt("appType", 0);
                    _PreferencesEditor.Apply();
                    ReplaceFragment(_mAlgemeenHome);
                    return true;
                case Resource.Id.zerken_app:
                    _PreferencesEditor.PutInt("appType", 1);
                    _PreferencesEditor.Apply();
                    ReplaceFragment(_mZerkenHome);
                    return true;
                case Resource.Id.kids_app:
                    _PreferencesEditor.PutInt("appType", 2);
                    _PreferencesEditor.Apply();
                    ReplaceFragment(_mKidsHome);
                    return true;
            }



            return base.OnOptionsItemSelected(item);
        }

        private void ReplaceFragment(Fragment fragment)
        {
            // If the selected fragment is already open, close drawer and stay on fragment
            if (fragment.IsVisible)
            {
                return;
            }

            var trans = FragmentManager.BeginTransaction();

            trans.Replace(Resource.Id.fragment_container, fragment);
            trans.AddToBackStack(null);
            trans.Commit();

            _mCurrentFragment = fragment;
        }

        private void SwitchApp(int appType, Fragment fragment)
        {
            _PreferencesEditor.PutInt("appType", appType);
            _PreferencesEditor.Apply();
            _mNavigtationView.Menu.Clear();

            NavigationView NavigationView = FindViewById<NavigationView>(Resource.Id.left_drawer);
            View NavigationHeaderView = NavigationView.GetHeaderView(0);

            TextView HeaderTitle = NavigationHeaderView.FindViewById<TextView>(Resource.Id.current_app_title);

            switch (_Preferences.GetInt("appType", 0))
            {
                case 0:
                    _mNavigtationView.InflateMenu(Resource.Menu.left_drawer_algemeen);
                    HeaderTitle.Text = "Algemene App";
                    break;
                case 1:
                    _mNavigtationView.InflateMenu(Resource.Menu.left_drawer_zerken);
                    HeaderTitle.Text = "Zerken App";
                    break;
                case 2:
                    _mNavigtationView.InflateMenu(Resource.Menu.left_drawer_kids);
                    HeaderTitle.Text = "Kids App";
                    break;
            }

            ReplaceFragment(fragment);
        }

        private void InitNavigation()
        {
            _mNavigtationView.InflateHeaderView(Resource.Layout.nav_header);

            int tempMenuToLoad = 0;
            string tempHeaderTitle = null;

            if (_Preferences.GetInt("appType", 0) == 0)
            {
                tempMenuToLoad = Resource.Menu.left_drawer_algemeen;
                tempHeaderTitle = "Algemene";
            }
            else if (_Preferences.GetInt("appType", 0) == 1)
            {
                tempMenuToLoad = Resource.Menu.left_drawer_zerken;
                tempHeaderTitle = "Zerken";
            }
            else if (_Preferences.GetInt("appType", 0) == 2)
            {
                tempMenuToLoad = Resource.Menu.left_drawer_kids;
                tempHeaderTitle = "Kids";
            }

            _mNavigtationView.InflateMenu(tempMenuToLoad);

            _NavigationView = FindViewById<NavigationView>(Resource.Id.left_drawer);
            _NavigationHeaderView = _NavigationView.GetHeaderView(0);

            _ChooseApplicationType = _NavigationHeaderView.FindViewById<ImageButton>(Resource.Id.choose_app);
            TextView HeaderTitle = _NavigationHeaderView.FindViewById<TextView>(Resource.Id.current_app_title);
            HeaderTitle.Text = tempHeaderTitle + " App";
        }

        private void InitToolbar()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Grote Kerk";
            SetSupportActionBar(toolbar);

            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu_white_24dp);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        #endregion
    }
}

