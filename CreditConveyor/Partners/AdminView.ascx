<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminView.ascx.cs" Inherits="rysbek.Modules.Beeline.AdminView" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" Skin="Telerik" />
<link href="<%=ControlPath %>bootstrap/css/bootstrap.css" rel="stylesheet" />
<link rel="stylesheet" href="<%=ControlPath %>css/chosen.css">
<script src="<%=ControlPath %>js/chosen.jquery.min.js"></script>
<script src="<%=ControlPath %>js/chosen.init.js"></script>
<script src="<%=ControlPath %>/js/datepicker-ru.js"></script>
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
<div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<asp:Panel ID="pnlUser" runat="server">
	<p class="right" style="font-size: 14px;">
        <asp:Label ID="lblGroup" runat="server" Text="Group" style="font-size:24px"></asp:Label><br />
		<asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbLogout" runat="server" OnClick="lbLogout_Click">Logout</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbSettings" runat="server" OnClick="lbSettings_Click">Настройки</asp:LinkButton>
	</p>
	<br /> <br />
    
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
                <td><asp:Button ID="btnSavePassword" runat="server" Text="Сохранить" OnClick="btnSavePassword_Click" ValidationGroup="changePassowrd" /></td>
                <td><asp:Button ID="btnCancelPassword" runat="server" Text="Закрыть" OnClick="btnCancelPassword_Click" /></td>
                <td></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
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
                             <td><asp:Button ID="btnSaveGroup" runat="server" Text="Сохранить" OnClick="btnSaveGroup_Click" /></td>
                             <td><asp:Button ID="btnCancelSaveGroup" runat="server" Text="Закрыть" OnClick="btnCancelPassword_Click" /></td>
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
    
        
    
        
    <br /><br />
	<asp:Panel ID="pnlCredit" runat="server" CssClass="panels">


		<table class="tblresp">
            <thead>
			<tr>
				<th></th>
                <th>№ заявки</th>
				<th>ИНН</th>
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
				<td data-label = "Фамилия"><asp:TextBox ID="tbSearchRequestSurname" runat="server"></asp:TextBox>&nbsp;</td>
				<td data-label = "Имя"><asp:TextBox ID="tbSearchRequestName" runat="server"></asp:TextBox>&nbsp;</td>
                <td data-label = "Дата"><asp:TextBox ID="tbDate1b" runat="server" ClientIDMode="Static" ></asp:TextBox>&nbsp;</td>
				<td data-label = ""><asp:TextBox ID="tbDate2b" runat="server" ClientIDMode="Static" Visible="False"></asp:TextBox>&nbsp;</td>
				<td data-label = ""><asp:Button ID="btnSearchRequest" runat="server" OnClick="btnSearchRequest_Click" Text="Найти" ValidationGroup="SearchRequest" /></td>
			</tr>
            <tr>
				<td data-label = "" colspan="7"><b>Статусы заявки</b>
                    <asp:CheckBoxList ID="chkbxlistStatus" runat="server" RepeatDirection="Horizontal" Visible="False" >
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
                    <asp:CheckBox ID="chkbxSelectAll" runat="server" AutoPostBack="True" Checked="True" OnCheckedChanged="chkbxSelectAll_CheckedChanged" Text="Выделить все" />

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
                        <asp:ListItem Selected="True">Beeshop</asp:ListItem>
                        <asp:ListItem Selected="True">Билайн</asp:ListItem>
                        <asp:ListItem Selected="True">Нано</asp:ListItem>
                    </asp:CheckBoxList></td>
                </td>
            </tr>
            </tbody>
		</table>
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
                            <asp:BoundField DataField="Surname" HeaderText="Фамилия" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Имя" />
                            <asp:BoundField DataField="GroupName" HeaderText="Группа" />
                            <asp:BoundField DataField="OrgName" HeaderText="Орг" />
                            <asp:BoundField DataField="IdentificationNumber" HeaderText="ИНН" />
