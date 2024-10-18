
using Bugeto_Store.Common.Dto;
using Bugeto_Store.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Strore.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugeto_Store.Application.Services.Carts
{
    public interface ICartService
    {
        ResultDto AddToCart(long ProductId, Guid BrowserId);
        ResultDto RemoveFromCart(long ProductId, Guid BrowserId);
        ResultDto<CartDto> GetMyCart(Guid BrowserId, long? UserId);
        ResultDto Add(long CartItemId);
        ResultDto LowOff(long CartItemId);
    }

    public class CartServices : ICartService
    {
        private readonly IDataBaseContext _context;

        public CartServices(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Add(long CartItemId)
        {
            var cartItem = _context.CartItems.Find(CartItemId);
            cartItem.Count++;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
            };
        }

        public ResultDto AddToCart(long ProductId, Guid BrowserId)
        {
            var cart = _context.Carts.Where(p => p.BrowserId == BrowserId && p.Finished == false).FirstOrDefault();
            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    Finished = false,
                    BrowserId = BrowserId,
                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();
                cart = newCart;
            }

            var product = _context.Products.Find(ProductId);

            var cartItem = _context.CartItems.Where(p => p.ProductId == ProductId && p.CartId == cart.Id).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Count++;
            }
            else
            {
                CartItem newCartItem = new CartItem()
                {
                    Cart = cart,
                    Count = 1,
                    price = product.Price,
                    Product = product,

                };
                _context.CartItems.Add(newCartItem);
                _context.SaveChanges();
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"محصول {product.Name} با موفقیت به سبد خرید شما اصافه شد  "
            };
        }

        public ResultDto<CartDto> GetMyCart(Guid BrowserId, long? UserId)
        {
            var cart = _context.Carts
                .Include(p => p.CartItems)
                .ThenInclude(p=> p.Product)
                .ThenInclude(p=> p.ProductImages)
                .Where(p => p.BrowserId == BrowserId && p.Finished == false)
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if(UserId != null)
            {
                var user = _context.Users.Find(UserId);
                cart.User = user;
                _context.SaveChanges();
            }
            return new ResultDto<CartDto>()
            {
                Data = new CartDto()
                {
                    ProductCount = cart.CartItems.Count(),
                    SumAmount= cart.CartItems.Sum(p=> p.price * p.Count),
                    CartItems = cart.CartItems.Select(p => new CartItemDto
                    {
                        Count = p.Count,
                        Price = p.price,
                        Product = p.Product.Name,
                        Id = p.Id,
                        Images=p.Product?.ProductImages?.FirstOrDefault()?.Src??"",
                    }).ToList(),
                },
                IsSuccess = true
            };
                
        }

        public ResultDto LowOff(long CartItemId)
        {
            var cartItem = _context.CartItems.Find(CartItemId);
            if (cartItem.Count <= 1)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                };
            }
            cartItem.Count--;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
            };
        }

        public ResultDto RemoveFromCart(long ProductId, Guid BrowserId)
        {
            var cartitem = _context.CartItems.Where(p => p.Cart.BrowserId == BrowserId).FirstOrDefault();
            if (cartitem != null)
            {
                cartitem.IsRemoved = true;
                cartitem.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "محصول از سبد خرید شما حذف شد"
                };
            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد "
                };
            }
        }
    }

    public class CartDto
    {
        public long ProductCount { get; set; }
        public int SumAmount { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public long Id { get; set; }
        public string Product { get; set; }
        public string Images { get; set; }
        public long Count { get; set; }
        public long Price { get; set; }
    }

}
