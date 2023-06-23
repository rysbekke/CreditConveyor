using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using СreditСonveyor.Models;
using System.Data.Linq;
using System.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Helpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Zamat;

namespace СreditСonveyor.Account
{
    public partial class Login : Page
    {
        public string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString(); //строка подключения к БД Онлайн База Зеркало
        public string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString(); //строка подключения к БД Онлайн База
        public string connectionStringZ = ConfigurationManager.ConnectionStrings["connectionStringZ"].ToString(); //строка подключения к БД Скоринг

        protected void Page_Load(object sender, EventArgs e) //Загрузка страницы
        {
            //RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
        }

        //protected void LogIn(object sender, EventArgs e)
        //{
        //    if (IsValid)
        //    {
        //        // Validate the user password
        //        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //        var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

        //        // This doen't count login failures towards account lockout
        //        // To enable password failures to trigger lockout, change to shouldLockout: true
        //        var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

        //        switch (result)
        //        {
        //            case SignInStatus.Success:
        //                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        //                break;
        //            case SignInStatus.LockedOut:
        //                Response.Redirect("/Account/Lockout");
        //                break;
        //            case SignInStatus.RequiresVerification:
        //                Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
        //                                                Request.QueryString["ReturnUrl"],
        //                                                RememberMe.Checked),
        //                                  true);
        //                break;
        //            case SignInStatus.Failure:
        //            default:
        //                FailureText.Text = "Invalid login attempt";
        //                ErrorMessage.Visible = true;
        //                break;
        //        }
        //    }
        //}

        protected void LogInFunction()
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbZ = new dbdataDataContext(connectionStringZ);
            var result = SignInStatus.LockedOut;
            if (check_login_password_with_СreditСonveyor(Email.Text, Password.Text, connectionStringZ) == 1) //успех
            {
                result = SignInStatus.Success; //Успешная авторизация, присваиваем к переменному result = Success
            }
            //else
            if (check_login_password_with_СreditСonveyor(Email.Text, Password.Text, connectionStringZ) == 0) //неверный логин или пароль
            {
                result = SignInStatus.LockedOut; //Неуспешная авторизация, присваиваем к переменному result = Success


            }
            if (check_login_password_with_СreditСonveyor(Email.Text, Password.Text, connectionStringZ) == 2) //пользователь блокирован
            {
                result = SignInStatus.LockedOut;
                Session["UserName"] = "";
                Response.Redirect("/Account/Blockout");
                //break;
            }


