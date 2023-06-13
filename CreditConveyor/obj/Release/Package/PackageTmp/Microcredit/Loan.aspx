<%@ Page Title="МК кредиты" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Loan.aspx.cs" Inherits="СreditСonveyor.Microcredit.Loan" Debug="true" Async="true" AsyncTimeout="60" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/Site.css" rel="stylesheet" />
    <link rel="stylesheet" href="/css/chosen.css">

    <%--<script src="/Scripts/bootstrap.min.js"></script>--%>
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
    display:inline-block;
    }
    #tbNoteCancelReqExp, #tbNoteCancelReq 
    {  
    max-width: 568px; max-height:120px;
    min-width: 568px; min-height:120px;
    }


    span.fio{
        width:75px;
        display: inline-block;

    }
    #tbActualDate{
        width:190px;
    }
        .auto-style1 {
            position: relative;
            min-height: 1px;
            float: left;
            width: 33.33333333%;
            left: -34px;
            top: 22px;
            padding-left: 15px;
            padding-right: 15px;
        }

        .grdSearchResultbreakword
{
  word-wrap:break-word;
  word-break:break-all;
}
    </style>


<div>
  
    <h4>МК кредиты</h4>
<asp:Panel ID="pnlUser" runat="server">

	<%--<p class="right" style="font-size: 14px;">
        <asp:Label ID="lblGroup" runat="server" Text="Group" style="font-size:14px"></asp:Label>
		<asp:Label ID="lblUserName" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbLogout" runat="server" OnClick="lbLogout_Click" Visible="false">Logout</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:LinkButton ID="lbSettings" runat="server" OnClick="lbSettings_Click">Настройки</asp:LinkButton>	</p>
	--%>
    
    <br />
    
    <%--<asp:Button ID="btnBack" runat="server" Text="Назад" class="btn btn-primary" OnClick="btnBack_Click" TabIndex="10"  />--%>
    
      
    <asp:Panel ID="pnlCredit" runat="server" >
    <a href="/Microcredit/Loans" class="btn btn-primary">Назад</a>



	
        <asp:HiddenField ID="hfRequestsRowID" runat="server" />
        <asp:Label ID="lblError" runat="server" Text="" Font-Size="Larger" ForeColor="Red"></asp:Label>
		
        <asp:Panel ID="pnlBlackList" runat="server" Visible="False">
                                        <br />
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
           
        <asp:Panel ID="pnlBlackListOrg" runat="server" Visible="False">
                                        <br />    
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
        </asp:Panel>
               

        <asp:Label ID="lblMsgBox" runat="server" Text="" Font-Size="Larger" ForeColor="Red" Visible="false"></asp:Label>    
        
        <asp:Label ID="lblMessageClient" runat="server" ForeColor="Red"></asp:Label>

        <h3>Форма заявки</h3><asp:HiddenField ID="hfRequestAction" runat="server" />

		<asp:Panel ID="pnlNewRequest" runat="server" Visible="false" >

            
<div>
  <!-- Nav tabs -->
  <%--<ul class="nav nav-tabs" role="tablist">--%>
   <ul class="tabs nav nav-tabs">
    <%--<li role="presentation" class="active">
        <a href="#home" aria-controls="home" role="tab" data-toggle="tab"><b>Данные</b></a>     </li>
    <li role="presentation">
        <a href="#office" aria-controls="profile" role="tab" data-toggle="tab"><b>Документы</b></a>
    </li>--%>
      <li><a href="#main" data-tab='main' class="active selected"><b>Данные</b></a></li>
      <li><a href="#remediation" data-tab='remediation'><b>Документы</b></a></li>
  </ul>

  <!-- Tab panes -->
  
    <%--<div role="tabpanel" class="tab-pane active" id="home">--%>
    <div data-content="main" class='tab-content fade-in'>
        


            <div class="row">

               

                <%--/***********************************/--%>

                 

                 <div class="col-md-4 panels">
                     <div><p>Статус кредитной заявки:<asp:Label ID="lblStatusRequest" runat="server" Text="Status"></asp:Label></p></div>
                <div style="text-align:center; ">
                <asp:Button ID="btnPhoto" runat="server" Text="Фото клиента" class="btn btn-primary" ClientIDMode="Static" OnClick="btnPhoto_Click" />
                <asp:Button ID="btnPhotoClose" runat="server" Text="Закрыть" class="btn btn-primary" OnClick="btnPhotoClose_Click" Visible="False" />
                <asp:Panel ID="pnlPhoto" runat="server" Visible ="false" ClientIDMode="Static">
               <div>
                <div id="my_camera">
                  
                </div>
                <div>
                <input id="btnPhotoOrClear"  type=button value="Сфотогорафировать" class="btn btn-primary" onclick="take_snapshot()">
                </div>
                
                <asp:HiddenField ID="hfPhoto2" runat="server" ClientIDMode="Static" />
                   
                
                <div id="results">
                <%--<img id="imgCam" runat="server" src="'+data_uri+'"/>--%>
                <%--<asp:Image ID="Image1" runat="server" />--%>
                <%-- <asp:Literal ID="Literal1" runat="server"></asp:Literal>--%>
                <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
                </div>
                <asp:Literal ID="Literal1" runat="server" ClientIDMode="Static"></asp:Literal>
                </div>
                <%--<input type="button" value="Фото клиента" onclick="window.open('/Webcam/WebCam?reqid=n','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
                <%--<input type="button" value="Фото клиента" onclick="window.open('/Webcam/WebCam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
                <%--<input type="button" value="Фото клиента" onclick="window.open('webcam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">            --%>

            </asp:Panel>
            <div class="PhotoCustomer">
            <asp:Image ID="Image1" runat="server" ClientIDMode="Static" />
            </div>
            <%--<input type="button" value="Фото клиента" onclick="window.open('WebCam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
            <%--<input type="button" value="Фото клиента" onclick="window.open('/Webcam/WebCam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
            <%--<input type="button" value="Фото клиента" onclick="window.open('https://credit.doscredobank.kg:60755/WebCam?reqid=<%=hfRequestID.Value%>','popUpWindow','height=800,width=800,left=100,top=100,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no, status=yes');">--%>
          </div>


                      <%--<div><p>Клиент (счет для погашения)<b class="red"><asp:Label ID="lblAccount" runat="server"></asp:Label></b></p></div>--%>
                     <asp:Button ID="btnCustomerSearch" runat="server" class="btn btn-primary" OnClick="btnCustomerSearch_Click" Text="Выбрать клиента" /><br />
