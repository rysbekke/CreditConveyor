using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using СreditСonveyor.Data.Partners;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Threading;
using System.Net.Http.Headers;
using СreditСonveyor.Data;
using Zamat;

namespace СreditСonveyor.Webadmin
{
    public partial class Webadmin : System.Web.UI.Page
    {
        public int AgentID, OrgID; public decimal fexp;
        public static int btnAddProductClick;
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        static string connectionStringOBAPIAddress = ConfigurationManager.ConnectionStrings["connectionStringOBAPIAddress"].ToString();
        static string connectionStringMpzApiAddress = ConfigurationManager.ConnectionStrings["connectionStringMpzApiAddress"].ToString();
        static string connectionStringMpzUser = ConfigurationManager.ConnectionStrings["connectionStringMpzUser"].ToString();
        static string connectionStringMpzPassword = ConfigurationManager.ConnectionStrings["connectionStringMpzPassword"].ToString();
        //static string connectionStringDCB360 = ConfigurationManager.ConnectionStrings["SendStatusDCB360"].ToString();
        //static string connectionStringDCB360Key = ConfigurationManager.ConnectionStrings["SendStatusDCB360Key"].ToString();

        public string fileupl = ConfigurationManager.ConnectionStrings["fileupl"].ToString();

        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        //public string connectionStringDNN = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=dnn803;User ID=sa;Password=Server2012";
        OleDbConnection oledbConn;
        DateTime dateNowServer, dateNow;
        protected string partnerdir = "Partcredits";
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{

            check_security();
            InitializeCulture();


            //}
            //catch (Exception exc) //Module failed to load
            //{
            //    //Exceptions.ProcessModuleLoadException(this, exc);
            //    //System.Windows.Forms.MessageBox.Show(exc.ToString());
            //    MsgBox(exc.ToString(), this.Page, this);
            //}


            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            try
            {
                if (!IsPostBack)
                {
                    //if (UserInfo.Username == "host")
                    {
                        pnlAdmin.Visible = true;
                        refreshRedirects();
                        var roles = (from r in dbRWZ.RequestsRoles select r).ToList();
                        ddlRole.DataSource = roles;
                        ddlRole.DataBind();
                        ddlRole2.DataSource = roles;
                        ddlRole2.DataBind();
                        var usrs = (from r in dbRWZ.Users2s orderby r.UserName select r).ToList();
                        ddlUsers.DataSource = usrs;
                        ddlUsers.DataBind();

                        /**/
                        refreshUsersRoles();
                        /**/
                        var branches = (from r in dbR.Branches select r).ToList();
                        ddlBranches.DataSource = branches;
                        ddlBranches.DataBind();
                        ddlCreditOfficers.DataSource = usrs;
                        ddlCreditOfficers.DataBind();
                        refreshRequestRedirects();
                        /*UserOffices*/
                        var offices = (from r in dbR.Offices select r).ToList();
                        ddlOffice.DataSource = offices;
                        ddlOffice.DataBind();

                        var usrs2 = (from r in dbRWZ.Users2s orderby r.UserName select r).ToList();
                        gvUsers.DataSource = usrs2;
                        gvUsers.DataBind();
                        /**/
                        var req = (from r in dbRWZ.Requests select r).ToList();
                        ddlRequests.DataSource = req;
                        ddlRequests.DataBind();
                        /**/
                        var gr = (from r in dbRWZ.Groups select r).ToList();
                        ddlGroups.DataSource = gr;
                        // ddlGroups.DataValueField = "GroupID";
                        // ddlGroups.DataTextField = "GroupName";
                        ddlGroups.DataBind();
                        /**/
                    }
                    //else
                    //{
                    //    pnlAdmin.Visible = false;
                    //}
                }
                else
                {
                    //if (UserInfo.Username == "host")
                    {
                        refreshUsersRoles();
                    }
                }

            }
            catch (Exception exc) //Module failed to load
            {
                //Exceptions.ProcessModuleLoadException(this, exc);
            }


        }


        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }


        public void check_security()
        {
            string UserName = (Session["UserName"] != null) ? Session["UserName"].ToString() : "";
            string[] file = Request.CurrentExecutionFilePath.Split('/');
            string pageName1 = file[file.Length - 2];
            string pageName2 = file[file.Length - 1];
            GeneralController gtx = new GeneralController();
            if (gtx.check_security_page(UserName, pageName1, pageName2) == false) Response.Redirect("/Account/Login");
        }

