using Collections.Models;
using Collections.Repository;
using Collections.Services;
using Collections.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Collections.Web.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly IApplicationUserService _applicationUserService;
        private readonly IApplicationRoleService _applicationRoleService;
        public ApplicationUserController(IApplicationUserService applicationUserService, IApplicationRoleService applicationRoleService)
        {
            _applicationUserService = applicationUserService;
            _applicationRoleService = applicationRoleService;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {

            return View(_applicationUserService.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = _applicationUserService.GetById(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = _applicationRoleService.GetAll().Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ApplicationUserViewModel model = new ApplicationUserViewModel();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ApplicationUserViewModel applicationUserViewModel) 
        { 
            _applicationUserService.Insert(applicationUserViewModel);
            return RedirectToAction("Index");
        }
        
        
        [HttpGet]
        public IActionResult CreateMultiple()
        {
            ViewBag.Roles = _applicationRoleService.GetAll().Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            List<ApplicationUserViewModel> modelList = new List<ApplicationUserViewModel>();
            for (int i = 0; i < 3; i++)
            {
                modelList.Add(new ApplicationUserViewModel());
            }
            return View(modelList);
        }

        [HttpPost]
        public IActionResult CreateMultiple(List<ApplicationUserViewModel> applicationUserViewModels) 
        { 
            _applicationUserService.InsertMultiple(applicationUserViewModels);
            return RedirectToAction("Index");
        }

    }
}
