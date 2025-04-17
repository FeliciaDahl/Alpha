
namespace WebApp.Services;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string folderName);
}

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webenv;

    public FileService(IWebHostEnvironment webenv)
    {
        _webenv = webenv;
    }

    public async Task<string> SaveFileAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0)
            return null!;

        var uploadFolder = Path.Combine(_webenv.WebRootPath, "uploads", folderName);
        Directory.CreateDirectory(uploadFolder);

        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(uploadFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine("uploads", folderName, uniqueFileName);
    }

}
