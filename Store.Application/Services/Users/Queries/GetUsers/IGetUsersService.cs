using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Bugeto_Store.Application.Services.Users.Queries.GetUsers.ResultGetUserDto;

namespace Bugeto_Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultGetUserDto Excute(RequestGetUserDto request);
    }
}
    
