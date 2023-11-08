using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Persistance;

public class ImageService : IImageService
{
    private readonly PersonDbContext _personDbContext;
    private readonly string _projectRootAddresss;

    public ImageService(PersonDbContext personDbContext)
    {
        _personDbContext = personDbContext;
        _projectRootAddresss = GetProjectRootAddress();
    }

    public async Task UploadAndSetPhoto(int personId, IFormFile imageFile)
    {
        var person = await _personDbContext.People.FindAsync(personId);
        if (person == null) throw new ArgumentException($"Person with {nameof(personId)} - {personId} not found");

        if (imageFile.Length > 0)
        {
            var uniqueFileName = GenerateUniqueFileName(imageFile.FileName);
            var uploadsPath = Path.Combine(_projectRootAddresss, Constants.UploadsDirectory);
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
                person.ImageRelativeAddress = Path.Combine(uploadsPath, uniqueFileName);
                await _personDbContext.SaveChangesAsync();
            }
        }
    }

    private string GenerateUniqueFileName(string previousName) =>
        Guid.NewGuid().ToString() + Path.GetExtension(previousName);

    private string GetProjectRootAddress()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
            return Path.GetDirectoryName(entryAssembly.Location);

        throw new Exception("Could  not get root address of Persistance project. Cant save file");
    }
}