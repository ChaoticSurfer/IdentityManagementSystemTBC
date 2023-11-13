using Domain;

namespace Application.DTOs.DTOtoClassConverter;

public static class DTOtoPerson
{
    public static Person Convert(PersonDTO personDto) =>
        new Person()
        {
            FirstName = personDto.FirstName,
            LastName = personDto.LastName,
            Sex = personDto.Sex,
            PrivateId = personDto.PrivateId,
            DateOfBirth = personDto.DateOfBirth,
            City = personDto.City,
            CityId = personDto.CityId,
            Phone = personDto.Phone,
            PhoneId = personDto.PhoneId,
            ImageRelativeAddress = personDto.ImageRelativeAddress
        };
}