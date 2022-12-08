using Mapster;
using Paraglider.Clients.Gorko.Models;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Domain.NoSQL.ValueObjects;
using System.Linq;

namespace Paraglider.Data.MongoDB.BackgroundUploads.ReccuringJobs.Gorko;

public class GorkoMapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //TODO смаппить сущности
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
            .Ignore(x => x.ManufactureYear!) //?
            .Ignore(x => x.MinRentLength!) //?
            .Ignore(x => x.Capacity!) //?
            .Ignore(x => x.Halls!) 
            .Ignore(x => x.CreatedAt) 
            .Ignore(x => x.UpdatedAt);

        config.NewConfig<List<CatalogMedia>, Album>()
            .Map(x => x.Media, y => new List<Media>(
                y.Select(z => new Media(
                    (MediaType)Enum.Parse(typeof(MediaType), z.Type!), 
                    z.OriginalUrl!))));

        config.NewConfig<Clients.Gorko.Models.Review, Domain.NoSQL.ValueObjects.Review>()
            .Map(x => x.Author, y => y.UserName)
            .Map(x => x.Text, y => y.Text)
            .Map(x => x.Date, y => new DateTime(y.CreatedAtTimestamp))
            .Ignore(x => x.Evaluation) //?
            .Ignore(x => x.AvatarUrl); //?
    }
}
