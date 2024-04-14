<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="Best_Brightness.UserPages.cart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>MY CART</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"/>
     <link rel="stylesheet" type="text/css" href="cartStyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
             <div class="hearder">
            <asp:Label runat="server" Text="BEST BRIGHTNESS" ID="lbBrightness" />
            <asp:Label runat="server" Text="MY CART" ID="LBnewItem" />
            <hr />
        </div>
     
          <div class="navbar">
            <label id="contentMenu" for="menu-toggle">&#9776;Content</label>
              <input type="checkbox" id="menu-toggle"/>
              <ul>
                 <li><a href="MainPage.aspx"><i class="fas fa-home"></i>Home</a></li>
                  <li><a href="cart.aspx"><i class="fas fa-shopping-cart"></i>My Cart</a></li>
                  <li><a href="../login.aspx"><i class="fas fa-sign-out-alt"></i>Log Out</a></li>
              </ul>

            
        </div>

         <div>
           
              <div class="gridview-container">
                <asp:Button runat="server" ID="btnAdd" Text="ADD" OnClick="btnADD_cliked" cssClass="buttons-style"/>
              <asp:Button runat="server" ID="btnLESS" Text="DECREASE" OnClick="btnLESS_cliked" cssClass="buttons-style" />
              <asp:Button runat="server" ID="btnREMOVE" Text="REMOVE" OnClick="btnDELETE_cliked" cssClass="buttons-style"/>

                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                     <Columns>
                         <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="select-button" />
                         <asp:BoundField DataField="ItemID" HeaderText="Item ID" />
                         <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                         <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                         <asp:BoundField DataField="SellingPrice" HeaderText="Selling Price" />
                         <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" />
                     </Columns>
                 </asp:GridView>

                 <asp:Label runat="server" ID="lblSelectedItemName" CssClass="label-style"></asp:Label>
             </div>

             <div class="contents">

                 <asp:Label runat="server" Text="TOTAL" ID="LBTOTAL" CssClass="label-style" /><br />
                 <asp:TextBox runat="server" ID="txtTOTAL" CssClass="textbox" /><br />

                 <asp:Label runat="server" Text="AMOUNT PAYED:" ID="lbAmountpayd" CssClass="label-style" /><br />
                 <asp:TextBox runat="server" ID="txtPay" CssClass="textbox" /><br />

                 <asp:Button ID="btnPAY" runat="server" Text="PAY" OnClick="btnPAY_Click"/>
                 <br />
                 <asp:Label runat="server" Text="CHANGE:" ID="lbCHANGE" CssClass="label-style" /><br />
                 <asp:TextBox runat="server" ID="txtChange" CssClass="textbox" /><br />

             </div>

              
        </div>
       
    </form>
</body>
</html>
