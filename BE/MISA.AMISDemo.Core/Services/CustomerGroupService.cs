using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.ValidateException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services
{
    public class CustomerGroupService : BaseService<CustomerGroup>, ICustomerGroupService
    {
        #region
        ICustomerGroupRepository _customerGroupRepository;
        #endregion

        #region Contructor
        public CustomerGroupService(ICustomerGroupRepository customerGroupRepository) : base(customerGroupRepository)
        {
            _customerGroupRepository = customerGroupRepository;
        }
        #endregion
    }
}
