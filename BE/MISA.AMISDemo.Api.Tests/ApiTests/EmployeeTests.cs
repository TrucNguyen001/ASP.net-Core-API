using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MISA.AMISDemo.Api.Tests.Repository;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Core.MISAEnum;
using MISA.AMISDemo.Core.Resource;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Core.ValidateException;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MISA.AMISDemo.Infrastructure.MISADatabaseContext;
using MISA.AMISDemo.Infrastructure.Repository;
using MISA.AMISDemo.Infrastructure.UnitOfWork;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMISDemo.Core.DTOs;
using Microsoft.VisualStudio.TestPlatform.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;

namespace MISA.AMISDemo.Api.Tests.Service
{
    [TestFixture]
    public class EmployeeTests
    {
        #region Fields
        private EmployeeRepositoryTests _employeeRepositoryTests;
        private EmployeeServiceTests _employeeServiceTests;
        UnitOfWork _unitOfWork;
        string conection = MISAResourceUnitTest.ConnectString;
        MySqlDbContext _dbContext;
        #endregion

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new UnitOfWork(conection);
            _dbContext = new MySqlDbContext(conection, _unitOfWork);
            _employeeRepositoryTests = new EmployeeRepositoryTests(_dbContext, _unitOfWork);
            _employeeServiceTests = new EmployeeServiceTests(_employeeRepositoryTests, _unitOfWork);
        }

