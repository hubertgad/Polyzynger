using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Controllers
{
    internal abstract class SevenController : Controller
    {
        protected Assembly Assembly => Assembly.GetExecutingAssembly();

        protected SevenController(EventHandler<State> handler)
            : base(handler) { }

        protected string GetEntryAssemblyDirName()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);

            return new FileInfo(location.AbsolutePath).DirectoryName;
        }

        protected async Task CopyResourceAsync(string resourceName, string destination)
        {
            using (Stream input = GetManifestResourceStream(resourceName))
            {
                using Stream output = File.Create(destination);

                await input.CopyToAsync(output);
            }
        }

        protected async Task<string> GetResourceAsync(string resourceName)
        {
            using (Stream stream = GetManifestResourceStream(resourceName))
            {
                using StreamReader reader = new StreamReader(stream);

                return await reader.ReadToEndAsync();
            }
        }

        protected Task SaveResourceAsFile(string destination, string content)
        {
            using (Stream stream = File.Create(destination))
            {
                using StreamWriter writer = new StreamWriter(stream);

                return writer.WriteAsync(content);
            }
        }

        protected Stream GetManifestResourceStream(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string fullResourceName = "PolyzyngerApplication.Resources." + resourceName;

            return assembly.GetManifestResourceStream(fullResourceName);
        }
    }
}