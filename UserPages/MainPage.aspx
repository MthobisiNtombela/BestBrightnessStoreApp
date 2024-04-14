<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Best_Brightness.UserPages.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>HOME</title>
      <link rel="stylesheet" type="text/css" href="MainPageStyleSheet.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hearder">
            <asp:Label runat="server" Text="BEST BRIGHTNESS" ID="Label2" />
            <asp:Label runat="server" Text="MENU" ID="LBnewItem" />
            <hr />
        </div>

        <div class="navbar">
            <label id="contentMenu" for="menu-toggle">&#9776;Content</label>
            <input type="checkbox" id="menu-toggle" />
            <ul>
                <li><a href="MainPage.aspx"><i class="fas fa-home"></i>Home</a></li>
                <li><a href="cart.aspx"><i class="fas fa-shopping-cart"></i>My Cart</a></li>
                <li><a href="../login.aspx"><i class="fas fa-sign-out-alt"></i>Log Out</a></li>
            </ul>
            
  
    </div>
     <asp:Repeater ID="rptItems" runat="server">
    <ItemTemplate>
        <div class="item-container">
            <img class="item-image" src='<%# Eval("ItemUrl") %>' alt='<%# Eval("ItemName") %>' runat="server" ID="Img1" />
            <div class="item-name">
                <asp:Literal runat="server" ID="litItemName" Text='<%# Eval("ItemName") %>'></asp:Literal>
            </div>
            <div class="item-price">
                <asp:Literal runat="server" ID="litItemPrice" Text='<%# Eval("SellingPrice", "{0:C}") %>'></asp:Literal>
            </div>
            <br />
            <div class="cartBtn">
                <asp:Button runat="server" Text="Add To Cart" ID="btnCart" OnCommand="btnCart_Click" CommandArgument='<%# Eval("ItemName") %>' />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
    
    </form>
  
</body>
</html>
