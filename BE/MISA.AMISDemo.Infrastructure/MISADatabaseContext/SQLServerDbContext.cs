using MISA.AMISDemo.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Infrastructure.MISADatabaseContext
{
    public class SQLServerDbContext : IMISADbContext
    {
        public IDbConnection Connection => throw new NotImplementedException();

        public IDbTransaction Transaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public int Delete<Type>(Guid misaEntityId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int Insert<Type>(Type type)
        {
            throw new NotImplementedException();
        }

        public int MultipleDelete(List<Guid> misaEntityIds)
        {
            throw new NotImplementedException();
        }

        public int MultipleDelete<Type>(List<Guid> misaEntityIds)
        {
            throw new NotImplementedException();
        }

        public int MultiplePost<Type>(List<Type> records)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public int Update<Type>(Type type, Guid typeId)
        {
            throw new NotImplementedException();
        }
    }
}
