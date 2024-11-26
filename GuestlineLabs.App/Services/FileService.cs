namespace GuestlineLabs.App.Services
{
    public class FileService : IFileService
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string ReadFileAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
