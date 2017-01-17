using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using CSA03.NorthwindSystem.Data;
using CSA03.NorthwindSystem.DAL;
using System.ComponentModel; //ODS
#endregion

namespace CSA03.NorthwindSystem.BLL
{
    [DataObject]
    public class SupplierController
    {

        //return all records on the sql table
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Supplier> Supplier_List()
        {
            using (var context = new NorthwindContext())
            {

                return context.Suppliers.ToList();
            }
        }

        //return a single record from the sql table that matches
        //the pKey value supplied
        public Supplier Supplier_Get(int supplierid)
        {
            using (var context = new NorthwindContext())
            {

                return context.Suppliers.Find(supplierid);
            }
        }
    }
}
