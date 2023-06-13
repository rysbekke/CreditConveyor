<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptAgreesInst.aspx.cs" Inherits="СreditСonveyor.Partners.rptAgreesInst" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script src="/js/jquery-barcode.js"></script>
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
      &nbsp;<br />
<%--<input type="text" id="barcodeValue" value="12345670">
<asp:HiddenField ID="HiddenField1" runat="server" value="12345670"/>--%>

      <br />

<asp:Label ID="barcodeValue" runat="server" Text="dfsdfds" ClientIDMode="Static"></asp:Label>
<asp:Label ID="barcodeValue2" runat="server" Text="32432" ClientIDMode="Static"></asp:Label>
<asp:Label ID="lblIsSulpak" runat="server" Text="0" ClientIDMode="Static"></asp:Label>

      <br /><br />
<%--      <input name="b_printclose" type="button" class="ipt" onClick="printdivclose('agreement');" value=" Закрыть ">--%>
      <br />
      <input name="b_print" type="button" class="ipt" onClick="printdiv('agreement');" value=" Печать ">
      <br /><br />
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
        

        .barcodeTarget {float:left}
        .barcodeTarget2 {float:right}
        #canvasTarget {visibility:hidden;}
        </style>

    <div style = "width:720px;text-align:justify;">
<div style="page-break-after: always">



    <div id="submit" hidden>
        <input type="button" value="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Generate the barcode&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;">
    </div>

<div id="barcodeTarget" class="barcodeTarget"></div>
<div id="barcodeTarget2" class="barcodeTarget2"></div>
<%--<canvas id="canvasTarget" ></canvas>--%>
    
    <div style="clear:both"></div>

     


<%--    <style>
        * {
    color:#7F7F7F;
    font-family:Arial, sans-serif;
    font-size:12px;
    font-weight:normal;
}
#config {
    margin: 10px 0 10px 0px;
}
.config {
    float: left;
    width: 200px;
    height: 250px;
    border: 1px solid #000;
    margin-left: 10px;
}
.config .title {
    font-weight: bold;
    text-align: center;
}
.config .barcode2D, #miscCanvas {
    display: none;
}
#submit {
    clear: both;
}

input[type="button"] {
    margin: 10px 0 10px 0px;
}

#barcodeTarget, #canvasTarget {
    margin-top: 20px;
}

    </style>--%>



<h4 class="centre">КРЕДИТНЫЙ ДОГОВОР (рассрочка) №<asp:Label ID="lblCreditNomer" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label></h4>
        <b><asp:Label ID="lblCity" runat="server" Text="Город"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Label ID="lblAgreementDate" runat="server" Text="Кредит.ДатаДоговора"></asp:Label>&nbsp;г.</b>
<br />
        <br />
        <b>ОАО «Дос-Кредобанк»</b>, именуемое в дальнейшем <b>«Кредитор»</b>, в лице 
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
    <asp:GridView ID="gvProducts2" runat="server" AutoGenerateColumns="False" CssClass="line">
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
        <b>7.5.</b> Стороны настоящего Договора договорились использовать для деловой переписки и претензионной работы возможность использования электронной или иной связи. Электронная почта Кредитора help@doscredobank.kg, электронная почта Заемщика _______________________. 
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
        <td><b>ОАО «Дос-Кредобанк»</b></td>
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
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>

<div style="page-break-after: always">
<br />


       <!-------------------------------------------------------------------------------------->

        <h4 class="centre">ДОГОВОР О ЗАЛОГЕ №<asp:Label ID="lblCreditNomer4" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label></h4>
        <asp:Label ID="lblCity2" runat="server" Text="Город"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:Label ID="lblAgreementDate4" runat="server" Text="Кредит.ДатаДоговора"></asp:Label>&nbsp;г.
<br />
<br />
ОАО «Дос-Кредобанк», в дальнейшем именуемое «ЗАЛОГОДЕРЖАТЕЛЬ», в лице <asp:Label ID="lblCompanyWorkPosition" runat="server" Text="[Филиал.ОсновнойРуководитель.Должность]" Visible="False"></asp:Label>&nbsp;<asp:Label ID="lblManagerFIO5" runat="server" Text="[Агент.Руководитель.ФИО]"></asp:Label>, действующего на основании Доверенности № <asp:Label ID="lblManagerDocNum2" runat="server" Text="[Филиал.НомерДокумента]"></asp:Label> от <asp:Label ID="lblManagerDocDate2" runat="server" Text="[Филиал.Документ.ДатаВыдачи]"></asp:Label>, с одной стороны, и
	<asp:Label ID="lblCustomerFIO6" runat="server" Text="[Клиент.ФИО]"></asp:Label>, именуемая в дальнейшем «ЗАЛОГОДАТЕЛЬ» (паспорт <asp:Label ID="lblDocumentSeries3" runat="server" Text="[Клиент.Документ.Серия]"></asp:Label> <asp:Label ID="lblDocumentNo3" runat="server" Text="[Клиент.Документ.Номер]"></asp:Label>, 
        выдан <asp:Label ID="lblIssueAuthority3" runat="server" Text="[Клиент.Документ.Выдан]"></asp:Label>, от <asp:Label ID="lblIssueDate3" runat="server" Text="[Клиент.Документ.ДатаВыдачи]"></asp:Label>&nbsp;г.,
        прописанный(-ая) по адресу:     <asp:Label ID="lblRegCustomerAddress3" runat="server" Text="[Клиент.Адрес.Прописка]"></asp:Label>,), зарегистрированная по адресу: <asp:Label ID="lblResCustomerAddress2" runat="server" Text="[Клиент.Адрес.Прописка]г. Бишкек, ж/м Колмо, ул. Биримдмк 14"></asp:Label>, я с другой стороны, вместе в дальнейшем именуемые «Стороны», заключили настоящий договор о нижеследующем:
    <br /><br /><div class="centre">1. ПРЕДМЕТ ДОГОВОРА<br /><br /></div>
	                                                       	
<br />1.1. Согласно Кредитному договору № <asp:Label ID="lblCreditNomer2" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label>&nbsp;от <asp:Label ID="lblAgreementDate2" runat="server" Text="Кредит.ДатаДоговора"></asp:Label> &nbsp;(далее по тексту – «Кредитный договор») Залогодержатель предоставляет Залогодателю кредит в сумме <asp:Label ID="lblRequestSumm3" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label> &nbsp;(<asp:Label ID="lblRequestSummWord3" runat="server" Text="[Кредит.ОбщаяСумма.Пропись]"></asp:Label>) <asp:Label ID="lblCurrencyName4" runat="server" Text="[Кредит.Валюта]"></asp:Label>,&nbsp; сроком на <asp:Label ID="lblApprovedPeriod3" runat="server" Text="[Кредит.Срок]"></asp:Label> месяц(-а/-ев). Залогодержатель взимает с Залогодателя за предоставленную сумму кредита &nbsp; <asp:Label ID="lblRequestRate3" runat="server" Text="[Кредит.Ставка]"></asp:Label> % годовых по срочным ссудам и &nbsp; <asp:Label ID="lblRequestRate8" runat="server" Text="[Кредит.Ставка]"></asp:Label> &nbsp; % годовых по просроченным ссудам.
<br />1.2.	В качестве обеспечения исполнения обязательств по Кредитному договору, Залогодатель передает, а Залогодержатель принимает в залог следующие движимое имущество:
    <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="line">
        <Columns>
            <asp:BoundField DataField="ProductMark" HeaderText="Марка" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductSerial" HeaderText="Серийный №" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductImei" HeaderText="imei-код" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Price" HeaderText="Цена" ItemStyle-CssClass="width165" />
        </Columns>
    </asp:GridView> 
