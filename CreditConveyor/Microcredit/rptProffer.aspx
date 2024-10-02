<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptProffer.aspx.cs" Inherits="СreditСonveyor.Microcredit.rptProffer" %>

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
body {font-size:12px;}
        .width100{width:100%;}
        table {border-collapse:collapse;font-size:12px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}  
        .auto-style1 {
            height: 16px;
        }
        </style>
<div style = "width:720px;text-align:justify;">
<div style = "text-align:right;">
    &nbsp;<asp:Label ID="lblBranch" runat="server" Text="Филиал"></asp:Label>,
    Офис - <asp:Label ID="lblOffice" runat="server" Text="Офис"></asp:Label>,
    № - <asp:Label ID="lblRequestN" runat="server" Text="№"></asp:Label>


</div>

<div class="centre"><b>Скоринговое заключение</b></div>
<div class="centre">(источник погашения - заработная плата)<br />
        </div>
<div class="centre">Информация о клиенте:</div>
    
  <table class="line">
    <tr>
        <td>Ф.И.О.</td>
        <td><asp:Label ID="lblCustomerFIO" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>ИНН</td>
        <td><asp:Label ID="lblINN" runat="server" Text=""></asp:Label></td>
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
        <td>Продавец:</td>
        <td><asp:Label ID="lblSeller" runat="server"></asp:Label></td>
    </tr>
</table> 
 
    <br />

<div class="centre">Информация о кредите:</div>
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
    <tr>
        <td>Цель кредита</td>
        <td>
            <asp:Label ID="lblPurpose" runat="server" Text=""></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
    <td>Дополнительные условия</td>
    <td>
        <asp:Label ID="lblAdditionalConditions" runat="server" Text=""></asp:Label>
    </td>
    <td></td>
</tr>
</table>
<div class="centre">
    <br />
    <asp:CheckBoxList ID="chkbxTypeOfCollateral" runat="server"  RepeatDirection="Horizontal" Enabled ="false">

                    <asp:ListItem Value="0">Без обеспечения</asp:ListItem>
                    <asp:ListItem Value="1">Поручительство</asp:ListItem>
                    <asp:ListItem Value="2">Движимое имущество</asp:ListItem>

                </asp:CheckBoxList>
    <br />
    
    <asp:Panel ID="pnlGuarantor" runat="server" Visible="true">
        <div class="centre">Поручительство</div>
                <table class="line">
                <tr>
					<td><label>ФИО</label></td>
                    <td><label>ИНН:</label></td>
                    <td><label>Место работы:</label></td>
                    <td><label>Телефон:</label></td>
				</tr>
				<tr>
		            <td><asp:Label ID="lblGuarantorFIO" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblGuarantorINN" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblWork" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblPhone" runat="server" Text=""></asp:Label></td>
                    
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
                
                 Акт оценки залогового имущества


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
     </div>

    <br />




