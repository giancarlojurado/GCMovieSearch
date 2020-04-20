using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Movies.Web.Models.Movies
{
    public class SearchResults
    {
        public IList<SearchResult> Results { get; set; } = new List<SearchResult>();
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

        //TODO: Delete this. This is just a place holder method to show data displaying
        public void LoadSampleData(string keySearch)
        {
            Results = new List<SearchResult>
            {
                new SearchResult
                {
                    Title = "The Avengers SUPER GC",
                    Year = "2012",
                    Poster = "https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg"
                },
                new SearchResult
                {
                    Title = "Avengers: Infinity War",
                    Year = "2018",
                    Poster = "https://m.media-amazon.com/images/M/MV5BMjMxNjY2MDU1OV5BMl5BanBnXkFtZTgwNzY1MTUwNTM@._V1_SX300.jpg"
                },
                new SearchResult
                {
                    Title = "Avengers: Age of Ultron",
                    Year = "2015",
                    Poster = "https://m.media-amazon.com/images/M/MV5BMTM4OGJmNWMtOTM4Ni00NTE3LTg3MDItZmQxYjc4N2JhNmUxXkEyXkFqcGdeQXVyNTgzMDMzMTg@._V1_SX300.jpg"
                },
                new SearchResult
                {
                    Title = "Avengers: Endgame",
                    Year = "2019",
                    Poster = "https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_SX300.jpg"
                },
            }; 
        }
    }
}