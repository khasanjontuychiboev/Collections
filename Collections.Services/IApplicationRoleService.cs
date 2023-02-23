using Collections.Utilities;
using Collections.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Services
{
    public interface IApplicationRoleService
    {
        PagedResult<ApplicationRoleViewModel> GetAll(int pageNumber, int pageSize);
        ApplicationRoleViewModel GetById(string Id);
        ApplicationRoleViewModel GetByName(string Name);

        void Update(ApplicationRoleViewModel model);
        void Insert(ApplicationRoleViewModel model);
        void Delete(string id);
        IEnumerable<ApplicationRoleViewModel> GetAll();
    }
}
