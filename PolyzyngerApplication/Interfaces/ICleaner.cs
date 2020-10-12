namespace PolyzyngerApplication.Interfaces
{
    internal interface ICleaner
    {
        void DeleteTempFilesAsync(string path);
    }
}