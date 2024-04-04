using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Core.ValidateException;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MySqlConnector;

namespace demo_infastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        #region Field
        IMISADbContext _dbContext;

        public CustomerRepository(IMISADbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Contructor

        #endregion

        #region Methods
        /// <summary>
        /// Xoá thông tin
        /// </summary>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreateBy: NVTruc(28/12/2023)
        public new int Delete(Guid customerId)
        {
            return base.Delete(customerId);
        }

        /// <summary>
        /// Lấy toàn bộ thông tin
        /// </summary>
        /// <returns>Trả về tất cả bản ghi</returns>
        /// CreateBy: NVTruc(28/12/2023)
        public new IEnumerable<Customer> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Trả về bản ghi theo Id</returns>
        /// CreateBy: NVTruc(28/12/2023)
        public new Customer GetById(Guid customerId)
        {
            return base.GetById(customerId);
        }

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="customer">Khách hàng</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreateBy: NVTruc(28/12/2023)
        public new int Insert(Customer customer)
        {
            return base.Insert(customer);
        }

        /// <summary>
        /// Câp nhật khách hàng
        /// </summary>
        /// <param name="customer">Khách hàng</param>
        /// <param name="customerId">Id khách hàng</param>
        /// <returns>Trả về 1 nếu cập nhật thành công</returns>
        /// CreateBy: NVTruc(28/12/2023)
        public new int Update(Customer customer, Guid customerId)
        {
            return base.Update(customer, customerId);
        }
        #endregion
    }
}
