using Mapster;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Domain.RDB.ValueObjects;
using static Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Infrastructure.StaticData;
using static Paraglider.Infrastructure.Common.AppData;
using GorkoModels = Paraglider.Clients.Gorko.Models;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko.Infrastructure;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GorkoModels.Restaurant, Component>()
            .Map(x => x.Id, y => Guid.NewGuid())
            .Map(x => x.ExternalId, y => y.Id.ToString())
            .Map(x => x.Name, y => y.Name)
            .Map(x => x.Halls, y => y.Rooms)
            .Map(x => x.Contacts, y => y.Contacts)
            .Map(x => x.Provider, y => Enum.GetName(Source.Gorko))
            .Map(x => x.Reviews, y => y.Reviews)
            .Map(x => x.Description, y => y.Description)
            .Map(x => x.AvatarUrl, y => y.Avatar ?? DefaultAvatarUrl)
            .Map(x => x.UpdatedAt, y => DateTime.UtcNow)
            .Ignore(x => x.Album!)
            .Ignore(x => x.Capacity!)
            .Ignore(x => x.City)
            .Ignore(x => x.CityId)
            .Ignore(x => x.Category)
            .Ignore(x => x.CategoryId)
            .Ignore(x => x.Services!)
            .Ignore(x => x.ManufactureYear!)
            .Ignore(x => x.MinRentLength!)
            .Ignore(x => x.Status)
            .Ignore(x => x.Rating)
            .Ignore(x => x.Popularity)
            .Ignore(x => x.AlbumId!);

        config.NewConfig<GorkoModels.User, Component>()
            .Map(x => x.Id, y => Guid.NewGuid())
            .Map(x => x.Album, y => y.CatalogMedia)
            .Map(x => x.AvatarUrl, y => y.AvatarUrl)
            .Map(x => x.Provider, y => Enum.GetName(Source.Gorko))
            .Map(x => x.Reviews, y => y.Reviews)
            .Map(x => x.Contacts, y => y.Contacts)
            .Map(x => x.Description, y => y.Text)
            .Map(x => x.ExternalId, y => y.Id.ToString())
            .Map(x => x.Name, y => y.Name)
            .Map(x => x.Services, y => y.Specs != null
                ? y.Specs
                    .Where(z => z.Prices != null)
                    .SelectMany(z => z.Prices!)
                    .ToHashSet()
                : null)
            .Map(x => x.UpdatedAt, y => DateTime.UtcNow)
            .Ignore(x => x.Category)
            .Ignore(x => x.CategoryId)
            .Ignore(x => x.City)
            .Ignore(x => x.CityId)
            .Ignore(x => x.ManufactureYear!)
            .Ignore(x => x.MinRentLength!)
            .Ignore(x => x.Capacity!)
            .Ignore(x => x.Halls!)
            .Ignore(x => x.Status)
            .Ignore(x => x.Rating)
            .Ignore(x => x.Popularity)
            .Ignore(x => x.AlbumId!);

        config.NewConfig<GorkoModels.Room, Hall>()
            .Map(x => x.Id, y => Guid.NewGuid())
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
                        : null
                    : null
            })
            .Map(x => x.Price, y => new HallPrice
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
                : null)
            .Ignore(x => x.AlbumId!);

        config.NewConfig<ICollection<GorkoModels.CatalogMedia>, Album>()
            .Map(x => x.Media, y => y.Select(z => new Media
            {
                Type = Video == z.Type
                    ? MediaType.Video
                    : MediaType.Image,
                Url = z.OriginalUrl!
            }))
            .Map(x => x.Id, y => Guid.NewGuid());

        config.NewConfig<GorkoModels.Review, Review>()
            .Map(x => x.Author, y => string.IsNullOrEmpty(y.UserName)
                ? DefaultUserName
                : y.UserName)
            .Map(x => x.Text, y => y.Text)
            .Map(x => x.AvatarUrl, y => y.UserAvatar ?? DefaultAvatarUrl)
            .Map(x => x.Evaluation, y => y.Marks != null && y.Marks.Any()
                ? y.Marks.Sum(x => x.Rating) / y.Marks.Count
                : 0)
            .Map(x => x.Id, y => Guid.NewGuid());

        config.NewConfig<GorkoModels.Contact, Contact>()
            .Map(x => x.Id, y => Guid.NewGuid())
            .Map(x => x.Key, y => y.Key)
            .Map(x => x.Value, y => y.Value);

        config.NewConfig<GorkoModels.Price, Service>()
            .Map(x => x.Id, y => Guid.NewGuid())
            .Map(x => x.Name, y => y.Title)
            .Map(x => x.Description, y => y.Description)
            .Map(x => x.Price, y => new Price
            {
                Min = y.ValueFrom == decimal.Zero ? null : y.ValueFrom,
                Max = y.ValueTo == decimal.Zero ? null : y.ValueTo
            });
    }
}
