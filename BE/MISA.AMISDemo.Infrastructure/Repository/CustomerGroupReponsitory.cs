using Dapper;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_infastructure.Repository
{
    public class CustomerGroupReponsitory : BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        public CustomerGroupReponsitory(IMISADbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }
        #region Contructor

        #endregion

        #region Methods
        /// <summary>
        /// Thêm nhóm khách hàng
        /// </summary>
        /// <param name="customerGroup">Nhóm khách hàng</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public new int Insert(CustomerGroup customerGroup)
        {
            return base.Insert(customerGroup);
        }

        /// <summary>
        /// Cập nhật nhóm khách hàng
        /// </summary>
        /// <param name="customerGroup">Nhóm kháchh hàng</param>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <returns>Trả về 1 nếu sửa thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public new int Update(CustomerGroup customerGroup, Guid customerGroupId)
        {
            return base.Update(customerGroup, customerGroupId);
        }

        /// <summary>
        /// Xoá nhóm khách hàng
        /// </summary>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public new int Delete(Guid customerGroupId)
        {
            return base.Delete(customerGroupId);
        }

        /// <summary>
        /// Lấy thông tin toàn bộ nhóm khách hàng
        /// </summary>
        /// <returns>Trả về tất cả bản ghi</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public new IEnumerable<CustomerGroup> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Lấy thông tin nhóm khách hàng theo Id
        /// </summary>
        /// <param name="customerGroupId">Id nhóm khách hàng</param>
        /// <returns>Trả về bản ghi theo Id</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public new CustomerGroup GetById(Guid customerGroupId)
        {
            return base.GetById(customerGroupId);
        }
        #endregion
    }
}
