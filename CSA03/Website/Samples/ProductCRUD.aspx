<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProductCRUD.aspx.cs" Inherits="Samples_ProductCRUD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  <div class="page-header">
        <h1>Product CRUD Maintenance</h1>
    </div>
    <div class="row col-md-12">
        <div class="alert alert-warning">
            <blockquote style="font-style:italic">
                This illustrates a single web page CRUD maintenance scenario.
            </blockquote>
        </div>
    </div>

    <div class="row">
       <%-- this area will be the search controls, submit buttons and messages--%>
          <div class="col-md-12"> 
            <div class="col-md-offset-2">
                <asp:Label ID="Label4" runat="server" Text="Select a product"></asp:Label>&nbsp;&nbsp;
                <asp:DropDownList ID="ProductList" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                <asp:LinkButton ID="Search" runat="server" CausesValidation="false" OnClick="Search_Click" Font-Size="X-Large" Font-Bold="True">Search</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="Clear" runat="server" CausesValidation="false" OnClick="Clear_Click" Font-Size="X-Large" Font-Bold="True">Clear</asp:LinkButton>&nbsp;&nbsp;
                 <asp:LinkButton ID="AddProduct" runat="server" Font-Size="X-Large" Font-Bold="True" OnClick="AddProduct_Click">Add</asp:LinkButton>&nbsp;&nbsp;
                 <asp:LinkButton ID="UpdateProduct" runat="server"  Font-Size="X-Large" Font-Bold="True" OnClick="UpdateProduct_Click">Update</asp:LinkButton>&nbsp;&nbsp;
                 <asp:LinkButton ID="RemoveProduct" runat="server"  Font-Size="X-Large" Font-Bold="True" OnClick="RemoveProduct_Click">Remove</asp:LinkButton>
            </div>
              <br /><br />
            <asp:Label ID="Message" runat="server" ></asp:Label>
              <br />
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                   HeaderText="Please fix the indicated issues with your data."/>
              <asp:RequiredFieldValidator ID="RequiredFieldProductName" runat="server" 
                  ErrorMessage="Product Name is required."
                  Display="None" ControlToValidate="ProductName" SetFocusOnError="True">
              </asp:RequiredFieldValidator>
              <asp:CompareValidator ID="CompareUnitPrice" runat="server" 
                  ErrorMessage="Unit Price is a dollar amount 0 or greater"
                  Display="None" SetFocusOnError="True"
                  ControlToValidate="UnitPrice" Operator="GreaterThanEqual" Type="Double" ValueToCompare="0.0">
              </asp:CompareValidator>
                 <asp:CompareValidator ID="CompareUnitsInStock" runat="server" 
                  ErrorMessage="Units in Stock is an amount 0 or greater"
                  Display="None" SetFocusOnError="True"
                  ControlToValidate="UnitsInStock" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0">
              </asp:CompareValidator>
                  <asp:CompareValidator ID="CompareUnitsOnOrder" runat="server" 
                  ErrorMessage="Units on Order is an amount 0 or greater"
                  Display="None" SetFocusOnError="True"
                  ControlToValidate="UnitsOnOrder" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0">
              </asp:CompareValidator>
                     <asp:CompareValidator ID="CompareReorderLevel" runat="server" 
                  ErrorMessage="ROL is an amount 0 or greater"
                  Display="None" SetFocusOnError="True"
                  ControlToValidate="ReorderLevel" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="0">
              </asp:CompareValidator>
        </div>

        <%--this area will be the product form--%>
        <div class="col-md-12">
            <fieldset class="form-horizontal">
                <legend>Product</legend>

               <%-- since each control should have a label, we will pair
                label plus associated control to create the individual
                data request item.--%>
                <asp:Label ID="Label1" runat="server" Text="Product ID"
                     AssociatedControlID="ProductID"></asp:Label>
                <asp:Label ID="ProductID" runat="server"></asp:Label>
               
                <asp:Label ID="Label2" runat="server" Text="Name"
                     AssociatedControlID="ProductName"></asp:Label>
                <asp:TextBox ID="ProductName" runat="server"></asp:TextBox>
               
                <asp:Label ID="Label3" runat="server" Text="Supplier"
                     AssociatedControlID="SupplierList"></asp:Label>
                <asp:DropdownList ID="SupplierList" runat="server"></asp:DropdownList>
               
                      <asp:Label ID="Label6" runat="server" Text="Category"
                     AssociatedControlID="CategoryList"></asp:Label>
                <asp:DropDownList ID="CategoryList" runat="server" ></asp:DropDownList> 
               
                    <asp:Label ID="Label7" runat="server" Text="Quantity/Unit"
                     AssociatedControlID="QuantityPerUnit"></asp:Label>
                <asp:TextBox ID="QuantityPerUnit" runat="server" MaxLength="20" ></asp:TextBox> 

                    <asp:Label ID="Label8" runat="server" Text="Unit Price"
                     AssociatedControlID="UnitPrice"></asp:Label>
                <asp:TextBox ID="UnitPrice" runat="server" ></asp:TextBox> 

                    <asp:Label ID="Label9" runat="server" Text="In Stock"
                     AssociatedControlID="UnitsInStock"></asp:Label>
                <asp:TextBox ID="UnitsInStock" runat="server" ></asp:TextBox> 

                    <asp:Label ID="Label10" runat="server" Text="On Order"
                     AssociatedControlID="UnitsOnOrder"></asp:Label>
                <asp:TextBox ID="UnitsOnOrder" runat="server" ></asp:TextBox> 

                    <asp:Label ID="Label11" runat="server" Text="ROL"
                     AssociatedControlID="ReorderLevel"></asp:Label>
                <asp:TextBox ID="ReorderLevel" runat="server" ></asp:TextBox> 
                              
                <asp:Label ID="Label5" runat="server" Text="Status"
                     AssociatedControlID="Discontinued"></asp:Label>
                <asp:CheckBox ID="Discontinued" runat="server" Text="Discontinued">
                </asp:CheckBox>
            </fieldset>
           
        </div>
      
    </div>
    <script src="../Scripts/bootwrap-freecode.js"></script>
</asp:Content>

