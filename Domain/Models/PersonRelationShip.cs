namespace Domain;

public class PersonRelationShip
{
    public int Id { get; set; }
    public Person RelatedFromPerson { get; set; }
    public PersonRelationType RelationType { get; set; }
    public Person RelatedToPerson { get; set; }
}