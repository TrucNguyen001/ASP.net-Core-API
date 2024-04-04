using Microsoft.Extensions.Configuration;
using MISA.AMISDemo.Core.Auth;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Core.ValidateException;
using MISA.AMISDemo.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Api.Tests.Service
{
    public class AccountServiceTests : AccountService
    {
        IAccountRepository _accountRepository;
        IConfiguration _configuration;
        public AccountServiceTests(IAccountRepository accountRepository, IConfiguration configuration) : base(accountRepository, configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Đăng nhâp
        /// </summary>
        /// <param name="account">Tài khoản dùng để đăng nhập</param>
        /// <returns>Trả về 1 nếu đăng nhập thành công</returns>
        /// CreatedBy: Nguyễn Văn Trúc(4/4/2024)
        public int Login(AccountLogin account)
        {
            return base.Login(account);
        }

        /// <summary>
        /// Kiểm tra xem tài khoản có tồn tại không
        /// </summary>
        ///  <param name="account">Tài khoản dùng để đăng nhập</param>
        /// <returns>Trả về thông tin token nếu tài khoản tồn tại</returns>
        /// CreatedBy: Nguyễn Văn Trúc(4/3/2024)
        public TokenModel CheckAccount(AccountLogin account)
        {
            return base.CheckAccount(account);
        }

        /// <summary>
        /// Hàm gửi email lấy lại mật khẩu
        /// </summary>
        /// <param name="email">Email muốn gửi đến</param>
        /// <returns>Trả về 1 nếu gửi thành công</returns>
        ///  CreatedBy: Nguyễn Văn Trúc(9/3/2024)
        public int SendEmail(string email)
        {
            CheckEmailNull(email);
            var account = _accountRepository.GetByEmail(email);
            if (account == null)
            {
                throw new MISAValidateException(MISAResourceVN.EmailNoValid);
            }
            // Lấy code ngẫu nhiên
            var codeRecoverPassword = GenerateRandomNumber();

            Cache.Set(MISAResourceVN.CodeRecoverPassword, codeRecoverPassword, DateTimeOffset.UtcNow.AddMinutes(5));

            // Lấy thông tin từ appsetting
            var fromMail = _configuration[MISAResourceVN.FromMail];
            var fromPassword = _configuration[MISAResourceVN.FromPassword];

            // Tạo một đối tượng MailMessage để gửi email
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = MISAResourceVN.SubjectCodeRecoverPassword;
            message.To.Add(new MailAddress(email));
            message.Body = $"{codeRecoverPassword} {MISAResourceVN.BodyCodeRecoverPassword}";
            message.IsBodyHtml = true;

            // Tạo một đối tượng SmtpClient để gửi email thông qua máy chủ SMTP của Gmail
            var smtpClient = new SmtpClient(_configuration[MISAResourceVN.SMTPGmail])
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            // Gửi
            smtpClient.Send(message);
            return 1;
        }

        /// <summary>
        /// Tạo ra số có 6 số ngẫu nhiên
        /// </summary>
        /// <returns>Trả về số ngẫu nhiên</returns>
        /// CreatedBy: Nguyễn Văn Trúc(9/3/2024)
        public int GenerateRandomNumber()
        {
            Random random = new Random();
            // Sử dụng Next() để tạo số ngẫu nhiên trong khoảng từ 100000 đến 999999
            int randomNumber = random.Next(100000, 999999);
            return randomNumber;
        }
    }
}
