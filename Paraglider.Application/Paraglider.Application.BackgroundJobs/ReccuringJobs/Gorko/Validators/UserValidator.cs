using FluentValidation;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Validators;

public class UserValidator : AbstractValidator<Component>
{
    public UserValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.ExternalId).NotNull().NotEmpty();
        RuleFor(x => x.Provider).IsEnumName(typeof(Source));
        RuleFor(x => x.Category).NotNull();
        RuleFor(x => x.City).NotNull();
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.AvatarUrl).NotNull().NotEmpty();

        RuleFor(x => x.Album).NotNull().NotEmpty();
        RuleFor(x => x.Album.Media).NotNull().NotEmpty();
        RuleForEach(x => x.Album.Media).ChildRules(media =>
        {
            media.RuleFor(x => x.Url).NotNull().NotEmpty();
        });

        RuleFor(x => x.Contacts).NotNull().NotEmpty();
        RuleFor(x => x.Contacts.PhoneNumbers).NotNull().NotEmpty();
        RuleForEach(x => x.Contacts.PhoneNumbers).ChildRules(phone =>
        {
            phone.RuleFor(x => x).NotNull().NotEmpty();
        });

        RuleFor(x => x.Reviews).NotNull();
        RuleForEach(x => x.Reviews).ChildRules(review =>
        {
            review.RuleFor(x => x.Evaluation).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10);
            review.RuleFor(x => x.Author).NotNull().NotEmpty();
            review.RuleFor(x => x.AvatarUrl).NotNull().NotEmpty();
        });

        RuleFor(x => x.Services).NotNull();
        RuleForEach(x => x.Services).ChildRules(service =>
        {
            service.RuleFor(x => x.Name).NotNull().NotEmpty();
            service.RuleFor(x => x.Price).NotNull();
        });

        RuleFor(x => x.ManufactureYear).Null();
        RuleFor(x => x.MinRentLength).Null();
        RuleFor(x => x.Capacity).Null();
        RuleFor(x => x.Halls).Null();
    }
}
