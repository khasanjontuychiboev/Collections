using Collections.Models;
using Collections.Repository;
using Collections.Repository.Interfaces;
using Collections.Utilities;
using Collections.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Services
{
    public class ApplicationUserService: IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IApplicationRoleService _applicationRoleService;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private ApplicationDbContext _context;



        public ApplicationUserService(UserManager<ApplicationUser> usermanager, RoleManager<ApplicationRole> roleManager, IApplicationRoleService applicationRoleService, ApplicationDbContext context)
        {
            _userManager = usermanager;
            _roleManager = roleManager;
            _applicationRoleService = applicationRoleService;
            _context = context;
        }

        public void Delete(string id)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                _context.ApplicationUsers.Remove(user);
                _context.SaveChanges();
            }
            
        }

        public PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ApplicationUserViewModel();
            int totalCount;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();

            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _userManager.Users
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _userManager.Users.ToList().Count;

                vmList = ConvertToViewModelList(modelList);

            }
            catch (Exception)
            {

                throw;
            }

            var result = new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize

            };
            return result;
        }

        public IEnumerable<ApplicationUserViewModel> GetAll()
        {
            var modelList = _userManager.Users.ToList();
            var vmList = ConvertToViewModelList(modelList);
            return vmList;
        }


        public ApplicationUserViewModel GetById(string Id)
        {
            var model = _context.ApplicationUsers.Find(Id);
            var vm = new ApplicationUserViewModel(model, _userManager.GetRolesAsync(model).Result.ToList());
            return vm;

        }

        public void Insert(ApplicationUserViewModel applicationUserViewModel)
        {
            var model = new ApplicationUserViewModel().ConvertViewModel(applicationUserViewModel);
            
            _userManager.CreateAsync(model, applicationUserViewModel.Password).GetAwaiter().GetResult();
            var Appuser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == model.Email);
            if (Appuser != null)
            {
                foreach (var item in applicationUserViewModel.Roles)
                {
                    _userManager.AddToRoleAsync(model, item.ToString()).GetAwaiter().GetResult();
                }
            }


        }
        public void InsertMultiple(List<ApplicationUserViewModel> multipleApplicationUserViewModel)
        {
            foreach (var viewModel in multipleApplicationUserViewModel)
            {
                if (viewModel.Email == null || viewModel.Password == null)
                {
                    continue;
                }
                var model = new ApplicationUserViewModel().ConvertViewModel(viewModel);
                _userManager.CreateAsync(model, viewModel.Password).GetAwaiter().GetResult();
                var Appuser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == model.Email);
                if (Appuser != null)
                {
                    foreach (var item in viewModel.Roles)
                    {
                        _userManager.AddToRoleAsync(model, item.ToString()).GetAwaiter().GetResult();
                    }
                }
                
            }
            

            
        }

        public void Update(ApplicationUserViewModel ApplicationUserViewModel)
        {
            var model = new ApplicationUserViewModel().ConvertViewModel(ApplicationUserViewModel);
            
        }

        private List<ApplicationUserViewModel> ConvertToViewModelList(List<ApplicationUser> modelList)
        {
            
            return modelList.Select(x => new ApplicationUserViewModel(x, _userManager.GetRolesAsync(x).Result.ToList())).ToList();
        }


    }
}
