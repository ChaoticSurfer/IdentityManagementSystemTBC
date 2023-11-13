using Domain;

namespace Application.DTOs.DTOtoClassConverter;

public class DTOtoCity
{
    public static City Convert(CityDto cityDto) =>
        new City() { Name = cityDto.Name };
}