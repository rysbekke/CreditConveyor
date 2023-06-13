<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptForPeriodWithProducts.aspx.cs" Inherits="СreditСonveyor.Partners.rptForPeriodWithProducts" %>

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

      <br />
      <input name="b_print" type="button" class="ipt" onClick="printdiv('proffer');" value=" Печать ">
      <asp:Button ID="btnExpToExcel" runat="server" Text="Export" OnClick="btnExpToExcel_Click"  />
<br />
<br />
<br />
<div id="proffer">
    <style>
        .centre{text-align:center;}
     
        .width100{width:100%;}
        table {border-collapse:collapse;font-size:14px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}  
    </style>
<div style = "width:720px;text-align:justify;">
<div class="centre"><b>Отчет</b></div>
<div class="centre">(c <asp:Label ID="lbldt1" runat="server" Text=""></asp:Label> &nbsp;по&nbsp; <asp:Label ID="lbldt2" runat="server" Text=""></asp:Label>) <br />
        </div>
        <br />

        <asp:GridView ID="gvForPeriod" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvForPeriod_RowDataBound" ShowFooter="True" EnableEventValidation = "false" >
            <Columns>
                            <asp:BoundField DataField="RequestID" HeaderText="Номер" />
                            <%--<asp:BoundField DataField="ShortName" HeaderText="Филиал" />--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <b>Филиал</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("ShortName") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b><asp:Label ID="lblShortName" runat="server" Text=""></asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField DataField="Surname" HeaderText="Фамилия" />--%>
                           <asp:TemplateField>
                                <HeaderTemplate>
                                    <b>Фамилия</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("Surname") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b><asp:Label ID="lblSurname" runat="server" Text=""></asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CustomerName" HeaderText="Имя" />
                            <asp:BoundField DataField="IdentificationNumber" HeaderText="ИНН" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <b>Договор№</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("AgreementNo") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b><asp:Label ID="lblAgreementNo" runat="server" Text=""></asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="AgreementNo" HeaderText="№ договора" />--%>
                           <%-- <asp:BoundField DataField="ProductPrice" HeaderText="Цена"  />
                            <asp:BoundField DataField="AmountDownPayment" HeaderText="Взнос"  />
                            <asp:BoundField DataField="RequestSumm" HeaderText="Кредит"  />--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <b>Стоимость</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("ProductPrice") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b><asp:Label ID="lblProductPrice" runat="server" Text=""></asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <b>Первонач.взнос</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("AmountDownPayment") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b><asp:Label ID="lblAmountDownPayment" runat="server" Text=""></asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <b>Сумма кредита</b>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("RequestSumm") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <b><asp:Label ID="lblRequestSumm" runat="server" Text=""></asp:Label></b>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RequestDate" HeaderText="Дата заявки" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="RequestStatus" HeaderText="Статус заявки" />
                  
             </Columns>
             
        </asp:GridView>
        <asp:GridView ID="gvForPeriodWithHistory" runat="server"></asp:GridView>

        <br />
    </div>
</div>

    </form>
</body>
</html>
