<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptAccount.aspx.cs" Inherits="СreditСonveyor.Partners.rptAccount" %>

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
<asp:HiddenField ID="hfGuarID" runat="server" />
<asp:HiddenField ID="hfUserID" runat="server" />
<asp:HiddenField ID="hfRequestID" runat="server" />
<%--<asp:Panel ID="pnlAgreement" runat="server">--%>
      <br />
      <input name="b_print" type="button" class="ipt" onClick="printdiv('agreement');" value=" Печать ">
      <br /><br />
<%--      <input name="b_printclose" type="button" class="ipt" onClick="printdivclose('agreement');" value=" Закрыть ">--%>

<div id="agreement">
    <style>
        body {
            font-size: 12px;
        }
        .centre{text-align:center;}
/**rptReceptionAct**/        
        .width100{width:100%;}
        table {border-collapse:collapse;font-size:12px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}
        .centre2 { text-align:center;  }
        .right{ text-align:right;  }
        
        .auto-style1 {
            width: 222px;
        }
        
        .auto-style2 {
            width: 139px;
        }
        
        .auto-style6 {
            width: 259px;
        }
        .auto-style7 {
            width: 278px;
        }
        .auto-style8 {
            width: 288px;
        }
        
        .auto-style9 {
            height: 16px;
        }
        
        .auto-style10 {
            height: 16px;
            width: 52px;
        }
        .auto-style11 {
            width: 52px;
        }
        
        .auto-style12 {
            width: 74px;
        }
        
        </style>

    <div style = "width:720px;text-align:justify;">

<div style="page-break-after: always">

    <div id="statements" runat="server">
    
    <br />
    <h6 class="centre">ЗАЯВЛЕНИЕ НА ОТКРЫТИЕ БАНКОВСКОГО СЧЕТА ФИЗИЧЕСКОГО ЛИЦА (АКЦЕПТ ПРИНЯТИЯ ПУБЛИЧНОЙ ОФЕРТЫ)</h6>
  

	Я, <asp:Label ID="lblCustomerFIO1" runat="server" Text=""></asp:Label>, в целях обслуживания в ОАО «Капитал Банк» (далее - Банк),<br />
    1. Прошу<br />
<table class="line">
    <tr>
        <td class="auto-style1">Открыть депозитный счет:</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • до востребования&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • срочный</td>
    </tr>
    <tr>
        <td class="auto-style1">Выпустить платежную карту:</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Да&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Нет</td>
    </tr>
    <tr>
        <td class="auto-style1">Номер для получения смс-оповещения:</td>
        <td>+996 |____|____|____| |____|____|____|____|____|____|</td>
    </tr>
    <tr>
        <td class="auto-style1">Открыть доступ на хранение ценностей в индивидуальном банковском сейфе</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Да&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Нет</td>
    </tr>
    <tr>
        <td class="auto-style1">Открыть доступ к интернет-платежам посредством платежной карты</td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Да&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Нет</td>
    </tr>
    <tr>
        <td class="auto-style1">Кодовое слово (на кириллице)</td>
        <td>____|____|____|____|____|____|____|____|____|____|____|____|____|____|____</td>
    </tr>
</table>

2. Идентификационные сведения о себе:<br />
<table class="line">
    <tr>
        <td colspan="2">Фамилия (девичья, если есть)</td>
        <td class="auto-style7">Имя</td>
        <td class="auto-style2">Отчество (при наличии)</td>
    </tr>
    <tr>
        <td colspan="2"><asp:Label ID="lblSurname" runat="server" Text=""></asp:Label></td>
        <td class="auto-style7"><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label></td>
        <td class="auto-style2"><asp:Label ID="lblOtchestvo" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style8">Дата рождения</td>
        <td class="auto-style6"><asp:Label ID="lblBirthDate" runat="server" Text="lblBirthDate"></asp:Label>
        </td>
        <td class="auto-style7">Место рождения</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style8">Гражданство</td>
        <td class="auto-style6">&nbsp;</td>
        <td class="auto-style7">ИНН</td>
        <td><asp:Label ID="lblINN" runat="server" Text="lblINN"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">Данные документа, удостоверяющего личность:</td>
    </tr>
    <tr>
        <td class="auto-style8">Наименование документа</td>
        <td class="auto-style6">Серия и номер документа</td>
        <td class="auto-style7">Срок действия</td>
        <td>Выдан, Код подразделения </td>
    </tr>
    <tr>
        <td class="auto-style8">Паспорт</td>
        <td class="auto-style6">
            <asp:Label ID="lblSeriesOfPasp" runat="server" Text="lblSeriesOfPasp"></asp:Label>&nbsp<asp:Label ID="lblNoOfPasp" runat="server" Text="lblNoOfPasp"></asp:Label></td>
        <td class="auto-style7">с <asp:Label ID="lblIssueDate" runat="server" Text="Issue"></asp:Label>&nbsp;г.  
            по <asp:Label ID="lblValidDate" runat="server" Text="Valid"></asp:Label>&nbsp;г.</td>
        <td> 
            <asp:Label ID="lblIssueAuthority" runat="server" Text="lblIssueAuthority"></asp:Label>
         </td>
    </tr>
    <tr>
        <td class="auto-style8">Для иностранных граждан и лиц без гражданства, находящихся в КР:</td>
        <td class="auto-style6">• Разрешение на     временное проживание
            <br />
            • Виза
        </td>
        <td class="auto-style7">Серия и номер документа</td>
        <td>Срок действия
