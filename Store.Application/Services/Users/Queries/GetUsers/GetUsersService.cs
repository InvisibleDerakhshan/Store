using Bugeto_Store.Common;
using Strore.Application.Interfaces.Contexts;
using System.Linq;

namespace Bugeto_Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;

        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultGetUserDto Excute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(x => x.FullName.Contains(request.SearchKey) && x.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var usersList = users.ToPaged(request.Page, 20, out rowsCount).Select(p => new GetUsersDto
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
                IsAvtive=p.IsActive,
            }).ToList();

            return new ResultGetUserDto
            {
                Rows = rowsCount,
                Users = usersList,
            };

        }
    }
}
    
