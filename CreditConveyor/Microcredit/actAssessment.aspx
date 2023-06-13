<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="actAssessment.aspx.cs" Inherits="СreditСonveyor.Microcredit.actAssessment" %>

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
        </style>
<div style = "width:720px;text-align:justify;">


<div class="centre">Акт оценки залогового имущества</div>
    



    <div>
            
            <br /><br />

            <asp:Panel ID="pnlGuarantees" runat="server" Visible="true">
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


                

                <br />
                Итого залоговая стоимость - прописью<br />
                <br />
                Метод оценки залога: Сравнительные продажи<br />
                <br />
                Аналоги взяты из: (Сайт, СМИ, газеты)<br />
                <br />
                <br />
                <br />
  

      </asp:Panel> 

    <br />
   



            Акт составил: <br />
        Ф.И.О. кредитного сотрудника:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Подпись:										
        <br />
        <br />
        <br />
        <br />
        <br />
        														

    </div>
</div>

        </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
