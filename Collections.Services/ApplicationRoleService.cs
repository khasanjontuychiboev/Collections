using Collections.Models;
using Collections.Repository;
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
    public class ApplicationRoleService: IApplicationRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        private ApplicationDbContext _context;



        public ApplicationRoleService( RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public void Delete(string id)
        {
            ApplicationRole role = _roleManager.FindByIdAsync(id).Result;
            if (role != null)
            {
                _context.ApplicationRoles.Remove(role);
                _context.SaveChanges();
            }

        }

        public PagedResult<ApplicationRoleViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ApplicationRoleViewModel();
            int totalCount;
            List<ApplicationRoleViewModel> vmList = new List<ApplicationRoleViewModel>();

            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _roleManager.Roles
                    .Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount = _roleManager.Roles.ToList().Count;

                vmList = ConvertToViewModelList(modelList);

            }
            catch (Exception)
            {

                throw;
            }

            var result = new PagedResult<ApplicationRoleViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize

            };
            return result;
        }

        public IEnumerable<ApplicationRoleViewModel> GetAll()
        {
            var modelList = _roleManager.Roles.ToList();
            var vmList = ConvertToViewModelList(modelList);
            return vmList;
        }


        public ApplicationRoleViewModel GetById(string Id)
        {
            var model = _context.ApplicationRoles.Find(Id);
            var vm = new ApplicationRoleViewModel(model);
            return vm;
        }
        
        public ApplicationRoleViewModel GetByName(string Name)
        {
            var model = _context.ApplicationRoles.First(x => x.Name == Name);
            var vm = new ApplicationRoleViewModel(model);
            return vm;
        }

        public void Insert(ApplicationRoleViewModel applicationRolerViewModel)
        {
            var model = new ApplicationRoleViewModel().ConvertViewModel(applicationRolerViewModel);
            _roleManager.CreateAsync(model);
            
        }

        public void Update(ApplicationRoleViewModel ApplicationRoleViewModel)
        {
            var model = new ApplicationRoleViewModel().ConvertViewModel(ApplicationRoleViewModel);

        }

        private List<ApplicationRoleViewModel> ConvertToViewModelList(List<ApplicationRole> modelList)
        {

            return modelList.Select(x => new ApplicationRoleViewModel(x)).ToList();
        }
    }
}
