using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace CSA03.NorthwindSystem.Data
{
    //indicate which sql table will be associated with this class
    //this is done using annotation from the System ComponentModel classes
    //the annotation need is [Table("sqltablename", Schema="dbschemasubgroupname")]
    //the subgroup schema is optional , Schema="dbschemasubgroupname"
    //to use the annotations we need to add a reference and bring in from NuGet
    //     EntityFramework

    [Table("Products")]
    public class Product
    {
        //if you are creating a database and plan to 
        //access using EntityFramework, you can save
        //yourself some work by ending your primary key
        //attribute name with ID
        //When EntityFramework maps your definition to
        //the database, EntityFrameworks will look for
        //a table with the primary key name equal to 
        //your defintion ID name.

        //if your pkey field does not end with ID then you need
        //to identify the primary key field using the annotation [Key]
        //you can use this annotation even if the field ends with ID

        //if you have a table with a compound pkey then the fields
        //envolved as the pkey fields need to be identified using
        //[Key, Column(Order=n)] where n starts at 0 and is incremented by 1

        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        //C# allows for null values inside a string
        //C# also allows for non string datatypes
        //    to contain nullable values
        //When declaring a non string nullable datatype
        //place a ? at the end of the datatype
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        //if you have a property that is NOT a field on your
        //sql table such as a read-only property then this
        //property needs the annotation [NotMapped]

        //example assume you table has firstname and lastname properties
        //assume you want a read-only property in your class that
        //will concatenate these two fields

        //[NotMapped]
        //public string FullName
        //{
        //  get
        //    {
        //      return lastname + ", " + firstname;
        //    }
        //}
    }
}
