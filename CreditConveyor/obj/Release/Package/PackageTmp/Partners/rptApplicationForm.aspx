<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptApplicationForm.aspx.cs" Inherits="СreditСonveyor.Partners.rptApplicationForm" %>

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
        table {border-collapse:collapse;font-size:12px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 5px;}
        table.line tr th {border:1px solid #000000;padding:0 5px;}
        table.line {border:1px solid #000000;padding:0 5px;}
        .width165{width:165px;}  
      body{font-size:12px;}
        .auto-style1 {
            height: 8px;
        }
        .auto-style2 {
            height: 16px;
        }
        </style>
<div style = "width:720px;text-align:justify;">

<div class="centre"><b>ОАО "Дос-Кредобанк"</b></div>
    <div class="centre"><b>ЗАЯВЛЕНИЕ-АНКЕТА НА ПОЛУЧЕНИЕ БИЗНЕС-КРЕДИТА "ДЕНЬГИ В ОБОРОТ" ДЛЯ ФИЗИЧЕСКИХ ЛИЦ (ИП)</b></div>
    <table class="line">
     <tr>
        <td>1.</td>
        <td colspan="6">Личные данные</td>
    </tr>
    <tr>
        <td></td>
        <td>Ф.И.О.</td>
        <td colspan="5"><asp:Label ID="lblCustomerFIO" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td>Возраст</td>
        <td></td>
        <td>год/лет</td>
        <td>Пол</td>
        <td><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>МУЖ</td>
        <td><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>ЖЕН</td>
    </tr>
    <tr>
        <td></td>
        <td>Адрес прописки</td>
        <td colspan="5"><asp:Label ID="lblCustomerRegAddress" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td>Адрес проживания</td>
        <td colspan="5"><asp:Label ID="lblCustomerResAddress" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td>Семейное положение</td>
        <td>ЖЕНАТ</td>
        <td><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span>ХОЛОСТ</td>
        <td><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span>ЗАМУЖЕМ</td>
        <td><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span>НЕЗАМУЖЕМ</td>
        <td><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span>РАЗВЕДЕН(А)</td>
    </tr>
    <tr>
        <td></td>
        <td>Телефоны:</td>
        <td colspan="6"><asp:Label ID="lblContactPhone1" runat="server"></asp:Label></td>
    </tr>
    </table>        										
    <table class="line">
     <tr>
        <td>2.</td>
        <td colspan="4">Данные о ваших доходах</td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">Данные о бизнесе/вид деятельности</td>
    </tr>
    <tr>
        <td class="auto-style2"></td>
        <td class="auto-style2">Вид деятельности</td>
        <td class="auto-style2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
        <td class="auto-style2">Минимальная выручка в день (сом)</td>
        <td><asp:Label ID="lblMinRevenue" runat="server" Text=""></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td>Вид ассортимента товара</td>
        <td></td>
        <td>Максимальная выручка в день (сом)</td>
        <td>
            <asp:Label ID="lblMaxRevenue" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td>Адрес бизнеса:</td>
        <td></td>
        <td>Количество рабочих дней в месяц:</td>
        <td>
            <asp:Label ID="lblCountWorkDay" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style2"></td>
        <td class="auto-style2">Опыт в данном бизнесе</td>
        <td class="auto-style2"></td>
        <td class="auto-style2">Средняя наценка в % (в торговле):</td>
        <td class="auto-style2">
            <asp:Label ID="lblСostPrice" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>Чистая прибыл в месяц (сом)
            <br />
            (примерно)</td>
        <td></td>
        <td>Расходы по бизнесу в мес(сом)</td>
        <td>
            <asp:Label ID="lblOverhead" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>Среднемесяный оборот (сом)</td>
        <td></td>
        <td>Расходы на семью в месяц (сом)</td>
        <td>
            <asp:Label ID="lblFamilyExpenses" runat="server"></asp:Label>
        </td>
    </tr>
    </table>       
    <table class="line">
    <tr>
        <td>3.</td>
        <td colspan="7">Сведения о кредите</td>
    </tr>
    <tr>
        <td></td>
        <td colspan="7">Прошу предоставить мне кредит на: (цель кредита)</td>
        
    </tr>
    <tr>
        <td></td>
        <td>Сумма кредитной линии</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;</td>
        <td>сом</td>
        <td>Срок крединой линии</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</td>
        <td>мес</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
    </tr>
    </table>        			
    <table class="line">
    <tr>
        <td class="auto-style1">4.</td>
        <td class="auto-style1">Есть активные кредиты</td>
        <td class="auto-style1"><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span></td>
        <td class="auto-style1">ДА</td>
        <td class="auto-style1"><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span></td>
        <td class="auto-style1">НЕТ</td>
        <td class="auto-style1">Взносы по активным кредитам</td>
        <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
        <td class="auto-style1">сом</td>
    </tr>
    <tr>
        <td></td>
        <td></td>
       
    </tr>
    </table>        			

     <table class="line">
    <tr>
        <td>5.</td>
        <td colspan="4">Укажите родстенников или знакомых, с которыми можно связаться в случае необходимости:</td>
    </tr>
    <tr>
        <td></td>
        <td>Ф.И.О.</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
        <td>Ф.И.О.</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
    </tr>
    <tr>
        <td></td>
        <td>Кем приходится</td>
        <td></td>
        <td>Кем приходится</td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>Адрес:</td>
        <td></td>
        <td>Адрес:</td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td>Контактные телефоны:</td>
        <td></td>
        <td>Контактные телефоны:</td>
        <td></td>
    </tr>
    </table>        			
    <table class="line">
    <tr>
        <td>6.</td>
        <td colspan="4">Данные о конечном выгодоприобретателе:</td>
    </tr>
    <tr>
        <td></td>
        <td>Подтверждаю, что (нужное отметить)</td>
    </tr>
    <tr>
        <td></td>
        <td><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>САМ ЯВЛЯЮСЬ ВЛАДЕЛЬЦЕМ ИЛИ КОНЕЧНЫМ ВЫГОДОПРИОБРЕТАТЕЛЕМ</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td><span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>КОНЕЧНЫМ ВЫГОДОПРИОБРЕТАТЕЛЕМ ЯВЛЯЕ(Ю)ТСЯ:</td>
       
    </tr>
    <tr>
        <td></td>
        <td>1. ФИО и паспортные данные:</td>
    </tr>
    <tr>
        <td></td>
        <td>Я, являюсь, иностранным политическим значимым лицом:    <span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>ДА     <span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">&nbsp;□ </span>НЕТ</td>
    </tr>
    <tr>
        <td></td>
        <td>Предпочтительный язык, на котором будет составлен договор (нужное отметить):   <span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>КЫРГЫЗСКИЙ&nbsp;&nbsp; <span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>РУССКИЙ</td>
    </tr>
    <tr>
        <td></td>
        <td>Препологаемое число, удобное для ежемесячного погашения кредита (например 10-е): _________</td>
    </tr>

    </table>


    <table class="line">
    <tr>
        <td>7.</td>
        <td>Информационная часть</td>
    </tr>
    <tr>
        <td></td>
        <td>Клиент  подтверждает, что вся вышеприведенная информация является подлинной, соответствует истинным фактам и выражает согласие на проведение дальнейшего  анализа своей деятельности, дает свое согласие на предоставление кредитного отчета и всю необходимую информацию о себе. Банк оставляет за собой право обращения к любому лицу, в/из Кредитное бюро, их правопреемникам, любому лицу, известному или неизвестному Клиенту , которое, как полагает БАНК, владеет информацией, которая может оказать влияние на принятие решения относительно предоставления или не предоставления кредита Клиенту.  ОАО «Дос-Кредобанк» гарантирует, что вся информация, предоставленная клиентом, будет использована строго конфиденциально и только для принятия решения по данной заявке. Заявитель согласен на получение рассылки материалов информационного и рекламного характера на все указанные в заявлении номера телефонов.</td>
    </tr>
    </table>
        Подпись заявителя ____________________________&nbsp;&nbsp;&nbsp; Дата заполнения анкеты: ___________________<br />
    Идентификация проведена&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _______________________________&nbsp;&nbsp;&nbsp; _______________<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (ФИО)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (подпись)<br />
     <table class="line">
    <tr>
        <td>ЗАПОЛНЯЕТСЯ БАНКОМ</td>
        <td colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td rowspan="2">Проверил по справочнику РЭГ</td>
        <td rowspan="2">______________</td>
        <td>Дата проверки по справочнику РЭГ</td>
        <td>Результат</td>
        <td>Подпись</td>
    </tr>
    <tr>
        
        
        <td></td>
        <td>&nbsp;</td>
        <td></td>
    </tr>
    <tr>
        <td rowspan="2">Проверил на аффилированность</td>
        <td rowspan="2">______________</td>
        <td>Дата проверки на аффилированность</td>
        <td>Результат</td>
        <td>Подпись</td>
    </tr>
    <tr>
        
        
        <td></td>
        <td>&nbsp;</td>
        <td></td>
    </tr>
     <tr>
        <td rowspan="2">Проверил по КИБ</td>
        <td rowspan="2">______________</td>
        <td>Дата проверки по КИБ</td>
        <td>Результат КИБ</td>
        <td>Подпись</td>
    </tr>
     <tr>
        
        
        <td></td>
        <td>&nbsp;</td>
        <td></td>
    </tr>
    </table>        			
        Степень (уровень) риска (нужное отметить):&nbsp;&nbsp; <span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>ВЫСОКИЙ&nbsp;&nbsp;&nbsp; <span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□ </span>СРЕДНИЙ&nbsp;&nbsp;&nbsp; <span style="font-size:11.0pt;line-height:107%;
font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;mso-fareast-font-family:
Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;
mso-bidi-theme-font:minor-latin;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">□</span>НИЗКИЙ<br />
    Обоснование оценки степени (уровня) Риска:<br />
     <table class="line">
    <tr>
        <td>Критерии</td>
        <td>Да</td>
        <td>Нет</td>
       
    </tr>
    <tr>
        <td>Страновой (географический) риск</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
      
    </tr>
    <tr>
        <td>Риск, связанный с клиентом</td>
        <td>&nbsp;</td>
        <td></td>
      
    </tr>
    <tr>
        <td>Риск, связанный с деятельностью или продукцией клиента</td>
        <td>&nbsp;</td>
        <td></td>
       
    </tr>
    </table>        
        Дата начала отношений с клиентом (открытия первого счета)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size:11.0pt;line-height:107%;
font-family:Wingdings;mso-ascii-font-family:Calibri;mso-ascii-theme-font:minor-latin;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-font-family:
Calibri;mso-hansi-theme-font:minor-latin;mso-bidi-font-family:&quot;Times New Roman&quot;;
mso-bidi-theme-font:minor-bidi;mso-ansi-language:RU;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA;mso-char-type:symbol;mso-symbol-font-family:Wingdings"><span style="mso-char-type:symbol;mso-symbol-font-family:Wingdings">¨¨<span lang="EN-US" style="font-size:11.0pt;line-height:
107%;font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:
minor-latin;mso-bidi-font-family:&quot;Times New Roman&quot;;mso-bidi-theme-font:minor-bidi;
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA">.</span>¨¨<span lang="EN-US" style="font-size:11.0pt;line-height:
107%;font-family:&quot;Calibri&quot;,sans-serif;mso-ascii-theme-font:minor-latin;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:minor-latin;mso-hansi-theme-font:
minor-latin;mso-bidi-font-family:&quot;Times New Roman&quot;;mso-bidi-theme-font:minor-bidi;
mso-ansi-language:EN-US;mso-fareast-language:EN-US;mso-bidi-language:AR-SA">.</span>¨¨¨¨</span></span> г.&nbsp;&nbsp;
    
    <br />
    ___________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ______________<br />
    Сотрудник кредитного отдела&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; подпись<br />
    ___________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ______________<br />
    Сотрудник Комплаенс-Контроля&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; подпись<br />        														

    </div>
</div>
        <%--</asp:Panel>--%>    </form>
</body>
</html>
