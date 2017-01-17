<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MultiRecordCodeBehind.aspx.cs" Inherits="Samples_MultiRecordCodeBehind" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <link href="../Content/GridViewOverrides.css" rel="stylesheet" />
    <div class="page-header">
        <h1>Product By Category - Code Behind</h1>
    </div>
    <div class="row col-md-12">
        <div class="alert alert-warning">
            <blockquote style="font-style:italic">
                This illustrates multiple record display query using code behind for the processing.
            </blockquote>
        </div>
    </div>

    <div class="row">
       <%-- this area will be the search controls, submit buttons and messages--%>
          <div class="col-md-12"> 
            <div class="col-md-offset-2">
                <asp:Label ID="Label4" runat="server" Text="Select a category"></asp:Label>&nbsp;&nbsp;
                <asp:DropDownList ID="CategoryList" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                <asp:LinkButton ID="Search" runat="server" Font-Size="X-Large" Font-Bold="True" OnClick="Search_Click">Search</asp:LinkButton>&nbsp;&nbsp;
                <br /><br />
                <asp:Label ID="Message" runat="server" ></asp:Label>          
            </div>
          </div>
    </div>
     <%-- this area will be the display control--%>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="ProductList" runat="server" AutoGenerateColumns="False"
                 Caption="Category Products" CssClass="tipGrid table table-condensed"  
                AllowPaging="True" PageSize="5" OnPageIndexChanging="ProductList_PageIndexChanging" OnSelectedIndexChanged="ProductList_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField CausesValidation="False" SelectText="View" 
                        ShowSelectButton="True"></asp:CommandField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <%-- The ItemTemplate refers to the default row display of the gridview
                                If you plan to access the web control within the ItemTemplate
                                    then assign it a unique name (ID="xxxxx")
                                To associate a data field in you data set to this web control
                                    you will change the Text="  " so that it contains the Eval("datasetfieldname")
                                Since there will be inner quotes used in the Eval() the outer quotes
                                    will need to be changed to single quotes Text='    '
                                Case and spelling of the fieldname must match the datasetfieldname   --%>
                            <asp:Label ID="ProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle BackColor="#999999" Font-Size="Large"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty/Unit">
                        <ItemTemplate>
                            <asp:Label ID="QuantityPerUnit" runat="server" Text='<%# Eval("QuantityPerUnit") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle BackColor="#999999" Font-Size="Large" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price ($)">
                        <ItemTemplate>
                            <%-- You can use string formating within the 
                                web control to alter your display.--%>
                            <asp:Label ID="UnitPrice" runat="server" Text='<%# string.Format("{0:0.00}", Eval("UnitPrice")) %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle BackColor="#999999" Font-Size="Large" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="In Stock">
                        <ItemTemplate>
                            <%-- Templates may contain a valid web control
                                web controls could be display (Label) or input (TextBox)--%>
                            <asp:TextBox ID="UnitsInStock" runat="server" Text='<%# Eval("UnitsInStock") %>' ></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle BackColor="#999999" Font-Size="Large" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Disc">
                        <ItemTemplate>
                            <%-- You could use an iif expression inside your web control
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("Discontinued") == "Y" ? true : false %>' />--%>
                            <asp:CheckBox ID="Discontinued" runat="server" Checked='<%#Eval("Discontinued") %>' />
                        </ItemTemplate>
                        <HeaderStyle BackColor="#999999" Font-Size="Large" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No data available at this time.
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

