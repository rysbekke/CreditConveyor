<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptProffer.aspx.cs" Inherits="СreditСonveyor.Card.rptProffer" %>

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
<div id="proffer">
    <style>
        .centre{text-align:center;}
/**rptReceptionAct**/        
        .width100{width:100%;}
        table {border-collapse:collapse;font-size:11px;}
        #proffer{font-size:11px;}
        div{font-size:11px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px; font-size:10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}  

        </style>
<div style = "width:720px;text-align:justify;">
<table>
    <tr>
        <td class="auto-style5">ДоскредоБанк&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                             </td>
        <td class="auto-style13">Открытое акционерное общество «Дос-Кредобанк» 
            <br />Кыргызская Республика, Бишкек, пр.Чуй, 92 (6 этаж)
            <br />
        </td>
    </tr>
</table>

    <div style="page-break-after: always">    

<div class="centre">ЗАЯВЛЕНИЕ–АНКЕТА  ФИЗИЧЕСКОГО ЛИЦА&nbsp;  (резидента и нерезидента)<br />
    (АКЦЕПТ ПРИНЯТИЯ ПУБЛИЧНОЙ ОФЕРТЫ БАНКА)<br />
        </div>


        <table class="line">

            <tr>
                <td colspan ="2" class="auto-style11">Вид анкеты (нужное отметить <span style="font-size:8.0pt;line-height:115%;
font-family:&quot;Wingdings 2&quot;;mso-ascii-font-family:Arial;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-fareast-theme-font:minor-fareast;mso-hansi-font-family:
Arial;mso-bidi-font-family:Arial;mso-ansi-language:RU;mso-fareast-language:
RU;mso-bidi-language:AR-SA;mso-char-type:symbol;mso-symbol-font-family:&quot;Wingdings 2&quot;"><span style="mso-char-type:symbol;mso-symbol-font-family:&quot;Wingdings 2&quot;">R</span></span>)</td>
                <td colspan ="1" class="auto-style11">  Первичная анкета</td>
                <td class="auto-style11">  Обновленная анкета</td>
            </tr>
            <tr>
                <td colspan="4">Я, <asp:Label ID="lblFIO1" runat="server" Text="FIO"></asp:Label>, в целях обслуживания в ОАО «Дос-Кредобанк» (далее – Банк),<br />
                    <strong>1) Прошу</strong></td>
            </tr>
            <tr>
                <td colspan="2">Открыть депозитный счет:</td>
                <td>	востребования</td>
                <td>	срочный</td>
            </tr>
            <tr>
                <td colspan="2">Выпустить платёжную карту:
                    
                </td>
                <td>	физическое лицо</td>
                <td>	зарплатный проект </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table>
                        <tr>
                            <td>9</td>
                            <td>4</td>
                            <td>1</td>
                            <td>7</td>
                            <td>1</td>
                            <td>3</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>	социальная карта</td>
                <td>	карта пенсионера </td>
                <td>	Ко-бренд</td>
                <td>	Элкарт Бизнес</td>
            </tr>
            <tr>
                <td colspan="2">Подключить услугу смс-оповещение по карте:</td>
                <td>	Да </td>
                <td>	Нет</td>
            </tr>
            <tr>
                <td colspan="2">Номер для получения СМС-оповещения:</td>
                <td>+996 ____________________</td>
                <td></td>
            </tr>
            <tr>
                <td colspan="2">Открыть доступ на хранение ценностей в индивидуальном банковском сейфе *</td>
                <td>	Да</td>
                <td>	Нет</td>
            </tr>
            <tr>
                <td colspan="2">Открыть доступ к системе Интернет/Мобильный банкинг и присвоить логин:</td>
                <td>Логин:_____________________<br />
                    	в режиме полного доступа<br />
                    	в режиме просмотр
                </td>
                <td>СМС авторизация:<br />
                    	Да<br />
                    	Нет
                </td>
            </tr>
            <tr>
                <td colspan="2">Открыть доступ к интернет-операциям посредством платежной карты:</td>
                <td>	Да</td>
                <td>	Нет</td>
            </tr>
            <tr>
                <td colspan="2">Провести идентификацию электронного кошелька</td>
                <td colspan ="2">Указать название кошелька ____________________
                    <table class="line">
                    <tr>
                        <td>9</td><td>9</td><td>6</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">Кодовое слово (не более 16 печатных букв кириллицы, цифры запрещены):</td>
                <td colspan ="2">
                    <table class="line">
                    <tr>
                        <td>&nbsp</td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                    </tr>
                    </table>
                </td>
            </tr>
              <tr>
                <td colspan="4"><strong>2) Сообщаю о себе следующие сведения:</strong></td>
            </tr>
    <tr>
        <td colspan ="2">Фамилия (если есть, девичья)<br />
            <asp:Label ID="lblCustomerSurname" runat="server" Text=""></asp:Label></td>

        <td>Имя<br />
            <asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label></td>
        <td>Отчество<br />
            <asp:Label ID="lblOtchestvo" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style3">Дата рождения<br />
            <asp:Label ID="lblDateOfBirth" runat="server" Text=""></asp:Label></td>
        <td>Национальность<br />

        </td>
        <td>Место рождения<br />
            <asp:Label ID="lblResidenceCityName" runat="server" Text=""></asp:Label></td>
        <td>Гражданство<br />
            <asp:Label ID="lblResidenceCountry" runat="server" Text="Кыргызская Республика"></asp:Label></td>
    </tr>
    <tr>
        <td colspan ="2">Пол _______<br />
            Мобильный телефон/Email<br />
            <asp:Label ID="lblContactPhone" runat="server" Text=""></asp:Label>
        </td>
        <td>Семейное положение<br />
            __________________</td>
        <td>Статус клиента <br />
            1) Резидент <br />
            2) Нерезидент </td>
    </tr>
    <tr>
        <td colspan ="2">Документ, удостоверяющий личность: (нужное отметить <span style="font-size:8.0pt;line-height:115%;
font-family:&quot;Wingdings 2&quot;;mso-ascii-font-family:Arial;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-fareast-theme-font:minor-fareast;mso-hansi-font-family:
Arial;mso-bidi-font-family:Arial;mso-ansi-language:RU;mso-fareast-language:
RU;mso-bidi-language:AR-SA;mso-char-type:symbol;mso-symbol-font-family:&quot;Wingdings 2&quot;"><span style="mso-char-type:symbol;mso-symbol-font-family:&quot;Wingdings 2&quot;">R</span></span>)<br />
            <strong>1) Для граждан КР:</strong><br />
            	паспорт гр. КР/ ID карта    <br />
            	удостоверение личности офицера 
