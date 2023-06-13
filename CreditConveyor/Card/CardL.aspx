<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardL.aspx.cs" Inherits="Zamat.CardL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>




            <div>

<asp:Panel ID="pnlUser" runat="server">
	<p class="right" style="font-size: 14px;">
        <br />
		<asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbLogout" runat="server" >Logout</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbSettings" runat="server" >Настройки</asp:LinkButton>
	</p>
	<br /> <br />
    
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
                <td><asp:Button ID="btnSavePassword" runat="server" Text="Сохранить"  ValidationGroup="changePassowrd" /></td>
                <td><asp:Button ID="btnCancelPassword" runat="server" Text="Закрыть"  /></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    
                </td>
            </tr>
            
        </table>
    </asp:Panel> 
    
        
    <br /><br />
	<asp:Panel ID="pnlCredit" runat="server" CssClass="panels">


      
        <div class="table-responsive">
		<table class="table">
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
				<td data-label = ""><asp:Button ID="btnSearchRequest" runat="server" Text="Найти" ValidationGroup="SearchRequest" /></td>
			</tr>
            <tr>
				<td data-label = "" colspan="7"><b>Статусы заявки</b>
                    <asp:CheckBoxList ID="chkbxlistStatus" runat="server" RepeatDirection="Horizontal" Visible="False">
                        <asp:ListItem Selected="True">Новая заявка</asp:ListItem>
                        <asp:ListItem Selected="True">Исправлено</asp:ListItem>
                        <asp:ListItem Selected="True">Подтверждено</asp:ListItem>
                        <asp:ListItem Selected="True">Утверждено</asp:ListItem>
                        <asp:ListItem Selected="True">Выдано</asp:ListItem>
                        <asp:ListItem Selected="True">Принято</asp:ListItem>
                        <asp:ListItem Selected="True">Не подтверждено</asp:ListItem>
                        <asp:ListItem Selected="True">Отменено</asp:ListItem>
                        <asp:ListItem Selected="True">Отказано</asp:ListItem>
                        <asp:ListItem Selected="True">Активировано</asp:ListItem>
                        <asp:ListItem Selected="True">Исправить</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
            <tr>
                <td data-label = "" colspan="7"><b></b>
                    </td>
            </tr>
            </tbody>
		</table>
        </div>



        <br />
        <div class="row">
            <div class="col-lg-12 ">
                <div class="table-responsive">
                    <asp:GridView ID="gvRequests" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="gvRequests" AllowPaging="True" PageSize="50">
                        <Columns>
                            <asp:TemplateField HeaderText="Выбрать">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbSelectRequest" runat="server" CommandArgument='<%# Eval("RequestID") %>' CommandName="Sel">Выбрать</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RequestID" HeaderText="Номер" />
                            <%--<asp:BoundField DataField="CreditID" HeaderText="CreditID" />--%>
                            <asp:BoundField DataField="ShortName" HeaderText="Филиал" />
                            <asp:BoundField DataField="Surname" HeaderText="Фамилия" />
                            <asp:BoundField DataField="Otchestvo" HeaderText="Отчество" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Имя" />
                            <asp:BoundField DataField="GroupName" HeaderText="Группа" />
                            <asp:BoundField DataField="OrgName" HeaderText="Орг" />
                            <asp:BoundField DataField="IdentificationNumber" HeaderText="ИНН" />
                 <%--           <asp:BoundField DataField="CreditPurpose" HeaderText="Назначение кредита" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" /> --%> 
                            <%--<asp:BoundField DataField="ProductPrice" HeaderText="Цена" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="AmountDownPayment" HeaderText="Взнос" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                            <asp:BoundField DataField="RequestSumm" HeaderText="Кредит" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                            <asp:BoundField DataField="RequestDate" HeaderText="Дата создания заявки" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="RequestStatus" HeaderText="Статус заявки" />
                            <asp:BoundField DataField="StatusDate" HeaderText="Дата изменения статуса"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
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

