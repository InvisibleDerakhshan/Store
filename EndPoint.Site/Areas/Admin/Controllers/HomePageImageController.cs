﻿using Bugeto_Store.Application.Services.HomePages.AddHomePageImages;
using Bugeto_Store.Domain.Entities.HomePages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EndPoint.Site.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HomePageImageController : Controller
    {
        private readonly IAddHomePageImagesService _addHomePageImagesService;

        public HomePageImageController(IAddHomePageImagesService addHomePageImagesService)
        {
            _addHomePageImagesService = addHomePageImagesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file, string link, ImageLocation imageLocation)
        {
            _addHomePageImagesService.Execute(new requestAddHomePageImagesDto
             {
                file = file,
                ImageLocation = imageLocation,
                Link = link
            });
            return View();
        }
    }
}