<br />1.3. Заложенные имущества оценивается Сторонами стоимостью в <asp:Label ID="lblRequestSumm2" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label>  (<asp:Label ID="lblRequestSummWord2" runat="server" Text="[Кредит.ОбщаяСумма.Пропись]"></asp:Label>) <asp:Label ID="lblCurrencyName2" runat="server" Text="[Кредит.Валюта]"></asp:Label>. 
<br />1.4. Залогодатель гарантирует, что в  дальнейшем заложенное имущество никому не продано, не заложено, в споре не состоит.
<br />1.5. Залогодатель не вправе распоряжаться заложенным имуществом без письменного согласия  Залогодержателя.
<br />1.6. Заложенное имущество остается на ответственном хранении у Залогодателя. 
<br />1.7. Залогодатель обязуется принимать все меры, необходимые для сохранения залога, и несет за него полную ответственность в соответствии с действующим законодательством.
<br />1.8. Риск случайной гибели заложенного имущества, вызванный форс-мажорными обстоятельствами, возлагается на Залогодателя.
<br />1.9. В случае невозможности восстановления залога Залогодатель обязан с согласия Залогодержателя заменить залог другим имуществом, стоимостью и ликвидностью (по оценке Залогодержателя) не ниже утраченного залога.  
<br />1.10. Если в течение действия настоящего договора стоимость заложенного имущества по оценке Залогодержателя окажется по каким-либо причинам ниже согласованной в п.3. стоимости, Залогодатель обязан предоставить дополнительное залоговое имущество в течение 5-ти дней до указанной в п.3. стоимости.  
<br />1.11. При неисполнении Залогодателем своих обязательств по Кредитному договору, Залогодержатель вправе, в том числе  и до истечения срока этих договоров, обратить взыскание на залог по стоимости, равной сумме неисполненного обязательства, с зачислением вырученной суммы в погашение задолженности в соответствии с нормами законодательства Кыргызской Республики. При этом Залогодатель обязуется всячески способствовать Залогодержателю в процессе передачи ему права собственности на предмет залога или третьему лицу по решению Залогодержателя.
<br />1.12. В соответствии с достигнутой договоренностью между сторонами, настоящим договором определен внесудебный порядок обращения  взыскания  на предмет залога в случае неисполнения или ненадлежащего выполнения Залогодателем своих обязательств по Кредитному договору. 
<br />1.13. В случае не выплаты на предмет залога, Залогодатель обязан обеспечить передачу предмета залога Залогодержателю  или  его  представителю, на основании письменного акта приема-передачи в течение _5_ дней с момента истечения срока.
<br />1.15. Залогодержатель  после  вступления во владение предметом залога обязан:
<br />     - обеспечить сохранность предмета залога;
<br />     - принимать добросовестные меры к реализации предмета залога, таким образом, чтобы оно было наиболее выгодно для всех участвующих сторон с учетом реальных условий действительности.
<br />1.16. Залогодержатель по своему усмотрению вправе применить один из следующих способов реализации заложенного имущества:
 вправе реализовать предмет залога путем прямого заключения  договора купли-продажи с третьими лицами. При этом Залогодержатель вправе распорядиться предметом залога без согласия Залогодателя как его представитель на основании данного соглашения;    
<br /><br />
<br />                                              Согласен ____________________
<br />                                                                               (подпись Залогодателя)
        <br />
<br />Соглашением сторон продажная начальная (стартовая) цена при реализации имуществ на торгах определена в размере <asp:Label ID="lblRequestSumm4" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label> &nbsp; (<asp:Label ID="lblRequestSummWord4" runat="server" Text="[Кредит.ОбщаяСумма.Пропись]"></asp:Label>) &nbsp; <asp:Label ID="lblCurrencyName5" runat="server" Text="[Кредит.Валюта]"></asp:Label>.
<br />
<br />                                              Согласен  ____________________
<br />                                                             (подпись Залогодателя) 

<br />
<br />     Залогодатель вправе приобрести предмет залога выкупить заложенное имущество с требованиями Закона КР "О залоге".
<br />     
<br />1.24. Все спорные вопросы, возникающие по данному договору, решаются Сторонами путем переговоров. 
<br />1.25. Срок действия договора:
<br />•	Начало - с момента подписания Сторонами настоящего договора.
<br />•	Окончание - после выполнения всех обязательств по Кредитному договору. 
<br />1.26. Договор составлен в двух экземплярах, по одному экземпляру для каждой из Сторон.
<br /><br />
        <div class="centre">Адреса и подписи сторон:</div>
<br />
<br />
<table>
    <tr>
        <td width="300">Залогодержатель:</td>
        <td width="300">Залогодатель:</td>
    </tr>
    <tr>
        <td>ОАО «Дос-Кредобанк»</td>
        <td><asp:Label ID="lblCustomerFIO7" runat="server" Text="[Клиент.ФИО]"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCompanyName2" runat="server" Text="[Филиал]"></asp:Label>
            <br />
            <asp:Label ID="lblCompanyAddress2" runat="server" Text="[Филиал.Адрес]"></asp:Label>
        </td>
        <td>Паспорт серии &nbsp; <asp:Label ID="lblDocumentSeries4" runat="server" Text="[Клиент.Документ.Серия]"></asp:Label> &nbsp; <asp:Label ID="lblDocumentNo4" runat="server" Text="[Клиент.Документ.Номер]"></asp:Label> &nbsp; выдан &nbsp; <asp:Label ID="lblIssueAuthority4" runat="server" Text="[Клиент.Документ.Выдан]"></asp:Label> &nbsp; от &nbsp; <asp:Label ID="lblIssueDate4" runat="server" Text="[Клиент.Документ.ДатаВыдачи]"></asp:Label>&nbsp;г.</td>
    </tr>
