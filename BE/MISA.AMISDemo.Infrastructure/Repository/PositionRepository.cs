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
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        IMISADbContext _dbContext;

        public PositionRepository(IMISADbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Lấy dữ liệu theo tên chức vụ
        /// </summary>
        /// <param name="PositionName">Tên chức vụ</param>
        /// <returns>Trả về bản ghi theo tên chức vụ</returns>
        /// CreatedBy: NVTruc(28/12/2023)
        public Position GetByName(string positionName)
        {
            var sqlCommand = $"SELECT * FROM Position WHERE PositionName = @positionName";

            DynamicParameters paramet = new DynamicParameters();

            paramet.Add("@positionName", positionName);

            var position = _dbContext.Connection.QueryFirstOrDefault<Position>(sql: sqlCommand, param: paramet);

            return position;
        }
    }
}
