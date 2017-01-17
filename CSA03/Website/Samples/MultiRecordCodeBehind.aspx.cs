using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additonal Namespaces
using CSA03.NorthwindSystem.BLL;
using CSA03.NorthwindSystem.Data;
#endregion

public partial class Samples_MultiRecordCodeBehind : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Message.Text = "";
        if (!Page.IsPostBack)
        {
            BindCategoryList();
        }
    }
    protected void BindCategoryList()
    {
        try
        {
            CategoryController sysmgr = new CategoryController();
            List<Category> info = sysmgr.Category_List();
            info.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));
            CategoryList.DataSource = info;
            CategoryList.DataTextField = "CategoryName";
            CategoryList.DataValueField = "CategoryID";
            CategoryList.DataBind();
            CategoryList.Items.Insert(0, "select category");
        }
        catch (Exception ex)
        {
            Message.Text = GetInnerException(ex).Message;
        }
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        if (CategoryList.SelectedIndex==0)
        {
            Message.Text = "Please select a category first.";
        }
        else
        {
            //retreive the records for display
            try
            {
                ProductController sysmgr = new ProductController();
                List<Product> info = sysmgr.Products_GetByCategories(int.Parse(CategoryList.SelectedValue));
                info.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                ProductList.DataSource = info;
                ProductList.DataBind();
               
            }
            catch (Exception ex)
            {
                Message.Text = GetInnerException(ex).Message;
            }
        }
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
    protected void ProductList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //you must manually alter the PageIndex of the gridview using e.NewPageIndex
        ProductList.PageIndex = e.NewPageIndex;

        //you must refresh your data from the store area : database query
        //retreive the records for display
        try
        {
            ProductController sysmgr = new ProductController();
            List<Product> info = sysmgr.Products_GetByCategories(int.Parse(CategoryList.SelectedValue));
            info.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
            ProductList.DataSource = info;
            ProductList.DataBind();

        }
        catch (Exception ex)
        {
            Message.Text = GetInnerException(ex).Message;
        }
    }
    protected void ProductList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //optional, create a short cut variable to act as the pointer
        //to the selected GridViewRow. this is for ease of typing

        GridViewRow agvrow = ProductList.SelectedRow;

        //to access data from a gridviewrow you need to know how the gridview
        //columns were set up. Depending on the setup, there are different
        //access syntaxes that to be used.

        string errormsg = "";
        int tempInt = 0;

        //the syntax to use when the gridview columns contain web controls is:
        //  (thegridviewrowpointername.FindControl("ControlIDName") as webcontroltype).webcontrolaccessmethod
        string productname = (agvrow.FindControl("ProductName") as Label).Text;
        //string productname = (ProductList.SelectedRow.FindControl("ProductName") as Label).Text;
        string quantityperunit =(agvrow.FindControl("QuantityPerUnit") as Label).Text;
        decimal unitprice = decimal.Parse((agvrow.FindControl("UnitPrice") as Label).Text);

        //you can do validation against the contents of a web control whether by itself or
        //in a gridview cell
        if (string.IsNullOrEmpty((agvrow.FindControl("UnitsInStock") as TextBox).Text))
        {
            errormsg = "Units in Stock is empty, number required.";
        }
        else
        {
            if (!int.TryParse((agvrow.FindControl("UnitsInStock") as TextBox).Text, out tempInt))
            {
                errormsg = "Units in Stock needs to be 0 or greater.";
            }
        }

        bool discontinued = (agvrow.FindControl("Discontinued") as CheckBox).Checked;

        //display the retrieved data
        Message.Text = string.Format("Name: {0}  Qty/Unit: {1}  Price: {2}  InStock: {3}  Disc: {4}",
            productname, quantityperunit, unitprice,
            errormsg == "" ? tempInt.ToString() : errormsg,
            discontinued);
    }
}