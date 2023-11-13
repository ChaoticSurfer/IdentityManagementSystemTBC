namespace Domain.Responses;

public class PeopleNumberOfRelationsReportResponse
{
    public IEnumerable<PersonRelationsReport> PersonRelationsReports { get; set; }
}

public class PersonRelationsReport
{
    public int PersonId { get; set; }

    public IEnumerable<RelationshipNumberTracker> Statistics { get; set; }
}

public class RelationshipNumberTracker
{
    public PersonRelationType RelationType { get; set; }
    public int Count { get; set; }
}