using MISA.AMISDemo.Core.Auth;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.Services
{
    public interface IAccountService : IBaseService<Account>
    {
        /// <summary>
        /// Kiểm tra xem tài khoản có tồn tại không
        /// </summary>
        ///  <param name="account">Tài khoản dùng để đăng nhập</param>
        /// <returns>Trả về 1 nếu tài khoản tồn tại</returns>
        /// CreatedBy: Nguyễn Văn Trúc(4/4/2024)
        TokenModel CheckAccount(AccountLogin account);

        /// <summary>
        /// Kiểm tra xem password có đúng hay không
        /// </summary>
        /// <param name="password">Mật khẩu nhập</param>
        /// <param name="account">Tài khoản tìm được theo username</param>
        /// <returns>
        /// true: Nhập đúng mật khẩu
        /// false: Sai mật khẩu
        /// </returns>
        /// Created by: Nguyễn Văn Trúc(17/02/2024)
        bool CheckPassword(Account account, string password);

        /// <summary>
        /// Hàm làm mới token
        /// </summary>
        /// <param name="tokenModel">Thông tin cần thiết để có thể làm mới token</param>
        /// <returns>Trả về model token chứa token mới</returns>
        /// Created by: Nguyễn Văn Trúc(4/4/2024)
        TokenModel RefreshToken(TokenModel tokenModel);

        /// <summary>
        /// Hàm đăng ký tài khoản
        /// </summary>
        /// <param name="model">Model chứa thông tin dùng để đăng ký</param>
        /// <returns>Trả về 1 nếu xác nhận thông tin thành công</returns>
        /// Created by: Nguyễn Văn Trúc(4/4/2024)
        int Register(RegisterModel model);

        /// <summary>
        /// Hàm huỷ bỏ token
        /// </summary>
        /// <param name="userName">Tên người dùng</param>
        /// <returns>Trả về người dùng nếu huỷ thành công</returns>
        /// Created by: Nguyễn Văn Trúc(4/4/2024)
        Account Revoke(string userName);

        /// <summary>
        /// Hàm xoá tất cả token
        /// </summary>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// Created by: Nguyễn Văn Trúc(4/4/2024)
        int RevokeAll();

        /// <summary>
        /// Hàm kiểm tra trước khi cập nhật lại mật khẩu
        /// </summary>
        /// <param name="AccountId">Id tài khoản</param>
        /// <param name="account">Tài khoản</param>
        /// CreatedBy: Nguyễn Văn Trúc(9/3/2024)
        void UpdatePassword(Guid accountId, Account account);

        /// <summary>
        /// Hàm gửi email lấy lại mật khẩu
        /// </summary>
        /// <param name="email">Email muốn gửi đến</param>
        /// <returns>Trả về 1 nếu gửi thành công</returns>
        ///  CreatedBy: Nguyễn Văn Trúc(9/3/2024)
        int SendEmail(string email);

        /// <summary>
        /// Kiểm tra mã code có đúng không
        /// </summary>
        /// <param name="code">Mã code</param>
        /// <returns>
        /// Trả về 1 nếu nhập đúng
        /// </returns>
        ///  CreatedBy: Nguyễn Văn Trúc(9/3/2024)
        int CheckCodeRecoverPassword(string code, string email);
    }
}
