using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movies.Web.Models.Movies;
using Schema.NET;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly string _apiKey;

        public MoviesController(IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("ApiKey").Value;
        }

        public async Task<IActionResult> Index(string searchString, string searchYear, int? pageNumber)
        {
            var model = new SearchResults();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                ViewData["SearchString"] = searchString;
                ViewData["SearchYear"] = searchYear;
                
                TempData["SearchString"] = searchString;
                TempData["SearchYear"] = searchYear;
                TempData["pageNumber"] = pageNumber ?? 1;

                var searchService = new SearchService(_apiKey);
                
                var moviesResponse = await searchService.SearchMovies(searchString, searchYear, pageNumber ?? 1);
                if (moviesResponse.Response)
                {
                    model.Results = moviesResponse.Search;
                    model.TotalItems = moviesResponse.TotalResults;
                    model.TotalPages = moviesResponse.TotalResults / 10;
                    model.CurrentPage = pageNumber ?? 1;

                    var structuredMovies = moviesResponse.Search.Select((t, i) => t.ToStructuredData(i)).ToList();

                    var structuredList = new Schema.NET.ItemList
                    {
                        ItemListElement = new Values<IListItem, string, IThing>(structuredMovies)
                    };

                    ViewBag.JsonLd = structuredList.ToString();
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            
            var searchService = new SearchService(_apiKey);
            var model = await searchService.GetMovieDetails(id);

            ViewBag.JsonLd = model.ToStructuredData().ToString();

            return View(model);
        }
    }
}