<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="СreditСonveyor.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    

    <div class="row">
        <div class="col-md-16">
            <section id="loginForm">
                <div class="form-horizontal">
                    <div class="div-center">

                    <div class="content">
                    <hr />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="">
                       <%-- <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Логин</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        </div>--%>
                         <asp:Label runat="server" AssociatedControlID="Email" CssClass="control-label">Логин</asp:Label>
                       
                          <asp:TextBox runat="server" ID="Email" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        
                    </div>
                    <div class="">
                        <%--<asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Пароль</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>--%>
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="control-label">Пароль</asp:Label>
                        
                        <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                       
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" Visible="False" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe" Visible="false">Запомнить?</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Войти" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>

                    </div>
               </div>

              <%--  <p>
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                </p>
                <p>
                     Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                    
                </p>--%>
            </section>
        </div>


    

        <%--<div class="col-md-4">
            <section id="socialLoginForm">
                <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
            </section>
        </div>--%>
    </div>

  <%--  <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
<div class="form-horizontal">

 <div class="div-center">


    <div class="content">


      <h3>Login</h3>
      <hr>
     
        <div class="form-group">
        
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="control-label">Логин</asp:Label>
                        <div class="">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="The email field is required." />
                        </div>
        </div>
        <div class="form-group">
        
             <asp:Label runat="server" AssociatedControlID="Password" CssClass="control-label">Пароль</asp:Label>
                        <div class="">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
        </div>
         <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" Visible="False" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe" Visible="false">Запомнить?</asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Вход" CssClass="btn btn-primary" />
                        </div>
                    </div>
     


        </div>

    
  </div>


</div>
                </section>
         </div>
    </div>--%>

    <style>
        .back {
    background: #e2e2e2;
    width: 100%;
    position: absolute;
    top: 0;
    bottom: 0;
}

        .div-center {
    width: 400px;
    height: 400px;
    background-color: #fff;
    /*position: absolute;*/
    left: 0;
    right: 0;
    top: 0;
    bottom: 0;
    margin: auto;
    max-width: 100%;
    max-height: 100%;
    overflow: auto;
    padding: 1em 2em;
    border-bottom: 2px solid #ccc;
    display: table;
}


        div.content {
    display: table-cell;
    vertical-align: middle;
}
    </style>
</asp:Content>