(прапорщика)/военный билет военнослужащегосрочной службы
            
            <br />
            <strong>2) Для иностранных граждан</strong><br />
            	паспорт иностранного гр-на <br />
            	вид на жительство в КР<br />
            	Удостоверение беженца<br />
            	Свидетельство о регистрации ходатайства о признании лица беженцем<br />
</td>
        <td>Серия и номер:<br />
            <asp:Label ID="lblDocumentSeries" runat="server" Text=""></asp:Label><br />
            кем выдан:<br />
            <asp:Label ID="lblIssueAuthority" runat="server" Text=""></asp:Label><br />
            ИНН<br />
            <asp:Label ID="lblINN" runat="server" Text=""></asp:Label>
</td>
        <td>Дата выдачи:<br />
            <asp:Label ID="lblIssueDate" runat="server" Text=""></asp:Label><br />
            Дата окончания срока действия:<br />
            <asp:Label ID="lblValidTill" runat="server" Text=""></asp:Label><br />
        </td>
    </tr>
    <tr>
        <td class="auto-style3">Для иностранных граждан и лиц без гражданства, находящихся в КР:
            <br />
            	Разрешение на временное проживание
            <br />
            	Виза
        </td>
        <td>Серия (если имеется) _________<br />
                Номер документа ____________<br />
                ПИН ___________
        </td>
        <td colspan="2">Дата начала срока действия права пребывания "___"__________20___года<br />
            Дата окончания срока действия права пребывания "___"__________20___года<br />
        </td>
    </tr>
    <tr>
        <td colspan ="2">Адрес регистрации (страна, область, район, населенный пункт, улица, дом, корпус, квартира): <br />
            <asp:Label ID="lblCustomerRegAddress" runat="server"></asp:Label>
            <br />
            <br />
            <br />
        </td>
        <td>Фактический адрес (страна, область, район, населенный пункт, улица, дом, корпус, квартира):<br />
            <asp:Label ID="lblCustomerResAddress" runat="server"></asp:Label>
            <br />
            <br />
            <br />
        </td>
        <td>Место работы/Род занятий 
