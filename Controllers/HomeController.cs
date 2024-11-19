using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using My_Pets_Sales.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace My_Pets_Sales.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    // inject the PetCollection model
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
          _httpClient = httpClientFactory.CreateClient();
    }

// make sure  correctly fetching the data and passing it to the view.
   public async Task<IActionResult> Dashboard()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://www.melivecode.com/api/pets/2023-01-01");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Log the API response for debugging
                _logger.LogInformation("API Response: {Content}", content);
                // Deserialize the JSON response with case-insensitivity
                var pets = JsonSerializer.Deserialize<List<Pet>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(pets);
            }
            else
            {
                _logger.LogWarning("Failed to fetch data: {StatusCode}", response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching pet data");
        }
        return View(new List<Pet>());
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
