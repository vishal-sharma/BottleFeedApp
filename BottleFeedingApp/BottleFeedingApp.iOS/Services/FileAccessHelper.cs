using BottleFeedingApp.Services.PlatformSpecific.Interfaces;
using System;

//[assembly: Xamarin.Forms.Dependency(typeof(FileAccessHelper))]
namespace BottleFeedingApp.iOS.Services
{
    public class FileAccessHelper : IFileAccess
    {
        public FileAccessHelper() { }
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = System.IO.Path.Combine(docFolder, "..", "Library");

            if (!System.IO.Directory.Exists(libFolder))
            {
                System.IO.Directory.CreateDirectory(libFolder);
            }

            return System.IO.Path.Combine(libFolder, filename);
        }
    }
}
