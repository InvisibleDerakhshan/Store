using Bugeto_Store.Common;
using Bugeto_Store.Common.Dto;
using Microsoft.EntityFrameworkCore;
using Strore.Application.Interfaces.Contexts;
using System;
using System.Linq;
using static Bugeto_Store.Application.Services.Products.Queries.GetProductForSite.ResultProductForSiteDto;

namespace Bugeto_Store.Application.Services.Products.Queries.GetProductForSite
{
    public class GetProductForSiteService : IGetProductForSiteService
    {
        private IDataBaseContext _context;

        public GetProductForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, string Searchkey, int Page,int pageSize ,long? CatId)
        {
            int totalRow = 0;
            var productQuery = _context.Products
                .Include(p => p.ProductImages).AsQueryable();

            if(CatId != null)
            {
                productQuery=productQuery.Where(p=>p.CategoryId == CatId ||p.Category.ParentCategoryId==CatId).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(Searchkey))
            {
                productQuery=productQuery.Where(p=>p.Name.Contains(Searchkey)|| p.Brand.Contains(Searchkey)).AsQueryable();
            }

            switch (ordering)
            {
                case Ordering.NotOrder:
                    productQuery =productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostVisited:
                    productQuery = productQuery.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                case Ordering.Bestselling:
                    break;
                case Ordering.MostPopular:
                    break;
                case Ordering.theNewest:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    productQuery = productQuery.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.theMostExpensive:
                    productQuery = productQuery.OrderByDescending(p => p.Price).AsQueryable();
                    break;
            }


            var product = productQuery.ToPaged(Page,pageSize, out totalRow);

            Random rd = new Random();

            return new ResultDto<ResultProductForSiteDto>
            {
                Data = new ResultProductForSiteDto
                {
                    TotalRow = totalRow,
                    Products = product.Select(p => new ProductForSiteDto
                    {
                        Id = p.Id,
                        Star = rd.Next(1, 5),
                        Title = p.Name,
                        ImageSrc = p.ProductImages.FirstOrDefault().Src,
                        Price = p.Price,
                    }).ToList(),
                },
                IsSuccess = true,

            };
        }
    }
}
