using AutoMapper;
using Dapper;
using demo_infastructure.Repository;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MISA.AMISDemo.Infrastructure.UnitOfWork;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class EmployeeReponsitory : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Field
        IMISADbContext _dbContext;
        IPositionRepository _positionRepository;
        IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Contructor
        public EmployeeReponsitory(IMISADbContext dbContext, IUnitOfWork unitOfWork, IPositionRepository positionRepository, IDepartmentRepository departmentRepository, IMapper mapper) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
            _positionRepository = positionRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public EmployeeReponsitory(IMISADbContext dbContext, IUnitOfWork unitOfWork):base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xoá thông tin
        /// </summary>
        /// <param name="employeeId">Id nhân viên</param>
        /// <returns>Trả về 1 nếu xoá thành công</returns>
        /// CreateBy: NVTruc(30/12/2023)
        public new int Delete(Guid employeeId)
        {
            return base.Delete(employeeId);
        }

        /// <summary>
        /// Lấy toàn bộ thông tin
        /// </summary>
        /// <returns>Trả về tất cả bản ghi</returns>
        /// CreateBy: NVTruc(30/12/2023)
        public new IEnumerable<Employee> GetAll()
        {
            return base.GetAll();
        }

        /// <summary>
        /// Lấy thông tin theo Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Trả về bản ghi theo Id</returns>
        /// CreateBy: NVTruc(30/12/2023)
        public new Employee GetById(Guid employeeId)
        {
            return base.GetById(employeeId);
        }

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <returns>Trả về 1 nếu thêm thành công</returns>
        /// CreateBy: NVTruc(30/12/2023)
        public new int Insert(Employee employee)
        {
            return base.Insert(employee);
        }

        /// <summary>
        /// Câp nhật nhân viên
        /// </summary>
        /// <param name="employee">Nhân viên</param>
        /// <param name="employeeId">Id nhân viên</param>
        /// <returns>Trả về 1 nếu cập nhật thành công</returns>
        /// CreateBy: NVTruc(30/12/2023)
        public new int Update(Employee employee, Guid employeeId)
        {
            return base.Update(employee, employeeId);
        }

        /// <summary>
        /// Sắp xếp giảm dần
        /// </summary>
        /// <returns>
        /// Trả về danh sách nhân viên giảm dần theo mã
        /// </returns>
        /// CreateBy: NVTruc(30/12/2023)
        public IEnumerable<Employee> SortDecrease()
        {
            var sqlCommand = $"SELECT * FROM Employee";
            var misaEntity = _dbContext.Connection.Query<Employee>(sql: sqlCommand);
            return misaEntity.OrderByDescending(employee => Convert.ToInt64(employee.EmployeeCode.Substring(3)));
        }

        /// <summary>
        /// Paging
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi</param>
        /// <param name="text">Nội dung tìm kiếm</param>
        /// <param name="pageIndex">Vị trí trang</param>
        /// <returns>Trả về số bản ghi muốn tìm</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public IEnumerable<EmployeeDTOs> GetPaging(int pageSize, int pageIndex, string text)
        {
            var sqlCommand = "Proc_Paging";
            DynamicParameters paramet = new DynamicParameters();
            paramet.Add("pageSize", pageSize);
            paramet.Add("pageIndex", pageIndex);
            paramet.Add("text", text);

            var misaEntity = _dbContext.Connection.Query<EmployeeDTOs>(sql: sqlCommand, param: paramet, commandType: System.Data.CommandType.StoredProcedure);
            return misaEntity;
        }

        /// <summary>
        /// Lấy tất cả danh sách nhân viên và liên kết với chức vụ và phòng ban
        /// </summary>
        /// <returns>
        /// Danh sách nhân viên sau khi liên kết
        /// </returns>
        /// CreateBy: NVTruc(30/12/2023)
        public IEnumerable<EmployeeDTOs> GetEmployees()
        {
            var sqlCommand = "Proc_GetEmployees";
            var res = _dbContext.Connection.Query<EmployeeDTOs>(sql: sqlCommand, commandType: System.Data.CommandType.StoredProcedure);
            return res;
        }

        /// <summary>
        /// Kiểm sô điện thoại tồn tại chưa
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại</param>
        /// <returns>
        /// false: Số điện thoại chưa tồn tại
        /// true: Số điện thoại đã tồn tại</returns>
        /// CreateBy: NVTruc(28/12/2023)
        public bool CheckDuplicatePhoneNumber(string phoneNumber)
        {


            var sqlCommand = $"SELECT * FROM Employee WHERE PhoneNumber = @phoneNumber";

            DynamicParameters paramet = new DynamicParameters();

            paramet.Add("@phoneNumber", phoneNumber);

            var misaEntity = _dbContext.Connection.QueryFirstOrDefault(sql: sqlCommand, param: paramet);

            if (misaEntity == null)
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// Tìm kiếm bản ghi theo text
        /// </summary>
        /// <param name="text">Nội dung muốn tìm kiếm</param>
        /// <returns>
        /// Trả về tất cả bản ghi có nội dung tìm kiếm
        /// </returns>
        /// CreateBy: NVTruc(25/12/2024)
        public IEnumerable<Employee> Search(string text)
        {
            var sqlCommand = $"SELECT * FROM Employee WHERE EmployeeCode LIKE @text OR fullName LIKE @text OR phoneNumber LIKE @text";
            DynamicParameters paramet = new DynamicParameters();
            paramet.Add("@text", "%" + text + "%", System.Data.DbType.String);

            var misaEntity = _dbContext.Connection.Query<Employee>(sql: sqlCommand, param: paramet);
            return misaEntity;
        }
        #endregion
    }
}
