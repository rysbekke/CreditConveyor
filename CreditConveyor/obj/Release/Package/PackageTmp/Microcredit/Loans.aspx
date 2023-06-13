<%@ Page Title="МК кредиты" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Loans.aspx.cs" Inherits="СreditСonveyor.Microcredit.Loans" Debug="true" Async="true" AsyncTimeout="60" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <%-- <link href="/css/bootstrap.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="/css/chosen.css">
    <script src="/js/webcam.js"></script>
<script src="/js/jquery.js"></script>
<script src="/js/jquery-ui.js"></script>

<script src="/js/chosen.jquery.min.js"></script>
<script src="/js/chosen.init.js"></script>
<script src="/js/datepicker-ru.js"></script>

    <style>
#my_camera{
 width: 320px;
 height: 240px;
 border: 1px solid black;
}
#tbNoteCancelReqExp, #tbNoteCancelReq {  max-width: 568px; max-height:120px;
          min-width: 568px; min-height:120px;
              
}

       div.search{ float:left}
       div.search span{ width:200px;}
        div.status {
            clear: both;
        }
        .auto-style1 {
            height: 20px;
        }

        div.clear {
               clear: both;
        }
    </style>

<%--        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>--%>
<div>
    <%--        </ContentTemplate>
                        </asp:UpdatePanel>--%>
    <h4>МК кредиты</h4>
