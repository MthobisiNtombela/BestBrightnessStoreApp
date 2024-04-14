<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="existingItems.aspx.cs" Inherits="Best_Brightness.AdminPages.existingItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EXISTING ITEMS</title>
      <link rel="stylesheet" type="text/css" href="existingItemsStyleSheet.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hearder">
            <asp:Label runat="server" Text="BEST BRIGHTNESS" ID="lbBrightness" />
            <asp:Label runat="server" Text="EXISTING ITEMS" ID="LBnewItem" />
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
        <div class="content">

             
                
       
            
                 <asp:Button runat="server" ID="btnRefresh" Text="Refresh" OnClick="btnRefresh_clicked"/>
              <asp:Label runat="server" Text="Search:" ID="lbSearch" CssClass="label-style" />
                <asp:TextBox runat="server" ID="txtSearch" />
                <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
               
                    <div class="gridview-container">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="select-button" />
                                <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="CostPrice" HeaderText="Cost Price" />

                            </Columns>
                        </asp:GridView>
                    </div>
              
                <br />
                <asp:Label runat="server" ID="lblSelectedItemName" CssClass="label-style"></asp:Label>
                <br />
                 <asp:Label runat="server" Text="Quantity:" ID="lblQuantity" CssClass="label-style" /><br />
                <asp:TextBox runat="server" ID="txtQuantity"  TextMode="Number" CssClass="textbox" /><br />

                <asp:Label runat="server" Text="Cost Price:"  ID="lblCostPrice" CssClass="label-style" /><br />
                <asp:TextBox runat="server" ID="txtCostPrice"  CssClass="textbox" /><br />

                  <asp:Button runat="server" ID="btnSave" Text="Save Item" OnClick="btnSave_cliked"/>
          
        </div>
    </form>
</body>
</html>
