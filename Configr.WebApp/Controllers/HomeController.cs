using Configr.Business.Abstract;
using Configr.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Configr.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfigurationDatasService _configurationDatasService;

        public HomeController(ILogger<HomeController> logger, IConfigurationDatasService configurationDatasService)
        {
            _logger = logger;
            _configurationDatasService = configurationDatasService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var test = await _configurationDatasService.GetAll();
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
}