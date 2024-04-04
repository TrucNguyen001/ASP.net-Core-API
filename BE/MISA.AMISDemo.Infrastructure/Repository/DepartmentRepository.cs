using Dapper;
using demo_infastructure.Repository;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        IMISADbContext _dbContext;
        IUnitOfWork _unitOfWork;

        public DepartmentRepository(IMISADbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Lấy dữ liệu theo tên phòng ban
        /// </summary>
        /// <param name="departmentName">Tên phòng ban</param>
        /// <returns>Trả về bản ghi theo tên phòng ban</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public Department GetByName(string departmentName)
        {
            var sqlCommand = $"SELECT * FROM Department WHERE DepartmentName = @departmentName";

            DynamicParameters paramet = new DynamicParameters();

            paramet.Add("@departmentName", departmentName);

            var deparment = _dbContext.Connection.QueryFirstOrDefault<Department>(sql: sqlCommand, param: paramet);

            return deparment;
        }
    }
}