<%--    <tr>
        <td>К/счет <asp:Label ID="Label29" runat="server" Text="[Филиал.К/счет] в НБКР" Visible="False"></asp:Label></td>
        <td>ИНН <asp:Label ID="Label30" runat="server" Text="[Клиент.ИНН]"></asp:Label></td>
    </tr>
    <tr>
        <td>БИК <asp:Label ID="Label31" runat="server" Text="[Филиал.БИК]" Visible="False"></asp:Label></td>
        <td>Адрес по прописке:</td>
    </tr>--%>
    <tr>
        <td>ИНН <asp:Label ID="lblCompanyINN2" runat="server" Text="[Филиал.ИНН]"></asp:Label></td>
        <td><asp:Label ID="lblRegCustomerAddress4" runat="server" Text="[Клиент.Адрес.Прописка]"></asp:Label></td>
    </tr>
    <tr>
        <td>Код ОКПО <asp:Label ID="lblCompanyOKPO2" runat="server" Text="[Филиал.ОКПО]"></asp:Label></td>
        <td>Адрес фактического проживания:</td>
    </tr>
    <tr>
        <td>По доверенности №
            <asp:Label ID="lblManagerDocNum5" runat="server" Text="Филиал.НомерДокумента"></asp:Label> &nbsp; от &nbsp <asp:Label ID="lblManagerDocDate5" runat="server" Text="Филиал.Документ.ДатаВыдачи"></asp:Label></td>
        <td><asp:Label ID="lblResCustomerAddress3" runat="server" Text="[Клиент.Адрес.Фактический]"></asp:Label></td>
    </tr>
    <tr>
        <td>_________________
          <br /><asp:Label ID="lblManagerFIO4" runat="server" Text="[Агент.Руководитель.ФИО]"></asp:Label></td>
        <td>_________________
           <br /><asp:Label ID="lblCustomerFIO8" runat="server" Text="[Клиент.ФИО]"></asp:Label></td>
    </tr>
</table>
 <br />
</div>




l<div style="page-break-after: always">
<br />
        <h4 class="centre2">Приложение № 1 к Кредитному договору №
            <asp:Label ID="lblCreditNomer3" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label>
            &nbsp; от
            <asp:Label ID="lblAgreementDate3" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>&nbsp;г.
        </h4>
            График погашения кредита и процентов			
<br />Заемщик: <asp:Label ID="lblCustomerFIO4" runat="server" Text="[Клиент.ФИО]"></asp:Label>				
<br />
<br />Сумма кредита: <asp:Label ID="lblApprovedSumm" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label> (<asp:Label ID="lblApprovedSummWord" runat="server" Text="lblApprovedSummWord"></asp:Label>)
<br />Валюта кредита: <asp:Label ID="lblCurrencyName3" runat="server" Text="[Кредит.Валюта]"></asp:Label>			
<br />Процентная ставка (% за год): <asp:Label ID="lblRequestRate4" runat="server" Text="[Кредит.Ставка]"></asp:Label> %		
<br /><asp:Label ID="lblCommission" runat="server" Text=""></asp:Label>
<br />Эффективная % ставка (% за год): <asp:Label ID="lblRequestRateYear" runat="server" Text=""></asp:Label>	
<br />Число периодов (месяцев): <asp:Label ID="lblApprovedPeriod2" runat="server" Text="[Кредит.Срок]"></asp:Label>		
<br />Дата выдачи: <asp:Label ID="lblCreditIssueDate" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>&nbsp;г.			
<br />Дата погашения: <asp:Label ID="lblEndDate" runat="server" Text="[Кредит.ДатаОкончания]"></asp:Label>&nbsp;г. 		
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
<br />Способы погашений кредитов ОАО "Дос-Кредобанк":
<br />- терминалы "О! "QuickPay", "Dos-Credobank", "Beeline", "Pay24"
<br />- электронные кошельки "Balance KG", "MegaPay", "О! Деньги"
<br />- мобильное приложение "Элькарт Мобайл",
<br />- кассы отделений Банка.
<br />Погашение по кредиту через сотрудников банка без наличия у него ордера на прием платежей категорически запрещено. За осуществление платежей по кредиту через сотрудников банка без наличия ордера, банк ответственности не несет.
<br /><u><b>Все платежи произведенные после 15:30 часов текущего дня считаются поступившими на следующий банковский день.</b></u>
<br />
<br />
     <table class="line">
        <tr>
            <td>
                Номер кредита для погашения через терминалы/эл.кошельки - <asp:Label ID="lblCreditID2" runat="server" Text="CreditID"></asp:Label>
            </td>
        </tr>
    </table>
<br />
<br />С условиями погашения кредита и процентов ознакомлен(а) и согласен(а)	
<br />дата: ___/____/_______ подпись: ____________ ФИО:_____________________________			
<br />	        		
<br />					
      <table>
          <tr>
              <td>Банк:</td>
              <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
              <td>Заемщик:</td>
          </tr>
          <tr>
              <td>ОАО «Дос-Кредобанк»</td>
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
 
<div style="page-break-after: always">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>


        <h4 class="right">Приложение №2 к кредитному договору</h4>
        <br /><p class="centre"><b>Перечень расходов (платежей) клиентов банка и штрафных санкций.</b></p>
					
<br />
<table class="line">
<tr><td colspan="2" class="centre"><b>Расходы (платежи) клиента банка по кредиту</b></td></tr>
<tr><td>Сумма кредита/банковской гарантии</td><td><asp:Label ID="lblApprovedSumm2" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label>&nbsp<asp:Label ID="lblCurrencyName6" runat="server" Text="[Кредит.Валюта]"></asp:Label></td></tr>	
<tr><td>Номинальная процентная ставка</td><td><asp:Label ID="lblRequestRate5" runat="server" Text="[Кредит.Ставка]"></asp:Label>%</td></tr>
<tr><td>
    <asp:Label ID="lblComOpenAcc11" runat="server" Text="Комиссия за открытие и обслуживание счета по кредиту (оплаченная через партнера)"></asp:Label></td><td>
    <asp:Label ID="lblComOpenAcc12" runat="server" Text="300,00 (Триста сом, 00 тыйын)"></asp:Label></td></tr>
<tr><td>
    <asp:Label ID="lblComOpenAcc21" runat="server" Text="Комиссия за открытие и обслуживание счета по кредиту (оплаченная в банке)"></asp:Label></td><td>
        <asp:Label ID="lblComOpenAcc22" runat="server" Text="Отсутствует"></asp:Label></td></tr>
<tr><td>Комиссия за открытие и обслуживание счета по кредитному траншу</td><td>Отсутствует</td></tr>
<tr><td>Комиссия за расчетно-кассовое обслуживание по кредиту</td><td>Отсутствует</td></tr>
<tr><td>Комиссия за изменение условий договора по заявлению клиента (конвертацию валюты, пересмотр действующих графиков, замена поручителя, изменении структуры залогового обеспечения и т.д., за исключением случаев реструктуризации кредита)</td><td>1500 сом</td></tr>
<tr><td>Комиссия за резервирование денежных средств (за неиспользуемую часть кредитной линии), если договором предусмотрено безусловное обязательство банка по предоставлению кредитных средств</td><td>От 0% до 2% годовых, согласно решению Кредитного Комитета</td></tr>
<tr><td>Платежи в пользу третьих лиц (плата за услуги страхования, нотариуса, Государственной регистрационной службы при Правительстве Кыргызской Республики, Государственного агентства по земельным ресурсам при Правительстве Кыргызской Республики и др.)</td><td>Согласно тарифам третьих лиц (от 150 – 5000 сом, при этом данные расходы могут измениться в будущем)</td></tr>

