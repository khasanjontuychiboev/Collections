using Collections.Models;
using Collections.Repository;
using Collections.Services;
using Collections.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collections.Web.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IApplicationUserService _applicationUserService;
        public ApplicationUserController(IApplicationUserService applicationUserService)
        {
            _applicationUserService= applicationUserService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize=10)
        {
           
            return View(_applicationUserService.GetAll(pageNumber,pageSize));
        }

    }
}
