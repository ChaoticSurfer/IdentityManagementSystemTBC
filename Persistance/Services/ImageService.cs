using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace Persistance.Services;

public class ImageService : IImageService
{
    private readonly IdentityManagementDbContext _identityManagementDbContext;
    private readonly string _projectRootAddresss;

    public ImageService(IdentityManagementDbContext identityManagementDbContext)
    {
        _identityManagementDbContext = identityManagementDbContext;
        _projectRootAddresss = GetProjectRootAddress();
    }

    public async Task UploadAndSetPhoto(int personId, IFormFile imageFile)
    {
        var person = await _identityManagementDbContext.People.FindAsync(personId);
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
                await _identityManagementDbContext.SaveChangesAsync();
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