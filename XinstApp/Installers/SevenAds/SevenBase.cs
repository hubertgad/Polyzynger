using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Threading.Tasks;

namespace XinstApp.Installers.SevenAds
{
    public abstract class SevenBase
    {
        public Controls Controls { get; set; }

        public SevenBase()
        {
            this.Controls = new Controls();
        }

        public abstract Task Perform();

        protected string GetEntryAssemblyDirName()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).DirectoryName;
        }

        protected void CopyResource(string resourceName, string destination)
        {
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string fullResourceName = "XinstApp.Resources." + resourceName;

                using (Stream input = assembly.GetManifestResourceStream(fullResourceName))
                using (Stream output = File.Create(destination))
                {
                    input.CopyTo(output);
                }
            }
        }

        protected void ExecuteScript(string script)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(script);
                var result = ps.BeginInvoke();
                ps.EndInvoke(result);
            }
        }
    }
}