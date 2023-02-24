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
        public string Password { get; set; }

        public List<string> Roles { get; set; }

        public ApplicationUserViewModel()
        {

        }

        public ApplicationUserViewModel(ApplicationUser applicationUser, List<string> roles)
        {
            Id = applicationUser.Id;
            UserName= applicationUser.UserName;
            Email= applicationUser.Email;
            Password = applicationUser.PasswordHash;
            Roles= roles;
           
        }

        public ApplicationUser ConvertViewModel(ApplicationUserViewModel applicationUserViewModel)
        {
            return new ApplicationUser
            {
                UserName = applicationUserViewModel.Email,
                Email = applicationUserViewModel.Email

            };
        }
    }
}
