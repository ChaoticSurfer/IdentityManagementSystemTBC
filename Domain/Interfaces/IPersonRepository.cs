namespace Domain.Interfaces;


    public interface IPersonRepository
    {
        Person GetById(int id);
        void Add(Person person);
        void Update(Person person);
        void Delete(Person person);
    }