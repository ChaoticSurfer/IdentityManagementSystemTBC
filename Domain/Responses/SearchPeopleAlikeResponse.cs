using Domain;

namespace Application.Persistance.Contracts;

public class SearchPeopleAlikeResponse
{
    public List<Person> PeopleAlike { get; set; }
}