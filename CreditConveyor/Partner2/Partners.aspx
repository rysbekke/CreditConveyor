<%@ Page Title="Партнеры" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Partners.aspx.cs" Inherits="СreditСonveyor.Partners2.Partners" Async="true" AsyncTimeout="60" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 <link href="/css/bootstrap.css" rel="stylesheet" />
<link rel="stylesheet" href="/css/chosen.css">
<script src="/js/jquery.js"></script>
<script src="/js/jquery-ui.js"></script>

<script src="/js/chosen.jquery.min.js"></script>
<script src="/js/chosen.init.js"></script>
<script src="/js/datepicker-ru.js"></script>
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

    <script src="/js/webcam.js"></script>
    <style>
#my_camera{
 width: 320px;
 height: 240px;
 border: 1px solid black;
}
#tbNoteCancelReqExp, #tbNoteCancelReq {  max-width: 568px; max-height:120px;
          min-width: 568px; min-height:120px;
              
}

        .auto-style1 {
            height: 25px;
        }
    </style>

<div>
<h4>Партнеры</h4>    
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
                <td><asp:RequiredFieldValidator ID="rfOldPassword" runat="server" ErrorMessage="*" ValidationGroup="changePassowrd" ControlToValidate="tbOldPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Новый пароль</td>
                <td><asp:TextBox ID="tbNewPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfNewPassword" runat="server" ErrorMessage="*" ControlToValidate="tbNewPassword" ValidationGroup="changePassowrd"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ErrorMessage="Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов" ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" ValidationGroup="changePassowrd" ControlToValidate="tbNewPassword"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Подтверждение</td>
                <td class="auto-style1"><asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                <td class="auto-style1"><asp:RequiredFieldValidator ID="rfConfirmPassword" runat="server" ErrorMessage="*" ValidationGroup="changePassowrd" ControlToValidate="tbConfirmPassword"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revConfirmPassword" runat="server" ErrorMessage="Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов" ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" ValidationGroup="changePassowrd" ControlToValidate="tbConfirmPassword"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMsgPassword" runat="server" Text="Неправильный пароль" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="btnSavePassword" runat="server" Text="Сохранить" OnClick="btnSavePassword_Click" ValidationGroup="changePassowrd" /></td>
                <td><asp:Button ID="btnCancelPassword" runat="server" Text="Закрыть" OnClick="btnCancelPassword_Click" /></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    
                </td>
            </tr>
            
        </table>
    </asp:Panel> 
    
        
    
	<asp:Panel ID="pnlCredit" runat="server" CssClass="panels">


      
        <div class="tblresp">
		<table class="table">
            <thead>
			<tr>
				<th></th>
                <th>№ заявки</th>
				<th>ИНН</th>
                <th>Credit ID</th>
				<th>Фамилия</th>
				<th>Имя</th>
				<th>Дата
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbDate1b" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SearchRequest"></asp:RegularExpressionValidator>
                </th>
				<th></th>
				<th></th>
			</tr>
            </thead>
            <tbody>
			<tr>
				<td data-label = ""></td>
                <td data-label = "№ заявки"><asp:TextBox ID="tbRequestID" runat="server"></asp:TextBox>&nbsp;</td>
				<td data-label = "ИНН"><asp:TextBox ID="tbSearchRequestINN" runat="server"></asp:TextBox>&nbsp;</td>
                <td data-label = "CreditID"><asp:TextBox ID="tbCreditID" runat="server"></asp:TextBox>&nbsp;</td>
				<td data-label = "Фамилия"><asp:TextBox ID="tbSearchRequestSurname" runat="server"></asp:TextBox>&nbsp;</td>
				<td data-label = "Имя"><asp:TextBox ID="tbSearchRequestName" runat="server"></asp:TextBox>&nbsp;</td>
                <td data-label = "Дата"><asp:TextBox ID="tbDate1b" runat="server" ClientIDMode="Static" ></asp:TextBox>&nbsp;</td>
				<td data-label = ""><asp:TextBox ID="tbDate2b" runat="server" ClientIDMode="Static" Visible="False"></asp:TextBox>&nbsp;</td>
				<td data-label = ""><asp:Button ID="btnSearchRequest" runat="server" OnClick="btnSearchRequest_Click" Text="Найти" ValidationGroup="SearchRequest" /></td>
			</tr>
            <tr>
				<td data-label = "" colspan="7"><b>Статусы заявки</b>
                    <asp:CheckBoxList ID="chkbxlistStatus" runat="server" RepeatDirection="Horizontal" Visible="False">
                        <asp:ListItem Selected="True">Новая заявка</asp:ListItem>
                        <asp:ListItem Selected="True">Исправить</asp:ListItem>
                        <asp:ListItem Selected="True">Исправлено</asp:ListItem>
                        <asp:ListItem Selected="True">Подтверждено</asp:ListItem>
                        <asp:ListItem Selected="True">Утверждено</asp:ListItem>
                        <asp:ListItem Selected="True">Выдано</asp:ListItem>
                        <asp:ListItem Selected="True">Принято</asp:ListItem>
                        <asp:ListItem Selected="True">Не подтверждено</asp:ListItem>
                        <asp:ListItem Selected="True">Отменено</asp:ListItem>
                        <asp:ListItem Selected="True">Отказано</asp:ListItem>
                        <asp:ListItem Selected="True">В обработке</asp:ListItem>
                        <asp:ListItem Selected="True">На выдаче</asp:ListItem>
                        <asp:ListItem Selected="True">К подписи</asp:ListItem>
                    </asp:CheckBoxList>
                    <asp:CheckBox ID="chkbxSelectAll" runat="server" AutoPostBack="True" Checked="True" Text="Выделить все" OnCheckedChanged="chkbxSelectAll_CheckedChanged"  />
				</td>
            </tr>
            <tr>
                <td data-label = "" colspan="7"><b>Вид работы</b>
                    <asp:CheckBoxList ID="chkbxlistKindActivity" runat="server" RepeatDirection="Horizontal" Visible="False">
                        <asp:ListItem Selected="True">Работа по найму</asp:ListItem>
                        <asp:ListItem Selected="True">Бизнес</asp:ListItem>
                        <asp:ListItem Selected="True">Агро</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td data-label = "" colspan="7"><b>Группа</b>
                    <asp:CheckBoxList ID="chkbxGroup" runat="server" RepeatDirection="Horizontal" Visible="False">
                        <asp:ListItem Selected="True" Value="128">DCB-онлайн</asp:ListItem>
                        <asp:ListItem Selected="True" Value="128">Партнеры</asp:ListItem>
                        <asp:ListItem Selected="True" Value="134">Кола</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td data-label = "" colspan="7"><b>Офисы</b>
                    <asp:CheckBoxList ID="chkbxOffice" runat="server" RepeatDirection="Horizontal" Visible="true">
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
                    </asp:CheckBoxList>
                    <asp:CheckBox ID="chkbxSelectAllOffice" runat="server" AutoPostBack="True" Checked="True" Text="Выделить все" OnCheckedChanged="chkbxSelectAllOffice_CheckedChanged" />
                </td>
            </tr>

            </tbody>
		</table>
        </div>



        <br />
        <div class="row">
            <div class="col-lg-12 ">
                <div class="table-responsive">
                    <asp:GridView ID="gvRequests" runat="server" ShowHeaderWhenEmpty="True" OnRowCommand="gvRequests_RowCommand" AutoGenerateColumns="False" OnRowDataBound="gvRequests_RowDataBound" CssClass="gvRequests" AllowPaging="True" OnPageIndexChanging="gvRequests_PageIndexChanging" PageSize="50">
                        <Columns>
                            <asp:TemplateField HeaderText="Выбрать">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbSelectRequest" runat="server" CommandArgument='<%# Eval("RequestID") %>' CommandName="Sel">Выбрать</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RequestID" HeaderText="Номер" />
                            <asp:BoundField DataField="CreditID" HeaderText="CreditID" />
                            <asp:BoundField DataField="ShortName" HeaderText="Филиал" />
							<asp:BoundField DataField="OfficeShortName" HeaderText="Офис" />
                            <asp:BoundField DataField="Surname" HeaderText="Фамилия" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Имя" />
                            <asp:BoundField DataField="GroupName" HeaderText="Группа" />
                            <asp:BoundField DataField="OrgName" HeaderText="Орг" />
                            <asp:BoundField DataField="IdentificationNumber" HeaderText="ИНН" />
                            <asp:BoundField DataField="ProductPrice" HeaderText="Цена" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="AmountDownPayment" HeaderText="Взнос" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="RequestSumm" HeaderText="Кредит" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="RequestDate" HeaderText="Дата заявки" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="RequestRate" HeaderText="Процент" />
                            <asp:BoundField DataField="RequestStatus" HeaderText="Статус заявки" />
                            <asp:BoundField DataField="StatusOB" HeaderText="Статус в ОБ" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#014783" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#014783" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                </div>
            </div>
        </div>

	
        <asp:HiddenField ID="hfRequestsRowID" runat="server" />
		<asp:HiddenField ID="hfCardNumber" runat="server" />
		<asp:HiddenField ID="hfRevenueMonth" runat="server" />
		<br />
        <asp:Label ID="lblError" runat="server" Text="" Font-Size="Larger" ForeColor="Red"></asp:Label>
           <%--<asp:Panel ID="pnlBlackList" runat="server" Visible="False">
                                        
                                        <asp:Label ID="lblBlacListCust" runat="server" Text="Клиент в черном списке!!!" Font-Size="Larger" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="gvBlackListCustomers" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                             <asp:BoundField DataField="Surname" HeaderText="Фамилия" />
                                             <asp:BoundField DataField="PersonName" HeaderText="Имя" />
                                             <asp:BoundField DataField="Otchestvo" HeaderText="Отчество" />
                                             <asp:BoundField DataField="CompanyName" HeaderText="Организация" />
                                             <asp:BoundField DataField="PassportNo" HeaderText="Паспорт" />
                                             <asp:BoundField DataField="PassportIssueDate" HeaderText="Дата выдачи паспорта" dataformatstring="{0:dd/MM/yyyy}" />
                                             <asp:BoundField DataField="BirthDate" HeaderText="Дата рожд." dataformatstring="{0:dd/MM/yyyy}"/>
                                             <asp:BoundField DataField="IdentificationNo" HeaderText="ИНН" />
                                             <asp:BoundField DataField="Description" HeaderText="Причина занесения в ЧС" />
                                             <asp:BoundField DataField="DateRegBL" HeaderText="Дата занесения в ЧС" dataformatstring="{0:dd/MM/yyyy HH:mm}"/>
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="red" />
                                        </asp:GridView>
                                    </asp:Panel>--%>
        <br />       
        <%--<asp:Panel ID="pnlBlackListOrg" runat="server" Visible="False">
                                        
                                        <asp:Label ID="lblBlacListOrg" runat="server" Text="Организация в черном списке!!!" Font-Size="Larger" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="gvBlackListOrg" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                             <asp:BoundField DataField="CompanyName" HeaderText="Организация" />
                                             <asp:BoundField DataField="PassportNo" HeaderText="Свидетельство №" />
                                             <asp:BoundField DataField="PassportIssueDate" HeaderText="Дата рег. в МЮ" dataformatstring="{0:dd/MM/yyyy}"/>
                                             <asp:BoundField DataField="BirthDate" HeaderText="Дата первичной рег." dataformatstring="{0:dd/MM/yyyy}"/>
                                             <asp:BoundField DataField="IdentificationNo" HeaderText="ИНН" />
                                             <asp:BoundField DataField="Description" HeaderText="Причина занесения в ЧС" />
                                             <asp:BoundField DataField="DateRegBL" HeaderText="Дата занесения в ЧС" dataformatstring="{0:dd/MM/yyyy HH:mm}"/>
                                            </Columns>
                                            <RowStyle BackColor="White" ForeColor="red" />
                                        </asp:GridView>
        </asp:Panel>--%>
        <br />       


		<h3>Форма заявки</h3><asp:HiddenField ID="hfRequestAction" runat="server" />

       <%--<asp:Label ID="lblError" runat="server" Text="error" Font-Size="Larger" ForeColor="Red"></asp:Label>--%>

		<asp:Panel ID="pnlNewRequest" runat="server" Visible="false" CssClass="panels">
			<table>
				<tr>
					<td></td>
					<td colspan="2">Статус кредитной заявки:<asp:Label ID="lblStatusRequest" runat="server" Text="Status"></asp:Label></td>
					<td></td>
					<td></td>
					<td><asp:Label ID="lblMessageClient" runat="server" CssClass="SaveRequest"></asp:Label></td>
					<td></td>
					<td></td>
					<td></td>
					<td><asp:Label ID="lblMsgGuar" runat="server" Text=""></asp:Label></td>
                    <td></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td></td>
					<td><label>Клиент (счет для погашения) </label></td>
					<td><b class="red"><asp:Label ID="lblAccount" runat="server"></asp:Label></b></td>
					<td></td>
					<td></td>
                    <td></td>
					<td></td>
                    <td></td>
                    <td></td>
				</tr>
                	<tr>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td></td>
					<td><label>Кредит ID </label></td>
					<td><b class="red"><asp:Label ID="lblCreditN" runat="server"></asp:Label></b></td>
					<td></td>
					<td></td>
                    <td></td>
					<td></td>
                    <td></td>
                    <td></td>
				</tr>
                <tr>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td></td>
					<td><label>Комиссия</label></td>
					<td><b class="red"><asp:Label ID="lblComission" runat="server"></asp:Label></b></td>
					<td></td>
					<td></td>
                    <td></td>
					<td><label>Поручитель</label></td>
                    <td></td>
                    <td></td>
				</tr>
				<tr>
					<td></td>
					<td><label>Цель кредита:</label></td>
					<td>
						<asp:TextBox ID="tbCreditPurpose" runat="server" TabIndex="1" MaxLength="30"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbCreditPurpose" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
					</td>
					<td></td>
					<td><label>Фамилия:</label></td>
					<td><asp:TextBox ID="tbSurname2" runat="server" Enabled="False" TabIndex="5"></asp:TextBox></td>
					<td><asp:Button ID="btnCustomerSearch" runat="server" OnClick="btnCustomerSearch_Click" Text="Выбрать клиента" /></td>
					<td>&nbsp;</td>
					<td></td>
					<td><label>Фамилия:</label></td>
                    <td><asp:TextBox ID="tbGuarantorSurname" runat="server" Enabled="False"></asp:TextBox></td>
                    <td>
                        <asp:Button ID="btnGuarantSearch" runat="server" Text="Выбрать поручителя" OnClick="btnGuarantSearch_Click" Enabled="false" />
                    </td>
				</tr>
				<tr>
					<td></td>
					<td>
						<label>% Cтавка:</label></td>
					<td> 

                        <asp:DropDownList ID="ddlRequestRate" runat="server" TabIndex="0" OnSelectedIndexChanged="ddlRequestRate_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0,00">0,00</asp:ListItem>
                            <asp:ListItem Value="29,90" Selected="True">29,90</asp:ListItem>
                        </asp:DropDownList> 
					</td>
					<td></td>
					<td>
						<label>Имя:</label></td>
					<td>
						<asp:TextBox ID="tbCustomerName2" runat="server" Enabled="False" TabIndex="6"></asp:TextBox></td>
					<td><asp:Button ID="btnCustomerEdit" runat="server" Text="Редактировать" OnClick="btnCustomerEdit_Click" Enabled="false"/></td>
					<td></td>
					<td></td>
                    <td><label>Имя:</label></td>
                    <td>
                        <asp:TextBox ID="tbGuarantorName" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnGuarantorEdit" runat="server" Text="Редактировать" Enabled="False" OnClick="btnGuarantorEdit_Click" />
                    </td>
				</tr>
				<tr>
					<td></td>
					<td><label>Срок кредита:</label></td>
					<td>               
						<asp:DropDownList ID="ddlRequestPeriod" runat="server" TabIndex="2" ClientIDMode="Static" OnSelectedIndexChanged="ddlRequestPeriod_SelectedIndexChanged" AutoPostBack="True">
							<asp:ListItem Value="3"></asp:ListItem>
							<asp:ListItem Value="4"></asp:ListItem>
							<asp:ListItem Value="5"></asp:ListItem>
							<asp:ListItem Value="6"></asp:ListItem>
							<asp:ListItem Value="7"></asp:ListItem>
							<asp:ListItem Value="8"></asp:ListItem>
							<asp:ListItem Value="9"></asp:ListItem>
							<asp:ListItem Value="10"></asp:ListItem>
							<asp:ListItem Value="11"></asp:ListItem>
							<asp:ListItem Value="12"></asp:ListItem>
                            <asp:ListItem Value="13"></asp:ListItem>
                            <asp:ListItem Value="14"></asp:ListItem>
                            <asp:ListItem Value="15"></asp:ListItem>
                            <asp:ListItem Value="16"></asp:ListItem>
                            <asp:ListItem Value="17"></asp:ListItem>
                            <asp:ListItem Value="18"></asp:ListItem>
                            <asp:ListItem Value="19"></asp:ListItem>
                            <asp:ListItem Value="20"></asp:ListItem>
                            <asp:ListItem Value="21"></asp:ListItem>
                            <asp:ListItem Value="22"></asp:ListItem>
                            <asp:ListItem Value="23"></asp:ListItem>
                            <asp:ListItem Value="24"></asp:ListItem>
                            <asp:ListItem Value="25"></asp:ListItem>
                            <asp:ListItem Value="26"></asp:ListItem>
                            <asp:ListItem Value="27"></asp:ListItem>
                            <asp:ListItem Value="28"></asp:ListItem>
                            <asp:ListItem Value="29"></asp:ListItem>
                            <asp:ListItem Value="30"></asp:ListItem>
                            <asp:ListItem Value="31"></asp:ListItem>
                            <asp:ListItem Value="32"></asp:ListItem>
                            <asp:ListItem Value="33"></asp:ListItem>
                            <asp:ListItem Value="34"></asp:ListItem>
                            <asp:ListItem Value="35"></asp:ListItem>
                            <asp:ListItem Value="36"></asp:ListItem>
						</asp:DropDownList> 
                        
						<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRequestPeriod" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
					</td>
					<td></td>
					<td><label>Отчество</label></td>
					<td><asp:TextBox ID="tbOtchestvo2" runat="server" Enabled="False" TabIndex="7"></asp:TextBox></td>
					<td>
						<asp:Button ID="btnUpdFIO" runat="server" Text="Обновить ФИО" OnClick="btnUpdFIO_Click"  />
					</td>
					<td></td>
					<td></td>
					<td><label>Отчество</label></td>
                    <td>
                        <asp:TextBox ID="tbGuarantorOtchestvo" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                    <td></td>
				</tr>
				<tr>
					<td></td>
					<td><label>Комиссия:</label></td>
					<td>
                        <asp:Label ID="lblCommission" runat="server" Text="0"></asp:Label></td>
					<td></td>
					<td><label>ИНН:</label></td>
					<td><asp:TextBox ID="tbINN2" runat="server" Enabled="False" TabIndex="8"></asp:TextBox></td>
					<td></td>
					<td></td>
                    <td></td>
					<td><label>ИНН:</label></td>
                    <td><asp:TextBox ID="tbGuarantorINN" runat="server" Enabled="False"></asp:TextBox></td>
                    <td></td>
				</tr>
				<tr>
					<td><br /></td>
                    <td><br />Офис:</td>
                    <td><br />
							<asp:DropDownList ID="ddlOffice" runat="server" Visible="False">
                            <asp:ListItem Value="1105">ЦФ</asp:ListItem>
                            <asp:ListItem Value="1082">Берекет</asp:ListItem>
                            <asp:ListItem Value="1134">Карабалта </asp:ListItem>
                            <asp:ListItem Value="1133">Талас</asp:ListItem>
                            <asp:ListItem Value="1077">Балыкчы</asp:ListItem>
                            <asp:ListItem Value="1080">Ош</asp:ListItem>
                            <asp:ListItem Value="1079">Нарын</asp:ListItem>
                            <asp:ListItem Value="1081">Токмок</asp:ListItem>
                            <asp:ListItem Value="1078">Каракол</asp:ListItem>
                            <asp:ListItem Value="1085">Жалалбад</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSaveOffice" runat="server" Height="29px" OnClick="btnSaveOffice_Click" Text="Сохранить" Visible="False" Enabled="false"/></td>
                    <td><br /></td>
                    <td>Сем.положение:</td>
                    <td>
                        <asp:RadioButtonList ID="rbtnMaritalStatus" runat="server">
                            <asp:ListItem Value="0">холост (не замужем)</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">женат (за мужем)</asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td><br /></td>
                    <td><br /></td>
                    <td><br /></td>

				</tr>
			</table>
            <asp:GridView ID="gvPositions" runat="server"></asp:GridView>
            <br />
			<table>
							<tr>
								<td>
									<asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="gvProducts" OnRowCommand="gvProducts_RowCommand" ShowHeader="true" ShowFooter = "true" onrowediting="gvProducts_RowEditing" OnRowCancelingEdit="gvProducts_RowCancelingEdit" OnRowUpdating="gvProducts_RowUpdating" DataKeyNames="ProductID">
										<Columns>
                                            <asp:TemplateField HeaderText = "ProductID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
											<asp:TemplateField  HeaderText = "Наименование">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductMark" runat="server"
                                                            Text='<%# Eval("ProductMark")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProductMark" runat="server"
                                                        Text='<%# Eval("ProductMark")%>'></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProductMark" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

											<asp:TemplateField  HeaderText = "Серийный №">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductSerial" runat="server"
                                                            Text='<%# Eval("ProductSerial")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProductSerial" runat="server"
                                                        Text='<%# Eval("ProductSerial")%>'></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProductSerial" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText = "imei-код">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductImei" runat="server" Text='<%# Eval("ProductImei")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProductImei" runat="server" Text='<%# Eval("ProductImei")%>'></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProductImei" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText = "Цена">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtPrice" Width="150px" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" TabIndex="13" ReadOnly="false" Text='<%# Eval("Price")%>' ClientIDMode="Static" onchange="demo.txtPrice()" ></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox runat="server" ID="txtPrice" Width="150px" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" TabIndex="13" ReadOnly="false" ClientIDMode="Static" onchange="demo.txtPrice()" ></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText = "Примечание">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNote" runat="server" Text='<%# Eval("Note")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNote" runat="server" Text='<%# Eval("Note")%>'></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNote" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                          <asp:TemplateField HeaderText = "Итого">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPriceWithTarif" runat="server" Text='<%# Eval("PriceWithTarif")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

											<asp:TemplateField HeaderText="Операции">
												<ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelItem" runat="server" CommandArgument='<%# Eval("ProductID") %>' CommandName="Del" OnClientClick = "return confirm('Do you want to delete?')">Delete</asp:LinkButton>
												</ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick = "AddNewProduct" />
                                                    </FooterTemplate>
											</asp:TemplateField>
                                            <asp:CommandField  ShowEditButton="True" />
										</Columns>
									</asp:GridView>

								</td>
							</tr>
							<tr>
								<td>
									<br />
								</td>
							</tr>
							<tr>
								<td>
									<br />
								</td>
							</tr>
			</table>


			<table>
				<tr>
					<td></td>
					<td>
						<table class="tblStoimost">
                            <tr>
                                <td>
                                    <asp:Label ID="lblOrgINN" runat="server" Text="ИНН организации"></asp:Label>
                                </td>
                                <td></td>
                                <td>
                                    <asp:TextBox ID="tbINNOrg" runat="server"></asp:TextBox>
					                <asp:RegularExpressionValidator ID="reINNOrg" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest" ControlToValidate="tbINNOrg" ValidationExpression="\d\d\d\d\d\d\d\d\d\d\d\d\d\d"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td></td>
                                <td></td>
                            </tr>
							<tr>
								<td>
									<label>Стоимость итого:</label></td>
								<td>&nbsp;</td>
								<td>
                                    <asp:TextBox ID="RadNumTbTotalPrice" runat="server" Width="150px" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" onchange="demo.valueChanged()" Enabled="true" TabIndex="13" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfProductPrice" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest" ControlToValidate="RadNumTbTotalPrice" ToolTip="Введите цену"></asp:RequiredFieldValidator>
								</td>
								<td></td>
							</tr>
							<tr>
								<td>
									<label>Сумма первоначального взноса:</label></td>
								<td></td>
								<td>
                                   
                                    <asp:TextBox ID="TbAmountOfDownPayment" runat="server" AutoPostBack="True" OnTextChanged="TbAmountOfDownPayment_TextChanged" Text="0,00" onchange="demo.TbAmountOfDownPayment()" ClientIDMode="Static"></asp:TextBox>
                                    <asp:HiddenField ID="hfAmountOfDownPayment" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest" ControlToValidate="TbAmountOfDownPayment"></asp:RequiredFieldValidator>

								</td>
							</tr>
							<tr>
								<td><label>Сумма кредита:</label></td>
								<td></td>
								<td>
                                    <asp:TextBox ID="RadNumTbRequestSumm" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="true" onchange="demo.valueChanged3()" TabIndex="15" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Ошибка!" ControlToValidate="RadNumTbRequestSumm" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
								</td>
							</tr>
                            <tr>
								<td></td>
								<td></td>
								<td>
                                    <asp:TextBox ID="RadNumTbMonthlyInstallment" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="false" TabIndex="16" visible="false"></asp:TextBox>
                                </td>
							</tr>
							<tr>
								<td>
									<label>Предполагаемая дата погашения</label></td>
								<td></td>
								<td>
									<asp:TextBox ID="tbActualDate" runat="server" ClientIDMode="Static" TabIndex="17" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" ></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="tbActualDate" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="дд.мм.гггг (возможные дни погашения: 2-19, кроме 5,10,15)" ValidationExpression="(?:02|03|04|06|07|08|09|11|12|13|14|16|17|18|19).\d\d.\d\d\d\d" ValidationGroup="SaveRequest" ControlToValidate="tbActualDate" CssClass="SaveRequest"></asp:RegularExpressionValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="дд.мм.гггг (возможные дни погашения: 2-19, кроме 5,10,15)" ValidationExpression="(?:02|03|04|06|07|08|09|11|12|13|14|16|17|18|19).\d\d.\d\d\d\d" ValidationGroup="SaveRequest" ControlToValidate="tbActualDate" CssClass="SaveRequest"></asp:RegularExpressionValidator>--%>
                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="дд.мм.гггг (возможные дни погашения: 2-19)" ValidationExpression="(?:02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19).\d\d.\d\d\d\d" ValidationGroup="SaveRequest" ControlToValidate="tbActualDate" CssClass="SaveRequest"></asp:RegularExpressionValidator>--%>
								</td>
							</tr>

							<tr>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td></td>
								<td></td>
								<td></td>
							</tr>
						</table>
					</td>
					<td>
						<asp:Button ID="btnGetMPZ" runat="server" OnClick="btnGetMPZ_Click" Text="Получение данных с МПЦ" />
						<br />
                        
						<asp:RadioButtonList ID="rbtnBusiness" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnBusiness_SelectedIndexChanged" AutoPostBack="True">
							<asp:ListItem Selected="True">Работа по найму</asp:ListItem>
							<asp:ListItem>Бизнес</asp:ListItem>
                            <asp:ListItem>Агро</asp:ListItem>
						</asp:RadioButtonList>
						<asp:Panel ID="pnlEmployment" runat="server">
							<table>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chbEmployer" runat="server" Text="Сотрудник" AutoPostBack="True" OnCheckedChanged="chbEmployer_CheckedChanged"/>
                                    </td>
                                </tr>
								<tr>
                                    <td>Общая сумма заработной платы</td>
                                    <td>
                                        <asp:TextBox ID="RadNumTbSumMonthSalary" runat="server" Width="150px" Value="0" EmptyMessage="" Type="Currency" MinValue="0" onchange="demo.valueChanged3()" AutoPostBack="false" TabIndex="18" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfAverageMonthSalary" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbSumMonthSalary" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Количество месяцев</td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonthCount" runat="server" TabIndex="1" ClientIDMode="Static" >
                                            <asp:ListItem Value="1"></asp:ListItem>
                                            <asp:ListItem Value="2"></asp:ListItem>
							                <asp:ListItem Value="3"></asp:ListItem>
							                <asp:ListItem Value="4"></asp:ListItem>
							                <asp:ListItem Value="5"></asp:ListItem>
							                <asp:ListItem Value="6"></asp:ListItem>
							                <asp:ListItem Value="7"></asp:ListItem>
							                <asp:ListItem Value="8"></asp:ListItem>
							                <asp:ListItem Value="9"></asp:ListItem>
							                <asp:ListItem Value="10"></asp:ListItem>
							                <asp:ListItem Value="11"></asp:ListItem>
							                <asp:ListItem Value="12"></asp:ListItem>
						                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
									<td><label>Среднемес. заработная плата:</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbAverageMonthSalary" Name="AverageMonthSalary" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="true" ClientIDMode="Static"></asp:TextBox>
									</td>
								</tr>
							</table>
						</asp:Panel>
						<asp:Panel ID="pnlBusiness" runat="server" Visible="false">
							<table>
                               <tr>
									<td><label>Минимальная выручка в день:</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbMinRevenue" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbMinRevenue()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfMinRevenue" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbMinRevenue" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                                <tr>
									<td><label>Максимальная выручка в день:</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbMaxRevenue" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbMaxRevenue()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfMaxRevenue" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbMaxRevenue" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                                <tr>
                                    <td><label>Количество рабочих дней в мес:</label></td>
                                    <td>
                                            <asp:DropDownList ID="ddlCountWorkDay" runat="server" TabIndex="1" ClientIDMode="Static" >
                                            <asp:ListItem Value="1"></asp:ListItem>
                                            <asp:ListItem Value="2"></asp:ListItem>
							                <asp:ListItem Value="3"></asp:ListItem>
							                <asp:ListItem Value="4"></asp:ListItem>
							                <asp:ListItem Value="5"></asp:ListItem>
							                <asp:ListItem Value="6"></asp:ListItem>
							                <asp:ListItem Value="7"></asp:ListItem>
							                <asp:ListItem Value="8"></asp:ListItem>
							                <asp:ListItem Value="9"></asp:ListItem>
							                <asp:ListItem Value="10"></asp:ListItem>
							                <asp:ListItem Value="11"></asp:ListItem>
							                <asp:ListItem Value="12"></asp:ListItem>
                                            <asp:ListItem Value="13"></asp:ListItem>
							                <asp:ListItem Value="14"></asp:ListItem>
							                <asp:ListItem Value="15"></asp:ListItem>
							                <asp:ListItem Value="16"></asp:ListItem>
							                <asp:ListItem Value="17"></asp:ListItem>
							                <asp:ListItem Value="18"></asp:ListItem>
							                <asp:ListItem Value="19"></asp:ListItem>
							                <asp:ListItem Value="20"></asp:ListItem>
							                <asp:ListItem Value="21"></asp:ListItem>
							                <asp:ListItem Value="22"></asp:ListItem>
                                            <asp:ListItem Value="23"></asp:ListItem>
							                <asp:ListItem Value="24"></asp:ListItem>
							                <asp:ListItem Value="25"></asp:ListItem>
							                <asp:ListItem Value="26"></asp:ListItem>
							                <asp:ListItem Value="27"></asp:ListItem>
							                <asp:ListItem Value="28"></asp:ListItem>
							                <asp:ListItem Value="29"></asp:ListItem>
							                <asp:ListItem Value="30"></asp:ListItem>
                                            <asp:ListItem Value="31"></asp:ListItem>
						                </asp:DropDownList>
                                    </td>
                                </tr>
								<tr>
									<td><label>Средняя наценка % (в торговле):</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbСostPrice" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="20" ClientIDMode="Static" onchange="demo.RadNumTbСostPrice()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfСostPrice" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbСostPrice" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>

								</tr>
								<tr>
									<td><label>Расходы по бизнесу в месяц (аренда, охрана, патент, транспорт, з/п и др.расходы связанные с бизнесом):</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbOverhead" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="21" ClientIDMode="Static" onchange="demo.RadNumTbOverhead()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfOverhead" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbOverhead" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
								<tr>
									<td><%--<label>Расходы на семью в месяц:</label>--%></td>
									<td>
                                        <%--<asp:TextBox ID="RadNumTbFamilyExpenses" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22" ClientIDMode="Static" onchange="demo.RadNumTbFamilyExpenses()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfFamilyExpenses" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbFamilyExpenses" CssClass="SaveRequest"></asp:RequiredFieldValidator>--%>
									</td>
								</tr>
                                 <tr>
                                   <td><asp:Label ID="lblColaComment" runat="server" Text="Данные по бизнесу :" Visible="false" ></asp:Label></td>
                                   <td><asp:TextBox ID="txtColaComment" runat="server" TextMode="MultiLine" Rows="8" Width="300px" Visible="false" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblBusinessComment" runat="server" Text="Комментарии по бизнесу :</label>"></asp:Label></td>
                                    <td><asp:TextBox ID="txtBusinessComment" runat="server" TextMode="MultiLine" Rows="8" Width="300px"></asp:TextBox></td>
                                </tr>
							</table>
						</asp:Panel>
                                                <asp:Panel ID="pnlAgro" runat="server" Visible="false">
							<table>
                               <tr>
									<td><label>Выручка от продажи скота за последние 3 мес:</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbRevenueAgro" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbRevenueAgro()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenueAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                               <tr>
									<td><label>Выручка от продажи молока в день:</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbRevenueMilk" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbRevenueMilk()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenueMilk" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                                <tr>
									<td><label>Выручка от продажи молочной продукции в день:</label></td>
									<td>
										<%--<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbRevenueMilk" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19"></telerik:RadNumericTextBox>--%>
                                        <asp:TextBox ID="RadNumTbRevenueMilkProd" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbRevenueMilk()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenueMilkProd" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                                <tr>
									<td><label>Расходы на содержание скота за последние 3 мес:</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbOverheadAgro" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbOverheadAgro()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbOverheadAgro" CssClass="SaveRequest" onchange="demo.RadNumTbOverheadAgro()"></asp:RequiredFieldValidator>
									</td>
								</tr>

								
								<tr>
									<td><label>Дополнительные расходы в мес:</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbAddOverheadAgro" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="21" ClientIDMode="Static" onchange="demo.RadNumTbAddOverheadAgro()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbAddOverheadAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
								<%--<tr>
									<td><label>Расходы на семью в месяц:</label></td>
									<td>
                                        <asp:TextBox ID="RadNumTbFamilyExpensesAgro" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22" ClientIDMode="Static" onchange="demo.RadNumTbFamilyExpensesAgro()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbFamilyExpensesAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>--%>
                                
                               
                                
                                <tr>
                                   <td><asp:Label ID="Label1" runat="server" Text="Комментарии по бизнесу :</label>"></asp:Label></td>
                                   <td><asp:TextBox ID="txtAgroComment" runat="server" TextMode="MultiLine" Rows="8" Width="300px"></asp:TextBox></td>
                                </tr>

							</table>
						</asp:Panel>
                        <table>
                                <tr>
									<td>Семейные расходы:</td>
									<td>
                                        <asp:TextBox ID="RadNumTbFamilyExpenses" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22" ClientIDMode="Static" onchange="demo.RadNumTbFamilyExpenses()">7000</asp:TextBox>
										<asp:RequiredFieldValidator ID="rfFamilyExpenses" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbFamilyExpenses" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                                 <tr>
									<td><label>Дополнительные доходы:</label></td>

									<td>
                                        <asp:TextBox ID="RadNumTbAdditionalIncome" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22" ClientIDMode="Static" onchange="demo.RadNumTbAdditionalIncome()"></asp:TextBox>
									</td>
                                </tr>
                        </table>

					</td>
					<td></td>
					<td>

		 Документы:
		<div class="divDocuments">
			<table>
				<tr>
					<td><label>Выберите файл:</label></td>
					<td>

                        <asp:FileUpload id="FileUploadControl" runat="server" />
					</td>
				</tr>
				<tr>
					<td><label>Описание файла:</label></td>
					<td><asp:TextBox ID="tbFileDescription" runat="server"></asp:TextBox></td>
				</tr>
			</table>
            <br />
            <br />
            <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
            <br />

			<br />
			<asp:Button ID="btnUploadFiles" runat="server" Text="Загрузить" OnClick="btnUploadFiles_Click" Enabled="False" />
			<asp:GridView ID="gvRequestsFiles" runat="server" HeaderStyle-BackColor="#336666" HeaderStyle-ForeColor="White"
				AlternatingRowStyle-ForeColor="#000" CssClass="gvRequestsFiles" AutoGenerateColumns="false" OnRowCommand="gvRequestsFiles_RowCommand" OnRowDataBound="gvRequestsFiles_RowDataBound">
				<Columns>
					<asp:BoundField DataField="Name" HeaderText="Имя файла" />
					<asp:BoundField DataField="FileDescription" HeaderText="Описание" />
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# fileupl + Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
            <br />
            <asp:GridView ID="gvRequestsFilesPhoto" runat="server" AutoGenerateColumns="False">
                <Columns>
					<asp:BoundField DataField="Name" HeaderText="Фото" />
					
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# fileupl + Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
                
				</Columns>
            </asp:GridView>
            <br />
            <br />

             <asp:Button ID="btnPhoto" runat="server" Text="Фото клиента" OnClick="btnPhoto_Click" />
            <asp:Panel ID="pnlPhoto" runat="server" Visible ="false">
            <div>

             <div id="my_camera">
                 <br />
                    </div>
                <input type=button value="Сфотогорафировать" onclick="take_snapshot()">
        
        <br />
        <br />
        <asp:HiddenField ID="hfPhoto2" runat="server" ClientIDMode="Static" />
        <div id="results">

     <%--<img id="imgCam" runat="server" src="'+data_uri+'"/>--%>
     <%--<asp:Image ID="Image1" runat="server" />--%>
       <%-- <asp:Literal ID="Literal1" runat="server"></asp:Literal>--%>
    <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
    