<%--                     <asp:Button ID="btnCustomerEdit" runat="server" Text="Редактировать" class="btn btn-primary" OnClick="btnCustomerEdit_Click" Enabled="False" /><br />--%>
                     <asp:Button ID="btnUpdFIO" runat="server" Text="Обновить ФИО" class="btn btn-primary" OnClick="btnUpdFIO_Click"  /><br />
                       <div>
                            <p><span class="fio">Фамилия:</span>
				            <asp:TextBox ID="tbSurname2" runat="server" class="form-control" Enabled="False" TabIndex="5"></asp:TextBox></p>
				            
                        </div>
            
                        <div>
                            <p><span class="fio">Имя:</span>
                            <asp:TextBox ID="tbCustomerName2" runat="server" class="form-control" Enabled="False" TabIndex="6"></asp:TextBox></p>
				            
                        </div>
            
                        <div>
                            <p><span class="fio">Отчество</span>
		                    <asp:TextBox ID="tbOtchestvo2" runat="server" class="form-control" Enabled="False" TabIndex="7"></asp:TextBox></p>
				            
                        </div>
                        
                        <div>
                            <p><span class="fio">ИНН:</span>
				            <asp:TextBox ID="tbINN2" runat="server" class="form-control" Enabled="False" TabIndex="8"></asp:TextBox></p>
                        </div>
                        <div>
                            <p><span class="fio">Телефон:</span>
                            <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label></p>
                        </div>
                        <div>
                            <p>Сем.положение:
                            <asp:RadioButtonList ID="rbtnMaritalStatus" runat="server">
                                        <asp:ListItem Value="0">холост (не замужем)</asp:ListItem>
                                        <asp:ListItem Value="1" Selected="True">женат (за мужем)</asp:ListItem>
                                    </asp:RadioButtonList></p>
                        </div>
                       
                       <br />

                     <div>
                          <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>--%>
                            <p>Обеспечение:</p>
                            <asp:CheckBoxList ID="chkbxTypeOfCollateral" runat="server" OnSelectedIndexChanged="chkbxTypeOfCollateral_SelectedIndexChanged" AutoPostBack="True" onclick="myFunction()"> 
                                <asp:ListItem Value="0">Без обеспечения</asp:ListItem>
                                <asp:ListItem Value="1">Поручительство</asp:ListItem>
                                <asp:ListItem Value="2">Движимое имущество</asp:ListItem>
                            </asp:CheckBoxList>
                        <%--  </ContentTemplate>
                           </asp:UpdatePanel>--%>
                       </div>     
                         <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                          <ContentTemplate>--%>
                 <asp:Panel ID="pnlGuarantor" runat="server" Visible="false">
                            <table>
                            <tr>
				                <td></td>
					            <td><label>Поручитель</label></td>
				            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnGuarantSearch" runat="server" class="btn btn-primary" Text="Выбрать поручителя" OnClick="btnGuarantSearch_Click" /><br />
                                  <%--  <asp:Button ID="btnGuarantorEdit" runat="server" Text="Редактировать" Enabled="False" />--%>
                                </td>
                            </tr>
				            <tr>
					            <td><label>Фамилия:</label></td>
                                <td><asp:TextBox ID="tbGuarantorSurname" class="form-control" runat="server" Enabled="False"></asp:TextBox></td>
				            </tr>
				            <tr>
                                <td><label>Имя:</label></td>
                                <td>
                                    <asp:TextBox ID="tbGuarantorName" runat="server" class="form-control" Enabled="False"></asp:TextBox>
                                </td>
				            </tr>
				            <tr>
					            <td><label>Отчество</label></td>
                                <td>
                                    <asp:TextBox ID="tbGuarantorOtchestvo" runat="server" class="form-control" Enabled="False"></asp:TextBox>
                                </td>
				            </tr>
				            <tr>
					            <td><label>ИНН:</label></td>
                                <td><asp:TextBox ID="tbGuarantorINN" runat="server" class="form-control" Enabled="False"></asp:TextBox></td>
				            </tr>
			            </table>

                        </asp:Panel>
                          <%--</ContentTemplate>
                          </asp:UpdatePanel>--%>
                 
                     
                       <br />
                <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>--%>
            
          <asp:Panel ID="pnlGuarantees" runat="server" Visible="false">
                <table>
                <tr>
				    <td></td>
					<td><label>Залогодатель</label></td>
				</tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnPledgerSearch" class="btn btn-primary" runat="server" Text="Выбрать залогодателя" OnClick="btnPledgerSearch_Click" /><br />
                        <%--<asp:Button ID="btnPledgerEdit" class="btn btn-primary" runat="server" Text="Редактировать" Enabled="False" />--%>
                    </td>
                </tr>
				<tr>
					<td><label>Фамилия:</label></td>
                    <td><asp:TextBox ID="tbPledgerSurname" runat="server" Enabled="False" class="form-control"></asp:TextBox></td>
				</tr>
				<tr>
                    <td><label>Имя:</label></td>
                    <td>
                        <asp:TextBox ID="tbPledgerName" runat="server" Enabled="False" class="form-control"></asp:TextBox>
                    </td>
				</tr>
				<tr>
					<td><label>Отчество</label></td>
                    <td>
                        <asp:TextBox ID="tbPledgerOtchestvo" runat="server" Enabled="False" class="form-control"></asp:TextBox>
                    </td>
				</tr>
				<tr>
					<td><label>ИНН:</label></td>
                    <td><asp:TextBox ID="tbPledgerINN" runat="server" Enabled="False" class="form-control"></asp:TextBox></td>
				</tr>
			    </table>
               <div class="table-responsive">
                <asp:GridView ID="gvGuarantees" runat="server" AutoGenerateColumns="False"  ShowHeader="true" ShowFooter = "true"  DataKeyNames="ID" Width="584px" OnRowCancelingEdit="gvGuarantees_RowCancelingEdit" OnRowCommand="gvGuarantees_RowCommand" OnRowEditing="gvGuarantees_RowEditing" OnRowUpdating="gvGuarantees_RowUpdating">
										<Columns>
                                            <asp:TemplateField HeaderText = "ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
											<asp:TemplateField  HeaderText = "Наименование имущества">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField  HeaderText = "Количество">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCount" runat="server" Text='<%# Eval("Count")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                <asp:TextBox ID="txtCount" runat="server" Text='<%# Eval("Count")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCount" runat="server" class="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

											<asp:TemplateField  HeaderText = "Описание залогового имущества">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server"
                                                            Text='<%# Eval("Description")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" 
                                                        Text='<%# Eval("Description")%>'></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDescription" runat="server" class="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText = "Место нахождения имущества">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtAddress" runat="server" Text='<%# Eval("Address")%>' class="form-control"></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAddress" runat="server" class="form-control"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText = "Рыночная стоимость (сом)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMarketPrice" runat="server" Text='<%# Eval("MarketPrice")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" class="form-control" ID="txtMarketPrice" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" TabIndex="13" ReadOnly="false" Text='<%# Eval("MarketPrice")%>' ClientIDMode="Static" onchange="demo.txtMarketPrice()" ></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox runat="server" class="form-control" ID="txtMarketPrice" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" TabIndex="13" ReadOnly="false" ClientIDMode="Static" onchange="demo.txtMarketPrice()" ></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText = "Коэффициент ликвидности (0,1 - 1)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCoefficient" runat="server" Text='<%# Eval("Coefficient")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCoefficient" runat="server" Text='<%# Eval("Coefficient")%>' class="form-control" onchange="demo.txtCoefficient()"></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCoefficient" runat="server" class="form-control" ClientIDMode="Static" onchange="demo.txtCoefficient()"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText = "Залоговая стоимость (сом)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssessedPrice" runat="server" Text='<%# Eval("AssessedPrice")%>' ></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" class="form-control" Width="100px" ID="txtAssessedPrice" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" TabIndex="13" ReadOnly="true" Enabled="false" Text='<%# Eval("AssessedPrice")%>' ClientIDMode="Static" onchange="demo.txtAssessedPrice()" ></asp:TextBox>
                                                </EditItemTemplate> 
                                                <FooterTemplate>
                                                    <asp:TextBox runat="server" class="form-control" Width="100px" ID="txtAssessedPrice" Value="0" EmptyMessage="Введите цену" Type="Currency" MinValue="0" TabIndex="13" ReadOnly="true" Enabled="false" ClientIDMode="Static" onchange="demo.txtAssessedPrice()" ></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            


											<asp:TemplateField HeaderText="Операции">
												<ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelItem" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Del" OnClientClick = "return confirm('Do you want to delete?')">Delete</asp:LinkButton>
												</ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick = "AddNewGuarant"/>
                                                    </FooterTemplate>
											</asp:TemplateField>
                                            <asp:CommandField  ShowEditButton="True" />
										</Columns>
									</asp:GridView>
                   </div>
                <asp:Label ID="lblgvGuaranteesError" runat="server" Text="" ForeColor="Red"></asp:Label>

            </asp:Panel>   

               <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
                       <br />
                       <br />
                      
                 </div>

                
                   

                <%--***********************************************--%>



                 <div class="col-md-4 panels">

                    
                       <div>
                            <p>Цель кредита:
			                <asp:TextBox ID="tbCreditPurpose" runat="server" class="form-control" TabIndex="1"></asp:TextBox></p>
				            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbCreditPurpose" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
		               </div>
                      <div>
                            <p>Продукт:
     		                <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" class="form-control">
                                        <asp:ListItem Value="92">Потребительский кредит</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                      </div>
                      <div>
                            <p>% Cтавка:
				            <asp:DropDownList ID="ddlRequestRate" runat="server" OnSelectedIndexChanged="ddlRequestRate_SelectedIndexChanged" AutoPostBack="True" class="form-control">
                               <asp:ListItem Value="30" Selected="True" >30</asp:ListItem>
                            </asp:DropDownList>
                                </p>
                      </div>
                      <div>
                            <p>Срок кредита:
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
			                <asp:DropDownList ID="ddlRequestPeriod" runat="server" TabIndex="2" ClientIDMode="Static" OnSelectedIndexChanged="ddlRequestPeriod_SelectedIndexChanged" AutoPostBack="True" class="form-control">
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
                                    </ContentTemplate>
                            </asp:UpdatePanel>
                                <p>
                                </p>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlRequestPeriod" CssClass="SaveRequest" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest"></asp:RequiredFieldValidator>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                <p>
                                </p>
                                </p>
                        </div>
                       <%-- <div>
                            <p>Комиссия:
                            <asp:Label ID="lblCommission" runat="server" Text="0"></asp:Label></p>
                        </div>--%>
                        <div>
                            <asp:DropDownList ID="ddlOffice" runat="server" Visible="False" class="form-control">
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
                                    <asp:Button ID="btnSaveOffice" runat="server" Height="29px" Text="Сохранить" class="btn btn-primary" OnClick="btnSaveOffice_Click" />
                        </div>
                        <div>
                            <asp:Label ID="lblOrgINN" runat="server" Text="ИНН организации"></asp:Label>
                            <asp:TextBox ID="tbINNOrg" runat="server" AutoPostBack="True" OnTextChanged="tbINNOrg_TextChanged" class="form-control"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbINNOrg" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>--%>
					        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" CssClass="SaveRequest" ControlToValidate="tbINNOrg" ValidationExpression="\d\d\d\d\d\d\d\d\d\d\d\d\d\d"></asp:RegularExpressionValidator>
                        </div>
                        <div>
                            <p>Сумма кредита:
                            <asp:TextBox ID="RadNumTbRequestSumm" runat="server" class="form-control" Type="Currency" MinValue="0" Enabled="true" onchange="demo.valueChanged3()" TabIndex="15" ReadOnly="false" ClientIDMode="Static"></asp:TextBox>
						    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Ошибка!" ControlToValidate="RadNumTbRequestSumm" ValidationGroup="SaveRequest" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                            </p>
                        </div>
                        <div>
                            <%--<label>Ежемесячный платеж:</label>--%>
                            <asp:TextBox ID="RadNumTbMonthlyInstallment" class="form-control" runat="server" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="false" TabIndex="16" visible="false"></asp:TextBox>
                        </div>
                        <div>
                                <p>Предполагаемая дата погашения
								<asp:TextBox ID="tbActualDate" runat="server" class="form-control" ClientIDMode="Static" TabIndex="17" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)" ></asp:TextBox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="tbActualDate" CssClass="SaveRequest"></asp:RequiredFieldValidator>
							    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="дд.мм.гггг (возможные дни погашения: 2-19)" ValidationExpression="(?:02|03|04|05|06|07|08|09|10|11|12|13|14|15|16|17|18|19).\d\d.\d\d\d\d" ValidationGroup="SaveRequest" ControlToValidate="tbActualDate" CssClass="SaveRequest"></asp:RegularExpressionValidator>--%>
                                </p>
                        </div>


                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                         <ContentTemplate>
                        <div>
                            <p>Выдача кредита:</p>
                            <asp:RadioButtonList ID="rbtnTypeOfIssue" runat="server" OnSelectedIndexChanged="rbtnTypeOfIssue_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="Касса">Касса</asp:ListItem>
                                <asp:ListItem Value="Карта">Карта</asp:ListItem>
                            </asp:RadioButtonList>
                            <p>
                            <asp:Label ID="lblCardNumber" runat="server" Text="Номер карты:" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtCardNumber" class="form-control" runat="server" Width="221px" Visible="false"></asp:TextBox>
                            </p>
                        </div>
                        </ContentTemplate>
                     </asp:UpdatePanel>
                        <br />
                        <br />


                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                         <ContentTemplate>
                       <asp:RadioButtonList ID="rbtnBusiness" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnBusiness_SelectedIndexChanged" AutoPostBack="True">
							<asp:ListItem Selected="True">Работа по найму</asp:ListItem>
							<asp:ListItem>Бизнес</asp:ListItem>
                            <asp:ListItem>Агро</asp:ListItem>
						</asp:RadioButtonList>
						<asp:Panel ID="pnlEmployment" runat="server">
                                <diV>
                                        <asp:CheckBox ID="chbEmployer" runat="server" Text="Сотрудник" AutoPostBack="True" OnCheckedChanged="chbEmployer_CheckedChanged" Visible="False"/>
                                </diV>
								<div>
                                    <p>Общая сумма заработной платы
                                        <asp:TextBox ID="RadNumTbSumMonthSalary" runat="server" class="form-control" Value="0" EmptyMessage="" Type="Currency" MinValue="0" onchange="demo.valueChanged3()" AutoPostBack="false" TabIndex="18" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfAverageMonthSalary" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbSumMonthSalary" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                                <div>
                                    <p>Количество месяцев
                                    <asp:DropDownList ID="ddlMonthCount" runat="server" TabIndex="1" ClientIDMode="Static" class="form-control">
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
                                        </p>
                                </div>
                                <div>
									<p>Среднемес. заработная плата:
                                        <asp:TextBox ID="RadNumTbAverageMonthSalary" Name="AverageMonthSalary" class="form-control" runat="server" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" Enabled="true" ClientIDMode="Static"></asp:TextBox>
                                    </p>
                                </div>
						</asp:Panel>
						<asp:Panel ID="pnlBusiness" runat="server" Visible="false">
                               <div>
									<p>Минимальная выручка в день:
                                    <asp:TextBox ID="RadNumTbMinRevenue" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbMinRevenue()"></asp:TextBox>
        							<asp:RequiredFieldValidator ID="rfMinRevenue" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbMinRevenue" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                    </p>
								</div>
                                <div>
									<p>Максимальная выручка в день:
                                    <asp:TextBox ID="RadNumTbMaxRevenue" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbMaxRevenue()"></asp:TextBox>
     								<asp:RequiredFieldValidator ID="rfMaxRevenue" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbMaxRevenue" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                    </p>
								</div>
                                <div>
                                    <p>Количество рабочих дней в мес:</p>
                                    <asp:DropDownList ID="ddlCountWorkDay" runat="server" TabIndex="1" class="form-control" ClientIDMode="Static" >
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
                                </div>
								<div>
									<p>Средняя наценка % (в торговле):
                                    <asp:TextBox ID="RadNumTbСostPrice" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="20" ClientIDMode="Static" onchange="demo.RadNumTbСostPrice()"></asp:TextBox>
									<asp:RequiredFieldValidator ID="rfСostPrice" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbСostPrice" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                    </p>
								</div>
								<div>
									<p>Расходы по бизнесу в месяц (аренда, охрана, патент, транспорт, з/п и др.расходы связанные с бизнесом):
                                    <asp:TextBox ID="RadNumTbOverhead" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="21" ClientIDMode="Static" onchange="demo.RadNumTbOverhead()"></asp:TextBox>
     								<asp:RequiredFieldValidator ID="rfOverhead" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbOverhead" CssClass="SaveRequest"></asp:RequiredFieldValidator>
									</p>
								</div>
                                <div>
                                    <p><asp:Label ID="lblBusinessComment" runat="server" Text="Комментарии по бизнесу :</label>"></asp:Label>
                                    <asp:TextBox ID="txtBusinessComment" runat="server" class="form-control" TextMode="MultiLine" Rows="8" Width="300px" style = "resize:none" ></asp:TextBox>
                                    </p>
                                </div>
						</asp:Panel>
                        <asp:Panel ID="pnlAgro" runat="server" Visible="false">
							<div>
                                <div>
                                    <p>Отрасль сельского хозяйства
                                    <asp:TextBox ID="txtbxKindOfAgriculture" runat="server" class="form-control"></asp:TextBox>
                                    </p>
                                </div>
                                <div>
                                    <p>КРС(быки)
                                    <asp:TextBox ID="txtBulls" runat="server" class="form-control"></asp:TextBox>
                                    </p>
                                </div>
                                <div>
                                    <p>КРС(Дойные коровы)
                                    <asp:TextBox ID="txtbxBairyCows" runat="server" class="form-control"></asp:TextBox>
                                    </p>
                                </div>
                                <div>
                                    <p>МРС
                                    <asp:TextBox ID="txtbxSheeps" runat="server" class="form-control"></asp:TextBox>
                                    </p>
                                </div>
                                <div>
                                    <p>Лошади
                                    <asp:TextBox ID="txtbxHorse" runat="server" class="form-control"></asp:TextBox>
                                    </p>
                                </div>
                                 <div>
                                        <p>Опыт в живодноводстве
                                        <asp:TextBox ID="txtbxExperienceAnimals" runat="server" class="form-control"></asp:TextBox>
                                        </p>
                                </div>
                                <div>
                                        <p>Выручка от продажи скота за последние 3 мес:
                                        <asp:TextBox ID="RadNumTbRevenueAgro" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbRevenueAgro()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenueAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                        </p>
								</div>
                                <div>
                                        <p>Выручка от продажи молока в день:
                                        <asp:TextBox ID="RadNumTbRevenueMilk" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbRevenueMilk()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenueMilk" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                        </p>
								</div>
                                <div>
                                        <p>Выручка от продажи молочной продукции в день:
                                        <asp:TextBox ID="RadNumTbRevenueMilkProd" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbRevenueMilk()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbRevenueMilkProd" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                        </p>
								</div>
                                <div>
                                        <p>Расходы на содержание скота за последние 3 мес:
                                        <asp:TextBox ID="RadNumTbOverheadAgro" runat="server" class="form-control"  Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="19" ClientIDMode="Static" onchange="demo.RadNumTbOverheadAgro()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbOverheadAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                        </p>
								</div>
								<div>
                                        <p>Дополнительные расходы в мес:
                                        <asp:TextBox ID="RadNumTbAddOverheadAgro" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="21" ClientIDMode="Static" onchange="demo.RadNumTbAddOverheadAgro()"></asp:TextBox>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbAddOverheadAgro" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                        </p>
								</div>
                                <div>
                                       <p>
                                       <asp:Label ID="Label1" runat="server" Text="Комментарии по бизнесу :</label>"></asp:Label>
                                       <asp:TextBox ID="txtAgroComment" runat="server" class="form-control" TextMode="MultiLine" Rows="8" Width="300px" style = "resize:none"></asp:TextBox>
                                       </p>
                                </div>

							</div>
						</asp:Panel>
                           </ContentTemplate>
                     </asp:UpdatePanel>

                                <div>
									<p>Семейные расходы
                                        <asp:TextBox ID="RadNumTbFamilyExpenses" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22" ClientIDMode="Static" onchange="demo.RadNumTbFamilyExpenses()">7000</asp:TextBox>
										<asp:RequiredFieldValidator ID="rfFamilyExpenses" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveRequest" ControlToValidate="RadNumTbFamilyExpenses" CssClass="SaveRequest"></asp:RequiredFieldValidator>
                                    </p>
								</div>
                                <div>
									<p>Дополнительные доходы:
                                        <asp:TextBox ID="RadNumTbAdditionalIncome" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="22" ClientIDMode="Static" onchange="demo.RadNumTbAdditionalIncome()"></asp:TextBox>
                                    </p>
                                </div>
                                <div>
                                    <p><asp:Label ID="lblOtherLoans" runat="server" Text="Взносы по др.кредитам:</label>"></asp:Label>
                                    <asp:TextBox ID="RadNumOtherLoans" runat="server" class="form-control" Value="0" EmptyMessage="0.00" Type="Currency" MinValue="0" TabIndex="23" Visible="true" ClientIDMode="Static" onchange="demo.RadNumOtherLoans()"></asp:TextBox>
                                    </p>
                                </div>
                    





                     
                      


                    

                      
            
                        <br />

                        
                  </div>

                
                
                
                 

               
                <%--***********************************--%>


                 <div class="col-md-4 panels">
                    
		        <div>

            
            
            <%--<hr />--%>
                     <div class="chkbxhead"> <p style="color:green">
                         <asp:Label ID="lblDocuments" runat="server" Text="Документы"></asp:Label></p></div>

                    <asp:GridView ID="gvRequestsFiles2" runat="server" HeaderStyle-BackColor="#336666" HeaderStyle-ForeColor="White"
				AlternatingRowStyle-ForeColor="#000" CssClass="gvRequestsFiles" AutoGenerateColumns="false" OnRowCommand="gvRequestsFiles_RowCommand">
				<Columns>
					<asp:BoundField DataField="Name" HeaderText="Имя файла" ControlStyle-Width="100px" ItemStyle-CssClass="grdSearchResultbreakword" />
					<asp:BoundField DataField="FileDescription" HeaderText="Описание" ControlStyle-Width="70px" ItemStyle-CssClass="grdSearchResultbreakword" />
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# fileupl + Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
          
				</Columns>
			</asp:GridView>
            <asp:GridView ID="gvRequestsFilesPhoto2" runat="server" AutoGenerateColumns="False">
                <Columns>
					<asp:BoundField DataField="Name" HeaderText="Фото" />
					<%--<asp:BoundField DataField="FileDescription" HeaderText="Описание" />--%>
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# fileupl + Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
          
				</Columns>
            </asp:GridView>



            <div class="chkbxhead"> <p style="color:green">
                <asp:Label ID="lblNameDcbServ" runat="server" Text="DCB-Сервисы"></asp:Label></p></div>

            <div>
            <asp:Button ID="btnPassport" runat="server" Text="Проверить паспорт" class="btn btn-primary" OnClick="btnPassport_Click" />
            </div>

            <div>
            <asp:Button ID="btnKIB" runat="server" Text="КИБ (полный)" class="btn btn-primary" OnClick="btnKIB_Click" />  
            <asp:Button ID="btnKIB2" runat="server" Text="КИБ (сжатый)" class="btn btn-primary" OnClick="btnKIB2_Click" />
            </div>

            <div>
            <asp:Button ID="btnAnimals" runat="server" Text="Аймак (кол-во скота)" class="btn btn-primary" OnClick="btnAnimals_Click" />
           </div>

            <div>
            <asp:Button ID="btnGns" runat="server" Text="ГНС" class="btn btn-primary" OnClick="btnGns_Click" />
           </div>

            <div>
            <asp:Button ID="btnInitPerm" runat="server" Text="Запрос на разрешение" class="btn btn-primary" OnClick="btnInitPerm_Click" />
           </div>

            <div>
            <asp:TextBox ID="txtCodePerm" runat="server" class="form-control" CssClass ="txtCodePerm"></asp:TextBox>
            <asp:Button ID="btnConfPerm" runat="server" OnClick="btnConfPerm_Click" class="btn btn-primary" Text="Отравить код" />
            </div>

            <div>
            <asp:Button ID="btnSalary" runat="server" OnClick="btnSalary_Click" class="btn btn-primary" Text="Зарплата/Страх.взносы" />
            </div>

            <div>
            <asp:Button ID="btnPension" runat="server" OnClick="btnPension_Click" class="btn btn-primary" Text="Пенсия" />
            </div>

          
            <%--<hr />--%>


            <div class="chkbxhead"><p style="color:green"><asp:Label ID="lblNameSMS" runat="server" Text="СМС"></asp:Label></p></div>

            <div>
            <asp:Button ID="btnShowSMS" runat="server" OnClick="btnShowSMS_Click" Text="Текст СМС - согласие" class="btn btn-primary" Width="156px" /> &nbsp;&nbsp <asp:Button ID="btnShowSMS2" runat="server"  Text="Текст СМС - график" class="btn btn-primary" OnClick="btnShowSMS2_Click" Width="156px" />
            </div>

            <div>
            <asp:Label ID="lblSMS" runat="server" Text="" ForeColor="#CC3300"></asp:Label>
            </div>
            
            <div>
            <asp:Button ID="btnSendSMS" runat="server" class="btn btn-primary" OnClick="btnSendSMS_Click"  Text="Отправить СМС" />
            </div>

            <div>
            <asp:Button ID="btnCheckSMS" runat="server" class="btn btn-primary" OnClick="btnCheckSMS_Click" Text="Проверить Код от клиента" />
            <asp:Label ID="lblCheckSMS" runat="server" Text=""></asp:Label>
           </div>

            <div>
            <asp:GridView ID="gvLogDcbService" runat="server" AutoGenerateColumns="false" OnRowCommand="gvLogDcbService_RowCommand">
                <Columns>
                    <asp:BoundField DataField="KindService" HeaderText="Вид запроса" />
                    <asp:BoundField DataField="DateCheck" HeaderText="Дата" />
                    <asp:BoundField DataField="Username" HeaderText="Пользователь" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%--<a target="_blank" href="<%# fileupl + Eval("FullName") %>" >Скачать</a>--%>
                            <asp:LinkButton ID="lnbtnDcbServ" runat="server" CommandArgument='<%# Eval("ServiceRequestID") %>' CommandName="Open">Выбрать</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>

          

            


		</div>
					
                </div>


                <%--/***********************************/--%>
                 
                
                 
                 
        </div> 
            
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
            <%--<asp:TextBox ID="tbNote" runat="server" class="form-control" ClientIDMode="Static" TextMode="MultiLine" Width="0" Height="0"></asp:TextBox>--%>
        <asp:HiddenField ID="hfNote" runat="server" ClientIDMode="Static"/>
    </div>
    <%--<div role="tabpanel" class="tab-pane" id="office">--%>
    <div data-content="remediation" class='tab-content'>
        <div id="divDocuments" class="divDocuments" runat="server" visible="false">
            
			<p><b>Документы:</b></p>
			<div>
                <p>Выберите файл:
			    <asp:FileUpload ID="FileUploadControl" runat="server" />
                </p>
            </div>
            <div>
                <p>Описание файла:
			    <asp:TextBox ID="tbFileDescription" runat="server" class="form-control"></asp:TextBox>
                </p>
            </div>

                <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>    --%>
			<asp:Button ID="btnUploadFiles" runat="server" Text="Загрузить" Enabled="False" class="btn btn-primary" OnClick="btnUploadFiles_Click" />
            <asp:Button runat="server" id="UploadButton" text="Upload" onclick="UploadButton_Click" class="btn btn-primary" Visible="False" />
            <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
			<asp:GridView ID="gvRequestsFiles" runat="server" HeaderStyle-BackColor="#336666" HeaderStyle-ForeColor="White"
				AlternatingRowStyle-ForeColor="#000" CssClass="gvRequestsFiles" AutoGenerateColumns="false" OnRowCommand="gvRequestsFiles_RowCommand">
				<Columns>
					<asp:BoundField DataField="Name" HeaderText="Имя файла" ControlStyle-Width="100px" ItemStyle-CssClass="grdSearchResultbreakword" />
					<asp:BoundField DataField="FileDescription" HeaderText="Описание" ControlStyle-Width="70px" ItemStyle-CssClass="grdSearchResultbreakword"/>
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# fileupl + Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
          
				</Columns>
			</asp:GridView>
            <asp:GridView ID="gvRequestsFilesPhoto" runat="server" AutoGenerateColumns="False">
                <Columns>
					<asp:BoundField DataField="Name" HeaderText="Фото" />
					<%--<asp:BoundField DataField="FileDescription" HeaderText="Описание" />--%>
					<asp:TemplateField ItemStyle-HorizontalAlign="Center">
						<ItemTemplate><a target="_blank" href="<%# fileupl + Eval("FullName") %>" >Скачать</a></ItemTemplate>
					</asp:TemplateField>
          
				</Columns>
            </asp:GridView>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>

           </div>

    </div>
  