            switch (result)
            {
                case SignInStatus.Success: //успешная авторизация
                    /***********/
                    logAuthUnBlock(); //Разблакировка логов пользователя
                    /*************/
                    unBlockUser(); //Разблакировка пользователя
                    /**************/
                    LogAuthorization logAuth3 = new LogAuthorization() //Логи авторизации
                    {
                        UserName = Email.Text.Trim(),
                        DateOfFailedAuth = DateTime.Now,
                        IsBlocked = 2, //обычный вход
                        Note = "successful"
                    };
                    dbZ.LogAuthorizations.InsertOnSubmit(logAuth3);
                    dbZ.LogAuthorizations.Context.SubmitChanges();

                    /***********/
                    //btnLogin(Email.Text, Password.Text);
                    var usr = dbZ.Users2s.Where(r => r.UserName == Email.Text).FirstOrDefault();
                    int usrID = usr.UserID;
                    var rle = dbZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;

                    //Создание сессий  
                    Session["UserName"] = Email.Text; 
                    Session["Password"] = Password.Text;
                    Session["RoleID"] = rle.ToString();
                    Session["UserID"] = usrID.ToString();
                    Session["FIO"] = usr.Fullname;
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    break;
                case SignInStatus.LockedOut: //Неуспешная авторизация

                    /***********/
                    LogAuthorization logAuth = new LogAuthorization() //Логи авторизации
                    {
                        UserName = Email.Text.Trim(),
                        DateOfFailedAuth = DateTime.Now,
                        IsBlocked = 1, //неудачный вход
                        Note = "unsuccessful"
                    };
                    dbZ.LogAuthorizations.InsertOnSubmit(logAuth);
                    dbZ.LogAuthorizations.Context.SubmitChanges();
                    /*************/
                    //в этом блоке программа считает количество неудачных попыток входа, 
                    //если кол-во неуд.вход. > 4 тогда пользователь блокируется

                    var dateOfFirstFailed = dbZ.LogAuthorizations.Where(x => x.UserName == Email.Text.Trim() && x.IsBlocked == 1).ToList().OrderBy(x => x.DateOfFailedAuth).ToList();
                    //int secDiff = System.Data.Linq.SqlClient.SqlMethods.DateDiffSecond((DateTime)dateOfFirstFailed.FirstOrDefault().DateOfFailedAuth, DateTime.Now);
                    if (dateOfFirstFailed.Count > 4)
                    {
                        blockUser(); //Блокировка пользователя
                        result = SignInStatus.LockedOut;
                        Session["UserName"] = "";
                        Response.Redirect("/Account/Blockout"); //переход к странице Blockout
                    }
                    /***********/

                    Session["UserName"] = "";
                    Response.Redirect("/Account/Lockout"); //переход к странице Lockout
                    break;
                case SignInStatus.RequiresVerification:
                    Session["UserName"] = "";
                    Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                    Request.QueryString["ReturnUrl"],
                                                    RememberMe.Checked),
                                      true);
                    break;
                case SignInStatus.Failure:
                default:
                    Session["UserName"] = "";
                    FailureText.Text = "Invalid login attempt";
                    ErrorMessage.Visible = true;
                    break;
            }
        }



        protected void LogIn(object sender, EventArgs e) //Авторизация
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbZ = new dbdataDataContext(connectionStringZ);
            //IdentityResult result;

            //if (check_password_with_ldap(UserName.Text, Password.Text)) //домен
            //
            if (IsValid)
            {
                if (check_login_with_СreditСonveyor(Email.Text, connectionStringZ) == 1)
                {
                    update_password_from_OB(Email.Text);
                    LogInFunction();
                }
                else
                {
                    int roleID = 0;
                    int role_ID = check_login_password_with_OB(Email.Text, Password.Text);
                    //if (check_login_password_with_OB(Email.Text, Password.Text) == 1)
                    if ((role_ID == 158) || (role_ID == 159)) roleID = 2;
                    if (role_ID > 0)
                    {

                        int UserIDOB = Convert.ToInt32(Session["UserIDOB"]);
                        var usr = (from u in dbR.Users where (u.UserID == UserIDOB) select u).FirstOrDefault();
                        //Создание логина
                        Users2 newItem = new Users2
                        {
                            //UserID = Convert.ToInt32(ddlUsers.SelectedItem.Value),
                            UserName = Email.Text,
                            Fullname = "",
                            Password = Crypto.SHA1(Password.Text).ToLower(),
                            EMail = Email.Text,
                            OfficeID = usr.OfficeID,
                            CreateDate = DateTime.Now,
                            PasswordExpiryDate = DateTime.MaxValue,
                            GroupID = 1,
                            OrgID = 1,
                            UserIDOB = UserIDOB
                        };
                        dbZ.Users2s.InsertOnSubmit(newItem);
                        dbZ.Users2s.Context.SubmitChanges();
                        int userID = newItem.UserID;
                        //Создание роли
                        RequestsUsersRole newItemRole = new RequestsUsersRole
                        {
                            UserID = userID,
                            UserName = Email.Text,
                            RoleID = roleID,
                            NameAgencyPoint = "КапиталБанк",
                            NameAgencyPoint2 = "КапиталБанк",
                            AddressAgencyPoint = "КапиталБанк",
                            AddressAgencyPoint2 = "КапиталБанк",
                            AttorneyDocName = "0",
                            //AttorneyDocDate = Convert.ToDateTime(tbAttorneyDocDate.Text.Substring(3, 2) + "." + tbAttorneyDocDate.Text.Substring(0, 2) + "." + tbAttorneyDocDate.Text.Substring(6, 4)),
                            AttorneyDocDate = DateTime.Now,
                        };
                        dbZ.RequestsUsersRoles.InsertOnSubmit(newItemRole);
                        dbZ.RequestsUsersRoles.Context.SubmitChanges();
                        //Авторизация
                        LogInFunction();
                    }


                }
            }
        }

        //protected void LogIn(object sender, EventArgs e) //Авторизация
        //{
        //    dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        //    dbdataDataContext dbZ = new dbdataDataContext(connectionStringZ);
        //    //IdentityResult result;

        //    //if (check_password_with_ldap(UserName.Text, Password.Text)) //домен
        //    //
        //    if (IsValid)
        //    {
        //        if (check_login_with_СreditСonveyor(Email.Text, connectionStringZ) == 1)
        //        {
        //            LogInFunction();
        //        }
        //        else
        //        {
        //            if (check_login_password_with_OB(Email.Text, Password.Text) == 1)
        //            {

        //                int UserIDOB = Convert.ToInt32(Session["UserIDOB"]);
        //                var usr = (from u in dbR.Users where (u.UserID == UserIDOB)  select u).FirstOrDefault();
        //                //Создание логина
        //                Users2 newItem = new Users2
        //                {
        //                    //UserID = Convert.ToInt32(ddlUsers.SelectedItem.Value),
        //                    UserName = Email.Text,
        //                    Fullname = "",
        //                    Password = Crypto.SHA1(Password.Text).ToLower(),
        //                    EMail = Email.Text,
        //                    OfficeID = usr.OfficeID,
        //                    CreateDate = DateTime.Now,
        //                    PasswordExpiryDate = DateTime.MaxValue,
        //                    GroupID = 1,
        //                    OrgID = 1,
        //                    UserIDOB = UserIDOB
        //                };
        //                dbZ.Users2s.InsertOnSubmit(newItem);
        //                dbZ.Users2s.Context.SubmitChanges();
        //                int userID = newItem.UserID;
        //                //Создание роли
        //                RequestsUsersRole newItemRole = new RequestsUsersRole
        //                {
        //                    UserID = userID,
        //                    UserName = Email.Text,
        //                    RoleID = 2,
        //                    NameAgencyPoint = "КапиталБанк",
        //                    NameAgencyPoint2 = "КапиталБанк",
        //                    AddressAgencyPoint = "КапиталБанк",
        //                    AddressAgencyPoint2 = "КапиталБанк",
        //                    AttorneyDocName = "0",
        //                    //AttorneyDocDate = Convert.ToDateTime(tbAttorneyDocDate.Text.Substring(3, 2) + "." + tbAttorneyDocDate.Text.Substring(0, 2) + "." + tbAttorneyDocDate.Text.Substring(6, 4)),
        //                    AttorneyDocDate = DateTime.Now,
        //                };
        //                dbZ.RequestsUsersRoles.InsertOnSubmit(newItemRole);
        //                dbZ.RequestsUsersRoles.Context.SubmitChanges();
        //                //Авторизация
        //                LogInFunction();
        //            }


        //        }
        //    }
        //}

        //Разблокировка логов пользователя
        public void logAuthUnBlock() 
        {
            string sqlExpression = "update [CreditConveyorWF].[dbo].[LogAuthorization] set IsBlocked = 0 where IsBlocked = 1 and UserName = '" + Email.Text.Trim() + "'";

            using (SqlConnection connection = new SqlConnection(connectionStringZ))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        //Кнопка Login - вход в систему
        public void btnLogin(string username, string password)
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR); //подключение к базе ОБ
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ); //подключение к базе КапиталБанк
            var psw = password;
            string sha1 = SHA1HashStringForUTF8String(password);
            //поиск пользователя
            var usr = (from u in dbRWZ.Users2s where ((u.UserName == username) && (u.Password == sha1)) select u).ToList();
            if (usr.Count > 0)
            {
                //Создание сессии
                Session["Check"] = "true";
                Session["UserName"] = usr.SingleOrDefault().UserName;
                Session["UserID"] = usr.SingleOrDefault().UserID;
                Session["FIO"] = usr.SingleOrDefault().Fullname;


                //Переход на страницу в зависимости от роли
                int usrID = usr.FirstOrDefault().UserID;
                var role = (from u in dbRWZ.RequestsUsersRoles where (u.UserID == usrID) select u).ToList().FirstOrDefault(); //роль ползователя
                if (role != null)
                {
                    Session["RoleID"] = role.RoleID.ToString();


                    if (usrID != -1)
                    {
                        var lst = (from v in dbRWZ.TblRedirects where (v.RoleID == role.RoleID) select v).FirstOrDefault(); //поиск роли

                        //if ((lst != null) && (lst.RoleID != -3))
                        if ((lst != null) && (lst.RoleID > 0))
                        {
                            //Response.Redirect("/" + lst.RedirectName);
                            string lnk = "https://credit.doscredobank.kg/" + lst.RedirectName;
                            Response.Redirect(lnk); //Переход на страницу 
                        }
                        else
                        {
                            Session["Check"] = false;
                            Session["UserName"] = "";
                            Session["UserID"] = "";
                            Response.Redirect("/UnLock"); //переход на страницу блокировки
                        }
                    }
                }
                else
                {
                    // DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "У пользователя не хватает доступов", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                }

            }
            else
            {
                Session["Check"] = false;
                Session["UserName"] = "";
                Session["UserID"] = "";
                Response.Redirect("/UnLock"); //переход на страницу блокировки
            }


        }


        //Разблокировка пользователя
        public void unBlockUser() 
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var lst2 = dbRWZ.Users2s.Where(r => r.UserName.Trim() == Email.Text.Trim()).FirstOrDefault();
             lst2.isBlocked = false;
            dbRWZ.Users2s.Context.SubmitChanges();
        }

        //Блокировка пользователя
        public void blockUser()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var lst2 = dbRWZ.Users2s.Where(r => r.UserName.Trim() == Email.Text.Trim()).FirstOrDefault();
            lst2.isBlocked = true;
            dbRWZ.Users2s.Context.SubmitChanges();
        }
        //Проверка доступов
        public byte check_login_password_with_СreditСonveyor(string username, string password, string connectstr)
        {
            var psw = password;
            //var sha1 = System.Security.Cryptography.SHA1.Create(psw);
            //string sha1 = tosha(password);
            string sha1 = SHA1HashStringForUTF8String(password);
            dbdataDataContext db = new dbdataDataContext(connectstr);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var usr = (from u in db.Users2s where ((u.UserName == username) && (u.Password == sha1)) select u).ToList();
            var usr2 = (from u in db.Users2s where ((u.UserName == username)) select u).ToList();
            if (usr2.Count > 0)
            if (usr2.FirstOrDefault().isBlocked == true) //если пользователь блокирован
            {
                var lst = db.LogAuthorizations.Where(x => x.UserName == username && x.Note == "unsuccessful").ToList().LastOrDefault();
                var now = DateTime.Now;

                TimeSpan elapsed = (TimeSpan) (DateTime.Now - lst.DateOfFailedAuth);
                
                //txtDays.Text = elapsed.TotalDays.ToString();
                //txtHours.Text = elapsed.TotalHours.ToString();
                //txtMinutes.Text = elapsed.TotalMinutes.ToString();

                //var z = (now - lst.DateOfFailedAuth);
                if (elapsed.TotalMinutes > 1) //Таймер времени
                {
                    //var lst = dbRWZ.Users2s.Where(r => r.UserName.Trim() == tbUserName.Text.Trim()).FirstOrDefault();
                    //usr2.FirstOrDefault().isBlocked = false;
                    //dbRWZ.Users2s.Context.SubmitChanges();

                    unBlockUser(); //разблокировка пользователя
                    logAuthUnBlock(); //раблокировка логов

                    LogAuthorization logAuth = new LogAuthorization()
                    {
                        UserName = lst.UserName,
                        DateOfFailedAuth = DateTime.Now,
                        IsBlocked = 0,
                        Note = "" // Session["UserName"] as string
                    };
                    dbRWZ.LogAuthorizations.InsertOnSubmit(logAuth);
                    dbRWZ.LogAuthorizations.Context.SubmitChanges();
                }
                else
                return 2; //пользователь блокирован
            }
            if (usr.Count > 0)
            {
                 return 1; //пользователь найден
            }
            else return 0; //пользователь не найден

        }

        public byte check_login_with_СreditСonveyor(string username, string connectstr)
        {
            //var sha1 = System.Security.Cryptography.SHA1.Create(psw);
            //string sha1 = tosha(password);
            dbdataDataContext db = new dbdataDataContext(connectstr);
            //dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var usr = (from u in db.Users2s where ((u.UserName == username)) select u).ToList();
          
            if (usr.Count > 0)
            {
                return 1; //пользователь найден
            }
            else return 0; //пользователь не найден

        }



        public void update_password_from_OB(string username)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            var usrOB = (from u in dbR.Users where ((u.UserName == username)) select u).ToList();

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var usr = (from u in dbRWZ.Users2s where ((u.UserName == username)) select u).ToList();

            if (usrOB.Count > 0)
            {
                string sqlExpression = "update [CreditConveyorWF].[onlineCredits].[Users2] set Password = '" + usrOB.FirstOrDefault().Password + "' where UserName = '" + usr.FirstOrDefault().UserName + "'";

                using (SqlConnection connection = new SqlConnection(connectionStringZ))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        public int check_login_password_with_OB(string username, string password)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);

            var psw = password;
            //var sha1 = System.Security.Cryptography.SHA1.Create(psw);
            //string sha1 = tosha(password);
            string sha1 = SHA1HashStringForUTF8String(password);
            //dbdataDataContext db = new dbdataDataContext(connectstr);
            //dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var usr = (from u in dbR.Users where ((u.UserName == username) && (u.Password == sha1)) select u).ToList();
            //ar usr2 = (from u in dbR.Users where ((u.UserName == username)) select u).ToList();
            //if (usr2.Count > 0)
            //if (usr2.FirstOrDefault().isBlocked == true) //если пользователь блокирован
            //{
            //    var lst = db.LogAuthorizations.Where(x => x.UserName == username && x.Note == "unsuccessful").ToList().LastOrDefault();
            //    var now = DateTime.Now;

            //    TimeSpan elapsed = (TimeSpan)(DateTime.Now - lst.DateOfFailedAuth);

            //    //txtDays.Text = elapsed.TotalDays.ToString();
            //    //txtHours.Text = elapsed.TotalHours.ToString();
            //    //txtMinutes.Text = elapsed.TotalMinutes.ToString();

            //    //var z = (now - lst.DateOfFailedAuth);
            //    if (elapsed.TotalMinutes > 1) //Таймер времени
            //    {
            //        //var lst = dbRWZ.Users2s.Where(r => r.UserName.Trim() == tbUserName.Text.Trim()).FirstOrDefault();
            //        //usr2.FirstOrDefault().isBlocked = false;
            //        //dbRWZ.Users2s.Context.SubmitChanges();

            //        unBlockUser(); //разблокировка пользователя
            //        logAuthUnBlock(); //раблокировка логов

            //        LogAuthorization logAuth = new LogAuthorization()
            //        {
            //            UserName = lst.UserName,
            //            DateOfFailedAuth = DateTime.Now,
            //            IsBlocked = 0,
            //            Note = "" // Session["UserName"] as string
            //        };
            //        dbRWZ.LogAuthorizations.InsertOnSubmit(logAuth);
            //        dbRWZ.LogAuthorizations.Context.SubmitChanges();
            //    }
            //    else
            //        return 2; //пользователь блокирован
            //}
            if (usr.Count > 0)
            {
                int roleID = 0;
                var rolesID = dbR.UsersRoles.Where((u) => u.UserID == usr.FirstOrDefault().UserID).ToList();
                bool f = false;
                foreach (var role in rolesID)
                {
                    if ((role.RoleID == 158) || (role.RoleID == 159)) { f = true; roleID = role.RoleID; }
                }
                if (f)
                {
                    Session["UserIDOB"] = usr.SingleOrDefault().UserID;
                    return roleID; //пользователь найден
                }
                else return 0;
            }
            else return 0; //пользователь не найден

        }

        public string tosha(string text)
        {
            string hash;
            try
            {
                Byte[] stream = null;
                using (System.Security.Cryptography.SHA1CryptoServiceProvider shaProvider = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                {
                    stream = shaProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
                    if (stream == null)
                    {
                        hash = "Error";
                    }
                    else
                    {
                        hash = System.BitConverter.ToString(stream);
                    }
                }
            }
            catch (Exception error)
            {
                hash = string.Format("Error SHA-1: {0}", error);
            }
            return hash;
        }

        //Кодировка SHA1 UTF8
        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);
            return HexStringFromBytes(hashBytes);
        }
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

    }
}