        //public void check_security(string UserName)
        //{
        //    //dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        //    //string userName = Session["UserName"] as string;

        //    //string path = HttpContext.Current.Request.Url.AbsolutePath;

        //    //string UserName = (Session["UserName"] != null) ? Session["UserName"].ToString():"";
        //    //if (Session["UserName"] == null)
        //    if (UserName == "")
        //    {
        //        //var usr = (from u in dbR.Users where (u.UserName == userName) select u).FirstOrDefault();
        //        //hfUserID.Value = usr.UserID.ToString();
        //        Response.Redirect("/Account/Login");
        //    }
        //    else
        //    {
        //        //string UserName = Session["UserName"].ToString();
        //        int userRoleID = GeneralController.getRoleIDfromСreditСonveyor(UserName);

        //        string[] file = Request.CurrentExecutionFilePath.Split('/');
        //        string fileName = file[file.Length - 1];
        //        bool f = false;

        //        //if (fileName == "Dashboard.aspx")


        //        if (userRoleID == 8) //агент билайн
        //        {
        //            if (fileName.Contains("AgentView")) f = true;
        //            if (fileName.Contains("Card")) f = true;
        //            //menuItem.NavigateUrl = "/Beeline/AgentView.aspx";
        //            //menuItem2.NavigateUrl = "/Card/Card.aspx";
        //        }

        //        if (userRoleID == 9) //админ билайн
        //        {
        //            if (fileName.Contains("AgentView")) f = true;
        //            //menuItem.NavigateUrl = "/Beeline/AgentView.aspx";
        //        }


        //        if (userRoleID == 1) //агент нур
        //        {
        //            if (fileName.Contains("Nurtelecom")) f = true;
        //            if (fileName.Contains("Card")) f = true;
        //            //menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
        //            //menuItem2.NavigateUrl = "/Card/Card.aspx";
        //        }

        //        if (userRoleID == 4) //админ нур
        //        {
        //            if (fileName.Contains("Nurtelecom")) f = true;
        //            //menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
        //        }

        //        if (userRoleID == 13) //агент техно
        //        {
        //            if (fileName.Contains("Partners")) f = true;
        //            //menuItem.NavigateUrl = "/Partners/Partners.aspx";
        //        }

        //        if ((userRoleID == 2) || (userRoleID == 5)) //эксперты
        //        {
        //            if (fileName.Contains("Nurtelecom")) f = true;
        //            if (fileName.Contains("AgentView")) f = true;
        //            if (fileName.Contains("Partners")) f = true;
        //            //menuItem.NavigateUrl = "/Nurtelecom/Nurtelecom.aspx";
        //            //menuItem2.NavigateUrl = "/Beeline/AgentView.aspx";
        //            //menuItem3.NavigateUrl = "/Partners/Partners.aspx";
        //        }

        //        if (userRoleID == 19) //опер ГБ кард
        //        {
        //            if (fileName.Contains("Card")) f = true;
        //            //menuItem.NavigateUrl = "/Card/Card.aspx";
        //        }

        //        if (userRoleID == 20) //опер ГБ кард
        //        {
        //            if (fileName.Contains("Card")) f = true;
        //            //menuItem.NavigateUrl = "/Card/Card.aspx";
        //        }

        //        if (UserName == "rtentiev")
        //        {
        //            if (fileName.Contains("Webadmin")) f = true;
        //            //menuItem4.NavigateUrl = "/Webadmin/Webadmin.aspx";
        //        }

        //        if (f == false) Response.Redirect("/Account/Login");
        //    }


        //}

        protected override void InitializeCulture()
        {
            CultureInfo CI = new CultureInfo("pt-PT");
            CI.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";

            Thread.CurrentThread.CurrentCulture = CI;
            Thread.CurrentThread.CurrentUICulture = CI;
            base.InitializeCulture();
        }

