<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptConsentSozfond.aspx.cs" Inherits="СreditСonveyor.Partners.rptConsentSozfond" %>

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
<div class="centre"><b>ТИПОВАЯ ФОРМА<br />
согласия субъекта персональных данных на сбор и обработку его персональных данных
</b></div>

    <br />
    Я,&nbsp;
    <asp:Label ID="lblCustomerFIO" runat="server" Text="lblCustomerFIO"></asp:Label>
<br />ПИН, присвоенный в Кыргызской Республике:
    <asp:Label ID="lblINN" runat="server" Text="lblINN"></asp:Label>
<br />Документ, удостоверяющий личность: Паспорт&nbsp; серия 
    <asp:Label ID="lblDocumentSeries" runat="server" Text="lblDocumentSeries"></asp:Label>
&nbsp;№
    <asp:Label ID="lblDocumentNo" runat="server" Text="lblDocumentNo"></asp:Label>
<br />                                                                   выдан: 
    <asp:Label ID="lblIssueAuthority" runat="server" Text="lblIssueAuthority"></asp:Label>
    ,
    <asp:Label ID="lblIssueDate" runat="server" Text="lblIssueDate"></asp:Label>
<br />                             
    <br />
    Адрес фактического проживания:
    <asp:Label ID="lblRegCustomerAddress" runat="server" Text="lblRegCustomerAddress"></asp:Label>
&nbsp;<br />Адрес места прописки:<asp:Label ID="lblResCustomerAddress" runat="server" Text="lblResCustomerAddress"></asp:Label>
<br />
    <br />
    Контактный телефон: 
    <asp:Label ID="lblContactPhone" runat="server" Text="lblContactPhone"></asp:Label>
    , эл.почта __________________________________
<br />даю согласие ОАО «Дос-Кредобанк»
<br /><span style="color: rgb(34, 34, 34); font-family: arial, sans-serif; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">☑</span> на обработку моих персональных данных (сбор, запись, хранение, актуализация (обновление, изменение), группировка персональных данных);
<br /><span style="color: rgb(34, 34, 34); font-family: arial, sans-serif; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">☑</span> на передачу моих персональных данных третьим лицам в соответствии с Законом Кыргызской Республики «Об информации персонального характера» и иными нормативными правовыми актами в сфере информации персонального характера;
<br /><span style="color: rgb(34, 34, 34); font-family: arial, sans-serif; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">☑</span> на передачу моих персональных данных кредитным бюро в соответствии с Законом Кыргызской Республики «Об обмене кредитной информацией» для последующей обработки (сбора, записи, хранения, актуализации (обновления, изменения), группировки персональных данных).
<br />в соответствии со следующим перечнем персональных данных и сведениями об их изменении: тип национального паспорта, ПИН, ФИО, дата рождения, номер документа, наименование органа, выдавшего документ и его код, дата выдачи, срок действия, пол, цифровое изображение лица, место жительства, семейное положение.
<br />&nbsp;&nbsp;&nbsp; Сбор, обработка персональных данных осуществляется исключительно в целях проверки данных для получения информации, которая может оказать влияние на принятие решения относительно предоставления или не предоставления кредита Клиенту.
<br />Настоящее согласие дается до истечения сроков хранения персональных данных или документов, содержащих, вышеуказанные сведения, определяемых в соответствии с законодательством Кыргызской Республики.
<br />&nbsp;&nbsp;&nbsp; Согласие на обработку персональных данных может быть отозвано субъектом на основании письменного
заявления в произвольной форме. В случае отзыва настоящего согласия, обработка персональных данных полностью или частично может быть продолжена в соответствии со статьями 5 и 15 Закона Кыргызской Республики «Об информации персонального характера».
<br />&nbsp;&nbsp;&nbsp;&nbsp;Субъект по письменному запросу имеет право на получение информации, касающейся обработки его персональных данных (в соответствии со ст.10 Закона Кыргызской Республики «Об информации персонального характера»).
<br />&nbsp;&nbsp;&nbsp; Я подтверждаю, что ознакомлен (а) с положениями Закона Кыргызской Республики «Об информации персонального характера», Порядком получения согласия субъекта персональных данных на сбор и обработку его персональных данных, порядком и формой уведомления субъектов персональных данных о передаче их персональных данных третьей стороне, утвержденным постановлением Правительства Кыргызской Республики от 21 ноября 2017 года № 759. 
<br />&nbsp;&nbsp;&nbsp; Права и обязанности в области защиты персональных данных мне разъяснены.
<br />
    <br />
<br />Дата: <asp:Label ID="lblDate" runat="server" Text="lblDate"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                         Подпись&nbsp; ___________&nbsp;
    <asp:Label ID="lblCustomerFIO2" runat="server" Text="lblCustomerFIO2"></asp:Label>
&nbsp;<br />
    <br />
    <br />
    <br />
    <br />
    <br />





       														

    </div>
</div>

    </form>
</body>
</html>
