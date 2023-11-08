using Domain;

namespace Application.DTOs;

public class PersonRelationshipDTO
{
    public int RelatedFromPersonId { get; set; }
    public int RelatedToPersonId { get; set; }
    public PersonRelationType RelationType { get; set; }
}