</div>

    <asp:Literal ID="Literal1" runat="server" ClientIDMode="Static"></asp:Literal>

        </div>


          <%--      <input type="button" value="Фото клиента" onclick="window.open('/Webcam/WebCam?reqid=n','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
             <%--   <input type="button" value="Фото клиента" onclick="window.open('/Webcam/WebCam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
            <%--<input type="button" value="Фото клиента" onclick="window.open('webcam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">
            --%>
                <br />
            </asp:Panel>

            <%--<input type="button" value="Фото клиента" onclick="window.open('/Webcam/WebCam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
            <%--<input type="button" value="Фото клиента" onclick="window.open('webcam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
		</div>
					
					</td>

				</tr>
				<tr>
					<td class="auto-style1"></td>
					<td class="auto-style1"></td>
                    <td class="auto-style1">
                        <table>
                            <tr>
                                <td><asp:Label ID="lblOtherLoans" runat="server" Text="Взносы по др.кредитам:</label>"></asp:Label></td>
                                <td>

                                        <asp:TextBox ID="RadNumOtherLoans" runat="server" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="23" Visible="true" ClientIDMode="Static" onchange="demo.RadNumOtherLoans()"></asp:TextBox>
                                        <asp:HiddenField ID="hfOtherLoans2" runat="server" />
                                        <asp:Label ID="lblOtherLoansComment" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;

                                        <%--<asp:RegularExpressionValidator ID="revRadNumOtherLoans" runat="server" ControlToValidate="RadNumOtherLoans" ErrorMessage="RegularExpressionValidator" ValidationExpression="" ForeColor="Red"></asp:RegularExpressionValidator>--%>

                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
			</table>
            
            <br />
             
            <br />
            <asp:LinkButton ID="lbHistory" runat="server" OnClick="lbHistory_Click">История</asp:LinkButton>
            <asp:LinkButton ID="lbBack" runat="server" OnClick="lbBack_Click" Visible="False">История</asp:LinkButton>
            
        <asp:Panel ID="pnlHistory" runat="server" Visible="False">
            <asp:GridView ID="gvHistory" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Status" HeaderText = "Статус" />
                    <asp:BoundField DataField="Date" HeaderText = "Дата" dataformatstring="{0:dd/MM/yyyy HH:mm}"/>
                    <asp:BoundField DataField="Note" HeaderText = "Примечание"/>
                    <asp:BoundField DataField="FIO" HeaderText = "Пользователь"/>
                </Columns>
            </asp:GridView>
            <br />
            
            <br />
        </asp:Panel>
            <br />
            <asp:Button ID="btnGetScoreBee" runat="server" OnClick="btnGetScoreBee_Click" Text="Получить скорбалл от Билайн" />
		</asp:Panel>
        

		<asp:TextBox ID="tbNote" runat="server" ClientIDMode="Static" TextMode="MultiLine" Width="0" Height="0"></asp:TextBox>

		<br />
		<asp:Panel ID="pnlMenuRequest" runat="server">
             <table>
                <tr>
                    <td></td>
                    <td>Поменять статус:</td>
                    <td></td>
                    <td><asp:Button ID="btnNoConfirm" runat="server" Text="Не подтверждено" onClientclick="myComment()" Visible="false" OnClick="btnNoConfirm_Click"/></td>
                    <td></td>
                    <td><asp:Button ID="btnFix" runat="server" Text="Исправить" onClientclick="myComment()" Visible="False" OnClick="btnFix_Click"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnFixed" runat="server" Text="Исправлено" onClientclick="myComment()" Visible="False" OnClick="btnFixed_Click" /></td>
                    <td></td>
                    <td><asp:Button ID="btnConfirm" runat="server" Text="Подтверждено" OnClick="btnConfirm_Click" Visible="false" onClientclick="myComment()"/></td>
                    <td></td>
                    <td><asp:Button ID="btnApproved" runat="server" Text="Утверждено" Visible="false" onClientclick="myComment()" OnClick="btnApproved_Click"/></td>
                    <td></td>
                    <td><asp:Button ID="btnSignature" runat="server" Text="К подписи" Visible="false" onClientclick="myComment()" OnClick="btnSignature_Click" /></td>
                    <td></td>
                    <td><button ID="btnCancelReq" runat="server" type="button" data-toggle="modal" data-target="#exampleModal">Отменено</button></td>
                    <td></td>
                    <td><button ID="btnCancelReqExp" runat="server" type="button" data-toggle="modal" data-target="#exampleModal2">Отказано</button></td>
                    <td></td>
					<td><asp:Button ID="btnIssue" runat="server" Text="Выдано" OnClick="btnIssue_Click" Visible="false" onClientclick="myComment()"/></td>
                    <td></td>
                    <td><asp:Button ID="btnReceived" runat="server" Text="Принято" Visible="false" onClientclick="myComment()" OnClick="btnReceived_Click"/></td>
                    <td></td>
                    <td><asp:Button ID="btnInProcess" runat="server" Text="В обработке" Visible="false" onClientclick="myComment()" OnClick="btnInProcess_Click" /></td>
                    <td></td>
                    <td><asp:Button ID="btnCancelIssue" runat="server" Text="Отмена выдачи" Visible="false" onClientclick="myComment()" OnClick="btnCancelIssue_Click" /></td>
                    <td></td>
                    
                </tr>
            </table><br /><br />
			<table>
				<tr>
                    <td></td>
					<td></td>
					<td></td>
					<td><asp:Button ID="btnAgreement" runat="server" Text="Договор" Visible="False" OnClick="btnAgreement_Click" OnClientClick="openNewWin();"/></td>
					<td></td>
                    <td><asp:Button ID="btnSozfondAgree" runat="server" Text="Заявление-Согласие" OnClientClick="openNewWin();"  Visible="False" OnClick="btnSozfondAgree_Click" /></td>
					<td></td>
					<td><asp:Button ID="btnReceptionAct" runat="server" Text="Акт приема-передачи" OnClick="btnReceptionAct_Click" Visible="False" /></td>
					<td></td>
					<td><asp:Button ID="btnPledgeAgreement" runat="server" Text="Договор о залоге" OnClick="btnPledgeAgreement_Click" Visible="False" /></td>
					<td></td>
					<td><asp:Button ID="btnProffer" runat="server" Text="Предложение-ЗП/ИП" OnClick="btnProffer_Click" Visible="False" OnClientClick="openNewWin();"/></td>
                    <td></td>
                    <td><asp:Button ID="btnComment" runat="server" Text="Комментарий" onClientclick="myComment()" OnClick="btnComment_Click" Visible="False" /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriod" runat="server" Text="Отчет" Visible="False" OnClientClick="openNewWin();" OnClick="btnForPeriod_Click"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriodWithHistory" runat="server" Text="Отчет 2" Visible="False" OnClientClick="openNewWin();" OnClick="btnForPeriodWithHistory_Click"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriodWithProducts" runat="server" Text="Отчет 3" OnClientClick="openNewWin();" OnClick="btnForPeriodWithProducts_Click" Visible="False" /></td>
                    <td></td>
                    <td><asp:Button ID="btnApplicationForm" runat="server" Text="Заявление-Анкета" OnClientClick="openNewWin();" Visible="False" OnClick="btnApplicationForm_Click" /></td>
				</tr>
                <tr>
                    <td><br /></td>
                </tr>
                <tr>
					<td></td>
					<td><asp:Button ID="btnNewRequest" runat="server" OnClick="btnNewRequest_Click" Text="Новая заявка" /></td>
					<td></td>
					<td><asp:Button ID="btnSendCreditRequest" runat="server" Text="Сохранить" OnClick="btnSendCreditRequest_Click" ValidationGroup="SaveRequest" Visible="False" onClientclick="myComment()"/></td>
					<td>
                        &nbsp;</td>
                    <td><asp:Button ID="btnCloseRequest" runat="server" Text="Закрыть" Style="height: 26px" OnClick="btnCloseRequest_Click" Visible ="false"/></td>
					<td></td>
                    
                </tr>
			</table>
		</asp:Panel>
	</asp:Panel>

 



	<asp:Panel ID="pnlMenuCustomer" runat="server" Visible="False" CssClass="panels">
		<table>
            <tr>
                <td>&nbsp;</td>
                <td>ИНН:</td>
                <td>&nbsp;</td>
                <td>Серия№:</td>
            </tr>
			<tr>
				<td>&nbsp;</td>
				<td class="auto-style1">
					<asp:TextBox ID="tbSearchINN" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfSearchCustomer" runat="server" ControlToValidate="tbSearchINN" ErrorMessage="Ошибка!" ValidationGroup="SearchCustomer"></asp:RequiredFieldValidator>
				</td>
                <td>&nbsp;</td>
                   <td>
                       <asp:TextBox ID="tbSerialN" runat="server"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="rfSerialN" runat="server" ControlToValidate="tbSerialN" ErrorMessage="Ошибка!" ValidationGroup="SearchCustomer"></asp:RequiredFieldValidator>
                   </td>
				<td>&nbsp;&nbsp;</td>
				<td>
					<asp:Button ID="btnSearchClient" runat="server" Text="Поиск" OnClick="btnSearchClient_Click" ValidationGroup="SearchCustomer" /></td>
				<td>&nbsp;</td>
				<td>
					<asp:Button ID="btnNewCustomer" runat="server" Text="Новый клиент" OnClick="btnNewCustomer_Click" /></td>
				<td>&nbsp;</td>
				<td>
					<asp:Button ID="btnCredit" runat="server" Text="Выбрать клиента" Enabled="False" OnClick="btnCredit_Click" Visible="False" /></td>
			</tr>
			<tr>

				<td>&nbsp;</td>
				<td><asp:HiddenField ID="hfUserID" runat="server" /></td>
				<td>
					<asp:HiddenField ID="hfCustomerID" runat="server" Value="noselect" />
				</td>
				<td>
					<asp:HiddenField ID="hfGuarantorID" runat="server" Value="noselect" />
				</td>
				<td>
					<asp:HiddenField ID="hfCreditID" runat="server" />
				</td>
				<td></td>
				<td>
					<asp:HiddenField ID="hfRequestID" runat="server" />
				</td>
				<td>
                    <asp:HiddenField ID="hfChooseClient" runat="server" />
                </td>
				<td>
                    <asp:HiddenField ID="hfRequestStatus" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="hfAgentRoleID" runat="server" />
                </td>
                <td><asp:HiddenField ID="hfRequestIDDel" runat="server" Value="-1" />
                    
                </td>
			</tr>


		</table>
	</asp:Panel>

	<asp:Panel ID="pnlCustomer" runat="server" Visible="False" CssClass="panels">
        <asp:GridView ID="gvCustomers" runat="server"></asp:GridView>
		<h3>Форма клиента</h3>
		<table>
			<tr>
				<td>&nbsp;</td>
				<td>
					<label>Данные клиента</label></td>
				<td style="width:258px">
                    <asp:HiddenField ID="hfIsNewCustomer" runat="server" />
                </td>
				<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				<td>
					<label>Паспортные данные</label></td>
				<td></td>
				<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td>
					<label>Фамилия:</label></td>
				<td>
					<asp:TextBox ID="tbSurname" runat="server" TabIndex="1"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfSurname" runat="server" ControlToValidate="tbSurname" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="tbSurname" ErrorMessage="в фио все буквы должны быть на русском" ValidationExpression="^[а-яА-ЯёЁъЪ -]{1,32}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Вид паспорта:</label></td>
				<td>
                    
					<asp:DropDownList ID="ddlDocumentTypeID" class="chosen" runat="server" DataTextField="TypeName" DataValueField="DocumentTypeID" TabIndex="11" Width="260px" OnSelectedIndexChanged="ddlDocumentTypeID_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="3">Паспорт (серия AN) образца 2004 года</asp:ListItem>
                        <asp:ListItem Value="14">Биометрический паспорт (серия ID)</asp:ListItem>
                    </asp:DropDownList>
                   
					<asp:RequiredFieldValidator ID="rfDocumentType" runat="server" ControlToValidate="ddlDocumentTypeID" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator></td>
				<td></td>
				<td>
					<label>Кем выдан:</label></td>
				<td>
					<asp:TextBox ID="tbIssueAuthority" runat="server" ValidationGroup="SaveCustomers" TabIndex="19"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbIssueAuthority" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td></td>
				<td>
					<label>Имя:</label></td>
				<td>
					<asp:TextBox ID="tbCustomerName" runat="server" TabIndex="2"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfCustomerName" runat="server" ControlToValidate="tbCustomerName" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="tbCustomerName" ErrorMessage="в фио все буквы должны быть на русском" ValidationExpression="^[а-яА-ЯёЁъЪ -]{1,32}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                </td>
				<td></td>
				<td>
					<label>Серия и номер:</label></td>
				<td>

					     <asp:TextBox ID="tbDocumentSeries" runat="server" TabIndex="12" AutoPostBack="True" OnTextChanged="tbDocumentSeries_TextChanged"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfDocumentSeries" runat="server" ControlToValidate="tbDocumentSeries" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					      
                       
					<asp:RegularExpressionValidator ID="revDocumentSeries" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbDocumentSeries" ValidationExpression="[A-Z][A-Z]\d\d\d\d\d\d\d" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Дата выдачи:</label></td><td>
					<asp:TextBox ID="tbIssueDate" runat="server" ClientIDMode="Static" TabIndex="20" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbIssueDate" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbIssueDate" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
			</tr>
			<tr>
				<td class="auto-style1"></td>
				<td class="auto-style1">
					<label>Отчество:</label></td>
				<td class="auto-style1">
					<asp:TextBox ID="tbOtchestvo" runat="server" TabIndex="3"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="tbOtchestvo" ErrorMessage="в фио все буквы должны быть на русском" ValidationExpression="^[а-яА-ЯёЁъЪ -]{1,32}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>--%>
                </td>
				<td class="auto-style1"></td>
				<td class="auto-style1"></td>
				<td class="auto-style1"></td>
				<td class="auto-style1"></td>
				<td class="auto-style1">
                    <asp:RadioButtonList ID="rbtnlistValidTill" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtnlistValidTill_SelectedIndexChanged">
                        <asp:ListItem Selected="True">Дата окончания</asp:ListItem>
                        <asp:ListItem>Бессрочный</asp:ListItem>
                    </asp:RadioButtonList>
				</td>
				<td class="auto-style1">
   					<asp:TextBox ID="tbValidTill" runat="server" ClientIDMode="Static" TabIndex="20" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvValidTill" runat="server" ControlToValidate="tbValidTill" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="revValidTill" runat="server" ControlToValidate="tbValidTill" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>

				</td>
			</tr>
			<tr>
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
			<tr>
				<td></td>
				<td>
					<label>ИНН:</label></td>
				<td>
					<asp:TextBox ID="tbIdentificationNumber" runat="server" TabIndex="4" AutoPostBack="True" OnTextChanged="tbIdentificationNumber_TextChanged"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfIdentificationNumber" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbIdentificationNumber" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="rfedentificationNumber" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers" ControlToValidate="tbIdentificationNumber" ValidationExpression="\d\d\d\d\d\d\d\d\d\d\d\d\d\d"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td>
					<asp:RadioButtonList ID="rbtIsResident" runat="server" RepeatDirection="Horizontal" Visible="False">
						<asp:ListItem Selected="True">Резидент</asp:ListItem>
						<asp:ListItem>Неризидент</asp:ListItem>
					</asp:RadioButtonList></td>
				<td></td>
				<td></td>
				<td>
					<label>Пол</label></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td></td>
				<td>
					<asp:DropDownList ID="ddlNationalityID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="5"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfCountry" Visible="false" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlNationalityID" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td>
					<asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtSex_SelectedIndexChanged">
						<asp:ListItem Selected="True">Мужской</asp:ListItem>
						<asp:ListItem>Женский</asp:ListItem>
					</asp:RadioButtonList></td>
				<td></td>
				<td></td>
				<td></td>
				<td>
					<asp:DropDownList ID="ddlBirthCountryID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="21"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfBirthofCountry" Visible="false" runat="server" ControlToValidate="ddlBirthCountryID" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td>
					<label>Дата рождения:</label></td>
				<td>
					<asp:TextBox ID="tbDateOfBirth" runat="server" ClientIDMode="Static" TabIndex="13" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfDateOfBirth" runat="server" ControlToValidate="tbDateOfBirth" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="tbDateOfBirth" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td></td>
				<td>
					<asp:DropDownList ID="ddlBirthCityName" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="22" Visible="False"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfBirthCityName" Visible="false" runat="server" ControlToValidate="ddlBirthCityName" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td>&nbsp</td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td>
					<label>Адрес по прописке</label></td>
				<td></td>
				<td></td>
				<td colspan="2">
					<label>Адрес фактического проживания:</label>&nbsp;&nbsp;<asp:CheckBox ID="chkbxAsRegistration" runat="server" OnCheckedChanged="chkbxAsRegistration_CheckedChanged" Text="как в прописке" AutoPostBack="True" />
                </td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td></td>
				<td>
					<asp:DropDownList ID="ddlRegistrationCountryID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="6"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfvRegistrationCountryID" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlRegistrationCountryID" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td></td>
				<td>
					<asp:DropDownList ID="ddlResidenceCountryID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="14"></asp:DropDownList></td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td>
					<label>Нас.пункт:</label></td>
				<td>
					<asp:DropDownList ID="ddlRegistrationCityName" class="chosen" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="7"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfvRegistrationCityName" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlRegistrationCityName" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td>
					<label>Нас.пункт:</label></td>
				<td>
					<asp:DropDownList ID="ddlResidenceCityName" class="chosen" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="15"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfvResidenceCityName" runat="server" ControlToValidate="ddlResidenceCityName" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
                <td><label>Телефон:</label></td>
				<td>
                    <asp:TextBox ID="tbContactPhone" runat="server"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="rfvContactPhone" runat="server" ControlToValidate="tbContactPhone" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="revContactPhone" runat="server" ControlToValidate="tbContactPhone" CssClass="SaveCustomers" ErrorMessage="+996 ### ######" ValidationExpression="^\+\d{3}\ \d{3}\ \d{6}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                </td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td><label>Улица:</label></td>
				<td>
					<asp:TextBox ID="tbRegistrationStreet" runat="server" TabIndex="8"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvRegistrationStreet" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbRegistrationStreet" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td><label>Улица:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceStreet" runat="server" TabIndex="16"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvResidenceStreet" runat="server" ControlToValidate="tbResidenceStreet" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td><label>Дом:</label></td>
				<td>
					<asp:TextBox ID="tbRegistrationHouse" runat="server" TabIndex="9"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvRegistrationHouse" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbRegistrationHouse" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revRegistrationHouse" runat="server" ControlToValidate="tbRegistrationHouse" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Дом:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceHouse" runat="server" TabIndex="17"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvResidenceHouse" runat="server" ControlToValidate="tbResidenceHouse" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revResidenceHouse" runat="server" ControlToValidate="tbResidenceHouse" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td></td>
				<td><label>Кв:</label></td>
				<td>
					<asp:TextBox ID="tbRegistrationFlat" runat="server" TabIndex="10"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rfvRegistrationFlat" runat="server" ControlToValidate="tbRegistrationFlat" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Кв:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceFlat" runat="server" TabIndex="18"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revResidenceFlat" runat="server" ControlToValidate="tbResidenceFlat" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>&nbsp</td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>&nbsp</td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
			</tr>
			<tr>
				<td>&nbsp</td>
			</tr>
		</table>
        <asp:GridView ID="gvPositions2" runat="server"></asp:GridView>
        <br />
		<asp:Panel ID="pnlSaveCustomer" runat="server">
			<table>
				<tr>
					<td>&nbsp;</td>
					<td><asp:Button ID="btnSaveCustomer" runat="server" Text="Сохранить" ValidationGroup="SaveCustomers" OnClick="btnSaveCustomer_Click" /></td>
					<td>&nbsp;</td>
					<td><asp:Button ID="btnCancel" runat="server" Text="Отменить" OnClick="btnCancel_Click" /></td>
					<td>&nbsp;</td>
					<td></td>
				</tr>
			</table>
		</asp:Panel>

	</asp:Panel>
	<!-pnlUser->


