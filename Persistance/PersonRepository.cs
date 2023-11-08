using System.Diagnostics;
using Application.Exceptions;
using Application.Persistance.Contracts;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class PersonRepository : IPersonRepository
{
    private readonly PersonDbContext _personDbContext;
    private readonly IImageService _imageService;

    public PersonRepository(
        PersonDbContext personDbContext,
        IImageService imageService
    )
    {
        _personDbContext = personDbContext;
        _imageService = imageService;
    }

    public async Task Save()
    {
        await _personDbContext.SaveChangesAsync();
    }

    public async Task<Person> GetPerson(int personId)
    {
        return await _personDbContext.People.FindAsync(personId);
    }

    public async Task AddPerson(Person person)
    {
        await _personDbContext.People.AddAsync(person);
    }

    public async Task RemovePerson(Person person)
    {
        _personDbContext.People.Remove(person);
        await _personDbContext.SaveChangesAsync();
    }


    public async Task UpdatePerson(Person updatedPerson)
    {
        
        var existingPerson = await _personDbContext.People.FindAsync(updatedPerson.Id);
        
        if (existingPerson != null)
        {
            if (updatedPerson.FirstName != null)
                existingPerson.FirstName = updatedPerson.FirstName;

            if (updatedPerson.LastName != null)
                existingPerson.LastName = updatedPerson.LastName;

            if (updatedPerson.Sex != null)
                existingPerson.Sex = updatedPerson.Sex;

            if (updatedPerson.PrivateId != null)
                existingPerson.PrivateId = updatedPerson.PrivateId;

            if (updatedPerson.DateOfBirth != null)
                existingPerson.DateOfBirth = updatedPerson.DateOfBirth;

            if (updatedPerson.City != null)
                existingPerson.City = updatedPerson.City;

            if (updatedPerson.Phone != null)
                existingPerson.Phone = updatedPerson.Phone;
        }
    }

    public async Task AddPersonRelationship(PersonRelationShip personRelationShip)
    {
        _personDbContext.PersonRelationShips.Add(personRelationShip);
    }

    public async Task RemoveRelatedPerson(PersonRelationShip personRelationShip)
    {
        var personRelationShipEntity = await _personDbContext.PersonRelationShips.SingleOrDefaultAsync(pr
            => pr.RelatedFromPerson == personRelationShip.RelatedFromPerson
               && pr.RelatedToPerson == personRelationShip.RelatedToPerson
               && pr.RelationType == personRelationShip.RelationType);

        if (personRelationShipEntity == null) throw new NotFoundException("Person RelationShip not found");
        _personDbContext.PersonRelationShips.Remove(personRelationShip);
    }

    public async Task<PeopleNumberOfRelationsReportResponse> GetReportNumberOfRelationsOfPeople()
    {
        var responseReport = new PeopleNumberOfRelationsReportResponse();
      var r =  _personDbContext.PersonRelationShips.GroupBy(pr => pr.)
        var people = await _personDbContext.People.AsNoTracking().ToListAsync();
        foreach (var person in people)
        {
            var groupedRelatedPeople = person.PersonRelationShips
                .GroupBy(relatedPerson => relatedPerson.RelationType);

            var personReport = new PersonRelationsReport() { Person = person };

            foreach (var typeRelatedPeople in groupedRelatedPeople)
            {
                if (typeRelatedPeople.Key == PersonRelationType.Unknown)
                    personReport.Unknown = typeRelatedPeople.Count();
                else if (typeRelatedPeople.Key == PersonRelationType.Relative)
                    personReport.Relative = typeRelatedPeople.Count();
                else if (typeRelatedPeople.Key == PersonRelationType.College)
                    personReport.College = typeRelatedPeople.Count();
                else if (typeRelatedPeople.Key == PersonRelationType.Acquaintance)
                    personReport.Acquaintance = typeRelatedPeople.Count();
            }

            responseReport.PersonRelationsReports.Add(personReport);
        }

        return responseReport;
    }

    public async Task<List<Person>> SearchPeopleAlike(string searchWord, int page = 1)
    {
        var searchQuery = _personDbContext.People.AsNoTracking()
            .Where(person =>
                person.FirstName.Contains(searchWord) ||
                person.LastName.Contains(searchWord) ||
                person.PrivateId.Contains(searchWord));
        var pagedSearchQuery = searchQuery
            .Skip((page - 1) * Constants.ItemsOnPage)
            .Take(Constants.ItemsOnPage);
        return await pagedSearchQuery.ToListAsync();
    }

    // Does not implement Unit of work as of now. Use Transaction if needed.
    public async Task AddImageToPerson(int personId, IFormFile imageFile)
    {
        await _imageService.UploadAndSetPhoto(personId, imageFile);
    }
}