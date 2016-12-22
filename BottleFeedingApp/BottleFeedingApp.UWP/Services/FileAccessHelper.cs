using BottleFeedingApp.Services.PlatformSpecific.Interfaces;
using BottleFeedingApp.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(FileAccessHelper))]
namespace BottleFeedingApp.UWP
{
    public class FileAccessHelper : IFileAccess
    {
        public FileAccessHelper() { }
        public string GetLocalFilePath(string filename)
        {
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            return System.IO.Path.Combine(path, filename);
        }
    }
}
