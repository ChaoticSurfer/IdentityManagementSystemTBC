namespace Domain.Interfaces;

public interface ICityRepository
{
    City GetByName(string name);
    void Add(City city);
    void Update(City city);
    void Delete(City city);
}