</asp:Panel>

       <!---------------------------------------------------------------------------------------------------------------------------->
    <!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
        <h4 class="modal-title" id="exampleModalLabel">Вы действительно хотите отменить заявку?</h4>
      </div>
        
      <div class="modal-body">
          
        <div class="form-group">
            
            <label for="message-text" class="col-form-label">Комментарий:</label>
            <br /><br />
           <%-- <textarea class="form-control" id="message-text"></textarea>--%>
            <asp:TextBox ID="tbNoteCancelReq" runat="server" ClientIDMode="Static" TextMode="MultiLine" Width="568" Height="120" ></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Объязательно заполните комментарий" ForeColor="#FF3300"></asp:Label><br />
            <asp:RequiredFieldValidator ID="rqftbNote" runat="server" ErrorMessage="Объязательно заполните комментарий" ControlToValidate="tbNoteCancelReq" ValidationGroup="cancelRequest" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
        
      </div>
        <br /><br />
      <div class="modal-footer">
        
        <%--<button type="button" class="btn btn-primary" runat="server">Save changes</button>--%>
        <asp:Button Width="100" ID="btnCancelReq2" runat="server" Text="Да"  Visible="true"  ClientIDMode="Static" data-toggle="modal" data-target="#exampleModal" OnClick="btnCancelReq_Click" ValidationGroup="cancelRequest" />
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <button style="Width:100px" type="button" data-dismiss="modal">Нет</button>
      </div>
    </div>
  </div>