<%--                            <asp:BoundField DataField="CreditPurpose" HeaderText="Назначение кредита" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" /> --%>
                            <asp:BoundField DataField="ProductPrice" HeaderText="Цена" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="AmountDownPayment" HeaderText="Взнос" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="RequestSumm" HeaderText="Кредит" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="RequestDate" HeaderText="Дата заявки" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="RequestRate" HeaderText="Процент" />
                            <asp:BoundField DataField="RequestStatus" HeaderText="Статус заявки" />
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

	 <asp:Timer ID="MyTimer" runat="server"  Interval="15000" OnTick="MyTimer_Tick" Enabled="false">
	  </asp:Timer>
        <asp:HiddenField ID="hfRequestsRowID" runat="server" />
		<br />
        <asp:Panel ID="pnlBlackList" runat="server" Visible="False">
                                        
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
                                    </asp:Panel>
        <br />       
        <asp:Panel ID="pnlBlackListOrg" runat="server" Visible="False">
                                        
                                        <asp:Label ID="lblBlacListOrg" runat="server" Text="Организация в черном списке!!!" Font-Size="Larger" ForeColor="Red"></asp:Label>
                                        <asp:GridView ID="gvBlackListOrg" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                             <%--<asp:BoundField DataField="Surname" HeaderText="Фамилия" />
                                             <asp:BoundField DataField="PersonName" HeaderText="Имя" />
                                             <asp:BoundField DataField="Otchestvo" HeaderText="Отчество" />--%>
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
        </asp:Panel>
        <br />       
        

        <h3>Форма заявки</h3><asp:HiddenField ID="hfRequestAction" runat="server" />
		<asp:Panel ID="pnlNewRequest" runat="server" Visible="false" CssClass="panels">
			<table>
				<tr>
					<td></td>
					<td colspan="2">Статус кредитной заявки:<asp:Label ID="lblStatusRequest" runat="server" Text="Status"></asp:Label></td>
					<td></td>
					<td></td>
					<td><asp:Label ID="lblMessageClient" runat="server"></asp:Label></td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td></td>
					<td><label>Клиент (счет для погашения)</label></td>
					<td><b class="red"><asp:Label ID="lblAccount" runat="server"></asp:Label></b></td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td></td>
					<td><label>Цель кредита:</label></td>
					<td>
						<asp:TextBox ID="tbCreditPurpose" runat="server" TabIndex="1"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbCreditPurpose" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
					</td>
					<td></td>
					<td><label>Фамилия:</label></td>
					<td><asp:TextBox ID="tbSurname2" runat="server" Enabled="False" TabIndex="5"></asp:TextBox></td>
					<td><asp:Button ID="btnCustomerSearch" runat="server" OnClick="btnCustomerSearch_Click" Text="Выбрать клиента" /></td>
					<td>&nbsp;</td>
					<td></td>
				</tr>
				<tr>
					<td></td>
					<td><label>Продукт:</label>
						</td>
					<td>
                       <%-- <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>--%>
					    <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                            <asp:ListItem>Замат</asp:ListItem>
                        </asp:DropDownList>
                           <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
						
					</td>
					<td></td>
					<td>
						<label>Имя:</label></td>
					<td>
						<asp:TextBox ID="tbCustomerName2" runat="server" Enabled="False" TabIndex="6"></asp:TextBox></td>
					<td><asp:Button ID="btnCustomerEdit" runat="server" Text="Редактировать" OnClick="btnCustomerEdit_Click" Enabled="False" /></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td></td>
					<td>
                        <label>% Cтавка:</label>
					</td>
					<td>
                      <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                            <asp:DropDownList ID="ddlRequestRate" runat="server" OnSelectedIndexChanged="ddlRequestRate_SelectedIndexChanged" AutoPostBack="True">
                               <asp:ListItem Value="0.00">0.00</asp:ListItem>
                               <asp:ListItem Value="29.90" Selected="True">29.90</asp:ListItem>
                           </asp:DropDownList>
                       <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
					</td>
					<td></td>
					<td><label>Отчество</label></td>
					<td><asp:TextBox ID="tbOtchestvo2" runat="server" Enabled="False" TabIndex="7"></asp:TextBox></td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td></td>
					<td>
                        <label>Срок кредита:</label>
					</td>
					<td>
                       <%--  <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>--%>
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
						</asp:DropDownList>
                      <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRequestPeriod" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                        </td>
					<td></td>
					<td><label>ИНН:</label></td>
					<td><asp:TextBox ID="tbINN2" runat="server" Enabled="False" TabIndex="8"></asp:TextBox></td>
					<td></td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td><br /></td>
                    <td><label>Комиссия:</label></td>
                    <td><asp:Label ID="lblCommission" runat="server" Text="0"></asp:Label></td>
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

			<table>
							<tr>
								<td>
                                   <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>--%>
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

                                            <asp:TemplateField HeaderText = "imei2-код">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductImei2" runat="server" Text='<%# Eval("ProductImei2")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProductImei2" runat="server" Text='<%# Eval("ProductImei2")%>'></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProductImei2" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText = "Цена">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txtPrice" Width="150px" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" TabIndex="13" ReadOnly="false" Text='<%# Eval("Price")%>'></telerik:RadNumericTextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="txtPrice" Width="150px" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" TabIndex="13" ReadOnly="false"></telerik:RadNumericTextBox>
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
                                        <%--    </ContentTemplate>
                                    </asp:UpdatePanel>--%>
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
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbINNOrg" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>--%>
					                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest" ControlToValidate="tbINNOrg" ValidationExpression="\d\d\d\d\d\d\d\d\d\d\d\d\d\d"></asp:RegularExpressionValidator>
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
                               <%--     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>--%>
									<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbTotalPrice" Width="150px" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" ClientEvents-OnValueChanged="demo.valueChanged" Enabled="true" TabIndex="13" ReadOnly="true"></telerik:RadNumericTextBox>
									<asp:RequiredFieldValidator ID="rfProductPrice" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest" ControlToValidate="RadNumTbTotalPrice" ToolTip="Введите цену"></asp:RequiredFieldValidator>
                    <%--                    </ContentTemplate>
                                    </asp:UpdatePanel>--%>
								</td>
								<td></td>
							</tr>
							<tr>
								<td>
									<label>Сумма первоначального взноса:</label></td>
								<td></td>
								<td>
                                    <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>--%>
									<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbAmountOfDownPayment" Width="150px" Value="0" EmptyMessage="min 10% от стоимости" Type="Currency" MinValue="0" ClientEvents-OnValueChanged="demo.valueChanged2" AutoPostBack="false" TabIndex="14"></telerik:RadNumericTextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest" ControlToValidate="RadNumTbAmountOfDownPayment"></asp:RequiredFieldValidator>
                                   <%--    </ContentTemplate>
                                    </asp:UpdatePanel>--%>
								</td>
							</tr>
							<tr>
								<td><label>Сумма кредита:</label></td>
								<td></td>
								<td>
                                  <%-- <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>--%>
									<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbRequestSumm" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="true" ClientEvents-OnValueChanged="demo.valueChanged3" TabIndex="15" ReadOnly="true"></telerik:RadNumericTextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Ошибка!" ControlToValidate="RadNumTbRequestSumm" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                       <%-- </ContentTemplate>
                                    </asp:UpdatePanel>--%>
								</td>
							</tr>
                            <tr>
								<td><%--<label>Ежемесячный платеж:</label>--%></td>
								<td></td>
								<td>
                                    <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbMonthlyInstallment" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="false" TabIndex="16" visible="false"></telerik:RadNumericTextBox>
                                </td>
							</tr>
							<tr>
								<td>
									<label>Предполагаемая дата погашения</label></td>
								<td></td>
								<td>
									<asp:TextBox ID="tbActualDate" runat="server" ClientIDMode="Static" TabIndex="17" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" ></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="tbActualDate" CssClass="SaveRequest"></asp:RequiredFieldValidator>
								    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="дд.мм.гггг (возможные дни погашения: 2-19, кроме 5,10,15)" ValidationExpression="(?:02|03|04|06|07|08|09|11|12|13|14|16|17|18|19).\d\d.\d\d\d\d" ValidationGroup="SaveRequest" ControlToValidate="tbActualDate" CssClass="SaveRequest"></asp:RegularExpressionValidator>
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
						<%-- <telerik:RadAjaxPanel ID="RadAjaxPanel1" EnableAJAX="true" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">--%>
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
                                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbSumMonthSalary" Width="150px" Value="0" EmptyMessage="" Type="Currency" MinValue="0" ClientEvents-OnValueChanged="demo.valueChanged3" AutoPostBack="false" TabIndex="18"></telerik:RadNumericTextBox>
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
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbAverageMonthSalary" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="true" ReadOnly="true"></telerik:RadNumericTextBox>
									</td>
								</tr>
							</table>
						</asp:Panel>
						<asp:Panel ID="pnlBusiness" runat="server" Visible="false">
							<table>
                               <tr>
									<td><label>Минимальная выручка в день:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbMinRevenue" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="rfMinRevenue" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbMinRevenue" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                                <tr>
									<td><label>Максимальная выручка в день:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbMaxRevenue" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="rfMaxRevenue" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbMaxRevenue" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
