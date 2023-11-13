using Application.DTOs;
using Application.DTOs.DTOtoClassConverter;
using Domain;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IPersonRepository _personRepository;
    private readonly AbstractValidator<Person> _personValidator;

    public PersonController(
        ILogger<PersonController> logger,
        IPersonRepository personRepository,
        AbstractValidator<Person> personValidator
    )
    {
        _logger = logger;
        _personRepository = personRepository;
        _personValidator = personValidator;
    }

    [HttpGet("{personId}")]
    public async Task<ActionResult<Person>> Get(int personId)
    {
        var person = await _personRepository.GetPerson(personId);
        if (person == null) return NotFound();
        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult> Add(PersonDTO personDto)
    {
        var person = DTOtoPerson.Convert(personDto);
        var validationResult = _personValidator.Validate(person);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _personRepository.AddPerson(person);
        await _personRepository.Save();

        return CreatedAtAction("Get", new { personId = person.Id }, person);
    }

    [HttpPut("{personId}")]
    public async Task<ActionResult> Update(Person person)
    {
        var validationResult = _personValidator.Validate(person);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        _personRepository.UpdatePerson(person);
        await _personRepository.Save();

        return this.Ok();
    }

    [HttpDelete("{personId}")]
    public async Task<ActionResult> Delete(int personId)
    {
        var person = await _personRepository.GetPerson(personId);

        if (person == null)
        {
            return NotFound();
        }

        _personRepository.RemovePerson(person);
        await _personRepository.Save();

        return Ok();
    }
}