</div>
    <!---------------------------------------------------------------------------------------------------------------------------->
       <!-- Modal -->
<div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
        <h4 class="modal-title" id="exampleModalLabel2">Вы действительно хотите отказать заявку?</h4>
      </div>
        
      <div class="modal-body">
          
        <div class="form-group">
            
            <label for="message-text" class="col-form-label">Комментарий:</label>
            <br /><br />
           <%-- <textarea class="form-control" id="message-text"></textarea>--%>
            <asp:TextBox ID="tbNoteCancelReqExp" runat="server" ClientIDMode="Static" TextMode="MultiLine" Width="568" Height="120" ></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="Объязательно заполните комментарий" ForeColor="#FF3300"></asp:Label><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Объязательно заполните комментарий" ControlToValidate="tbNoteCancelReqExp" ValidationGroup="cancelRequest2" ForeColor="#FF3300"></asp:RequiredFieldValidator>
          </div>
        
      </div>
        <br /><br />
      <div class="modal-footer">
        
        <%--<button type="button" class="btn btn-primary" runat="server">Save changes</button>--%>
        <asp:Button Width="100" ID="btnCancelReqExp2" runat="server" Text="Да"  Visible="true"  ClientIDMode="Static" data-toggle="modal" data-target="#exampleModal2" OnClick="btnCancelReqExp_Click" ValidationGroup="cancelRequest2" />
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <button style="Width:100px" type="button" data-dismiss="modal">Нет</button>
      </div>
    </div>
  </div>