<%--								<tr>
									<td><label>Выручка в день:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbRevenue" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="rfRevenue" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenue" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>--%>
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
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbСostPrice" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="20"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="rfСostPrice" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbСostPrice" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>

								</tr>
								<tr>
									<td><label>Расходы по бизнесу в месяц (аренда, охрана, патент, транспорт, з/п и др.расходы связанные с бизнесом):</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbOverhead" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="21"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="rfOverhead" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbOverhead" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
								<tr>
									<td><label>Расходы на семью в месяц:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbFamilyExpenses" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="rfFamilyExpenses" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbFamilyExpenses" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
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
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbRevenueAgro" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenueAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                               <tr>
									<td><label>Выручка от продажи молока в день:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbRevenueMilk" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenueMilk" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                                <tr>
									<td><label>Расходы на содержание скота за последние 3 мес:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbOverheadAgro" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbOverheadAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
<%--								<tr>
									<td><label>Выручка в день:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbRevenue" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="rfRevenue" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenue" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>--%>
                                
								
								<tr>
									<td><label>Дополнительные расходы в мес:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbAddOverheadAgro" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="21"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbAddOverheadAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
								<tr>
									<td><label>Расходы на семью в месяц:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbFamilyExpensesAgro" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22"></telerik:RadNumericTextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbFamilyExpensesAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</td>
								</tr>
                                
                                <tr>
                                   <td><asp:Label ID="Label1" runat="server" Text="Комментарии по бизнесу :</label>"></asp:Label></td>
                                   <td><asp:TextBox ID="txtAgroComment" runat="server" TextMode="MultiLine" Rows="8" Width="300px"></asp:TextBox></td>
                                </tr>

							</table>
						</asp:Panel>
                        <table>
                                <tr>
									<td><label>Дополнительные доходы:</label></td>
									<td>
										<telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbAdditionalIncome" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22"></telerik:RadNumericTextBox>
										<%--<asp:RequiredFieldValidator ID="rfAdditionalIncome" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbAdditionalIncome" CssClass="SaveRequest"></asp:RequiredFieldValidator>--%>
									</td>
                                </tr>
                        </table>
						<%--                </telerik:RadAjaxPanel>--%>
					</td>
					<td></td>
					<td>
						<%--         <telerik:RadAjaxPanel ID="RadAjaxPanel2" EnableAJAX="true" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">--%>
		 Документы:
		<div class="divDocuments">
			<table>
				<tr>
					<td><label>Выберите файл:</label></td>
					<td>
						<telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" CssClass="async-attachment" ID="AsyncUpload1" HideFileInput="true" AllowedFileExtensions=".jpeg,.jpg,.png,.doc,.docx,.xls,.xlsx,.pdf" MaxFileInputsCount="1" />
					</td>
				</tr>
				<tr>
					<td><label>Описание файла:</label></td>
					<td><asp:TextBox ID="tbFileDescription" runat="server"></asp:TextBox></td>
				</tr>
			</table>


			<br />
			<asp:Button ID="btnUploadFiles" runat="server" Text="Загрузить" OnClick="btnUploadFiles_Click" Enabled="False" />
			<asp:GridView ID="gvRequestsFiles" runat="server" HeaderStyle-BackColor="#336666" HeaderStyle-ForeColor="White"
				AlternatingRowStyle-ForeColor="#000" CssClass="gvRequestsFiles" AutoGenerateColumns="false" OnRowCommand="gvRequestsFiles_RowCommand" OnRowDataBound="gvRequestsFiles_RowDataBound">
				<Columns>
					<asp:BoundField DataField="Name" HeaderText="Имя файла" />
					<asp:BoundField DataField="FileDescription" HeaderText="Описание" />
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
            <asp:LinkButton ID="LinkButton1" OnClientClick="aspnetForm.target ='_blank';" PostBackUrl="https://admin:{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W@https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>

            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <br />
            <asp:GridView ID="gvRequestsFilesPhoto" runat="server" AutoGenerateColumns="False">
                <Columns>
					<asp:BoundField DataField="Name" HeaderText="Фото" />
					<%--<asp:BoundField DataField="FileDescription" HeaderText="Описание" />--%>
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
				</Columns>
            </asp:GridView>
            <br />
            <br />
            <input type="button" value="Фото клиента" onclick="window.open('webcam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">
		</div>
						<%--    </telerik:RadAjaxPanel>--%>
					</td>

				</tr>
				<tr>
					<td></td>
					<td></td>
                    <td>
                        <table>
                            <tr>
                                <td><asp:Label ID="lblOtherLoans" runat="server" Text="Взносы по др.кредитам:</label>"></asp:Label></td>
                                <td><telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumOtherLoans" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="23" Visible="true"></telerik:RadNumericTextBox></td>
                            </tr>
                        </table>
                    </td>
				</tr>
			</table>
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
        </asp:Panel>
            <br />
            <asp:Button ID="btnGetScoreBee" runat="server" Text="Получить скорбалл от Билайн" OnClick="btnGetScoreBee_Click" Visible="False" />
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
                    <td><asp:Button ID="btnCancelReq" runat="server" Text="Отменено" onClientclick="myComment()"  Visible="false" OnClick="btnCancelReq_Click"/></td>
                    <td></td>
                    <td><asp:Button ID="btnCancelReqExp" runat="server" Text="Отказано" onClientclick="myComment()"  Visible="false" OnClick="btnCancelReqExp_Click"/></td>
                    <td></td>
					<td><asp:Button ID="btnIssue" runat="server" Text="Выдано" OnClick="btnIssue_Click" Visible="false" onClientclick="myComment()"/></td>
                    <td></td>
                    <td><asp:Button ID="btnReceived" runat="server" Text="Принято" Visible="false" onClientclick="myComment()" OnClick="btnReceived_Click"/></td>
                    <td></td>
                    <td><asp:Button ID="btnInProcess" runat="server" Text="В обработке" Visible="false" onClientclick="myComment()" OnClick="btnInProcess_Click" /></td>
                    <td></td>
                    
                </tr>
            </table><br /><br />
			<table>
				<tr>
                    <td></td>
					<td><asp:Button ID="btnCalculator" runat="server" OnClick="btnCalculator_Click" Text="Калькулятор" /></td>
					<td></td>
					<td><asp:Button ID="btnAgreement" runat="server" Text="Договор" Visible="False" OnClick="btnAgreement_Click" OnClientClick="openNewWin();"/></td>
					<td></td>
					<td><asp:Button ID="btnReceptionAct" runat="server" Text="Акт приема-передачи" OnClick="btnReceptionAct_Click" Visible="False" /></td>
					<td></td>
					<td><asp:Button ID="btnPledgeAgreement" runat="server" Text="Договор о залоге" OnClick="btnPledgeAgreement_Click" Visible="False" /></td>
					<td></td>
					<td><asp:Button ID="btnProffer" runat="server" Text="Предложение-ЗП/ИП" OnClick="btnProffer_Click" Visible="False" OnClientClick="openNewWin();"/></td>
                    <td></td>
                    <td><asp:Button ID="btnComment" runat="server" Text="Комментарий" onClientclick="myComment()" OnClick="btnComment_Click"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriod" runat="server" Text="Отчет" Visible="False" OnClientClick="openNewWin();" OnClick="btnForPeriod_Click"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriodWithHistory" runat="server" Text="Отчет 2" Visible="False" OnClientClick="openNewWin();" OnClick="btnForPeriodWithHistory_Click"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriodWithProducts" runat="server" Text="Отчет 3" OnClientClick="openNewWin();" OnClick="btnForPeriodWithProducts_Click" Visible="False" /></td>
                    <td></td>
                    <td><asp:Button ID="btnProfileNano" runat="server" Text="Анкета Нано" OnClientClick="openNewWin();" Visible="False" OnClick="btnProfileNano_Click" /></td>
				</tr>
                <tr>
                    <td><br /></td>
                </tr>
                <tr>
					<td></td>
					<td><asp:Button ID="btnNewRequest" runat="server" OnClick="btnNewRequest_Click" Text="Новая заявка" /></td>
					<td></td>
					<td><asp:Button ID="btnSendCreditRequest" runat="server" Text="Сохранить" OnClick="btnSendCreditRequest_Click" ValidationGroup="SaveRequest" Style="height: 26px" Visible="False" onClientclick="myComment()"/></td>
					<td></td>
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
				<td>
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
				<td> 
                    <asp:HiddenField ID="hfUserID" runat="server" />
				</td>
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
				<td></td>
				<td>
                    <asp:HiddenField ID="hfRequestStatus" runat="server" />
                </td>
                <td>
                    <asp:HiddenField ID="hfAgentRoleID" runat="server" />
                </td>
                <td><asp:HiddenField ID="hfRequestIDDel" runat="server" Value="-1" /></td>
			</tr>


		</table>
	</asp:Panel>

	<asp:Panel ID="pnlCustomer" runat="server" Visible="False" CssClass="panels">
		<h3>Форма клиента</h3>
		<table>
			<tr>
				<td>&nbsp;</td>
				<td>
					<label>Данные клиента</label></td>
				<td style="width:258px"></td>
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
                    <%--<asp:UpdatePanel ID="UpdatePanel8" runat="server"> 
                      <ContentTemplate>--%>
					<asp:DropDownList ID="ddlDocumentTypeID" class="chosen" runat="server" DataTextField="TypeName" DataValueField="DocumentTypeID" TabIndex="11" OnSelectedIndexChanged="ddlDocumentTypeID_SelectedIndexChanged" Width="260px" AutoPostBack="True">
                        <asp:ListItem Value="3">Паспорт (серия AN) образца 2004 года</asp:ListItem>
                        <asp:ListItem Value="14">Биометрический паспорт (серия ID)</asp:ListItem>
                    </asp:DropDownList>
                    <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
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
                   <%-- <asp:UpdatePanel ID="UpdatePanel7" runat="server"> 
                      <ContentTemplate>--%>
					<asp:TextBox ID="tbDocumentSeries" runat="server" TabIndex="12" OnTextChanged="tbDocumentSeries_TextChanged" AutoPostBack="True"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfDocumentSeries" runat="server" ControlToValidate="tbDocumentSeries" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
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
				<td></td>
				<td>
					<label>Отчество:</label></td>
				<td>
					<asp:TextBox ID="tbOtchestvo" runat="server" TabIndex="3"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="tbOtchestvo" ErrorMessage="в фио все буквы должны быть на русском" ValidationExpression="^[а-яА-ЯёЁъЪ -]{1,32}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                </td>
				<td></td>
				<td></td>
				<td></td>
				<td></td>
				<td><label>Срок действия:</label></td>
				<td>
   					<asp:TextBox ID="tbValidTill" runat="server" ClientIDMode="Static" TabIndex="20" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbValidTill" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="tbValidTill" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>

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
					<asp:TextBox ID="tbIdentificationNumber" runat="server" TabIndex="4"></asp:TextBox>
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
					<asp:RequiredFieldValidator ID="rfCountry" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlNationalityID" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td>
					<asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Selected="True">Мужской</asp:ListItem>
						<asp:ListItem>Женский</asp:ListItem>
					</asp:RadioButtonList></td>
				<td></td>
				<td></td>
				<td></td>
				<td>
					<asp:DropDownList ID="ddlBirthCountryID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="21"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfBirthofCountry" runat="server" ControlToValidate="ddlBirthCountryID" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="rfBirthCityName" runat="server" ControlToValidate="ddlBirthCityName" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="rfRegCountry" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlRegistrationCountryID" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="rfRegCityName" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlRegistrationCityName" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td>
					<label>Нас.пункт:</label></td>
				<td>
					<asp:DropDownList ID="ddlResidenceCityName" class="chosen" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="15"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlResidenceCityName" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
                <td><label>Телефон:</label></td>
				<td>
                    <asp:TextBox ID="tbContactPhone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="tbContactPhone" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="tbContactPhone" CssClass="SaveCustomers" ErrorMessage="+996 ### ######" ValidationExpression="^\+\d{3}\ \d{3}\ \d{6}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                </td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td><label>Улица:</label></td>
				<td>
					<asp:TextBox ID="tbRegistrationStreet" runat="server" TabIndex="8"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfRegStreet" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbRegistrationStreet" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td><label>Улица:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceStreet" runat="server" TabIndex="16"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="tbResidenceStreet" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="rfRegHouse" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbRegistrationHouse" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="tbRegistrationHouse" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Дом:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceHouse" runat="server" TabIndex="17"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="tbResidenceHouse" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="tbResidenceHouse" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
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
				    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="tbRegistrationFlat" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Кв:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceFlat" runat="server" TabIndex="18"></asp:TextBox>
				    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="tbResidenceFlat" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
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



