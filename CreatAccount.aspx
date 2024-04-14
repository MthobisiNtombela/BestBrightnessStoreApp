<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatAccount.aspx.cs" Inherits="Best_Brightness.CreatAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CreatAccount</title>
    <link rel="stylesheet" type="text/css" href="CreatAccountStyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
         <h1>Create Account</h1>
        <div id="MyDiv">
             <br /> <br />
            <asp:Label ID="LBfname" runat="server" Text="Enter Firstname"  CssClass="label-style"/>
           
            <asp:TextBox ID="txtFname" runat="server" placeholder="Firstname:" />
            <br />
            <asp:Label ID="LBsname" runat="server" Text="Enter Surname:" CssClass="label-style" />

            <asp:TextBox ID="txtLname" runat="server"  placeholder="Enter Surname:" />
            <br />
               <asp:Label ID="LBusersname" runat="server" Text="Enter Username:" CssClass="label-style" />

            <asp:TextBox ID="txtUsername" runat="server"  placeholder="Enter Username:" />
            <br />
               <asp:Label ID="LBpassword" runat="server" Text="Enter Password:" CssClass="label-style" />

            <asp:TextBox ID="txtpassword" runat="server"  placeholder="Enter password:" />
            <br />
               <asp:Label ID="LBpassword1" runat="server" Text="Confirm Password:" CssClass="label-style" />

            <asp:TextBox ID="txtConfirmPassword" runat="server"  placeholder="Confirm Password:" />
            <br />

            <asp:Button ID="btnRegister" runat="server" Text="Register"  OnClick="btnRegister_Click"/>
         
        </div>
    </form>
</body>
</html>
