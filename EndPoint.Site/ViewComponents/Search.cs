using Bugeto_Store.Application.Services.Common.Queries.GetCategory;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class Search: ViewComponent
    {
        private readonly IGetCategoryService _getcategoryService;

        public Search(IGetCategoryService getcategoryService)
        {
            _getcategoryService = getcategoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(viewName:"Search" , _getcategoryService.Execute().Data);
        }
    }
}
