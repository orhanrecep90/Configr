using Configr.Business.Abstract;
using Configr.Entities.Concrete;
using Configr.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ListData(string Name)
        {
            List<ConfigurationDatas> dataList = new List<ConfigurationDatas>();
            if (string.IsNullOrEmpty(Name))
                dataList = await _configurationDatasService.GetAll();
            else
                dataList = await _configurationDatasService.GetAllByName(Name);
            return View(dataList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConfigurationDatas configurationDatas)
        {
            if (ModelState.IsValid)
            {
                var result = await _configurationDatasService.Add(configurationDatas);
                return RedirectToAction(nameof(Index));
            }
            return View(configurationDatas);
        }

        public async Task<IActionResult> Update(int id)
        {
            var confData = await _configurationDatasService.GetById(id);

            return View(confData);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ConfigurationDatas data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
           var result= await _configurationDatasService.Update(data);

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Delete(int id)
        {
           var result= await _configurationDatasService.Delete(id);

            return RedirectToAction(nameof(Index));
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