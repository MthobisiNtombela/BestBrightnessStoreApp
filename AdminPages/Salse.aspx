<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salse.aspx.cs" Inherits="Best_Brightness.AdminPages.Salse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SALSE</title>
     <link rel="stylesheet" type="text/css" href="SalseStyleSheet.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
            <div class="hearder">
            <asp:Label runat="server" Text="BEST BRIGHTNESS" ID="lbBrightness" />
            <asp:Label runat="server" Text="VIEW SOLD ITEMS" ID="LBnewItem" />
            <hr />
        </div>

          <div class="navbar">
            <label id="contentMenu" for="menu-toggle">&#9776;Content</label>
            <input type="checkbox" id="menu-toggle"/>
            <ul>
        
 <li><a href="main.aspx"><i class="fas fa-home"></i>Home</a></li>
                  <li><a href="moveItems.aspx"><i class="fas fa-arrows-alt"></i>Move Items</a></li>
                  <li><a href="ViewItems.aspx"><i class="fas fa-list-ul"></i>View Items</a></li>
                   <li><a href="NewItems.aspx"><i class="fas fa-plus"></i>Add new Item</a></li>
                  <li><a href="Salse.aspx"><i class="fas fa-dollar-sign"></i>Sales</a></li>
                  <li><a href="../login.aspx"><i class="fas fa-sign-out-alt"></i>Log Out</a></li>
            </ul>
        </div>

        <br />
        <div class="gridview-container">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
                    <asp:BoundField DataField="UserID" HeaderText="User ID" />
                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="SellingPrice" HeaderText="Selling Price" />
                    <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