</div>
<!---------------------------------------------------------------------------------------------------------------------------->
  

</div>



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





<script>
     $('#tbDate1b').change(function () {
        dt1s = $('#tbDate1b').val();
        dt2s = $('#tbDate2b').val();
            if (dt1s.length == 10)
            {
                dt1s = dt1s.replace('.', '/');
                dt2s = dt2s.replace('.', '/');

                dt1s = dt1s.replace('.', '/');
                dt2s = dt2s.replace('.', '/');
              
                var date = dt1s;
var d=new Date(date.split("/").reverse().join("-"));
var dd=d.getDate();
var mm=d.getMonth()+1;
var yy=d.getFullYear();
                dt1 = new Date(yy + "/" + mm + "/" + dd);

                var date= dt2s;
var d=new Date(date.split("/").reverse().join("-"));
var dd=d.getDate();
var mm=d.getMonth()+1;
var yy=d.getFullYear();
                dt2 = new Date(yy + "/" + mm + "/" + dd);


                if (dt1 > dt2) { $('#tbDate2b').val($('#tbDate1b').val()); }

                

                var dt2i = new Date();
                dt2i = add_months(dt1, 6)
                if (dt2i < dt2)
                {
                    var date = add_months(dt2i, 1)
                    yr      = date.getFullYear(),
                    month   = date.getMonth() < 10 ? '0' + date.getMonth() : date.getMonth(),
                    day     = date.getDate()  < 10 ? '0' + date.getDate()  : date.getDate(),
                    newDate = day + '.' + month + '.' + yr;
                    $('#tbDate2b').val(newDate);
                }
            }

    });


     $('#tbDate2b').change(function () {
        dt1s = $('#tbDate1b').val();
        dt2s = $('#tbDate2b').val();
            if (dt1s.length == 10)
            {
                dt1s = dt1s.replace('.', '/');
                dt2s = dt2s.replace('.', '/');

                dt1s = dt1s.replace('.', '/');
                dt2s = dt2s.replace('.', '/');
              
                var date = dt1s;
var d=new Date(date.split("/").reverse().join("-"));
var dd=d.getDate();
var mm=d.getMonth()+1;
var yy=d.getFullYear();
                dt1 = new Date(yy + "/" + mm + "/" + dd);

                var date= dt2s;
var d=new Date(date.split("/").reverse().join("-"));
var dd=d.getDate();
var mm=d.getMonth()+1;
var yy=d.getFullYear();
                dt2 = new Date(yy + "/" + mm + "/" + dd);

                if (dt1 > dt2) { $('#tbDate1b').val($('#tbDate2b').val()); }

                var dt2i = new Date();
                dt2i = add_months(dt2, -6)
                if (dt2i > dt1)
                {
                    var date = add_months(dt2i, 1)
                    yr      = date.getFullYear(),
                    month   = date.getMonth() < 10 ? '0' + date.getMonth() : date.getMonth(),
                    day     = date.getDate()  < 10 ? '0' + date.getDate()  : date.getDate(),
                    newDate = day + '.' + month + '.' + yr;
                    $('#tbDate1b').val(newDate);
                }
            }

    });




 function add_months(dt, n) 
 {
   return new Date(dt.setMonth(dt.getMonth() + n));      
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

<script>
    ; (function () {
        var demo = window.demo = {};

        demo.valueChanged = function (sender, args) {
            price = RadNumTbTotalPrice.get_value();
            amount10 = Math.round(10 * price / 100);
            RadNumTbAmountOfDownPayment.set_value(amount10);
            request = price - amount10;
            RadNumTbRequestSumm.set_value(request);
            n = parseInt(document.getElementById("ddlRequestPeriod").options[document.getElementById("ddlRequestPeriod").selectedIndex].value);
            stavka = parseInt(document.getElementById('<%=ddlRequestRate.ClientID%>').value);
            s = RadNumTbTotalPrice.get_value() - amount10;
            ii = stavka / 12 / 100;
            k = (((Math.pow((1 + ii), n)) * (ii)) * s) / ((Math.pow((1 + (ii)), n)) - 1);
        }
        demo.valueChanged2 = function (sender, args) {
            price = RadNumTbTotalPrice.get_value();
            stavka = parseFloat(document.getElementById('<%=ddlRequestRate.ClientID%>').value);
            //
            amount1 = 0;
            if (stavka == 0.00) amount1 = 0.00;
            if ((stavka == 25.00) || (stavka == 30.00))
            {
                if (price <= 50000) amount1 = 0;
                else amount1 = 10;
            }
            if ((stavka == 27) || (stavka == 29)) amount1 = 10;
            //
            amount10 = Math.round(amount1 * price / 100);
            amount = RadNumTbAmountOfDownPayment.get_value();
            // if (amount >= amount10) { request = price - amount; }
            // else { request = ""; }
            if (stavka == 0)
            { 
                request = price - amount;
            }
            else
            {
                if (amount >= amount10) { request = price - amount; }
                else { request = ""; }
            }

            n = parseInt(document.getElementById("ddlRequestPeriod").options[document.getElementById("ddlRequestPeriod").selectedIndex].value);
            
            s = RadNumTbTotalPrice.get_value() - amount;
            ii = stavka / 12 / 100;
            //k = (((Math.pow((1 + ii), n)) * (ii)) * s) / ((Math.pow((1 + (ii)), n)) - 1);
            //RadNumTbMonthlyInstallment.set_value(k.toFixed(0));
            RadNumTbRequestSumm.set_value(request);
        }

        demo.valueChanged3 = function (sender, args) {
            //SumMonthSalary = RadNumTbSumMonthSalary.get_value();
            //CountSalary = parseInt(document.getElementById("ddlMonthCount").options[document.getElementById("ddlMonthCount").selectedIndex].value);
            //AverageMonthSalary = SumMonthSalary / CountSalary
            //RadNumTbAverageMonthSalary.set_value(AverageMonthSalary.toFixed(0));
            
            SumMonthSalary = parseFloat(document.getElementById("RadNumTbSumMonthSalary").value.replace(/,/, '.'));
            CountSalary = parseInt(document.getElementById("ddlMonthCount").options[document.getElementById("ddlMonthCount").selectedIndex].value);
            AverageMonthSalary = SumMonthSalary / CountSalary
            document.getElementById("RadNumTbSumMonthSalary").value = SumMonthSalary.toFixed(2).replace('.', ',');
            document.getElementById("RadNumTbAverageMonthSalary").value = AverageMonthSalary.toFixed(2).replace('.', ',');

        }
    /**********************/

        
        demo.RadNumTbAdditionalIncome = function (sender, args) {
            AdditionalIncome = parseFloat(document.getElementById("RadNumTbAdditionalIncome").value.replace(/,/, '.'));
            document.getElementById("RadNumTbAdditionalIncome").value = AdditionalIncome.toFixed(2).replace('.', ',');
          
        }

    /**********************/



        demo.cvalueChanged = function (sender, args) {
            price = RadNumTbTotalPrice2.get_value();
            amount10 = 10 * price / 100;
            RadNumTbAmountOfDownPayment2.set_value(amount10);
            request = price - amount10;
            RadNumTbRequestSumm.set_value2(request);

        }
        demo.cvalueChanged2 = function (sender, args) {
            price = RadNumTbTotalPrice2.get_value();
            amount10 = 10 * price / 100;
            amount = RadNumTbAmountOfDownPayment2.get_value();
            if (amount >= amount10) { request = price - amount; }
            else { request = ""; }
            RadNumTbRequestSumm2.set_value(request);
        }
        /**********************************/
         demo.rateChanged = function (sender, args) {
             rate = ddlRequestRate.get_value();
            if (rate.Text == "0.00")
            {
                //ddlRequestPeriod.Items.Clear();
                //ddlRequestPeriod.Items.Add("3");
                //ddlRequestPeriod.Items.Add("6");
            }
            if (rate.Text == "25.00")
            {
                //ddlRequestPeriod.Items.Clear();
                //ddlRequestPeriod.Items.Add("15");
            }
            if ((rate.Text == "30.00") || (ddlRequestRate.Text == "29.00"))
            {
                //ddlRequestPeriod.Items.Clear();
                //for (int i = 3; i < 36; i++)
                //{
                //    ddlRequestPeriod.Items.Add(i.ToString());
                //}
            }
//            RadNumTbRequestSumm2.set_value(request);
        }


    /************************/


        demo.txtPrice = function (sender, args) {
            txtPrice = parseFloat(document.getElementById("txtPrice").value.replace(/,/, '.'));
            document.getElementById("txtPrice").value = txtPrice.toFixed(2).replace('.', ',');
        }

        demo.RadNumTbAdditionalIncome = function (sender, args) {
            AdditionalIncome = parseFloat(document.getElementById("RadNumTbAdditionalIncome").value.replace(/,/, '.'));
            document.getElementById("RadNumTbAdditionalIncome").value = AdditionalIncome.toFixed(2).replace('.', ',');
        }

        demo.TbAmountOfDownPayment = function (sender, args) {
            TbAmountOfDownPayment = parseFloat(document.getElementById("TbAmountOfDownPayment").value.replace(/,/, '.'));
            document.getElementById("TbAmountOfDownPayment").value = TbAmountOfDownPayment.toFixed(2).replace('.', ',');
        }

        demo.RadNumOtherLoans = function (sender, args) {
            RadNumOtherLoans = parseFloat(document.getElementById("RadNumOtherLoans").value.replace(/,/, '.'));
            document.getElementById("RadNumOtherLoans").value = RadNumOtherLoans.toFixed(2).replace('.', ',');
        }

        demo.RadNumTbMinRevenue = function (sender, args) {
            RadNumTbMinRevenue = parseFloat(document.getElementById("RadNumTbMinRevenue").value.replace(/,/, '.'));
            document.getElementById("RadNumTbMinRevenue").value = RadNumTbMinRevenue.toFixed(2).replace('.', ',');
        }

        demo.RadNumTbMaxRevenue = function (sender, args) {
            RadNumTbMaxRevenue = parseFloat(document.getElementById("RadNumTbMaxRevenue").value.replace(/,/, '.'));
            document.getElementById("RadNumTbMaxRevenue").value = RadNumTbMaxRevenue.toFixed(2).replace('.', ',');
        }
        demo.RadNumTbСostPrice = function (sender, args) {
            RadNumTbСostPrice = parseFloat(document.getElementById("RadNumTbСostPrice").value.replace(/,/, '.'));
            document.getElementById("RadNumTbСostPrice").value = RadNumTbСostPrice.toFixed(2).replace('.', ',');
        }
        demo.RadNumTbOverhead = function (sender, args) {
            RadNumTbOverhead = parseFloat(document.getElementById("RadNumTbOverhead").value.replace(/,/, '.'));
            document.getElementById("RadNumTbOverhead").value = RadNumTbOverhead.toFixed(2).replace('.', ',');

        }
        demo.RadNumTbFamilyExpenses = function (sender, args) {
            RadNumTbFamilyExpenses = parseFloat(document.getElementById("RadNumTbFamilyExpenses").value.replace(/,/, '.'));
            document.getElementById("RadNumTbFamilyExpenses").value = RadNumTbFamilyExpenses.toFixed(2).replace('.', ',');

        }
        demo.RadNumTbRevenueAgro = function (sender, args) {
            RadNumTbRevenueAgro = parseFloat(document.getElementById("RadNumTbRevenueAgro").value.replace(/,/, '.'));
            document.getElementById("RadNumTbRevenueAgro").value = RadNumTbRevenueAgro.toFixed(2).replace('.', ',');
        }
        demo.RadNumTbRevenueMilk = function (sender, args) {
            RadNumTbRevenueMilk = parseFloat(document.getElementById("RadNumTbRevenueMilk").value.replace(/,/, '.'));
            document.getElementById("RadNumTbRevenueMilk").value = RadNumTbRevenueMilk.toFixed(2).replace('.', ',');
        }
        demo.RadNumTbOverheadAgro = function (sender, args) {
            RadNumTbOverheadAgro = parseFloat(document.getElementById("RadNumTbOverheadAgro").value.replace(/,/, '.'));
            document.getElementById("RadNumTbOverheadAgro").value = RadNumTbOverheadAgro.toFixed(2).replace('.', ',');
        }
        demo.RadNumTbAddOverheadAgro = function (sender, args) {
            RadNumTbAddOverheadAgro = parseFloat(document.getElementById("RadNumTbAddOverheadAgro").value.replace(/,/, '.'));
            document.getElementById("RadNumTbAddOverheadAgro").value = RadNumTbAddOverheadAgro.toFixed(2).replace('.', ',');
        }
        demo.RadNumTbFamilyExpensesAgro = function (sender, args) {
            RadNumTbFamilyExpensesAgro = parseFloat(document.getElementById("RadNumTbFamilyExpensesAgro").value.replace(/,/, '.'));
            document.getElementById("RadNumTbFamilyExpensesAgro").value = RadNumTbFamilyExpensesAgro.toFixed(2).replace('.', ',');
        }




    /**********************/

    })();
</script>

<script type="text/javascript">
    function pageLoad() {
        RadNumTbTotalPrice = $find("<%=RadNumTbTotalPrice.ClientID%>");
    
        RadNumTbRequestSumm = $find("<%=RadNumTbRequestSumm.ClientID%>");
        
            RadNumTbRequestSumm = $find("<%=RadNumTbRequestSumm.ClientID%>");
        
                RadNumTbMonthlyInstallment = $find("<%=RadNumTbMonthlyInstallment.ClientID%>");
            RadNumTbSumMonthSalary = $find("<%=RadNumTbSumMonthSalary.ClientID%>");
        RadNumTbAverageMonthSalary = $find("<%=RadNumTbAverageMonthSalary.ClientID%>");
        /*******************/
         ddlRequestRate = $find("<%=ddlRequestRate.ClientID%>");
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
      </script>


</asp:Content>