с________________________ по </td>
    </tr>
    <tr>
        <td class="auto-style8">Адрес места регистрации</td>
        <td colspan="3" class="auto-style6"><asp:Label ID="lblAddressRegistration" runat="server" Text="lblAddressRegistration"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">Адрес места фактического проживания (со слов клиента)</td>
        <td colspan="3" class="auto-style6"><asp:Label ID="lblAddressResidence" runat="server" Text="lblAddressResidence"></asp:Label>
        </td>
    </tr>
     <tr>
        <td colspan="4">Номера телефонов (мобильный, рабочий), адрес электронной почты (при наличии):
            <asp:Label ID="lblPhone" runat="server" Text="lblPhone"></asp:Label>
         </td>
    </tr>
    <tr>
        <td class="auto-style8">Цель и предполагаемый характер деловых отношений с Банком</td>
        <td class="auto-style6"></td>
        <td class="auto-style7"></td>
        <td></td>
    </tr>
    <tr>
        <td class="auto-style8">Место работы и должность</td>
        <td class="auto-style6"></td>
        <td class="auto-style7"></td>
        <td></td>
    </tr>
</table>



        <br />



        Я подтверждаю, что ознакомлен(а) и согласен(а) с условиями предоставления и использования банковских услуг (публичная оферта) и тарифами, размещенными на официальном сайте <a href="http://www.capitalbank.kg">www.capitalbank.kg</a>.
        <br />
        Приведенные выше сведения являются достоверными. В случае изменения места жительства, данных паспорта, фамилии – обязуюсь информировать Банк в течении 5 рабочих дней.
        <br />
        Я даю согласие на сбор, обработку моих персональных данных, представленных в настоящем Заявлении, а также данных, полученных в ходе исполнения Договора, и передачу их:
        <br />
        •	С целью получения Банком отчета в/из Кредитного бюро, их правопреемникам, любому лицу, которое как полагает Банк, может оказать содействие в принятии решения относительно предоставления кредитов и/или кредитных заменителей, для предварительного одобрения возможного кредитного лимита по потребительским кредитам;
        <br />
        •	С целью получения рассылки материалов информационного/рекламного на указанный в заявлении номера телефона;
        <br />
        •	Иным лицам для целей, не противоречащих законодательству в сфере правового регулирования работы с персональными данными.
        <br />
        <br />
        Данное согласие действует в течение всего срока предоставления мне услуг, для целей которых предоставлены мои персональные данные, и хранения данных об оказанных услугах в соответствии с законодательством Кыргызской Республики. Я понимаю все риски, связанные с оплатой через Интернет, обязуюсь хранить и не передавать данные карты третьим лицам.
        <br />
        <br />
        *При заказе пенсионной карты:
        <br />
        Я осведомлен о нижеследующем:
        <br />
        •	обязан лично каждые 6 месяцев извещать письменно управление Социального фонда КР по месту жительства о продлении срока перечисления причитающейся мне пенсии на данный счет, в случае неисполнения данного требования по истечении указанного срока перечисление моей пенсии на счет пенсионера будет прекращено;
        <br />
        •	в случае моего выбытия за пределы республики я обязан известить письменно управление Социального фонда КР по месту жительства;
В рамках обслуживания моей Карты пенсионера разрешаю:
        <br />
        •	снять со счета излишне перечисленную сумму пенсии и иных социальных выплат с 1 числа месяца прекращения права на получение пенсии;
        <br />
        •	сотрудникам Социального фонда Кыргызской Республики производить проверку правильности зачисления причитающихся мне пенсий и иных социальных выплат на счет;
        <br />
        •	Банку предоставлять сведения Социальному фонду КР по моему счету пенсионера в случае не снятия денежных средств в течение 6 и более месяцев.
                                                                    
        <br />
        <br />
        Клиент ____________________ / Асанов Асан Асанович / 21 октября 2022 г.
							              <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Подпись клиента)                                 	               

	        <br />
        ______________________ / TEST / 21 октября 2022 г.
       <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Подпись)
			
               <br />
        ______________________ / Ф.И.О. контролера_________________________________ / 21 октября 2022 г.
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Подпись)
								
        <br />
        <br />
        Разрешаю  _______________________ </div> <!--statements -->

 </div>

