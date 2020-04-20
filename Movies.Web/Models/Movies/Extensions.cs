using System;
using System.Collections.Generic;
using System.Globalization;
using Schema.NET;

namespace Movies.Web.Models.Movies
{
    public static class Extensions
    {
        public static Schema.NET.Movie ToStructuredData(this Details  model)
        {
            var actors = model.Actors.Split(",");
            var actorsList = new List<Person>();
            foreach (var actor in actors)
            {
                var actorStructured = new Person
                {
                    Name = actor
                };
                actorsList.Add(actorStructured);
            }

            var aggregateRatingList = new List<AggregateRating>
            {
                new AggregateRating
                {
                    Author = new Values<IOrganization, IPerson>(new Organization
                    {
                        Name = "IMDb"
                    }),
                    RatingValue = model.ImdbRating,
                    RatingCount = int.Parse(model.ImdbVotes, NumberStyles.AllowThousands, new CultureInfo("en-au")),
                    BestRating = 10,
                    WorstRating = 0
                }
            };

            var movieStructuredSchema = new Schema.NET.Movie
            {
                Name = model.Title,
                Director = new OneOrMany<IPerson>(new Person
                {
                    Name = model.Director
                }),
                CountryOfOrigin = new OneOrMany<ICountry>(new Country
                {
                    Name = model.Country
                }),
                ContentRating = model.Rated,
                DateCreated = DateTime.Parse(model.Released),
                Actor = new OneOrMany<IPerson>(actorsList),
                Genre = new Values<string, Uri>(model.Genre),
                Award = new OneOrMany<string>(model.Awards),
                AggregateRating = new OneOrMany<IAggregateRating>(aggregateRatingList)
            };

            try
            {
                movieStructuredSchema.Image = new Uri(model.Poster);
                movieStructuredSchema.Url = new Uri($"https://www.imdb.com/title/{model.ImdbID}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error converting to ld+json: {ex.Message}");
            }

            return movieStructuredSchema;
        }
        
        public static Schema.NET.Movie ToStructuredData(this SearchResult  model, int position)
        {
            var movieStructuredSchema = new Schema.NET.Movie
            {
                Name = model.Title,
                Position = position
            };
            try
            {
                movieStructuredSchema.Image = new Uri(model.Poster);
                movieStructuredSchema.Url = new Uri($"https://www.imdb.com/title/{model.ImdbID}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error converting to ld+json: {ex.Message}");
            }

            return movieStructuredSchema;
        }
    }
}