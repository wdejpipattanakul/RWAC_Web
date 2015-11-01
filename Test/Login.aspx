<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Test.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="~/Content/Style.css" rel="stylesheet" /> 
<title>RWAC Login Screen</title>
</head>
<body>
<form id="form1" runat="server">
<div>
<h1 style="text-align:center">RWAC Login Screen</h1> 
<div class="main">
<div class="login">
<p><asp:TextBox ID="uname" runat="server" type="text"  placeholder="Username"></asp:TextBox></p>
<p><asp:TextBox ID="upass" runat="server" type="password"  placeholder="Password"></asp:TextBox></p>
<p class="forgot">
<label>
<a href="#">Forgot Password ?</a>
</label>
</p>
<p class="submit">
<asp:Button runat="server" ID="submir" type="submit" Text="Login" OnClick="submir_Click" />
</p>
</div>
<div class="footer">
<p>&copy; 2015 All rights reserved by Jimmy Consulting Group.,Ltd
<a href="http://jimmyconsultinggroup.com/" target="_blank">jimmyconsultinggroup.com</a>
</p>
</div>
</div>   
</div>
</form>
</body>
</html>
