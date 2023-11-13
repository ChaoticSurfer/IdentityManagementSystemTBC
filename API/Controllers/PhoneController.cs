using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.DTOtoClassConverter;
using Domain;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PhoneController : ControllerBase
{
    private readonly ILogger<PhoneController> _logger;
    private readonly IPhoneRepository _phoneRepository;
    private readonly AbstractValidator<Phone> _phoneValidator;

    public PhoneController(
        ILogger<PhoneController> logger,
        IPhoneRepository phoneRepository,
        AbstractValidator<Phone> phoneValidator
    )
    {
        _logger = logger;
        _phoneRepository = phoneRepository;
        _phoneValidator = phoneValidator;
    }

    [HttpGet("{phoneId}")]
    public async Task<ActionResult<Phone>> Get(int phoneId)
    {
        var phone = await _phoneRepository.GetById(phoneId);
        if (phone == null) return NotFound();
        return Ok(phone);
    }

    [HttpPost]
    public async Task<ActionResult> Add(PhoneDTO phoneDto)
    {
        var phone = DTOtoPhone.Convert(phoneDto);
        var validationResult = _phoneValidator.Validate(phone);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        await _phoneRepository.Add(phone);
        await _phoneRepository.SaveChanges();

        return CreatedAtAction("Get", new { phoneId = phone.Id }, phone);
    }

    [HttpPut("{phoneId}")]
    public async Task<ActionResult> Update(Phone phone)
    {
        var validationResult = _phoneValidator.Validate(phone);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        _phoneRepository.Update(phone);
        await _phoneRepository.SaveChanges();

        return Ok();
    }

    [HttpDelete("{phoneId}")]
    public async Task<ActionResult> Delete(int phoneId)
    {
        var phone = await _phoneRepository.GetById(phoneId);

        if (phone == null)
        {
            return NotFound();
        }

        _phoneRepository.Delete(phone);
        await _phoneRepository.SaveChanges();

        return Ok();
    }
}