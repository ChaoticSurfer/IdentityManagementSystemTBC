using Microsoft.AspNetCore.Http;

namespace Persistance;

public interface IImageService
{
    public Task UploadAndSetPhoto(int personId, IFormFile imageFile);
}