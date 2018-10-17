using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConsoleEF6_DataLog
{
    public class LookupListItem
    {
        public string Lookup { get; set; }
        public string Value { get; set; }
        public bool Selected  { get; set; }
    }

    public interface IRepositoryLookup : IRepository<T_Lookup>
    {
        List<LookupListItem> getLookup(string Lookup, string SelectedValue);
        T_Lookup getLookup(string Lookup);
    }
    public class RepositoryTLookup:Repository<T_Lookup>, IRepositoryLookup
    {
        private new ApplicationDbContext _Context;
        public RepositoryTLookup(ApplicationDbContext Context) : base(Context)
        {
            _Context = Context;
        }
        public List<LookupListItem> getLookup(string Lookup, string SelectedValue)
        {
            List<LookupListItem> ToReturn = new List<LookupListItem>();
            var result = _Context.lookup.Where(x => x.Lookup == Lookup).OrderBy(o => o.Value).ToList();
            foreach (var Line in result)
            {
                if (Line.Value == SelectedValue)
                {
                    ToReturn.Add(new LookupListItem()
                    {
                        Selected = true,
                        Lookup = Line.Lookup,
                        Value = Line.Value
                    });
                }
                else
                {
                    ToReturn.Add(new LookupListItem()
                    {
                        Selected = false,
                        Lookup = Line.Lookup,
                        Value = Line.Value

                    });
                }
            }
            return ToReturn;
        }
        public T_Lookup getLookup(string Lookup)
        {
            return _Context.lookup.First(x => x.Lookup == Lookup);
        }
    }
}
