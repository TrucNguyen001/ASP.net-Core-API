using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Infrastructure
{
    public interface IAccountRepository:IBaseRepository<Account>
    {
        /// <summary>
        /// Đăng nhâp
        /// </summary>
        /// <param name="account">Tài khoản dùng để đăng nhập</param>
        /// <returns>Trả về 1 nếu đăng nhập thành công</returns>
        Account Login(AccountLogin account);

        /// <summary>
        /// Tìm kiếm tài khoản theo tên tài khoản
        /// </summary>
        /// <param name="username">Tên tài khoản</param>
        /// <returns>Trả về thông tin tài khoản</returns>
        /// Created by: Nguyễn Văn Trúc(17/2/2024)
        Account GetByUserName(string username);

        /// <summary>
        /// Tìm kiếm tài khoản theo số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns>Trả về thông tin tài khoản</returns>
        /// Created by: Nguyễn Văn Trúc(17/2/2024)
        Account GetByPhoneNumber(string phoneNumber);

        /// <summary>
        /// Tìm kiếm tài khoản theo email
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Trả về thông tin tài khoản</returns>
        /// Created by: Nguyễn Văn Trúc(17/2/2024)
        Account GetByEmail(string email);

        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        /// <param name="accountNew">Tài khoản muốn cập nhật</param>
        /// <returns>trả về 1 nếu cập nhật thành công</returns>
        /// Created by: Nguyễn Văn Trúc(19/2/2024)
        int UpdateAccount(Account accountNew);

        /// <summary>
        /// Thêm thông tin tài khoản
        /// </summary>
        /// <param name="account">tài khoản muốn thêm</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// Created by: Nguyễn Văn Trúc(19/2/2024)
        int InsertAccount(Account account);

        /// <summary>
        /// Update lại mật khẩu
        /// </summary>
        /// <param name="accountId">Id tài khoản</param>
        /// <param name="account">Tài khoản</param>
        /// <returns>Trả về 1 nếu update thành công</returns>
        /// Created by: Nguyễn Văn Trúc(9/3/2024)
        int UpdatePassword(Guid accountId, Account account);
    }
}