<asp:Panel ID="pnlUser" runat="server">
	<p class="right" style="font-size: 14px;">
        <asp:Label ID="lblGroup" runat="server" Text="Group" style="font-size:14px"></asp:Label>
		<asp:Label ID="lblUserName" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbLogout" runat="server" OnClick="lbLogout_Click" Visible="false">Logout</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbSettings" runat="server" OnClick="lbSettings_Click">Настройки</asp:LinkButton>
	</p>
	
    
    <asp:Panel ID="pnlSettings" runat="server" Visible="false">  
        <table>
            <tr>
                <td>Старый пароль</td>
                <td><asp:TextBox ID="tbOldPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfOldPassword" runat="server" ErrorMessage="Ошибка!" ValidationGroup="changePassowrd" ControlToValidate="tbOldPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Новый пароль</td>
                <td><asp:TextBox ID="tbNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfNewPassword" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbNewPassword" ValidationGroup="changePassowrd"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ErrorMessage="Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов" ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.Ошибка!\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" ValidationGroup="changePassowrd" ControlToValidate="tbNewPassword"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Подтверждение</td>
                <td><asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfConfirmPassword" runat="server" ErrorMessage="Ошибка!" ValidationGroup="changePassowrd" ControlToValidate="tbConfirmPassword"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revConfirmPassword" runat="server" ErrorMessage="Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов" ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" ValidationGroup="changePassowrd" ControlToValidate="tbConfirmPassword"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblMsgPassword" runat="server" Text="Неправильный пароль" Visible="false"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Button ID="btnSavePassword" runat="server" Text="Сохранить" class="btn btn-primary" OnClick="btnSavePassword_Click" ValidationGroup="changePassowrd" /></td>
                <td><asp:Button ID="btnCancelPassword" runat="server" Text="Закрыть" class="btn btn-primary" OnClick="btnCancelPassword_Click" /></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="2"> 
                   <asp:Panel ID="pnlGroup" runat="server">
                    <table>
                        <tr>
                            <td>Группа&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td><asp:DropDownList ID="ddlGroup" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Магазин</td>
                            <td><asp:DropDownList ID="ddlShop" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblSHop" runat="server" Font-Size="8pt"></asp:Label></td>
                            <td></td>
                        </tr>
                        <tr>
                             <td><asp:Button ID="btnSaveGroup" runat="server" Text="Сохранить" class="btn btn-primary" OnClick="btnSaveGroup_Click" /></td>
                             <td><asp:Button ID="btnCancelSaveGroup" runat="server" Text="Закрыть" class="btn btn-primary" OnClick="btnCancelSaveGroup_Click" /></td>
                             <td></td>
                        </tr>
                    </table>
                </asp:Panel>
                </td>
                <td></td>
                <td></td>
                
            </tr>
            

        </table>
    </asp:Panel> 
    
        
    
        
    
	<asp:Panel ID="pnlCredit" runat="server" CssClass="panels">



			
				
                <div class="search"><p>№ заявки</p><asp:TextBox ID="tbRequestID" runat="server" class="form-control"></asp:TextBox>&nbsp;</div>
				<div class="search"><p>ИНН</p><asp:TextBox ID="tbSearchRequestINN" runat="server" class="form-control"></asp:TextBox>&nbsp;</div>
                <div class="search"><p>CreditID</p><asp:TextBox ID="tbCreditID" runat="server" class="form-control"></asp:TextBox>&nbsp;</div>
				<div class="search"><p>Фамилия</p><asp:TextBox ID="tbSearchRequestSurname" runat="server" class="form-control"></asp:TextBox>&nbsp;</div>
				<div class="search"><p>Имя</p><asp:TextBox ID="tbSearchRequestName" runat="server" class="form-control"></asp:TextBox>&nbsp;</div>
                <div class="search"><p>Дата</p><asp:TextBox ID="tbDate1b" runat="server" ClientIDMode="Static" class="form-control"></asp:TextBox>&nbsp;</div>
				<div class="search"><p>&nbsp</p><asp:TextBox ID="tbDate2b" runat="server" ClientIDMode="Static" Visible="False" class="form-control"></asp:TextBox>&nbsp;</div>
				<div class="search"><p>&nbsp</p><asp:Button ID="btnSearchRequest" runat="server" class="btn btn-primary" OnClick="btnSearchRequest_Click" Text="Найти" ValidationGroup="SearchRequest" /></div>
			
       
		<div class="clear"></div>		
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                        <div class="clear chkbxhead"><p style="text-align:center"> <b>Статусы заявок</b></p></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus1" runat="server" Text="Новая заявка" Checked="true" /></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus2" runat="server" Text="Исправить" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus3" runat="server" Text="Исправлено" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus4" runat="server" Text="Подтверждено" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus5" runat="server" Text="Утверждено" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus6" runat="server" Text="Выдано" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus7" runat="server" Text="Принято" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus8" runat="server" Text="Не подтверждено" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus9" runat="server" Text="Отменено" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus10" runat="server" Text="Отказано" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus11" runat="server" Text="В обработке" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus12" runat="server" Text="На выдаче" Checked="true"/></div>
                        <div class="left"><asp:CheckBox ID="chkbxStatus13" runat="server" Text="К подписи" Checked="true"/></div>
                        <div class="clear"><asp:CheckBox ID="chkbxSelectAll" runat="server" AutoPostBack="True" Checked="True" Text="Выделить все" OnCheckedChanged="chkbxSelectAll_CheckedChanged" /></div>
                </ContentTemplate>
              </asp:UpdatePanel>
        </div>
        <div>
            <div class="clear chkbxhead"><p style="text-align:center"><b>Вид работы</b></p></div>
             <%--<asp:CheckBoxList ID="chkbxlistKindActivity" runat="server" RepeatDirection="Horizontal" Visible="False">
                        <asp:ListItem Selected="True">Работа по найму</asp:ListItem>
                        <asp:ListItem Selected="True">Бизнес</asp:ListItem>
                        <asp:ListItem Selected="True">Агро</asp:ListItem>
                    </asp:CheckBoxList>--%>
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>--%>
                    <div class="left"><asp:CheckBox ID="chkbxKindActivity1" Text="Работа по найму" runat="server" class="form-check-input" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxKindActivity2" Text="Бизнес" runat="server" class="form-check-input" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxKindActivity3" Text="Агро" runat="server" class="form-check-input" Checked="true"/></div>
                    <div class="clear"></div>
             <%--       </ContentTemplate>
              </asp:UpdatePanel>--%>
        </div>
        <div>
            <%--<asp:CheckBoxList ID="chkbxGroup" runat="server" RepeatDirection="Horizontal" Visible="False">
                        <asp:ListItem Selected="True">Замат</asp:ListItem>
                        <asp:ListItem Selected="True">Нано</asp:ListItem>
                    </asp:CheckBoxList>--%>
      <%--      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>--%>
                <div class="left"><asp:CheckBox ID="chkbxGroup1" runat="server" Text="Замат" Visible="false" Checked="true"/></div>
                <div class="left"><asp:CheckBox ID="chkbxGroup2" runat="server" Text="Нано"  Visible="false" Checked="true"/></div>
   <%--             </ContentTemplate>
              </asp:UpdatePanel>--%>
        </div>
        <div>
            <div id="divChkbxOffice" runat="server">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="clear chkbxhead"><p style="text-align:center"><b>Офисы</b></p></div>
                    <%--<asp:CheckBoxList ID="chkbxOffice" runat="server" RepeatDirection="Horizontal" Visible="true">
                        <asp:ListItem Selected="True" Value="1027">ЦФ</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1022">Берекет</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1134">Карабалта</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1133">Талас</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1016">Балыкчы</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1017">Ош</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1018">Нарын</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1019">Токмок</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1020">Каракол</asp:ListItem> 
                        <asp:ListItem Selected="True" Value="1023">Жалалбад</asp:ListItem> 
                    </asp:CheckBoxList>--%>
                    
                    <div class="left"><asp:CheckBox ID="chkbxOffice1" runat="server" Text="ЦФ" Value="1027" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice2" runat="server" Text="Берекет" Value="1022" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice3" runat="server" Text="Карабалта" Value="1134" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice4" runat="server" Text="Талас" Value="1133" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice5" runat="server" Text="Балыкчы" Value="1016" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice6" runat="server" Text="Ош" Value="1017" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice7" runat="server" Text="Нарын" Value="1018" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice8" runat="server" Text="Токмок" Value="1019" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice9" runat="server" Text="Каракол" Value="1020" Checked="true"/></div>
                    <div class="left"><asp:CheckBox ID="chkbxOffice10" runat="server" Text="Жалалбад" Value="1023" Checked="true"/></div>
                    
                    <div class="clear"><asp:CheckBox ID="chkbxSelectAllOffice" runat="server" AutoPostBack="True" Checked="True" Text="Выделить все" OnCheckedChanged="chkbxSelectAllOffice_CheckedChanged"  /></div>
                    </ContentTemplate>
              </asp:UpdatePanel>
        </div>
        </div>
        
        <br />
        <div class="row">
            <div class="col-lg-12 ">
                <div class="table-responsive">
                    <asp:GridView ID="gvRequests" runat="server" ShowHeaderWhenEmpty="True" OnRowCommand="gvRequests_RowCommand" AutoGenerateColumns="False" OnRowDataBound="gvRequests_RowDataBound" CssClass="gvRequests" AllowPaging="True" OnPageIndexChanging="gvRequests_PageIndexChanging" PageSize="50">
                        <Columns>
                            <asp:TemplateField HeaderText="Выбрать">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lbSelectRequest" runat="server" CommandArgument='<%# Eval("RequestID") %>' CommandName="Sel">Выбрать</asp:LinkButton>--%>
                                    <a href='/Microcredit/Loan?RequestID=<%# Eval("RequestID") %>' >Выбрать</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RequestID" HeaderText="Номер" />
                            <asp:BoundField DataField="CreditID" HeaderText="CreditID" />
                            <asp:BoundField DataField="RequestStatus" HeaderText="Статус заявки" />
                            <asp:BoundField DataField="Surname" HeaderText="Фамилия" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Имя" />
                            <asp:BoundField DataField="ShortName" HeaderText="Филиал" />
							<asp:BoundField DataField="OfficeShortName" HeaderText="Офис" />
                            <asp:BoundField DataField="GroupName" HeaderText="Группа" />
                            <asp:BoundField DataField="OrgName" HeaderText="Орг" />
                            <asp:BoundField DataField="IdentificationNumber" HeaderText="ИНН" />