<div class="centre">Финансовые данные</div>
<table class="line">
  
    <tr>
        <td>Среднемес.зар. плата клиента:</td>
        <td><asp:Label ID="lblAverageMonthSalary" runat="server" Text=""></asp:Label></td>
       
    </tr>
    <tr>
        <td>Доп.доход:</td>
        <td><asp:Label ID="lblAdditionalIncom" runat="server" Text=""></asp:Label></td>
       
    </tr>
    <tr>
        <td>ИТОГО ДОХОДОВ</td>
        <td><asp:Label ID="lblAverageMonthSalary2" runat="server" Text=""></asp:Label></td>
       
    </tr>
    <tr>
        <td>Семейные расходы</td>
        <td><asp:Label ID="lblFamilyExpenses" runat="server" Text=""></asp:Label></td>
        
    </tr>
    <tr>
        <td>ИТОГО РАСХОДОВ </td>
        <td><asp:Label ID="lblTotalСonsumption" runat="server" Text=""></asp:Label></td>
        
    </tr>
    <tr>
        <td>ЧИСТАЯ ПРИБЫЛЬ</td>
        <td>
            <asp:Label ID="lblChp" runat="server" Text=""></asp:Label>
        </td>
        
    </tr>
    <tr>
        <td>Взнос банку ОАО &quot;КапиталБанк&quot;</td>
        <td>
            <asp:Label ID="lblK" runat="server" Text=""></asp:Label>
        </td>
       
    </tr>
    <tr>
        <td>Взносы по другим кредитам:</td>
        <td>
            <asp:Label ID="lblOtherLoans" runat="server" Text=""></asp:Label>
        </td>
       
    </tr>
    <tr>
        <td>ЧИСТЫЙ ОСТАТОК ДЕНЕГ</td>
        <td>
            <asp:Label ID="lblCho" runat="server" Text=""></asp:Label>
        </td>
       
    </tr>
    <tr>
        <td>Чистый остаток в %&nbsp; соотношении</td>
        <td>
            <asp:Label ID="lblY1" runat="server" Text=""></asp:Label>
        </td>
       
    </tr>
    <tr>
        <td>Соотношение ежемесячных платежей по кредиту на совокупный доход</td>
        <td>
            <asp:Label ID="lblY2" runat="server" Text=""></asp:Label>
        </td>
        
    </tr>
</table>
    <br />

<div class="centre">Кредитная история</div>
<table class="line">
 <tr>
    <td>Общее сальдо текущих кредитов</td>
    <td>
        <asp:Label ID="lblTotBalCurrLoans" runat="server" Text=""></asp:Label>
    </td>
 </tr>
 <tr>
    <td>Общее количество текущих кредитов</td>
    <td>
        <asp:Label ID="lblTotCountCurrLoans" runat="server" Text=""></asp:Label>
    </td>
 </tr>
 <tr>
    <td>Общее количество просрочек</td>
    <td>
        <asp:Label ID="lblTotCountDelays" runat="server" Text=""></asp:Label>
    </td>
 </tr>
 <tr>
    <td>Максимальное количество дней в проссрочке</td>
    <td>
        <asp:Label ID="lblMaxDayDelays" runat="server" Text=""></asp:Label>
    </td>
 </tr>
</table>
    <br />

<div class="centre">Заключение</div>
<div class="centre">Кредитный рейтинг</div>
<table class="line">
 <tr>
    <td>A</td>
    <td>Количество баллов по коэффициенту покрытия взноса</td>
    <td>
        <asp:Label ID="lblA" runat="server" Text=""></asp:Label>
    </td>
 </tr>
 <tr>
    <td>B</td>
    <td>Количество баллов по соотношению планируемых ежемесячных платежей по кредиту</td>
    <td>
        <asp:Label ID="lblB" runat="server" Text=""></asp:Label>
    </td>
 </tr>
 <tr>
    <td>C</td>
    <td>Количество баллов по количеству текущих параллельных кредитов</td>
    <td>
        <asp:Label ID="lblC" runat="server" Text=""></asp:Label>
    </td>
 </tr>
 <tr>
    <td>D</td>
    <td>Количество баллов по Кредитной истории</td>
    <td>
        <asp:Label ID="lblD" runat="server" Text=""></asp:Label>
    </td>
 </tr>
 <tr>
    <td></td>
    <td>Итого баллов по кредитному рейтингу</td>
    <td>
        <asp:Label ID="lblABCD" runat="server" Text=""></asp:Label>
    </td>
 </tr>
 <tr>
    <td></td>
    <td>Решение</td>
    <td>
        <asp:Label ID="lblSolution" runat="server" Text=""></asp:Label>
    </td>
 </tr>
</table>

    <br />

<table>
    <tr>
        <td>Специалист Отдела Кредитования:</td>
        <td></td>
        <td><asp:Label ID="lblSpecialist" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>Начальник Отдела Кредитования/Зав Сектором кредитования</td>
        <td></td>
        <td><asp:Label ID="lblBoss" runat="server" Text=""></asp:Label></td>
    </tr>
</table>   



        														

    </div>
</div>

    </form>
</body>
</html>
