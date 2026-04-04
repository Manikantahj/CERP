using System.Data;

namespace CERP.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
