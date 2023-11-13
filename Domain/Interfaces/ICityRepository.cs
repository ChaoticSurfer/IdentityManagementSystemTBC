namespace Domain.Interfaces;

public interface ICityRepository
{
    Task Add(City city);
    Task<City> GetByName(string name);
    Task<City> GetById(int cityId);
    Task Update(City city);
    Task Delete(int cityId);
    Task<IEnumerable<City>> GetCities();
    Task SaveChanges();
}