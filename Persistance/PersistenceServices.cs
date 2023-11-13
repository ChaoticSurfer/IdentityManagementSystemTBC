using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Repositories;
using Persistance.Services;

namespace Persistance;

public static class PersistenceServices
{
    public static IServiceCollection Register(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddScoped<IPersonRepository, PersonRepository>();
        collection.AddScoped<ICityRepository, CityRepository>();
        collection.AddScoped<IImageService, ImageService>();

        collection.AddDbContext<IdentityManagementDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        return collection;
    }
}