<!--/////////////////////////////////////// -->

<div style="page-break-after: always">

    <div id="questionnaire" runat="server">
    
    <br />
    <h6 class="centre">Анкета клиента физического лица – резидента</h6>
  

<table class="line">
   <tr>
        <td colspan="4" style="text-align:center"><b>Вид анкеты</b></td>
   </tr>
   <tr>
        <td colspan="2">• Заполняется впервые</td>
        <td colspan="2">• Обновление анкетных данных</td>
   </tr>
   <tr>
        <td colspan="4" style="text-align:center"><b>I. Идентификационные сведения клиента</b></td>
   </tr>
   <tr>
        <td class="auto-style10">№</td>
        <td class="auto-style9">Наименование поля</td>
        <td colspan="2" class="auto-style9">Сведения о клиенте</td>
    </tr>
    <tr>
        <td class="auto-style10">1</td>
        <td class="auto-style9">Статус клиента</td>
        <td colspan="2" class="auto-style9">• Резидент                                   • Нерезидент </td>
    </tr>
    <tr>
        <td class="auto-style11">2</td>
        <td>Фамилия
        </td>
        <td>Имя
        </td>
        <td>Отчество (при наличии)
        </td>
    </tr>
    <tr>
        <td class="auto-style11"></td>
        <td><asp:Label ID="lblSurname2" runat="server" Text="lblSurname2"></asp:Label></td>
        <td><asp:Label ID="lblCustomerName2" runat="server" Text="lblCustomerName2"></asp:Label></td>
        <td><asp:Label ID="lblOtchestvo2" runat="server" Text="lblOtchestvo2"></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style11">3</td>
        <td>Дата рождения</td>
        <td colspan="2"><asp:Label ID="lblBirthDate2" runat="server" Text="lblBirthDate2"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style10">4</td>
        <td class="auto-style9">Место рождения (при наличии)</td>
        <td colspan="2" class="auto-style9">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style11">5</td>
        <td>Национальность (при наличии)</td>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td class="auto-style11">6</td>
        <td>Пол</td>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style11">7</td>
        <td>Гражданство</td>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style11">8</td>
        <td>Гражданин / резидент США</td>
        <td colspan="2">• Да (в т.ч. Green Card)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Нет </td>
    </tr>
    <tr>
        <td class="auto-style11">9</td>
        <td>Семейное положение</td>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td class="auto-style11">10</td>
        <td colspan="3">Данные документа, удостоверяющего личность:</td>
    </tr>
    <tr>
        <td class="auto-style11">Наименование документа</td>
        <td>Серия и номер документа</td>
        <td>Срок действия</td>
        <td>Выдан, Код подразделения (при наличии)</td>
    </tr>
    <tr>
        <td class="auto-style11">Паспорт</td>
        <td><asp:Label ID="lblSeriesOfPasp2" runat="server" Text="lblSeriesOfPasp2"></asp:Label>&nbsp<asp:Label ID="lblNoOfPasp2" runat="server" Text="lblNoOfPasp2"></asp:Label></td>
        <td>с <asp:Label ID="lblIssueDate2" runat="server" Text="Issue2"></asp:Label>&nbsp;г.  
            по <asp:Label ID="lblValidDate2" runat="server" Text="Valid2"></asp:Label>&nbsp;г.</td>
        <td>
                <asp:Label ID="lblIssueAuthority2" runat="server" Text="lblIssueAuthority2"></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style11">11</td>
        <td>ПИН / ИНН / Номер удостоверения социальной защиты</td>
        <td><asp:Label ID="lblINN2" runat="server" Text="lblINN2"></asp:Label></td>
        <td></td>
    </tr>
    <tr>
        <td class="auto-style11">12</td>
        <td>Адрес места регистрации (при наличии)</td>
        <td colspan="2"><asp:Label ID="lblAddressRegistration2" runat="server" Text="lblAddressRegistration2"></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style11">13</td>
        <td>Адрес места фактического проживания (со слов клиента)</td>
        <td colspan="2"><asp:Label ID="lblAddressResidence2" runat="server" Text="lblAddressResidence2"></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style11">14</td>
        <td colspan="3">Номера телефонов (мобильный, рабочий), адрес электронной почты (при наличии): <asp:Label ID="lblPhone2" runat="server" Text="lblPhone2"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4" style="text-align:center"><b>II. Сведения о деловом профиле клиента</b></td>
   </tr>
    <tr>
        <td class="auto-style11">15</td>
        <td>Цель открытия счета в Банке</td>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td class="auto-style11">16</td>
        <td>Место работы и должность</td>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td class="auto-style11">17</td>
        <td>Источник происхождения денежных средств</td>
        <td colspan="2">Заработная плата</td>
    </tr>
    <tr>
        <td class="auto-style11">18</td>
        <td>Среднегодовой (ожидаемый) оборот денежных средств</td>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td class="auto-style11">19</td>
        <td>Наличие у клиента бенефициарного владельца</td>
        <td colspan="2">• Да (при наличии заполняется анкета бенефициарного владельца) 
            <br />
            • Нет</td>
    </tr>
    <tr>
        <td class="auto-style11">20</td>
        <td colspan="3">Сведения о документах клиента, подтверждающих полномочия по распоряжению денежными средствами / имуществом (согласно карточке образов подписей) (доверенности):</td>
    </tr>
    <tr>
        <td class="auto-style11">21</td>
        <td>Являетесь ли Вы публичным должностным лицом (ПДЛ)</td>
        <td colspan="2">• Да (заполняется анкета ПДЛ)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; • Нет</td>
    </tr>
   
