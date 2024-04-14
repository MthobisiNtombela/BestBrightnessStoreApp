<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Best_Brightness.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
   <link rel="stylesheet" type="text/css" href="loginStyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
         <h1>Best Brightness</h1>
       <h4>Login</h4>
        <div>
            <br /> <br />
            <asp:Label ID="lbUser" runat="server" Text="Enter Username:"  CssClass="label-style"/>
            <br />
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Username:" />
            <br />
            <asp:Label ID="password" runat="server" Text="Enter Password:" CssClass="label-style" />
            <br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password:" />

            <asp:Button Font-Size="X-Small" ID="btnForgot" runat="server" Text="forgot password?" OnClick="btnForgotMethod"/> <br />

            <asp:Button fill="clear" expand="block" ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />

        </div>
         <br />
        <p>Create New Account: <asp:Button ID="btnClick" runat="server" Text="Click" /></p>
    </form>
</body>
</html>
