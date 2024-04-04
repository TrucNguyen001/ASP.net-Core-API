using AutoMapper;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        #region Constructor
        public AutoMapperProfile() {
            CreateMap<EmployeeImport, Employee>();
            CreateMap<Employee, EmployeeDTOs>();
        }
        #endregion
    }
}
