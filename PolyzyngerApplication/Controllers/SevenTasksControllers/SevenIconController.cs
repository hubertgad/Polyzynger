using System;
using System.IO;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers.SevenTasksControllers
{
    internal class SevenIconController : SevenController
    {
        internal override Task InstallAsync()
        {
            string sevenDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString(), "Seven");
            
            Directory.CreateDirectory(sevenDir);
            
            string iconDestination = Path.Combine(sevenDir, "Seven.ico");

            string linkDestination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString(), "Prosimy o opinię!.url");
            
            string lnkText = GetResource("Prosimy o opinie!.url");
            
            lnkText = lnkText.Replace("USERNAME", Environment.UserName.ToString());
            
            CopyResource("Seven.ico", iconDestination);
            
            using (Stream stream = File.Create(linkDestination))
            {
                using StreamWriter writer = new StreamWriter(stream);
                
                return writer.WriteAsync(lnkText);
            }
        }
    }
}