using Bugeto_Store.Common.Dto;
using Bugeto_Store.Domain.Entities.HomePages;
using Strore.Application.Interfaces.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Bugeto_Store.Application.Services.Common.Queries.GetHomePageImages
{
    public interface IGetHomePageImageService
    {

        ResultDto<List<HomePageImagesdto>> Execcute();

    }

    public class GetHomePageImageService : IGetHomePageImageService
    {
        private readonly IDataBaseContext _context;

        public GetHomePageImageService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<HomePageImagesdto>> Execcute()
        {
            var images = _context.HomePagesImages.OrderByDescending(x => x.Id)
                .Select(x => new HomePageImagesdto
                {
                    Id= x.Id,
                    ImageLocation = x.ImageLocation,
                    Link  =x.Link,
                    Src = x.Src,
                }).ToList();
            return new ResultDto<List<HomePageImagesdto>>()
            {
                Data = images,
                IsSuccess = true
            };
        }
    }

    public class HomePageImagesdto
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}

