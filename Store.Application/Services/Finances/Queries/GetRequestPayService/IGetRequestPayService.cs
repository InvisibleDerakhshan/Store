using Bugeto_Store.Application.Services.Finances.Queries.GetRequestPayService;
using Bugeto_Store.Common.Dto;
using Strore.Application.Interfaces.Contexts;
using System;
using System.Linq;

namespace Bugeto_Store.Application.Services.Finances.Queries.GetRequestPayService
{
    public interface IGetRequestPayService
    {
        ResultDto<RequestPayDto> Execute(Guid guid);
    }
}
public class GetRequestPayService : IGetRequestPayService
{
    private readonly IDataBaseContext _context;
    public ResultDto<RequestPayDto> Execute(Guid guid)
    {
       var requestpay = _context.RequestPays.Where(p=>p.Guid == guid).FirstOrDefault();
        if(requestpay!= null)
        {
            return new ResultDto<RequestPayDto>()
            {
                Data = new RequestPayDto()
                {
                    Amount = requestpay.Amount,
                }
            };
        }
        else
        {
            throw new Exception("request pay not found !!!!");
        }
    }
}

public class RequestPayDto
{
    public int Amount { get; set; }
}