<%--                            <asp:BoundField DataField="CreditPurpose" HeaderText="Назначение кредита" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" /> --%>
                            <asp:BoundField DataField="ProductPrice" HeaderText="Цена" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="AmountDownPayment" HeaderText="Взнос" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="RequestSumm" HeaderText="Кредит" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="RequestDate" HeaderText="Дата заявки" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="RequestRate" HeaderText="Процент" />
                            
                            <asp:BoundField DataField="StatusOB" HeaderText="Статус в ОБ" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                    </div>
            </div>
        </div>

	
       <%-- <asp:HiddenField ID="hfRequestsRowID" runat="server" />
        <asp:Label ID="lblError" runat="server" Text="" Font-Size="Larger" ForeColor="Red"></asp:Label>--%>
		<br />
        <asp:HiddenField ID="hfUserID" runat="server" />
            <asp:HiddenField ID="hfRequestID" runat="server" />
    <asp:HiddenField ID="hfRequestsRowID" runat="server" />
    <asp:Button ID="btnNewRequest" runat="server" Text="Новая заявка" class="btn btn-primary" OnClick="btnNewRequest_Click" />
    <asp:Button ID="btnForPeriod" runat="server" Text="Отчет" Visible="False" OnClientClick="openNewWin();" class="btn btn-primary" OnClick="btnForPeriod_Click" />
    <asp:Button ID="btnForPeriodWithHistory" runat="server" Text="Отчет 2" Visible="False" OnClientClick="openNewWin();" class="btn btn-primary" OnClick="btnForPeriodWithHistory_Click"  />
    <asp:Button ID="btnForPeriodWithProducts" runat="server" Text="Отчет 3" Visible="False" OnClientClick="openNewWin();" class="btn btn-primary" OnClick="btnForPeriodWithProducts_Click" />