<%--<tr><td>Комиссия  за рассмотрение заявления  о реструктуризации (в том числе за конвертацию валюты), пересмотр действующих графиков, замене поручителя, изменении структуры залогового обеспечения и т.д.</td><td>1500 сом</td></tr>
<tr><td>Перерасчет графика в случае частичного досрочного погашения основной суммы</td><td>Без комиссии</td></tr>
<tr><td>Платежи в пользу третьих лиц (плата за услуги страхования, нотариуса и др.)</td><td>Согласно тарифам третьих лиц</td></tr>
<tr><td>Наложение ареста на заложенное недвижимое имущество в государственных органах</td><td>1500-5000 сом</td></tr>
<tr><td>Регистрация движимого имущества в ЦЗРК</td><td>Согласно тарифам третьих лиц</td></tr>
<tr><td>Комиссия за частичное досрочное погашение кредита</td><td>Отсутсвует</td></tr>
<tr><td>Комиссия за полное  досрочное погашение кредита</td><td>Отсутсвует</td></tr>--%>

<tr><td colspan="2" class="centre"><b>Штрафные санкции и пени банка</b></td></tr>
<%--<tr><td>За просрочку оплаты платежей по основной сумме долга (в день)</td><td><asp:Label ID="lblRequestRate6" runat="server" Text="[Кредит.Ставка]"></asp:Label>%/360</td></tr>--%>
    <tr><td>За просрочку оплаты платежей по основной сумме долга (в день)</td><td>Отсутствует</td></tr>
<%--<tr><td>Пеня за просрочку оплаты начисленных процентов (в день)</td><td><asp:Label ID="lblRequestRate7" runat="server" Text="[Кредит.Ставка]"></asp:Label>%/360</td></tr>--%>
<tr><td>За просрочку оплаты платежей по процентам (в день)</td><td>Отсутствует</td></tr>
<tr><td>За просрочку более 30 календарных дней пополнения депозита, предоставленного в залог по кредиту</td><td>Отсутствует</td></tr>
<tr><td>За несвоевременный возврат пакета оригиналов документов по залоговому обеспечению (за исключением случаев несвоевременного возврата в связи с форс-мажорными обстоятельствами и другими объективными причинами, которые должны быть рассмотрены банком)</td><td>0,1% в день от суммы кредита/кредитной линии</td></tr>

<tr><td>Условия расторжения договора после получения денежных средств</td><td>2% от ссудной задолженности по кредиту на день погашения</td></tr>
<tr><td>Другие расходы (платежи): за несвоевременное выполнение условий по дополнительным соглашениям</td><td>0,1% в день от суммы кредита/кредитной линии</td></tr>
<tr><td>Другие расходы (платежи): за не целевое использование кредита</td><td>20 % от суммы, использованной не по целевому назначению</td></tr>
</table>
<br /><br />

<%--<br />ОАО &quot;Доскредобанк&quot;, по доверенности № <asp:Label ID="lblManagerDocNum6" runat="server" Text="Филиал.НомерДокумента"></asp:Label> от <asp:Label ID="lblManagerDocDate6" runat="server" Text="Филиал.Документ.ДатаВыдачи"></asp:Label>:--%>
        <br />
<%--        <asp:Label ID="lblManagerFIO6" runat="server" Text="[Система.Пользователь]"></asp:Label>	/____________/
<br />(подпись)	 <asp:Label ID="lblAgreementDate8" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>
<br /><br />Заемщик: <asp:Label ID="lblCustomerFIO11" runat="server" Text="[Клиент.ФИО]"></asp:Label>	/____________/
<br />(подпись)	 <asp:Label ID="lblAgreementDate9" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>--%>

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
<br />настоящей распиской подтверждаю, что получил(а) на руки оригинал Кредитного договора, ознакомлен(а) со всеми условиями договора, условия договора понятны, согласен(на) с условиями Кредитного договора, заключенного между мною и ОАО «Дос-Кредобанк», №<asp:Label ID="lblCreditNomer6" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label> &nbsp;от <asp:Label ID="lblAgreementDate7" runat="server" Text="[Кредит.ДатаВыдачи]"></asp:Label>.
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

<!---------------------------------------------------------------------------------------->
  <%--          <div style="page-break-after: always">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>


    <div class="right">Приложение №6 к Программе кредитования потребительский кредит «Быстрый»</div>
    <br />
    <h4 class="centre">Акт приема – передачи товара от Продавца к Клиенту 
        <br />по договору № <asp:Label ID="lblCreditNomer8" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label> ОАО "Дос-Кредобанк"
        <br /> <asp:Label ID="lblAgreementDate11" runat="server" Text="Кредит.ДатаДоговора"></asp:Label> г.</h4>
    <br /> 
            Покупатель принимает Товар следующего ассортимента и количества:
            <br />
    <br />
    <table class="line">
        <tr>
            <td>Наименование Продавец</td>
            <td><asp:Label ID="lblNameAgencyPoint" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>ФИО Продавца</td>
            <td><asp:Label ID="lblUserFIO" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td> Клиент</td>
            <td><asp:Label ID="lblCustomerFIO9" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
    <br /><br />
    <asp:GridView ID="gvProducts2" runat="server" AutoGenerateColumns="False" CssClass="line">
        <Columns>
            <asp:BoundField DataField="ProductMark" HeaderText="Марка" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductSerial" HeaderText="Серийный №" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductImei" HeaderText="imei-код" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Price" HeaderText="Цена" ItemStyle-CssClass="width165" />
        </Columns>
    </asp:GridView>
    <table class="line">
        <tr>
            <td class="width165">Итого:</td>
            <td class="width165"></td>
            <td class="width165"></td>
            <td class="width165"><asp:Label ID="lblTotalSum" runat="server"></asp:Label></td>
        </tr>
    </table>
    <br />
    <br />
    Сумма прописью: (<asp:Label ID="lblSumToWord" runat="server" Text="Label"></asp:Label>) сом Клиент подтверждает, что им получен товар для личного пользования.
    <br />
    <br /><p style="font-size:12px;">
    <br />1. Принятый Покупателем товар обладает качеством и ассортиментом, соответсвтующим требованиям Договора. Товар поставлен в установленные в Договоре сроки. Покупатель не имеет никаких претензий к принятому им товару.
    <br />2. Настоящий Акт составлен на русском языке в двух экземплярах, имеющих равную юридическую силу, по одному экземпляру для каждой из Сторон и является неотъемлемой частью указанного выше Договора между Сторонами.</p>

    <br />
    <div class="centre">Адреса и реквизиты Сторон</div>
    <br />
    <div class="centre">
    <table class="width100 line">
        <tr>
            <td>Продавец:</td>
            <td>Покупатель:</td>
        </tr>
        <tr>
            <td><asp:Label ID="lblNameAgencyPoint2" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblNameAgencyPointAddress" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblUserFIO2" runat="server" Text=""></asp:Label>
                <br />
                ____________________________<br />
                (подпись)</td>
            <td><asp:Label ID="lblCustomerFIO10" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblCustomerPassport" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lblCustomerAddress" runat="server" Text=""></asp:Label>
                <br />
                 Адрес фактического проживания:
                <br />
                <asp:Label ID="lblResCustomerAddress4" runat="server" Text=""></asp:Label>
                <br />
                ____________________________<br />
                (подпись)</td>
        </tr>
    </table>
        </div>
        </div>--%>
         
