<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="page._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Database</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Toggle" Text="Toggle DB:" Height="22" runat="server"/> 
        <asp:Label ID="Current" Text="remote" Height="22" runat="server" />
        <br />
    </div>
    <div>
        <asp:TextBox ID="Names" Height="65"  Width="420" runat="server" TextMode="MultiLine" />
        <br /><br />
        <asp:TextBox ID="Phones" Height="65" Width="420" runat="server" TextMode="MultiLine" />
        <br /><br />
        <asp:TextBox ID="Rooms" Height="65" Width="420" runat="server" TextMode="MultiLine" />
    </div>
    <div>
        <asp:Button ID="Search" Text="search" Height="22" Width="120" runat="server"/> 
        <asp:Button ID="Enter" Text="enter" Height="22" Width="120" runat="server"/> 
        <asp:Button ID="Remove" Text="remove" Height="22" Width="120" runat="server"/> 
        <asp:Label ID="Size" Text="" Height="22" Width="60" runat="server" />
    </div>
    </form>
</body>
</html>
