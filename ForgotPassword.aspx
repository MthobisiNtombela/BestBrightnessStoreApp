<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Best_Brightness.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
       <link rel="stylesheet" type="text/css" href="ForgotStyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
        
               <h1>Best Brightness</h1>
       <h4>Change Password</h4>
             <div>
            <br /> <br />
            <asp:Label ID="lbUser" runat="server" Text="Enter Password:"  CssClass="label-style"/>
            <br />
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password:" />
            <br />
            <asp:Label ID="password" runat="server" Text="Confirm Password:" CssClass="label-style" />
            <br />
            <asp:TextBox ID="txtNewPassword" runat="server"  placeholder="Confirm Password:" />

          <br />

            <asp:Button fill="clear" ID="btnLogin" runat="server" Text="Change Password" OnClick="ChangePass" />

        </div>
     
    </form>
</body>
</html>