<!---------------------------------------------------------------------------------------->

<div style="page-break-after: always">
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>

<div id="pril6" runat="server" visible="false">
    <div style="page-break-after: always" >
        <br />
    <div class="right">Приложение №6 к Программе кредитования потребительский кредит «Замат»</div>
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


<table>
<tr>
    <td>Кредитный договор № <asp:Label ID="lblCreditNomer11" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label> &nbsp;от&nbsp;  <asp:Label ID="lblAgreementDate14" runat="server" Text="Кредит.ДатаДоговора"></asp:Label>&nbsp;г.</td>
    <td>
        <br />
        Наименование торговой точки______________________ <%--<asp:Label ID="lblNameAgencyPoint2_2" runat="server" Text=""></asp:Label>--%>
    <br /><br />Адрес торговой точки <%--<asp:Label ID="lblNameAgencyPointAddress2_2" runat="server" Text=""></asp:Label>--%>
        ____________________________<br />
</td>
<tr>
    <td colspan="2">
        <center><b>
            <br />
            УВЕДОМЛЕНИЕ ОБ ОДОБРЕНИИ БАНКОВСКОГО ЗАЙМА<br />
            </b></center>
    </td>
</tr>
<tr>
    <td colspan="2">
        <br />На основании Договора о сотрудничестве, данным уведомлением ОАО «Доскредобанк» подтверждает одобрение на выдачу банковского займа, с последующим перечислением суммы  банковского займа за товары:
<br /><br />Наименование товара:<br />
        <br />
        <asp:GridView ID="gvProducts3" runat="server" AutoGenerateColumns="False" CssClass="line">
        <Columns>
            <asp:BoundField DataField="ProductMark" HeaderText="Марка" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductSerial" HeaderText="Серийный №" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="ProductImei" HeaderText="imei-код" ItemStyle-CssClass="width165" />
            <asp:BoundField DataField="Price" HeaderText="Цена" ItemStyle-CssClass="width165" />
        </Columns>
    </asp:GridView>


<br />Код кредитного продукта: <asp:Label ID="lblCodeProduct" runat="server" Text="CodeProduct"></asp:Label>
<br /><br />Название кредитного продукта: <asp:Label ID="lblNameProduct" runat="server" Text="NameProduct"></asp:Label>
<br /><br />Сумма банковского займа <asp:Label ID="lblApprovedSumm3" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label> (сумма прописью) <asp:Label ID="lblApprovedSummWord3" runat="server" Text="lblApprovedSummWord"></asp:Label>
<br /><br />Сумма первоначального взноса к оплате <asp:Label ID="lblSellerAccount" runat="server" Text=" в кассу Продавца "></asp:Label> <asp:Label ID="lblAmountDownPayment" runat="server" Text="[Первоначальный взнос]"></asp:Label> &nbsp;(сумма прописью) <asp:Label ID="lblAmountDownPaymentSumWord" runat="server" Text="[Первоначальный взнос]"></asp:Label>
<br /><br />Сумма расходов клиента по оформлению кредита в кассу Продавца ______________________________
<br /><br />Сумма к оплате Банком на расчетный счет Продавца перечислением______________________________<br /><br />Полученный (-е)&nbsp; <asp:Label ID="lblCustomerFIO15" runat="server" Text=""></asp:Label> &nbsp;(<asp:Label ID="lblCustomerPassport2" runat="server" Text=""></asp:Label> )
        <br />
    </td>
</tr>
<tr>
    <td colspan="2">
<br />
        <br />
        Кредитный специалист:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <%--<asp:Label ID="lblUserFIO3" runat="server" Text=""></asp:Label>--%>
<asp:Label ID="lblCustomerFIO16" runat="server" Text=""></asp:Label><br />
        <br />
        _____________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; __________________________                                                                                                                        
<br />&nbsp;&nbsp;&nbsp; (подпись и печать)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (подпись)                 
<br />
        <br />
Дата:___________

        <br />

    </td>
</tr>

</table>


        <br />
        <br />


        Товар выдал&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Товар получил
        <br />
        __________________________&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ______________________________
        <br />
        (подпись Продавца)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (<asp:Label ID="lblCustomerFIO17" runat="server" Text=""></asp:Label>, подпись Клиента/Заемщика)






<!---------------------------------------------------------------------------------------->
<div style="page-break-after: always">
    <br />
    <br />
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />

</div>

<!---------------------------------------------------------------------------------------->
    
    <asp:Panel ID="guarant" runat="server">
        <h4 class="centre">ДОГОВОР ПОРУЧИТЕЛЬСТВА №<asp:Label ID="lblCreditNomer9" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label></h4>
       <b><asp:Label ID="lblCity3" runat="server" Text="Город"></asp:Label></b> 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
       <b><asp:Label ID="lblAgreementDate12" runat="server" Text="Кредит.ДатаДоговора"></asp:Label>&nbsp;г.</b>
