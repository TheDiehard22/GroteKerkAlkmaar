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

namespace GroteKerk.Classes.Models
{
    class Zerk
    {
        public string Db_Id { get; set; }
        public int Zerk_Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    class ZerkObject
    {
        public Zerk Zerk { get; set; }
    }
}