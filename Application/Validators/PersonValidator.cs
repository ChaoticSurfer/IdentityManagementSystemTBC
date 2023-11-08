using System;
using System.Text.RegularExpressions;
using Domain;
using FluentValidation;

namespace Application
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator(AbstractValidator<Phone> phoneValidator)
        {
            RuleFor(person => person.FirstName)
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
                .Custom((s, context) =>
                {
                    if (IsNotValidSymbols(s))
                    {
                        context.AddFailure("First name can only contain Georgian or English symbols.");
                    }
                });

            RuleFor(person => person.LastName)
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
                .Custom((s, context) =>
                {
                    if (IsNotValidSymbols(s))
                    {
                        context.AddFailure("Last name can only contain Georgian or English symbols.");
                    }
                });

            RuleFor(person => person.PrivateId)
                .Length(11).WithMessage("The PrivateId must be exactly 11 characters long.");

            RuleFor(person => person.DateOfBirth).Custom((dateOfBirth, context) =>
            {
                if (IsNotEligibleAge(dateOfBirth))
                {
                    context.AddFailure($"Age must be more than or equal to {Constants.Eligibility.Age}.");
                }
            });

            RuleFor(person => person.Phone).Custom((phone, context) =>
            {
                var validationResult = phoneValidator.Validate(phone);
                if (!validationResult.IsValid)
                {
                    foreach (var failure in validationResult.Errors)
                    {
                        context.AddFailure(failure.ErrorMessage);
                    }
                }
            });
        }

        private static bool IsNotEligibleAge(DateTime dateOfBirth) => !IsEligibleAge(dateOfBirth);

        private static bool IsEligibleAge(DateTime dateOfBirth)
        {
            var borderDate = DateTime.Now.AddYears(-Constants.Eligibility.Age);
            return dateOfBirth <= borderDate;
        }

        private static bool IsNotValidSymbols(string value) => !IsValidSymbols(value);

        private static bool IsValidSymbols(string value)
        {
            var hasEnglish = Regex.IsMatch(value, Constants.LanguageRegexPatterns.English);
            var hasGeorgian = Regex.IsMatch(value, Constants.LanguageRegexPatterns.Georgian);
            if (hasEnglish && hasGeorgian) return false;
            return true;
        }
    }
}