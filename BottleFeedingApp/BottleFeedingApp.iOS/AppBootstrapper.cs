using BottleFeedingApp.iOS.Services;
using BottleFeedingApp.Services.PlatformSpecific.Interfaces;
using Splat;

namespace BottleFeedingApp.iOS
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterDependencies();
        }

        public void RegisterDependencies()
        {
            Locator.CurrentMutable.RegisterConstant(new FileAccessHelper(), typeof(IFileAccess));
        }
    }
}
