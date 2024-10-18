using Bugeto_Store.Application.Interfaces.FacadPatterns;
using Bugeto_Store.Application.Services.Common.Queries.GetHomePageImages;
using Bugeto_Store.Application.Services.Common.Queries.GetSlider;
using Bugeto_Store.Application.Services.Products.Queries.GetProductForSite;
using EndPoint.Site.Models;
using EndPoint.Site.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetSliderService _getSliderService;
        private readonly IGetHomePageImageService _getHomePageImageService;
        private readonly IProductFacad _productFacad;
        public HomeController(ILogger<HomeController> logger, IGetSliderService getSliderService, IGetHomePageImageService getHomePageImageService, IProductFacad productFacad)
        {
            _logger = logger;
            _getSliderService = getSliderService;
            _getHomePageImageService = getHomePageImageService;
            _productFacad = productFacad;
        }

        public IActionResult Index()
        {
            HomePageViewModel homePage =new HomePageViewModel()
            {
                Sliders = _getSliderService.Excute().Data,
                PageImages = _getHomePageImageService.Execcute().Data,
                Camera = _productFacad.GetProductForSiteService.Execute(Ordering.theNewest
                , null, 1, 6,3 ).Data.Products,
            };
            return View(homePage);
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
