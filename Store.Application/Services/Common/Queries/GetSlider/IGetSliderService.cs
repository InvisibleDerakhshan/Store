using Bugeto_Store.Common.Dto;
using Strore.Application.Interfaces.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Bugeto_Store.Application.Services.Common.Queries.GetSlider
{
    public interface IGetSliderService
    {
        ResultDto<List<SliderDto>> Excute();
    }

    public class GetSliderService : IGetSliderService
    {
        private readonly IDataBaseContext _context;

        public GetSliderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<SliderDto>> Excute()
        {
            var sliders = _context.Sliders.OrderByDescending(p => p.Id).ToList().Select(
                p => new SliderDto
                {
                    Link = p.link,
                    Src = p.Src,
                }).ToList();

            return new ResultDto<List<SliderDto>>()
            {
                Data = sliders,
                IsSuccess = true
            };
        }
    }
    public class SliderDto
    {
        public string Src { get; set; }
        public string Link { get; set; }
    }
}