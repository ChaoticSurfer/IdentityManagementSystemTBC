namespace Domain.Interfaces;

public interface IPhoneRepository
{
    Task<Phone> GetById(int id);
    Task Add(Phone phone);
    void Update(Phone phone);
    void Delete(Phone phone);
    Task SaveChanges();
}