</div>
           
                   
          
            
		</asp:Panel>

		
<div data-content="main" class='tab-content'>

		<br />
		<asp:Panel ID="pnlMenuRequest" runat="server" ClientIDMode="Static">
           
                    <p>Поменять статус:</p>
                    
                    <asp:Button ID="btnNoConfirm" runat="server" Text="Не подтверждено" onClientclick="myComment()" Visible="false" class="btn btn-primary" OnClick="btnNoConfirm_Click"/>
                   
                    <asp:Button ID="btnFix" runat="server" Text="Исправить" onClientclick="myComment()" Visible="False" class="btn btn-primary" OnClick="btnFix_Click"  />
                    
                    <asp:Button ID="btnFixed" runat="server" Text="Исправлено" onClientclick="myComment()" Visible="False" class="btn btn-primary" OnClick="btnFixed_Click" />
                   
                    <asp:Button ID="btnConfirm" runat="server" Text="Подтверждено" OnClick="btnConfirm_Click" Visible="false" class="btn btn-success" onClientclick="myComment()"/>
                    
                    <asp:Button ID="btnApproved" runat="server" Text="Утверждено" Visible="false" onClientclick="myComment()" class="btn btn-success" OnClick="btnApproved_Click"/>
                    
                    <asp:Button ID="btnSignature" runat="server" Text="К подписи" Visible="false" onClientclick="myComment()" class="btn btn-success" OnClick="btnSignature_Click" />
                    
                    <button ID="btnCancelReq" runat="server" type="button" data-toggle="modal" data-target="#exampleModal" class="btn btn-danger">Отменено</button>
                   
                    <button ID="btnCancelReqExp" runat="server" type="button" data-toggle="modal" data-target="#exampleModal2" class="btn btn-danger">Отказано</button>
                    
					<asp:Button ID="btnIssue" runat="server" Text="Выдано" OnClick="btnIssue_Click" Visible="false" class="btn btn-success" onClientclick="myComment()" />
                   
                    <asp:Button ID="btnReceived" runat="server" Text="Принято" Visible="false" onClientclick="myComment()" class="btn btn-success" OnClick="btnReceived_Click"/>
                    
                    <asp:Button ID="btnInProcess" runat="server" Text="В обработке" Visible="false" onClientclick="myComment()" class="btn btn-primary" OnClick="btnInProcess_Click" />
                    
                    <asp:Button ID="btnCancelIssue" runat="server" Text="Отмена выдачи" Visible="false" onClientclick="myComment()" class="btn btn-danger" OnClick="btnCancelIssue_Click"/>
                    
                    <br /><br /><br />
                
			
					<asp:Button ID="btnAgreement" runat="server" Text="Договор" Visible="False" OnClientClick="openNewWin();" class="btn btn-primary" OnClick="btnAgreement_Click"/>
					
					<asp:Button ID="btnSozfondAgree" runat="server" Text="Заявление-Согласие" OnClientClick="openNewWin();" class="btn btn-primary" OnClick="btnSozfondAgree_Click" Visible="False"/>
					
					<asp:Button ID="btnReceptionAct" runat="server" Text="Акт приема-передачи" Visible="False" class="btn btn-primary" OnClick="btnReceptionAct_Click" />
					
					<asp:Button ID="btnPledgeAgreement" runat="server" Text="Договор о залоге" Visible="False" class="btn btn-primary" OnClick="btnPledgeAgreement_Click" />
					
					<asp:Button ID="btnProffer" runat="server" Text="Предложение-ЗП/ИП" Visible="False" class="btn btn-primary" OnClientClick="openNewWin();" OnClick="btnProffer_Click"/>
                    
                    <asp:Button ID="btnComment" runat="server" Text="Комментарий" onClientclick="myComment()" class="btn btn-primary" OnClick="btnComment_Click" Visible="False" />
                    
					<asp:Button ID="btnProfileNano" runat="server" Text="Анкета Нано" OnClientClick="openNewWin();" Visible="False" class="btn btn-primary" OnClick="btnProfileNano_Click" />
                    
                    <asp:Button ID="btnActAssessment" runat="server" Text="Акт оценки" Visible="False" OnClientClick="openNewWin();" class="btn btn-primary" OnClick="btnActAssessment_Click" />
				
                    
					
		</asp:Panel>
        <br /><br /><br />

    
					
					<asp:Button ID="btnNewRequest" runat="server" Text="Новая заявка" class="btn btn-primary" OnClick="btnNewRequest_Click1" />
					
					<asp:Button ID="btnSendCreditRequest" runat="server" Text="Сохранить" class="btn btn-success" OnClick="btnSendCreditRequest_Click" ValidationGroup="SaveRequest" Visible="False" onClientclick="myComment()"/>
					
                    <asp:Button ID="btnCloseRequest" runat="server" Text="Закрыть" class="btn btn-dark" OnClick="btnCloseRequest_Click" Visible ="false"/>

 </div>


	</asp:Panel>


	<asp:Panel ID="pnlMenuCustomer" runat="server" Visible="False" CssClass="panels">
        <asp:Button ID="btnBackToCreditForm" runat="server" Text="Назад" class="btn btn-primary" OnClick="btnBackToCreditForm_Click" />
        <br />
         <div>
             <p>
             <span class="fio">ИНН:</span>
			 <asp:TextBox ID="tbSearchINN" runat="server" class="form-control"></asp:TextBox>
			 <asp:RequiredFieldValidator ID="rfSearchCustomer" runat="server" ControlToValidate="tbSearchINN" ErrorMessage="Ошибка!" ValidationGroup="SearchCustomer"></asp:RequiredFieldValidator>
             </p>
		</div>
        
        <div>
            <p>
            <span class="fio">Серия№:</span>
            <asp:TextBox ID="tbSerialN" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfSerialN" runat="server" ControlToValidate="tbSerialN" ErrorMessage="Ошибка!" ValidationGroup="SearchCustomer"></asp:RequiredFieldValidator>
            </p>
        </div>
        <div>
            <asp:Button ID="btnSearchClient" runat="server" Text="Поиск" class="btn btn-primary" OnClick="btnSearchClient_Click" ValidationGroup="SearchCustomer" />
            <asp:Button ID="btnNewCustomer" runat="server" Text="Новый клиент" class="btn btn-primary" OnClick="btnNewCustomer_Click" />
            <asp:Button ID="btnCredit" runat="server" Text="Выбрать клиента" Enabled="False" class="btn btn-primary" OnClick="btnCredit_Click" Visible="False" />
        </div>
        <div>
            <asp:HiddenField ID="hfChooseClient" runat="server" />
            <asp:HiddenField ID="hfUserID" runat="server" />
	    	<asp:HiddenField ID="hfCustomerID" runat="server" Value="noselect" />
			<asp:HiddenField ID="hfGuarantorID" runat="server" Value="noselect" />
            <asp:HiddenField ID="hfPledgerID" runat="server" Value="noselect" />
			<asp:HiddenField ID="hfCreditID" runat="server" />
			<asp:HiddenField ID="hfRequestID" runat="server" />
            <asp:HiddenField ID="hfRequestStatus" runat="server" />
            <asp:HiddenField ID="hfAgentRoleID" runat="server" />
            <asp:HiddenField ID="hfRequestIDDel" runat="server" Value="-1" />
         </div>      
                    

	</asp:Panel>
  <%--  <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>--%>

	<asp:Panel ID="pnlCustomer" runat="server" Visible="False" CssClass="panels">
        
		<h3>Форма клиента</h3>
 <div class="row">
        <%--******************************************--%>
         <div class="col-md-4">
             <div>
                    <p>
                    <b>Данные клиента</b>
                    <asp:HiddenField ID="hfIsNewCustomer" runat="server" />
                    </p>
			</div>
             <div>  <p>
                    <span class="fio">Фамилия:</span>
					<asp:TextBox ID="tbSurname" runat="server" TabIndex="1" class="form-control"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfSurname" runat="server" ControlToValidate="tbSurname" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="tbSurname" ErrorMessage="в фио все буквы должны быть на русском" ValidationExpression="^[а-яА-ЯёЁъЪ -]{1,32}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
			 </div>
             <div>
                    <p>
                    <span class="fio">Имя:</span>
					<asp:TextBox ID="tbCustomerName" runat="server" TabIndex="2" class="form-control"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfCustomerName" runat="server" ControlToValidate="tbCustomerName" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="tbCustomerName" ErrorMessage="в фио все буквы должны быть на русском" ValidationExpression="^[а-яА-ЯёЁъЪ -]{1,32}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
                </div>
             <div>
                 <p>
                 <span class="fio">Отчество:</span>
				 <asp:TextBox ID="tbOtchestvo" runat="server" TabIndex="3" class="form-control"></asp:TextBox>
                 </p>
             </div>
             <div>
                 <p>
                 <span class="fio">ИНН:</span>
					<asp:TextBox ID="tbIdentificationNumber" runat="server" class="form-control" TabIndex="4" AutoPostBack="True" OnTextChanged="tbIdentificationNumber_TextChanged"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfIdentificationNumber" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbIdentificationNumber" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="rfedentificationNumber" runat="server" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers" ControlToValidate="tbIdentificationNumber" ValidationExpression="\d\d\d\d\d\d\d\d\d\d\d\d\d\d"></asp:RegularExpressionValidator>
                   </p>
			 </div>
             <div>
                 <p>
                    <span class="fio">Телефон:</span>
                    <asp:TextBox ID="tbContactPhone" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvContactPhone" runat="server" ControlToValidate="tbContactPhone" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers"></asp:RequiredFieldValidator>
                 
                    &nbsp;<asp:RegularExpressionValidator ID="revContactPhone" runat="server" ControlToValidate="tbContactPhone" CssClass="SaveCustomers" ErrorMessage="+996 ### ######" ValidationExpression="^\+\d{3}\ \d{3}\ \d{6}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                 </p>
			</div>
         </div>
         <%--******************************************--%>
         <div class="col-md-4">
             <div><b>Паспортные данные</b></div>
             <div>
                 <p><span class="fio">Вид паспорта:</span>
                    	<asp:DropDownList ID="ddlDocumentTypeID" class="chosen" runat="server" DataTextField="TypeName" DataValueField="DocumentTypeID" TabIndex="11" Width="260px" OnSelectedIndexChanged="ddlDocumentTypeID_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="3">Паспорт (серия AN) образца 2004 года</asp:ListItem>
                        <asp:ListItem Value="14">Биометрический паспорт (серия ID)</asp:ListItem>
                    </asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfDocumentType" runat="server" ControlToValidate="ddlDocumentTypeID" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                 </p>
             </div>
				  
				
               <div>
                    <p>
                    <span class="fio">Серия и номер:</span>
					<asp:TextBox ID="tbDocumentSeries" runat="server" class="form-control" TabIndex="12" OnTextChanged="tbDocumentSeries_TextChanged" AutoPostBack="True"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfDocumentSeries" runat="server" ControlToValidate="tbDocumentSeries" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="revDocumentSeries" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbDocumentSeries" ValidationExpression="[A-Z][A-Z]\d\d\d\d\d\d\d" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
				</div>
                <div>
                    <p>
                    <span class="fio">Кем выдан:</span>
					<asp:TextBox ID="tbIssueAuthority" runat="server" class="form-control" ValidationGroup="SaveCustomers" TabIndex="19"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbIssueAuthority" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    </p>
				</div>
				<div>
                    <p>
                    <span class="fio">Дата выдачи:</span>
					<asp:TextBox ID="tbIssueDate" runat="server" class="form-control" ClientIDMode="Static" TabIndex="20" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbIssueDate" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbIssueDate" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
				</div>
                <div>
                    <p>
                    <asp:RadioButtonList ID="rbtnlistValidTill" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbtnlistValidTill_SelectedIndexChanged">
                        <asp:ListItem Selected="True">Дата окончания</asp:ListItem>
                        <asp:ListItem>Бессрочный</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:TextBox ID="tbValidTill" runat="server" class="form-control" ClientIDMode="Static" TabIndex="20" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvValidTill" runat="server" ControlToValidate="tbValidTill" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="revValidTill" runat="server" ControlToValidate="tbValidTill" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
                </div>
                <div> 
                    <p>
                    <span class="fio">Дата рождения:</span>
					<asp:TextBox ID="tbDateOfBirth" runat="server" class="form-control" ClientIDMode="Static" TabIndex="13" onfocus="this.select();lcs(this)" onclick="event.cancelBubble=true;this.select();lcs(this)"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfDateOfBirth" runat="server" ControlToValidate="tbDateOfBirth" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="tbDateOfBirth" ErrorMessage="dd.mm.yyyy" ValidationExpression="\d\d.\d\d.\d\d\d\d" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
				</div>
				
				<div>
                    <p>
                    <span class="fio">Пол</span><asp:RadioButtonList ID="rbtSex" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtSex_SelectedIndexChanged">
						<asp:ListItem Selected="True">Мужской</asp:ListItem>
						<asp:ListItem>Женский</asp:ListItem>
					</asp:RadioButtonList>
                    </p>
				</div>


         </div>
         <%--******************************************--%>
         <div class="col-md-4">
         
                <div><b>Адрес по прописке</b></div>
                <div>
                    <p>
					<asp:DropDownList ID="ddlRegistrationCountryID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="6"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfvRegistrationCountryID" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlRegistrationCountryID" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    </p>
				</div>
				<div>
                    <p><span class="fio">Нас.пункт:</span>
					<asp:DropDownList ID="ddlRegistrationCityName" class="chosen" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="7"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfvRegistrationCityName" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlRegistrationCityName" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    </p>
				</div>
				<div>
                    <p>
                    <span class="fio">Улица:</span>
					<asp:TextBox ID="tbRegistrationStreet" runat="server" class="form-control" TabIndex="8"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvRegistrationStreet" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbRegistrationStreet" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    </p>
				</div>
				<div>
                    <p>
                    <span class="fio">Дом:</span>
					<asp:TextBox ID="tbRegistrationHouse" runat="server" class="form-control" TabIndex="9"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvRegistrationHouse" runat="server" ErrorMessage="Ошибка!" ControlToValidate="tbRegistrationHouse" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				    <asp:RegularExpressionValidator ID="revRegistrationHouse" runat="server" ControlToValidate="tbRegistrationHouse" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
				    </p>
                </div>
				<div>
                    <p>
                    <span class="fio">Кв:</span>
					<asp:TextBox ID="tbRegistrationFlat" runat="server" class="form-control" TabIndex="10"></asp:TextBox>
				    <asp:RegularExpressionValidator ID="revRegistrationFlat" runat="server" ControlToValidate="tbRegistrationFlat" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" CssClass="SaveCustomers" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
				</div>
				
				
				
				
				

                <div>
                    <p>
                    <b>Адрес фактического проживания:</b>
                    <asp:CheckBox ID="chkbxAsRegistration" runat="server" OnCheckedChanged="chkbxAsRegistration_CheckedChanged" Text="как в прописке" AutoPostBack="True" />
                    </p>
				<div>
                    <p>
					<asp:DropDownList ID="ddlResidenceCountryID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="14"></asp:DropDownList>
                    </p>
				</div>
                <div>
                    <p>
                    <span class="fio">Нас.пункт:</span>
					<asp:DropDownList ID="ddlResidenceCityName" class="chosen" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="15"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfvResidenceCityName" runat="server" ControlToValidate="ddlResidenceCityName" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    </p>
				</div>
                <div>
                    <p>
                    <span class="fio">Улица:</span>
					<asp:TextBox ID="tbResidenceStreet" runat="server" class="form-control" TabIndex="16"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvResidenceStreet" runat="server" ControlToValidate="tbResidenceStreet" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
                    </p>
				</div>
				<div>
                    <p>
                    <span class="fio">Дом:</span>
					<asp:TextBox ID="tbResidenceHouse" runat="server" class="form-control" TabIndex="17"></asp:TextBox>
					<asp:RequiredFieldValidator ID="rfvResidenceHouse" runat="server" ControlToValidate="tbResidenceHouse" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				    <asp:RegularExpressionValidator ID="revResidenceHouse" runat="server" ControlToValidate="tbResidenceHouse" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
				</div>
                <div>
                    <p>
                    <span class="fio">Кв:</span>
					<asp:TextBox ID="tbResidenceFlat" runat="server" class="form-control" TabIndex="18"></asp:TextBox>
				    <asp:RegularExpressionValidator ID="revResidenceFlat" runat="server" ControlToValidate="tbResidenceFlat" CssClass="SaveCustomers" ErrorMessage="Ошибка!" ValidationExpression="^[а-яА-ЯёЁa-zA-Z0-9 -/.,]{0,10}$" ValidationGroup="SaveCustomers"></asp:RegularExpressionValidator>
                    </p>
				</div>

         </div>
        
  </div>
        <%--******************************************--%>
 </div>	
				<div>
					<asp:RadioButtonList ID="rbtIsResident" runat="server" RepeatDirection="Horizontal" Visible="False">
						<asp:ListItem Selected="True">Резидент</asp:ListItem>
						<asp:ListItem>Неризидент</asp:ListItem>
					</asp:RadioButtonList>
				</div>
									
				<div>
					<asp:DropDownList ID="ddlNationalityID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="5"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfCountry" Visible="false" runat="server" ErrorMessage="Ошибка!" ControlToValidate="ddlNationalityID" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</div>

              
				<div><asp:DropDownList ID="ddlBirthCountryID" runat="server" DataTextField="ShortName" DataValueField="CountryID" Visible="False" TabIndex="21"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfBirthofCountry" Visible="false" runat="server" ControlToValidate="ddlBirthCountryID" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator></div>
				
			
				<div><asp:DropDownList ID="ddlBirthCityName" runat="server" DataTextField="CityName" DataValueField="CityID" TabIndex="22" Visible="False"></asp:DropDownList>
					<asp:RequiredFieldValidator ID="rfBirthCityName" Visible="false" runat="server" ControlToValidate="ddlBirthCityName" ErrorMessage="Ошибка!" ValidationGroup="SaveCustomers" CssClass="SaveCustomers"></asp:RequiredFieldValidator>
				</div>
				
			     
				
			
		

		<asp:Panel ID="pnlSaveCustomer" runat="server">
			<table>
				<tr>
					<td>&nbsp;</td>
					<td><asp:Button ID="btnSaveCustomer" runat="server" Text="Сохранить" ValidationGroup="SaveCustomers" class="btn btn-primary" OnClick="btnSaveCustomer_Click" /></td>
					<td>&nbsp;</td>
					<td><asp:Button ID="btnCancel" runat="server" Text="Отменить" class="btn btn-dark" OnClick="btnCancel_Click" /></td>
					<td>&nbsp;</td>
					<td></td>
				</tr>
			</table>
		</asp:Panel>

	</asp:Panel>
	
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>


