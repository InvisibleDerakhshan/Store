using Bugeto_Store.Application.Services.Carts;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartService _cartService;
        private readonly CookiesManeger cookiesManeger;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            cookiesManeger = new CookiesManeger();
        }

        public IActionResult Index()
        {
            var userId=  ClaimUtility.GetUserId(User);

            var resultGetLst = _cartService.GetMyCart(cookiesManeger.GetBrowserId(HttpContext),0);
            return View(resultGetLst.Data);
        }

        public IActionResult AddToCart(long ProductId)
        {

            var resultAdd = _cartService.AddToCart(ProductId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        public IActionResult Add(long cartItemId)
        {
            _cartService.Add(cartItemId);
            return RedirectToAction("Index");
        }
        public IActionResult LowOff(long cartItemId)
        {
            _cartService.LowOff(cartItemId);
            return RedirectToAction("Index");
        }
        public IActionResult Remove(long ProductId)
        {
            _cartService.RemoveFromCart(ProductId, cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }
    }
}
