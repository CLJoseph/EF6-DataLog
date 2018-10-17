using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConsoleEF6_DataLog
{
    // conventions used here 
    // Tables prefixed with T_<table name>,  makes it easier to find the SQL server and using intellisense. 
    // all tables have a ID field , a GUID of 40 chars in length.
    // Guid is used to enable a row key to be generated outside of the database. 


    [Table(name: "T_Person")]
    public class T_Person
    {
        [Key]
        [Required]
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<T_Address> Addresses { get; set; }
        //public ICollection<T_Address> Addresses { get; set; }

    }
    [Table(name: "T_Address")]
    public class T_Address
    {      
        [Key]
        [Required]
        public Guid ID { get; set; }
        public string Line001 { get; set; }
        public string Line002 { get; set; }
        public string Line003 { get; set; }
        public string Line004 { get; set; }
        public string Line005 { get; set; }
        public string Code { get; set; }
    }
    [Table(name: "T_Lookup")]
    public class T_Lookup
    {
        [Key]
        [Required]
        public Guid ID { get; set; }
        public string Lookup { get; set; }
        public string Value { get; set; }
    }
    [Table(name: "T_DBLog")]
    public class T_DbLog
    {
        [Key]
        [Required]      
        public Guid ID { get; set; }
        public DateTime LogDateTime { get; set; }    // DateTime on computer 
        public string UserId { get; set; }              
        public string UserName { get; set; }
        public string Device { get; set; }           // details of the device the user is using   
        public string TableName { get; set; }        // table affected 
        public string TableRowID { get; set; }      
        public string Event { get; set; }            // add,update or remove data from a table
        public string Detail { get; set; }           // row columns affected. 
    }
}
