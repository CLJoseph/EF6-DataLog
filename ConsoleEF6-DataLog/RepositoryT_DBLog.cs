using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEF6_DataLog
{
    
    public interface IRepositoryTDBLog : IRepository<T_DbLog>
    {
        
    }

    public class RepositoryTDBLog : Repository<T_DbLog>, IRepositoryTDBLog
    {
        private new ApplicationDbContext _Context;
        public RepositoryTDBLog(ApplicationDbContext Context) : base(Context)
        {
            _Context = Context;
        }
    }

}
