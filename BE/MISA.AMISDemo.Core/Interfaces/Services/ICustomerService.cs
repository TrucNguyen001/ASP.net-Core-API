using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Interfaces.Services
{
    public interface ICustomerService:IBaseService<Customer>
    {
        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>
        /// True: Nếu đúng định dạng
        /// False: Không đúng định dạng
        /// </returns>
        /// CreatedBy: NVTruc(25/12/2023)
        bool IsValidEmail(string email);
    }
}
