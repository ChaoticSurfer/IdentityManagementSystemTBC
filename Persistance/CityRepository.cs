namespace Persistance;

public class CityRepository
{
    private readonly PersonDbContext _personDbContext;
    private readonly IImageService _imageService;

    public CityRepository(
        PersonDbContext personDbContext,
        IImageService imageService
    )
    {
        _personDbContext = personDbContext;
        _imageService = imageService;
    }
}