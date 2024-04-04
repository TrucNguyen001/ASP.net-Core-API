using AutoMapper;
using demo_infastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MISA.AMISDemo.Core.Interfaces.Services;
using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MISA.AMISDemo.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Api.Tests.Repository
{
    public class EmployeeRepositoryTests : EmployeeReponsitory
    {
        #region Field
        IMISADbContext _dbContext;
        IUnitOfWork _unitOfWork;
        IPositionRepository _positionRepository;
        IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Contructor
        public EmployeeRepositoryTests(IMISADbContext dbContext, IUnitOfWork unitOfWork, IPositionRepository positionRepository, IDepartmentRepository departmentRepository, IMapper mapper) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
            _positionRepository = positionRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public EmployeeRepositoryTests(IMISADbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="misaEntity">Mã bản ghi</param>
        /// <returns>
        /// false: Mã chưa tồn tại
        /// true: Mã đã tồn tại</returns>
        /// CreateBy: NVTruc(28/12/2023
        public new bool CheckDuplicateCode(string employeeCode)
        {
            return base.CheckDuplicateCode(employeeCode);
        }
        #endregion
    }
}
