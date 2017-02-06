using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GroteKerk
{
    class RefLayoutManager
    {
        ISharedPreferences sharePref;
        ISharedPreferencesEditor editor;
        Context context;

        private static string pref_name = "Intro Slider";
        private static string is_first_time_launch = "thefirst";


        public RefLayoutManager(Context context)
        {
            this.context = context;
            sharePref = this.context.GetSharedPreferences(pref_name, FileCreationMode.Private);
            editor = sharePref.Edit();
        }

        public void SetFirstTimeLaunch(bool isFirstTime)
        {
            editor.PutBoolean(is_first_time_launch, isFirstTime);
            editor.Commit();
        }

        public Boolean isFirstTimeLaunch()
        {
            return sharePref.GetBoolean(is_first_time_launch, true);
        }
    }
}