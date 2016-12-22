using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleFeedingApp.Services.PlatformSpecific.Interfaces
{
    public interface IFileAccess
    {
        string GetLocalFilePath(string filename);
    }
}
