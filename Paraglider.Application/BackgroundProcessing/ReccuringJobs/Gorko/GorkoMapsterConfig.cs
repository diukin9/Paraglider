using Mapster;
using Paraglider.Clients.Gorko.Models;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.API.BackgroundProcessing.ReccuringJobs.Gorko;

public class GorkoMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Restaurant, Component>()
            .Map(x => x.Id, y => $"{Enum.GetName(Source.Gorko)}:{y.Id}")
            .Map(x => x.ExternalId, y => y.Id)
            .Map(x => x.Name, y => y.Name)
            .Map(x => x.Halls, y => y.Rooms)
            .Map(x => x.Contacts, y => y.Contacts)
            .Map(x => x.Provider, y => Enum.GetName(Source.Gorko))
            .Map(x => x.Reviews, y => y.Reviews)

            .Ignore(x => x.Description) //?
            .Ignore(x => x.Album) //?
            .Ignore(x => x.AvatarUrl)
            .Ignore(x => x.Capacity)
            .Ignore(x => x.City)
            .Ignore(x => x.Category)
            .Ignore(x => x.Services)
            .Ignore(x => x.ManufactureYear!)
            .Ignore(x => x.MinRentLength!)
            .Ignore(x => x.CreatedAt)
            .Ignore(x => x.UpdatedAt);

        config.NewConfig<User, Component>()
            .Map(x => x.Id, y => $"{Enum.GetName(Source.Gorko)}:{y.Id}")
            .Map(x => x.Album, y => y.CatalogMedia)
            .Map(x => x.AvatarUrl, y => y.AvatarUrl)
            .Map(x => x.Provider, y => Enum.GetName(Source.Gorko))
            .Map(x => x.Reviews, y => y.Reviews)
            .Map(x => x.Contacts, y => y.Contacts)
            .Map(x => x.Description, y => y.Text)
            .Map(x => x.ExternalId, y => y.Id)
            .Map(x => x.Name, y => y.Name)
            .Map(x => x.Services, y => y.Specs!.Capacity)

            .Ignore(x => x.Category)
            .Ignore(x => x.City)
            .Ignore(x => x.ManufactureYear!)
            .Ignore(x => x.MinRentLength!)
            .Ignore(x => x.Capacity!)
            .Ignore(x => x.Halls!) 
            .Ignore(x => x.CreatedAt) 
            .Ignore(x => x.UpdatedAt);

        config.NewConfig<Room, Hall>()
            .Map(x => x.Name, y => y.Name)
            .Map(x => x.Album, y => y.Media)
            .Map(x => x.Capacity, y => new Capacity(y.Parameters!.CapacityMin!.Value, y.Parameters!.CapacityMax!.Value))
            .Map(x => x.RentalPrice, y => new HallRentalPrice(y.Parameters!.PricePerPerson!.Value, HallRentalPriceType.PricePerPerson)) //TODO не передается rentalPrice
            .Ignore(x => x.Description) //?
            .Ignore(x => x.MinimalPrice); //?

        config.NewConfig<IReadOnlyCollection<CatalogMedia>, Album>()
            .Map(x => x.Media, y => y.Select(z => new Media(GorkoStaticData.Video == z.Type ? MediaType.Video : MediaType.Image, z.OriginalUrl!)));

        config.NewConfig<Clients.Gorko.Models.Review, Domain.NoSQL.ValueObjects.Review>()
            .Map(x => x.Author, y => y.UserName)
            .Map(x => x.Text, y => y.Text)
            .Map(x => x.Date, y => new DateTime(y.CreatedAtTimestamp))
            .Ignore(x => x.Evaluation) //?
            .Ignore(x => x.AvatarUrl); //?

        config.NewConfig<IReadOnlyCollection<Contact>, Contacts>()
            .Map(x => x.Emails, y => y.Where(x => x.Key == GorkoStaticData.Email).Select(x => x.Value).ToList())
            .Map(x => x.PhoneNumbers, y => y.Where(x => x.Key == GorkoStaticData.PhoneNumber).Select(x => x.Value).ToList());
    }
}
