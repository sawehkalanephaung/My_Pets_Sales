using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using My_Pets_Sales.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;


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

  
    

public async Task<IActionResult> Dashboard()
{
    var client = new HttpClient();

    // Fetch daily sales data
    var dailyRequest = new HttpRequestMessage(HttpMethod.Get, "https://www.melivecode.com/api/pets/2023-01-01");
    var dailyResponse = await client.SendAsync(dailyRequest);
    dailyResponse.EnsureSuccessStatusCode();
    var dailyJsonString = await dailyResponse.Content.ReadAsStringAsync();
    var dailySales = JsonConvert.DeserializeObject<List<Pet>>(dailyJsonString);

    // Fetch weekly sales data
    var weeklyRequest = new HttpRequestMessage(HttpMethod.Get, "https://www.melivecode.com/api/pets/7days/2023-01-01");
    var weeklyResponse = await client.SendAsync(weeklyRequest);
    weeklyResponse.EnsureSuccessStatusCode();
    var weeklyJsonString = await weeklyResponse.Content.ReadAsStringAsync();
    var weeklySales = JsonConvert.DeserializeObject<WeeklySalesData>(weeklyJsonString);

    var viewModel = new DashboardViewModel
    {
        DailySales = dailySales,
        WeeklySales = weeklySales
    };

    return View("Dashboard", viewModel); // Ensure the correct view is specified
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
