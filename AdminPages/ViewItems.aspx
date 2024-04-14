<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewItems.aspx.cs" Inherits="Best_Brightness.AdminPages.ViewItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Stock</title>
            <link rel="stylesheet" type="text/css" href="viewItemsStyleSheet.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
         <div class="hearder">
            <asp:Label runat="server" Text="BEST BRIGHTNESS" ID="lbBrightness" />
            <asp:Label runat="server" Text="VIEW ITEMS" ID="LBnewItem" />
            <hr />
        </div>
     
          <div class="navbar">
            <label id="contentMenu" for="menu-toggle">&#9776;Content</label>
              <input type="checkbox" id="menu-toggle" />
              <ul>
                 <li><a href="main.aspx"><i class="fas fa-home"></i>Home</a></li>
                  <li><a href="moveItems.aspx"><i class="fas fa-arrows-alt"></i>Move Items</a></li>
                  <li><a href="ViewItems.aspx"><i class="fas fa-list-ul"></i>View Items</a></li>
                   <li><a href="NewItems.aspx"><i class="fas fa-plus"></i>Add new Item</a></li>
                  <li><a href="Salse.aspx"><i class="fas fa-dollar-sign"></i>Sales</a></li>
                  <li><a href="../login.aspx"><i class="fas fa-sign-out-alt"></i>Log Out</a></li>
              </ul>

             <br /><br /><br /><br />
              <div class="content">
                  <asp:Button runat="server" ID="btnRefresh" Text="Refresh" OnClick="btnRefresh_clicked" />
                  <asp:Label runat="server" Text="Search:" ID="lbSearch" CssClass="label-style" />
                  <asp:TextBox runat="server" ID="txtSearch" />
                  <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" /><br />

                   <asp:Label runat="server" CssClass="titles">WAREHOUSE ITEMS</asp:Label><br />

                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridView">
                        <Columns>
                            
                            <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:BoundField DataField="CostPrice" HeaderText="Cost Price" />

                        </Columns>
                    </asp:GridView>
                    <asp:Label runat="server" ID="lblSelectedItemName" CssClass="label-style"></asp:Label>
                </div>

                <asp:Label runat="server" CssClass="titles">STORE ROOM ITEMS</asp:Label><br />
                


                <div class="gridview-container">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="GridView">
                        <Columns>
                          
                            <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:BoundField DataField="SellingPrice" HeaderText="Selling Price" />

                        </Columns>
                    </asp:GridView>
                    <asp:Label runat="server" ID="Label1" CssClass="label-style"></asp:Label>
                </div>

                <asp:Label runat="server" CssClass="titles">STOCK ITEMS</asp:Label><br />
               

                <div class="gridview-container">
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"  CssClass="GridView">
                        <Columns>
                          
                            <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:BoundField DataField="SellingPrice" HeaderText="Selling Price" />

                        </Columns>
                    </asp:GridView>
                    <asp:Label runat="server" ID="Label2" CssClass="label-style"></asp:Label>
                </div>

              </div>
    </form>
</body>
</html>
