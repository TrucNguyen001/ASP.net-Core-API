using Microsoft.Extensions.Configuration;
using MISA.AMISDemo.Api.Tests.Repository;
using MISA.AMISDemo.Api.Tests.Service;
using MISA.AMISDemo.Core.Auth;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.ValidateException;
using MISA.AMISDemo.Infrastructure.MISADatabaseContext;
using MISA.AMISDemo.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Api.Tests.ApiTests
{
    [TestFixture]
    public class AccountTests
    {
        #region Fields
        AccountRepositoryTests _accountRepositoryTests;
        AccountServiceTests _accountServiceTests;
        UnitOfWork _unitOfWork;
        string conection = MISAResourceUnitTest.ConnectString;
        MySqlDbContext _dbContext;
        IConfiguration _configuration;
        #endregion

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new UnitOfWork(conection);
            _dbContext = new MySqlDbContext(conection, _unitOfWork);
            _accountRepositoryTests = new AccountRepositoryTests(_dbContext, _unitOfWork);
            _accountServiceTests = new AccountServiceTests(_accountRepositoryTests, _configuration);
        }

        /// <summary>
        /// Hàm kiểm tra đầu vào đăng nhập
        /// Mong muốn kết quả trả về 1(Đăng nhập thành công)
        /// CreatedBy: Nguyễn Văn Trúc(11/3/2024)
        /// </summary>
        [Test]
        public void Login_WithAccount_Return1()
        {
            // Arrange
            var account = new AccountLogin
            {
                Account = "admin@gmail.com",
                Password = "admin"
            };
            
            // Act
            var result = _accountServiceTests.Login(account);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        /// <summary>
        /// Hàm kiểm tra đầu vào đăng nhập
        /// Mong muốn kết quả trả về MISAValidateExceptionAuth(Đăng nhập không thành công)
        /// CreatedBy: Nguyễn Văn Trúc(11/3/2024)
        /// </summary>
        [Test]
        public void Login_WithAccount_MISAValidateExceptionAuth()
        {
            // Arrange
            var account = new AccountLogin
            {
                Account = "admin@gmail.com",
                Password = ""
            };

            // Act && Assert
            Assert.Throws<MISAValidateExceptionAuth>(() => _accountServiceTests.Login(account));
        }

        /// <summary>
        /// Hàm kiểm tra tài khoản đăng ký
        /// Mong muốn kết quả trả về: 1(Đăng ký thành công)
        /// CreatedBy: Nguyễn Văn Trúc(11/3/2024)
        /// </summary>
        [Test]
        public void Register_WithAccount_Return1()
        {
            // Arrage
            var account = new RegisterModel
            {
                UserName = "demotest",
                Email = "test@gmail.com",
                PhoneNumber = "1234567890",
                Password = "demo"
            };

            // Act
            var result = _accountServiceTests.Register(account);

            // Assert
            Assert.That(result, Is.EqualTo(1));

        }

        /// <summary>
        /// Hàm kiểm tra tài khoản đăng ký
        /// Mong muốn kết quả trả về: MISAValidateException(Đăng ký không thành công)
        /// CreatedBy: Nguyễn Văn Trúc(11/3/2024)
        /// </summary>
        [Test]
        public void Register_WithAccount_MISAValidateException()
        {
            // Arrage
            RegisterModel account = new RegisterModel
            {
                UserName = "admin",
                Email = "test@gmail.com",
                PhoneNumber = "1234567890",
                Password = "demo"
            };

            // Act && Assert
            Assert.Throws<MISAValidateException>(() => _accountServiceTests.Register(account));

        }

        /// <summary>
        /// Hàm kiểm tra xoá refreshToken
        /// Mong muốn kết quả trả về: user(Thông tin user)
        /// CreatedBy: Nguyễn Văn Trúc(11/3/2024)
        /// </summary>
        [Test]
        public void Revoke_WithUserName_ReturnUser()
        {
            // Arrange
            var userName = "admin";

            // Act 
            var result = _accountServiceTests.Revoke(userName);

            //Assert
            Assert.That(result, Is.InstanceOf<Account>());
        }

        /// <summary>
        /// Hàm kiểm tra xoá refreshToken
        /// Mong muốn kết quả trả về: MISAValidateException(lỗi)
        /// CreatedBy: Nguyễn Văn Trúc(11/3/2024)
        /// </summary>
        [Test]
        public void Revoke_WithUserName_MISAValidateException()
        {
            // Arrange
            var userName = "adminn";

            // Act && Assert
            Assert.Throws<MISAValidateException>(() => _accountServiceTests.Revoke(userName));
        }

        /// <summary>
        /// Hàm kiểm tra xoá tất cả refreshToken
        /// Mong muốn kết quả trả về: 1(Xoá thành công)
        /// CreatedBy: Nguyễn Văn Trúc(11/3/2024)
        /// </summary>
        [Test]
        public void RevokeAll_ReturnUser()
        {
            // Act 
            var result = _accountServiceTests.RevokeAll();

            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SendEmail_WithEmail_Return1()
        {
            // Arrange
            var email = "nguyenvantruc539@gmail.com";

            // Act
            var result = _accountServiceTests.SendEmail(email);

            Assert.That(result, Is.EqualTo(1));
        }

            //[Test]
            //public void CheckAccount_WithAccount_ReturnTokenModel()
            //{
            //    // Arrange
            //    var account = new AccountLogin
            //    {
            //        Account = "admin@gmail.com",
            //        Password = "admin"
            //    };

            //    // Act
            //    TokenModel result = _accountServiceTests.CheckAccount(account);

            //    // Assert
            //    Assert.That(result, Is.InstanceOf<TokenModel>());
            //}



            /// <summary>
            /// Giải phóng tài nguyên mỗi lần kết thúc
            /// CreatedBy: Nguyễn Văn Trúc(25/2/2024)
            /// </summary>
            [TearDown]
        public void TearDown()
        {
            //Giải phóng tài nguyên sau khi mỗi bài kiểm tra kết thúc
            _accountRepositoryTests.Dispose();
            _unitOfWork.Dispose();
        }

    }
}
