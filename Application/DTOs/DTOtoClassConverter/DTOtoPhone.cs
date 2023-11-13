using Domain;

namespace Application.DTOs.DTOtoClassConverter;

public class DTOtoPhone
{
    public static Phone Convert(PhoneDTO phoneDto) =>
        new Phone()
        {
            PhoneNumber = phoneDto.PhoneNumber,
            PhoneType = phoneDto.PhoneType
        };
}