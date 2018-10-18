using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;



namespace ConsoleEF6_DataLog
{
    public interface IRepositoryUnitofWork : IDisposable
    {
        bool Complete();
    }
    public class RepositoryUnitofWork : IRepositoryUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public RepositoryUnitofWork(ApplicationDbContext Context)
        {
            _context = Context;
            Lookup = new RepositoryTLookup(_context);
            People = new RepositoryTPerson(_context);
            Addresses = new RepositoryTAddress(_context);
            DBLog = new RepositoryTDBLog(_context);
        }

        public IRepositoryLookup Lookup { get; private set; }
        public IRepositoryPerson People { get; private set; }
        public IRepositoryAddress Addresses { get; private set; }
        public IRepositoryTDBLog DBLog { get; private set; }

        public bool Complete()
        {
            try
            {
                //GetChanges(((IObjectContextAdapter)_context).ObjectContext.ObjectStateManager);
                GetChanges();
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException Ex)
            {
                Console.WriteLine("Error :" + Ex.Message);
                Console.WriteLine("Error :" + Ex.InnerException.ToString());

                return false;
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error :" + Ex.Message + " Detail :" + Ex.InnerException.InnerException.Message);
                return false;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void GetChanges(ObjectStateManager Entities)
        {
            var entries = Entities.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted);
            foreach (var Item in entries)
            {
            }
        }
        public void GetChanges()
        {
            // now add the changes recorded to the db context
            List<T_DbLog> AllChanges = new List<T_DbLog>();
            foreach (var Item in _context.ChangeTracker.Entries().ToList()  )
            {
                if (Item.Entity is T_DbLog || Item.State == EntityState.Detached || Item.State == EntityState.Unchanged)
                {
                    continue;
                }
                T_DbLog change = new T_DbLog();
                change.ID = Guid.NewGuid();
                change.LogDateTime = DateTime.Now;
                change.TableName = Item.Entity.GetType().Name;
                switch (Item.State)
                {
                    case EntityState.Added:
                        change.Event = "Added";
                        break;
                    case EntityState.Deleted:
                        change.Event = "Deleted";
                        break;
                    case EntityState.Modified:
                        change.Event = "Modified";
                        break;
                }
                foreach (var Prop in Item.CurrentValues.PropertyNames)
                {
                    if (Prop == "ID" || Prop == "Id")
                    {
                        change.TableRowID = Item.CurrentValues[Prop].ToString();
                        continue;
                    }
                    change.Detail = change.Detail + Prop + ":" + Item.CurrentValues[Prop].ToString() + " ";
                }
                var ItemProperties = Item.Entity.GetType().GetProperties();
                foreach (var test in ItemProperties)
                {
                    if (test.PropertyType.IsGenericType == true)
                    {
                        switch (test.Name)
                        {
                            case "Addresses":
                                var Alist = Item.Entity.GetType().GetProperty(test.Name).GetValue(Item.Entity, null); 
                                var Collection = (List<T_Address>)Alist;
                                foreach(var Line in Collection)
                                {
                                    change.TableFK += "T_Address ID:" + Line.ID + " ";
                                }
                                break;
                            default: break;
                        }                       
                    }
                }
                AllChanges.Add(change);
            }
            // now add the changes recorded to the db context 
            _context.DBlog.AddRange(AllChanges);
        }

    }
}
   