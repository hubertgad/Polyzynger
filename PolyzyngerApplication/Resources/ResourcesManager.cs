using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace PolyzyngerApplication.Resources
{
    public static class ResourcesManager
    {
        public static async Task CopyResourceAsync(string resourceName, string destination)
        {
            using (Stream input = GetManifestResourceStream(resourceName))
            {
                using Stream output = File.Create(destination);

                await input.CopyToAsync(output);
            }
        }

        internal static async Task<string> GetResourceAsync(string resourceName)
        {
            using (Stream stream = GetManifestResourceStream(resourceName))
            {
                using StreamReader reader = new StreamReader(stream);

                return await reader.ReadToEndAsync();
            }
        }

        internal static Task SaveStringAsFile(string content, string destination)
        {
            using (Stream stream = File.Create(destination))
            {
                using StreamWriter writer = new StreamWriter(stream);

                return writer.WriteAsync(content);
            }
        }

        internal static Stream GetManifestResourceStream(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string fullResourceName = "PolyzyngerApplication.Resources." + resourceName;

            return assembly.GetManifestResourceStream(fullResourceName);
        }
    }
}