</asp:Panel> <!--pnlUser-->

   <!---------------------------------------------------------------------------------------------------------------------------->
    <!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal" aria-label="Close"/>
          <span aria-hidden="true">&times; id="exampleModalLabel">Вы действительно хотите отменить заявку?</span>
      </div>
        
      <div class="modal-body">
          
        <div class="form-group">
            
            <label for="message-text" class="col-form-label">Комментарий:</label>
            <br /><br />
           <%-- <textarea class="form-control" id="message-text"></textarea>--%>
            <asp:TextBox ID="tbNoteCancelReq" runat="server" class="form-control" ClientIDMode="Static" TextMode="MultiLine" Width="568" Height="120" ></asp:TextBox>
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
            <asp:TextBox ID="tbNoteCancelReqExp" runat="server" class="form-control" ClientIDMode="Static" TextMode="MultiLine" Width="568" Height="120" ></asp:TextBox>
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
<!---------------------------------------------------------------------------------------------------------------------------->
 
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
            if (tabName == 'main')
            {
                sessionStorage.setItem('tab', '1');
            }
            if (tabName == 'remediation')
            {
                sessionStorage.setItem('tab', '2');
            }
            $('.tabs li a').removeClass('selected');
            $targ.addClass('selected');
            $('[data-content]').removeClass('fade-in').hide();

            var targetContent = '[data-content=' + tabName + ']';
            $(targetContent).addClass('fade-in').show();
        }

    });
