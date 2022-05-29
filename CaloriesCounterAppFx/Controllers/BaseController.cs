using CaloriesCounterAppFx.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CaloriesCounterAppFx.Controllers
{
    public class BaseController : Controller
    {
        protected virtual TModel CreateModel<TModel>() where TModel : BaseViewModel, new()
        {
            TModel model = new TModel();
            if(User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                model.UserData = new CurrentUser();
                model.UserData.Name = userName;
            }

            return model;
        }
    }
}