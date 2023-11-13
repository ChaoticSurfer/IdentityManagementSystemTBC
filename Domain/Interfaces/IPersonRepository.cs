using Application.Persistance.Contracts;
using Domain.Responses;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces;

public interface IPersonRepository
{
    /// <summary>
    /// Unit of Work implemented by adding save. Needs to be called after every write operation.
    /// </summary>
    public Task Save();

    public Task<Person> GetPerson(int personId);
    public Task AddPerson(Person person);
    public void RemovePerson(Person person);
    public void UpdatePerson(Person person);
    public Task AddPersonRelationship(PersonRelationShip personRelationShip);
    public Task RemoveRelatedPerson(PersonRelationShip personRelationShip);
    public Task<PeopleNumberOfRelationsReportResponse> GetReportNumberOfRelationsOfPeople();


    /// <summary>
    /// Get list of people whose Name, Surname, privateId  is contained with Paging. 
    /// </summary>
    /// <param name="searchWord"></param>
    /// <returns>List of matched people, with paging</returns>
    public Task<List<Person>> SearchPeopleAlike(string searchWord, int page);

    public Task AddImageToPerson(int personId, IFormFile imageFile);
}