namespace Domain.Interfaces;

public interface IPhoneRepository
{
    Phone GetById(int id);
    void Add(Phone phone);
    void Update(Phone phone);
    void Delete(Phone phone);
}