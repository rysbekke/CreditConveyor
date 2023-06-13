<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptRequests.aspx.cs" Inherits="СreditСonveyor.Card.rptRequests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <script type="text/javascript">
function printdiv(printpage)
{
var headstr = "<html><head><title></title></head><body>";
var footstr = "</body>";
var newstr = document.all.item(printpage).innerHTML;
var oldstr = document.body.innerHTML;
document.body.innerHTML = headstr+newstr+footstr;
w=window.open("","_blank","k");
w.document.write(headstr+newstr+footstr);
w.print();
document.body.innerHTML = oldstr;
return false;
}
function printdivclose(printpage)
{
    document.getElementById(printpage).style.display = 'none';
}
       </script>
<asp:HiddenField ID="hfCustomerID" runat="server" />
<asp:HiddenField ID="hfCreditID" runat="server" />

<asp:Panel ID="pnlRptRequests" runat="server">
    <br />
    <input name="b_print" type="button" class="ipt" onClick="printdiv('rptRequests');" value=" Печать "><br />
    <br />
    &nbsp;<div id="rptRequests">

<div style = "width:720px;text-align:justify;">
<h4 class="centre">Отчет за <asp:Label ID="lblDate" runat="server" Text="[Дата]"></asp:Label></h4>
<br />
    <table>
        <tr>
            <td>&nbsp;&nbsp;</td>
            <td>Логин агента: </td>
            <td><asp:Label ID="lblAgentLogin" runat="server" Text="Логин"></asp:Label></td>
            <td>ФИО агента: </td>
            <td><asp:Label ID="lblAgentFIO" runat="server" Text="ФИО"></asp:Label></td>
        </tr>
    </table>
    <table>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><b>Статус заявок</b></td>
            <td><b>Количество</b></td>
            <td><b>Сумма</b></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td class="auto-style1">Отказан</td>
            <td class="auto-style1">
                <asp:Label ID="lblCountCancel" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style1"></td>
        </tr>
         <tr>
            <td></td>
            <td>Новая заявка</td>
            <td>
                <asp:Label ID="lblCountWait" runat="server" Text="Label"></asp:Label>
             </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
             </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>Утвержден</td>
            <td>
                <asp:Label ID="lbl" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td><b>Всего заявок:</b></td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
            </td>
            <td></td>
        </tr>
    </table>
    
    <br />
</div>
</div>

    
</asp:Panel>
    </form>
</body>
</html>
