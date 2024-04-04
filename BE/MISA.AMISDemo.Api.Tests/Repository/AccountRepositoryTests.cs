using MISA.AMISDemo.Core.Interfaces.UnitOfWork;
using MISA.AMISDemo.Infrastructure.Interfaces;
using MISA.AMISDemo.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Api.Tests.Repository
{
    public class AccountRepositoryTests : AccountRepository
    {
        public AccountRepositoryTests(IMISADbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }
    }
}
