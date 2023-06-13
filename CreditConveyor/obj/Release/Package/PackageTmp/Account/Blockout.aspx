<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Blockout.aspx.cs" Inherits="СreditСonveyor.Account.Blockout" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup>
        <%--<h1>Пользователь заблокирован.</h1>--%>
        <h1>Пользователь временно заблокирован по причине более чем 5 неудачных попыток входа. Попробуйте войти через 5 минут.</h1>
        <h2 class="text-danger"></h2>
    </hgroup>
</asp:Content>
