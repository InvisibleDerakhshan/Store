using Bugeto_Store.Common.Dto;
using Strore.Application.Interfaces.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace Bugeto_Store.Application.Services.Users.Queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IDataBaseContext _Context;

        public GetRolesService(IDataBaseContext context)
        {
            _Context = context;
        }

        public ResultDto<List<RolesDto>> Excute()
        {
            var roles = _Context.Roles.ToList().Select(x => new RolesDto
            {

                Id = x.Id,
                Name = x.Name,

            }).ToList();


            return new ResultDto<List<RolesDto>>()
            {
                Data = roles,
                IsSuccess = true,
                Message = "",
            };
        }


    }
}
