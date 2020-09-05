using System.IO;
using System.Reflection;

namespace Polyzynger.Resources
{
    static class ContentLoading
    {
        public static string GetResource(string name)
        {
            string fullName = ("Polyzynger.Resources." + name);
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(fullName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}