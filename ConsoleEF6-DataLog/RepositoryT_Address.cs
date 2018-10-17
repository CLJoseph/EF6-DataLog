using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEF6_DataLog
{
    
    public interface IRepositoryAddress : IRepository<T_Address>
    {        
    }

    public class RepositoryTAddress : Repository<T_Address>, IRepositoryAddress
    {
        private new ApplicationDbContext _Context;
        public RepositoryTAddress(ApplicationDbContext Context) : base(Context)
        {
            _Context = Context;
        }
    }
}
