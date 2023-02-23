using Collections.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.ViewModel
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public IEnumerable<ApplicationRoleViewModel> ApplicationRoleViewModels { get; set; }

        public ApplicationUserViewModel()
        {

        }

        public ApplicationUserViewModel(ApplicationUser applicationUser, IEnumerable<ApplicationRoleViewModel> applicationRoleViewModels)
        {
            Id = applicationUser.Id;
            UserName= applicationUser.UserName;
            Email= applicationUser.Email;
            ApplicationRoleViewModels= applicationRoleViewModels;
           
        }

        public ApplicationUser ConvertViewModel(ApplicationUserViewModel applicationUserViewModel)
        {
            return new ApplicationUser
            {
                Id = applicationUserViewModel.Id,
                UserName = applicationUserViewModel.UserName,
                Email = applicationUserViewModel.Email,

            };
        }
    }
}
