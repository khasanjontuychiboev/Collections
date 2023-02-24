using Collections.Models;
using Collections.Utilities;
using Collections.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Services
{
    public interface IApplicationUserService
    {
        PagedResult<ApplicationUserViewModel> GetAll(int pageNumber, int pageSize);
        ApplicationUserViewModel GetById(string Id);

        void Update(ApplicationUserViewModel model);
        void Insert(ApplicationUserViewModel model);
        void InsertMultiple(List<ApplicationUserViewModel> modelList);
        void Delete(string id);
        IEnumerable<ApplicationUserViewModel> GetAll();
    }
}
