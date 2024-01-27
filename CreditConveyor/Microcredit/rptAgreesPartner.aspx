﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptAgreesPartner.aspx.cs" Inherits="СreditСonveyor.Microcredit.rptAgreesPartner" %>

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
        .centre{text-align:center;}
/**rptReceptionAct**/        
        .width100{width:100%;}
        table {border-collapse:collapse;font-size:14px;}
        table tr td, table tr th{padding:0 2px;}
        table.line tr td {border:1px solid #000000;padding:0 10px;}
        table.line tr th {border:1px solid #000000;padding:0 10px;}
        table.line {border:1px solid #000000;padding:0 10px;}
        .width165{width:165px;}
        .centre2 { text-align:center;  }
        .right{ text-align:right;  }
        
        </style>

    <div style = "width:720px;text-align:justify;">
<div style="page-break-after: always">
<h4 class="centre">КРЕДИТНЫЙ ДОГОВОР (партнер) №<asp:Label ID="lblCreditNomer" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label></h4>
        <b><asp:Label ID="lblCity" runat="server" Text="Город"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Label ID="lblAgreementDate" runat="server" Text="Кредит.ДатаДоговора"></asp:Label>&nbsp;г.</b>
<br />
        <br />
        <b>ОАО «Капитал Банк Центральной Азии»</b>, именуемое в дальнейшем <b>«Кредитор»</b>, в лице 
        <b><asp:Label ID="lblManagerFIO" runat="server" Text="[Агент.Руководитель.ФИО]"></asp:Label></b>
        &nbsp;действующего(-ей) на основании Доверенности № <asp:Label ID="lblManagerDocNum" runat="server" Text="Филиал.НомерДокумента"></asp:Label> &nbsp; от &nbsp <asp:Label ID="lblManagerDocDate" runat="server" Text="Филиал.Документ.ДатаВыдачи"></asp:Label> г. с одной стороны, и гр.
       <b><asp:Label ID="lblCustomerFIO" runat="server" Text="[Клиент.ФИО]"></asp:Label></b> ,
        паспорт <asp:Label ID="lblDocumentSeries" runat="server" Text="[Клиент.Документ.Серия]"></asp:Label> <asp:Label ID="lblDocumentNo" runat="server" Text="[Клиент.Документ.Номер]"></asp:Label>, 
        выдан <asp:Label ID="lblIssueAuthority" runat="server" Text="[Клиент.Документ.Выдан]"></asp:Label>, от <asp:Label ID="lblIssueDate" runat="server" Text="[Клиент.Документ.ДатаВыдачи]"></asp:Label>&nbsp;г.,
        прописанный(-ая) по адресу:     <asp:Label ID="lblRegCustomerAddress" runat="server" Text="[Клиент.Адрес.Прописка]"></asp:Label>, 
        именуемый(-ая) в дальнейшем «Заемщик», с другой стороны, вместе в дальнейшем именуемые «Стороны», заключили настоящий Договор о нижеследующем:
        <br /><br /><div class="centre"><b>1. ПРЕДМЕТ ДОГОВОРА</b><br /> 
        <br />
        </div>
    <%--1.1. Кредитор предоставляет Заемщику кредит в сумме <br />--%>
        <b>1.1.</b>  Кредитор предоставляет Заемщику кредит в сумме&nbsp; <asp:Label ID="lblRequestSumm" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label> &nbsp;
        (<asp:Label ID="lblRequestSummWord" runat="server" Text="[Кредит.ОбщаяСумма.Пропись]"></asp:Label>)&nbsp;&nbsp;  <asp:Label ID="lblCurrencyName" runat="server" Text="[Кредит.Валюта]"></asp:Label>, для оплаты за  
        <asp:Label ID="lblCreditPurpose" runat="server" Text="[Заявка.Клиент.Цель]"></asp:Label> &nbsp;на условиях, предусмотренных настоящим Договором.   
        <br />
        <b>1.2.</b> Кредит предоставляется сроком на <b><asp:Label ID="lblApprovedPeriod" runat="server" Text="[Кредит.Срок]"></asp:Label></b> &nbsp;месяц (-а/-ев), погашение кредита и процентов по ним производится Заемщиком согласно установленному графику, являющемуся неотъемлемой частью настоящего Договора (Приложение № 1). 
        <br />
        <b>1.3.</b> Стороны обязуются соблюдать принципы кредитования: срочности, возвратности, целевого характера, платности и обеспеченности.
        <br />
        <br />
        <div class="centre"><b>2. УСЛОВИЯ ПРЕДОСТАВЛЕНИЯ КРЕДИТА И УПЛАТЫ ПРОЦЕНТОВ ЗА ПОЛЬЗОВАНИЕ ИМ</b></div>
  <br /><b>2.1.</b> За пользование кредитом Заемщик  ежемесячно уплачивает  Кредитору <b><asp:Label ID="lblRequestRate" runat="server" Text="[Кредит.Ставка]"></asp:Label> &nbsp;(<asp:Label ID="lblRequestRateWord" runat="server" Text="[Кредит.Ставка.Пропись]"></asp:Label>)</b> процент (-а/-ов) годовых  со дня  перечисления суммы кредита на транзитный счет по кредитам, в том числе налог с продаж в размере 2 % от суммы начисленных процентов.
        <%--2.1. За пользование кредитом Заемщик ежемесячно уплачивает Кредитору <asp:Label ID="lblRequestRate2" runat="server" Text="[Кредит.Ставка]"></asp:Label> () процент (-а/-ов) годовых со дня перечисления денежных средств в пользу Торговой организации на счет, открытый Банком согласно Договору сотрудничества, в том числе налог с продаж в размере 2 % от суммы начисленных процентов.--%>
        <br />
        <b>2.2.</b> В случае не погашения Заемщиком суммы основного долга и/или процентов в сроки, оговоренные в Приложении №1, Заемщик за просроченную задолженность основного долга и/или начисленных и не оплаченных процентов выплачивает Банку неустойку, в виде пени, в размере <asp:Label ID="lblRequestRate2" runat="server" Text="[Кредит.Ставка]"></asp:Label>&nbsp;(<asp:Label ID="lblRequestRateWord2" runat="server" Text="[Кредит.Ставка.Пропись]"></asp:Label>) процент (-а/-ов) годовых от суммы просроченной задолженности за каждый день просрочки. Начисление пени производится по фактическому числу дней со дня невыполнения обязательства.
        <br />
        <b>2.3.</b> При своевременном погашении Заемщиком основного долга по кредиту в соответствии с Приложением №1, годовая эффективная процентная ставка будет составлять <asp:Label ID="lblRequestRateEffect" runat="server" Text="[Кредит.ЭффСтавка]"></asp:Label> %. 
        <br />
        <b>2.4.</b> При получении кредита Заемщик уплачивает Кредитору организационные взносы, указанные в перечне расходов (платежей) клиентов Банка (Приложение № 2).
        <br />
        <br />
        <div class="centre"><b>3. ОБЕСПЕЧЕНИЕ КРЕДИТА</b><br /></div>
        <br /><b>3.1.</b> В обеспечение своевременного возврата Заемщиком кредита и выплаты процентов по нему, в залог предоставляется: 
        <br /><b>3.1.1</b>
        <br />
    <asp:GridView ID="gvProducts3" runat="server" AutoGenerateColumns="False" CssClass="line">
        <Columns>
            <asp:BoundField DataField="ProductMark" HeaderText="Марка" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductSerial" HeaderText="Серийный №" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductImei" HeaderText="imei-код" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Price" HeaderText="Цена" ItemStyle-CssClass="width165" />
        </Columns>
    </asp:GridView>
        <%--<asp:Label ID="lblCreditPurpose2" runat="server" Text="[Заявка.Клиент.Цель]"></asp:Label> --%>
    согласно Договору залога № <asp:Label ID="lblCreditNomer5" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label> &nbsp;от <asp:Label ID="lblAgreementDate5" runat="server" Text="[Кредит.ДатаДоговора]"></asp:Label>&nbsp;г..
        <br />
        <br />
        <div class="centre"><b>4. ВЫДАЧА КРЕДИТА И ПОРЯДОК РАСЧЕТОВ</b><br /> <br /> </div>
        <b>4.1.</b> Кредит предоставляется путем выдачи денег Заёмщику наличными со ссудного счёта или перечислением Кредитором суммы Кредита на транзитный счет для кредитов Заёмщика для дальнейшего перечисления в пользу Партнера на счет, открытый Банком согласно Договору о сотрудничестве. При этом обязательства Кредитора по предоставлению Кредита считаются исполненными с момента перечисления денежных средств на транзитный счет для кредитов Заёмщика.
        <br />
        <b>4.2. </b> Все платежи рассчитываются на базе календарного года равного 360 дням и месяца равного 30 дням, исходя из фактического числа дней пользования Кредитом, включая первый день пользования и исключая день погашения.
        <br />
        <b>4.3. </b> Максимальная сумма начисленной неустойки в виде пени, указанных в п.п. 2.2. не должна превышать 20 % от суммы выданного кредита. 
        <br />
        <b>4.4. </b> Начисление неустойки в виде пени, указанных в п. 2.2. прекращается,  по истечении 15 дней с момента направления извещения о возбуждении процедуры обращения взыскания на предмет залога.
        <br />
        <br />
        <div class="centre"><b>5. ПРАВА И ОБЯЗАННОСТИ СТОРОН</b><br /> <br /> </div>
        <b>5.1.</b> Кредитор вправе в любое время проводить проверки целевого использования кредита, а также состояния залогового обеспечения и выполнения обязательств Заемщиком по настоящему Договору и Договорам о залоге.
        <br />
        <b>5.2.</b> Кредитор вправе при наступлении срока платежа в безакцептном порядке списывать сумму основного долга, процентов и других платежей, связанных с настоящим Договором, со счетов Заемщика в сомах и в иностранной валюте, находящихся в Банке.
        <br />
        <b>5.3.</b> Кредитор вправе полностью или частично переуступать свои права и обязательства по настоящему Договору, а также по сделкам, связанным с обеспечением возврата кредита Заемщиком, другому лицу без согласия Заемщика. Заемщик не вправе полностью или частично переуступать свои права и обязательства по настоящему Договору, а также по сделкам, связанным с обеспечением возврата кредита Заемщиком, другому лицу без письменного согласия Банка.
        <br />
        <b>5.4.</b> В целях защиты общих интересов финансово-кредитных учреждений участников Кредитного Информационного Бюро (Кредитное Бюро), последние сохраняют за собой право обмениваться  информацией о кредитах  (для банков и других  финансово-кредитных учреждений, имеющих  лицензию на осуществление  банковской деятельности) или займах (для финансово-кредитных учреждений, не имеющих  лицензию) с другими финансовыми институтами посредством предоставления  соответствующей информации Кредитному Бюро в установленном порядке.
        <br />
        <b>5.5.</b> В случае полного/частичного возврата Заёмщиком приобретенного у Партнера Банка товара/услуги, Партнёр возвращает Банку сумму Кредита, согласно сумме возвращенного Заёмщиком товара/услуги по кредитному договору, перечисленного в пользу партнера по кредиту Заемщика.
        <br />
        <b>5.6.</b> Заемщик вправе досрочно погасить кредит полностью или по частям в любое время без взимания штрафных санкций (комиссий и иной платы) при условии предварительного письменного уведомления об этом Кредитора за тридцать дней до дня такого возврата.
        <br />
        <b>5.7.</b> Заемщик обязуется предоставлять информацию о наличии кредитов в других финансово-кредитных учреждениях, включая кредиты супруга(и), родителей и детей,  а также о соблюдении кредитной дисциплины по данным кредитам не реже чем 1 раз в полгода.
        <br />
        <b>5.8.</b> Заемщик обязуется погасить выданный ему кредит в сроки, указанные в Приложении №1 к  настоящему договору, путем внесения соответствующей суммы в кассу Кредитора наличными или перечислением на соответствующий счет, указанный в п.4.1. настоящего договора.
        <br />
        <b>5.9.</b> Заемщик незамедлительно уведомляет Кредитора об изменении адреса, реквизитов и обо всех других обстоятельствах, связанных с надлежащим исполнением своих обязательств по настоящему Договору, которые могут стать причиной неисполнения его обязательств, а также принимает все меры по их устранению. Почтовым (юридическим) адресом Заемщика является адрес, указанный  в настоящем Договоре. Все сообщения, отправленные Кредитором на данный адрес Заемщика, считаются доставленными Заемщику, если только Кредитор не уведомлен письменно об изменении постоянного адреса Заемщика.
        <br />
        <b>5.10.</b> Заемщик вправе обратиться к Кредитору в последующем с просьбой о пролонгации или реструктуризации кредита, а также получить разъяснения по порядку расчетов процентов по кредиту и штрафных санкций.
        <br />
        <b>5.11.</b> Заемщик вправе отказаться от получения кредита с момента подписания настоящего Договора до получения денежных средств.
        <br />
        <b>5.12.</b> При выполнении частично-досрочного погашения, заемщику производится пересчет графика погашения кредита и процентов при условии уплаты им комиссии за пересчет графика в размере, утвержденном тарифом банка и указанном в перечне расходов (платежей) клиентов Банка (Приложение № 2).
        <br />
        <b>5.13.</b> Подписание нового графика погашения Кредита производится в срок не позднее даты внесения очередного платежа по Кредиту. График, датированный более поздним числом, является основанием для платежей, приобщается в качестве Приложения № 1, заменяет прежний график, который утрачивает силу с момента подписания нового графика.
        <br />
        <b>5.14.</b> Заемщик гарантирует, что предоставленные им сведения о своем финансовом состоянии, данные о предпринимательской деятельности и/или имущественном положении являются достоверными и подтверждает, что предупрежден об уголовной ответственности за незаконное получение кредита путем предоставления ложных сведений о финансовом состоянии, данных о предпринимательской деятельности и/или имущественном положении.
        <br />
        <br />
        <div class="centre"><b>6. ОБСТОЯТЕЛЬСТВА НЕПРЕОДОЛИМОЙ СИЛЫ (ФОРС-МАЖОР)</b><br /> <br /> </div>
        <b>6.1.</b> Стороны освобождаются от частичного либо полного неисполнения или ненадлежащего исполнения обязательств по договору, если это явилось следствием обстоятельств непреодолимой силы (форс-мажор). О возникновении и прекращении обстоятельств непреодолимой силы Стороны информируют друг друга в срок не позднее 3 рабочих дней с момента начала указанных обстоятельств с целью выработки совместного решения альтернативных способов изменения своих обязательств. В случае несоблюдения условия об уведомлении, данная Сторона не сможет ссылаться на эти обстоятельства в последствии.
        <br />
        <br />
        <div class="centre"><b>7. ПРОЧИЕ ПОЛОЖЕНИЯ</b><br /> <br /> </div>
        <b>7.1.</b> Настоящий Договор заключен в двух подлинных экземплярах, имеющих одинаковую юридическую силу, по одному экземпляру для каждой.
        <br />
        <b>7.2.</b> Любые изменения и дополнения к настоящему договору имеют силу в том случае, если они оформлены в письменном виде и подписаны обеими Сторонами. 
        <br />
        <b>7.3.</b> Все споры и разногласия, возникающие между Сторонами по настоящему Договору, разрешается путем переговоров между Сторонами, а при не достижении соглашения подлежат рассмотрению в соответствии с действующим законодательством Кыргызской Республики в судебном порядке по месту нахождения Кредитора. 
        <br />
        <b>7.4.</b> Настоящий Договор вступает в силу с момента его подписания и действует до полного исполнения Сторонами предусмотренных настоящим Договором обязательств.
        <br />
        <b>7.5.</b>	Стороны настоящего Договора договорились использовать для деловой переписки и претензионной работы возможность использования электронной или иной связи. Электронная почта Кредитора help@doscredobank.kg, электронная почта Заемщика _______________________. 
        <br />Сторона, получившая от другой Стороны претензию или иное уведомление по указанным электронным адресам, считается надлежащим образом извещенной.

        <br />
        ______________ ______________________________________________________________
<br />&nbsp;&nbsp;&nbsp; (подпись)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Ф.И.О.) <br />
        <br />
        <div class="centre"><b>8. ЮРИДИЧЕСКИЕ АДРЕСА, ПОДПИСИ И ПЕЧАТИ СТОРОН</b></div>
<br />
<br />
<table>
    <tr>
        <td width="300"><b>Кредитор:</b></td>
        <td width="300"><b>Заемщик:</b></td>
    </tr>
    <tr>
        <td><b>ОАО «КапиталБанк»</b></td>
        <td><b><asp:Label ID="lblCustomerFIO2" runat="server" Text="[Клиент.ФИО]"></asp:Label></b></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCompanyName" runat="server" Text="[Филиал]" Visible="false"></asp:Label>
            <br />
            <asp:Label ID="lblCompanyAddress" runat="server" Text="[Филиал.Адрес]"></asp:Label>
        </td>
        <td>Паспорт серии &nbsp; <asp:Label ID="lblDocumentSeries2" runat="server" Text="[Клиент.Документ.Серия]"></asp:Label> &nbsp; <asp:Label ID="lblDocumentNo2" runat="server" Text="[Клиент.Документ.Номер]"></asp:Label> &nbsp; выдан &nbsp; <asp:Label ID="lblIssueAuthority2" runat="server" Text="[Клиент.Документ.Выдан]"></asp:Label> &nbsp; от &nbsp; <asp:Label ID="lblIssueDate2" runat="server" Text="[Клиент.Документ.ДатаВыдачи]"></asp:Label>&nbsp;г.</td>
    </tr>
    <tr>
        <td><%--К/счет --%><asp:Label ID="lblaccountNBKR" runat="server" Text="[Филиал.К/счет] в НБКР" Visible="false"></asp:Label></td>
        <td>ИНН <asp:Label ID="lblIdentificationNumber" runat="server" Text="[Клиент.ИНН]"></asp:Label></td>
    </tr>
    <tr>
        <td><%--БИК --%><asp:Label ID="lblCompanyBIK" runat="server" Text="[Филиал.БИК]" Visible="false"></asp:Label></td>
        <td>Адрес по прописке:</td>
    </tr>
    <tr>
        <td>ИНН <asp:Label ID="lblCompanyINN" runat="server" Text="[Филиал.ИНН]"></asp:Label></td>
        <td><asp:Label ID="lblRegCustomerAddress2" runat="server" Text="[Клиент.Адрес.Прописка]"></asp:Label></td>
    </tr>
    <tr>
        <td>Код ОКПО <asp:Label ID="lblCompanyOKPO" runat="server" Text="[Филиал.ОКПО]"></asp:Label></td>
        <td>Адрес фактического проживания:</td>
    </tr>
    <tr>
        <td>По доверенности №
            <asp:Label ID="lblManagerDocNum3" runat="server" Text="[Доверенность]" ></asp:Label> &nbsp; от &nbsp <asp:Label ID="lblManagerDocDate3" runat="server" Text="Филиал.Документ.ДатаВыдачи"></asp:Label></td>
        <td><asp:Label ID="lblResCustomerAddress" runat="server" Text="[Клиент.Адрес.Фактический]"></asp:Label></td>
    </tr>
    <tr>
        <td>_________________
            <br /><asp:Label ID="lblManagerFIO2" runat="server" Text="[Агент.Руководитель.ФИО]"></asp:Label></td>
        <td>(м.п.)	_________________
            <br /><asp:Label ID="lblCustomerFIO3" runat="server" Text="[Клиент.ФИО]" Visible="false"></asp:Label></td>
    </tr>
</table>
<br />
</div>
<div style="page-break-after: always">
<br />
        <h4 class="centre2">Приложение № 1 к Кредитному договору №
            <asp:Label ID="lblCreditNomer3" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label>
            &nbsp; от
            <asp:Label ID="lblAgreementDate3" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>&nbsp;г.
        </h4>
        <br />График погашения кредита и процентов			
<br /><b>Заемщик: <asp:Label ID="lblCustomerFIO4" runat="server" Text="[Клиент.ФИО]"></asp:Label></b>
<br />
<br />
<br /><b>Сумма кредита: <asp:Label ID="lblApprovedSumm" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label> (<asp:Label ID="lblApprovedSummWord" runat="server" Text="lblApprovedSummWord"></asp:Label>)</b>
<br /><b>Валюта кредита: <asp:Label ID="lblCurrencyName3" runat="server" Text="[Кредит.Валюта]"></asp:Label></b>			
<br /><b>Процентная ставка (% за год): <asp:Label ID="lblRequestRate4" runat="server" Text="[Кредит.Ставка]"></asp:Label> %</b>		
<br /><b>Эффективная % ставка (% за год): <asp:Label ID="lblRequestRateYear" runat="server" Text=""></asp:Label></b>	
<br /><b>Число периодов (месяцев): <asp:Label ID="lblApprovedPeriod2" runat="server" Text="[Кредит.Срок]"></asp:Label></b>		
<br /><b>Дата выдачи: <asp:Label ID="lblCreditIssueDate" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>&nbsp;г.</b>			
<br /><b>Дата погашения: <asp:Label ID="lblEndDate" runat="server" Text="[Кредит.ДатаОкончания]"></asp:Label>&nbsp;г.</b> 		
<br />
<br />   	
     <table class="line">
        <tr>
            <td>
                Номер кредита для погашения через терминалы/эл.кошельки - <asp:Label ID="lblCreditID" runat="server" Text="CreditID"></asp:Label>
            </td>
        </tr>
    </table>
<br />					
    <asp:GridView ID="gvGraphic" runat="server" AutoGenerateColumns="False">
     <Columns>
        <asp:BoundField DataField="PayDate" HeaderText="Дата выплаты" DataFormatString="{0:dd.MM.yyyy} г." ItemStyle-Width="110"/>
        <asp:BoundField DataField="MainSumm" HeaderText="Основная сумма" ItemStyle-Width="120"/>
        <asp:BoundField DataField="PercentsSumm" HeaderText="Проценты" ItemStyle-Width="80"/>
        <asp:BoundField DataField="TotalSumm" HeaderText="Итого" ItemStyle-Width="110"/>
        <asp:BoundField DataField="BaseDeptToPay" HeaderText="Остаток основной суммы" ItemStyle-Width="110"/>
    </Columns>
    </asp:GridView>
    <table>
        <tr>
            <td width="126">Итого</td>
            <td width="134"><asp:Label ID="lblMainSum" runat="server" Text="Label"></asp:Label></td>
            <td width="90"><asp:Label ID="lblPercentsSumm" runat="server" Text="Label"></asp:Label></td>
            <td width="110"><asp:Label ID="lblTotalSumm" runat="server" Text="Label"></asp:Label></td>
            <td width="110"></td>
        </tr>
    </table>
<br />
<br />Способы погашений кредитов ОАО "КапиталБанк"
<br />- терминалы "O!", "QuickPay", "Dos_credobank", "Beeline", "Pay24"
<br />- электронные кошелки "Balance KG", "MegaPay", "О! Деньги"
<br />- мобильное приложение "Элкарт Мобайл"
<br />- кассы отделений Банка.
<br />Погашения по кредитам через сотрудников банка без наличия у него ордера на прием платежей категорически запрещено. За осуществление платежей по кредиту через сотрудников банка без наличия ордера, банк ответственности не несет.
<br /><b><u>Все платежи произведенные после 15:30 часов текущего дня считаются поступившими на следующий банковский день.</u></b>
<br />
<br />
         <table class="line">
        <tr>
            <td>
                Номер кредита для погашения через терминалы/эл.кошельки - <b><asp:Label ID="lblCreditID2" runat="server" Text="CreditID"></asp:Label></b>
            </td>
        </tr>
    </table>
    
<br />
<br />С условиями погашения кредита и процентов ознакомлен(а) и согласен(а)	
<br />
<br />	        		
<br />					
<br />
<br />
      <table>
          <tr>
              <td>Банк:</td>
              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
              <td>Заемщик:</td>
          </tr>
          <tr>
              <td>ОАО «КапиталБанк»</td>
              <td></td>
              <td></td>
          </tr>
          <tr>
              <td>По доверенности №<asp:Label ID="lblManagerDocNum4" runat="server" Text="Филиал.НомерДокумента"></asp:Label> &nbsp; от &nbsp <asp:Label ID="lblManagerDocDate4" runat="server" Text="Филиал.Документ.ДатаВыдачи"></asp:Label></td>
              <td></td>
              <td></td>
          </tr>
          <tr>
              <td>/______________/
                  <br />
                  <asp:Label ID="lblManagerFIO3" runat="server" Text="[Агент.Руководитель.ФИО]"></asp:Label>
<%--                  <asp:Label ID="lblCompanyCustomerManagersFIO3" runat="server" Text="[Филиал.ОсновнойРуководитель]" Visible ="false"></asp:Label></td>--%>
              <td></td>
              <td>/______________/
                  <br />
                  <asp:Label ID="lblCustomerFIO5" runat="server" Text="[Клиент.ФИО]"></asp:Label></td>
          </tr>

      </table>
        <br />
        </div>
        <div style="page-break-after: always">
        <br />
 
       <h4 class="right">Приложение №2 к кредитному договору</h4>
        <br /><p class="centre"><b>Перечень расходов (платежей) клиентов банка и штрафных санкций.</b></p>
					
<br />
<%--<table class="line">
<tr><td colspan="2" class="centre"><b>Расходы (платежи) клиента банка по кредиту</b></td></tr>
<tr><td>Сумма кредита</td><td><asp:Label ID="lblApprovedSumm2" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label>&nbsp<asp:Label ID="lblCurrencyName6" runat="server" Text="[Кредит.Валюта]"></asp:Label></td></tr>	
<tr><td>Номинальная процентная ставка</td><td><asp:Label ID="lblRequestRate5" runat="server" Text="[Кредит.Ставка]"></asp:Label>%</td></tr>
<tr><td>Комиссия за рассмотрение кредитной заявки (для юридических лиц)</td><td>Согласно тарифам Банка</td></tr>
<tr><td>Комиссия за предоставление кредита/банковской гарантии</td><td>Согласно тарифам Банка</td></tr>
<tr><td>Комиссия за выдачу очередного транша</td><td>Согласно тарифам Банка</td></tr>
<tr><td>Комиссия за открытие и обслуживание ссудного и/или текущего счетов</td><td>Согласно тарифам Банка</td></tr>
<tr><td>Комиссия за расчетно-кассовое обслуживание по кредиту</td><td>Согласно тарифам Банка</td></tr>
<tr><td>Плата за предоставление выписок со счетов заемщика</td><td>Согласно тарифам Банка</td></tr>
<tr><td>Комиссия  за рассмотрение заявления  о реструктуризации (в том числе за конвертацию валюты), пересмотр действующих графиков, замене поручителя, изменении структуры залогового обеспечения и т.д.</td><td>1500 сом</td></tr>
<tr><td>Перерасчет графика в случае частичного досрочного погашения основной суммы</td><td>Без комиссии</td></tr>
<tr><td>Платежи в пользу третьих лиц (плата за услуги страхования, нотариуса и др.)</td><td>Согласно тарифам третьих лиц</td></tr>
<tr><td>Наложение ареста на заложенное недвижимое имущество в государственных органах</td><td>1500-5000 сом</td></tr>
<tr><td>Регистрация движимого имущества в ЦЗРК</td><td>Согласно тарифам третьих лиц</td></tr>
<tr><td>Комиссия за частичное досрочное погашение кредита</td><td>Отсутсвует</td></tr>
<tr><td>Комиссия за полное  досрочное погашение кредита</td><td>Отсутсвует</td></tr>
<tr><td colspan="2" class="centre"><b>Штрафные санкции и пени банка</b></td></tr>
<tr><td>Пеня за просрочку основной суммы долга (в день)</td><td><asp:Label ID="lblRequestRate6" runat="server" Text="[Кредит.Ставка]"></asp:Label>%/360</td></tr>
<tr><td>Пеня за просрочку оплаты начисленных процентов (в день)</td><td><asp:Label ID="lblRequestRate7" runat="server" Text="[Кредит.Ставка]"></asp:Label>%/360</td></tr>
<tr><td>Штраф за несвоевременное выполнение условий по дополнительным соглашениям</td><td>0,1% в день от суммы кредита/кредитной линии</td></tr>
<tr><td>Штраф за нецелевое использование кредита от первоначальной суммы кредита</td><td>20% от суммы, использованной не по целевому назначению</td></tr>
<tr><td>Штраф в связи с утратой и/или ухудшением качества залога</td><td>Согласно тарифам Банка</td></tr>
</table>
<br /><br />

<br />ОАО &quot;Капиталбанк&quot;, по доверенности № <asp:Label ID="lblManagerDocNum6" runat="server" Text="Филиал.НомерДокумента"></asp:Label> от <asp:Label ID="lblManagerDocDate6" runat="server" Text="Филиал.Документ.ДатаВыдачи"></asp:Label>:
        <br />
        <asp:Label ID="lblManagerFIO6" runat="server" Text="[Система.Пользователь]"></asp:Label>	/____________/
<br />(подпись)	 <asp:Label ID="lblAgreementDate8" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>
<br /><br />Заемщик: <asp:Label ID="lblCustomerFIO11" runat="server" Text="[Клиент.ФИО]"></asp:Label>	/____________/
<br />(подпись)	 <asp:Label ID="lblAgreementDate9" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>--%>



<table class="line">
<tr><td colspan="2" class="centre"><b>Расходы (платежи) клиента банка по кредиту</b></td></tr>
<tr><td>Сумма кредита/банковской гарантии</td><td><asp:Label ID="lblApprovedSumm2" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label>&nbsp<asp:Label ID="lblCurrencyName6" runat="server" Text="[Кредит.Валюта]"></asp:Label></td></tr>	
<tr><td>Номинальная процентная ставка</td><td><asp:Label ID="lblRequestRate5" runat="server" Text="[Кредит.Ставка]"></asp:Label>%</td></tr>
<tr><td>Комиссия за открытие и обслуживание счета по кредиту/банковской гарантии</td><td><asp:Label ID="lblComisiionForOpenCount" runat="server" Text="150,00 (Сто пятьдесят сом, 00 тыйын)"></asp:Label></td></tr>
<tr><td>Комиссия за открытие и обслуживание счета по кредитному траншу</td><td>Отсутствует</td></tr>
<tr><td>Комиссия за расчетно-кассовое обслуживание по кредиту</td><td>Отсутствует</td></tr>
<tr><td>Комиссия за изменение условий договора по заявлению клиента (конвертацию валюты, пересмотр действующих графиков, замена поручителя, изменении структуры залогового обеспечения и т.д., за исключением случаев реструктуризации кредита)</td><td>1500 сом</td></tr>
<tr><td>Комиссия за резервирование денежных средств (за неиспользуемую часть кредитной линии), если договором предусмотрено безусловное обязательство банка по предоставлению кредитных средств</td><td>От 0% до 2% годовых, согласно решению Кредитного Комитета</td></tr>
<tr><td>Платежи в пользу третьих лиц (плата за услуги страхования, нотариуса, Государственной регистрационной службы при Правительстве Кыргызской Республики, Государственного агентства по земельным ресурсам при Правительстве Кыргызской Республики и др.)</td><td>Согласно тарифам третьих лиц (от 150 – 5000 сом, при этом данные расходы могут измениться в будущем)</td></tr>


<tr><td colspan="2" class="centre"><b>Штрафные санкции и пени банка</b></td></tr>
<tr><td>За просрочку оплаты платежей по основной сумме долга и по процентам (в день)</td><td><asp:Label ID="lblRequestRate7" runat="server" Text="[Кредит.Ставка]"></asp:Label>%/360</td></tr>
<tr><td>За просрочку более 30 календарных дней пополнения депозита, предоставленного в залог по кредиту</td><td>Отсутствует</td></tr>
<tr><td>За несвоевременный возврат пакета оригиналов документов по залоговому обеспечению (за исключением случаев несвоевременного возврата в связи с форс-мажорными обстоятельствами и другими объективными причинами, которые должны быть рассмотрены банком)</td><td>0,1% в день от суммы кредита/кредитной линии</td></tr>

<tr><td>Условия расторжения договора после получения денежных средств</td><td>2% от ссудной задолженности по кредиту на день погашения</td></tr>
<tr><td>Другие расходы (платежи): за несвоевременное выполнение условий по дополнительным соглашениям</td><td>0,1% в день от суммы кредита/кредитной линии</td></tr>
<tr><td>Другие расходы (платежи): за не целевое использование кредита</td><td>20 % от суммы, использованной не по целевому назначению</td></tr>
</table>
<br /><br />

            <br />
        <br />
<table>
    <tr>
        <td><asp:Label ID="lblManagerFIO6" runat="server" Text="[Система.Пользователь]"></asp:Label></td>
        <td>/____________/</td>
        <td><asp:Label ID="lblAgreementDate8" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td>(подпись)</td>
        <td></td>
    </tr>
    <tr>
        <td>Заемщик: <asp:Label ID="lblCustomerFIO11" runat="server" Text="[Клиент.ФИО]"></asp:Label></td>
        <td>/____________/</td>
        <td><asp:Label ID="lblAgreementDate9" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td>(подпись)</td>
        <td></td>
    </tr>
</table>
			
<br />
</div>

<div style="page-break-after: always">        
<br />

<h4 class="centre">РАСПИСКА<br />
о получении оригинала и ознакомлении с условиями Кредитного договора</h4>
<br />Я, _____________________________________________________________________, 
<br />настоящей распиской подтверждаю, что получил(а) на руки оригинал Кредитного договора, ознакомлен(а) со всеми условиями договора, условия договора понятны, согласен(на) с условиями Кредитного договора, заключенного между мною и ОАО «КапиталБанк», №<asp:Label ID="lblCreditNomer6" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label> &nbsp;от <asp:Label ID="lblAgreementDate7" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>.
<br />Я обязуюсь возместить Банку все расходы, связанные с неисполнением или ненадлежащим исполнением мною обязательств по кредитному договору (направление уведомлений, регистрация в уполномоченных органах, направление извещений, получение исполнительной надписи нотариуса и др.).<br />
        <br />
&nbsp;__________________________________                /____________/
                                  <br />
        (ФИО) заемщика&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Подпись)

        <br />
        <br />
        <asp:Label ID="lblAgreementDate6" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>

        <br />
        <br />
        <p class="centre">Отметки Банка</p>
        Настоящая расписка составлена и подписана в присутствии кредитного офицера/администратора
<br />&nbsp;________________________________                 /_____________/
<br />(Ф.И.О.)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Подпись)<br />
        <br />
        <br />
        <br />
        <br />
        <br />

                                                                
      		