<br />
<br />
<b>Открытое Акционерное Общество «Дос-Кредобанк»</b>, далее именуемое <b>«Банк»</b>, в лице <b><asp:Label ID="lblManagerFIO7" runat="server" Text="[Агент.Руководитель.ФИО]"></asp:Label></b>, действующего на основании Доверенности № <asp:Label ID="lblManagerDocNum7" runat="server" Text="НомерДоверенности"></asp:Label> &nbsp; от  <asp:Label ID="lblManagerDocDate7" runat="server" Text="Дата"></asp:Label> г. с одной стороны, 
    <br />
    гр. <b><asp:Label ID="lblCustomerFIO12" runat="server" Text="[Клиент.ФИО]"></asp:Label></b>, паспорт <asp:Label ID="lblDocumentSeries5" runat="server" Text="[Клиент.Документ.Серия]"></asp:Label> <asp:Label ID="lblDocumentNo5" runat="server" Text="[Клиент.Документ.Номер]"></asp:Label>, выдан <asp:Label ID="lblIssueAuthority5" runat="server" Text="[Клиент.Документ.Выдан]"></asp:Label>, от <asp:Label ID="lblIssueDate5" runat="server" Text="[Клиент.Документ.ДатаВыдачи]"></asp:Label>&nbsp;г., прописанный(-ая) по адресу: <asp:Label ID="lblRegCustomerAddress5" runat="server" Text="[Клиент.Адрес.Прописка]"></asp:Label>, в дальнейшем именуемый(-ая/-ое) <b>«Заемщик»</b>, с другой  стороны, и 
	<br />
    гр. <b><asp:Label ID="lblGuaranterFIO" runat="server" Text="[Поручитель.ФИО]"></asp:Label></b>, паспорт <asp:Label ID="lblDocumentSeriesGua" runat="server" Text="[Поручитель.Документ.Серия]"></asp:Label> <asp:Label ID="lblDocumentNoGua" runat="server" Text="[Поручитель.Документ.Номер]"></asp:Label>, выдан <asp:Label ID="lblIssueAuthorityGua" runat="server" Text="[Поручитель.Документ.Выдан]"></asp:Label>, от <asp:Label ID="lblIssueDateGua" runat="server" Text="[Поручитель.Документ.ДатаВыдачи]"></asp:Label>&nbsp;г., прописанный(-ая) по адресу: <asp:Label ID="lblRegCustomerAddressGua" runat="server" Text="[Поручитель.Адрес.Прописка]"></asp:Label>  (для физических лиц) в дальнейшем именуемый(-ая/-ое) <b>«Поручитель»</b>, с третьей стороны, по отдельности именуемые «Сторона», вместе именуемые <b>«Стороны»</b>, заключили настоящий договор о нижеследующем:
        <br />
    <br />
    <div class="centre"><b>1. ПРЕДМЕТ ДОГОВОРА<br /> </b></div>
    1.1.  Поскольку между Банком и Заемщиком заключен Кредитный договор №<asp:Label ID="lblCreditNomer10" runat="server" Text="[Кредит.НомерДоговора]"></asp:Label> &nbsp;от  <asp:Label ID="lblAgreementDate13" runat="server" Text="Кредит.ДатаДоговора"></asp:Label>, далее именуемый «Договор», согласно которому Банк предоставил кредит в сумме 
<b><asp:Label ID="lblRequestSumm5" runat="server" Text="[Кредит.ОбщаяСумма]"></asp:Label>&nbsp;(<asp:Label ID="lblRequestSummWord5" runat="server" Text="[Кредит.ОбщаяСумма.Пропись]"></asp:Label>)</b> <asp:Label ID="lblCurrencyName7" runat="server" Text="[Кредит.Валюта]"></asp:Label>, сроком на <asp:Label ID="lblApprovedPeriod4" runat="server" Text="[Кредит.Срок]"></asp:Label> &nbsp;месяц (-а/-ев), с взиманием за предоставленный кредит <asp:Label ID="lblRequestRate9" runat="server" Text="[Кредит.Ставка]"></asp:Label>% годовых, с целью обеспечения обязательства, Поручитель обязуется перед Банком, являющимся Кредитором Заемщика, полностью отвечать за исполнение Заемщиком его обязательств по Договору.
    <br />
    1.2.  При этом Поручитель берет на себя обязательства, как по основной сумме кредита, так и по процентам, подлежащим выплате Банку согласно Договору, а также по выплате Банку пени и штрафов, если такие будут иметь место, и за другие платежи, которые могут возникнуть в связи с невыполнением Заемщиком своих обязательств по Договору. Стороны согласны, что нет необходимости в дальнейшем в настоящем договоре перечислять все обязательства, которые могут возникнуть из условий Договора, и подлежат выплате Поручителем, и что в последующем в настоящем договоре они будут именоваться «Обязательства».
        <br />
    <br />
    <div class="centre"><b>2. ПРАВА И ОБЯЗАННОСТИ СТОРОН<br /> </b></div>
    2.1.  Поручитель обязуется полностью отвечать своим имуществом и денежными средствами перед Банком за исполнение обязательств Заемщиком по условиям и в объеме, указанном в Договоре.
    <br />
    2.2.  Поручитель безоговорочно обязуется произвести полную и своевременную  выплату всех обязательств Заемщика, возникших на основании Договора, в случаях, предусмотренных в п. 
    <br />
    2.3. настоящего договора. Такие выплаты должны осуществляться Поручителем в той же валюте и в том же порядке, в котором это предусмотрено Договором (банковские счета, метод конвертации валюты и т.п.) как при осуществлении выплат Заемщиком.
    <br />
    2.3.  Основаниями ответственности Поручителя, в частности, являются:
-         невозвращение Заемщиком кредита в обусловленный Договором срок;
-         неуплата Заемщиком процентов по кредиту в установленный срок;
-         не целевое использование Заемщиком полученного кредита;
-         нарушение Заемщиком любого из условий Договора.
    <br />
    2.4.  Заемщик обязуется немедленно извещать Поручителя обо всех допущенных им нарушениях Договора, в том числе о просрочке уплаты процентов, возврата суммы основного долга и о любых нарушениях, а также обо всех других обстоятельствах, влияющих на исполнение Заемщиком своих обязательств перед Банком.
    <br />
    2.5.  Заемщик обязан немедленно письменно извещать Поручителя о полном или частичном исполнении обязательств по указанному Договору, в том числе об уплате процентов, штрафных санкций и возврате суммы кредита с предоставлением соответствующих документов.
    <br />
    2.6.  В случае просрочки исполнения Заемщиком обязательств перед Банком, Банк вправе по своему выбору потребовать исполнения обязательств от Заемщика или от Поручителя, либо осуществить в установленном законом порядке и/или в порядке, предусмотренном настоящим договором принудительное взыскание сумм неисполненных обязательств с Поручителя или Заемщика.
    <br />
    2.7.  Банк вправе без дополнительного согласия или уведомления Поручителя, в случае наступления оснований, предусмотренных пунктом 2.3. настоящего договора, в безакцептном порядке удержать сумму задолженности Заемщика перед Банком с любого счета Поручителя, открытого в ОАО «Дос-Кредобанк».
    <br />
    2.8.  Поручитель не вправе выдвигать против требований Банка каких-либо возражений, которые мог бы выдвинуть Банку Заемщик (Пункт 1 Статьи 345 Гражданского Кодекса). Поручитель не вправе отказаться от своих обязательств, даже если Заемщик отказался от своих обязательств или признал свой долг.
    <br />
    2.9.  Поручитель дает безусловное согласие на предоставление Банком, в случае нарушения условий настоящего договора Поручителем, любой имеющейся информации о Поручителе и условиях настоящего договора третьим лицам для последующего использования данной информации в целях взыскания задолженности.
    <br />
    2.10. В целях защиты общих интересов финансово-кредитных учреждений участников кредитного Информационного Бюро (Бюро), последние сохраняют за собой право обмениваться  информацией о кредитах  (для банков и других  финансово-кредитных учреждений, имеющих  лицензию на осуществление  банковской деятельности) или займах (для финансово-кредитных учреждений, не имеющих  лицензию) с другими финансовыми институтами посредством предоставления  соответствующей информации Бюро в установленном порядке. Вышеуказанный обмен данными осуществляется с целью избежание  чрезмерных  обязательств, безнадежных долгов, мошенничества, отмывания денег и предотвращения  практики  погашения заемщиком своих  денежных обязательств перед одним  финансово-кредитным учреждением  за счет средств  другого финансового учреждения, а также для  обеспечения  принятия  обоснованных решений  относительно выдаваемых кредитов (для финансово-кредитных учреждений), займов (для финансово-кредитных учреждений, не имеющих лицензию). Никакие другие  коммерческие или профессиональные данные клиентов  не могут  быть  предоставлены Бюро и /или его членам. Предоставление в установленном порядке  информации Кредитному Бюро не может рассматриваться  как нарушение Закона Кыргызской Республики «О банковской тайне».
        <br />
    <br />
    <div class="centre"><b>3. ОТВЕТСТВЕННОСТЬ СТОРОН<br /> </b></div>
    3.1.  Поручитель несет полную солидарную ответственность с Заемщиком, но не субсидиарную (Пункты 1 Статьи 343-344  Гражданского Кодекса) и несет ответственность в том же объеме, как и Заемщик (Пункт 2 Статьи 344 Гражданского Кодекса).
    <br />
    3.2.  Поручитель признает, что он берет на себя ответственность, как только возникнет какое либо нарушение (неисполнение обязательств) со стороны Заемщика в отношении его обязательств, и эта ответственность не зависит от того: 