</script>

<script>
    $(document).ready(function () {
        //this.Session["description"] = description;    
        //var tab = '@Session["tab"]';
        var tab = sessionStorage.getItem('tab');
        if (tab == 2) {

            $('.tabs li a').removeClass('active');
            $('.tabs li a').removeClass('selected');
            //var li = $('.tabs li a').first();
            var li = $('.tabs li');
            var next = $('.tabs li:nth-child(2) a')
            next.addClass('active selected');


            //next.style.backgroundColor = '#eee';
            $('[data-content]').removeClass('fade-in').hide();

            var targetContent = '[data-content=remediation]';
            $(targetContent).addClass('fade-in').show();

            
            

        } else
        {

            $('.tabs li a').removeClass('active');
            $('.tabs li a').removeClass('selected');
            //var li = $('.tabs li a').first();
            var li = $('.tabs li');
            var next = $('.tabs li:nth-child(1) a')
            next.addClass('active selected');


            //next.style.backgroundColor = '#eee';
            $('[data-content]').removeClass('fade-in').hide();

            var targetContent = '[data-content=main]';
            $(targetContent).addClass('fade-in').show();
            
        }
    })   

</script>

<script>
    //MainContent_chkbxTypeOfCollateral_2
    //$('#MainContent_chkbxTypeOfCollateral_2').click(function () {
    //    ;
    //    ;
    //    ;
    //})


    //$(document).ready(function () {
    //    alert('ok');
    //});​
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


    function sumChanged() {
        document.getElementById('tbRequestSumm').value = RadNumTbTotalPrice.get_value() - document.getElementById('tbAmountOfDownPayment').value;
    }
