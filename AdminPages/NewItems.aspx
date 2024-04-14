<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewItems.aspx.cs" Inherits="Best_Brightness.AdminPages.NewItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NEW ITEMS</title>
      <link rel="stylesheet" type="text/css" href="newItemsStyle.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="hearder">
            <asp:Label runat="server" Text="BEST BRIGHTNESS" ID="lbBrightness" />
            <asp:Label runat="server" Text="NEW ITEM" ID="LBnewItem" />
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
         <asp:Button runat="server" Text="Existing Items" ID="btnExisting" Onclick="btnExisting_clicked"/>
        <div class="contents">
           
            <asp:Label runat="server" Text="Item Name:" ID="lblItemName" CssClass="label-style" /><br />
            <asp:TextBox runat="server" ID="txtItemName" CssClass="textbox" /><br />

            <asp:Label runat="server" Text="Quantity:" ID="lblQuantity"  CssClass="label-style" /><br />
            <asp:TextBox runat="server" ID="txtQuantity" CssClass="textbox" /><br />

            <asp:Label runat="server" Text="Cost Price:" ID="lblCostPrice"  CssClass="label-style"/><br />
            <asp:TextBox runat="server" ID="txtCostPrice" CssClass="textbox" /><br />
            <br />

            <asp:Label runat="server" Text="Item Imagee:" ID="lbItemImage"  CssClass="label-style"/>
            <asp:FileUpload ID="fileUpload" runat="server" onchange="previewImage()" />
            <br />
            <asp:Image ID="uploadedImage" runat="server" />
            <br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnSubmit_Click" />

        </div>
    </form>
</body>
    <script type="text/javascript">
    function previewImage() {
        var fileUpload = document.getElementById("fileUpload");
        var preview = document.getElementById("uploadedImage");

        if (fileUpload.files && fileUpload.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                preview.src = e.target.result;
                preview.style.display = "block";
            };
            reader.readAsDataURL(fileUpload.files[0]);
        }
    }
    </script>

</html>
