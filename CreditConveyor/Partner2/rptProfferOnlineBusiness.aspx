<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptProfferOnlineBusiness.aspx.cs" Inherits="СreditСonveyor.Partners2.rptProfferOnlineBusiness" %>

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
        body {font-size:12px;}
        table {border-collapse:collapse;font-size:12px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}  
    </style>
<div style = "width:720px;text-align:justify;">
    <div style = "text-align:right;">
    <asp:Label ID="lblBranch" runat="server" Text="Филиал"></asp:Label>,
    Офис - <asp:Label ID="lblOffice" runat="server" Text="Офис"></asp:Label>,
    № - <asp:Label ID="lblRequestN" runat="server" Text="№"></asp:Label>
</div>
<div class="centre"><b>Предложение на Кредитный комитет</b></div>
<div class="centre">(источник погашения - бизнес)<br />
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
        <td>Комментарии по бизнесу:</td>
        <td><asp:Label ID="lblBusinessComment" runat="server"></asp:Label></td>
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
        <td>Средняя выруча в месяц:</td>
        <td><asp:Label ID="lblRevenueMonth" runat="server" Text=""></asp:Label></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Средняя наценка в %</td>
        <td><asp:Label ID="lblCostPrice" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Валовая прибыль</td>
        <td><asp:Label ID="lblValP" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Накладные расходы</td>
        <td><asp:Label ID="lblOverHead" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Семейные расходы</td>
        <td><asp:Label ID="lblFamilyExpenses" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>ИТОГО РАСХОДОВ</td>
        <td><asp:Label ID="lblTotalСonsumption" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Дополнительные доходы</td>
        <td><asp:Label ID="lblAdditionalIncomes" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>ЧИСТАЯ ПРИБЫЛЬ</td>
        <td><asp:Label ID="lblChp" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Взнос банку ОАО &quot;КапиталБанк&quot;</td>
        <td><asp:Label ID="lblK" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>Взносы по другим кредитам:</td>
        <td><asp:Label ID="lblOtherLoans" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>ЧИСТЫЙ ОСТАТОК ДЕНЕГ</td>
        <td><asp:Label ID="lblCho" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Чистый остаток в %&nbsp; соотношении</td>
        <td><asp:Label ID="lblY1" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblIssuanceOfCredit" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Соотношение ежемесячных платежей по кредиту на совокупный доход</td>
        <td><asp:Label ID="lblY2" runat="server" Text=""></asp:Label></td>
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
    Акт оценки залогового имущества</div>
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

  Верификацию провел 
    <br />
    <asp:Label ID="lblVerificator" runat="server" text="ФИО"></asp:Label> :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;Подпись:										
        <br />
        <br />
        <asp:Label ID="lblReqDecision" runat="server" Text="РЕШЕНИЕ Скоринг-программы "></asp:Label>№ <asp:Label ID="lblNJournal" runat="server" text="__"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; от <asp:Label ID="lblDateJournal" runat="server" text="_______"></asp:Label> г.<br />
													
        <br />
<table class="line">
    <tr>
        <td>ФИО:</td>
        <td><asp:Label ID="lblCustomerFIO2" runat="server"></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Вид кредита:</td>
        <td>Потребительский кредит "Онлайн"</td>
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
        <td>Счет для зачисления:</td>
        <td><asp:Label ID="lblCardAccount" runat="server"></asp:Label></td>
        <td></td>
    </tr>


</table>
								
														
        <br />
        
<table class="line">
    <tr>
        <td>Решение</td>
        <td>Пользователь</td>
        <td>Статус решения</td>
        <td>Кредитный рейтинг</td>
        <td>Примечание</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDecision1" runat="server" Text="Скоринг программа"></asp:Label>
        </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SystemUser&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            &nbsp; </td>
       <td>
           <asp:Label ID="lblCreditRating" runat="server" Text=""></asp:Label></td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
   
</table>														
        <br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />

      											
        <br />
        <br />
        
        <br />
        <br />
        														

									

    </div>
</div>
<%--</asp:Panel>--%>
    </form>
</body>
</html>
