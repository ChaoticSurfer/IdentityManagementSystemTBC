using Application;
using Domain;

namespace ApplicationTests;

public class ValidationTests
{
    [Theory]
    [TestCase("Anri", "Kezeroti", true)]
    [TestCase("ააააaaaa", "Kezeroti", false)]
    public void LanguageTests(string name, string surname, bool passedIsValid)
    {
        var validator = new PersonValidator(new PhoneValidator());
        var phone = new Phone() { PhoneNumber = "12345678" };
        var person = new Person() { FirstName = name, LastName = surname, Phone = phone };
        var result = validator.Validate(person);
        var isValid = result.IsValid;
        Assert.AreEqual(passedIsValid, isValid);
    }
}