using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        public DepartmentService(IBaseRepository<Department> baseRepository) : base(baseRepository)
        {
        }
    }
}
