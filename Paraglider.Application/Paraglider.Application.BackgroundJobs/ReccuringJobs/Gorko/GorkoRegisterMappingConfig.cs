using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Domain.RDB.ValueObjects;
using Paraglider.Infrastructure.Common.Extensions;
using GorkoModels = Paraglider.Clients.Gorko.Models;
using static Paraglider.Infrastructure.Common.AppData;
using static Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.GorkoStaticData;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;

public class GorkoRegisterMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GorkoModels.Restaurant, Component>()
            .Map(x => x.Id, y => $"{Enum.GetName(Source.Gorko)}:{y.Id}")
            .Map(x => x.ExternalId, y => y.Id)
            .Map(x => x.Name, y => y.Name)
            .Map(x => x.Halls, y => y.Rooms)
            .Map(x => x.Contacts, y => y.Contacts)
            .Map(x => x.Provider, y => Enum.GetName(Source.Gorko))
            .Map(x => x.Reviews, y => y.Reviews)
            .Map(x => x.Description, y => y.Description)
            .Map(x => x.AvatarUrl, y => y.Avatar ?? DefaultAvatarUrl)
            .Ignore(x => x.Album)
            .Ignore(x => x.Capacity!)
            .Ignore(x => x.City)
            .Ignore(x => x.Category)
            .Ignore(x => x.Services!)
            .Ignore(x => x.ManufactureYear!)
            .Ignore(x => x.MinRentLength!)
            .Ignore(x => x.UpdatedAt)
            .Ignore(x => x.Status)
            .Ignore(x => x.Rating)
            .Ignore(x => x.Popularity);

        config.NewConfig<GorkoModels.User, Component>()
            .Map(x => x.Id, y => $"{Enum.GetName(Source.Gorko)}:{y.Id}")
            .Map(x => x.Album, y => y.CatalogMedia)
            .Map(x => x.AvatarUrl, y => y.AvatarUrl)
            .Map(x => x.Provider, y => Enum.GetName(Source.Gorko))
            .Map(x => x.Reviews, y => y.Reviews)
            .Map(x => x.Contacts, y => y.Contacts)
            .Map(x => x.Description, y => y.Text)
            .Map(x => x.ExternalId, y => y.Id)
            .Map(x => x.Name, y => y.Name)
            .Map(x => x.Services, y => y.Specs != null
                ? y.Specs
                    .Where(z => z.Prices != null)
                    .SelectMany(z => z.Prices!)
                    .ToHashSet()
                : null)
            .Ignore(x => x.Category)
            .Ignore(x => x.City)
            .Ignore(x => x.ManufactureYear!)
            .Ignore(x => x.MinRentLength!)
            .Ignore(x => x.Capacity!)
            .Ignore(x => x.Halls!)
            .Ignore(x => x.UpdatedAt)
            .Ignore(x => x.Status)
            .Ignore(x => x.Rating)
            .Ignore(x => x.Popularity);

        config.NewConfig<GorkoModels.Room, Hall>()
            .Map(x => x.Name, y => y.Name)
            .Map(x => x.Album, y => y.Media)
            .Map(x => x.Description, y => y.Parameters != null
                ? y.Parameters.Features != null 
                    ? y.Parameters.Features.Value
                    : null
                : null)
            .Map(x => x.Capacity, y => new Capacity
            {
                Min = y.Parameters != null
                    ? y.Parameters.CapacityMin != null
                        ? y.Parameters.CapacityMin.Value
                        : null
                    : null,
                Max = y.Parameters != null
                    ? y.Parameters.CapacityMax != null
                        ? y.Parameters.CapacityMax.Value
                        :null
                    : null
            })
            .Map(x => x.RentalPrice, y => new HallRentalPrice
            {
                PricePerPerson = y.Parameters != null
                    ? y.Parameters.PricePerPerson != null
                        ? y.Parameters.PricePerPerson.Value
                        : null
                    : null,
                RentalPrice = y.Parameters != null 
                    ? y.Parameters.RentalPrice != null
                        ? y.Parameters.RentalPrice.Value
                        : null
                    : null
            })
            .Map(x => x.MinimalPrice, y => y.Parameters != null
                ? y.Parameters.MininalPrice != null
                    ? y.Parameters.MininalPrice.Value
                    : null
                : null);

        config.NewConfig<ICollection<GorkoModels.CatalogMedia>, Album>()
            .Map(x => x.Media, y => y.Select(z => new Media
            {
                Type = Video == z.Type
                    ? MediaType.Video
                    : MediaType.Image,
                Url = z.OriginalUrl!
            }));

        config.NewConfig<GorkoModels.Review, Review>()
            .Map(x => x.Author, y => string.IsNullOrEmpty(y.UserName) 
                ? DefaultUserName
                : y.UserName)
            .Map(x => x.Text, y => y.Text)
            .Map(x => x.AvatarUrl, y => y.UserAvatar ?? DefaultAvatarUrl)
            .Map(x => x.Evaluation, y => y.Marks != null && y.Marks.Any()
                ? y.Marks.Sum(x => x.Rating) / y.Marks.Count
                : 0);

        config.NewConfig<ICollection<GorkoModels.Contact>, ICollection<Contact>>()
            .Map(x => x, y => new List<Contact>()
                .Concat(
                    y.Where(z => !string.IsNullOrEmpty(z.Value) 
                    && (z.Key == Email || z.Key == Telegram || z.Key == WhatsApp))
                    .Select(z => new Contact() { Key = z.Key!, Value = z.Value! })
                    .ToList())
                .Concat(
                    y.Where(z => z.Key == PhoneNumber && !string.IsNullOrEmpty(z.Value))
                    .Select(z => new Contact() { Key = z.Key!, Value = z.Value!.ToPhoneNumberOrDefault()! })
                    .ToList()));

        config.NewConfig<GorkoModels.Price, Service>()
            .Map(x => x.Name, y => y.Title)
            .Map(x => x.Description, y => y.Description)
            .Map(x => x.Price, y => new Price
            {
                Min = y.ValueFrom,
                Max = y.ValueTo
            });
    }
}
