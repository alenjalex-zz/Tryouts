using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;

namespace FindViewTryout.Controllers
{
    public class NewsController : Controller
    {
        private readonly IRazorViewEngine _razorViewEngine;

        private readonly IActionContextAccessor _actionContextAccessor;

        public NewsController(IRazorViewEngine razorViewEngine, IActionContextAccessor actionContextAccessor)
        {
            _razorViewEngine = razorViewEngine;
            _actionContextAccessor = actionContextAccessor;
        }

        public IActionResult Index()
        {
            var viewName = "~/Views/News/Index.cshtml";

            var foundView = _razorViewEngine.FindView(_actionContextAccessor.ActionContext, viewName, true);

            var gotView = _razorViewEngine.GetView(string.Empty, viewName, true);

            if (foundView.Success)      // gets false
            {
                return View(viewName);
            }
            else if (gotView.Success)   //gets true
            {
                return View(viewName);
            }

            return NotFound();
        }
    }
}