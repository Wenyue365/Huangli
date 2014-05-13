<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayLog.aspx.cs" Inherits="HuangDao.log.DisplayLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnPrev" runat="server" Text="Previous" OnClick="btnPrev_Click" />
        <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
        <asp:Literal ID="spanCurrentFileName" runat="server"></asp:Literal>
        <asp:HiddenField ID="fieldCurrentDateTime" runat="server" />
    <div>
        <asp:TextBox ID="txbLogFileContent" runat="server" Height="451px" TextMode="MultiLine" Width="743px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
