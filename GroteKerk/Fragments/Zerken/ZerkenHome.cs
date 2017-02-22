using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Json;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using GroteKerk.Classes;
using Newtonsoft.Json;
using GroteKerk.Classes.Models;
using System.Collections.Generic;

namespace GroteKerk.Fragments.Zerken
{
    public class ZerkenHome : Fragment
    {
        JsonLoader _mJsonLoader;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ZerkenHome, container, false);

            #region Properties

            _mJsonLoader = new JsonLoader();

            #endregion

            string url = "http://peerligthart.com/grotekerk/v1/api.php/zerken?transform=1";

            var json = _mJsonLoader.FetchJson(url);

            var zerken = JsonConvert.DeserializeObject<List<ZerkObject>>(json.ToString());


            return view;
        }
    }
}