using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork:IDisposable, IAsyncDisposable
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }

        // Bắt đầu mở
        void BeginTransaction();
        Task BeginTransactionAsync();

        // Commit
        void Commit();
        Task CommitAsync();

        // Quay lại khi có lỗi
        void Rollback();
        Task RollbackAsync();
    }
}
