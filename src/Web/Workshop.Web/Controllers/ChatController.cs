﻿namespace Workshop.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Workshop.Data;
    using Workshop.Web.ViewModels.Users;

    [Authorize]
    public class ChatController : BaseController
    {
        private ApplicationDbContext db;

        public ChatController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Private(string id)
        {
            var usersModel = this.db.Users
                               .Select(x => new UserModel()
                               {
                                   Id = x.Id,
                                   UserName = x.Email,
                               })
                               .ToList();

            this.ViewBag.Username = usersModel.FirstOrDefault(x => x.Id == id)?.UserName;
            return this.View(usersModel);
        }

        public IActionResult UsersList()
        {
            var usersModel = this.db.Users
                               .Select(x => new UserModel()
                               {
                                   Id = x.Id,
                                   UserName = x.Email,
                               })
                               .ToList();

            return this.View(usersModel);
        }
    }
}
