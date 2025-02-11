﻿using Bugeto_Store.Application.Services.Users.Commands.EditUser;
using Bugeto_Store.Application.Services.Users.Commands.RegisterUser;
using Bugeto_Store.Application.Services.Users.Commands.RemoveUser;
using Bugeto_Store.Application.Services.Users.Commands.UserStatusChange;
using Bugeto_Store.Application.Services.Users.Queries.GetRoles;
using Bugeto_Store.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IRemoveUserService _removeUserService;
        private readonly IUserStatusChangeService _userStatusChangeService;
        private readonly IEditUserService _editUserService;

        public UsersController(IGetUsersService getUsersService, IGetRolesService getRolesService, IRegisterUserService registerUserService, IRemoveUserService removeUserService, IUserStatusChangeService userSatusChangeService, IEditUserService editUserService)
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _removeUserService = removeUserService;
            _userStatusChangeService = userSatusChangeService;
            _editUserService = editUserService;
        }


        public IActionResult Index(string searchKey, int page = 1)
        {
            return View(_getUsersService.Excute(new RequestGetUserDto
            {
                Page = page,
                SearchKey = searchKey
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Excute().Data, "Id", "Name");
            return View();
        }


        [HttpPost]

        public IActionResult Create(string Email, string FullName, long RoleId, string Password, string RePassword)
        {
            var result = _registerUserService.Excute(new RequestRegisterUserDto
            {
                Email = Email,
                FullName = FullName,
                Roles = new List<RolesInRegisterUserDto>()
               {
                   new RolesInRegisterUserDto()
                   {
                       Id= RoleId,
                   }

               },

                Password = Password,
                RePasword = RePassword,

            });
            return Json(result);

        }




        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_removeUserService.Execute(UserId));
        }


        [HttpPost]
        public IActionResult UserStatusChange(long UserId)
        {
            return Json(_userStatusChangeService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult Edit(long UserId, string FullName)
        {
            return Json(_editUserService.Execute(new RequestEdituserDto
            {
                Fullname = FullName,
                UserId = UserId,
            }));
        }
    }
}
