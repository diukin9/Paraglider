using MongoDB.Driver;
using Paraglider.Data.MongoDB;
using Paraglider.Domain.Common.Enums;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.NoSQL.Enums;
using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Infrastructure.Common.MongoDB;
using static Paraglider.API.Tests.Common.StaticData;

namespace Paraglider.API.Tests.Common;

public class ComponentDataAccessFactory
{
    public static IMongoDataAccess<Component> Create()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        client.DropDatabase(MongoTestDbName);
        var settings = new MongoDbSettings() { DatabaseName = MongoTestDbName };
        var components = new ComponentDataAccess(client, settings);

        components.AddRange(new List<Component>()
        {
            new Component()
            {
                Id = VideographComponentId,
                ExternalId = Guid.NewGuid().ToString(),
                Provider = nameof(ComponentDataAccessFactory),
                Category = new Category()
                {
                    Id = VideographerCategoryId,
                    Name = VideographerCategoryName
                },
                Name = "Алина Долгопрудная",
                Description = "Видеограф с многолетним опытом!",
                AvatarUrl = "https://clck.ru/32moNK",
                City = new City()
                {
                    Id = DefaultCityId,
                    Name = DefaultCityName
                },
                Album = new Album()
                {
                    Name = "Main album",
                    Media = new List<Media>()
                    {
                        new Media(MediaType.Image, "https://clck.ru/32moUX"),
                        new Media(MediaType.Image, "https://clck.ru/32moVU")
                    }
                },
                Contacts = new Contacts("89193799969", "dolgoprudnaya@paraglider.com", telegram: "dolgoprudka"),
                Reviews = new List<Review>()
                {
                    new Review()
                    {
                        Author = "Ольга Черымшняк",
                        Evaluation = 4,
                        AvatarUrl = "https://clck.ru/32moZS",
                        Text = "Видеограф понравился, но игнорировал некоторые просьбы гостей",
                    }
                },
                Services = new List<Service>()
                {
                    new Service()
                    {
                        Name = "Свадебная видеосъемка (10-12 часов)",
                        Price = new Price(30000, IntervalType.FromTo)
                    },
                    new Service()
                    {
                        Name = "Свадебная видеосъемка без банкета (2-8 часов)",
                        Price = new Price(10000, 20000)
                    }
                }
            },
            new Component()
            {
                Id = PhotographerComponentId,
                ExternalId = Guid.NewGuid().ToString(),
                Provider = nameof(ComponentDataAccessFactory),
                Category = new Category()
                {
                    Id = PhotographerCategoryId,
                    Name = PhotographerCategoryName
                },
                Name = "Анжела Васильевна",
                Description = "Снимаю свадьбы, корпоративы, детские праздники",
                AvatarUrl = "https://clck.ru/32mom4",
                City = new City()
                {
                    Id = DefaultCityId,
                    Name = DefaultCityName
                },
                Album = new Album()
                {
                    Media = new List<Media>()
                    {
                        new Media(MediaType.Image, "https://clck.ru/32monZ")
                    }
                },
                Contacts = new Contacts("89502507525", "photo66@paraglider.com"),
                Reviews = new List<Review>()
                {
                    new Review()
                    {
                        Author = "Наташа Карасева",
                        Evaluation = 5,
                        AvatarUrl = "https://clck.ru/32mopQ",
                        Text = "Очень приятная девушка, а ее работы - просто огонь! Рекомендую",
                    },
                    new Review()
                    {
                        Author = "Женя Шнякина",
                        Evaluation = 5,
                        AvatarUrl = "https://clck.ru/32moqQ",
                        Text = "Работы действительно просто восхитительные",
                    }
                },
                Services = new List<Service>()
                {
                    new Service()
                    {
                        Name = "Услуги свадебного фотографа (весь день)",
                        Price = new Price(25000, IntervalType.From)
                    }
                }
            }
        });

        return components;
    }

    public static void Destroy(IMongoClient client, string mongoDbName)
    {
        client.DropDatabase(mongoDbName);
    }
}