<asp:Panel ID="pnlCalculator" runat="server" Visible="False" CssClass="panels">
	<br />
	<br />
    <table>
        <tr>
            <td>Стоимость товара:</td>
            <td>
                <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbTotalPrice2" Width="150px" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" ClientEvents-OnValueChanged="demo.cvalueChanged" Enabled="True" TabIndex="11"></telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td>Первоначальный взнос:</td>
            <td><telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbAmountOfDownPayment2" Width="150px" Value="0" EmptyMessage="min 10% от стоимости" Type="Currency" MinValue="0" ClientEvents-OnValueChanged="demo.cvalueChanged2" AutoPostBack="false" TabIndex="12"></telerik:RadNumericTextBox></td>
        </tr>
        <tr>
            <td>Сумма кредита:</td>
            <td><telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" ID="RadNumTbRequestSumm2" Width="150px" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="true" ClientEvents-OnValueChanged="demo.cvalueChanged2" TabIndex="13"></telerik:RadNumericTextBox></td>
        </tr>
        <tr>
            <td>Срок:</td>
            <td><asp:DropDownList ID="ddlPeriod" runat="server">
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Процент</td>
            <td>
                <asp:DropDownList ID="ddlReqRateCalc" runat="server">
                   <asp:ListItem Value="32.00"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <br />

    <asp:Button ID="btnCalculate" runat="server" Text="Вычислить" OnClick="btnCalculate_Click" />
    <input name="b_print" type="button" class="ipt" onclick="printdiv('div_print');" value=" Печать ">
	<asp:Button ID="btnCalculatorClose" runat="server" Text="Закрыть" OnClick="btnCalculatorClose_Click" />
    <br /><br />
	<div id="div_print">
		<table>
			<tr>
				<td><label>Сумма кредита</label></td>
				<td>&nbsp;&nbsp;</td>
				<td><asp:Label ID="lblRequestSum" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td><label>Срок кредита (месяцы)</label></td>
				<td></td>
				<td><asp:Label ID="lblRequestPeriod" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td><label>Фактический процент</label></td>
				<td></td>
				<td><asp:Label ID="lblInterestRate" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><label>Ежемесячный взнос</label></td>
				<td></td>
				<td><asp:Label ID="lblMonthlyInstallment" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td><label>Сумма процентов за период</label></td>
				<td></td>
				<td><asp:Label ID="lblAmountInterestForPeriod" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td><label>Сумма к возврату общая</label></td>
				<td></td>
				<td><asp:Label ID="lblSumTotalReturn" runat="server" Text=""></asp:Label></td>
			</tr>
		</table>



	</div>
    </asp:Panel>

