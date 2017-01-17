using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using CSA03.NorthwindSystem.Data;
using CSA03.NorthwindSystem.DAL;
using System.Data.SqlClient;
using System.ComponentModel; //for ODS
#endregion

namespace CSA03.NorthwindSystem.BLL
{
    //expose the class
    [DataObject]
    public class ProductController
    {
        //retreive all the rows of a specific sql table
        //expose a method
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Product> Product_List()
        {
            using(var context = new NorthwindContext())
            {
                return context.Products.ToList();
                
            }
        }

        //retreive the row of a specific pkey from the sql table
          [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Product Product_Get(int productid)
        {
            using (var context = new NorthwindContext())
            {
                return context.Products.Find(productid);

            }
        }

        //add a row to the database table
        //optionally return a value
          [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Product_Add(Product newitem)
        {
            using (var context = new NorthwindContext())
            {
                //on the add, the process will return a copy of the new item from the database
                Product addeditem = context.Products.Add(newitem);
                //YOU MUST REQUEST THAT THE CHANGES TO THE DATABASE BE SAVED
                context.SaveChanges();
                return addeditem.ProductID;
            }
        }

        //update an existing row on the database table
          [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Product_Update(Product newitem)
        {
            using (var context = new NorthwindContext())
            {
                //indicate to entity framework that the record has been altered
                //and to use the supplied instance to update the database record
                context.Entry(newitem).State = System.Data.Entity.EntityState.Modified;
                //YOU MUST REQUEST THAT THE CHANGES TO THE DATABASE BE SAVED
                context.SaveChanges();
               
            }
        }

        //if you plan to do a entity level CRUD via ODS
        //then you need to create a Delete method that
        //will recieve the entity, extract the entity pkey
        //then call the appropriate delete method
        
        //overloaded method
          [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Product_Delete(Product item)
          {
              Product_Delete(item.ProductID);
          }

        //deleting an existing row from the database table
        //overloaded method
        public void Product_Delete(int productid)
        {
            using (var context = new NorthwindContext())
            {
                //retrieve the existing record from the database
                Product existing = context.Products.Find(productid);

                //physcial request the removal to the existing record (instance)
                context.Products.Remove(existing);

                //if, for some reason you cannot physically remove a record
                //you may need to logically indicate the record is not to be used
                //existing.theflagattributefieldname = desiredvalue;
                //context.Entry(existing).Property("theflagattributefieldname").IsModified = true;
                
                //YOU MUST REQUEST THAT THE CHANGES TO THE DATABASE BE SAVED
                context.SaveChanges();

            }
        }
          [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Product> Products_GetByCategories(int categoryid)
        {
            using (var context = new NorthwindContext())
            {
                //this is an example of using a Sql stored procedure
                //the collection returned is a generic collection
                //   that can be type cast

                //the call to the database will use .Database.SqlQuery<datatype>()
                //the query contents consists of the procedurename, any parameterplaceholdername,
                //  a SqlParameter object relating the Parameterplaceholder and the supplied 
                //  parameter variable

                IEnumerable<Product> results = context.Database.SqlQuery<Product>("Products_GetByCategories @CategoryID",
                    new SqlParameter("CategoryID", categoryid));
                return results.ToList();
            }
        }
    }//eoc
}//eon
