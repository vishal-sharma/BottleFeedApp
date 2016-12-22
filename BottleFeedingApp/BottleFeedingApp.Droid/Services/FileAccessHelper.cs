using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//using BottleFeedingApp.Droid.Services;
using BottleFeedingApp.Services.PlatformSpecific.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//[assembly: Xamarin.Forms.Dependency(typeof(FileAccessHelper))]
namespace BottleFeedingApp.Droid.Services
{
    public class FileAccessHelper : IFileAccess
    {
        public FileAccessHelper() { }
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }
    }
}