<br />
</div>

<div style="page-break-after: always">
<br />


       <!-------------------------------------------------------------------------------------->


<!---------------------------------------------------------------------------------------->

         
<!---------------------------------------------------------------------------------------->
<div id="pril6" runat="server" visible="false">
    <div style="page-break-after: always" >
        <br />
    <div class="right">Приложение №6 к Программе кредитования потребительский кредит «КапиталБанк»</div>
    <br />
    <h4 class="centre">Форма финансовых данных клиента</h4>
    <br />
    <div class="centre"></div>
    <br />
    <div class="centre">
    <table class="width100 line">
        <tr>
            <td>Средняя выручка в день:</td>
            <td><asp:Label ID="lblRevenueDay" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td>Количество рабочих дней в месяце:</td>
            <td>30</td>
        </tr>
        <tr>
            <td>Наценка (в %):</td>
            <td><asp:Label ID="lblCostPrice" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td>Валовая прибыль:</td>
            <td>
                <asp:Label ID="lbl" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td>Накладные расходы:</td>
            <td><asp:Label ID="lblOverhead" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td>Семейные расходы:</td>
            <td><asp:Label ID="lblFamilyExpenses" runat="server" Text="Label"></asp:Label></td>
        </tr>

        <tr>
            <td>Доходы членов семьи:</td>
            <td></td>
        </tr>
        <tr>
            <td>Чистая прибыль:</td>
            <td>
                <asp:Label ID="lblChp" runat="server" Text="Label"></asp:Label></td>
        </tr>
    </table>
        </div>
        </div>
