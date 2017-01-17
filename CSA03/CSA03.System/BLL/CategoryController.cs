using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using CSA03.NorthwindSystem.Data;
using CSA03.NorthwindSystem.DAL;
using System.ComponentModel;  //required for exposing classes and methods to ODS
#endregion

namespace CSA03.NorthwindSystem.BLL
{
    //you must expose the class to the ODS for it to have access
    //to the methods within
    [DataObject]
    public class CategoryController
    {
        //return all records on the sql table
        //to expose a method to be available to an ODS
        //you will use the syntax [DataObjectMethod(DataObjectMethodType.xxxxx, boolDefault)]
        //where xxxxx is Select, Insert, Update, Delete
        //boolDefault indicates whether the method is shown during configuration as
        //    the default method or no default method shown
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Category> Category_List()
        {
            using (var context = new NorthwindContext())
            {

                return context.Categories.ToList();
            }
        }

        //return a single record from the sql table that matches
        //the pKey value supplied
        public Category Category_Get(int categoryid)
        {
            using (var context = new NorthwindContext())
            {

                return context.Categories.Find(categoryid);
            }
        }
    }
}
