using Application.Persistance.Contracts;
using Domain;
using Persistance;

namespace API.Middleware;

public class DataSeedingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IPersonRepository _personRepository;
    private readonly PersonDbContext _personDbContext;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public DataSeedingMiddleware(
        RequestDelegate next,
        IPersonRepository personRepository,
        PersonDbContext personDbContext
    )
    {
        _next = next;
        _personRepository = personRepository;
        _personDbContext = personDbContext;
        this._logger = _logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      //  !_personDbContext.Cities.Any();
        if (!_personDbContext.People.Any() && !_personDbContext.Cities.Any())
        {
            var city = new City() { Name = "Tbilisi" };
            var phone = new Phone() { PhoneNumber = "59815657", PhoneType = PhoneType.Mobile };
            var person = new Person() { FirstName = "Anri", LastName = "Kezeroti", Sex = Sex.Male };
            await _personRepository.AddPerson(person);
        }

        await _next(httpContext);
    }
}