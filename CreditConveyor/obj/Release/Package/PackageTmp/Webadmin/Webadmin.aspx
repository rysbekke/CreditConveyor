<%@ Page Title="Партнеры" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Webadmin.aspx.cs" Inherits="СreditСonveyor.Webadmin.Webadmin" Async="true" AsyncTimeout="60" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

     <link href="/css/bootstrap.css" rel="stylesheet" />
<link rel="stylesheet" href="/css/chosen.css">
<script src="/js/jquery.js"></script>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>--%>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>--%>
<script src="/js/jquery-ui.js"></script>

<script src="/js/chosen.jquery.min.js"></script>
<script src="/js/chosen.init.js"></script>
<script src="/js/datepicker-ru.js"></script>

    <asp:HiddenField ID="hfUserID" runat="server" />
 

    <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
<table>
    <tr>
    <td>Роли</td>
        <td>Страницы</td>
        <td></td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlRole" runat="server" DataTextField="RoleName" DataValueField="RoleID" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList></td>
        <%--<td><uc1:urlcontrol ID="urlpagess" runat="server" ShowDatabase="False" ShowFiles="False"
                                ShowImages="False" ShowLog="False" ShowNewWindow="False" ShowTabs="True" ShowTrack="False"
                                ShowUpLoad="False" ShowUrls="False" ShowUsers="False" IncludeActiveTab="True" />
        </td>--%>
        <td><asp:Button ID="btnAppendRedirects" runat="server" Text="Добавить" OnClick="btnAppendRedirects_Click" /></td>
    </tr>
</table>

<asp:GridView ID="gvRedirect" runat="server" OnRowCommand="gvRedirect_RowCommand">
    <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbSelItem" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Sel">Выбрать</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbDelItem" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Del">Удалить</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>
</asp:GridView>   

<br /><br /><br /><br /><br /><br />
    <asp:HiddenField ID="hfselectUserRolesID" runat="server" />
    <br />
<table>
    <tr>
        <td>Роли</td>
        <td>Пользователи</td>
        <td>Наименование точки</td>
        <td>Адрес</td>
        <td>№ документа</td>
        <td>Дата документа</td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlRole2" runat="server" DataTextField="RoleName" DataValueField="RoleID" AutoPostBack="True" OnSelectedIndexChanged="ddlRole2_SelectedIndexChanged"></asp:DropDownList></td>
        <td><asp:DropDownList ID="ddlUsers" runat="server" DataTextField="UserName" DataValueField="UserID"></asp:DropDownList></td>
        <td><asp:TextBox ID="tbNameAgencyPoint" runat="server"></asp:TextBox></td>
        <td><asp:TextBox ID="tbAddressAgencyPoint" runat="server"></asp:TextBox></td>
        <td><asp:TextBox ID="tbAttorneyDocName" runat="server"></asp:TextBox></td>
        <td><asp:TextBox ID="tbAttorneyDocDate" runat="server" ClientIDMode="Static"></asp:TextBox></td>
        <td><asp:Button ID="btnAppendUsersRoles" runat="server" OnClick="btnAppendUsersRoles_Click" Text="Добавить" /></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td>
            <asp:TextBox ID="tbNameAgencyPoint2" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="tbAddressAgencyPoint2" runat="server"></asp:TextBox>
        </td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table><br />
<asp:GridView ID="gvUsersRoles" runat="server" OnRowCommand="gvUsersRoles_RowCommand" OnPageIndexChanging="gvUsersRoles_PageIndexChanging">
    <Columns>
        <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbSelItem" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Sel">Выбрать</asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbDelItem" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Del">Удалить</asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

    <br /><br /><br /><br /><br /><br /><br />
<table>
    <tr>
        <td>Филиалы</td>
        <td>Крединые эксперты</td>
        <td>Примечание</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><asp:DropDownList ID="ddlBranches" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList></td>
        <td><asp:DropDownList ID="ddlCreditOfficers" runat="server" DataTextField="UserName" DataValueField="UserID"></asp:DropDownList></td>
        <td><asp:TextBox ID="tbNote" runat="server"></asp:TextBox></td>
        <td><asp:Button ID="btnAppend" runat="server" Text="Добавить" OnClick="btnAppend_Click" /></td>
    </tr>
</table><br />
<asp:GridView ID="gvRequestsRedirect" runat="server" OnRowCommand="gvRequestsRedirect_RowCommand">
    <Columns>
        <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbDelItem" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Del">Удалить</asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        <br /><br /><br /><br /><br /><br /><br />
        <table>
        <tr>
            <td><asp:DropDownList ID="ddlRequests" runat="server" DataTextField="RequestID" ></asp:DropDownList></td>
            <td><asp:DropDownList ID="ddlStatus" runat="server" >
                <asp:ListItem>Отменено</asp:ListItem>
                <asp:ListItem>Отказано</asp:ListItem>
                <asp:ListItem>Новая заявка</asp:ListItem>
                <asp:ListItem>Исправлено</asp:ListItem>
                <asp:ListItem>Не подтверждено</asp:ListItem>
                <asp:ListItem>Подтверждено</asp:ListItem>
                <asp:ListItem>Утверждено</asp:ListItem>
                <asp:ListItem>Выдано</asp:ListItem>
                <asp:ListItem>Принято</asp:ListItem>
                <asp:ListItem>К подписи</asp:ListItem>
                <asp:ListItem>На выдаче</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:TextBox ID="tbNote2" runat="server"></asp:TextBox></td>
            <td><asp:Button ID="btnReqStatus" runat="server" Text="Сохранить" OnClick="btnReqStatus_Click" /></td>
        </tr>
    </table>

    <br /><br /><br /><br /><br /><br /><br />
    <table>
        <tr>
            <td>UserName:</td>
            <td><asp:TextBox ID="tbUserName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>FullName:</td>
            <td><asp:TextBox ID="tbFullName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><asp:TextBox ID="tbPassw" runat="server"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td>EMail:</td>
            <td><asp:TextBox ID="tbEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Офис:</td>
            <td><asp:DropDownList ID="ddlOffice" runat="server" DataTextField="Name" DataValueField="ID"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Группа:</td>
            <td><asp:DropDownList ID="ddlGroups" runat="server" DataTextField="GroupName" DataValueField="GroupID"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Пользователь блокирован?</td>
            <td>
                <asp:CheckBox ID="ckbxIsBlocked" runat="server" OnCheckedChanged="ckbxIsBlocked_CheckedChanged" AutoPostBack="True" />
            </td>
        </tr>
        <tr>
            <td>&nbsp</td>
            <td></td>
        </tr>
        <tr>
            <td><asp:Button ID="btnSaveUsers" runat="server" Text="Сохранить" OnClick="btnSaveUsers_Click" /></td>
            <td><asp:Button ID="btnCancel" runat="server" Text="Отменить" OnClick="btnCancel_Click" /></td>
        </tr>
        
    </table>
    <asp:GridView ID="gvUsers" runat="server" OnPageIndexChanging="gvUsers_PageIndexChanging" OnRowCommand="gvUsers_RowCommand">
            <Columns>
        <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbSelItem" runat="server" CommandArgument='<%# Eval("UserID") %>' CommandName="Sel">Выбрать</asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lbDelItem" runat="server" CommandArgument='<%# Eval("UserID") %>' CommandName="Del">Удалить</asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
</asp:Panel>

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

        $("#tbAttorneyDocDate").datepicker({
            changeMonth: true,
            numberOfMonths: 1,
        });
    });
</script>


 
</asp:Content>