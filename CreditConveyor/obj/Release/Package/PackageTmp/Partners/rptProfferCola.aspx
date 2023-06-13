<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptProfferCola.aspx.cs" Inherits="СreditСonveyor.Partners.rptProfferCola" %>

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
        body{font-size:12px;}
        .centre{text-align:center;}
        .left{text-align:left;}
        .right{text-align:right;}
/**rptReceptionAct**/        
        .width100{width:100%;}
        table {border-collapse:collapse;font-size:12px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}  
    </style>
<div style = "width:720px;text-align:justify;">
    <div style = "text-align:right;">
        Потребительский кредит "Деньги в оборот" <br />

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
        <td>Наименование магазина:</td>
        <td><asp:Label ID="lblShop" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Сумма запрашиваемого кредита:</td>
        <td><asp:Label ID="lblSummCredit" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>Категория клиента:</td>
        <td><asp:Label ID="lblCategory" runat="server"></asp:Label></td>
    </tr>

</table>    
    <br />
    <table class="line">
    <tr>
        <td rowspan="2">Данные по закупу у партнера в рамках программы</td>
        <td>Мин.сумма закупа</td>
        <td>Макс.сумма закупа</td>
        <td>Средняя сумма разового закупа</td>
        <td>Сумма закупа в месяц по всей категории</td>
    </tr>
    <tr>
        
        <td><asp:Label ID="lblMinBuy" runat="server" Text="Label"></asp:Label></td>
        <td><asp:Label ID="lblMaxBuy" runat="server" Text="Label"></asp:Label></td>
        <td><asp:Label ID="lblAverageBuy" runat="server" Text="Label"></asp:Label></td>
        <td><asp:Label ID="lblMonthBuy" runat="server" Text="Label"></asp:Label></td>
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
        <td><asp:Label ID="lblCostPrice" runat="server" Text="25%"></asp:Label></td>
        <td></td>
    </tr>
     <tr>
        <td>Средняя с/с </td>
        <td><asp:Label ID="lblAverageCost" runat="server" Text=""></asp:Label></td>
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
        <td>Прибыль по бизнесу:</td>
        <td><asp:Label ID="lblBusinessProfit" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Дополнительные доходы (з/п, пенсия)</td>
        <td><asp:Label ID="lblAdditionalIncomes" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
     <tr>
        <td>Итого доходов</td>
        <td><asp:Label ID="lblTotalIncome" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>ЧИСТАЯ ПРИБЫЛЬ</td>
        <td><asp:Label ID="lblChp" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Взнос банку ОАО &quot;Дос-Кредобанк&quot;</td>
        <td><asp:Label ID="lblK" runat="server" Text=""></asp:Label>
        </td>
        <td>
            
        </td>
    </tr>
    <tr>
        <td>Взносы по другим кредитам:</td>
        <td><asp:Label ID="lblOtherLoans" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblOtherLoansComment" runat="server" Text=""></asp:Label></td>
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

</table>

    С учетом детализации данных, предоставленных и утвержденных в рамках программы "Деньги в оборот":	<br />						
Для категории <asp:Label ID="lblCategory2" runat="server" Text=""></asp:Label>&nbsp- ОПиУ <br />							
				



    <br />



Данные о кредитной линии:
<table class="line">
    <tr>
        <td>Срок кредита</td>
        <td>
            <asp:Label ID="lblRequestPeriod2" runat="server" Text=""></asp:Label>
        </td>
        <td>мес.</td>
    </tr>
    <tr>
        <td>Максимальная сумма кредита</td>
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
</table>
    <div class="centre">
        <br />
    Заключение:</div>
<div class="left">
    
    
    



        <br />
        Ф.И.О. кредитного сотрудника:&nbsp; <asp:Label ID="lblManagerFIO" runat="server" Text=""></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Подпись:										
        <br />
        <br />
        На основе проведённого анализа и изложенной информации предлагаю:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span> Утвердить&nbsp;&nbsp;&nbsp;&nbsp;<span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span> Отказать<br />
        <br />
        <asp:Label ID="lblReqDecision" runat="server" Text="РЕШЕНИЕ КРЕДИТНОГО КОМИТЕТА _______________"></asp:Label>№_____&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; от _____/__________2022 г.<br />
Утвердить выдачу кредита на следующих условиях:														
        <br />
        <br />
<table class="line">
   <tr>
        <td>ФИО:</td>
        <td><asp:Label ID="lblCustomerFIO2" runat="server"></asp:Label></td>
        <td></td>
    </tr>
     <tr>
        <td>Цель кредитной линии:</td>
        <td>Закуп товара</td>
        <td></td>
    </tr>
    <tr>
        <td>Вид кредита:</td>
        <td>Потребительский кредит &quot;Деньги в оборот&quot;</td>
        <td></td>
    </tr>
    <tr>
        <td>Сумма кредита:</td>
        <td><asp:Label ID="lblRequestSumm" runat="server"></asp:Label></td>
        <td>сом</td>
    </tr>
    <tr>
        <td>Срок кредита:</td>
        <td><asp:Label ID="lblRequestPeriod" runat="server"></asp:Label></td>
        <td>мес.</td>
    </tr>
    <tr>
        <td>Годовая процентная ставка:</td>
        <td>
            <asp:Label ID="lblYearPercent" runat="server"></asp:Label>
        </td>
        <td>%</td>
    </tr>
    <tr>
        <td>Порядок оплаты процентов:</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>Порядок основного долга:</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
   
</table>
														
        <p class ="left">Отказать в выдаче кредита по следующим причинам:
          ____________________________________________________________________________________________________<br />
    ____________________________________________________________________________________________________<br />
      </p>
    
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
        <asp:Label ID="lblDecision4" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Секретарь КК&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ФИО"></asp:Label>
        <br />
        														

									

    </div>
</div>
<%--</asp:Panel>--%>
    </form>
</body>
</html>
