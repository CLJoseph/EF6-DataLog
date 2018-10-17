using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace ConsoleEF6_DataLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App launched");
            Console.WriteLine("=================================================================================");
            RepositoryUnitofWork UofW = new RepositoryUnitofWork(new ApplicationDbContext());

            T_Person P = new T_Person() { Title = "Mr", FirstName = "John", LastName = "Smith", ID = Guid.NewGuid() };
            T_Address A = new T_Address() {Line001 = "Line1", Line002 = "Line2", Line003 = "Line3", Line004 = "Line4", Line005 = "Line5", Code = "AAAA", ID = Guid.NewGuid()};
            T_Address B = new T_Address() { Line001 = "Line1", Line002 = "Line2", Line003 = "Line3", Line004 = "Line4", Line005 = "Line5", Code = "BBBB", ID = Guid.NewGuid()};
            List<T_Address> Addresses = new List<T_Address>();
            Addresses.Add(A);
            Addresses.Add(B);

            P.Addresses = Addresses;
            UofW.People.Add(P);
            UofW.Complete();

            Console.WriteLine("Press return to close App. ");
            Console.WriteLine("=================================================================================");
            Console.ReadLine();
        }
    }
}