<%--	 <asp:Timer ID="MyTimer" runat="server"  Interval="15000" Enabled="false">
	  </asp:Timer>--%>
        <asp:HiddenField ID="hfRequestsRowID" runat="server" />
		<br />
		<br />
		<h3>Форма заявки</h3><asp:HiddenField ID="hfRequestAction" runat="server" />


        <asp:GridView ID="gvAllReqCust" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="gvRequests" AllowPaging="True" >
             <Columns>
                            <asp:BoundField DataField="RequestID" HeaderText="Номер" />
                            <asp:BoundField DataField="CardN" HeaderText="Номер карты" />
                            <asp:BoundField DataField="Surname" HeaderText="Фамилия" />
                            <asp:BoundField DataField="Otchestvo" HeaderText="Отчество" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Имя" />
                            <%--<asp:BoundField DataField="GroupName" HeaderText="Группа" />
                            <asp:BoundField DataField="OrgName" HeaderText="Орг" />--%>
                            <asp:BoundField DataField="IdentificationNumber" HeaderText="ИНН" />
                            <asp:BoundField DataField="RequestDate" HeaderText="Дата заявки" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="RequestStatus" HeaderText="Статус заявки" />
                        </Columns>
        </asp:GridView>


		<asp:Panel ID="pnlNewRequest" runat="server" Visible="true" CssClass="panels">
			<table>
				<tr>
					<td></td>
					<td colspan="2">Статус заявки:<asp:Label ID="lblStatusRequest" runat="server" Text="Status"></asp:Label></td>
					<td></td>
					<td></td>
					<td><asp:Label ID="lblMessageClient" runat="server"></asp:Label></td>
					<td></td>
					<td></td>
					<td></td>
					<td>&nbsp;</td>
                    <td></td>
				</tr>
				<tr>
					<td><br /></td>
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
                    <td></td>
				</tr>
				<tr>
					<td></td>
					<td>
                        <asp:Label ID="lblCardN" runat="server" Text="Номер карты:"></asp:Label></td>
					<td>
						<asp:TextBox ID="tbCardN" runat="server" TabIndex="1">941713</asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbCardN" ErrorMessage="*" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
					    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="tbCardN" ErrorMessage="16-значное число" ValidationExpression="\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d" CssClass="SaveRequest"></asp:RegularExpressionValidator>
					</td>
					<td></td>
					<td><label>Фамилия:</label></td>
					<td><asp:TextBox ID="tbSurname2" runat="server" Enabled="False" TabIndex="5"></asp:TextBox></td>
					<td><asp:Button ID="btnCustomerSearch" runat="server" Text="Выбрать клиента" /></td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
                    <td></td>
                    <td></td>
				</tr>
				<tr>
					<td></td>
					<td>
                        <asp:Label ID="lblAccountN" runat="server" Text="Номер счета:"></asp:Label></td>
					<td>
                        <asp:TextBox ID="tbAccountN" runat="server"></asp:TextBox></td>
					<td></td>
					<td>
						<label>Имя:</label></td>
					<td>
						<asp:TextBox ID="tbCustomerName2" runat="server" Enabled="False" TabIndex="6"></asp:TextBox></td>
					<td><asp:Button ID="btnCustomerEdit" runat="server" Text="Редактировать" Enabled="False" /></td>
					<td></td>
					<td></td>
                    <td></td>
                    <td></td>
                    <td></td>
				</tr>
				<tr>
					<td></td>
					<td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
					<td>
                        <asp:TextBox ID="tbEmail2" runat="server"></asp:TextBox>
                    </td>
					<td></td>
					<td><label>Отчество</label></td>
					<td><asp:TextBox ID="tbOtchestvo2" runat="server" Enabled="False" TabIndex="7"></asp:TextBox></td>
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
                        <asp:Label ID="lblCodeWord" runat="server" Text="Кодовое слово"></asp:Label></td>
					<td>
                        <asp:TextBox ID="tbCodeName2" runat="server"></asp:TextBox>
                    </td>
					<td></td>
					<td><label>ИНН:</label></td>
					<td><asp:TextBox ID="tbINN2" runat="server" Enabled="False" TabIndex="8"></asp:TextBox></td>
					<td></td>
					<td></td>
                    <td></td>
					<td></td>
                    <td></td>
                    <td></td>
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
			</table>
            <br />
			<table>
							<tr>
								<td>
              
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
					<td></td>
					<td></td>
					<td></td>
					<td>
		 Документы:
                        
		<div class="divDocuments">
			<table>
				<tr>
					<td><label>Выберите файл:</label></td>
					<td>
                        <asp:ScriptManager runat="server"></asp:ScriptManager>
						<telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" CssClass="async-attachment" ID="AsyncUpload1" HideFileInput="true" AllowedFileExtensions=".jpeg,.jpg,.png,.doc,.docx,.xls,.xlsx,.pdf" MaxFileInputsCount="1" />
					</td>
				</tr>
				<tr>
					<td><label>Описание файла:</label></td>
					<td><asp:TextBox ID="tbFileDescription" runat="server"></asp:TextBox></td>
				</tr>
			</table>


			<br />
			<asp:Button ID="btnUploadFiles" runat="server" Text="Загрузить" Enabled="False" />
			<asp:GridView ID="gvRequestsFiles" runat="server" HeaderStyle-BackColor="#336666" HeaderStyle-ForeColor="White"
				AlternatingRowStyle-ForeColor="#000" CssClass="gvRequestsFiles" AutoGenerateColumns="false" >
				<Columns>
					<asp:BoundField DataField="Name" HeaderText="Имя файла" />
					<asp:BoundField DataField="FileDescription" HeaderText="Описание" />
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
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
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
				</tr>
			</table>
            <asp:LinkButton ID="lbHistory" runat="server">История</asp:LinkButton>
            <asp:LinkButton ID="lbBack" runat="server" Visible="False">История</asp:LinkButton>
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
		</asp:Panel>

		<asp:TextBox ID="tbNote" runat="server" ClientIDMode="Static" TextMode="MultiLine" Width="0" Height="0"></asp:TextBox>

		<br />
		<asp:Panel ID="pnlMenuRequest" runat="server">
             <table>
                <tr>
                    <td></td>
                    <td>Поменять статус:</td>
                    <td></td>
                    <td><asp:Button ID="btnFix" runat="server" Text="Исправить" onClientclick="myComment()" Visible="False" /></td>
                    <td></td>
                    <td><asp:Button ID="btnFixed" runat="server" Text="Исправлено" onClientclick="myComment()" Visible="False" /></td>
                    <td></td>
                    <td><asp:Button ID="btnConfirm" runat="server" Text="Подтверждено" Visible="false" onClientclick="myComment()"/></td>
                    <td></td>
                    <td><asp:Button ID="btnApproved" runat="server" Text="Утверждено" Visible="false" onClientclick="myComment()"/></td>
                    <td></td>
                    <td><asp:Button ID="btnActivated" runat="server" Text="Активировано" onClientclick="myComment()"  Visible="false" /></td>
                    <td></td>
                    <td><asp:Button ID="btnCancelReq" runat="server" Text="Отменено" onClientclick="myComment()"  Visible="false" /></td>
                    <td></td>
                    <td><asp:Button ID="btnCancelReqExp" runat="server" Text="Отказано" onClientclick="myComment()"  Visible="false" /></td>
                    <td></td>
					<td><asp:Button ID="btnIssue" runat="server" Text="Выдано"  Visible="false" onClientclick="myComment()"/></td>
                    <td></td>
                    <td><asp:Button ID="btnReceived" runat="server" Text="Принято" Visible="false" onClientclick="myComment()" /></td>
                    <td></td>
                </tr>
            </table><br /><br />
			<table>
				<tr>
                    <td></td>
					<td><asp:Button ID="btnAgreement" runat="server" Text="Договор" Visible="False" OnClientClick="openNewWin();"/></td>
					<td></td>
					<td><asp:Button ID="btnReceptionAct" runat="server" Text="Акт приема-передачи" Visible="False" /></td>
					<td></td>
					<td><asp:Button ID="btnPledgeAgreement" runat="server" Text="Договор о залоге" Visible="False" /></td>
					<td></td>
					<td><asp:Button ID="btnProffer" runat="server" Text="Анкета" Visible="False" OnClientClick="openNewWin();"/></td>
                    <td></td>
                    <td><asp:Button ID="btnComment" runat="server" Text="Комментарий" onClientclick="myComment()"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriod" runat="server" Text="Отчет" Visible="False" OnClientClick="openNewWin();"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriodWithHistory" runat="server" Text="Отчет 2" Visible="False" OnClientClick="openNewWin();"  /></td>
                    <td></td>
                    <td><asp:Button ID="btnForPeriodWithProducts" runat="server" Text="Отчет 3" OnClientClick="openNewWin();"  Visible="False" /></td>
				</tr>
                <tr>
                    <td><br /></td>
                </tr>
                <tr>
					<td></td>
					<td><asp:Button ID="btnNewRequest" runat="server" Text="Новая заявка" /></td>
					<td></td>
					<td><asp:Button ID="btnSendCreditRequest" runat="server" Text="Сохранить" ValidationGroup="SaveRequest" Style="height: 26px" Visible="False" onClientclick="myComment()"/></td>
					<td>
                        &nbsp;</td>
                    <td><asp:Button ID="btnCloseRequest" runat="server" Text="Закрыть" Style="height: 26px" Visible ="false"/></td>
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
					<asp:RequiredFieldValidator ID="rfSearchCustomer" runat="server" ControlToValidate="tbSearchINN" ErrorMessage="*" ValidationGroup="SearchCustomer"></asp:RequiredFieldValidator>
				</td>
                <td>&nbsp;</td>
                   <td>
                       <asp:TextBox ID="tbSerialN" runat="server"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="rfSerialN" runat="server" ControlToValidate="tbSerialN" ErrorMessage="*" ValidationGroup="SearchCustomer"></asp:RequiredFieldValidator>
                   </td>
				<td>&nbsp;&nbsp;</td>
				<td>
					<asp:Button ID="btnSearchClient" runat="server" Text="Поиск" ValidationGroup="SearchCustomer" /></td>
				<td>&nbsp;</td>
				<td>
					<asp:Button ID="btnNewCustomer" runat="server" Text="Новый клиент" /></td>
				<td>&nbsp;</td>
				<td>
					<asp:Button ID="btnCredit" runat="server" Text="Выбрать клиента" Enabled="False" /></td>
			</tr>
			<tr>

				<td>&nbsp;</td>
				<td></td>
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
        <br />
        <asp:GridView ID="gvAllReqCustomer" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="gvRequests" AllowPaging="True" >
             <Columns>
                            <asp:BoundField DataField="RequestID" HeaderText="Номер" />
                            <asp:BoundField DataField="Surname" HeaderText="Фамилия" />
                            <asp:BoundField DataField="Otchestvo" HeaderText="Отчество" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Имя" />
                            <%--<asp:BoundField DataField="GroupName" HeaderText="Группа" />
                            <asp:BoundField DataField="OrgName" HeaderText="Орг" />--%>
                            <asp:BoundField DataField="IdentificationNumber" HeaderText="ИНН" />
                            <asp:BoundField DataField="RequestDate" HeaderText="Дата создания заявки" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="RequestStatus" HeaderText="Статус заявки" />
                            <asp:BoundField DataField="StatusDate" HeaderText="Дата изменения статуса"  HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" dataformatstring="{0:dd/MM/yyyy HH:mm}" />
                        </Columns>
        </asp:GridView>

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
					<asp:RequiredFieldValidator ID="rfSurname" runat="server" ControlToValidate="tbSurname" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="tbSurname" ErrorMessage="в фио все буквы должны быть на русском" ValidationExpression="^[а-яА-ЯёЁъЪ -]{1,32}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td></td>
				<td>
					<asp:DropDownList ID="ddlDocumentTypeID" class="chosen" runat="server" DataTextField="TypeName" DataValueField="DocumentTypeID" TabIndex="11">
                        <asp:ListItem Value="3">Паспорт гражданина Кыргызской Республики образца 2004 года</asp:ListItem>
                        <asp:ListItem Value="14">Биометрический паспорт гражданина Кыргызской Республики образца 2017 года</asp:ListItem>
                    </asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfDocumentType" runat="server" ControlToValidate="ddlDocumentTypeID" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator></td>
				<td></td>
				<td>
					<label>Кем выдан:</label></td>
				<td>
					<asp:TextBox ID="tbIssueAuthority" runat="server" ValidationGroup="SaveCustomers" TabIndex="19"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbIssueAuthority" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
				<td></td>
				<td>
					<label>Имя:</label></td>
				<td>
					<asp:TextBox ID="tbCustomerName" runat="server" TabIndex="2"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfCustomerName" runat="server" ControlToValidate="tbCustomerName" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="tbCustomerName" ErrorMessage="в фио все буквы должны быть на русском" ValidationExpression="^[а-яА-ЯёЁъЪ -]{1,32}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                </td>
				<td></td>
				<td>
					<label>Серия№:</label></td>
				<td>
					<asp:TextBox ID="tbDocumentSeries" runat="server" TabIndex="12"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfDocumentSeries" runat="server" ControlToValidate="tbDocumentSeries" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="revDocumentSeries" runat="server" ErrorMessage="*" ControlToValidate="tbDocumentSeries" ValidationExpression="[A-Z][A-Z]\d\d\d\d\d\d\d" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Дата выдачи:</label></td><td>
					<asp:TextBox ID="tbIssueDate" runat="server" ClientIDMode="Static" TabIndex="20" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbIssueDate" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbValidTill" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="rfIdentificationNumber" runat="server" ErrorMessage="*" ControlToValidate="tbIdentificationNumber" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="rfedentificationNumber" runat="server" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers" ControlToValidate="tbIdentificationNumber" ValidationExpression="\d\d\d\d\d\d\d\d\d\d\d\d\d\d"></asp:RegularExpressionValidator>
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
					<asp:RequiredFieldValidator ID="rfCountry" runat="server" ErrorMessage="*" ControlToValidate="ddlNationalityID" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="rfBirthofCountry" runat="server" ControlToValidate="ddlBirthCountryID" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="rfDateOfBirth" runat="server" ControlToValidate="tbDateOfBirth" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="tbDateOfBirth" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td></td>
				<td>
					<asp:DropDownList ID="ddlBirthCityName" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="22" Visible="False"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfBirthCityName" runat="server" ControlToValidate="ddlBirthCityName" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<label>Адрес фактического проживания:</label>&nbsp;&nbsp;<asp:CheckBox ID="chkbxAsRegistration" runat="server" Text="как в прописке" AutoPostBack="True" />
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
					<asp:RequiredFieldValidator ID="rfRegCountry" runat="server" ErrorMessage="*" ControlToValidate="ddlRegistrationCountryID" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
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
					<asp:RequiredFieldValidator ID="rfRegCityName" runat="server" ErrorMessage="*" ControlToValidate="ddlRegistrationCityName" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td>
					<label>Нас.пункт:</label></td>
				<td>
					<asp:DropDownList ID="ddlResidenceCityName" class="chosen" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="15"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlResidenceCityName" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
                <td><label>Телефон:</label></td>
				<td>
                    <asp:TextBox ID="tbContactPhone" runat="server"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="tbContactPhone" CssClass="SaveCustomers" ErrorMessage="*" ValidationGroup="SaveCustomers"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="tbContactPhone" CssClass="SaveCustomers" ErrorMessage="+996 ### ######" ValidationExpression="^\+\d{3}\ \d{3}\ \d{6}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                </td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td><label>Улица:</label></td>
				<td>
					<asp:TextBox ID="tbRegistrationStreet" runat="server" TabIndex="8"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfRegStreet" runat="server" ErrorMessage="*" ControlToValidate="tbRegistrationStreet" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td></td>
				<td><label>Улица:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceStreet" runat="server" TabIndex="16"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="tbResidenceStreet" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</td>
				<td><label>Email:</label></td>
				<td>
                    <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                </td>
				<td></td>
			</tr>
			<tr>
				<td></td>
				<td><label>Дом:</label></td>
				<td>
					<asp:TextBox ID="tbRegistrationHouse" runat="server" TabIndex="9"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfRegHouse" runat="server" ErrorMessage="*" ControlToValidate="tbRegistrationHouse" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="tbRegistrationHouse" ErrorMessage="*" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Дом:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceHouse" runat="server" TabIndex="17"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="tbResidenceHouse" ErrorMessage="*" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="tbResidenceHouse" CssClass="SaveCustomers" ErrorMessage="*" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td><label>Кодовое слово:</label></td>
				<td>
                    <asp:TextBox ID="tbCodeName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="tbCodeName" ErrorMessage="*" ValidationGroup="SaveCustomers"></asp:RequiredFieldValidator>
                </td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td></td>
				<td><label>Кв:</label></td>
				<td>
					<asp:TextBox ID="tbRegistrationFlat" runat="server" TabIndex="10"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="tbRegistrationFlat" ErrorMessage="*" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				</td>
				<td></td>
				<td><label>Кв:</label></td>
				<td>
					<asp:TextBox ID="tbResidenceFlat" runat="server" TabIndex="18"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="tbResidenceFlat" CssClass="SaveCustomers" ErrorMessage="*" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
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
        <br />
		<asp:Panel ID="pnlSaveCustomer" runat="server">
			<table>
				<tr>
					<td>&nbsp;</td>
					<td><asp:Button ID="btnSaveCustomer" runat="server" Text="Сохранить" ValidationGroup="SaveCustomers" /></td>
					<td>&nbsp;</td>
					<td><asp:Button ID="btnCancel" runat="server" Text="Отменить" /></td>
					<td>&nbsp;</td>
					<td></td>
				</tr>
			</table>
		</asp:Panel>

	</asp:Panel>
	<!-pnlUser->


</asp:Panel>


</div>






        </div>
    </form>
</body>
</html>
