using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal abstract class SevenController
    {
        internal abstract Task InstallAsync();

        protected string GetEntryAssemblyDirName()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).DirectoryName;
        }

        protected void CopyResource(string resourceName, string destination)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
                
            string fullResourceName = "PolyzyngerApplication.Resources." + resourceName;

            using Stream input = assembly.GetManifestResourceStream(fullResourceName);
                
            using Stream output = File.Create(destination);
                
            input.CopyTo(output);
        }

        public static string GetResource(string name)
        {
            string fullName = ("PolyzyngerApplication.Resources." + name);
            
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(fullName))
            {
                using StreamReader reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
        }
    }
}