        public void isRole()
        {
            if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
            {
                //RadNumOtherLoans.Visible = true;
                //lblOtherLoans.Visible = true;
                //txtBusinessComment.Visible = true;
                //lblBusinessComment.Visible = true;
                //chkbxlistStatus.Visible = true;
                //btnProffer.Visible = false;
                //btnIssue.Visible = false;
                //chkbxGroup.Visible = true;
                //btnConfirm.Visible = false;
                //btnNoConfirm.Visible = false;
                //tbDate2b.Visible = true;
                //btnFixed.Visible = false;
                int usrID = Convert.ToInt32(Session["UserID"].ToString());
                if ((usrID == 7420) || (usrID == 7512)) { 
                //    btnProffer.Visible = true; 
                }
            }
            if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты
            {
                //RadNumOtherLoans.Visible = true;
                //lblOtherLoans.Visible = true;
                //txtBusinessComment.Visible = true;
                //lblBusinessComment.Visible = true;
                //chkbxlistStatus.Visible = true;
                //chkbxGroup.Visible = true;
                //btnProffer.Visible = true;
                //btnIssue.Visible = false;
                //btnConfirm.Visible = false;
                //btnNoConfirm.Visible = false;
                //btnFix.Visible = false;
                //tbDate2b.Visible = true;
            }
         
       
        }

      

        public string getCorrectDatetxt(object o)
        {
            return Convert.ToDateTime(o).Date.ToString("yyyy.MM.dd");
        }


        /***********************/


