using Domain;

namespace Application.Persistance.Contracts;

public class PeopleNumberOfRelationsReportResponse
{
    public List<PersonRelationsReport> PersonRelationsReports { get; set; } = new();
}

public class PersonRelationsReport
{
    public Person Person { get; set; }
    public int Unknown { get; set; }
    public int College { get; set; }
    public int Acquaintance { get; set; }
    public int Relative { get; set; }
}