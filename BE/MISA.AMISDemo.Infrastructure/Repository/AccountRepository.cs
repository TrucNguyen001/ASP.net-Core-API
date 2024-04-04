using Dapper;
using demo_infastructure.Repository;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.ValidateException;
using MISA.AMISDemo.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        IMISADbContext _dbContext;
        public AccountRepository(IMISADbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Tìm kiếm tài khoản theo tên tài khoản
        /// </summary>
        /// <param name="username">Tên tài khoản</param>
        /// <returns>Trả về thông tin tài khoản</returns>
        /// Created by: Nguyễn Văn Trúc(17/2/2024)
        public Account GetByUserName(string username)
        {
            var sqlCommand = "SELECT * FROM Account WHERE UserName = @username";
            DynamicParameters paramet = new DynamicParameters();
            paramet.Add("@username", username);

            var user = _dbContext.Connection.QueryFirstOrDefault<Account>(sql: sqlCommand, param: paramet);

            return user;
        }

        /// <summary>
        /// Tìm kiếm tài khoản theo số điện thoại
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns>Trả về thông tin tài khoản</returns>
        /// Created by: Nguyễn Văn Trúc(17/2/2024)
        public Account GetByPhoneNumber(string phoneNumber)
        {
            var sqlCommand = "SELECT * FROM Account WHERE PhoneNumber = @phoneNumber";
            DynamicParameters paramet = new DynamicParameters();
            paramet.Add("@phoneNumber", phoneNumber);

            var user = _dbContext.Connection.QueryFirstOrDefault<Account>(sql: sqlCommand, param: paramet);

            return user;
        }

        /// <summary>
        /// Tìm kiếm tài khoản theo email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Trả về thông tin tài khoản</returns>
        /// Created by: Nguyễn Văn Trúc(17/2/2024)
        public Account GetByEmail(string email)
        {
            var sqlCommand = "SELECT * FROM Account WHERE Email = @email";
            DynamicParameters paramet = new DynamicParameters();
            paramet.Add("@email", email);

            var user = _dbContext.Connection.QueryFirstOrDefault<Account>(sql: sqlCommand, param: paramet);

            return user;
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản
        /// </summary>
        /// <param name="accountNew">Tài khoản muốn cập nhật</param>
        /// <returns>trả về 1 nếu cập nhật thành công</returns>
        /// Created by: Nguyễn Văn Trúc(19/2/2024)
        public int UpdateAccount(Account accountNew)
        {
            var account = GetById(accountNew.AccountId);
            if (account != null)
            {
                var sqlCommand = "Proc_UpdateAccount";
                DynamicParameters paramet = new DynamicParameters();
                paramet.Add("m_AccessFailedCount", accountNew.AccessFailedCount);
                paramet.Add("m_ConcurrencyStamp", accountNew.ConcurrencyStamp);
                paramet.Add("m_LockoutEnabled", accountNew.LockoutEnabled);
                paramet.Add("m_SecurityStamp", accountNew.SecurityStamp);
                paramet.Add("m_RefreshToken", accountNew.RefreshToken);
                paramet.Add("m_RefreshTokenExpiryTime", accountNew.RefreshTokenExpiryTime);
                paramet.Add("m_AccountId", accountNew.AccountId);

                var user = _dbContext.Connection.Query<Account>(sql: sqlCommand, param: paramet, commandType: System.Data.CommandType.StoredProcedure);
            }
            return 1;
        }

        /// <summary>
        /// Thêm thông tin tài khoản
        /// </summary>
        /// <param name="account">tài khoản muốn thêm</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// Created by: Nguyễn Văn Trúc(19/2/2024)
        public int InsertAccount(Account account)
        {
            var sqlCommand = "Proc_InsertAccount";
            DynamicParameters paramet = new DynamicParameters();

            var props = typeof(Account).GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var propValue = prop.GetValue(account, null);
                paramet.Add($"m_{propName}", propValue);
            }
            var user = _dbContext.Connection.Query<Account>(sql: sqlCommand, param: paramet, commandType: System.Data.CommandType.StoredProcedure);
            return 1;
        }

        /// <summary>
        /// Đăng nhâp
        /// </summary>
        /// <param name="account">Tài khoản dùng để đăng nhập</param>
        /// <returns>Trả về 1 nếu đăng nhập thành công</returns>
        /// Created by: Nguyễn Văn Trúc(19/2/2024)
        public Account Login(AccountLogin account)
        {
            var user = GetByEmail(account.Account);
            user = user == null ? GetByPhoneNumber(account.Account) : user;
            return user;
        }

        /// <summary>
        /// Update lại mật khẩu
        /// </summary>
        /// <param name="accountId">Id tài khoản</param>
        /// <param name="account">Tài khoản</param>
        /// <returns>Trả về 1 nếu update thành công</returns>
        /// Created by: Nguyễn Văn Trúc(9/3/2024)
        public int UpdatePassword(Guid accountId, Account account)
        {
            base.Update(account, account.AccountId);
            return 1;
        }
    }
}