</script>



<script type="text/javascript">
    var previousRow;

    function ChangeRowColor(row) {
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
            //price = RadNumTbTotalPrice.get_value();
            price = document.getElementById("RadNumTbTotalPrice").value;
            amount10 = 10 * price / 100;
            RadNumTbAmountOfDownPayment.set_value(amount10);
            request = price - amount10;
            //RadNumTbRequestSumm.set_value(request);
            //if (request.indexOf('hello') > -1) request = request.replace('.',',');
            document.getElementById("RadNumTbRequestSumm").value = request.toFixed(2).replace('.', ',');
            n = parseInt(document.getElementById("ddlRequestPeriod").options[document.getElementById("ddlRequestPeriod").selectedIndex].value);
            stavka = parseInt(document.getElementById('<%=ddlRequestRate.ClientID%>').value);
            s = RadNumTbTotalPrice.get_value() - amount10;
            ii = stavka / 12 / 100;
            k = (((Math.pow((1 + ii), n)) * (ii)) * s) / ((Math.pow((1 + (ii)), n)) - 1);
        }
        demo.valueChanged2 = function (sender, args) {
            //price = RadNumTbTotalPrice.get_value();
            price = parseFloat(document.getElementById("RadNumTbTotalPrice").value);
            stavka = document.getElementById('<%=ddlRequestRate.ClientID%>').value;

            amount1 = 0;
            if (stavka == '0,00') amount1 = 0.00;
            if (stavka == '29,90') {
                //if (price <= 50000) amount1 = 0; else amount1 = 10;
                amount1 = 10;
            }

            amount10 = amount1 * price / 100;
            //amount = RadNumTbAmountOfDownPayment.get_value();
            amount = parseFloat(document.getElementById("RadNumTbAmountOfDownPayment").value).toFixed(2).replace(',', '.');
            //if (amount >= amount10) { request = price - amount; }
            //else { request = ""; }

            if (stavka == 0) {
                request = price - amount;
            }
            else {
                if (amount >= amount10) { request = price - amount; }
                else { request = ""; }
            }
            //request = price - amount;

            n = parseInt(document.getElementById("ddlRequestPeriod").options[document.getElementById("ddlRequestPeriod").selectedIndex].value);

            //s = RadNumTbTotalPrice.get_value() - amount;
            s = document.getElementById("RadNumTbTotalPrice").value - amount;
            ii = stavka / 12 / 100;
            //k = (((Math.pow((1 + ii), n)) * (ii)) * s) / ((Math.pow((1 + (ii)), n)) - 1);
            //RadNumTbMonthlyInstallment.set_value(k.toFixed(0));
            //RadNumTbRequestSumm.set_value(request);
            //document.getElementById("RadNumTbAmountOfDownPayment").value = amount.replace('.',',');
            //document.getElementById("RadNumTbRequestSumm").value = request.toFixed(2).replace('.',',');
        }

        demo.valueChanged3 = function (sender, args) {
            //SumMonthSalary = RadNumTbSumMonthSalary.get_value();
            SumMonthSalary = parseFloat(document.getElementById("RadNumTbSumMonthSalary").value.replace(/,/, '.'));
            //document.getElementById("RadNumTbSumMonthSalary").value = SumMonthSalary;
            //SumMonthSalary = RadNumTbSumMonthSalary.get_value();
            CountSalary = parseInt(document.getElementById("ddlMonthCount").options[document.getElementById("ddlMonthCount").selectedIndex].value);
            AverageMonthSalary = SumMonthSalary / CountSalary
            //RadNumTbAverageMonthSalary.set_value(RadNumTbAverageMonthSalary.toFixed(0));
            //document.getElementById("RadNumTbAverageMonthSalary").value = AverageMonthSalary.toFixed(0);
            document.getElementById("RadNumTbSumMonthSalary").value = SumMonthSalary.toFixed(2).replace('.', ',');
            document.getElementById("RadNumTbAverageMonthSalary").value = AverageMonthSalary.toFixed(2).replace('.', ',');
            //document.getElementById("RadNumTbAverageMonthSalary").set_value = AverageMonthSalary;

        }
        /**********************/
        demo.cvalueChanged = function (sender, args) {
            //price = RadNumTbTotalPrice2.get_value();
            price = document.getElementById("RadNumTbTotalPrice2").value;
            amount10 = 10 * price / 100;
            //RadNumTbAmountOfDownPayment2.set_value(amount10);
            document.getElementById("RadNumTbAmountOfDownPayment2").value = amount10;
            request = price - amount10;
            //RadNumTbRequestSumm.set_value2(request);
            document.getElementById("RadNumTbRequestSumm").value = request;

        }
        demo.cvalueChanged2 = function (sender, args) {
            //price = RadNumTbTotalPrice2.get_value();
            price = document.getElementById("RadNumTbTotalPrice2").value;
            amount10 = 10 * price / 100;
            //amount = RadNumTbAmountOfDownPayment2.get_value();
            amount = document.getElementById("RadNumTbAmountOfDownPayment2").value;
            if (amount >= amount10) { request = price - amount; }
            else { request = ""; }
            //RadNumTbRequestSumm2.set_value(request);
            document.getElementById("RadNumTbRequestSumm").value = request;

        }


        /************************/


        //demo.txtMarketPrice = function (sender, args) {
        //    txtMarketPrice = parseFloat(document.getElementById("txtMarketPrice").value.replace(/,/, '.'));
        //    document.getElementById("txtMarketPrice").value = txtPrice.toFixed(2).replace('.', ',');
        //}

        demo.txtMarketPrice = function (sender, args) {
            txtMarketPrice = parseFloat(document.getElementById("txtMarketPrice").value.replace(/,/, '.'));
            document.getElementById("txtMarketPrice").value = txtMarketPrice.toFixed(2).replace('.', ',');
        }

        demo.txtCoefficient = function (sender, args) {
            txtCoefficient = parseFloat(document.getElementById("txtCoefficient").value.replace(/,/, '.'));
            document.getElementById("txtCoefficient").value = txtCoefficient.toFixed(2).replace('.', ',');
        }
        
        demo.RadNumTbAdditionalIncome = function (sender, args) {
            AdditionalIncome = parseFloat(document.getElementById("RadNumTbAdditionalIncome").value.replace(/,/, '.'));
            document.getElementById("RadNumTbAdditionalIncome").value = AdditionalIncome.toFixed(2).replace('.', ',');
        }

        demo.RadNumTbAmountOfDownPayment = function (sender, args) {
            RadNumTbAmountOfDownPayment = parseFloat(document.getElementById("RadNumTbAmountOfDownPayment").value.replace(/,/, '.'));
            document.getElementById("RadNumTbAmountOfDownPayment").value = RadNumTbAmountOfDownPayment.toFixed(2).replace('.', ',');
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

        RadNumTbRequestSumm = $find("<%=RadNumTbRequestSumm.ClientID%>");

        RadNumTbRequestSumm = $find("<%=RadNumTbRequestSumm.ClientID%>");

        RadNumTbMonthlyInstallment = $find("<%=RadNumTbMonthlyInstallment.ClientID%>");
        RadNumTbSumMonthSalary = $find("<%=RadNumTbSumMonthSalary.ClientID%>");
        RadNumTbAverageMonthSalary = $find("<%=RadNumTbAverageMonthSalary.ClientID%>");

        Image1 = $find("<%=Image1.ClientID%>");

        v = document.getElementById("<%= RadNumTbRequestSumm.ClientID %>").value;
     
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
         //document.getElementById("tbNote").innerHTML = txt;
         document.getElementById("hfNote").value = txt;
         
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
                  //document.getElementById('results').innerHTML = '<img id="imgCam" src="' + data_uri + '"/> <p>Чтобы сохранить фото, нужно сохранить заявку</p>';
                  document.getElementById('hfPhoto2').value = data_uri.replace(/^data\:image\/\w+\;base64\,/, '');
                  //document.getElementById('hfPhoto1').value = data_uri;
                  //document.getElementById('Image1').value = data_uri.replace(/^data\:image\/\w+\;base64\,/, '');
                  document.getElementById("Image1").src = data_uri;
                  document.getElementById("Image1").style.display = 'inline-block';
                  document.getElementById("Image1").style.width = '200px';
                  document.getElementById("Image1").style.height = '235px';

                  document.getElementById("pnlPhoto").style.display = 'none';
                  document.getElementById("btnPhoto").innerText = 'Очистить';
                  

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
