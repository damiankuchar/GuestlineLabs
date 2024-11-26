namespace GuestlineLabs.App.Services
{
    public interface IFileService
    {
        bool FileExists(string path);
        string ReadFileAllText(string path);
    }
}
