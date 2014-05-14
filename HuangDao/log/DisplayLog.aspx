<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayLog.aspx.cs" Inherits="HuangDao.log.DisplayLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1" />
<meta content="width=device-width,user-scalable=no" name="viewport" />
<meta name="apple-mobile-web-app-capable" content="yes" />
    <title>查看日志文件</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnPrev" runat="server" Text="Previous" OnClick="btnPrev_Click" />
        <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
        <asp:Button ID="btnRefresh" runat="server" Text="Refesh" OnClick="btnRefresh_Click" />

        <asp:Literal ID="spanCurrentFileName" runat="server"></asp:Literal>
        <asp:HiddenField ID="fieldCurrentDateTime" runat="server" />
    <div>
        <asp:TextBox ID="txbLogFileContent" runat="server" Height="451px" TextMode="MultiLine" Width="743px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
