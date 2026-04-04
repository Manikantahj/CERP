using CERP.Data;
using CERP.UnitOfWork.Interfaces;
using System.Data;

namespace CERP.UnitOfWork.UnitOfWorkImplementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DapperContext _context;
        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; private set; }
        public UnitOfWork(DapperContext context)
        {
            _context = context;
        }
        public void BeginTransaction()
        {
            Connection = _context.CreateConnection();
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }
        public void Commit()
        {
            Transaction?.Commit();
            Connection?.Close();
        }

        public void Rollback()
        {
            Transaction?.Rollback();
            Connection?.Close();
        }
    }
}