-         сообщалось ли Заемщиком Поручителю о случаях нарушения Договора;
-         предпринимает ли Банк, какие бы то ни было действия против Заемщика или любого другого лица, с целью принудительного исполнения данными лицами обязательств Заемщика по Договору.
    <br />
    3.3.  Поручитель несет ответственность за немедленное исполнение своих обязательств в соответствии с настоящим договором по первому требованию банка.
        <br />
    <br />
    <div class="centre"><b>4. СРОКИ ДЕЙСТВИЯ ДОГОВОРА И ПОРУЧИТЕЛЬСТВА<br /> </b></div>
    4.1.  Настоящий договор вступает в силу с момента его подписания. Настоящий договор остается в силе до момента выполнения Поручителем всех обязательств, предусмотренных настоящим договором обязательства Поручителя, возникающие на основании  настоящего договора, являются абсолютными и безусловными.
    <br />
    4.2.  Поручительство прекращается:
-         в случае полного исполнения Заемщиком обязательств по Договору;
-         при переводе долга на другое лицо, если Поручитель не дал Кредитору согласия отвечать за нового должника;
-         в случае принятия Банком отступного;
-         в иных, предусмотренных законом, случаях.<br />
    <br />
    <div class="centre"><b>5. ПРОЧИЕ УСЛОВИЯ<br /> </b></div>
    5.1.  Настоящий договор будет являться обязательным для исполнения Поручителем и Заемщиком, а в случае их ликвидации (смерти)  – для правопреемников (наследников) их имущества.
    <br />
    5.2.  В случае ликвидации или реорганизации Банка, все его права и обязанности по настоящему договору переходят к его правопреемнику.
    <br />
    5.3.  Все извещения и весь обмен информацией будет осуществляться по адресам, предусмотренным настоящим договором и Кредитным договором.
    <br />
    5.4.  Настоящий договор подписан в трех идентичных экземплярах на русском языке, по одному экземпляру для каждой из сторон. 
    <br />
    5.5.  В случаях, не предусмотренных настоящим договором, стороны руководствуются законодательством Кыргызской Республики.
    <br />
    5.6.  Споры, которые могут возникнуть между сторонами договора, будут решаться в порядке, предусмотренном законодательством Кыргызской Республики.
 
    <br />
 
        <br />
        <br />
 
    <br />
    <div class="centre"><b>6. ЮРИДИЧЕСКИЕ АДРЕСА И РЕКВИЗИТЫ СТОРОН</b><br />
    </div>
    <br />
<table class="rekvisity">
    <tr>
        <td width="240"><b>Кредитор:</b></td>
        <td width="240"><b>Заемщик:</b></td>
        <td width="240"><b>Поручитель:</b></td>
    </tr>
    <tr>
        <td><b>ОАО «Дос-Кредобанк»</b></td>
        <td><b><asp:Label ID="lblCustomerFIO13" runat="server" Text="[Клиент.ФИО]"></asp:Label></b></td>
        <td><b><asp:Label ID="lblGuaranterFIO2" runat="server" Text="[Поручитель.ФИО]"></asp:Label></b></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCompanyName3" runat="server" Text="[Филиал]"></asp:Label>
            <br />
            <asp:Label ID="lblCompanyAddress3" runat="server" Text="[Филиал.Адрес]"></asp:Label>
        </td>
        <td>Паспорт серии &nbsp; <asp:Label ID="lblDocumentSeries6" runat="server" Text="[Клиент.Документ.Серия]"></asp:Label> &nbsp; <asp:Label ID="lblDocumentNo6" runat="server" Text="[Клиент.Документ.Номер]"></asp:Label> &nbsp; выдан &nbsp; <asp:Label ID="lblIssueAuthority6" runat="server" Text="[Клиент.Документ.Выдан]"></asp:Label> &nbsp; от &nbsp; <asp:Label ID="lblIssueDate6" runat="server" Text="[Клиент.Документ.ДатаВыдачи]"></asp:Label>&nbsp;г.</td>
        <td>Паспорт серии &nbsp; <asp:Label ID="lblDocumentSeriesGua2" runat="server" Text="[Поручитель.Документ.Серия]"></asp:Label> &nbsp; <asp:Label ID="lblDocumentNoGua2" runat="server" Text="[Поручитель.Документ.Номер]"></asp:Label> &nbsp; выдан &nbsp; <asp:Label ID="lblIssueAuthorityGua2" runat="server" Text="[Поручитель.Документ.Выдан]"></asp:Label> &nbsp; от &nbsp; <asp:Label ID="lblIssueDateGua2" runat="server" Text="[Поручитель.Документ.ДатаВыдачи]"></asp:Label>&nbsp;г.</td>
    </tr>
    <tr>
        <td><%--К/счет <asp:Label ID="lblaccountNBKR" runat="server" Text="[Филиал.К/счет] в НБКР"></asp:Label>--%></td>
        <td>ИНН <asp:Label ID="lblIdentificationNumber2" runat="server" Text="[Клиент.ИНН]" Visible="false"></asp:Label></td>
        <td>ИНН <asp:Label ID="lblIdentificationNumberGua" runat="server" Text="[Поручитель.ИНН]" Visible="false"></asp:Label></td>
    </tr>
    <tr>
        <td><%--БИК <asp:Label ID="lblCompanyBIK" runat="server" Text="[Филиал.БИК]"></asp:Label>--%></td>
        <td>Адрес по прописке:</td>
        <td>Адрес по прописке:</td>
    </tr>
    <tr>
        <td>ИНН <asp:Label ID="lblCompanyINN3" runat="server" Text="[Филиал.ИНН]"></asp:Label></td>
        <td><asp:Label ID="lblResCustomerAddress5" runat="server" Text="[Клиент.Адрес.Прописка]"></asp:Label></td>
        <td><asp:Label ID="lblRegCustomerAddressGua2" runat="server" Text="[Поручитель.Адрес.Прописка]"></asp:Label></td>
    </tr>
    <tr>
        <td>Код ОКПО <asp:Label ID="lblCompanyOKPO3" runat="server" Text="[Филиал.ОКПО]"></asp:Label></td>
        <td>Адрес фактического проживания:</td>
        <td>Адрес фактического проживания:</td>
    </tr>
    <tr>
        <td>По доверенности<%--<asp:Label ID="lblCompanyWorkPosition2" runat="server" Text="[Филиал.ОснРуководитель.Должность]"></asp:Label>--%></td>
        <td><asp:Label ID="lblRegCustomerAddress6" runat="server" Text="[Клиент.Адрес.Фактический]"></asp:Label></td>
        <td><asp:Label ID="lblResCustomerAddressGua" runat="server" Text="[Клиент.Адрес.Фактический]"></asp:Label></td>
    </tr>
    <tr>
        <td class="auto-style1">_________________<br />
            <asp:Label ID="lblManagerFIO8" runat="server" Text="ФИО Агента"></asp:Label><%--<asp:Label ID="lblCompanyCustomerManagersFIO" runat="server" Text="[Филиал.ОснРуководитель]"></asp:Label>--%></td>
        <td class="auto-style1">(м.п.)	_________________<br /><asp:Label ID="lblCustomerFIO14" runat="server" Text="[Клиент.ФИО]"></asp:Label></td>
        <td class="auto-style1">(м.п.)	_________________<br /><asp:Label ID="lblGuaranterFIO3" runat="server" Text="[Поручитель.ФИО]"></asp:Label></td>
    </tr>