</div>


<script type="text/javascript">
    $(".chosen").chosen();
</script>

<script type="text/javascript">   
 
    $(function () {
        $.datepicker.setDefaults({
            showOn: "both",
            buttonImage: "<%=ControlPath %>images/cal-ico.png",
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


<%--<script type="text/javascript">
	var input = document.getElementById('tbProductPrice');
	input.oninput = function() {
		document.getElementById('tbRequestSumm').innerHTML = input.value;
	};

	function sumChanged()
	{
		document.getElementById('tbRequestSumm').value = RadNumTbTotalPrice.get_value() - document.getElementById('tbAmountOfDownPayment').value;
	}
</script>--%>

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
            amount10 = 10 * price / 100;
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
            stavka = parseInt(document.getElementById('<%=ddlRequestRate.ClientID%>').value);

             amount1 = 0;
            if (stavka == 0.00) amount1 = 0.00;
            if (stavka == 32.00)
            {
                //if (price <= 50000) amount1 = 0; else amount1 = 10;
                 amount1 = 10;
            }

            amount10 = amount1 * price / 100;
            amount = RadNumTbAmountOfDownPayment.get_value();
            //if (amount >= amount10) { request = price - amount; }
            //else { request = ""; }

            if (stavka == 0)
            { 
                request = price - amount;
            }
            else
            {
                if (amount >= amount10) { request = price - amount; }
                else { request = ""; }
            }
            //request = price - amount;

            n = parseInt(document.getElementById("ddlRequestPeriod").options[document.getElementById("ddlRequestPeriod").selectedIndex].value);
           
            s = RadNumTbTotalPrice.get_value() - amount;
            ii = stavka / 12 / 100;
            //k = (((Math.pow((1 + ii), n)) * (ii)) * s) / ((Math.pow((1 + (ii)), n)) - 1);
            //RadNumTbMonthlyInstallment.set_value(k.toFixed(0));
            RadNumTbRequestSumm.set_value(request);
        }

        demo.valueChanged3 = function (sender, args) {
            SumMonthSalary = RadNumTbSumMonthSalary.get_value();
            CountSalary = parseInt(document.getElementById("ddlMonthCount").options[document.getElementById("ddlMonthCount").selectedIndex].value);
            AverageMonthSalary = SumMonthSalary / CountSalary
            RadNumTbAverageMonthSalary.set_value(AverageMonthSalary.toFixed(0));
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
    })();
</script>

<script type="text/javascript">
    function pageLoad() {
        RadNumTbTotalPrice = $find("<%=RadNumTbTotalPrice.ClientID%>");
        RadNumTbAmountOfDownPayment = $find("<%=RadNumTbAmountOfDownPayment.ClientID%>");
        RadNumTbRequestSumm = $find("<%=RadNumTbRequestSumm.ClientID%>");
        RadNumTbTotalPrice2 = $find("<%=RadNumTbTotalPrice2.ClientID%>");
            RadNumTbAmountOfDownPayment2 = $find("<%=RadNumTbAmountOfDownPayment2.ClientID%>");
            RadNumTbRequestSumm = $find("<%=RadNumTbRequestSumm.ClientID%>");
            RadNumTbRequestSumm2 = $find("<%=RadNumTbRequestSumm2.ClientID%>");
                RadNumTbMonthlyInstallment = $find("<%=RadNumTbMonthlyInstallment.ClientID%>");
            RadNumTbSumMonthSalary = $find("<%=RadNumTbSumMonthSalary.ClientID%>");
            RadNumTbAverageMonthSalary = $find("<%=RadNumTbAverageMonthSalary.ClientID%>");
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