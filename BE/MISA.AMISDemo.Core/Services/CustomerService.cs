using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.ValidateException;
using MISA.AMISDemo.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Diagnostics;
using MISA.AMISDemo.Core.DTOs;

namespace MISA.AMISDemo.Core.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        #region Fileds
        ICustomerRepository _customerRepository;
        #endregion

        #region Contructor
        public CustomerService(ICustomerRepository customerRepository) : base(customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Ghi đè bổ sung thêm validate 
        /// </summary>
        /// <param name="customer">Khách hàng</param>
        /// <param name="status">Trạng thái bản ghi</param>
        /// <exception cref="MISAValidateException">Trả về lỗi 400</exception>
        /// CreatedBy: NVTruc(27/12/2023)
        protected override void MISAValidate(Customer customer, int status)
        {
            if(status == (int)MISAEnum.Status.INSERT)
            {
                if (_customerRepository.CheckDuplicateCode(customer.CustomerCode))
                {
                    throw new MISAValidateException(MISAResourceVN.CustomerCodeDuplicate);
                }
            }
            else
            {
                if (!_customerRepository.CheckDuplicateCode(customer.CustomerCode))
                {
                    throw new MISAValidateException(MISAResourceVN.CustomerCodeNotFound);
                }
            }

            if (!IsValidEmail(customer.Email))
            {
                throw new MISAValidateException(MISAResourceVN.EmailMalformed);
            }
        }

        /// <summary>
        /// Kiểm tra định dạng email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>
        /// false: Email không đúng định dạng
        /// true: Email đúng định dạng
        /// </returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
