using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using СreditСonveyor.Data;
using СreditСonveyor.Data.Card;

namespace СreditСonveyor
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            //if (!IsPostBack)
            //{
            //    check_security();
            //}
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }


        public void check_security()
        {
            //dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //string userName = Session["UserName"] as string;
            //if (Session["UserName"] == null)
            //{
            //    //var usr = (from u in dbR.Users where (u.UserName == userName) select u).FirstOrDefault();
            //    //hfUserID.Value = usr.UserID.ToString();
            //    Response.Redirect("/Account/Login");
            //}
            //else
            //{


            //}

            string UserName = (Session["UserName"] != null) ? Session["UserName"].ToString() : "";
            string[] file = Request.CurrentExecutionFilePath.Split('/');
            string pageName1 = file[file.Length - 2];
            string pageName2 = file[file.Length - 1];
            GeneralController gtx = new GeneralController();
            if (gtx.check_security_page(UserName, pageName1, pageName2) == false) Response.Redirect("/Account/Login");

        }


        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string UserName = Session["UserName"] as string;
            if ((UserName == null) || (UserName == "")) 
            { 
                divLogin.Visible = true; 
                divLoginOk.Visible = false; 
            }
            else 
            { 
                divLogin.Visible = false; 
                divLoginOk.Visible = true; 
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void Menu1_Init(object sender, EventArgs e)
        {
            initMenufromСreditСonveyor();
        }



        public void initMenufromOB()
        {
            //string UserName = Context.User.Identity.GetUserName();
            string UserName = Session["UserName"] as string;
            if (UserName != null)
            {
                int userRoleID = ItemController.getRoleIDfromOB(UserName);
                MenuItemCollection menuItems = Menu1.Items;
                MenuItem menuItem = new MenuItem();
                MenuItem menuItem2 = new MenuItem();
                MenuItem menuItem3 = new MenuItem();
                if (userRoleID == 1262) //Роль кредитного специалиста фил -1262
                {
                    Menu1.Items.Clear();

                    menuItem.Text = "Проссрочки";
                    menuItem.NavigateUrl = "Overdues.aspx";
                    menuItems.Add(menuItem);

                    menuItem2.Text = "Задачи";
                    menuItem2.NavigateUrl = "RequestsAll.aspx";
                    menuItems.Add(menuItem2);

                }


                string path = HttpContext.Current.Request.Url.AbsolutePath;
                if (path == "/Overdues")
                {
                    menuItem.Selected = true;
                };

                if (path == "/RequestsAll")
                {
                    menuItem2.Selected = true;
                };
            }
        }

        public void initMenufromСreditСonveyor()
        {
            //string UserName = Context.User.Identity.GetUserName();
            string UserName = Session["UserName"] as string;
            if (UserName != null)
            {
                int userRoleID = ItemController.getRoleIDfromСreditСonveyor(UserName);
                MenuItemCollection menuItems = Menu1.Items;
                MenuItem menuItem = new MenuItem();
                MenuItem menuItem2 = new MenuItem();
                MenuItem menuItem3 = new MenuItem();
                MenuItem menuItem4 = new MenuItem();
                MenuItem menuItem5 = new MenuItem();
                MenuItem menuItem6 = new MenuItem();
                if (userRoleID == 8) //агент билайн
                {
                    Menu1.Items.Clear();

                    menuItem.Text = "Кредиты";
                    menuItem.NavigateUrl = "/Beeline/AgentView.aspx";
                    menuItems.Add(menuItem);

                    menuItem2.Text = "Карты";
                    menuItem2.NavigateUrl = "/Card/Card.aspx";
                    menuItems.Add(menuItem2);

                    //menuItem3.Text = "Карты для ИП/ЧП";
                    //menuItem3.NavigateUrl = "/Card/CardL.aspx";
                    //menuItems.Add(menuItem3);
                }

                if (userRoleID == 9) //админ билайн
                {
                    Menu1.Items.Clear();

                    menuItem.Text = "Кредиты";
                    menuItem.NavigateUrl = "/Beeline/AgentView.aspx";
                    menuItems.Add(menuItem);

                    //menuItem2.Text = "Карты";
                    //menuItem2.NavigateUrl = "/Card/Card.aspx";
                    //menuItems.Add(menuItem2);

                    //menuItem3.Text = "Карты для ИП/ЧП";
                    //menuItem3.NavigateUrl = "/Card/CardL.aspx";
                    //menuItems.Add(menuItem3);
                }


                if (userRoleID == 1) //агент нур
                {
                    Menu1.Items.Clear();

                    menuItem.Text = "Кредиты";
                    menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
                    menuItems.Add(menuItem);

                    //menuItem2.Text = "Карты";
                    //menuItem2.NavigateUrl = "/Card/Card.aspx";
                    //menuItems.Add(menuItem2);

                    //menuItem3.Text = "Карты для ИП/ЧП";
                    //menuItem3.NavigateUrl = "/Card/CardL.aspx";
                    //menuItems.Add(menuItem3);
                }

                if (userRoleID == 4) //админ нур
                {
                    Menu1.Items.Clear();

                    menuItem.Text = "Кредиты";
                    menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
                    menuItems.Add(menuItem);

                    menuItem2.Text = "Карты";
                    menuItem2.NavigateUrl = "/Card/Card.aspx";
                    menuItems.Add(menuItem2);

                    //menuItem3.Text = "Карты для ИП/ЧП";
                    //menuItem3.NavigateUrl = "/Card/CardL.aspx";
                    //menuItems.Add(menuItem3);
                }

                if (userRoleID == 13) //агент техно
                {
                    Menu1.Items.Clear();

                    menuItem.Text = "Партнеры";
                    menuItem.NavigateUrl = "/Partners/Loans.aspx";
                    menuItems.Add(menuItem);

                   
                    //menuItem2.Text = "Карты";
                    //menuItem2.NavigateUrl = "/Card/Card.aspx";
                    //menuItems.Add(menuItem2);

                    //menuItem3.Text = "Карты для ИП/ЧП";
                    //menuItem3.NavigateUrl = "/Card/CardL.aspx";
                    //menuItems.Add(menuItem3);
                }


                if (userRoleID == 2) //эксперты
                {
                    Menu1.Items.Clear();

                    //Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
                    //Console.WriteLine("Width: {0}, Height: {1}", resolution.Width, resolution.Height);
                    //if (resolution.Width > 1200)
                    {

                        //menuItem.Text = "Нуртелеком";
                        //menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
                        //menuItems.Add(menuItem);

                        //menuItem2.Text = "Билайн";
                        //menuItem2.NavigateUrl = "/Beeline/AgentView.aspx";
                        //menuItems.Add(menuItem2);

                        menuItem3.Text = "Партнеры";
                        menuItem3.NavigateUrl = "/Partners/Loans.aspx";
                        menuItems.Add(menuItem3);

                        menuItem5.Text = "МК кредиты";
                        menuItem5.NavigateUrl = "/Microcredit/Loans.aspx";
                        menuItems.Add(menuItem5);
                    }
                    //else
                    //{
                    //    menuItem6.Text = "МК кредиты";
                    //    menuItem6.NavigateUrl = "/Microcredit/Loans.aspx";
                    //    menuItems.Add(menuItem6);
                        
                    //}

                   
                }


                if (userRoleID == 5) //эксперты
                {
                    Menu1.Items.Clear();


                    //menuItem.Text = "Нуртелеком";
                    //menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
                    //menuItems.Add(menuItem);

                    //menuItem2.Text = "Билайн";
                    //menuItem2.NavigateUrl = "/Beeline/AgentView.aspx";
                    //menuItems.Add(menuItem2);

                    menuItem3.Text = "Партнеры";
                    menuItem3.NavigateUrl = "/Partners/Loans.aspx";
                    menuItems.Add(menuItem3);

                    menuItem5.Text = "МК кредиты";
                    menuItem5.NavigateUrl = "/Microcredit/Loans.aspx";
                    menuItems.Add(menuItem5);

                    //if (userRoleID == 5)
                    //{
                    //    menuItem4.Text = "Черновой скоринг";
                    //    menuItem4.NavigateUrl = "/BeelineS/AgentView.aspx";
                    //    menuItems.Add(menuItem4);
                    //}
                }


                if (userRoleID == 24) //эксперты бизнес
                {
                    Menu1.Items.Clear();
                    menuItem3.Text = "Партнеры";
                    menuItem3.NavigateUrl = "/Partners/Partners.aspx";
                    menuItems.Add(menuItem3);
                }

                if (userRoleID == 25) //Webadmin
                {
                    Menu1.Items.Clear();
                    menuItem5.Text = "Webadmin";
                    menuItem5.NavigateUrl = "/Webadmin/Webadmin.aspx";
                    menuItems.Add(menuItem5);
                }

                if (userRoleID == 19) //опер ГБ кард
                {
                    Menu1.Items.Clear();

                    menuItem.Text = "Карты";
                    menuItem.NavigateUrl = "/Card/Card.aspx";
                    menuItems.Add(menuItem);

                    menuItem2.Text = "Кредиты";
                    menuItem2.NavigateUrl = "/Microcredit/Loans.aspx";
                    menuItems.Add(menuItem2);

                    //menuItem2.Text = "Карты для ИП/ЧП";
                    //menuItem2.NavigateUrl = "/Card/CardL.aspx";
                    //menuItems.Add(menuItem2);

                }
                if (userRoleID == 20) //опер ГБ кард
                {
                    Menu1.Items.Clear();

                    menuItem.Text = "Карты";
                    menuItem.NavigateUrl = "/Card/Card.aspx";
                    menuItems.Add(menuItem);

                    //menuItem2.Text = "Карты для ИП/ЧП";
                    //menuItem2.NavigateUrl = "/Card/CardL.aspx";
                    //menuItems.Add(menuItem2);

                }

                if (UserName == "rtentiev") 
                {
                    menuItem6.Text = "Webadmin";
                    menuItem6.NavigateUrl = "/Webadmin/Webadmin.aspx";
                    menuItems.Add(menuItem6);
                }


                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                if (path == "/Overdues")
                {
                    menuItem.Selected = true;
                };

                if (path == "/RequestsAll")
                {
                    menuItem2.Selected = true;
                };
            }
        }

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            //Session.Abondon();
            //Response.Redirect("Home.aspx");
            Response.Redirect("/");
        }
    }

}