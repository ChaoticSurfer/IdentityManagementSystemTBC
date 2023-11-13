using Domain;

namespace Application.DTOs;

public class PersonDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Sex Sex { get; set; }
    public string PrivateId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public City City { get; set; }
    public int CityId { get; set; }
    public Phone Phone { get; set; }
    public int PhoneId { get; set; }
    public string? ImageRelativeAddress { get; set; }
}