        public void refreshRedirects()
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            var redirect = (from r in dbRWZ.TblRedirects select r).ToList();
            gvRedirect.DataSource = redirect;
            gvRedirect.DataBind();
        }

        public void refreshRequestRedirects()
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            //var reqredirect = (from r in dbRWZ.RequestsRedirects
            //                   join t in dbRWZ.Users2s on r.creditOfficerID equals t.UserID
            //                   join y in dbR.Branches on r.branchID equals y.ID
            //                   select new { id = r.id, BranchID = r.branchID, CreditOfficerID = t.UserID, y.Name, t.UserName }).ToList();
            var reqredirect = (from r in dbRWZ.RequestsRedirect
                               join t in dbRWZ.Users2s on r.creditOfficerID equals t.UserID
                               //join y in dbR.Branches on r.branchID equals y.ID
                               select new { id = r.id, BranchID = r.branchID, CreditOfficerID = t.UserID, t.UserName }).ToList();
            gvRequestsRedirect.DataSource = reqredirect;
            gvRequestsRedirect.DataBind();
        }

        

        public void refreshUsersRoles()
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var usersRoles = (from r in dbRWZ.RequestsUsersRoles
                              join t in dbRWZ.Users2s on r.UserID equals t.UserID
                              where (r.RoleID.ToString() == ddlRole2.SelectedItem.Value)
                              select new { ID = r.ID, UserID = r.UserID, RoleID = r.RoleID, NameAgent = r.NameAgencyPoint, Addres = r.AddressAgencyPoint, NameAgent2 = r.NameAgencyPoint2, Addres2 = r.AddressAgencyPoint2, UserName = t.UserName, AttorneyDocName = r.AttorneyDocName, AttorneyDocDate = r.AttorneyDocDate, CustomerID = r.CustomerID }).ToList();
            gvUsersRoles.DataSource = usersRoles;
            gvUsersRoles.DataBind();
        }

        protected void gvUsersRoles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            //int id = Convert.ToInt32(e.CommandArgument);
            //hfRequestID.Value = id.ToString();
            //if (e.CommandName == "Sel")
            //{
            //    editcommand(id);
            //    refreshfiles();
            //    refreshProducts(2);
            //    LinkButton lb = e.CommandSource as LinkButton;
            //    GridViewRow gvr = lb.Parent.Parent as GridViewRow;
            //    gvr.BackColor = System.Drawing.Color.Empty;
            //    //gvr.BackColor = System.Drawing.Color.Red;
            //    string hex = "#cbceea";
            //    Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
            //    gvr.BackColor = _color;
            //    ibtnAddProduct.ImageUrl = "images/add.png";
            //}

            int id = Convert.ToInt32(e.CommandArgument);


            if (e.CommandName == "Sel")
            {

                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow gvr = lb.Parent.Parent as GridViewRow;
                gvr.BackColor = System.Drawing.Color.Empty;
                string hex = "#cbceea";
                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                gvr.BackColor = _color;
                var reqroles = (from v in dbRWZ.RequestsUsersRoles where (v.ID == id) select v).ToList().SingleOrDefault();
                tbAddressAgencyPoint.Text = reqroles.AddressAgencyPoint;
                tbNameAgencyPoint.Text = reqroles.NameAgencyPoint;
                tbAttorneyDocName.Text = reqroles.AttorneyDocName;
                tbAddressAgencyPoint2.Text = reqroles.AddressAgencyPoint2;
                tbNameAgencyPoint2.Text = reqroles.NameAgencyPoint2;

                tbAttorneyDocDate.Text = Convert.ToDateTime(reqroles.AttorneyDocDate).ToString("dd.MM.yyyy");
                //ddlRequestPeriod.SelectedIndex = ddlRequestPeriod.Items.IndexOf(ddlRequestPeriod.Items.FindByValue(lst.RequestPeriod.ToString()));
                ddlUsers.SelectedIndex = ddlUsers.Items.IndexOf(ddlUsers.Items.FindByValue(reqroles.UserID.ToString()));
            }
            if (e.CommandName == "Del")
            {
                dbRWZ.RequestsUsersRoles.DeleteAllOnSubmit(from v in dbRWZ.RequestsUsersRoles where (v.ID == id) select v);
                dbRWZ.RequestsUsersRoles.Context.SubmitChanges();
                refreshUsersRoles();
            }
        }

        protected void gvUsersRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsersRoles.PageIndex = e.NewPageIndex;
            refreshUsersRoles();
        }

        protected void btnAppendRedirects_Click(object sender, EventArgs e)
        {
            //dbdataDataContext dbDNN = new dbdataDataContext(connectionStringDNN);
            //dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            //dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            //var lst = (from v in dbDNN.Tabs where (v.TabID == Convert.ToInt32(urlpagess.Url)) select v).FirstOrDefault();
            
            //TblRedirect newItem = new TblRedirect
            //{
            //    RoleName = ddlRole.SelectedItem.Text,
            //    RoleID = Convert.ToInt32(ddlRole.SelectedItem.Value),
            //    RedirectName = lst.TabName,
            //    RedirectID = lst.TabID
            //};
            //dbRWZ.TblRedirects.InsertOnSubmit(newItem);
            //dbRWZ.TblRedirects.Context.SubmitChanges();
            //refreshRedirects();
            //Response.Redirect("/webadmin");
        }

        protected void btnAppendUsersRoles_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            RequestsUsersRole requserrole = dbRWZ.RequestsUsersRoles.Where(r => r.RoleID == Convert.ToInt32(ddlRole2.SelectedValue) && r.UserID == Convert.ToInt32(ddlUsers.SelectedValue)).ToList().FirstOrDefault();
            RequestsUsersRole requserrole2 = dbRWZ.RequestsUsersRoles.Where(r => r.RoleID == Convert.ToInt32(ddlRole2.SelectedValue) && r.UserID == Convert.ToInt32(ddlUsers.SelectedValue)).ToList().FirstOrDefault();
            if (requserrole != null) //редактирование
            {
                //Customer item = new Customer();
                //item = (from v in db.Customers where v.CustomerID == CustomerID select v).FirstOrDefault();
                requserrole.NameAgencyPoint = tbNameAgencyPoint.Text;
                requserrole.NameAgencyPoint2 = tbNameAgencyPoint2.Text;
                requserrole.AddressAgencyPoint = tbAddressAgencyPoint.Text;
                requserrole.AddressAgencyPoint2 = tbAddressAgencyPoint2.Text;
                requserrole.AttorneyDocName = tbAttorneyDocName.Text;
                requserrole.AttorneyDocDate = Convert.ToDateTime(tbAttorneyDocDate.Text.Substring(3, 2) + "." + tbAttorneyDocDate.Text.Substring(0, 2) + "." + tbAttorneyDocDate.Text.Substring(6, 4));
                dbRWZ.RequestsUsersRoles.Context.SubmitChanges();
            }
            else //новая запись
            {

                if (requserrole2 == null) //новый пользователь?
                {
                    RequestsUsersRole newItem = new RequestsUsersRole
                    {
                        UserID = Convert.ToInt32(ddlUsers.SelectedItem.Value),
                        UserName = ddlUsers.SelectedItem.Text,
                        RoleID = Convert.ToInt32(ddlRole2.SelectedItem.Value),
                        NameAgencyPoint = tbNameAgencyPoint.Text,
                        NameAgencyPoint2 = tbNameAgencyPoint2.Text,
                        AddressAgencyPoint = tbAddressAgencyPoint.Text,
                        AddressAgencyPoint2 = tbAddressAgencyPoint2.Text,
                        AttorneyDocName = tbAttorneyDocName.Text,
                        //AttorneyDocDate = Convert.ToDateTime(tbAttorneyDocDate.Text.Substring(3, 2) + "." + tbAttorneyDocDate.Text.Substring(0, 2) + "." + tbAttorneyDocDate.Text.Substring(6, 4)),
                        AttorneyDocDate = Convert.ToDateTime(tbAttorneyDocDate.Text),
                    };
                    dbRWZ.RequestsUsersRoles.InsertOnSubmit(newItem);
                    dbRWZ.RequestsUsersRoles.Context.SubmitChanges();
                    refreshRedirects();
                    Response.Redirect("/Webadmin/Webadmin");
                }
                else // пользователь уже есть в друг ролях
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Пользователь уже есть", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    MsgBox("Пользователь уже есть ", this.Page, this);
                }
            }
            refreshUsersRoles();
        }

        protected void btnAppend_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            RequestsRedirect newItem = new RequestsRedirect
            {
                branchID = Convert.ToInt32(ddlBranches.SelectedItem.Value),
                creditOfficerID = Convert.ToInt32(ddlCreditOfficers.SelectedItem.Value),
                note = tbNote.Text,
            };
            dbRWZ.RequestsRedirect.InsertOnSubmit(newItem);
            dbRWZ.RequestsRedirect.Context.SubmitChanges();
            refreshRequestRedirects();
            Response.Redirect("/Webadmin/Webadmin");
        }

        protected void gvRequestsRedirect_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Del")
            {
                dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
                dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

                dbRWZ.RequestsRedirect.DeleteAllOnSubmit(from v in dbRWZ.RequestsRedirect where (v.id == id) select v);
                dbRWZ.RequestsRedirect.Context.SubmitChanges();
                refreshRequestRedirects();
            }
        }

        protected void btnReqStatus_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Request request = new Request();
            request = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(ddlRequests.SelectedItem.Text)).FirstOrDefault();
            request.RequestStatus = ddlStatus.SelectedItem.ToString();
            dbRWZ.Requests.Context.SubmitChanges();



            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = -1,
                CreditID = request.CreditID,
                StatusDate = DateTime.Now,
                Status = request.RequestStatus,
                RequestID = request.RequestID
            };
            dbRWZ.RequestsHistories.InsertOnSubmit(newItem);
            dbRWZ.RequestsHistories.Context.SubmitChanges();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            if (e.CommandName == "Sel")
            {

                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow gvr = lb.Parent.Parent as GridViewRow;
                gvr.BackColor = System.Drawing.Color.Empty;
                string hex = "#cbceea";
                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                gvr.BackColor = _color;
                var requsers = (from v in dbRWZ.Users2s where (v.UserID == id) select v).ToList().SingleOrDefault();
                tbUserName.Text = requsers.UserName;
                tbFullName.Text = requsers.Fullname;
                tbEmail.Text = requsers.EMail;
                tbPassw.Text = "";
                ddlOffice.SelectedIndex = ddlOffice.Items.IndexOf(ddlOffice.Items.FindByValue(requsers.OfficeID.ToString()));
                ddlGroups.SelectedIndex = ddlGroups.Items.IndexOf(ddlGroups.Items.FindByValue(requsers.GroupID.ToString()));
                if (requsers.isBlocked is null) requsers.isBlocked = false;
                ckbxIsBlocked.Checked = (bool)requsers.isBlocked;
            }
            if (e.CommandName == "Del")
            {
                dbRWZ.Users2s.DeleteAllOnSubmit(from v in dbRWZ.Users2s where (v.UserID == id) select v);
                dbRWZ.Users2s.Context.SubmitChanges();
                refreshUsers();
            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            refreshUsers();
        }

        protected void btnSaveUsers_Click(object sender, EventArgs e)
        {
            //var sha1 = Crypto.SHA1(psw).ToLower();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Users2 editusr = new Users2();
            editusr = dbRWZ.Users2s.Where(u => u.UserName == tbUserName.Text).FirstOrDefault();
            int grID = Convert.ToInt32(ddlGroups.SelectedValue);
            int? orgID = dbRWZ.Groups.Where(r => r.GroupID == grID).FirstOrDefault().OrgID;
            if (editusr != null)
            { //user есть в базе
                editusr.UserName = tbUserName.Text;
                editusr.Fullname = tbFullName.Text;
                editusr.EMail = tbEmail.Text;
                if (tbPassw.Text != "") editusr.Password = Crypto.SHA1(tbPassw.Text).ToLower();
                editusr.OfficeID = Convert.ToInt32(ddlOffice.SelectedItem.Value);
                dbRWZ.Users2s.Context.SubmitChanges();
                editusr.GroupID = grID;
                editusr.OrgID = orgID;
                dbRWZ.Users2s.Context.SubmitChanges();
            }
            else
            { //новый user
                Users2 newItem = new Users2
                {
                    UserID = Convert.ToInt32(ddlUsers.SelectedItem.Value),
                    UserName = tbUserName.Text,
                    Fullname = tbFullName.Text,
                    Password = Crypto.SHA1(tbPassw.Text).ToLower(),
                    EMail = tbEmail.Text,
                    OfficeID = Convert.ToInt32(ddlOffice.SelectedValue),
                    CreateDate = DateTime.Now,
                    PasswordExpiryDate = DateTime.MaxValue,
                    GroupID = grID,
                    OrgID = orgID
                };
                dbRWZ.Users2s.InsertOnSubmit(newItem);
                dbRWZ.Users2s.Context.SubmitChanges();
                refreshUsers();
                Response.Redirect("/Webadmin/Webadmin");
            }
        }

        protected void gvRedirect_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            foreach (GridViewRow gvr2 in gvRedirect.Rows)
            {
                gvr2.BackColor = System.Drawing.Color.Empty;
            }
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Del")
            {
                dbRWZ.TblRedirects.DeleteAllOnSubmit(from v in dbRWZ.TblRedirects where (v.id == id) select v);
                dbRWZ.TblRedirects.Context.SubmitChanges();
                refreshRedirects();
            }
            if (e.CommandName == "Sel")
            {
                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow gvr = lb.Parent.Parent as GridViewRow;
                gvr.BackColor = System.Drawing.Color.Empty;
                string hex = "#cbceea";
                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                gvr.BackColor = _color;
            }
        }

        protected void ddlRole2_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshUsersRoles();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void ckbxIsBlocked_CheckedChanged(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            if (ckbxIsBlocked.Checked) 
            {
                var lst = dbRWZ.Users2s.Where(r => r.UserName.Trim() == tbUserName.Text.Trim()).FirstOrDefault();
                lst.isBlocked = true;
                dbRWZ.Users2s.Context.SubmitChanges();

                LogAuthorization logAuth = new LogAuthorization()
                {
                    UserName = lst.UserName,
                    DateOfFailedAuth = DateTime.Now,
                    IsBlocked = 1,
                    Note = Session["UserName"] as string
                };
                dbRWZ.LogAuthorizations.InsertOnSubmit(logAuth);
                dbRWZ.LogAuthorizations.Context.SubmitChanges();
            }
            else
            {
                var logAuth2 = dbRWZ.LogAuthorizations.Where(x => x.UserName == tbUserName.Text.Trim()).ToList().OrderBy(x => x.DateOfFailedAuth).ToList();
                //dbRWZ.LogAuthorizations.DeleteAllOnSubmit(logAuth2);
                foreach (var log in logAuth2)
                {
                    log.IsBlocked = 0;
                }
                dbRWZ.LogAuthorizations.Context.SubmitChanges();



                var lst = dbRWZ.Users2s.Where(r => r.UserName.Trim() == tbUserName.Text.Trim()).FirstOrDefault();
                lst.isBlocked = false;
                dbRWZ.Users2s.Context.SubmitChanges();


                LogAuthorization logAuth = new LogAuthorization()
                {
                    UserName = lst.UserName,
                    DateOfFailedAuth = DateTime.Now,
                    IsBlocked = 0,
                    Note = Session["UserName"] as string
                };
                dbRWZ.LogAuthorizations.InsertOnSubmit(logAuth);
                dbRWZ.LogAuthorizations.Context.SubmitChanges();

            }
        }

        public void refreshUsers()
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var users = (from r in dbRWZ.Users2s select r).ToList();
            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshUsersRoles();
        }




    }
}