</asp:Panel>
</asp:Panel>

    <%--<ul class="tabs">
        <li><a href="#main" data-tab='main' class="selected">Main</a></li>
        <li><a href="#remediation" data-tab='remediation'>Remediation</a></li>
        <li><a href="#summary" data-tab='summary'>Summary</a></li>
      </ul>
      
      <div data-content="main" class='tab-content fade-in'>
         Main Content
      </div>
      <div data-content="remediation" class='tab-content'>
         Remediation Content
      </div>
      <div data-content="summary" class='tab-content'>
         Summary Content
      </div>      --%>


<!---------------------------------------------------------------------------------------------------------------------------->

</div>

<script>
    $(function () {

        $('.tabs li a').click(function () {
            var $targ = $(this)
            onTabClick($targ);
        });

        function onTabClick($targ) {
            var tabName = $targ.attr('href').replace('#', '');
            $('.tabs li a').removeClass('selected');
            $targ.addClass('selected');
            $('[data-content]').removeClass('fade-in').hide();

            var targetContent = '[data-content=' + tabName + ']';
            $(targetContent).addClass('fade-in').show();
        }

    });
</script>


<script>
    $('#tbDate1b').change(function () {
        dt1s = $('#tbDate1b').val();
        dt2s = $('#tbDate2b').val();
        if (dt1s.length == 10) {
            dt1s = dt1s.replace('.', '/');
            dt2s = dt2s.replace('.', '/');

            dt1s = dt1s.replace('.', '/');
            dt2s = dt2s.replace('.', '/');

            var date = dt1s;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            dt1 = new Date(yy + "/" + mm + "/" + dd);

            var date = dt2s;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            dt2 = new Date(yy + "/" + mm + "/" + dd);


            if (dt1 > dt2) { $('#tbDate2b').val($('#tbDate1b').val()); }

            var dt2i = new Date();
            dt2i = add_months(dt1, 6)
            if (dt2i < dt2) {
                var date = add_months(dt2i, 1)
                yr = date.getFullYear(),
                    month = date.getMonth() < 10 ? '0' + date.getMonth() : date.getMonth(),
                    day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate(),
                    newDate = day + '.' + month + '.' + yr;
                $('#tbDate2b').val(newDate);
            }
        }

    });


    $('#tbDate2b').change(function () {
        dt1s = $('#tbDate1b').val();
        dt2s = $('#tbDate2b').val();
        if (dt1s.length == 10) {
            dt1s = dt1s.replace('.', '/');
            dt2s = dt2s.replace('.', '/');

            dt1s = dt1s.replace('.', '/');
            dt2s = dt2s.replace('.', '/');

            var date = dt1s;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            dt1 = new Date(yy + "/" + mm + "/" + dd);

            var date = dt2s;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            dt2 = new Date(yy + "/" + mm + "/" + dd);

            if (dt1 > dt2) { $('#tbDate1b').val($('#tbDate2b').val()); }

            var dt2i = new Date();
            dt2i = add_months(dt2, -6)
            if (dt2i > dt1) {
                var date = add_months(dt2i, 1)
                yr = date.getFullYear(),
                    month = date.getMonth() < 10 ? '0' + date.getMonth() : date.getMonth(),
                    day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate(),
                    newDate = day + '.' + month + '.' + yr;
                $('#tbDate1b').val(newDate);
            }
        }

    });




    function add_months(dt, n) {
        return new Date(dt.setMonth(dt.getMonth() + n));
    }
