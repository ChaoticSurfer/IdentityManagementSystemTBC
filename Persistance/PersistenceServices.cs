using Application.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance;

public static class PersistenceServices
{
    public static IServiceCollection Register(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddScoped<IPersonRepository, PersonRepository>();
        collection.AddScoped<IImageService, ImageService>();

        collection.AddDbContext<PersonDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        return collection;
    }
}