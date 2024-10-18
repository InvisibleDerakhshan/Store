using Bugeto_Store.Application.Services.Common.Queries.GetHomePageImages;
using Bugeto_Store.Application.Services.Common.Queries.GetSlider;
using System.Collections.Generic;
using static Bugeto_Store.Application.Services.Products.Queries.GetProductForSite.ResultProductForSiteDto;

namespace EndPoint.Site.Models.ViewModels.HomePages
{
    public class HomePageViewModel
    {
        public List<SliderDto> Sliders { get; set; }
        public List<HomePageImagesdto> PageImages{ get; set; }
        public List<ProductForSiteDto> Camera {  get; set; }
        public List<ProductForSiteDto> Mobile {  get; set; }
        public List<ProductForSiteDto> Laptop {  get; set; }

    }
}