Должность

            <br />
            __________________________<br />
            Должность<br />
            __________________________<br />
            <br />
        </td>
    </tr>
  
    <tr>
        <td colspan="4">Сведения о наличии у клиента бенефицианского (конечный выгодоприобретатель) владельца (нужное подчеркнуть <span style="font-size:8.0pt;line-height:115%;
font-family:&quot;Wingdings 2&quot;;mso-ascii-font-family:Arial;mso-fareast-font-family:
&quot;Times New Roman&quot;;mso-fareast-theme-font:minor-fareast;mso-hansi-font-family:
Arial;mso-bidi-font-family:Arial;mso-ansi-language:RU;mso-fareast-language:
RU;mso-bidi-language:AR-SA;mso-char-type:symbol;mso-symbol-font-family:&quot;Wingdings 2&quot;"><span style="mso-char-type:symbol;mso-symbol-font-family:&quot;Wingdings 2&quot;">R</span></span>)</td>
    </tr>
    <tr>
        <td colspan="2" class="auto-style12"> Подтверждаю, что являюсь бенефициарным владельцем (конечный выгодоприобретатель)</td>
        <td colspan="2" class="auto-style12"> Бенефициарным * владельцем являе(ю)ся: Ф.И.О<br />
            ________________________________________________</td>
    </tr>
    <tr>
        <td colspan="2">Я являюсь публичным должностным лицом</td>
        <td colspan="1"> ДА</td>
        <td colspan="1"> НЕТ</td>
       
    </tr>

    <tr>
        <td colspan="2">Источники происхождения денежных средств:<br />
            Заработок от деятельности, продажа собственного имущества, сдача в аренду собственного имущества, материальная помощь, накопления (нужное подчеркнуть)<br />
            <br />
            Другое ____________________________________________<br />
        </td>
        <td colspan="2">Обоснование:<br />
            Более подробно указать вид деятельности, указать вид продажи или сдачи в аренду имущества (авто, мебель, быт.техника, недвижимость). Указать от кого получена материальная помощь, указать источник накоплений (зарплата, гонорары, пенсия, алименты) (нужное подчеркнуть)<br />
            Другое
            ___________________________________________<br />
            <br />
        </td>
    </tr>
</table>

        <p style="font-size:9px">
Я подтверждаю, что ознакомлен(-на) и согласен (-на) с условиями предоставления и использования банковских услуг (публичная оферта) и тарифами, размещённый на официальном сайте <a href="http://www.dcb.kg">www.dcb.kg</a>.,
<br />Подтверждаю, что информация, указанная в моей анкете, является полной и правдивой. Обязуюсь письменно уведомлять Банк о любых изменениях, касающихся сведений, указанных в анкете, в течение 3 рабочих дней.
<br />Я даю согласие на сбор, обработку моих персональных данных, представленных в настоящей Анкете, а также данных, полученных в ходе исполнения Договора, и передачу их:
<br />
	С целью работы с электронным кошельком и идентификации согласно законодательству Кыргызской Республики на срок действия электронного кошелька;
<br />	С целью получения Банком отчета в/из Кредитного бюро, их правопреемникам, любому лицу, которое как полагает Банк, может оказать содействие в принятии решения относительно предоставления кредитов и/или кредитных заменителей, для предварительного одобрения возможного кредитного лимита по потребительским кредитам; 
<br />	С целью получения рассылки материалов информационного/рекламного на указанный в заявлении номер телефона;
<br />	С целью использования в дальнейшем в документах Банка простой электронной подписи для подтверждения моих действий, связанных с электронным кошельком.
<br />	Иным лицам для целей, не противоречащих законодательству в сфере правового регулирования работы с персональными данными.
<br />
        </p>
        <p style="font-size:9px">
            Данное согласие действует в течение всего срока предоставления мне услуг, для целей которых предоставлены мои персональные данные, и хранения данных об оказанных услугах в соответствии с законодательством Кыргызской Республики. Я понимаю все риски, связанные с оплатой через Интернет, обязуюсь хранить и не передавать данные карты третьим лицам.<br />
        </p>

    <table>
        <tr>
            <td class="auto-style9">Сообщаю образец моей подписи, которая является обязательной 
                <br />
                при совершении операций по моему у Вас счету№</td>
            <td class="auto-style10">Подпись г-на (-жи)__<u><asp:Label ID="lblFIO6" runat="server" Text="FIO Customer"></asp:Label>&nbsp;</u>учинена в моем присутствии</td>
        </tr>
        <tr>
            <td class="auto-style7">
                ____________________<br />
                Подпись клиента
            </td>
            <td class="auto-style8">
&nbsp;_________________&nbsp <asp:label ID="lblFioUser" runat="server" text="FIO User"></asp:label>
                <br />
                Подпись контролера
            </td>
        </tr>
    </table>
    <br />
    <br />

    <br />
    <br />
 
</div>



    
<div style="page-break-after: always">    

    <div class="centre">ЗАПОЛНЯЕТСЯ БАНКОМ</div>
    <br />




    <table class="line">
    <tr>
        <td class="auto-style14">Сведения о проведении верификации идентификационных сведений клиента (нужное отметить)</td>
        <td>Проведено <br />
            Иное примечание<br /> _____________</td>
    </tr>
    <tr>
        <td class="auto-style14">Сведения о проверке клиента в Списках наблюдения Банка (Санкционный перечень, список OFAC и др.), и о результатах проверки (нужное отметить)</td>
        <td>
            В Списках наблюдения:<br />
            Отсутствует 
            Присутствует <br />
            Дата проведения:<br />
&nbsp;&quot;___&quot;_______20__года<br />
        </td>
    </tr>
    <tr>
        <td class="auto-style14">Сведения о письменном разрешении по принятию на обслуживание публичного должностного лица ***</td>
        <td></td>
    </tr>
    <tr>
        <td class="auto-style14">Дата занесения в базу данных информации, указанной в настоящей анкете, и Ф.И.О. ответственного сотрудника</td>
        <td>ФИО:<br />
            Дата:&nbsp;&nbsp; &quot;___&quot;_______20__года</td>
    </tr>
    <tr>
        <td class="auto-style14">Дата очередного обновления сведений, изложенных в анкете (на основе результата оценки риска)</td>
        <td>ФИО:<br />
            Дата:&nbsp;&nbsp; &quot;___&quot;_______20__года</td>
    </tr>
    <tr>
        <td class="auto-style14">Дата очередного обновления сведений, изложенных в анкете (на основе результата оценки риска), и Ф.И.О. ответственного сотрудника</td>
        <td>ФИО:<br />
            Дата:&nbsp;&nbsp; &quot;___&quot;_______20__года</td>
    </tr> 
     <tr>
        <td class="auto-style14">Дата заполнения или последнего обновления сведений, изложенных в анкете (на основании результата оценки риска), и Ф.И.О. ответственного сотрудника</td>
        <td>ФИО:<br />
            Дата:&nbsp;&nbsp; &quot;___&quot;_______20__года</td>
    </tr>
  
    </table>
    <br />
    <table class="line">
        <tr>
            <td>Степень (уровень) Риска:</td>
            <td>высокий </td>
            <td></td>
            <td>средний </td>
            <td></td>
            <td>низкий </td>
            <td></td>
            <td>(нужное отметить символом v)</td>
        </tr>
    </table>
    <br />
    Дата очередной проверки_______________         ________________Ф.И.О. сотрудника, проводившего проверку Подпись__________<br />
        <br />
    <table class="line">
        <tr>
            <td>Степень (уровень) Риска:</td>
            <td>высокий </td>
            <td></td>
            <td>средний </td>
            <td></td>
            <td>низкий </td>
            <td></td>
            <td>(нужное отметить символом v)</td>
        </tr>
    </table>
    <br />
    Дата очередной проверки_______________         ________________Ф.И.О. сотрудника, проводившего проверку Подпись__________<br />

        <br />
    <table class="line">
        <tr>
            <td>Степень (уровень) Риска:</td>
            <td>высокий </td>
            <td></td>
            <td>средний </td>
            <td></td>
            <td>низкий </td>
            <td></td>
            <td>(нужное отметить символом v)</td>
        </tr>
    </table>
    <br />
    Дата очередной проверки_______________         ________________Ф.И.О. сотрудника, проводившего проверку Подпись__________<br />

&nbsp;<table class="line">
        <tr>
            <td rowspan="4">Обоснование оценки <br />степени (уровня) Риска:</td>
            <td>Критерии</td>
            <td>Да</td>
            <td>Нет</td>
        </tr>
        <tr>
            <td>Страновой (географический) риск</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Риск, связанный с клиентом</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>Риск, связанный с деятельностью или продукцией клиента</td>
            <td></td>
            <td></td>
        </tr>
    </table>

    <br />Верификация клиента
    <table class="line">
        <tr>
            <td>
ФИО клиента:   _____<u><asp:Label ID="lblFIO2" runat="server" Text="FIO"></asp:Label></u>____
<br />Дата и время контакта __________________________________________________________
<br />Вид контакта: 
<br />1) Телефонный звонок ______________  2) Электронная почта_________________ 3) Иной метод_____________
<br />Подтверждение адреса (юридического и физического)&nbsp;<asp:Label ID="lblCustomerResAddress2" runat="server"></asp:Label>
                <br />
            Заключение по верификации:<br />
            Соответствуют предоставленные клиентом данные:  Да  Нет</td>
        </tr>
     </table>

    <br />