</div>    
<!---------------------------------------------------------------------------------------->
        <!---------------------------------------------------------------------------------------->
       <div style="page-break-after: always">
        <br />


<div id="maritalStatus0" runat="server">
    
    <br />
    <h4 class="centre">Заявление о семейном положении Заемщика</h4>
    <br />
    <br />
  

	Я, <asp:Label ID="lblCustomerFIO12" runat="server" Text=""></asp:Label> паспортные данные: 
    <br />
    <asp:Label ID="lblCustomerPassport2" runat="server" Text=""></asp:Label> заявляю, 
    <br />
    что на момент получения кредита в ОАО «КапиталБанк» в браке не состою.
    <br />

           <br />
           Дата:
    <asp:Label ID="lblAgreementDate10" runat="server" Text="Дата"></asp:Label>

           <br />
           Подпись: ____________________

        <br />
    <br />

        </div> <!--maritalStatus2 -->


    <div id="maritalStatus1" runat="server">
    
    <br />
    <h4 class="centre">Заявление</h4>
    <br />
    <br />
  

	Я, <asp:Label ID="lblCustomerFIO13" runat="server" Text=""></asp:Label>, 
        <asp:Label ID="lblDateOfBirth" runat="server" Text="DateOfBirth"></asp:Label> г.р., паспортные данные: <asp:Label ID="lblCustomerPassport3" runat="server" Text=""></asp:Label>, настоящим обязуюсь довести сведения до своего (ей)
                                                            <br />
           <br />
           супруга(ги)&nbsp; ФИО_____________________________________________________________________, 
информацию о получении кредита и предоставления залога (движимое имущество) в ОАО «КапиталБанк» на предусмотренных условиях: 
<br />
           в сумме <asp:Label ID="lblRequestSumm5" runat="server" Text="lblRequestSumm"></asp:Label> сом под <asp:Label ID="lblRequestRate9" runat="server" Text="Rate"></asp:Label>% годовых.<br />
           <br />
           <br />
&nbsp;<br />
           Дата:  <asp:Label ID="lblAgreementDate12" runat="server" Text="Дата"></asp:Label>

           <br />

           <br />
           Подпись: ____________________

        <br />

        </div> <!--maritalStatus2 -->
 </div>
</div>
    </div></form>
     
</body>
</html>


 <!-----------------------------------------------------------------------------------  >

</div>
</div>

<%--</asp:Panel>--%>



