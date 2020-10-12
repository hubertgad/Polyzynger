using PolyzyngerApplication.Interfaces;
using System.IO;

namespace PolyzyngerApplication.Cleaners
{
    class DefaultCleaner : ICleaner
    {
        public void DeleteTempFilesAsync(string path)
        {
            if (File.Exists(path)) 
            { 
                File.Delete(path); 
            }
        }
    }
}