</table>
        Настоящим подтверждаю достоверность данных, указанных в настоящей анкете и обязуюсь в течении пяти рабочих дней предоставлять информацию обо всех изменениях данных, указанных в настоящей анкете, а также обязуюсь предоставлять копии документов, содержащих такие сведения. В соответствии с требованиями Закона КР «Об информации персонального характера» от 14 апреля 2008 года №58, даю согласие на обработку персональных данных в целях выполнения требований законодательства Кыргызской Республики в сфере противодействия финансированию террористической деятельности и легализации (отмыванию) преступных доходов.<br />
        <br />
        Клиент ____________________ / Асанов Асан Асанович / 21 октября 2022 г.
							              <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Подпись клиента)                                 	               

	        <br />
        <br />
<table class="line">
   <tr>
        <td colspan="4" style="text-align:center"><b>III.	Информация о верификации, проверке и уровне риска клиента</b></td>
   </tr>
   <tr>
        <td class="auto-style12">№</td>
        <td>Наименование поля</td>
        <td colspan="2">Сведения о клиенте</td>
    </tr>
    <tr>
        <td class="auto-style12">22</td>
        <td>Сведения о проведении верификации клиента и о результатах верификации</td>
        <td>• Проведена			• Иное примечание
• Не проведена			_____________________
Дата проведения: «         »                       202    г.
</td>
    </tr>
    <tr>
        <td class="auto-style12">23</td>
        <td>Сведения о проверке клиента в Санкционных перечнях, устанавливаемых Законодательством Кыргызской Республики, и результатах проверки</td>
        <td>• Отсутствует в перечнях
• Присутствует в перечнях
Дата и время проверки: 21 октября 2022 г.
</td>
    </tr>
    <tr>
        <td class="auto-style12">24</td>
        <td>Уровень риска</td>
        <td>• Высокий&nbsp;&nbsp;&nbsp; • Средний&nbsp;&nbsp; • Низкий</td>
    </tr>
    <tr>
        <td class="auto-style12">25</td>
        <td>Обоснование оценки уровня риска</td>
        <td></td>
    </tr>
    <tr>
        <td class="auto-style12">26</td>
        <td>Дата занесения в АБС информации, указанной в настоящей анкете, заполнения или последнего обновления анкеты</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style12">27</td>
        <td>Дата очередного обновления анкеты</td>
        <td></td>
    </tr>
 
</table>
        <br />

	        <br />
        TEST /______________________ /  ___________г.
       <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; (Подпись)
			
               <br />
        ______________________ / Ф.И.О. контролера_________________________________ / _________г.
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Подпись)
								
        <br />
    </div> <!--statements -->

 </div>

<!--/////////////////////////////////////// -->

<div style="page-break-after: always">

    <div id="Div1" runat="server">
    
    <br />
    <h6 class="centre">КАРТОЧКА ОБРАЗЦА ПОДПИСИ
                            физического лица</h6>

        Я, Асанов Асан Асанович,

предоставляю образец подписи, которую прошу считать обязательной при совершении операций по счету. Чеки и другие распоряжения по счету прошу считать действительными при наличии на них указанной ниже подписи:


        <br />
        <br />
        _______________________                       Подпись г-на(-жи) Асанов Асан Асанович
               учинена в моем присутствии&nbsp;
        <br />
        Образец подписи&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
        <br />
        _______________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ______________________
        <br />
        Подпись владельца счета&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Подпись сотрудника



    </div>
</div>

</div>
</div>

</form>
     
</body>
</html>


 <!-----------------------------------------------------------------------------------  >

</div>
</div>

<%--</asp:Panel>--%>



