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
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using OfficeOpenXml.Style;
using System.Drawing;
using MISA.AMISDemo.Core.MISAEnum;
using System.Net.WebSockets;
using System.Globalization;
using System.Text.RegularExpressions;
//using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using OfficeOpenXml;
using NUnit.Framework.Internal.Execution;

namespace MISA.AMISDemo.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Fileds
        IEmployeeRepository _employeeRepository;
        IPositionRepository _positionRepository;
        IDepartmentRepository _departmentRepository;
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Contructor
        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper, IPositionRepository positionRepository, IDepartmentRepository departmentRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork):base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Ghi đè bổ sung thêm validate 
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <param name="status">Trạng thái nhân viên</param>
        /// <exception cref="MISAValidateException">Trả về lỗi 400</exception>
        /// CreatedBy: NVTruc(27/12/2023)
        protected override void MISAValidate(Employee employee, int status)
        {
            if(!CheckEmployeeCodeNotChar(employee.EmployeeCode))
            {
                throw new MISAValidateException(MISAResourceVN.EmployeeCodeNotChar);
            }
            if (status == (int)MISAEnum.Status.INSERT)
            {
                if (_employeeRepository.CheckDuplicateCode(employee.EmployeeCode))
                {
                    throw new MISAValidateException($"{MISAResourceVN.EmployeeCode} {employee.EmployeeCode} {MISAResourceVN.EmployeeCodeDuplicate}");
                }
            }
            else if (status == (int)MISAEnum.Status.UPDATE)
            {
                if (!_employeeRepository.CheckDuplicateCode(employee.EmployeeCode))
                {
                    throw new MISAValidateException(MISAResourceVN.EmployeeCodeNotFound);
                }
            }

            if (!IsValidEmail(employee.Email))
            {
                throw new MISAValidateException(MISAResourceVN.EmailMalformed);
            }
        }

        /// <summary>
        /// Hàm kiểm tra xem có chữ số trong hậu tố mã nhân viên hay không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>
        /// false: Nếu tồn tại chữ cái
        /// true: Đúng định dạng yêu cầu
        /// </returns>
        /// CreatedBy: Nguyễn Văn Trúc(15/3/2024)
        public bool CheckEmployeeCodeNotChar(string employeeCode)
        {
            for(int i = 3; i < employeeCode.Length; i++)
            {
                if (!char.IsDigit(employeeCode[i])) {
                    return false;
                }
            }
            return true;
        }

        ///// <summary>
        ///// Kiểm tra tiền tố của mã nhân viên
        ///// </summary>
        ///// <param name="employeeCode">Mã nhân viên</param>
        ///// <returns>
        ///// false: Mã nhân viên < 4 ký tự hoặc không bắt đầu bằng NV-
        ///// true: Đúng định dạng yêu cầu
        ///// </returns>
        ///// CreatedBy: Nguyễn Văn Trúc(15/3/2024)
        //public bool CheckEmployeeCodePrefix(string employeeCode)
        //{
        //    if (employeeCode.Length <= 3 || employeeCode.Substring(0, 3) != "NV-")
        //    {
        //        return false;
        //    }
        //    return true;
        //}

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
            if (email == "" || email == null)
            {
                return true;
            }
            else
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
        }

        /// <summary>
        /// Hàm import file
        /// </summary>
        /// <param name="fileImport">file import</param>
        ///  <param name="isCommit" Check xem có commit hay không> </param>
        /// <returns>Trả về danh sách nhân viên import</returns>
        /// CreatedBy: NVTruc(9/1/2024)
        public IEnumerable<Employee> ImportEmployee(bool isCommit, IFormFile fileImport)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            CheckFileImport(fileImport);
            var employees = new List<EmployeeImport>();

            _unitOfWork.BeginTransaction();

            // Kiểm tra xem dữ liệu lưu trong cache chưa
            var cache = MemoryCache.Default;
            var cachedEmployees = cache[MISAResourceVN.CachedEmployees] as List<EmployeeImport>;
            if(cachedEmployees != null)
            {
                if(isCommit)
                {
                    List<EmployeeImport> importedEmployees = cachedEmployees.Where(employee => employee.IsImported == true).ToList();
                    List<Employee> employeeMap = _mapper.Map<List<EmployeeImport>, List<Employee>>(importedEmployees);
                    foreach( var employee in employeeMap )
                    {
                        employee.CreatedDate = DateTime.Now;
                    }
                    var result = _employeeRepository.MultiplePost(employeeMap);
                    _unitOfWork.Commit();
                    return cachedEmployees;
                }
            }

            using (var stream = new MemoryStream())
            {
                // Copy tệp vào Stream
                fileImport.CopyToAsync(stream);

                //Thực hiện đọc dữ liệu
                using (var package = new ExcelPackage(stream))
                {
                    // Sheet đọc dữ liệu:
                    var currentSheet = package.Workbook.Worksheets;
                    ExcelWorksheet worksheet = currentSheet.FirstOrDefault();
                    if (worksheet != null)
                    {
                        string[] arr = { MISAResourceVN.EmployeeCode, MISAResourceVN.EmployeeName, MISAResourceVN.Gender, MISAResourceVN.Birth,
                            MISAResourceVN.Position, MISAResourceVN.Department, MISAResourceVN.PhoneNumber, MISAResourceVN.CMND,
                            MISAResourceVN.BankName };
                        // Tổng số dòng dữ liệu:
                        var rowCount = worksheet.Dimension.Rows;
                        var checkHeader = true;
                        int i = 0;
                        foreach (var item in arr)
                        {
                            if (item != worksheet?.Cells[2, i + 2]?.Value?.ToString()?.Trim())
                            {
                                checkHeader = false;
                            }
                            i++;
                        }
                   
                        if (!checkHeader)
                        {
                            throw new MISAValidateException(MISAResourceVN.FileNotValid);
                        }

                        // Bắt đầu đọc dữ liệu 
                        for (int row = 3; row <= rowCount; row++)
                        {
                            var employeeImport = new EmployeeImport();

                            //Kiểm tra mã nhân viên
                            var employeeCode = worksheet?.Cells[row, 2]?.Value?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(employeeCode))
                            {
                                employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.EmployeeCodeNotBlank);
                            }
                            else
                            {
                                if(!CheckEmployeeCode(employeeCode, employees))
                                {
                                    employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.EmployeeCodeDuplicate);
                                }
                                if (!CheckEmployeeCodeNotChar(employeeCode))
                                {
                                    employeeImport.ImportInvalidErrors.Add(MISAResourceVN.EmployeeCodeNotChar);
                                }
                            }

                            // Họ tên
                            var fullName = worksheet?.Cells[row, 3]?.Value?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(fullName))
                            {
                                employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.EmployeeNameNotBlank);
                            }

                            // Kiểm tra chức danh
                            var positionName = worksheet.Cells[row, 6]?.Value?.ToString()?.Trim();
                            positionName = (positionName == "") ? null : positionName;
                            if (positionName != null)
                            {
                                var position = _positionRepository.GetByName(positionName);
                                if (position == null)
                                {
                                    employeeImport.ImportInvalidErrors.Add(MISAResourceVN.PostionNotExist);
                                }
                                else
                                {
                                    employeeImport.PositionId = position.PositionId;
                                }
                            }
                            else
                            {
                                employeeImport.PositionId = null;
                                employeeImport.ImportInvalidErrors.Add(MISAResourceVN.PositionNotEmpty);
                            }

                            // Kiểm tra chức vụ
                            var departmentName = worksheet.Cells[row, 7]?.Value?.ToString()?.Trim();
                            if (departmentName != null)
                            {
                                var department = _departmentRepository.GetByName(departmentName);
                                if (department == null)
                                {
                                    employeeImport.ImportInvalidErrors.Add(MISAResourceVN.DepartmentExist);
                                }
                                else
                                {
                                    employeeImport.DepartmentId = department.DepartmentId;
                                }
                            }
                            else
                            {
                                employeeImport.DepartmentId = null;
                                employeeImport.ImportInvalidErrors.Add(MISAResourceVN.DepartmentNotEmpty);
                            }

                            // Kiểm tra số điện thoại(File Excel anh gửi để import employee không có cột SĐT. Em xin phép thay cột Số tài khoản thành Số điện thoại để test chức năng)
                            var phoneNumber = worksheet.Cells[row, 8]?.Value?.ToString()?.Trim();
                            phoneNumber = phoneNumber == null ? "" : phoneNumber.Trim();
                            if (phoneNumber.Length > 0)
                            {
                                if(!CheckPhoneNumber(phoneNumber, employees))
                                {
                                    employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.PhoneNumberDuplicate);
                                }
                            }

                            var genderName = worksheet.Cells[row, 4]?.Value?.ToString()?.Trim();
                            Gender gender = (Gender)((genderName == MISAResourceVN.MALE) ? 0 : (genderName == MISAResourceVN.FEMALE) ? 1 : 2);

                            var dob = worksheet.Cells[row, 5]?.Value?.ToString()?.Trim();
                            if(!string.IsNullOrEmpty(dob))
                            {
                                if(ConvertDateTime(dob) == null)
                                {
                                    employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.DateOfBirthNotValid);
                                }
                                else if(ConvertDateTime(dob) > DateTime.Now)
                                {
                                    employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.DateOfBirthNotCurrentDate);
                                }
                            }

                            employeeImport.EmployeeId = Guid.NewGuid();
                            employeeImport.EmployeeCode = employeeCode;
                            employeeImport.FullName = fullName;
                            employeeImport.Gender = gender != null ? gender : null;
                            employeeImport.DateOfBirth = ConvertDateTime(dob);
                            employeeImport.PositionName = positionName;
                            employeeImport.DepartmentName = departmentName;
                            employeeImport.PhoneNumber = phoneNumber;
                            employeeImport.IdentificationCard = worksheet.Cells[row, 9]?.Value?.ToString()?.Trim();
                            employeeImport.BankName = worksheet.Cells[row, 10]?.Value?.ToString()?.Trim();




                            // Kiểm tra trùng mã
                            var isAlreadyExistEmployeeCode = _employeeRepository.CheckDuplicateCode(employeeImport.EmployeeCode);

                            // Kiểm tra trùng số điện thoại
                            if(employeeImport.PhoneNumber.Length > 0)
                            {
                                var isAlreadyExistPhoneNumber = _employeeRepository.CheckDuplicatePhoneNumber(employeeImport.PhoneNumber);
                                if (isAlreadyExistPhoneNumber)
                                {
                                    employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.PhoneNumberAlreadyExist);
                                }
                            }

                            if (isAlreadyExistEmployeeCode)
                            {
                                employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.EmployeeCodeAlreadyExist);
                            }

                            if (employeeImport.ImportInvalidErrors.Count() == 0)
                            {
                                employeeImport.IsImported = true;
                                employeeImport.ImportInvalidErrors.Add(MISAExportFileExcel.RecordSuccess);
                            }
                            employees.Add(employeeImport);
                        }
                    }
                }

                // Lưu danh sách nhân viên vào memory cache
                cache.Set(MISAResourceVN.CachedEmployees, employees, DateTimeOffset.UtcNow.AddHours(1));
            }
            return employees;
        }

        //public void CheckFileImport(IFr)

        /// <summary>
        /// Hàm chuyển đổi string thành datetime
        /// </summary>
        /// <param name="date">chuỗi chuyển đổi</param>
        /// <returns>Trả về datetinme sau khi chuyển đổi</returns>
        /// CreatedBy: NVTruc(30/1/2024)
        public DateTime? ConvertDateTime(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                // Trả về null nếu chuỗi rỗng
                return null;
            }

            // Kiểm tra nếu chuỗi chứa ngày/tháng/năm
            if (Regex.IsMatch(date, @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$"))
            {
                // Trả về ngày/tháng/năm dưới dạng dd/MM/yyyy
                return DateTime.ParseExact(date, MISAResourceVN.FormatDate, null);
            }

            // Kiểm tra nếu chuỗi chứa tháng/năm
            if (Regex.IsMatch(date, @"^(0[1-9]|1[0-2])/\d{4}$"))
            {
                // Trả về ngày đầu tháng dưới dạng dd/MM/yyyy
                DateTime ngaySinh = DateTime.ParseExact(date, MISAResourceVN.FormatMonthYear, null);
                return new DateTime(ngaySinh.Year, ngaySinh.Month, 1);
            }

            // Kiểm tra nếu chuỗi chỉ chứa năm
            if (Regex.IsMatch(date, @"^\d{4}$") && int.TryParse(date, out int nam))
            {
                // Trả về ngày 01/01/năm dưới dạng dd/MM/yyyy
                return new DateTime(nam, 1, 1);
            }
            return null;
        }

        /// <summary>
        /// Kiểm tra xem mã nhân viên đã có trong danh sách thêm hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <param name="list">Danh sách thêm</param>
        /// <returns>
        /// false: Đã tồn tại trong danh sách
        /// true: Chưa tồn tại trong danh sách
        /// </returns>
        /// CreatedBy: NVTruc(31/1/2024)
        public bool CheckEmployeeCode(string employeeCode, IEnumerable<Employee> list)
        {
            foreach(var employee in list)
            {
                if(employeeCode == employee.EmployeeCode)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra xem số điện thoại đã có trong danh sách thêm hay chưa
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <param name="list">Danh sách thêm</param>
        /// <returns>
        /// false: Đã tồn tại trong danh sách
        /// true: Chưa tồn tại trong danh sách
        /// </returns>
        /// CreatedBy: NVTruc(31/1/2024)
        public bool CheckPhoneNumber(string phoneNumber, IEnumerable<Employee> list)
        {
            foreach (var employee in list)
            {
                if (phoneNumber == employee.PhoneNumber)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Kiểmm tra file Import
        /// </summary>
        /// <param name="fileImport">File Import</param>
        /// <exception cref="NotImplementedException"></exception>
        /// CreatedBy: NVTruc(16/1/2024)
        public void CheckFileImport(IFormFile fileImport)
        {
            if (fileImport == null || fileImport.Length <= 0)
            {
                throw new MISAValidateException(MISAResourceVN.FileImportNotNull);
            }

            if (!Path.GetExtension(fileImport.FileName).Equals(MISAResourceVN.XLSX, StringComparison.OrdinalIgnoreCase))
            {
                throw new MISAValidateException(MISAResourceVN.FileImportMalformed);
            }
        }

        /// <summary>
        /// Hàm xuất file
        /// </summary>
        /// <returns>
        /// Trả về đối tượng file
        /// </returns>
        /// CreatedBy: NVTruc(16/1/2024)
        public FileExport ExportExcel(List<EmployeeDTOs> list)
        {
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using var package = new ExcelPackage(stream);
            var workSheet = package.Workbook.Worksheets.Add(MISAExportFileExcel.TITLE);

            workSheet.Column(1).Width = 10;
            workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Column(2).Width = 20;
            workSheet.Column(3).Width = 30;
            workSheet.Column(4).Width = 20;
            workSheet.Column(5).Width = 20;
            workSheet.Column(6).Width = 20;
            workSheet.Column(7).Width = 20;
            workSheet.Column(8).Width = 30;
            workSheet.Column(9).Width = 50;
            workSheet.Column(10).Width = 20;
            workSheet.Column(11).Width = 50;
            workSheet.Column(12).Width = 20;
            workSheet.Column(13).Width = 30;
            workSheet.Column(14).Width = 30;
            workSheet.Column(15).Width = 30;
            workSheet.Column(16).Width = 40;
            workSheet.Column(17).Width = 40;
           
            if (list[0].ImportInvalidErrors.Count != 0)
            {
                workSheet.Column(18).Width = 80;
                workSheet.Cells[2, 18].Value = MISAExportFileExcel.COLUMN_STATUT;
                var row1 = workSheet.Cells[MISAExportFileExcel.R1];
               
                var row2 = workSheet.Cells[MISAExportFileExcel.R2];
                row2.Style.Fill.PatternType = ExcelFillStyle.Solid;
                row2.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                row2.Style.Font.Bold = true;
                row2.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                row2.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            using (var row = workSheet.Cells[MISAExportFileExcel.A1Q1])
            {
                row.Merge = true;
                row.Value = MISAExportFileExcel.TITLE;
                row.Style.Font.Bold = true;
                row.Style.Font.Size = 16;
                row.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // style cho excel.
            workSheet.Cells[2, 1].Value = MISAExportFileExcel.COLUMN_STT;
            workSheet.Cells[2, 2].Value = MISAExportFileExcel.COLUMN_CODE;
            workSheet.Cells[2, 3].Value = MISAExportFileExcel.COLUMN_FULLNAME;
            workSheet.Cells[2, 4].Value = MISAExportFileExcel.COLUMN_GENDER;
            workSheet.Cells[2, 5].Value = MISAExportFileExcel.COLUMN_DATEOFBIRTHDAY;
            workSheet.Cells[2, 6].Value = MISAExportFileExcel.COLUMN_PHONENUMBER;
            workSheet.Cells[2, 7].Value = MISAExportFileExcel.COLUMN_LANDLINE_PHONE;
            workSheet.Cells[2, 8].Value = MISAExportFileExcel.COLUMN_EMAIL;
            workSheet.Cells[2, 9].Value = MISAExportFileExcel.COLUMN_ADDRESS;
            workSheet.Cells[2, 10].Value = MISAExportFileExcel.COLUMN_IDENTIFICATIONCARD;
            workSheet.Cells[2, 11].Value = MISAExportFileExcel.COLUMN_PLACE_IDENTIFICATIONCARD;
            workSheet.Cells[2, 12].Value = MISAExportFileExcel.COLUMN_DATEOFIDENTITYCARD;
            workSheet.Cells[2, 13].Value = MISAExportFileExcel.COLUMN_DEPARTMENTNAME;
            workSheet.Cells[2, 14].Value = MISAExportFileExcel.COLUMN_POSITIONNAME;
            workSheet.Cells[2, 15].Value = MISAExportFileExcel.COLUMN_BANKACCOUNT;
            workSheet.Cells[2, 16].Value = MISAExportFileExcel.COLUMN_BANKNAME;
            workSheet.Cells[2, 17].Value = MISAExportFileExcel.COLUMN_BRANCH;

            using (var row = workSheet.Cells[MISAExportFileExcel.A2Q2])
            {
                row.Style.Fill.PatternType = ExcelFillStyle.Solid;
                row.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                row.Style.Font.Bold = true;
                row.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                row.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            int i = 0;
            // đổ dữ liệu từ list vào.
            foreach (var employee in list)
            {
                workSheet.Cells[i + 3, 1].Value = i + 1;
                workSheet.Cells[i + 3, 2].Value = employee.EmployeeCode;
                workSheet.Cells[i + 3, 3].Value = employee.FullName;
                workSheet.Cells[i + 3, 4].Value = (employee.Gender == Gender.FEMALE) ? MISAExportFileExcel.FEMALE : (employee.Gender == Gender.MALE) ? MISAExportFileExcel.MALE : MISAExportFileExcel.OTHER;
                workSheet.Cells[i + 3, 5].Value = employee.DateOfBirth != null ? employee.DateOfBirth?.ToString(MISAResourceVN.FormatDate) : "";
                workSheet.Cells[i + 3, 6].Value = employee.PhoneNumber;
                workSheet.Cells[i + 3, 7].Value = employee.LandlinePhone;
                workSheet.Cells[i + 3, 8].Value = employee.Email;
                workSheet.Cells[i + 3, 9].Value = employee.Address;
                workSheet.Cells[i + 3, 10].Value = employee.IdentificationCard;
                workSheet.Cells[i + 3, 11].Value = employee.PlaceIdentificationCard;
                workSheet.Cells[i + 3, 12].Value = employee.DateOfIdentityCard != null ? employee.DateOfIdentityCard?.ToString(MISAResourceVN.FormatDate) : ""; ;
                workSheet.Cells[i + 3, 13].Value = employee.DepartmentName;
                workSheet.Cells[i + 3, 14].Value = employee.PositionName;
                workSheet.Cells[i + 3, 15].Value = employee.BankAccount;
                workSheet.Cells[i + 3, 16].Value = employee.BankName;
                workSheet.Cells[i + 3, 17].Value = employee.Branch;

                if (list[0].ImportInvalidErrors.Count != 0)
                {
                    List<string> lines = new List<string>();

                    foreach (var error in employee.ImportInvalidErrors)
                    {
                        lines.Add(error.ToString());
                    }

                    string errorString = string.Join("\n", lines);

                    workSheet.Cells[i + 3, 18].Value = errorString;
                    workSheet.Cells[i + 3, 18].Style.WrapText = true;
                    workSheet.Cells[i + 3, 18].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                using (var row = workSheet.Cells[i + 3, 1, i + 3, 17])
                {
                    row.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                using (var row = workSheet.Cells[i + 3, 5])
                {
                    row.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }



                i++;
            }
            package.Save();
            stream.Position = 0;
            string excelName = MISAExportFileExcel.FILENAME;
            string excelType = MISAExportFileExcel.TYPE;
            return new FileExport(stream, excelType, excelName);
        }

        /// <summary>
        /// Paging
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <param name="pageIndex">Vị trí trang</param>
        /// <returns>Trả về 1 nếu validate thành công</returns>
        /// <exception cref="MISAValidateException"></exception>
        /// CreatedBy: NVTruc(27/12/2023)
        public int GetPaging(int pageSize, int pageIndex, string? text)
        {
            if (pageSize < 1)
            {
                throw new MISAValidateException(MISAResourceVN.PageSizeNotLessThan1);
            }
            if (pageIndex < 1)
            {
                throw new MISAValidateException(MISAResourceVN.PageIndexNotLessThan1);
            }
            return 1;
        }

        /// <summary>
        /// Hàm lấy ra mã nhân viên lớn nhất và tự động tăng
        /// </summary>
        /// <returns>
        /// Trả về mã nhân viên lớn nhất sau khi tự động tăng
        /// </returns>
        /// CreatedBy: NVTruc(27/1/2024)
        public string GetEmployeeCodeBiggest()
        {
            var employees = _employeeRepository.SortDecrease();
            Employee employee = employees.FirstOrDefault();
            var employeeCode = employee.EmployeeCode;
            var numberCode = employeeCode.Substring(3);
            var numberCodeBiggest = Convert.ToInt64(numberCode) + 1;
            numberCode = PadLeftCustom(Convert.ToString(numberCodeBiggest), numberCode.Length, '0');
            employeeCode = employeeCode.Substring(0, 3) + numberCode;
            return employeeCode;


        }

        /// <summary>
        /// Hàm kiểm tra chuỗi
        /// </summary>
        /// <param name="str">chuỗi truyền vào</param>
        /// <param name="totalWidth">Độ dài ban đầu</param>
        /// <param name="paddingChar">Kí tự thêm vào</param>
        /// <returns>
        /// Trả về chuỗi ban đầu nếu có độ dài >= độ dài ban đầu
        /// Trả về chuỗi thêm kí tự đằng trước nếu độ dài < độ dài ban đầu
        /// </returns>
        /// CreatedBy: NVTruc(27/1/2024)
        public string PadLeftCustom(string str, int totalWidth, char paddingChar)
        {
            if (str.Length >= totalWidth)
            {
                return str;
            }
            else
            {
                return new string(paddingChar, totalWidth - str.Length) + str;
            }
        }
        #endregion
    }
}
