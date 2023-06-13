<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptProfferAgro.aspx.cs" Inherits="СreditСonveyor.Partners.rptProfferAgro" %>

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
<div class="centre">(источник погашения - агро)<br />
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
        <td><label>Выручка от продажи скота в мес:</label></td>
        <td><asp:Label ID="lblRevenueAgro" runat="server" Text=""></asp:Label></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><label>Выручка от продажи молока в мес:</label></td>
        <td><asp:Label ID="lblRevenueMilk" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td><label>Выручка от продажи молочной продукции в мес:</label></td>
        <td><asp:Label ID="lblRevenueMilkProd" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>

    <tr>
        <td><label>Расходы на содержание скота в мес:</label></td>
        <td><asp:Label ID="lblOverheadAgro" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
        <tr>
        <td><label>Дополнительные расходы в мес</label></td>
        <td><asp:Label ID="lblAddOverheadAgro" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Валовая прибыль</td>
        <td><asp:Label ID="lblValP" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Семейные расходы</td>
        <td><asp:Label ID="lblFamilyExpenses" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td>Дополнительные доходы</td>
        <td><asp:Label ID="lblAdditionalIncomes" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td><b>ЧИСТАЯ ПРИБЫЛЬ</b></td>
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
        <td><b>ЧИСТЫЙ ОСТАТОК ДЕНЕГ</b></td>
        <td><asp:Label ID="lblCho" runat="server" Text=""></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td><b>Чистый остаток в % соотношении</b></td>
        <td><asp:Label ID="lblY1" runat="server" Text=""></asp:Label></td>
        <td><asp:Label ID="lblIssuanceOfCredit" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td><b>Соотношение ежемесячных платежей по кредиту на совокупный доход</b></td>
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
    <br />

     <asp:CheckBoxList ID="chkbxTypeOfCollateral" runat="server"  RepeatDirection="Horizontal" Enabled ="false">

                    <asp:ListItem Value="0">Без обеспечения</asp:ListItem>
                    <asp:ListItem Value="1">Поручительство</asp:ListItem>
                    <asp:ListItem Value="2">Движимое имущество</asp:ListItem>

                </asp:CheckBoxList>
    <br />

    <asp:Panel ID="pnlGuarantor" runat="server" Visible="false">
                <table class="line">
                <tr>
					<td><label>Поручитель</label></td>
                    <td></td>
                    
				</tr>
				<tr>
					<td><label>ФИО:</label></td>
                    <td><asp:Label ID="lblGuarantorFIO" runat="server" Text=""></asp:Label></td>
                    
				</tr>
				<tr>
					<td><label>ИНН:</label></td>
                    <td><asp:Label ID="lblGuarantorINN" runat="server" Text=""></asp:Label></td>
				</tr>
                  <%--<tr>
                    <td>Адрес прописки</td>
                    <td><asp:Label ID="lblGurantorRegAddress" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Адрес проживания</td>
                    <td><asp:Label ID="lblGurantorResAddress" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Телефоны:</td>
                    <td><asp:Label ID="lblGurantorPhone" runat="server"></asp:Label></td>
                </tr>--%>
			</table>

            </asp:Panel>
            
            <br /><br />

            <asp:Panel ID="pnlGuarantees" runat="server" Visible="false">
                <table class="line">
                <tr>
					<td><label>Залогодатель</label></td>
                    <td></td>
				</tr>
				<tr>
					<td><label>Фамилия:</label></td>
                    <td><asp:Label ID="lblPledgerFIO" runat="server" Text=""></asp:Label></td>
				</tr>
				<tr>
					<td><label>ИНН:</label></td>
                    <td><asp:Label ID="lblPledgerINN" runat="server" Text=""></asp:Label></td>
				</tr>
			</table>
                
    
                <br />
                
                 Акт оценки залогового имущества</div>


<asp:GridView ID="gvGuarantees" runat="server" AutoGenerateColumns="false"  ShowHeader="true" ShowFooter = "true"  DataKeyNames="ID" Width="584px" >
										<Columns>
            <asp:BoundField DataField="Name" HeaderText="Наименование имущества" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Count" HeaderText="Количество" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Description" HeaderText="Описание залогового имущества" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Address" HeaderText="Место нахождения имущества" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="MarketPrice" HeaderText="Рыночная стоимость (сом)" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="AssessedPrice" HeaderText="Залоговая стоимость (сом)" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Coefficient" HeaderText="Коэффициент ликвидности" ItemStyle-CssClass="width165" />
										</Columns>
									</asp:GridView>
  

      </asp:Panel> 

    <br />
    <br />
    Акт оценки залогового имущества</div>




        
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
        <td>Потребительский кредит "КапиталБанк"</td>
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
        <td>Ежемесячно</td>
        <td>сом</td>
    </tr>
    <tr>
        <td>Порядок основного долга:</td>
        <td>Ежемесячно аннуитетными платежами</td>
        <td>сом</td>
    </tr>


<%--    <tr>
        <td>Единовременная комиссия за выдачу кредита:</td>
        <td>
            <asp:Label ID="lblComission" runat="server" Text=""></asp:Label>
        </td>
        <td>сом</td>
    </tr>--%>
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
        <asp:Label ID="lblDecision4" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Секретарь КК&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ФИО"></asp:Label>
        <br />
				<br />					

    </div>
</div>
    </form>
</body>
</html>