</table>
<br />
         

    </asp:Panel>
<!---------------------------------------------------------------------------------------->
<!---------------------------------------------------------------------------------------->
       <div style="page-break-after: always">
        <br />


<div id="maritalStatus0" runat="server">
    
    <br />
    <h4 class="centre">Заявление о семейном положении Заемщика</h4>
    <br />
    <br />
  

	Я, <asp:Label ID="lblCustomerFIO18" runat="server" Text=""></asp:Label> паспортные данные: 
    <br />
    <asp:Label ID="lblCustomerPassport3" runat="server" Text=""></asp:Label> заявляю, 
    <br />
    что на момент получения кредита в ОАО «Дос-Кредобанк» в браке не состою.
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
  

	Я, <asp:Label ID="lblCustomerFIO19" runat="server" Text=""></asp:Label>, 
        <asp:Label ID="lblDateOfBirth" runat="server" Text="DateOfBirth"></asp:Label> г.р., паспортные данные: <asp:Label ID="lblCustomerPassport4" runat="server" Text=""></asp:Label>, настоящим обязуюсь довести сведения до своего (ей)
                                                            <br />
           <br />
           супруга(ги)&nbsp; ФИО_____________________________________________________________________, 
информацию о получении кредита и предоставления залога (движимое имущество) в ОАО «Дос-Кредобанк» на предусмотренных условиях: 
<br />
           в сумме <asp:Label ID="lblRequestSumm6" runat="server" Text="lblRequestSumm"></asp:Label> сом под <asp:Label ID="lblRequestRate10" runat="server" Text="Rate"></asp:Label>% годовых.<br />
           <br />
           <br />

           <br />
           Дата:  <asp:Label ID="lblAgreementDate11" runat="server" Text="Дата"></asp:Label>

           <br />

           <br />
           Подпись: ____________________

        <br />

        </div> <!--maritalStatus2 -->
 </div>
 
     
 <!----------------------------------------------------------------------------------->




</div>
</div>
    </div>

<%--</asp:Panel>--%>





   <script type="text/javascript">
        $(document).ready(function () {
            $("input[type='button']").click(function () {
                generateBarcode();
    });

            function generateBarcode() {
                var value = $("#barcodeValue").text();
                var value2 = $("#barcodeValue2").text();
                var btype = $("input[name=btype]:checked").val();
                var renderer = "css";
                    var isSulpak = $("#lblIsSulpak").text();
                if (isSulpak == "1")
                
                {
                var quietZone = false;
                if ($("#quietzone").is(':checked') || $("#quietzone").attr('checked')) {
                    quietZone = true;
                }

                var settings = {
                    output: renderer,
                    bgColor: "#FFFFFF",
                    color: "#000000",
                    barWidth: 1,
                    barHeight: 50,
                    moduleSize: $("#moduleSize").val(),
                    posX: $("#posX").val(),
                    posY: $("#posY").val(),
                    addQuietZone: $("#quietZoneSize").val()
                };
                if ($("#rectangular").is(':checked') || $("#rectangular").attr('checked')) {
                    value = {
                        code: value,
                        rect: true
                    };
                    value2 = {
                        code: value2,
                        rect: true
                    };
                }
                if (renderer == 'canvas') {
                    clearCanvas();
                    $("#canvasTarget").hide();
                    $("#barcodeTarget").hide();
                    $("#barcodeTarget2").hide();
                    //$("#canvasTarget").show().barcode(value, 'code39', settings);
                    //$("#canvasTarget2").show().barcode(value2, 'code39', settings);
                } else {
                    $("#canvasTarget").hide();
                    $("#barcodeTarget").html("").show().barcode(value, 'code39', settings);
                    $("#barcodeTarget2").html("").show().barcode(value2, 'code39', settings);
                }
            }
    }

    function showConfig1D() {
        $('.config .barcode1D').show();
        $('.config .barcode2D').hide();
    }

    function showConfig2D() {
        $('.config .barcode1D').hide();
        $('.config .barcode2D').show();
    }

    function clearCanvas() {
        var canvas = $('#canvasTarget').get(0);
        var ctx = canvas.getContext('2d');
        ctx.lineWidth = 1;
        ctx.lineCap = 'butt';
        ctx.fillStyle = '#FFFFFF';
        ctx.strokeStyle = '#000000';
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctx.strokeRect(0, 0, canvas.width, canvas.height);
    }

    $(function () {
        $('input[name=btype]').click(function () {
            if ($(this).attr('id') == 'datamatrix') showConfig2D();
            else showConfig1D();
        });
        $('input[name=renderer]').click(function () {
            if ($(this).attr('id') == 'canvas') $('#miscCanvas').show();
            else $('#miscCanvas').hide();
        });
        generateBarcode();
    });
});
    </script>
    </form>
</body>
</html>
