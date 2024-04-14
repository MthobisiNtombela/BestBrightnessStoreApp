<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Best_Brightness.AdminPages.main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main</title>
     <link rel="stylesheet" type="text/css" href="mainStyle.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hearder">
            <asp:Label runat="server" Text="BEST BRIGHTNESS" ID="Label3" />
            <asp:Label runat="server" Text="MAIN" ID="LBnewItem" />
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
       
        <div class="contents">
           
            <span  class="mydiv">
      
            <asp:Label runat="server" Text="WELCOME" ID="Label1"/>
            <br />
            <asp:Label runat="server" Text="TO" ID="Label2"/>
            <br />
            
           <asp:Label runat="server" Text="BEST BRIGHTNESS" ID="LBbrightness"></asp:Label>
            </span>
             
          
        </div>
    </form>
</body>
</html>