<br />ФИО ответственного сотрудник ________________________________			Подпись _______________________<br />
    <br />
    ФИО контроллера ________________________________			Подпись _______________________<br /><br />
    <br />
   


    <br />
    <p style="font-size:9px">
 1 Бенефициарный владелец – физическое лицо (физические лица), которое в конечном итоге (через цепочку владения и контроля) прямо или косвенно (через третьих лиц) владеет правом собственности или контролирует клиента либо физическое лицо, от имени или в интересах которого совершается операция (сделка)
<br />2 Публичные должностные лица (ПДЛ) - одно из следующих физических лиц: а) иностранное публичное должностное лицо (ИПДЛ) - лицо, выполняющее или выполнявшее значительные государственные или политические функции (публичные функции) в иностранном государстве (главы государств или правительств, высшие должностные лица в правительстве и иных государственных органах, судах, вооруженных силах, на государственных предприятиях, а также видные политические деятели, в том числе видные деятели политических партий);б) национальное публичное должностное лицо (НПДЛ)  - лицо, занимающее или занимавшее политическую и специальную государственную должность или политическую муниципальную должность в Кыргызской Республике, предусмотренную Реестром государственных и муниципальных должностей Кыргызской Республики, утверждаемым Президентом Кыргызской Республики, а также высшее руководство государственных корпораций, видные политические деятели, в том числе видные деятели политических партий; в) публичное должностное лицо международной организации  (ПДЛМО)- высшее должностное лицо международной организации, которому доверены или были доверены важные функции международной организацией (руководители, заместители руководителей и члены правления международной организации или лица, занимающие эквивалентные должности в международной организации).
<br />* Банковская ячейка открывается в Центральном филиале (адрес: г.Бишкекпр.Чуй92 )
<br />** В случае если клиент не является бенефициарным владельцем заполняется анкета бенефициарного владельца.
<br />*** Разрешение руководства Банка на принятие на обслуживание публичного должностного лица требуется для ИПДЛ, а также НПДЛ и ПДЛМО, которым установлен высокий уровень риска. 

    </p>
    
	
 </div>



    </div>
</div>
<%--</asp:Panel>--%>
    </form>
</body>
</html>
