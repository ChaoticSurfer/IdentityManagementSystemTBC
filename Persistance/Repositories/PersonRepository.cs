using Application.Exceptions;
using Domain;
using Domain.Interfaces;
using Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IdentityManagementDbContext _identityManagementDbContext;
    private readonly IImageService _imageService;

    public PersonRepository(
        IdentityManagementDbContext identityManagementDbContext,
        IImageService imageService
    )
    {
        _identityManagementDbContext = identityManagementDbContext;
        _imageService = imageService;
    }

    public async Task Save()
    {
        await _identityManagementDbContext.SaveChangesAsync();
    }

    public async Task<Person> GetPerson(int personId)
    {
        return await _identityManagementDbContext.People.FindAsync(personId);
    }

    public async Task AddPerson(Person person)
    {
        await _identityManagementDbContext.People.AddAsync(person);
    }

    public void RemovePerson(Person person)
    {
        _identityManagementDbContext.People.Remove(person);
    }


    public void UpdatePerson(Person updatedPerson)
    {
        _identityManagementDbContext.People.Update(updatedPerson);
    }

    public async Task AddPersonRelationship(PersonRelationShip personRelationShip)
    {
        _identityManagementDbContext.PersonRelationShips.Add(personRelationShip);
    }

    public async Task RemoveRelatedPerson(PersonRelationShip personRelationShip)
    {
        var personRelationShipEntity = await _identityManagementDbContext.PersonRelationShips.SingleOrDefaultAsync(pr
            => pr.RelatedFromPerson == personRelationShip.RelatedFromPerson
               && pr.RelatedToPerson == personRelationShip.RelatedToPerson
               && pr.RelationType == personRelationShip.RelationType);

        if (personRelationShipEntity == null) throw new NotFoundException("Person RelationShip not found");
        _identityManagementDbContext.PersonRelationShips.Remove(personRelationShip);
    }

    public async Task<PeopleNumberOfRelationsReportResponse> GetReportNumberOfRelationsOfPeople()
    {
        var responseReport = new PeopleNumberOfRelationsReportResponse();
        var pair = _identityManagementDbContext.PersonRelationShips
            .GroupBy(pr => pr.RelatedFromPerson.Id);
        var reports = pair.Select((group) => new PersonRelationsReport
        {
            PersonId = group.Key,
            Statistics = (group.GroupBy(rl => rl.RelationType)
                .Select(group => new RelationshipNumberTracker { RelationType = group.Key, Count = group.Count() }))
        });
        var relationsReportResponse = new PeopleNumberOfRelationsReportResponse { PersonRelationsReports = reports };
        return relationsReportResponse;
    }

    public async Task<List<Person>> SearchPeopleAlike(string searchWord, int page = 1)
    {
        var searchQuery = _identityManagementDbContext.People.AsNoTracking()
            .Where(person =>
                person.FirstName.Contains(searchWord) ||
                person.LastName.Contains(searchWord) ||
                person.PrivateId.Contains(searchWord));
        var pagedSearchQuery = searchQuery
            .Skip((page - 1) * Constants.ItemsOnPage)
            .Take(Constants.ItemsOnPage);
        return await pagedSearchQuery.ToListAsync();
    }

    // Does not implement Unit of work as of now.
    // Use Transaction if needed, consider risk of file being saved without address.
    public async Task AddImageToPerson(int personId, IFormFile imageFile)
    {
        await _imageService.UploadAndSetPhoto(personId, imageFile);
    }
}