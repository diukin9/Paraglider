using FluentValidation;
using Paraglider.Domain.Common.Enums;
using Paraglider.Domain.NoSQL.Entities;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Validators;

public class RestaurantValidator : AbstractValidator<Component>
{
    public RestaurantValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.ExternalId).NotNull().NotEmpty();
        RuleFor(x => x.Provider).IsEnumName(typeof(Source));
        RuleFor(x => x.Category).NotNull();
        RuleFor(x => x.City).NotNull();
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.AvatarUrl).NotNull().NotEmpty();

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

        RuleFor(x => x.Halls).NotNull().NotEmpty();
        RuleForEach(x => x.Halls).ChildRules(hall =>
        {
            hall.RuleFor(x => x.Name).NotNull().NotEmpty();
            hall.RuleFor(x => x.RentalPrice).NotNull().NotEmpty();
            hall.RuleFor(x => x.Capacity).NotNull().NotEmpty();
            hall.RuleFor(x => x.Album).NotNull().NotEmpty();
            hall.RuleFor(x => x.Album.Media).NotNull();
        });

        RuleFor(x => x.Album).Null();
        RuleFor(x => x.Services).Null();
        RuleFor(x => x.ManufactureYear).Null();
        RuleFor(x => x.MinRentLength).Null();
        RuleFor(x => x.Capacity).Null();
    }
}
