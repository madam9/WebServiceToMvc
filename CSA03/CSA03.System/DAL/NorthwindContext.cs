using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using CSA03.NorthwindSystem.Data; //this is to your entity definitions
using System.Data.Entity;
#endregion

namespace CSA03.NorthwindSystem.DAL
{
    //internal allows other classes within this component class library
    //to have access to this class
    //calls to this class from outside the component class library
    //will be rejected as if this class was private.

    //this class will inherit from EntityFramework the DbContext class
    internal class NorthwindContext : DbContext
    {
        //you must code the default constructor for this class
        //so when an instance of NorthwindContext is created
        //it will pass as a parameter value the connection string name
        public NorthwindContext():base("NWDB")
        {
            //default constructor
        }

        //create a properties which will be the reference
        //to each entity in the sql table
        //the returndatatype is the Entity class as a collection
        //the returandatatype is DbSet<T> where T is your entity
        public DbSet<Product> Products{get;set;}
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}
