using Bugeto_Store.Application.Services.Common.Queries.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenu :ViewComponent
    {
        private readonly IGetMenuItemService _getmenuItemService;

        public GetMenu(IGetMenuItemService getmenuItemService)
        {
            _getmenuItemService = getmenuItemService;
        }


        public IViewComponentResult Invoke()
        {
            var menuItem = _getmenuItemService.Execute();
            return View(viewName: "GetMenu", menuItem.Data);
        }
    }
}
