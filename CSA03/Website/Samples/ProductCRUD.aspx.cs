using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using CSA03.NorthwindSystem.BLL;
using CSA03.NorthwindSystem.Data;
#endregion

public partial class Samples_ProductCRUD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this method executes on each internet trip to this page.
        //clear old messages
        Message.Text = "";

        //you can test to determine if this is the first trip or a repeat (postback)
        //trip using Page.IsPostBack
        //the first trip IsPostBack is false
        //the repeated trip IsPostBack is true

        if (!Page.IsPostBack)
        {
            //one can do initialization work for the form before it
            //appears to the user for the first time

            BindProductList();
            BindSupplierList();
            BindCategoryList();

        }
    }

    protected void BindProductList()
    {
        //this method is used to load the ProductList ddl
        //create an instance of the required BLL class
        ProductController sysmgr = new ProductController();
        //retreive all the rows from the Product table in sql
        List<Product> info = sysmgr.Product_List();

        //sort List<T> ascending
        info.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));

        //load the ddl with the rows, assign the ddl fields and bind data
        ProductList.DataSource = info;
        ProductList.DataTextField = "ProductName";
        ProductList.DataValueField = "ProductID";
        ProductList.DataBind();
        //place a prompt line on the ddl.
        ProductList.Items.Insert(0, "select product");
    }


    protected void BindSupplierList()
    {
        SupplierController sysmgr = new SupplierController();
        List<Supplier> info = sysmgr.Supplier_List();
        info.Sort((x, y) => x.CompanyName.CompareTo(y.CompanyName));
        SupplierList.DataSource = info;
        SupplierList.DataTextField = "CompanyName";
        SupplierList.DataValueField = "SupplierID";
        SupplierList.DataBind();
        SupplierList.Items.Insert(0, "select supplier");
    }


    protected void BindCategoryList()
    {
        CategoryController sysmgr = new CategoryController();
        List<Category> info = sysmgr.Category_List();

        //to sort a List<t> descending

        //method one -1 * (x.fieldname.CompareTo(y.fieldname))
        //info.Sort((x, y) => -1 * (x.CategoryName.CompareTo(y.CategoryName)));

        //method two switch x and y to y.fieldname.CompareTo(x.fieldname)
        info.Sort((x, y) => y.CategoryName.CompareTo(x.CategoryName));

        CategoryList.DataSource = info;
        CategoryList.DataTextField = "CategoryName";
        CategoryList.DataValueField = "CategoryID";
        CategoryList.DataBind();
        CategoryList.Items.Insert(0, "select category");
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        //ensure that you have an argument to actually do the lookup
        //in this example, we need to have a SelectedIndex greater than 0
        if (ProductList.SelectedIndex == 0)
        {
            Message.Text = "Please select a product to query.";
        }
        else
        {
            //you have a query argument

            //use user friendly error handling around your process
            try
            {
                //standard query
                //create an instance of the controller class
                ProductController sysmgr = new ProductController();
                //call a method within the controller instance that
                //does the query
                Product info = sysmgr.Product_Get(int.Parse(ProductList.SelectedValue));
                //test the results of the query
                //if NOT found, issue an appropriate message
                //if found, load the form controls with the record data
                if (info == null)
                {
                    //query failed record not found
                    Message.Text = "Product " + ProductList.SelectedItem + " not found.";
                    //optionally you could refresh the query argument list
                    BindProductList();
                }
                else
                {
                    //record found, load display controls
                    //data going to the controls need to be strings
                    //data cannot be null
                    //ddl should use SelectedValue for positioning
                    ProductID.Text = info.ProductID.ToString();
                    ProductName.Text = info.ProductName;
                    if (info.SupplierID == null)
                    {
                        SupplierList.SelectedIndex = 0;
                    }
                    else
                    {
                        SupplierList.SelectedValue = info.SupplierID.ToString();
                    }
                    if (info.CategoryID == null)
                    {
                        CategoryList.SelectedIndex = 0;
                    }
                    else
                    {
                        CategoryList.SelectedValue = info.CategoryID.ToString();
                    }

                    QuantityPerUnit.Text = info.QuantityPerUnit == null ? "" : info.QuantityPerUnit;

                    //for currency amounts you should consider formatting your value
                    UnitPrice.Text = info.UnitPrice == null ? "" : string.Format("{0:0.00}",info.UnitPrice);

                    UnitsInStock.Text = info.UnitsInStock == null ? "" : info.UnitsInStock.ToString();
                    UnitsOnOrder.Text = info.UnitsOnOrder == null ? "" : info.UnitsOnOrder.ToString();
                    ReorderLevel.Text = info.ReorderLevel == null ? "" : info.ReorderLevel.ToString();
                    Discontinued.Checked = info.Discontinued;
                }
            }
            catch (Exception ex)
            {
                Message.Text = GetInnerException(ex).Message;
            }
        }
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        //all controls fields should be emptied for reset
        ProductID.Text = "";
        ProductName.Text = "";
        SupplierList.SelectedIndex = 0;
        CategoryList.SelectedIndex = 0;
        QuantityPerUnit.Text = "";
        UnitPrice.Text = "";
        UnitsInStock.Text = "";
        UnitsOnOrder.Text = "";
        ReorderLevel.Text = "";
        Discontinued.Checked = false;
    }
    protected void AddProduct_Click(object sender, EventArgs e)
    {
        //you can revalidate your web form controls on the server side
        //using the validation controls
        if (Page.IsValid)
        {
            //the data is valid according to your validation controls

            //you may have business rules that need additional validation
            //that cannot be done via the validation controls
            //example: ddl check for prompt line
            try
            {
                // standard add
                // collect the data from the web form controls
                Product newitem = GetProductData();
                // create an instance of the controller class
                ProductController sysmgr = new ProductController();
                //issue the command to the controller class
                //receiving the new pkey value is optional and a desgin decision
                int newprimarykeyID = sysmgr.Product_Add(newitem);
                //check the results of the command and issue an appropriate message.
                //optionally refresh any required control on the web form such as a ddl
                Message.Text = "Product: " + newprimarykeyID.ToString() + " has been added";
                BindProductList();
            }
            catch (Exception ex)
            {
                Message.Text = GetInnerException(ex).Message;
            }
        }
    }
    protected void UpdateProduct_Click(object sender, EventArgs e)
    {
        //ensure that the user has search for the existing record
        //on the database.
        //this can be done by checking the contents of your Label  control
        //which carries the primary key
        if (string.IsNullOrEmpty(ProductID.Text))
        {
            Message.Text = "You must first search for the existing Product data. Update not done.";
        }
        else
        {
            if (Page.IsValid)
            {
                //since the pkey label field could be altered by a
                //"bad" person you should check to see if the pkey
                //value is an appropriate value
                int productid;
                if (int.TryParse(ProductID.Text, out productid))
                {
                    //you can assume that your data should have meet
                    //you basic validation and you can start the update
                    try
                    {
                        // standard update
                        // collect the data from the web form controls
                        Product newitem = GetProductData();

                        //also, in the update we need to collect and store
                        //the Label pkey value
                        newitem.ProductID = productid;

                        // create an instance of the controller class
                        ProductController sysmgr = new ProductController();
                        //issue the command to the controller class
                       
                        sysmgr.Product_Update(newitem);
                        //check the results of the command and issue an appropriate message.
                        //optionally refresh any required control on the web form such as a ddl
                        Message.Text = "Product: " + productid.ToString() + " has been updated.";
                        BindProductList();
                        ProductList.SelectedValue = productid.ToString();
                    }
                    catch (Exception ex)
                    {
                        Message.Text = GetInnerException(ex).Message;
                        BindProductList();
                    }
                }
                else
                {
                    Message.Text = "Product ID is invalid. Update not done.";
                }
            }
        }
    }
    protected void RemoveProduct_Click(object sender, EventArgs e)
    {
        //ensure that the user has search for the existing record
        //on the database.
        //this can be done by checking the contents of your Label  control
        //which carries the primary key
        if (string.IsNullOrEmpty(ProductID.Text))
        {
            Message.Text = "You must first search for the existing Product data. Remove not done.";
        }
        else
        {
           
                //since the pkey label field could be altered by a
                //"bad" person you should check to see if the pkey
                //value is an appropriate value
                int productid;
                if (int.TryParse(ProductID.Text, out productid))
                {
                    //you can assume that your data should have meet
                    //you basic validation and you can start the update
                    try
                    {
                        // standard delete
                        // create an instance of the controller class
                        ProductController sysmgr = new ProductController();
                        //issue the command to the controller class
                        sysmgr.Product_Delete(productid);
                        //check the results of the command and issue an appropriate message.
                        //optionally refresh any required control on the web form such as a ddl
                        Message.Text = "Product: " + productid.ToString() + " has been removed.";
                       
                    }
                    catch (Exception ex)
                    {
                        Message.Text = GetInnerException(ex).Message;
                        
                    }
                    finally
                    {
                        BindProductList();
                        ProductID.Text = "";
                    }
                }
                else
                {
                    Message.Text = "Product ID is invalid. Update not done.";
                }
       }
    }

    protected Product GetProductData()
    {
        //create a new instance of the entity
        //access the web data controls
        //extract the data values
        //add to the appropriate instance field
        //finally, return the instance
        Product newitem = new Product();
        newitem.ProductName = ProductName.Text;
        if (SupplierList.SelectedIndex == 0)
        {
            newitem.SupplierID = null;
        }
        else
        {
            newitem.SupplierID = int.Parse(SupplierList.SelectedValue);
        }
        if (CategoryList.SelectedIndex == 0)
        {
            newitem.CategoryID = null;
        }
        else
        {
            newitem.CategoryID = int.Parse(CategoryList.SelectedValue);
        }
        //immediateIF (iif) syntax    condition(s) ? true path value : false path value
        //if ( condition(s))
        //{ true path}
        //else { false path}
        newitem.QuantityPerUnit = string.IsNullOrEmpty(QuantityPerUnit.Text) ? null : QuantityPerUnit.Text;
        if (string.IsNullOrEmpty(UnitPrice.Text))
        {
            newitem.UnitPrice = null;
        }
        else
        {
            newitem.UnitPrice = decimal.Parse(UnitPrice.Text);
        }
        if (string.IsNullOrEmpty(UnitsInStock.Text))
        {
            newitem.UnitsInStock = null;
        }
        else
        {
            newitem.UnitsInStock = Int16.Parse(UnitsInStock.Text);
        }
        if (string.IsNullOrEmpty(UnitsOnOrder.Text))
        {
            newitem.UnitsOnOrder = null;
        }
        else
        {
            newitem.UnitsOnOrder = Int16.Parse(UnitsOnOrder.Text);
        }
        if (string.IsNullOrEmpty(ReorderLevel.Text))
        {
            newitem.ReorderLevel = null;
        }
        else
        {
            newitem.ReorderLevel = Int16.Parse(ReorderLevel.Text);
        }
        newitem.Discontinued = Discontinued.Checked;
        return newitem;
    }

    protected Exception GetInnerException(Exception ex)
    {
        //drill down through the layers of InnerExceptions until
        //we get to the actual exception that has your detail error message
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
        }
        return ex;
    }
}//eoc