</script>


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




<script type="text/javascript">
    $(".chosen").chosen();
</script>

<script type="text/javascript">   
 
    $(function () {
        $.datepicker.setDefaults({
            showOn: "both",
            buttonImage: "/js/cal-ico.png",
                    buttonImageOnly: true,
                    buttonText: "Выбрать дату"
                });
                $.datepicker.setDefaults($.datepicker.regional["ru"]);
                $.datepicker.formatDate("dd.mm.yyyy", new Date(<%=DateTime.Now.Year %>, <%=DateTime.Now.Month %>-1, <%=DateTime.Now.Day %>));
            
               $("#tbIssueDate").datepicker({
                   changeMonth: true,
                   numberOfMonths: 1,
                });

               $("#tbValidTill").datepicker({
                   changeMonth: true,
                   numberOfMonths: 1,
               });

               $("#tbDateOfBirth").datepicker({
                   changeMonth: true,
                   numberOfMonths: 1,
                   mask: 99
               });
                
              
               $("#tbActualDate").datepicker({
                   changeMonth: true,
                   numberOfMonths: 1,
                   mask: 99
               });

               $("#tbDate1b").datepicker({
                   changeMonth: true,
                   numberOfMonths: 1,
                   mask: 99,
               });
               $("#tbDate2b").datepicker({
                   changeMonth: true,
                   numberOfMonths: 1,
                   mask: 99,
               });
        
            });
</script>


<script type="text/javascript">
	//var input = document.getElementById('tbProductPrice');
	//input.oninput = function() {
	//	document.getElementById('tbRequestSumm').innerHTML = input.value;
	//};


	function sumChanged()
	{
		document.getElementById('tbRequestSumm').value = RadNumTbTotalPrice.get_value() - document.getElementById('tbAmountOfDownPayment').value;
	}
</script>



<script type="text/javascript">
	var previousRow;

	function ChangeRowColor(row)
	{
		if (previousRow == row)
			return;//do nothing
		else if (previousRow != null)
			document.getElementById(previousRow).style.backgroundColor = "#ffffff";

		document.getElementById(row).style.backgroundColor = "#ffffda";
		previousRow = row;
	}
</script>





<script type="text/javascript">
    $(document).ready(function () {
        $('#ddlMonthCount').change(function () {
            demo.valueChanged3();
            return false;
        })
    });
</script>

<script type="text/javascript">
    $(document).ready(function () {

        $('#ddlRequestPeriod').change(function () {
            demo.valueChanged2();
            return false;
        })
    });
</script>

<script>
     function myComment() {
         var txt;
         var person = prompt("Комментарий:", "");
         if (person == null || person == "") {
             txt = "";
         } else {
             txt = person;
         }
         document.getElementById("tbNote").innerHTML = txt;
     }
</script>

<script>
     function openNewWin() {
         $('form').attr('target', '_blank');
         setTimeout('resetFormTarget()', 500);
     }

     function resetFormTarget() {
         $('form').attr('target', '');
     }
