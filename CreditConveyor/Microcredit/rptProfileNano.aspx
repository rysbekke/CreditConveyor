<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptProfileNano.aspx.cs" Inherits="Zamat.Microcredit.rptProfileNano" %>

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
<%--<asp:Panel ID="pnlProffer" runat="server">--%>
      <br />
      <input name="b_print" type="button" class="ipt" onClick="printdiv('proffer');" value=" Печать ">
<br />
<br />
<br />
<div id="proffer">
    <style>
        .centre{text-align:center;}
/**rptReceptionAct**/        
        .width100{width:100%;}
        table {border-collapse:collapse;font-size:14px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}  
    </style>
<div style = "width:720px;text-align:justify;">
<div class="centre"><b>ОАО "Дос-Кредобанк</b></div>
<div class="centre">ЗАЯВЛЕНИЕ-АНКЕТА НА ПОЛУЧЕНИЕ НАНО КРЕДИТА</div><br />
<table class="line">
    <tr>
        <td>Прошу предоставить мне потребительский кредит на электронный кошелек №</td>
        <td><asp:Label ID="lblPhone0" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone1" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone2" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone3" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone4" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone5" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone6" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone7" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone8" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone9" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone10" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone11" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone12" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone13" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone14" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone15" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblPhone16" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
    <br />
в сумме ________<asp:Label ID="lblRequestSumm" runat="server" Text="[Кредит.Сумма]"></asp:Label>
    __________________________________&nbsp;&nbsp; сроком на ____<asp:Label ID="lblRequestPeriod" runat="server" Text="[Кредит.Срок]"></asp:Label>_______&nbsp;&nbsp;&nbsp; месяц
    <br />
<br />



<br />

<table class="line">
<tr>
    <td colspan ="6">Личные данные</td>
</tr>
<tr>
    <td>Ф.И.О. клиента:</td>
    <td colspan ="5">
        <asp:Label ID="lblCustomerFIO" runat="server" Text="[Ф.И.О. клиента]"></asp:Label></td>
</tr>
<tr>
    <td>Дата рождения:</td>
    <td>
        <asp:Label ID="lblCustomerBirth" runat="server" Text="[Дата рожд]"></asp:Label></td>
    <td>ИНН:</td>
    <td>
        <asp:Label ID="lblINN" runat="server" Text="[ИНН]"></asp:Label></td>
    <td>Пол (нужное отметить)</td>
    <td>□ муж&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; □ жен</td>
</tr>
<tr>
    <td>Паспорт серия №:</td>
    <td>
        <asp:Label ID="lblDocumentSeries" runat="server" Text="[Клиент.Документ.Серия]"></asp:Label><asp:Label ID="lblDocumentNo" runat="server" Text="[Клиент.Документ.Номер]"></asp:Label>
    </td>
    <td>Дата выдачи:</td>
    <td><asp:Label ID="lblIssueDate" runat="server" Text="[Клиент.Документ.ДатаВыдачи]"></asp:Label></td>
    <td>Кем выдан</td>
    <td><asp:Label ID="lblIssueAuthority" runat="server" Text="[Клиент.Документ.Выдан]"></asp:Label></td>
</tr>
<tr>
    <td>Адрес прописки:</td>
    <td colspan ="5"><asp:Label ID="lblRegCustomerAddress" runat="server" Text="[Клиент.Адрес.Прописка]"></asp:Label></td>
</tr>
<tr>
    <td>Адрес проживания:</td>
    <td colspan ="5">
        <asp:Label ID="lblResCustomerAddress" runat="server" Text="[Клиент.Адрес.Проживания]"></asp:Label>
    </td>
</tr>
<tr>
    <td>Семейное положение:</td>
    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
    <td colspan="4">
        □ Женат&nbsp;&nbsp;&nbsp;&nbsp; □ Замужем&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; □ Не замужем<br />
        □ Холост&nbsp;&nbsp;&nbsp; □ Разведен(а) </td>
</tr>
<tr>
    <td>Телефоны:</td>
    <td></td>
    <td colspan="4">если женат/замужем (указать ФИО супруга (-и)</td>
</tr>
</table>

    <br />


<%--<table class="line">
<tr>
    <td colspan="2">Скорбалл и данные по доходам</td>
</tr>
<tr>
    <td>Скорбалл</td>
    <td>
        <asp:Label ID="lblScorPoint" runat="server" Text="Label"></asp:Label></td>
</tr>
<tr>
    <td>Группа Клиента по доходам</td>
    <td></td>
</tr>
<tr>
    <td>Средний доход в зависимости от Группы Клиента</td>
    <td>
        <asp:Label ID="lblIncome" runat="server" Text="Label"></asp:Label></td>
</tr>
</table>--%>


    <br />


<table class="line">
<tr>
    <td colspan="2">Сведения о кредитной линии</td>
</tr>
<tr>
    <td>Сумма кредитной линии</td>
    <td><asp:Label ID="lblRequestSumm2" runat="server" Text="[Кредит.Сумма]"></asp:Label> &nbsp;<asp:Label ID="lblRequestSummWord" runat="server" Text="[Кредит.ОбщаяСумма.Пропись]"></asp:Label></td>
    <td>(цифрами и прописью)</td>
</tr>
<tr>
    <td>Срок кредитной линии</td>
    <td><asp:Label ID="lblRequestPeriod2" runat="server" Text="[Кредит.Срок]"></asp:Label></td>
    <td></td>
</tr>
</table>


    <br />

<table class="line">
<tr>
    <td colspan="4">Укажите родственников или знакомых, с которыми можно связаться в случае необходимости и которые могут стать поручителями по кредиту:</td>
</tr>
<tr>
    <td>Ф.И.О.:</td>
    <td></td>
    <td>Ф.И.О.:</td>
    <td></td>
</tr>
<tr>
    <td>Кем приходится:</td>
    <td></td>
    <td>Кем приходится:</td>
    <td></td>
</tr>
<tr>
    <td>Адрес:</td>
    <td></td>
    <td>Адрес:</td>
    <td></td>
</tr>
<tr>
    <td>Телефоны:</td>
    <td></td>
    <td>Телефоны:</td>
    <td></td>
</tr>
</table>

    <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Подпись заявителя:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Дата заполнения анкеты:<br />
        <br />
    <br />
    <br />
    *Заполняется сотрудником банка
        <br />


   <table class="line">
    <tr>
        <td rowspan = "2">Проверил по списку наблюдений Банка (Санкционные перечни и др.)</td>
        <td rowspan = "2">&nbsp;&nbsp; &nbsp;</td>
        <td rowspan = "2">Дата проверки по списку наблюдений Банка (Санкционные перечни и др.)</td>
        <td>Результат</td>
        <td>Подпись</td>
    </tr>
    <tr>
        <td>&nbsp</td>
        <td></td>
    </tr>

    <tr>
        <td rowspan = "2">Проверил на аффилированность</td>
        <td rowspan = "2"></td>
        <td rowspan = "2">Дата проверки на аффилированность</td>
        <td>Результат</td>
        <td>Подпись</td>
    </tr>
    <tr>
        <td>&nbsp</td>
        <td></td>
    </tr>

    <tr>
        <td rowspan = "2">Проверил по КИБ</td>
        <td rowspan = "2"></td>
        <td rowspan = "2">Дата проверки по КИБ</td>
        <td>Результат КИБ</td>
        <td>Подпись</td>
    </tr>
    <tr>
        <td>&nbsp</td>
        <td></td>
    </tr>


</table>
        														

    </div>
</div>

    </form>
</body>
</html>