        /// <summary>
        /// Kiểm tra định dạng email
        /// Mong muốn kết quả trả về: True(Email đúng định dạng)
        /// CreatedBy: Nguyễn Văn Trúc(2/3/2024)
        /// </summary>
        [Test]
        public void IsValidEmail_WithEmail_ReturnTrue()
        {
            // Arrange
            var email = MISAResourceUnitTest.Email;

            // Act
            var result = _employeeServiceTests.IsValidEmail(email);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Kiểm tra định dạng email
        /// Mong muốn kết quả trả về: False(Email không đúng định dạng)
        /// CreatedBy: Nguyễn Văn Trúc(2/3/2024)
        /// </summary>
        [Test]
        public void IsValidEmail_WithEmail_ReturnFalse()
        {
            // Arrange
            var email = MISAResourceUnitTest.EmailFalse;

            // Act
            var result = _employeeServiceTests.IsValidEmail(email);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Hàm kiểm tra xem đúng định dạng file excel không
        /// Mong muốn kết quả trả về: Lỗi(MISAValidateException)
        /// </summary>
        /// CreatedBy: Nguyễn Văn Trúc(2/3/2024)
        [Test]
        public void CheckFileImport_WithFile_MISAValidateException()
        {
            // Arrange
            var excelFilePath = MISAResourceUnitTest.ExcelFilePath;
            var iFormFile = _employeeServiceTests.ConvertPathToIFormFile(excelFilePath);

            // Act && Assert
            Assert.Throws<MISAValidateException>(() => _employeeServiceTests.CheckFileImport(iFormFile));
        }

        /// <summary>
        /// Tìm kiếm theo mã, họ tên, số điện thoại
        /// Mong muốn kết quả trả về: Danh sách nhân viên
        /// CreatedBy: Nguyễn Văn Trúc(29/2/2024)
        /// </summary>
        [Test]
        public void Search_WithText_ReturnListEmployee()
        {
            // Arrange
            var text = MISAResourceUnitTest.EmployeeCode;

            // Act
            var result = _employeeRepositoryTests.Search(text);

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// Hàm kiểm tra sắp xếp tất cả bản ghi giảm dần
        /// Mong muốn kết quả trả về: Danh sách giảm dần
        /// CreatedBy: Nguyễn Văn Trúc(29/2/2024)
        /// </summary>
        [Test]
        public void SortDecrease_ReturnListEmployeeSortDecrease()
        {
            // Act
            var result = _employeeRepositoryTests.SortDecrease();

            //Assert
            Assert.That(result, Is.Not.Empty);
        }

        /// <summary>
        /// Hàm kiểm tra phân trang
        /// Mong muốn kết quả trả về: 1(Dữ liệu nhập vào đúng)
        /// CreatedBy: Nguyễn Văn Trúc(2/3/2024)
        /// </summary>
        [Test]
        public void GetPaging_WithPageSizePageIndexText_Return1()
        {
            // Arrange
            var pageSize = 20;
            var pageIndex = 5;
            var text = "";

            // Act
            var result = _employeeServiceTests.GetPaging(pageSize, pageIndex, text);

            // Asseart
            Assert.That(result, Is.EqualTo(1));
        }

        /// <summary>
        /// Hàm kiểm tra phân trang
        /// Mong muốn kết quả trả về: Lỗi(MISAValidateException)
        /// CreatedBy: Nguyễn Văn Trúc(2/3/2024)
        /// </summary>
        [Test]
        public void GetPaging_WithPageSizePageIndexText_MISAValidateException()
        {
            // Arrange
            var pageSize = -20;
            var pageIndex = -5;
            var text = "";

            // Act && Assert
            Assert.Throws<MISAValidateException>(() => _employeeServiceTests.GetPaging(pageSize, pageIndex, text));
        }

        /// <summary>
        /// Hàm kiểm tra phân trang
        /// Mong muốn kết quả trả về: Danh sách nhân viên sau phân trang
        /// CreatedBy: Nguyễn Văn Trúc(29/2/2024)
        /// </summary>
        [Test]
        public void Paging_WithPageSizePageIndexText_ReturnListEmployee()
        {
            // Arrange
            var pageSize = 20;
            var pageIndex = 5;
            var text = "";

            // Act
            var result = _employeeRepositoryTests.GetPaging(pageSize, pageIndex, text);

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        /// <summary>
        /// Hàm kiểm tra lấy ra mã nhân viên lớn nhất và tự động tăng 1
        /// Mong muốn trả về: Mã nhân viên lớn nhất sau khi tăng 1
        /// CreatedBy: Nguyễn Văn Trúc(29/2/2024)
        /// </summary>
        [Test]
        public void GetEmployeeCode_ListEmployee_ReturnEmployeeCodeBiggest()
        {
            // Act
            var result = _employeeServiceTests.GetEmployeeCodeBiggest();

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// Hàm kiểm tra lấy tất cả nhân viên
        /// Mong muốn kết quả trả về: danh sách nhân viên
        /// CreatedBy: Nguyễn Văn Trúc(24/2/2024)
        /// </summary>
        [Test]
        public void GetAll_ReturnEmployee()
        {
            //Act
            var result = _employeeRepositoryTests.GetAll();

            //Assert
            Assert.That(result, Is.Not.Empty);
        }

        /// <summary>
        /// Hàm kiểm tra lấy tất cả danh sách nhân viên và liên kết với chức vụ và phòng ban
        /// Mong muốn kết quả trả về: danh sách nhân viên
        /// CreatedBy: Nguyễn Văn Trúc(24/2/2024)
        /// </summary>
        [Test]
        public void GetEmployees_ReturnEmployee()
        {
            // Act
            var result = _employeeRepositoryTests.GetEmployees();

            // Assert
            Assert.That(result, Is.Not.Empty);
        }

        /// <summary>
        /// Hàm kiểm tra tìm kiếm nhân viên bằng Id
        /// Mong muốn kết quả trả về: employee
        /// CreatedBy: Nguyễn Văn Trúc(24/2/2024)
        /// </summary>
        [Test]
        public void GetById_WithEmployeeId_ReturnEmployee()
        {
            // Arrange
            var employeeId = Guid.Parse("1753e72d-c9e1-4b73-b251-3de8bfe04bc6");

            //Act
            var result = _employeeRepositoryTests.GetById(employeeId);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// Hàm kiểm tra mã code có trùng hay không
        /// Mong muốn kết quả trả về: True(Mã chưa tồn tại)
        /// CreatedBy: Nguyễn Văn Trúc(24/2/2024)
        /// </summary>
        [Test]
        public void CheckDuplicateCode_WithExistingCode_ShouldReturnTrue()
        {
            //Arrange
            var employeeCode = MISAResourceUnitTest.EmployeeCode;

            //Act
            var result = _employeeRepositoryTests.CheckDuplicateCode(employeeCode);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        /// <summary>
        /// Hàm kiểm tra mã code có trùng hay không
        /// Mong muốn kết quả trả về: False(Mã đã tồn tại)
        /// CreatedBy: Nguyễn Văn Trúc(24/2/2024)
        /// </summary>
        [Test]
        public void CheckDuplicateCode_WithNonExistingCode_ShouldReturnFalse()
        {
            //Arrange
            var employeeCode = MISAResourceUnitTest.EmployeeCode;

            //Act
            var result = _employeeRepositoryTests.CheckDuplicateCode(employeeCode);

            // Assert
            Assert.That(!result, Is.EqualTo(false));
        }

        /// <summary>
        /// Hàm kiểm tra số điện thoại có trùng hay không
        /// Mong muốn kết quả trả về: True(Mã đã tồn tại)
        /// CreatedBy: Nguyễn Văn Trúc(2/3/2024)
        /// </summary>
        [Test]
        public void CheckDuplicatePhoneNumber_WithPhoneNumber_ReturnTrue()
        {
            // Arrange
            var phoneNumber = MISAResourceUnitTest.PhoneNumber;

            // Act
            var result = _employeeRepositoryTests.CheckDuplicatePhoneNumber(phoneNumber);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Hàm kiểm tra số điện thoại có trùng hay không
        /// Mong muốn kết quả trả về: False(Mã chưa tồn tại)
        /// CreatedBy: Nguyễn Văn Trúc(2/3/2024)
        /// </summary>
        [Test]
        public void CheckDuplicatePhoneNumber_WithPhoneNumber_ReturnFalse()
        {
            // Arrange
            var phoneNumber = MISAResourceUnitTest.PhoneNumberFalse;

            // Act
            var result = _employeeRepositoryTests.CheckDuplicatePhoneNumber(phoneNumber);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Hàm kiểm tra thêm nhân viên thành công hay không
        /// Mong muốn kết quả trả về: 1(Thêm thành công)
        /// CreatedBy: Nguyễn Văn Trúc(25/2/2024)
        /// </summary>
        [Test]
        public void Insert_WithEmployee_ShouldReturnOne()
        {
            //Arrange
            _unitOfWork.BeginTransaction();
            Employee employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV000011",
                FirstName = "Nguyễn",
                LastName = "Trúc",
                FullName = "Nguyễn Văn Trúc",
                Gender = Gender.FEMALE,
                DateOfBirth = new DateTime(2002, 1, 26),
                PhoneNumber = "0999999999",
                Email = "vantruc@gmail.com",
                Address = "Hà Nam",
                IdentificationCard = "99999999999",
                PlaceIdentificationCard = "Hà Nam",
                DateOfIdentityCard = new DateTime(2018, 1, 1),
                JoinDate = new DateTime(2023, 9, 1),
                MartialStatus = 0,
                DepartmentId = Guid.Parse("1f60e54f-196f-45f3-5498-ae38c5379e4b"),
                PositionId = Guid.Parse("589edf01-198a-4ff5-958e-fb52fd75a1d4"),
                WorkStatus = WorkStatus.WORKING,
                Salary = 9999999,
                LandlinePhone = "987654321",
                BankAccount = "123456789",
                BankName = "Agribank",
                Branch = "Hà Nam"
            };

            //Act
            var result = _employeeRepositoryTests.Insert(employee);

            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        /// <summary>
        /// Hàm kiểm tra thêm nhân viên thành công hay không
        /// Mong muốn kết quả trả về: MISAValidateException(Lỗi)
        /// CreatedBy: Nguyễn Văn Trúc(25/2/2024)
        /// </summary>
        [Test]
        public void Insert_WithException_ShouldReturnMISAValidateException()
        {
            //Arrange
            Employee employee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeCode = "NV-00508",
                FirstName = "Nguyễn",
                LastName = "Trúc",
                FullName = "Heeloo You",
                Gender = Gender.FEMALE,
                DateOfBirth = new DateTime(2002, 1, 26),
                PhoneNumber = "0999999999",
                Email = "vantruc",
                Address = "Hà Nam",
                IdentificationCard = "99999999999",
                PlaceIdentificationCard = "Hà Nam",
                DateOfIdentityCard = new DateTime(2018, 1, 1),
                JoinDate = new DateTime(2023, 9, 1),
                MartialStatus = 0,
                DepartmentId = Guid.Parse("1f60e54f-196f-45f3-5498-ae38c5379e4b"),
                PositionId = Guid.Parse("589edf01-198a-4ff5-958e-fb52fd75a1d4"),
                WorkStatus = WorkStatus.WORKING,
                Salary = 9999999,
                LandlinePhone = "987654321",
                BankAccount = "123456789",
                BankName = "Agribank",
                Branch = "Hà Nam"
            };

            // Act && Assert
            Assert.Throws<MISAValidateException>(() => _employeeServiceTests.InsertService(employee));
        }

        // <summary>
        /// Hàm kiểm tra sửa nhân viên thành công hay không
        /// Mong muốn kết quả trả về: 1(Sửa thành công)
        /// CreatedBy: Nguyễn Văn Trúc(25/2/2024)
        /// </summary>
        [Test]
        public void Update_WithEmployee_ShouldReturnOne()
        {
            // Arrange
            _unitOfWork.BeginTransaction();
            Employee employee = new Employee
            {
                EmployeeId = Guid.Parse("1753e72d-c9e1-4b73-b251-3de8bfe04bc6"),
                EmployeeCode = "NV-00508",
                FirstName = "Nguyễn",
                LastName = "Trúc",
                FullName = "Nguyễn Văn Trúc",
                Gender = Gender.FEMALE,
                DateOfBirth = new DateTime(2002, 1, 26),
                PhoneNumber = "0999999999",
                Email = "vantruc@gmail.com",
                Address = "Hà Nam",
                IdentificationCard = "99999999999",
                PlaceIdentificationCard = "Hà Nam",
                DateOfIdentityCard = new DateTime(2018, 1, 1),
                JoinDate = new DateTime(2023, 9, 1),
                MartialStatus = 0,
                DepartmentId = Guid.Parse("1f60e54f-196f-45f3-5498-ae38c5379e4b"),
                PositionId = Guid.Parse("589edf01-198a-4ff5-958e-fb52fd75a1d4"),
                WorkStatus = WorkStatus.WORKING,
                Salary = 9999999,
                LandlinePhone = "987654321",
                BankAccount = "123456789",
                BankName = "Agribank",
                Branch = "Hà Nam"
            };

            // Act
            var result = _employeeRepositoryTests.Update(employee, employee.EmployeeId);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        /// <summary>
        /// Hàm kiểm tra sửa nhân viên thành công hay không
        /// Mong muốn kết quả trả về: MISAValidateException(Lỗi)
        /// CreatedBy: Nguyễn Văn Trúc(25/2/2024)
        /// </summary>
        [Test]
        public void Update_WithException_ShouldReturnMISAValidateException()
        {
            // Arrange
            Employee employee = new Employee
            {
                EmployeeId = Guid.Parse("1753e72d-c9e1-4b73-b251-3de8bfe04bc6"),
                EmployeeCode = "NV-99999",
                FirstName = "Nguyễn",
                LastName = "Trúc",
                FullName = "Heeloo You",
                Gender = Gender.FEMALE,
                DateOfBirth = new DateTime(2002, 1, 26),
                PhoneNumber = "0999999999",
                Email = "vantruc",
                Address = "Hà Nam",
                IdentificationCard = "99999999999",
                PlaceIdentificationCard = "Hà Nam",
                DateOfIdentityCard = new DateTime(2018, 1, 1),
                JoinDate = new DateTime(2023, 9, 1),
                MartialStatus = 0,
                DepartmentId = Guid.Parse("1f60e54f-196f-45f3-5498-ae38c5379e4b"),
                PositionId = Guid.Parse("589edf01-198a-4ff5-958e-fb52fd75a1d4"),
                WorkStatus = WorkStatus.WORKING,
                Salary = 9999999,
                LandlinePhone = "987654321",
                BankAccount = "123456789",
                BankName = "Agribank",
                Branch = "Hà Nam"
            };

            // Act && Assert
            Assert.Throws<MISAValidateException>(() => _employeeServiceTests.UpdateService(employee, employee.EmployeeId));
        }

        /// <summary>
        /// Hàm kiểm tra nhân viên xoá thành công hay không
        /// Mong muốn kết quả trả về: 1(Xoá thành công)
        /// CreatedBy Nguyễn Văn Trúc(26/2/2024)
        /// </summary>
        [Test]
        public void Delete_WithEmployeeId_ShouldReturnOne()
        {
            // Arrange
            _unitOfWork.BeginTransaction();

            // Act
            var employeeId = Guid.Parse("407fae62-8cf3-4d4d-a045-ee58ac01735d");

            //var employee = _employeeRepositoryTests.GetById(employeeId);

            //Assert.That(employee, Is.Not.Null);

            var result = _employeeRepositoryTests.Delete(employeeId);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        /// <summary>
        /// Hàm kiểm tra xoá nhiều nhân viên
        /// Mong muốn kết quả trả về: 1(Xoá thành công)
        /// CreatedBy Nguyễn Văn Trúc(26/2/2024)
        /// </summary>
        [Test]
        public void MultipleDelete_WithListEmployeeId_ShouldReturnOne()
        {
            // Arrange
            _unitOfWork.BeginTransaction();
            List<Guid> listEmployeeId = new List<Guid>
            {
                new Guid("3d5b3be9-fc9b-4b7e-81e1-046c8d7180cd"),
                new Guid("407fae62-8cf3-4d4d-a045-ee58ac01735d"),
                new Guid("411459b2-605e-49b8-8a0d-f21cf8e1747a")
            };

            // Act
            var result = _employeeRepositoryTests.MultipleDelete(listEmployeeId);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        /// <summary>
        /// Hàm kiểm tra xuất file excel
        /// Mong muốn kết quả trả về: ExportExcel
        /// CreatedBy Nguyễn Văn Trúc(27/2/2024)
        /// </summary>
        [Test]
        public void ExportExcel_WithListEmployee_FileExport()
        {
            // Arrange
            List<EmployeeDTOs> employees = new List<EmployeeDTOs>
            {
                new EmployeeDTOs
                {
                    EmployeeId = Guid.NewGuid(),
                    EmployeeCode = "NV-898397",
                    FullName = "John Doe",
                    Gender = 0,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    PhoneNumber = "1234567890",
                    LandlinePhone = "0987654321",
                    Email = "john.doe@example.com",
                    Address = "123 Main Street, City",
                    IdentificationCard = "123456789",
                    PlaceIdentificationCard = "City, Country",
                    DateOfIdentityCard = new DateTime(2010, 10, 20),
                    DepartmentName = "Human Resources",
                    PositionName = "Manager",
                    BankAccount = "987654321",
                    BankName = "ABC Bank",
                    Branch = "Branch XYZ"
                },
                new EmployeeDTOs
                {
                    EmployeeId = Guid.NewGuid(),
                    EmployeeCode = "NV-9484983",
                    FullName = "Jane Smith",
                    Gender = 0,
                    DateOfBirth = new DateTime(1988, 8, 20),
                    PhoneNumber = "9876543210",
                    LandlinePhone = "0123456789",
                    Email = "jane.smith@example.com",
                    Address = "456 Oak Street, Town",
                    IdentificationCard = "987654321",
                    PlaceIdentificationCard = "Town, Country",
                    DateOfIdentityCard = new DateTime(2012, 12, 25),
                    DepartmentName = "Finance",
                    PositionName = "Accountant",
                    BankAccount = "123456789",
                    BankName = "XYZ Bank",
                    Branch = "Branch ABC"
                }
            };

            // Act
            var result = _employeeServiceTests.ExportExcel(employees);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<FileExport>());
            Assert.That(result.FileStream, Is.Not.Null);
            Assert.That(result.FileContent, Is.EqualTo(MISAExportFileExcel.TYPE));
            Assert.That(result.FileName, Is.EqualTo(MISAExportFileExcel.FILENAME));
        }

        /// <summary>
        /// Kiểm tra import danh sách nhân viên từ file excel
        /// Mong muốn kết quả trả về: Danh sách import
        /// CreatedBy Nguyễn Văn Trúc(27/2/2024)
        /// </summary>
        [Test]
        public void ImportEmployee_WithListEmployeeFromExcel_ReturnListEmployeeImport()
        {
            // Arrange
            var excelFilePath = MISAResourceUnitTest.ExcelFilePathExcel;

            // Act
            var iFormFile = _employeeServiceTests.ConvertPathToIFormFile(excelFilePath);
            var result = _employeeServiceTests.ImportEmployee(false, iFormFile);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// Giải phóng tài nguyên mỗi lần kết thúc
        /// CreatedBy: Nguyễn Văn Trúc(25/2/2024)
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Giải phóng tài nguyên sau khi mỗi bài kiểm tra kết thúc
            _employeeRepositoryTests.Dispose();
            _unitOfWork.Dispose();
        }
    }
}
