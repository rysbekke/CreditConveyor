<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptProffer.aspx.cs" Inherits="СreditСonveyor.Partners2.rptProffer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

<script type="text/javascript">
    function printdiv(printpage) {
        var headstr = "<html><head><title></title></head><body>";
        var footstr = "</body>";
        var newstr = document.all.item(printpage).innerHTML;
        var oldstr = document.body.innerHTML;
        document.body.innerHTML = headstr + newstr + footstr;
        w = window.open("", "_blank", "k");
        w.document.write(headstr + newstr + footstr);
        w.print();
        document.body.innerHTML = oldstr;
        return false;
    }
    function printdivclose(printpage) {
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
        body {font-size:12px;}
        table {border-collapse:collapse;font-size:12px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}  
        .auto-style1 {
            height: 17px;
        }
    </style>
<div style = "width:720px;text-align:justify;">
    <div style = "text-align:right;">
   <asp:Label ID="lblBranch" runat="server" Text="Филиал"></asp:Label>,
    Офис - <asp:Label ID="lblOffice" runat="server" Text="Офис"></asp:Label>,
    № - <asp:Label ID="lblRequestN" runat="server" Text="№"></asp:Label>
</div>
<div class="centre"><b>Предложение на Кредитный комитет</b></div>
<div class="centre">(источник погашения - заработная плата)<br />
        </div>
Данные о клиенте:<br />
        <table class="line">
    <tr>
        <td>Ф.И.О.</td>
        <td><asp:Label ID="lblCustomerFIO" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Адрес прописки</td>
        <td><asp:Label ID="lblCustomerRegAddress" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Адрес проживания</td>
        <td><asp:Label ID="lblCustomerResAddress" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Телефоны:</td>
        <td><asp:Label ID="lblContactPhone1" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Место работы:</td>
        <td><asp:Label ID="lblWorkName" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Должность:</td>
        <td><asp:Label ID="lblWorkPosition" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Сотрудник КапиталБанк</td>
        <td>   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   да&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; /&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; нет  </td>
    </tr>
    <tr>
        <td>Продавец:</td>
        <td><asp:Label ID="lblSeller" runat="server"></asp:Label></td>
    </tr>
</table>        										
<div class="centre">
    Расчет платежеспособности клиента<br />
        </div>
<table class="line">
    <tr>
        <th>Наименование статьи</th>
        <th>Показатели за месяц, сом</th>
        <th>Комментарии к статьям</th>
    </tr>
    <tr>
        <td>Среднемес.зар. плата клиента:</td>
        <td><asp:Label ID="lblAverageMonthSalary" runat="server" Text=""></asp:Label></td>
        <td>Зарплата клиента</td>
    </tr>
    <tr>
        <td>Доп.доход:</td>
        <td><asp:Label ID="lblAdditionalIncomes" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td class="auto-style1">ИТОГО ДОХОДОВ</td>
        <td class="auto-style1"><asp:Label ID="lblAverageMonthSalary2" runat="server" Text=""></asp:Label></td>
        <td class="auto-style1"></td>
    </tr>
    <tr>
        <td>Семейные расходы</td>
        <td><asp:Label ID="lblFamilyExpenses" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>ИТОГО РАСХОДОВ </td>
        <td><asp:Label ID="lblTotalСonsumption" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>ЧИСТАЯ ПРИБЫЛЬ</td>
        <td>
            <asp:Label ID="lblChp" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>Взнос банку ОАО &quot;КапиталБанк&quot;</td>
        <td>
            <asp:Label ID="lblK" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>Взносы по другим кредитам:</td>
        <td>
            <asp:Label ID="lblOtherLoans" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>ЧИСТЫЙ ОСТАТОК ДЕНЕГ</td>
        <td>
            <asp:Label ID="lblCho" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>Чистый остаток в %&nbsp; соотношении</td>
        <td>
            <asp:Label ID="lblY1" runat="server" Text=""></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblIssuanceOfCredit" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Соотношение ежемесячных платежей по кредиту на совокупный доход</td>
        <td>
            <asp:Label ID="lblY2" runat="server" Text=""></asp:Label>
        </td>
        <td><asp:Label ID="lblIssuanceOfCredit2" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
<div class="centre">
    Заключение:</div>
Данные о финансировании:
<table class="line">
    <tr>
        <td>Срок кредита</td>
        <td>
            <asp:Label ID="lblRequestPeriod2" runat="server" Text=""></asp:Label>
        </td>
        <td>мес.</td>
    </tr>
    <tr>
        <td>Сумма кредита </td>
        <td>
            <asp:Label ID="lblRequestSumm2" runat="server" Text=""></asp:Label>
        </td>
        <td>сом</td>
    </tr>
    <tr>
        <td>Годовая процентная ставка</td>
        <td>
            <asp:Label ID="lblRate" runat="server" Text="Label"></asp:Label></td>
        <td>%</td>
    </tr>
    <tr>
        <td>Ежемесячный платёж</td>
        <td>
            <asp:Label ID="lblK2" runat="server" Text=""></asp:Label>
        </td>
        <td>сом</td>
    </tr>
</table>
<div class="centre">
    <br />
    <asp:Label ID="lblAct" runat="server" Text="Акт оценки залогового имущества"></asp:Label></div>
<asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="line">
        <Columns>
            <asp:BoundField DataField="ProductMark" HeaderText="Марка" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductSerial" HeaderText="Серийный №" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductImei" HeaderText="imei-код" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Price" HeaderText="Цена" ItemStyle-CssClass="width165" />
        </Columns>
</asp:GridView>
    <table class="line">
        <tr>
            <td class="width165">Итого:</td>
            <td class="width165"></td>
            <td class="width165"></td>
            <td class="width165"><asp:Label ID="lblTotalSum" runat="server"></asp:Label></td>
        </tr>
    </table>



        <br />
        Ф.И.О. кредитного сотрудника:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Подпись:										
        <br />
        На основе проведённого анализа и изложенной информации предлагаю:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Утвердить&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Отказать<br />
        <br />
        <asp:Label ID="lblReqDecision" runat="server" Text="РЕШЕНИЕ КРЕДИТНОГО КОМИТЕТА _______________"></asp:Label>№_____&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; от _____/__________2022 г.<br />
Утвердить выдачу кредита без изменения условий:														
        <br />
        <br />
<table class="line">
    <tr>
        <td>ФИО:</td>
        <td><asp:Label ID="lblCustomerFIO2" runat="server"></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Вид кредита:</td>
        <td>Потребительский кредит &nbsp;<asp:Label ID="lblNameOfCredit" runat="server" Text="КапиталБанк"></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Сумма кредита</td>
        <td>
            <asp:Label ID="lblRequestSumm" runat="server"></asp:Label>
        </td>
        <td>сом</td>
    </tr>
    <tr>
        <td>Срок кредита:</td>
        <td>
            <asp:Label ID="lblRequestPeriod" runat="server"></asp:Label>
        </td>
        <td>мес.</td>
    </tr>
    <tr>
        <td>Годовая процентная ставка:</td>
        <td><asp:Label ID="lblYearPercent" runat="server"></asp:Label></td>
        <td>%</td>
    </tr>
        <tr>
        <td>Порядок оплаты процентов:</td>
        <td>Ежемесячно</td>
        <td>сом</td>
    </tr>
    <tr>
        <td>Порядок основного долга:</td>
        <td>Ежемесячно аннуитетными платежами</td>
        <td>сом</td>
    </tr>
    <tr>
        <td>Комиссия за выдачу кредита:</td>
        <td>
            <asp:Label ID="lblComission" runat="server" Text=""></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblCommisionType" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
</table>
								
														
        <br />
        Отказать в выдаче кредита по следующим причинам:<br />
    <br />
														
<table class="line">
    <tr>
        <td>Кредитный комитет</td>
        <td>Ф.И.О.</td>
        <td>За</td>
        <td>Против</td>
        <td>Примечание</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDecision1" runat="server" Text="Председатель Кредитного комитета"></asp:Label>
        </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDecision2" runat="server" Text="Член Кредитного комитета"></asp:Label>
        </td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDecision3" runat="server" Text="Член Кредитного комитета"></asp:Label>
        </td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>														
        <br />
        <br />
        <asp:Label ID="lblDecision4" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Секретарь КК&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ФИО"></asp:Label>
        <br />
        <br />        														

    </div>
</div>
<%--</asp:Panel>--%>    </form>
</body>
</html>
