using Collections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.ViewModel
{
    public class ApplicationRoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ApplicationRoleViewModel()
        {

        }

        public ApplicationRoleViewModel(ApplicationRole applicationRole)
        {
            Id= applicationRole.Id;
            Name= applicationRole.Name;
        }

        public ApplicationRole ConvertViewModel(ApplicationRoleViewModel applicationRoleViewModel)
        {
            return new ApplicationRole
            {
                Id= applicationRoleViewModel.Id,
                Name= applicationRoleViewModel.Name,

            };
        }
    }
}
