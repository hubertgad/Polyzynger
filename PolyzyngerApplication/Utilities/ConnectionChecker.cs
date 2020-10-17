using System.Runtime.InteropServices;

namespace PolyzyngerApplication.Utilities
{
    internal static class ConnectionChecker
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        internal static bool IsConnectedToInternet()
        {
            return InternetGetConnectedState(out _, 0);
        }
    }
}