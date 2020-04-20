using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Movies.Web.Models.Movies
{
    public class SearchService
    {
        private static string _apiKey;

        public SearchService(string apiKey)
        {
            _apiKey = apiKey;
        }
        public async Task<SearchResponse> SearchMovies(string keySearch, string searchYear, int pageNumber)
        {
            var query = $"s={keySearch}";
            if (!string.IsNullOrWhiteSpace(searchYear))
            {
                query = $"{query}&y={searchYear}";
            }
            
            query = $"{query}&page={pageNumber}";
            
            var movies = await GetData<SearchResponse>(query);

            return movies;
        }
        
        public async Task<Details> GetMovieDetails(string id)
        {
            var query = $"i={id}";
            var details = await GetData<Details>(query);

            return details;
        }

        private static async Task<T> GetData<T>(string query = null)
            where T : class
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var endpoint = $"https://www.omdbapi.com/?apikey={_apiKey}&{query}";

                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    var response = await httpClient.GetAsync(new Uri(endpoint));

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(
                            $"Fail to send get request to movies API.  query: {query} response: {response}");
                        return null;
                    }

                    return await GetResponse<T>(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fail to communicate with movies API {e.Message}. Query: {query}");

                throw;
            }
        }
        
        private static async Task<T> GetResponse<T>(HttpResponseMessage response)
        {
            var bodyStr = await response.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<T>(bodyStr);

            return obj;
        }
    }
}