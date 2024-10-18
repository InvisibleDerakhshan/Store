﻿using Bugeto_Store.Common.Dto;
using Bugeto_Store.Domain.Entities.Finances;
using Strore.Application.Interfaces.Contexts;
using System;

namespace Bugeto_Store.Application.Services.Finances.Commands.AddRequestPay
{
    public interface IAddRequestPayServices
    {

        public ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId);
        
    }

    public class AddRequestPayService : IAddRequestPayServices
    {
        private readonly IDataBaseContext _context;

        public AddRequestPayService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId)
        {
            var user = _context.Users.Find(UserId);
            RequestPay requestPay = new RequestPay()
            {
                Amount = Amount,
                Guid = Guid.NewGuid(),
                IsPay = false,
                User = user,
            };
            _context.RequestPays.Add(requestPay);
            _context.SaveChanges();

            return new ResultDto<ResultRequestPayDto>()
            {
                Data = new ResultRequestPayDto
                {
                    Guid = requestPay.Guid,
                    Amount = requestPay.Amount,
                    Email = user.Email,
                    RequestPayId = requestPay.Id,

                },
                IsSuccess = true,
            };
        }
    }

    public class ResultRequestPayDto
    {
        public Guid Guid { get; set; }
        public int Amount { get; set; } 
        public string Email { get; set; }
        public long RequestPayId    { get; set; }
    }
}
