using System.Collections.Generic;

namespace Movies.Web.Models.Movies
{
    public class SearchResponse
    {
        public List<SearchResult> Search { get; set; }
        public int TotalResults { get; set; }
        public bool Response { get; set; }
    }
}