using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class CityRepository : ICityRepository
{
    private readonly IdentityManagementDbContext _identityManagementDbContext;
    private readonly IImageService _imageService;

    public CityRepository(
        IdentityManagementDbContext identityManagementDbContext,
        IImageService imageService
    )
    {
        _identityManagementDbContext = identityManagementDbContext;
        _imageService = imageService;
    }

    public async Task Add(City city)
    {
        await _identityManagementDbContext.Cities.AddAsync(city);
    }

    public async Task<City> GetByName(string name)
    {
        return await _identityManagementDbContext.Cities.SingleOrDefaultAsync(city => city.Name == name);
    }

    public async Task<City> GetById(int cityId)
    {
        return await _identityManagementDbContext.Cities.FindAsync(cityId);
    }

    public async Task Update(City city)
    {
        _identityManagementDbContext.Update(city);
    }

    public async Task Delete(int cityId)
    {
        var city = await _identityManagementDbContext.Cities.FindAsync(cityId);

        if (city != null)
        {
            _identityManagementDbContext.Cities.Remove(city);
        }
    }

    public async Task<IEnumerable<City>> GetCities()
    {
        return await _identityManagementDbContext.Cities.ToListAsync();
    }

    public async Task SaveChanges()
    {
        await _identityManagementDbContext.SaveChangesAsync();
    }
}