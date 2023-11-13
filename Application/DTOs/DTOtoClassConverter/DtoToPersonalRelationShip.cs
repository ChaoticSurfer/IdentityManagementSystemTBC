using Domain;

namespace Application.DTOs.DTOtoClassConverter;

public class DtoToPersonalRelationShip
{
    public static PersonRelationShip Convert(PersonRelationshipDTO personRelationshipDto)
    {
        return new PersonRelationShip()
        {
            RelatedFromPersonId = personRelationshipDto.RelatedFromPersonId,
            RelatedToPersonId = personRelationshipDto.RelatedToPersonId,
            RelationType = personRelationshipDto.RelationType
        };
    }

    public static PersonRelationShip ConvertToInverted(PersonRelationshipDTO personRelationshipDto)
    {
        return new PersonRelationShip()
        {
            RelatedFromPersonId = personRelationshipDto.RelatedToPersonId,
            RelatedToPersonId = personRelationshipDto.RelatedFromPersonId,
            RelationType = personRelationshipDto.RelationType
        };
    }
}