</script>





      <script>


          

              try {
                  // Configure a few settings and attach camera
                  Webcam.set({
                      width: 320,
                      height: 240,
                      image_format: 'jpeg',
                      jpeg_quality: 90
                  });

                  var element = document.getElementById('my_camera');
                  if (typeof (element) != 'undefined' && element != null) {
                      // Exists.
                      Webcam.attach('#my_camera');
                  }



              }
              catch (err) {

              }

              // preload shutter audio clip
              //var shutter = new Audio();
              //shutter.autoplay = true;
              //shutter.src = navigator.userAgent.match(/Firefox/) ? 'shutter.ogg' : 'shutter.mp3';

              function take_snapshot() {
                  // play sound effect
                  //shutter.play();

                  // take snapshot and get image data
                  Webcam.snap(function (data_uri) {
                      // display results in page
                      document.getElementById('results').innerHTML = '<img id="imgCam" src="' + data_uri + '"/> <p>Чтобы сохранить фото, нужно сохранить заявку</p>';
                      document.getElementById('hfPhoto2').value = data_uri.replace(/^data\:image\/\w+\;base64\,/, '');

                  });
              }


          //$.ajax({
          //    type: "POST",
          //    url: "AgentView.aspx/btnSendSMS_click",
          //    data: "{}",
          //    contentType: "application/json; charset=utf-8",
          //    dataType: "json",
          //    success: function (msg) {
          //        // Do something interesting with msg.d here.
          //    }
          //});

          
          //$(document).ready(function () {
          //    $("#btnSendSMS").click(function () {
          //        AgentView.SayHello("Name", function (result) {
          //            alert(result);
          //        });
          //    });

          //})


          //$(document).ready(function () {
          //    $("#btnSendSMS5").click(function () {
          //        $.ajax({
          //            type: "POST",
          //            url: "AgentView.aspx/btnSendSMS_click",
          //            data: "{}",
          //            contentType: "application/json; charset=utf-8",
          //            dataType: "json",
          //            success: function (msg) {
          //                // Do something interesting with msg.d here.
          //                ;
          //                ;
          //            }
          //        });
          //    });

          //})
              

          //$(document).ready(function () {
          //    $("#btnSendSMS").click(function (e) {
           
          //        $.ajax({
          //            type: "POST",
          //            url: "AgentView.aspx/btnSendSMS_click",
          //            //url: '@Url.Action("btnSendSMS_click", "AgentView")',
          //            data: { creditId: $("#creditId").val() },
          //            //dataType: "html",
          //            success: function (result, status, xhr) {
          //                s = '<table class="line">';

          //                s = s + "<tr>"
          //                s = s + "<th>" + "Дата выплаты" + "</th>";
          //                s = s + "<th>" + "Основная сумма" + "</th>";
          //                s = s + "<th>" + "Проценты" + "</th>";
          //                s = s + "<th>" + "Итого" + "</th>";
          //                s = s + "<th>" + "Остаток основной суммы" + "</th>";
          //                s = s + "</tr>"

          //                result.forEach(r => {
          //                    s = s + "<tr>"
          //                    s = s + "<td>" + r.payDate + "</td>"; //Date.parse(r.payDate).toLocaleString()
          //                    s = s + "<td>" + r.mainSumm + "</td>";
          //                    s = s + "<td>" + r.percentsSumm + "</td>";
          //                    s = s + "<td>" + r.totalSumm + "</td>";
          //                    s = s + "<td>" + r.baseDeptToPay + "</td>";
          //                    s = s + "</tr>"
          //                })

          //            //    $("#dataDiv").html(s + " </table>");
                       


          //            },
          //            error: function (xhr, status, error) {
          //               // $("#dataDiv").html("Result: " + status + " " + error + " " + xhr.status + " " + xhr.statusText)
          //            }
          //        });
          //        //}
          //        return false;
          //    });
          //});



          //$(document).ready(function () {
          //    $("#btnSendSMS5").click(function (e) {
          //        $.ajax({
          //            type: "GET",
          //            url: 'AgentView.aspx/GetEmpList',
          //            data: {},
          //            contentType: "application/json; charset=utf-8",
          //            dataType: "json",
          //            beforeSend: function () {
          //               // Show(); // Show loader icon  
          //            },
          //            success: function (response) {

          //                // Looping over emloyee list and display it  
          //                //$.each(response, function (index, emp) {
          //                //    $('#output').append('<p>Id: ' + emp.ID + '</p>' +
          //                //        '<p>Id: ' + emp.Name + '</p>');
          //                //});
          //            },
          //            complete: function () {
          //                //Hide(); // Hide loader icon  
          //            },
          //            failure: function (jqXHR, textStatus, errorThrown) {
          //                alert("HTTP Status: " + jqXHR.status + "; Error Text: " + jqXHR.responseText); // Display error message  
          //            }
          //        });
          //    })  
          //   })

         
      </script>


   
</asp:Content>