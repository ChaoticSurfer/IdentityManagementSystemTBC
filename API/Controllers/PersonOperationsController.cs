using System.Runtime.CompilerServices;
using Application;
using Application.DTOs;
using Application.Persistance.Contracts;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
public class PersonOperationsController : ControllerBase
{
    private readonly IPersonRepository _personRepository;
    private readonly AbstractValidator<Person> _personValidator;

    public PersonOperationsController(
        IPersonRepository personRepository,
        AbstractValidator<Person> personValidator)
    {
        _personRepository = personRepository;
        _personValidator = personValidator;
    }

    [HttpPost]
    public async Task<ActionResult> AddRelatedPerson(PersonRelationshipDTO personRelationshipDto)
    {
        var relatedPersonFrom = await _personRepository.GetPerson(personRelationshipDto.RelatedFromPersonId);
        var relatedPersonTo = await _personRepository.GetPerson(personRelationshipDto.RelatedToPersonId);
        if (relatedPersonFrom == null) return NotFound("Person Related From not found");
        if (relatedPersonTo == null) return NotFound("Person Related To not found");

        var relationship = new PersonRelationShip()
        {
            RelationType = personRelationshipDto.RelationType,
            RelatedFromPerson = relatedPersonFrom,
            RelatedToPerson = relatedPersonTo
        };
        await _personRepository.AddPersonRelationship(relationship);
        await _personRepository.Save();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> RemoveRelatedPerson(PersonRelationshipDTO personRelationshipDto)
    {
        var relatedPersonFrom = await _personRepository.GetPerson(personRelationshipDto.RelatedFromPersonId);
        var relatedPersonTo = await _personRepository.GetPerson(personRelationshipDto.RelatedToPersonId);
        if (relatedPersonFrom == null) return NotFound("Person Related From not found");
        if (relatedPersonTo == null) return NotFound("Person Related To not found");

        var relationship = new PersonRelationShip()
        {
            RelationType = personRelationshipDto.RelationType,
            RelatedFromPerson = relatedPersonFrom,
            RelatedToPerson = relatedPersonTo
        };

        await _personRepository.RemoveRelatedPerson(relationship);
        await _personRepository.Save();
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<PeopleNumberOfRelationsReportResponse>> GetReportNumberOfRelationsOfPeople()
    {
        return Ok(await _personRepository.GetReportNumberOfRelationsOfPeople());
    }

    [HttpGet]
    public async Task<ActionResult<SearchPeopleAlikeResponse>> SearchPeopleAlike(string searchWord, int page = 1)
    {
        return Ok(await _personRepository.SearchPeopleAlike(searchWord, page));
    }
}