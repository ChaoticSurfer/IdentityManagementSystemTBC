using Domain;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServices
{
    public static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<AbstractValidator<Person>, PersonValidator>();
        serviceCollection.AddTransient<AbstractValidator<Phone>, PhoneValidator>();
    }
}