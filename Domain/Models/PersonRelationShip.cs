namespace Domain;

public class PersonRelationShip
{
    public int Id { get; set; }
    public Person RelatedFromPerson { get; set; }
    public int RelatedFromPersonId { get; set; }
    public PersonRelationType RelationType { get; set; }
    public Person RelatedToPerson { get; set; }
    public int RelatedToPersonId { get; set; }
}