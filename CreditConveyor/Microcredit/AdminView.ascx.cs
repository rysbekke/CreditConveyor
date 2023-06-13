using System;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Configuration;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Host;
using DotNetNuke.Framework.Providers;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Globalization;
using System.Drawing;
using Telerik;
using System.IO;
using System.Web;
using System.Net.Mail;
using DotNetNuke.Services.Mail;
using MailPriority = DotNetNuke.Services.Mail.MailPriority;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Web.Helpers;
using System.Text.RegularExpressions;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace rysbek.Modules.Beeline
{
    public partial class AdminView : BeelineAgentModuleBase, IActionable
    {
        public int AgentID, OrgID; public decimal fexp;
        public static int btnAddProductClick;
        static string connectionStringR = ConfigurationManager.ConnectionStrings["DosCredobankConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["DosCredobankConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["DosCredobankConnectionStringRWZ"].ToString();
        //static string connectionStringNur = ConfigurationManager.ConnectionStrings["SendStatusNur"].ToString();
        //static string connectionStringNurKey = ConfigurationManager.ConnectionStrings["SendStatusNurKey"].ToString();
        static string connectionStringOBAPIAddress = ConfigurationManager.ConnectionStrings["connectionStringOBAPIAddress"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        //public string connectionStringDNN = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=dnn803;User ID=sa;Password=Server2012";
        OleDbConnection oledbConn;
        DateTime dateNowServer, dateNow;
        protected void Page_Load(object sender, EventArgs e)
        {
            //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, i.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
            try
            {
                if ((Session["Check"] == null) || (Session["Check"].ToString() != "true")) Response.Redirect("/Home");
                RadNumTbTotalPrice.Culture.NumberFormat.CurrencySymbol = "";
                RadNumTbTotalPrice.ToolTip = "Введите цену";
                if (Session["FIO"] != null) { lblUserName.Text = Session["FIO"].ToString(); }

                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                dateNowServer = dbR.GetTable<SysInfo>().FirstOrDefault().DateOD;
                dateNow = Convert.ToDateTime(DateTime.Now);
                //var time = dateNow.ToString("HH:mm:ss");
                //double hh = Convert.ToInt32(time.Substring(0, 2));
                //double mm = Convert.ToInt32(time.Substring(3, 2));
                //double hhmm = hh + mm / 100;
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "По техническим причинам программа временно не работает", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //if (((hhmm > 09.30) && (hhmm < 09.45)) || ((hhmm > 10.00) && (hhmm < 12.25)))
                //{
                //    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "DNN Error Connection To The Failed. The server response was: 5.4.5 Daily user sending quota exceeded. p17sm3281026ljb.43", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //    btnNewRequest.Visible = false;
                //}
                //else
                {

                    if (!IsPostBack)
                    {
                        databindDDL();
                        tbDate1b.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
                        tbDate2b.Text = DateTime.Today.Date.ToString("dd.MM.yyyy"); //"yyyy-MM-dd"
                        hfRequestAction.Value = "new";
                        /*-----*/
                        //Session["PostID"] = "1001";
                        //ViewState["PostID"] = Session["PostID"].ToString();
                        /*-----*/
                        btnAddProductClick = 1;
                        UpdateStockPrice();
                        //MyTimer.Enabled = true;
                        hfUserID.Value = Session["UserID"].ToString();
                        isRole();

                        /**/
                    }
                    else
                    {
                        /*-----*/
                        //if (ViewState["PostID"].ToString() == Session["PostID"].ToString())
                        //{
                        //    Session["PostID"] = (Convert.ToInt16(Session["PostID"]) + 1).ToString();
                        //    ViewState["PostID"] = Session["PostID"].ToString();
                        //}
                        //else
                        //{
                        //    ViewState["PostID"] = Session["PostID"].ToString();
                        //}
                        /*-----*/
                        isRole();
                    }
                    refreshGrid();

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }

        public void isRole()
        {
            if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
            {
                RadNumOtherLoans.Visible = false;
                lblOtherLoans.Visible = false;
                txtBusinessComment.Visible = false;
                lblBusinessComment.Visible = false;
                btnProffer.Visible = false;
                btnIssue.Visible = false;
                btnConfirm.Visible = false;
                btnNoConfirm.Visible = false;
                tbDate2b.Visible = true;
                btnFix.Visible = false;
                btnFixed.Visible = false;
                chkbxlistStatus.Visible = true;
                chkbxGroup.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты
            {
                RadNumOtherLoans.Visible = true;
                lblOtherLoans.Visible = true;
                txtBusinessComment.Visible = true;
                lblBusinessComment.Visible = true;
                btnProffer.Visible = true;
                btnIssue.Visible = false;
                btnConfirm.Visible = false;
                btnNoConfirm.Visible = false;
                btnFix.Visible = false;
                btnFixed.Visible = false;
                tbDate2b.Visible = true;
                chkbxlistStatus.Visible = true;
                chkbxGroup.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
            {
                RadNumOtherLoans.Visible = false;
                lblOtherLoans.Visible = false;
                txtBusinessComment.Visible = false;
                lblBusinessComment.Visible = false;
                btnProffer.Visible = false;
                btnIssue.Visible = false;
                btnFix.Visible = false;
                btnFixed.Visible = false;
                tbDate2b.Visible = true;
                chkbxlistStatus.Visible = true;
                chkbxlistKindActivity.Visible = true;
                chkbxGroup.Visible = true;
                btnForPeriod.Visible = true;
                btnForPeriodWithHistory.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ
            {
                RadNumOtherLoans.Visible = true;
                lblOtherLoans.Visible = true;
                txtBusinessComment.Visible = true;
                lblBusinessComment.Visible = true;
                btnProffer.Visible = true;
                btnIssue.Visible = false;
                btnConfirm.Visible = false;
                btnNoConfirm.Visible = false;
                btnFix.Visible = false;
                btnFixed.Visible = false;
                tbDate2b.Visible = true;
                chkbxlistStatus.Visible = true;
                chkbxGroup.Visible = true;
                chkbxlistKindActivity.Visible = true;
                btnForPeriod.Visible = true;
                btnForPeriodWithHistory.Visible = true;
            }
        }

        public void refreshGrid()
        {
            string dt1 = "", dt2 = "", surname = "", inn = "", name = "", nRequest = "";

            if (tbRequestID.Text != "") { nRequest = tbRequestID.Text; }
            if (tbSearchRequestINN.Text != "") { inn = tbSearchRequestINN.Text; }
            if (tbSearchRequestSurname.Text != "") { surname = tbSearchRequestSurname.Text; }
            if (tbSearchRequestName.Text != "") { name = tbSearchRequestName.Text; }

            if (tbDate1b.Text != "")
            {
                dt1 = tbDate1b.Text.Substring(6, 4) + "." + tbDate1b.Text.Substring(3, 2) + "." + tbDate1b.Text.Substring(0, 2);
            }
            if (tbDate2b.Text != "")
            {
                dt2 = tbDate2b.Text.Substring(6, 4) + "." + tbDate2b.Text.Substring(3, 2) + "." + tbDate2b.Text.Substring(0, 2);
            }

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
            lblGroup.Text = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupName;
            int? agentRoleID = 8; //int? agentRoleID = db.RequestsUsersRoles.Where(r => (r.UserID == usrID)).FirstOrDefault().RoleID;
                                  //1-все заявки Ошка
                                  //3-Тур
                                  //6-все заявки Светофор
                                  //8-Билайн
            var rle = Convert.ToInt32(Session["RoleID"]);
            var users2 = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault();
            int? orgID = dbRWZ.Groups.Where(g => g.GroupID == groupID).FirstOrDefault().OrgID;
            List<Request> lst;
            if (rle == 5) { lst = ItemController.GetRequestsAllForExpert(usrID, agentRoleID, nRequest, inn, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize); } //Эсперты ГБ Доскредо
            else if (rle == 2) { lst = ItemController.GetRequestsForExpert(usrID, agentRoleID, nRequest, inn, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize); } //Эксперты филиала Доскредо
            else if (rle == 9) { lst = ItemController.GetRequestsAllForAdmin(usrID, agentRoleID, orgID, nRequest, inn, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize); } //Админы Билайн
            else { lst = ItemController.GetRequestsForAgent(usrID, agentRoleID, nRequest, inn, surname, name, dt1, dt2, connectionStringR, groupID, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize); } //Агенты Нуртелеком
            if (lst.Count > 0)
            {
                var lst5 = (from r in lst
                            join b in dbRWZ.Branches on r.BranchID equals b.ID
                            select new { r.RequestID, r.CreditID, b.ShortName, r.Surname, r.CustomerName, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, r.GroupID, r.OrgID, r.StatusOB, r.RequestRate }).ToList();
                var lst6 = (from r in lst5
                            join g in dbRWZ.Groups on r.GroupID equals g.GroupID
                            select new { r.RequestID, r.CreditID, r.ShortName, r.Surname, r.CustomerName, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, g.GroupName, r.OrgID, r.StatusOB, r.RequestRate }).ToList();
                var lst7 = (from r in lst6
                            join o in dbRWZ.Organizations on r.OrgID equals o.OrgID
                            orderby r.RequestDate
                            select new { r.RequestID, r.CreditID, r.ShortName, r.Surname, r.CustomerName, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, r.GroupName, o.OrgName, r.StatusOB, r.RequestRate }).ToList();
                gvRequests.DataSource = lst7;
                gvRequests.DataBind();
            }
            else
            {
                gvRequests.DataSource = new string[] { };
                gvRequests.DataBind();
            }
            if ((rle == 2) || (rle == 5))
            {
                // gvRequests.Columns[13].Visible = true;
                gvRequests.Columns[15].Visible = true;
            }
            else
            {
                // gvRequests.Columns[13].Visible = true;
                gvRequests.Columns[15].Visible = false;
            }


        }

        public string getCorrectDatetxt(object o)
        {
            return Convert.ToDateTime(o).Date.ToString("yyyy.MM.dd");
        }

        protected void btnSearchClient_Click(object sender, EventArgs e)
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            var query = (from u in dbR.Customers where ((u.IdentificationNumber == tbSearchINN.Text) && (u.DocumentSeries == tbSerialN.Text.Substring(0, 5)) && (u.DocumentNo == tbSerialN.Text.Substring(5, 4))) select u).ToList().FirstOrDefault();
            if (query != null)
            {
                pnlCustomer.Visible = true;
                pnlCredit.Visible = false;
                //btnSaveCustomer.Enabled = false;
                btnCredit.Enabled = true;
                hfCustomerID.Value = query.CustomerID.ToString();
                tbSurname.Text = query.Surname;
                tbCustomerName.Text = query.CustomerName;
                tbOtchestvo.Text = query.Otchestvo;
                if (query.IsResident == true) { rbtIsResident.SelectedIndex = 0; } else { rbtIsResident.SelectedIndex = 1; }
                tbIdentificationNumber.Text = query.IdentificationNumber;
                tbDocumentSeries.Text = query.DocumentSeries + query.DocumentNo;
                tbIssueDate.Text = Convert.ToDateTime(query.IssueDate).ToString("dd.MM.yyyy");
                // tbValidTill.Text = Convert.ToDateTime(query.DocumentValidTill).ToString("yyyy-MM-dd");
                tbValidTill.Text = Convert.ToDateTime(query.DocumentValidTill).ToString("dd.MM.yyyy");
                tbIssueAuthority.Text = query.IssueAuthority;
                if (query.Sex == true) { rbtSex.SelectedIndex = 0; } else { rbtSex.SelectedIndex = 1; }
                tbDateOfBirth.Text = Convert.ToDateTime(query.DateOfBirth).ToString("dd.MM.yyyy");
                tbRegistrationStreet.Text = query.RegistrationStreet;
                tbRegistrationHouse.Text = query.RegistrationHouse;
                tbRegistrationFlat.Text = query.RegistrationFlat;
                tbResidenceStreet.Text = query.ResidenceStreet;
                tbResidenceHouse.Text = query.ResidenceHouse;
                tbContactPhone.Text = query.ContactPhone1;
                tbResidenceFlat.Text = query.ResidenceFlat;
                if (ddlDocumentTypeID.Items.Count > 0 && !string.IsNullOrEmpty(query.DocumentTypeID.ToString()))
                    ddlDocumentTypeID.SelectedIndex = ddlDocumentTypeID.Items.IndexOf(ddlDocumentTypeID.Items.FindByValue(query.DocumentTypeID.ToString()));
                if (ddlNationalityID.Items.Count > 0 && !string.IsNullOrEmpty(query.NationalityID.ToString()))
                    ddlNationalityID.SelectedIndex = ddlNationalityID.Items.IndexOf(ddlNationalityID.Items.FindByValue(query.NationalityID.ToString()));
                if (ddlBirthCountryID.Items.Count > 0 && !string.IsNullOrEmpty(query.BirthCountryID.ToString()))
                    ddlBirthCountryID.SelectedIndex = ddlBirthCountryID.Items.IndexOf(ddlBirthCountryID.Items.FindByValue(query.BirthCountryID.ToString()));
                if (ddlRegistrationCountryID.Items.Count > 0 && !string.IsNullOrEmpty(query.RegistrationCountryID.ToString()))
                    ddlRegistrationCountryID.SelectedIndex = ddlRegistrationCountryID.Items.IndexOf(ddlRegistrationCountryID.Items.FindByValue(query.RegistrationCountryID.ToString()));
                if (ddlResidenceCountryID.Items.Count > 0 && !string.IsNullOrEmpty(query.ResidenceCountryID.ToString()))
                    ddlResidenceCountryID.SelectedIndex = ddlResidenceCountryID.Items.IndexOf(ddlResidenceCountryID.Items.FindByValue(query.ResidenceCountryID.ToString()));
                if (ddlBirthCityName.Items.Count > 0 && !string.IsNullOrEmpty(query.BirthCityID.ToString()))
                    ddlBirthCityName.SelectedIndex = ddlBirthCityName.Items.IndexOf(ddlBirthCityName.Items.FindByValue(query.BirthCityID.ToString()));
                var birthCity = dbR.GetTable<City>().Where(c => c.CityID == query.BirthCityID).FirstOrDefault();
                if (birthCity != null)
                {
                    var region = dbR.GetTable<Region>().Where(reg => reg.RegionID == birthCity.RegionID).FirstOrDefault();
                    if (region.RegionName != null)
                    {
                    }
                }
                if (ddlRegistrationCityName.Items.Count > 0 && !string.IsNullOrEmpty(query.RegistrationCityID.ToString()))
                    ddlRegistrationCityName.SelectedIndex = ddlRegistrationCityName.Items.IndexOf(ddlRegistrationCityName.Items.FindByValue(query.RegistrationCityID.ToString()));
                var registrationCity = dbR.GetTable<City>().Where(c => c.CityID == query.RegistrationCityID).FirstOrDefault();
                if (registrationCity != null)
                {
                    var region = dbR.GetTable<Region>().Where(reg => reg.RegionID == registrationCity.RegionID).FirstOrDefault();
                }
                if (ddlResidenceCityName.Items.Count > 0 && !string.IsNullOrEmpty(query.ResidenceCityID.ToString()))
                    ddlResidenceCityName.SelectedIndex = ddlResidenceCityName.Items.IndexOf(ddlResidenceCityName.Items.FindByValue(query.ResidenceCityID.ToString()));
                var residenceCity = dbR.GetTable<City>().Where(c => c.CityID == query.ResidenceCityID).FirstOrDefault();
                if (residenceCity != null)
                {
                    var region = dbR.GetTable<Region>().Where(reg => reg.RegionID == residenceCity.RegionID).FirstOrDefault();
                }
            }
            else
            {
                pnlCustomer.Visible = true;
                pnlCredit.Visible = false;
                clearEditControls();
                btnSaveCustomer.Enabled = true;
                btnCredit.Enabled = false;
            }
        }

        public void databindDDL()
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            ddlNationalityID.DataSource = dbR.GetTable<Country>();
            ddlNationalityID.DataBind();
            ddlBirthCountryID.DataSource = dbR.GetTable<Country>();
            ddlBirthCountryID.DataBind();
            ddlRegistrationCountryID.DataSource = dbR.GetTable<Country>();
            ddlRegistrationCountryID.DataBind();
            ddlResidenceCountryID.DataSource = dbR.GetTable<Country>();
            ddlResidenceCountryID.DataBind();
            ddlBirthCityName.Items.Clear();
            ddlRegistrationCityName.Items.Clear();
            ddlResidenceCityName.Items.Clear();
            var tblCity = dbR.GetTable<City>();
            foreach (var rowCity in tblCity)
            {
                var region = dbR.GetTable<Region>().Where(reg => reg.RegionID == rowCity.RegionID).FirstOrDefault();
                ddlBirthCityName.Items.Add(new ListItem(rowCity.CityName + ", " + region.RegionName, rowCity.CityID.ToString()));
                ddlRegistrationCityName.Items.Add(new ListItem(rowCity.CityName + ", " + region.RegionName, rowCity.CityID.ToString()));
                ddlResidenceCityName.Items.Add(new ListItem(rowCity.CityName + ", " + region.RegionName, rowCity.CityID.ToString()));
            }
            ddlProductIndChg();

            /**/
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
            int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
            int groupID = Convert.ToInt32(dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID);
            int orgID = Convert.ToInt32(dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID);
            databindDDLProduct(orgID, groupID);
            /***/

            databindDDLRatePeriod();

        }


        public void databindDDLRatePeriod2()
        {
            /**********/
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
            int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
            int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
            int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
            if (orgID == 3) //beeline
            {
                if (ddlRequestRate.Text == "0.00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("12");
                }

                if (ddlRequestRate.Text == "29.90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }


            if (orgID == 7)
            {
                if (ddlRequestRate.Text == "0.00")
                {
                    if (groupID == 36) //Пролинк
                    {
                        ddlRequestPeriod.Items.Clear();
                        //ddlRequestPeriod.Items.Add("3");
                        ddlRequestPeriod.Items.Add("6");
                        //ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 38) //ИП Мазниченко СМ
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                    }
                    if (groupID == 41) //ИП Рыжова Ирина Игоревна
                    {
                        ddlRequestPeriod.Items.Clear();
                        //ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 40) //ИП Ким Андрей Аванасьевич
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                    }
                    if (groupID == 53) //ИП Юмакаева Литфие Мельсовна
                    {
                        ddlRequestPeriod.Items.Clear();
                        //ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 54) //ИП Масловец Андрей Викторович
                    {
                        ddlRequestPeriod.Items.Clear();
                        //ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 57) //ОсОО "EPSILON LTD"
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 60) //ИП Волин Константин Петрович
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                        //ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 77) //ОсОО "Азия Сеть Ритейл"
                    {
                        ddlRequestPeriod.Items.Clear();
                    }
                    if (groupID == 81) //ИП Маметова Зумрад Акбаровна
                    {
                        ddlRequestPeriod.Items.Clear();
                        //ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 93) //ИП Волженко Елена Анатольевна
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                    }
                    if (groupID == 96) //ИП Байоглиева София Закировна
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                    }

                    if (groupID == 107) //ИП Черкащенко Елена Сергеевна
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                    }
                    if (groupID == 112) //ИП Раенко Дарья
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                    }
                    if (groupID == 113) //ИП Субанбеков Нуртай
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                    }

                }

                if (ddlRequestRate.Text == "29.90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }

                btnGetScoreBee.Visible = false;
            }
            else
            {
                btnGetScoreBee.Visible = true;
            }

            /***********/
        }


        public void databindDDLRatePeriod()
        {
            if (ddlProduct.SelectedValue == "Замат")
            {
                ddlRequestPeriod.Items.Clear();
                for (int i = 3; i < 13; i++)
                {
                    ddlRequestPeriod.Items.Add(i.ToString());
                }
            }
            if (ddlProduct.SelectedValue == "003")
            {
                ddlRequestPeriod.Items.Clear();
                ddlRequestPeriod.Items.Add("3");
            }

            if (ddlProduct.SelectedValue == "006(Honor S7)")
            {
                ddlRequestPeriod.Items.Clear();
                ddlRequestPeriod.Items.Add("6");
            }
            if (ddlProduct.SelectedValue == "006")
            {
                ddlRequestPeriod.Items.Clear();
                ddlRequestPeriod.Items.Add("6");
            }
            if (ddlProduct.SelectedValue == "0012")
            {
                ddlRequestPeriod.Items.Clear();
                ddlRequestPeriod.Items.Add("12");
            }
            if (ddlProduct.SelectedValue == "0012(Honor S7)")
            {
                ddlRequestPeriod.Items.Clear();
                ddlRequestPeriod.Items.Add("12");
            }
            if (ddlProduct.SelectedValue == "0018")
            {
                ddlRequestPeriod.Items.Clear();
                ddlRequestPeriod.Items.Add("18");
            }
        }

        public void databindDDLProduct(int orgID, int groupID)
        {

            if (orgID == 3) //beeline
            {
                ddlProduct.Items.Clear();
                ddlProduct.Items.Add("Замат");
                ddlProduct.Items.Add("003");
                ddlProduct.Items.Add("006");
                //ddlProduct.Items.Add("0012");

                if (groupID == 121) //
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("003");
                    ddlProduct.Items.Add("006(Honor S7)");
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("0012");
                    ddlProduct.Items.Add("0012(Honor S7)");
                    ddlProduct.Items.Add("0018");
                }

                if (groupID == 110)
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Нано");
                }

            }


            if (orgID == 7)
            {
                ddlProduct.Items.Clear();
                ddlProduct.Items.Add("Замат");
                ddlProduct.Items.Add("003");
                ddlProduct.Items.Add("006");

                if (groupID == 36) //Пролинк
                {

                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    //ddlProduct.Items.Add("003");
                    ddlProduct.Items.Add("006");

                }
                if (groupID == 38) //ИП Мазниченко СМ
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("006");

                }
                if (groupID == 41) //ИП Рыжова Ирина Игоревна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("0012");
                }
                if (groupID == 40) //ИП Ким Андрей Аванасьевич
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("006");
                }
                if (groupID == 53) //ИП Юмакаева Литфие Мельсовна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                }
                if (groupID == 54) //ИП Масловец Андрей Викторович
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                }
                if (groupID == 57) //ОсОО "EPSILON LTD"
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("0012");
                }
                if (groupID == 60) //ИП Волин Константин Петрович
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("006");
                }
                if (groupID == 77) //ОсОО "Азия Сеть Ритейл"
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                }
                if (groupID == 81) //ИП Маметова Зумрад Акбаровна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                }

                if (groupID == 93) //ИП Волженко Елена Анатольевна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("Замат");
                }

                if (groupID == 96) //ИП Байоглиева София Закировна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                }

                if (groupID == 107) //ИП Черкащенко Елена Сергеевна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("006");
                }
                if (groupID == 112) //ИП Раенко Дарья
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("006");
                }
                if (groupID == 113) //ИП Субанбеков Нуртай
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("Замат");
                    ddlProduct.Items.Add("006");
                }



                //if (ddlRequestRate.Text == "29.90")
                //{
                //    ddlRequestPeriod.Items.Clear();
                //    for (int i = 3; i < 13; i++)
                //    {
                //        ddlRequestPeriod.Items.Add(i.ToString());
                //    }
                //}

                btnGetScoreBee.Visible = false;
            }
            else
            {
                btnGetScoreBee.Visible = true;
            }

            /***********/

        }





        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            clearEditControls();
            btnSaveCustomer.Enabled = true;
            btnCredit.Enabled = false;
            pnlCustomer.Visible = true;
            pnlCredit.Visible = false;
            hfCustomerID.Value = "noselect";
            ddlDocumentTypeID.SelectedIndex = 0;
        }

        public void clearEditControls()
        {
            lblStatusRequest.Text = "";
            tbSurname.Text = "";
            tbResidenceStreet.Text = "";
            tbResidenceHouse.Text = "";
            tbResidenceFlat.Text = "";
            tbRegistrationStreet.Text = "";
            tbRegistrationHouse.Text = "";
            tbRegistrationFlat.Text = "";
            tbOtchestvo.Text = "";
            tbIssueDate.Text = "";
            tbValidTill.Text = "";
            tbIssueAuthority.Text = "";
            tbIdentificationNumber.Text = "";
            tbDocumentSeries.Text = "";
            tbDateOfBirth.Text = "";
            tbContactPhone.Text = "";
            tbCustomerName.Text = "";
            RadNumTbSumMonthSalary.Text = "";
            RadNumTbRevenueAgro.Text = "";
            RadNumTbMinRevenue.Text = "";
            RadNumTbMaxRevenue.Text = "";
        }

        protected void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DateTime DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4));
                string DateOfBirth = tbDateOfBirth.Text.Substring(0, 2) + (tbDateOfBirth.Text.Substring(3, 2) + tbDateOfBirth.Text.Substring(6, 4));
                string DateOfBirthINN = (tbIdentificationNumber.Text.Substring(1, 8)); bool f = true;
                if (DocumentValidTill.Date < System.DateTime.Now.Date.AddDays(1)) { MsgBox("Паспорт проссрочен", this.Page, this); f = false; }
                if (DateOfBirth != DateOfBirthINN) { MsgBox("Неправильно введены паспортные данные", this.Page, this); f = false; }
                if (f)
                {
                    if (hfCustomerID.Value == "noselect")
                    {
                        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                        var cust = dbR.Customers.Where(c => ((c.IdentificationNumber == tbIdentificationNumber.Text) && (c.DocumentSeries == tbDocumentSeries.Text.Substring(0, 5) && (c.DocumentNo == tbDocumentSeries.Text.Substring(5, 4))))).ToList();
                        if (cust.Count == 0)
                        {
                            Customer newItem = new Customer
                            {
                                Surname = tbSurname.Text.Trim(),
                                CustomerName = tbCustomerName.Text.Trim(),
                                Otchestvo = tbOtchestvo.Text.Trim(),
                                CustomerTypeID = 1,
                                PersonActivityTypeID = 19,
                                WorkTypeID = 0,
                                IsResident = (rbtIsResident.SelectedIndex == 0) ? true : false,
                                IdentificationNumber = tbIdentificationNumber.Text,
                                DocumentSeries = tbDocumentSeries.Text.Substring(0, 5),
                                DocumentNo = tbDocumentSeries.Text.Substring(5, 4), //tbDocumentNo.Text,
                                IssueDate = Convert.ToDateTime(tbIssueDate.Text.Substring(3, 2) + "." + tbIssueDate.Text.Substring(0, 2) + "." + tbIssueDate.Text.Substring(6, 4)),
                                DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4)),
                                IssueAuthority = tbIssueAuthority.Text,
                                Sex = (rbtSex.SelectedIndex == 0) ? true : false,
                                DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text.Substring(3, 2) + "." + tbDateOfBirth.Text.Substring(0, 2) + "." + tbDateOfBirth.Text.Substring(6, 4)),
                                RegistrationStreet = tbRegistrationStreet.Text,
                                RegistrationHouse = tbRegistrationHouse.Text,
                                RegistrationFlat = tbRegistrationFlat.Text,
                                ResidenceStreet = tbResidenceStreet.Text,
                                ResidenceHouse = tbResidenceHouse.Text,
                                ResidenceFlat = tbResidenceFlat.Text,
                                DocumentTypeID = Convert.ToInt32(ddlDocumentTypeID.SelectedValue),
                                NationalityID = Convert.ToInt32(ddlNationalityID.SelectedItem.Value),
                                BirthCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value),
                                RegistrationCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value),
                                ResidenceCountryID = Convert.ToInt32(ddlResidenceCountryID.SelectedItem.Value),
                                BirthCityID = Convert.ToInt32(ddlBirthCityName.SelectedItem.Value),
                                RegistrationCityID = Convert.ToInt32(ddlRegistrationCityName.SelectedItem.Value),
                                ResidenceCityID = Convert.ToInt32(ddlResidenceCityName.SelectedItem.Value),
                                ContactPhone1 = tbContactPhone.Text,
                            };
                            SysController ctx = new SysController();
                            ctx.CustomerAddItem(newItem);
                            hfCustomerID.Value = newItem.CustomerID.ToString();
                            btnCredit.Enabled = true;
                            btnSaveCustomer.Enabled = false;
                            /*btnSave_click*/
                            pnlMenuCustomer.Visible = false;
                            pnlCustomer.Visible = false;
                            pnlCredit.Visible = true;
                            if (btnCredit.Text == "Выбрать клиента")
                            {
                                hfCustomerID.Value = hfCustomerID.Value;
                                tbSurname2.Text = tbSurname.Text;
                                tbCustomerName2.Text = tbCustomerName.Text;
                                tbOtchestvo2.Text = tbOtchestvo.Text;
                                tbINN2.Text = tbIdentificationNumber.Text;
                            }
                        }
                        else
                        {
                            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Клиент есть в базе, сделайте поиск по ИНН", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        }
                        /*btnSave_click*/
                    }
                    else
                    {
                        /*Редактирование*/
                        int CustID;
                        //DataContext db = new DataContext(connectionString);
                        SysController ctx = new SysController();
                        if (btnCredit.Text == "Выбрать клиента") { CustID = Convert.ToInt32(hfCustomerID.Value); } else { CustID = Convert.ToInt32(hfGuarantorID.Value); }
                        Customer cust = ctx.CustomerGetItem(CustID);
                        //cust.DocumentSeries = tbDocumentSeries.Text.Substring(0, 5);
                        //cust.DocumentNo = tbDocumentSeries.Text.Substring(5, 4);
                        //cust.IssueDate = Convert.ToDateTime(tbIssueDate.Text.Substring(3, 2) + "." + tbIssueDate.Text.Substring(0, 2) + "." + tbIssueDate.Text.Substring(6, 4));
                        //cust.DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4));
                        //cust.IssueAuthority = tbIssueAuthority.Text;
                        //cust.Sex = (rbtSex.SelectedIndex == 0) ? true : false;
                        //cust.DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text.Substring(3, 2) + "." + tbDateOfBirth.Text.Substring(0, 2) + "." + tbDateOfBirth.Text.Substring(6, 4));
                        //cust.RegistrationStreet = tbRegistrationStreet.Text;
                        //cust.RegistrationHouse = tbRegistrationHouse.Text;
                        //cust.RegistrationFlat = tbRegistrationFlat.Text;
                        //cust.ResidenceStreet = tbResidenceStreet.Text;
                        //cust.ResidenceHouse = tbResidenceHouse.Text;
                        //cust.ResidenceFlat = tbResidenceFlat.Text;
                        cust.ContactPhone1 = tbContactPhone.Text;
                        ctx.CustomerUpdItem(cust);
                        pnlCredit.Visible = true;
                        pnlMenuCustomer.Visible = false;
                        pnlCustomer.Visible = false;
                        //if (hfChooseClient.Value == "Выбрать клиента")
                        {
                            hfCustomerID.Value = hfCustomerID.Value;
                            tbSurname2.Text = tbSurname.Text;
                            tbCustomerName2.Text = tbCustomerName.Text;
                            tbOtchestvo2.Text = tbOtchestvo.Text;
                            tbINN2.Text = tbIdentificationNumber.Text;
                        }
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearEditControls();
            btnCredit.Enabled = false;
            pnlMenuCustomer.Visible = false;
            pnlCustomer.Visible = false;
            pnlCredit.Visible = true;
        }

        protected void btnCredit_Click(object sender, EventArgs e)
        {
            pnlMenuCustomer.Visible = false;
            pnlCustomer.Visible = false;
            pnlCredit.Visible = true;
            if (btnCredit.Text == "Выбрать клиента")
            {
                hfCustomerID.Value = hfCustomerID.Value;
                tbSurname2.Text = tbSurname.Text;
                tbCustomerName2.Text = tbCustomerName.Text;
                tbOtchestvo2.Text = tbOtchestvo.Text;
                tbINN2.Text = tbIdentificationNumber.Text;
                btnCustomerEdit.Enabled = true;
            }
        }

        protected void btnCalculator_Click(object sender, EventArgs e)
        {
            if (pnlCalculator.Visible == true)
            { pnlCalculator.Visible = false; }
            else
            { pnlCalculator.Visible = true; }
        }

        public int issuanceOfCredit()
        {
            double i, k = 0;
            double s = Convert.ToDouble(RadNumTbRequestSumm.Text);
            double n = Convert.ToDouble(ddlRequestPeriod.Text);
            double stavka = Convert.ToDouble(ddlRequestRate.SelectedItem.Value);
            //double i = stavka / 12 / 100;
            i = (stavka != 0) ? stavka / 12 / 100 : 0;
            if (stavka == 0) k = s / n;
            if (stavka == 29.90) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            int f = 0; int y22 = 40;
            double additionalIncome = (RadNumTbAdditionalIncome.Text != "") ? Convert.ToDouble(RadNumTbAdditionalIncome.Text) : 0;
            double OtherLoans = (RadNumOtherLoans.Text != "") ? Convert.ToDouble(RadNumOtherLoans.Text) : 0;
            if (rbtnBusiness.SelectedIndex == 0)
            {
                decimal SumMonthSalary = Convert.ToDecimal(RadNumTbSumMonthSalary.Text);
                uint CountSalary = Convert.ToUInt32(ddlMonthCount.SelectedItem.Text);
                decimal AverageMonthSalary = SumMonthSalary / CountSalary;
                double zp = Convert.ToDouble(AverageMonthSalary);
                double chp = (zp + additionalIncome) - 7000;
                double cho = chp - k - OtherLoans;
                double y1 = 100 * cho / chp;
                double y2 = 100 * (k + OtherLoans) / (zp + additionalIncome);
                if ((zp > 50000) || (zp == 50000)) y22 = 50;
                if (zp < 50000) y22 = 40;

                if ((y1 > 24) && (y2 < y22)) f = 1;
                if (stavka == 0) { if (s > 100000) f = -100000; }
                else { if (s > 100000) f = -100000; }
            }
            if (rbtnBusiness.SelectedIndex == 1)
            {
                decimal MinRevenue = Convert.ToDecimal(RadNumTbMinRevenue.Text);
                decimal MaxRevenue = Convert.ToDecimal(RadNumTbMaxRevenue.Text);
                decimal Revenue = ((MinRevenue != 0) && (MaxRevenue != 0)) ? (MinRevenue + MaxRevenue) / 2 : MinRevenue + MaxRevenue;
                double costprice = Convert.ToDouble(RadNumTbСostPrice.Text);
                double v = Convert.ToDouble(Revenue) * Convert.ToUInt32(ddlCountWorkDay.SelectedItem.Text);
                double valp = (costprice == 0) ? v : (v * costprice) / (100 + costprice);
                double chp = (valp + additionalIncome) - (Convert.ToDouble(RadNumTbOverhead.Text) + Convert.ToDouble(RadNumTbFamilyExpenses.Text));
                if ((valp > 50000) || (valp == 50000)) y22 = 50;
                if (valp < 50000) y22 = 40;
                if (chp > 0)
                {
                    double cho = chp - k - OtherLoans;
                    double y1 = 100 * cho / chp;
                    double y2 = 100 * (k + OtherLoans) / (valp + additionalIncome);
                    if ((y1 > 24) && (y2 < y22)) f = 1;
                }
                if (stavka == 0) { if (s > 100000) f = -100000; }
                else { if (s > 100000) f = -100000; }
            }
            if (rbtnBusiness.SelectedIndex == 2) //Агро
            {
                double RevenueAgro = Convert.ToDouble(RadNumTbRevenueAgro.Text);
                double RevenueMilk = Convert.ToDouble(RadNumTbRevenueMilk.Text);
                double OverheadAgro = Convert.ToDouble(RadNumTbOverheadAgro.Text);
                OverheadAgro = (OverheadAgro > 0) ? OverheadAgro / 3 : 0;
                double AddOverheadAgro = Convert.ToDouble(RadNumTbAddOverheadAgro.Text);
                double FamilyExpensesAgro = Convert.ToDouble(RadNumTbFamilyExpensesAgro.Text);
                double v = Convert.ToDouble(RevenueAgro) / 3 + (Convert.ToDouble(RevenueMilk) * 25);
                double valp = v - OverheadAgro - AddOverheadAgro;
                double chp = (valp + additionalIncome) - Convert.ToDouble(FamilyExpensesAgro);
                if ((valp > 50000) || (valp == 50000)) y22 = 50;
                if (valp < 50000) y22 = 40;
                if (chp > 0)
                {
                    double cho = chp - k - OtherLoans;
                    double y1 = 100 * cho / chp;
                    double y2 = 100 * (k + OtherLoans) / (valp + additionalIncome);
                    if ((y1 > 24) && (y2 < y22)) f = 1;
                }
                if (s > 20000) f = -20000;
            }
            return f;
        }

        protected void btnSendCreditRequest_Click(object sender, EventArgs e)
        {
            if (hfCustomerID.Value != "noselect" && Page.IsValid) //условие для поруч
            {
                if (chbEmployer.Checked) // если сотрудник
                {
                    if ((Convert.ToInt32(Session["RoleID"]) == 8) || (Convert.ToInt32(Session["RoleID"]) == 9)) { AddRequest(); } //и (агент и админу то без скорринга)
                    else //не агент значить проверяем скорринг
                    {
                        int f = issuanceOfCredit();
                        if (f == 1)
                        {
                            AddRequest();
                        }
                        if (f == 0) { MsgBox("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита", this.Page, this); }
                        if (f == -20000) { MsgBox("Максимальная сумма кредита 20000 сом", this.Page, this); }
                        if (f == -100000) { MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this); }
                    }
                }
                else // если не сотрудник, то скорринг
                {

                    int age = GetCustomerAge(Convert.ToInt32(hfCustomerID.Value));
                    //MsgBox(age.ToString(), this.Page, this);
                    bool fage = false;
                    if (rbtnBusiness.SelectedIndex == 2) //Агро
                    {
                        if ((age > 24) && (age < 66))
                        {
                            fage = true;
                        }
                        else
                        {
                            MsgBox("Выдача кредита не возможна, возрастное ограничение 25-65, клиенту " + age.ToString() + " лет", this.Page, this);
                            fage = false;
                        }
                    }
                    else
                    {
                        if ((age > 22) && (age < 66))
                        {
                            fage = true;
                        }
                        else
                        {
                            fage = false;
                            MsgBox("Выдача кредита не возможна, возрастное ограничение 23-65, клиенту " + age.ToString() + " лет", this.Page, this);
                        }
                    }

                    int f = issuanceOfCredit();
                    if ((f == 1) && (fage))
                    {
                        AddRequest();
                    }
                    if (f == 0) { MsgBox("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита", this.Page, this); }
                    if (f == -20000) { MsgBox("Максимальная сумма кредита 20000 сом, если доход от агро", this.Page, this); }
                    if (f == -100000) { MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this); }
                }
            }
            else
            {
                lblMessageClient.Text = "Перед сохранением заявки нужно выбрать клиента";
            }
        }


        protected int GetCustomerAge(int CustomerId)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            DateTime birthDate = dbR.Customers.Where(c => c.CustomerID == CustomerId).FirstOrDefault().DateOfBirth.Value;
            DateTime nowDate = DateTime.Today;
            int age = nowDate.Year - birthDate.Year;
            if (birthDate > nowDate.AddYears(-age)) age--;
            return age;
        }

        protected void AddRequest()
        {
            decimal MinRevenue = (RadNumTbMinRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMinRevenue.Text) : 0;
            decimal MaxRevenue = (RadNumTbMaxRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMaxRevenue.Text) : 0;
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            if (hfRequestAction.Value == "new")
            {
                /*Новая заявка*/
                int usrID = Convert.ToInt32(Session["UserID"].ToString());
                int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
                int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
                int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                //int? orgID = db.RequestsRoles.Where(r => r.RoleID == usrRoleID).FirstOrDefault().OrgID;
                int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
                int? GroupCode = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupCode;
                int branchID = dbR.Offices.Where(r => r.ID == officeID).FirstOrDefault().BranchID;
                int? credOfficerID = 4583; //по умолч АЙбек
                credOfficerID = dbRWZ.RequestsRedirects.Where(r => r.branchID == branchID).FirstOrDefault().creditOfficerID;
                hfAgentRoleID.Value = usrRoleID.ToString();
                DateTime dateTimeServer = dateNowServer;
                DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now);

                decimal commision = 0; string NameOfCredit = "Замат"; int prodID = 1152; //замат
                if (chbEmployer.Checked) prodID = 1153; else prodID = 1152; //замат для сотруд иначе замат
                decimal rate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);
                byte period = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                bool isEmployer = chbEmployer.Checked ? true : false;
                //if (Convert.ToDecimal(RadNumTbRequestSumm.Text) > 100000) { NameOfCredit = "Потребительский"; prodID = 109; }

                //if ((rate == 0) && (period == 3)) { commision = 0; NameOfCredit = "0-0-3"; prodID = 1163; }
                //if ((rate == 0) && (period == 6)) { commision = 0; NameOfCredit = "0-0-6"; prodID = 1164; }
                //if ((rate == 0) && (period == 12)) { commision = 0; NameOfCredit = "0-0-12"; prodID = 1177; }


                if (ddlProduct.SelectedValue == "003") { commision = 0; NameOfCredit = "0-0-3"; prodID = 1163; }
                if (ddlProduct.SelectedValue == "006") { commision = 0; NameOfCredit = "0-0-6"; prodID = 1164; }
                if (ddlProduct.SelectedValue == "006(Honor S7)") { commision = 0; NameOfCredit = "0-0-6 (Honor S7)"; prodID = 1202; }
                //if (ddlProduct.SelectedValue == "006(Honor S7)") { commision = 0; NameOfCredit = "0-0-6 (Honor S7)"; prodID = 1193; }
                if (ddlProduct.SelectedValue == "009") { commision = 0; NameOfCredit = "0-0-9"; prodID = 1165; }
                if (ddlProduct.SelectedValue == "0012") { commision = 0; NameOfCredit = "0-0-12"; prodID = 1177; }
                if (ddlProduct.SelectedValue == "0012(Honor S7)") { commision = 0; NameOfCredit = "0-0-12 (Honor S7)"; prodID = 1201; }
                //if (ddlProduct.SelectedValue == "0012(Honor S7)") { commision = 0; NameOfCredit = "0-0-12 (Honor S7)"; prodID = 1189; }
                if (ddlProduct.SelectedValue == "0018") { commision = 0; NameOfCredit = "0-0-18"; prodID = 1190; }

                if (isEmployer == true) commision = 0;

                History newItemHistory = new History
                {
                    ProductID = prodID, // Замат для сотрудников
                    RequestDate = dateTimeServer, // Convert.ToDateTime(DateTime.Now),
                    RequestCurrencyID = 417,
                    RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                    RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value),
                    PaymentSourceID = 1, //1-зп 2-бизнес
                    LanguageID = 1,
                    //RequestMortrageTypeID = 14, //14-поручительство, 1-недвижимость
                    IncomeApproveTypeID = 1, //непонятно
                                             //LoanLocation = 
                                             //MarketingSourceID = 
                                             //RequestGrantComission
                                             //RequestGrantComissionType
                    OfficeID = officeID,  // 1105-центральный // officeID,
                    LoanPurposeTypeID = 2,
                    CalculationTypeID = 1,
                    //PartnerCompanyID = 1511854 // Для Нуртел - 1511854
                    //PartnerCompanyID = GroupCode,
                    ApprovedCurrencyID = 417,
                    ApprovedPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                    ApprovedRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value),
                    //ApprovedGrantComission = commision,
                    ApprovedGrantComissionType = 1
                };

                CreditController ctx = new CreditController();
                int CreditsHistoriesID = ctx.HistoriesAddItem(newItemHistory);
                /*----------------------------------------------------*/

                PartnersHistory parthis = new PartnersHistory()
                {
                    CompanyID = (int)GroupCode,
                    CreditID = CreditsHistoriesID,
                    TranchID = 1,
                    SumV = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    ComissionSum = 0,
                    IssueComissionPaymentTypeID = 1
                };
                ctx.PartnerHistoriesAddItem(parthis);
                /*----------------------------------------------------*/
                HistoriesCustomer newItemHistoriesCustomer = new HistoriesCustomer
                {
                    CreditID = CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    IsLeader = true,
                    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    ApprovedSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    CreditPurpose = tbCreditPurpose.Text,
                    //ApprovedSumm = /непонятно
                };
                ctx.HistoriesCustomerAddItem(newItemHistoriesCustomer);
                /*----------------------------------------------------*/
                var ProductsEarlyPaymentComissions = dbR.ProductsEarlyPaymentComissions.Where(r => r.ProductID == prodID).ToList().OrderBy(o => o.ChangeDate);
                decimal? RequestPartialComissionType = ProductsEarlyPaymentComissions.LastOrDefault().PartialComission;
                decimal? RequestFullPaymentComissionType = ProductsEarlyPaymentComissions.LastOrDefault().FullPaymentComission;
                HistoriesEarlyPaymentComission newItemHistoriesEarlyPaymentComission = new HistoriesEarlyPaymentComission
                {
                    CreditID = CreditsHistoriesID,
                    Period = 1,
                    RequestPartialComissionType = 2, //непонятно
                    RequestFullPaymentComissionType = 2,
                    ApprovedPartialComissionType = 2,
                    ApprovedFullPaymentComissionType = 2,
                    RequestPartialComission = Convert.ToByte(RequestPartialComissionType),
                    ApprovedPartialComission = Convert.ToByte(RequestPartialComissionType),
                    RequestFullPaymentComission = Convert.ToByte(RequestFullPaymentComissionType),
                    ApprovedFullPaymentComission = Convert.ToByte(RequestFullPaymentComissionType),
                };
                ctx.HistoriesEarlyPaymentComissionAddItem(newItemHistoriesEarlyPaymentComission);
                /*----------------------------------------------------*/
                HistoriesOfficer newItemHistoriesOfficer = new HistoriesOfficer
                {
                    CreditID = CreditsHistoriesID,
                    OfficerTypeID = 1, //уточнить
                    StartDate = dateTimeServer, //уточнить
                    UserID = credOfficerID, // 4583- Код Айбека  //usrID
                };
                ctx.HistoriesOfficerAddItem(newItemHistoriesOfficer);
                /*----------------------------------------------------*/
                string actdate = tbActualDate.Text.Substring(6, 4) + "." + tbActualDate.Text.Substring(3, 2) + "." + tbActualDate.Text.Substring(0, 2);
                IncomesStructuresActualDate newItemIncomesStructuresActualDate = new IncomesStructuresActualDate
                {
                    CreditID = CreditsHistoriesID,
                    ActualDate = dateTimeServer //Convert.ToDateTime(actdate), // dateTimeServer //Convert.ToDateTime(actdate),
                };
                ctx.IncomesStructuresActualDateAddItem(newItemIncomesStructuresActualDate);
                /*----------------------------------------------------*/
                IncomesStructure newItemIncomesStructures = new IncomesStructure
                {
                    CreditID = CreditsHistoriesID,
                    ActualDate = dateTimeServer,//Convert.ToDateTime(actdate), // dateTimeServer, //Convert.ToDateTime(actdate),
                    CurrencyID = 417,
                    TotalPercents = 100
                };
                ctx.ItemIncomesStructuresAddItem(newItemIncomesStructures);
                /*----------------------------------------------------*/ //save to dnn
                string comment = "";
                if (rbtnBusiness.SelectedIndex == 1) { fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 0; comment = txtBusinessComment.Text; }
                if (rbtnBusiness.SelectedIndex == 2) { fexp = (RadNumTbFamilyExpensesAgro.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpensesAgro.Text) : 0; comment = txtAgroComment.Text; }

                Request newRequest = new Request
                {
                    CreditPurpose = tbCreditPurpose.Text,
                    RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                    CreditProduct = ddlProduct.SelectedValue,
                    ProductPrice = Convert.ToDecimal(RadNumTbTotalPrice.Text),
                    AmountDownPayment = Convert.ToDecimal(RadNumTbAmountOfDownPayment.Text),
                    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text),
                    RequestGrantComission = Convert.ToDecimal(lblCommission.Text),
                    ActualDate = Convert.ToDateTime(actdate),
                    Surname = tbSurname2.Text,
                    CustomerName = tbCustomerName2.Text,
                    Otchestvo = tbOtchestvo2.Text,
                    IdentificationNumber = tbINN2.Text,
                    RequestDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    AgentID = usrID,
                    AgentRoleID = usrRoleID,
                    AgentUsername = Session["UserName"].ToString(),
                    AgentFirstName = Session["FIO"].ToString(),
                    AgentLastName = Session["FIO"].ToString(),
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    RequestStatus = "Новая заявка",
                    CreditID = CreditsHistoriesID,
                    BranchID = branchID,
                    AverageMonthSalary = (RadNumTbSumMonthSalary.Text != "") ? Convert.ToDecimal(RadNumTbSumMonthSalary.Text) / Convert.ToByte(ddlMonthCount.SelectedValue) : 0,
                    SumMonthSalary = (RadNumTbSumMonthSalary.Text != "") ? Convert.ToDecimal(RadNumTbSumMonthSalary.Text) : 0,
                    CountMonthSalary = Convert.ToByte(ddlMonthCount.SelectedValue),
                    //MonthlyInstallment = (RadNumTbMonthlyInstallment.Text != "") ? Convert.ToDecimal(RadNumTbMonthlyInstallment.Text) : 0,
                    //editRequest.Revenue = (RadNumTbRevenue.Text != "") ? Convert.ToDecimal(RadNumTbRevenue.Text) : 0;
                    MinRevenue = (RadNumTbMinRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMinRevenue.Text) : 0,
                    MaxRevenue = (RadNumTbMaxRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMaxRevenue.Text) : 0,
                    Revenue = ((MinRevenue != 0) && (MaxRevenue != 0)) ? (MinRevenue + MaxRevenue) / 2 : MinRevenue + MaxRevenue,
                    CountWorkDay = Convert.ToByte(ddlCountWorkDay.SelectedValue),
                    СostPrice = (RadNumTbСostPrice.Text != "") ? Convert.ToDecimal(RadNumTbСostPrice.Text) : 0,
                    Overhead = (RadNumTbOverhead.Text != "") ? Convert.ToDecimal(RadNumTbOverhead.Text) : 0,
                    FamilyExpenses = fexp,
                    Bussiness = rbtnBusiness.SelectedIndex,
                    OtherLoans = (RadNumOtherLoans.Text != "") ? Convert.ToDecimal(RadNumOtherLoans.Text) : 0,
                    BusinessComment = comment,
                    AdditionalIncome = (RadNumTbAdditionalIncome.Text != "") ? Convert.ToDecimal(RadNumTbAdditionalIncome.Text) : 0,
                    RevenueAgro = (RadNumTbRevenueAgro.Text != "") ? Convert.ToDecimal(RadNumTbRevenueAgro.Text) : 0,
                    RevenueMilk = (RadNumTbRevenueMilk.Text != "") ? Convert.ToDecimal(RadNumTbRevenueMilk.Text) : 0,
                    OverheadAgro = (RadNumTbOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbOverheadAgro.Text) : 0,
                    AddOverheadAgro = (RadNumTbAddOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbAddOverheadAgro.Text) : 0,
                    IsEmployer = chbEmployer.Checked ? true : false,
                    MaritalStatus = Convert.ToInt32(rbtnMaritalStatus.SelectedItem.Value),
                    GroupID = groupID,
                    OrgID = orgID,
                    OrganizationINN = tbINNOrg.Text
                };
                ItemController ctl = new ItemController();
                int requestID = ctl.ItemRequestAddItem(newRequest);
                /*RequestHistory*//*----------------------------------------------------*/
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = usrID,
                    CreditID = CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow,
                    Status = "Новая заявка",
                    note = tbNote.Text,
                    RequestID = requestID
                };
                ctx.ItemRequestHistoriesAddItem(newItem);
                /*HistoriesStatuses*//*----------------------------------------------------*/
                                     //HistoriesStatuse hisStat = new HistoriesStatuse()
                                     //{
                                     //    CreditID = CreditsHistoriesID,
                                     //    StatusID = 1,
                                     //    StatusDate = dateTimeServer,
                                     //    OperationDate = dateTimeServer,
                                     //    UserID = usrID
                                     //};
                                     //ctx.ItemHistoriesStatuseAddItem(hisStat);
                                     /********Update Salary Customers**********/
                if (rbtnBusiness.SelectedItem.Value == "Работа по найму")
                {
                    int CustID;
                    SysController ctx2 = new SysController();
                    CustID = Convert.ToInt32(hfCustomerID.Value);
                    Customer cust = ctx2.CustomerGetItem(CustID);
                    cust.WorkSalary = (RadNumTbAverageMonthSalary.Text == "") ? Convert.ToDouble(RadNumTbAverageMonthSalary.Text) : 0.00;
                    ctx2.CustomerUpdItem(cust);
                }
                else { }
                /*Products*//*----------------------------------------------------*/


                int reqID = Convert.ToInt32(hfRequestID.Value);
                var RequestProduct = (from v in dbRWZ.RequestsProducts where (v.RequestID == reqID) select v);
                foreach (RequestsProduct rq in RequestProduct)
                {
                    rq.RequestID = requestID;
                }
                dbRWZ.SubmitChanges();
                refreshProducts(1);
                /*****************************/
                dbRWZ.RequestsProductsDels.DeleteAllOnSubmit(from v in dbRWZ.RequestsProductsDels where (v.RequestID == reqID) select v);
                dbRWZ.RequestsProductsDels.Context.SubmitChanges();
                /*****************************/
                /*Files*/
                var RequestFiles = (from v in dbRWZ.RequestsFiles where (v.RequestID == reqID) select v);
                foreach (RequestsFile rf in RequestFiles)
                {
                    rf.RequestID = requestID;
                }
                dbRWZ.SubmitChanges();
                refreshfiles();
                //send
                ////SendMailTo(branchID, "Новая заявка", tbSurname2.Text + " " + tbCustomerName2.Text + " " + tbOtchestvo2.Text, true, true, true);
                /**/
                refreshGrid();
                clearEditControlsRequest();
                hfRequestAction.Value = "";
                pnlNewRequest.Visible = false;
                btnNewRequest.Visible = true;
                if (Convert.ToInt32(Session["RoleID"]) == 1)
                { Response.Redirect("/AgentPage"); }
                if (Convert.ToInt32(Session["RoleID"]) == 2)
                { Response.Redirect("/ExpertBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 3)
                { Response.Redirect("/AgentTour"); }
                if (Convert.ToInt32(Session["RoleID"]) == 4)
                { Response.Redirect("/AdminPage"); }
                if (Convert.ToInt32(Session["RoleID"]) == 5)
                { Response.Redirect("/ExpertBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 6)
                { Response.Redirect("/AgentSvetofor"); }
                if (Convert.ToInt32(Session["RoleID"]) == 7)
                { Response.Redirect("/AdminSvetofor"); }
                if (Convert.ToInt32(Session["RoleID"]) == 8)
                { Response.Redirect("/AgentBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 19)
                { Response.Redirect("/AgentCards"); }
                if (Convert.ToInt32(Session["RoleID"]) == 20)
                { Response.Redirect("/AgentCards"); }
                if (Convert.ToInt32(Session["RoleID"]) == 9)
                { Response.Redirect("/AdminBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 10)
                { Response.Redirect("/ExpertBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 12)
                { Response.Redirect("/NurOnline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 13)
                { Response.Redirect("/AgentTechnodom"); }
                if (Convert.ToInt32(Session["RoleID"]) == 14)
                { Response.Redirect("/AdminTechnodom"); }
                if (Convert.ToInt32(Session["RoleID"]) == 16)
                { Response.Redirect("/AgentPlaneta"); }
                if (Convert.ToInt32(Session["RoleID"]) == 17)
                { Response.Redirect("/AdminPlaneta"); }
                if (Convert.ToInt32(Session["RoleID"]) == 18)
                { Response.Redirect("/BeelineOnline"); }
                enableUpoadFiles();
                /*****************************/
            }
            if (hfRequestAction.Value == "edit")
            {
                DateTime dateTimeServer = dateNowServer;
                DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now);
                /*Edit*/
                int usrID = Convert.ToInt32(Session["UserID"].ToString());

                CreditController ctlCredit = new CreditController();

                decimal commision = 0; string NameOfCredit = "Замат"; int prodID = 1152; //замат
                if (chbEmployer.Checked) prodID = 1153; else prodID = 1152; //замат для сотруд иначе замат
                decimal rate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);
                byte period = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                bool isEmployer = chbEmployer.Checked ? true : false;
                int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                int? GroupCode = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupCode;

                ////if (Convert.ToDecimal(RadNumTbRequestSumm.Text) > 100000) { NameOfCredit = "Потребительский"; prodID = 109; }
                //if ((rate == 0) && (period == 3)) { commision = 0; NameOfCredit = "0-0-3"; prodID = 1163; }
                //if ((rate == 0) && (period == 6)) { commision = 0; NameOfCredit = "0-0-6"; prodID = 1164; }
                ////if ((rate == 0) && (period == 9)) { commision = 0; NameOfCredit = "0-0-9"; prodID = 1165; }
                //if ((rate == 0) && (period == 12)) { commision = 0; NameOfCredit = "0-0-12"; prodID = 1177; }

                if (ddlProduct.SelectedValue == "003") { commision = 0; NameOfCredit = "0-0-3"; prodID = 1163; }
                if (ddlProduct.SelectedValue == "006") { commision = 0; NameOfCredit = "0-0-6"; prodID = 1164; }
                if (ddlProduct.SelectedValue == "006(Honor S7)") { commision = 0; NameOfCredit = "0-0-6 (Honor S7)"; prodID = 1202; }
                //if (ddlProduct.SelectedValue == "006(Honor S7)") { commision = 0; NameOfCredit = "0-0-6 (Honor S7)"; prodID = 1193; }
                if (ddlProduct.SelectedValue == "009") { commision = 0; NameOfCredit = "0-0-9"; prodID = 1165; }
                if (ddlProduct.SelectedValue == "0012") { commision = 0; NameOfCredit = "0-0-12"; prodID = 1177; }
                if (ddlProduct.SelectedValue == "0012(Honor S7)") { commision = 0; NameOfCredit = "0-0-12 (Honor S7)"; prodID = 1201; }
                //if (ddlProduct.SelectedValue == "0012(Honor S7)") { commision = 0; NameOfCredit = "0-0-12 (Honor S7)"; prodID = 1189; }
                if (ddlProduct.SelectedValue == "0018") { commision = 0; NameOfCredit = "0-0-18"; prodID = 1190; }

                if (isEmployer == true) commision = 0;
                string comment = "";
                if (rbtnBusiness.SelectedIndex == 1) { fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 0; comment = txtBusinessComment.Text; }
                if (rbtnBusiness.SelectedIndex == 2) { fexp = (RadNumTbFamilyExpensesAgro.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpensesAgro.Text) : 0; comment = txtAgroComment.Text; }

                /*----------------------------------------------------*/
                History editItemHistory = new History();
                editItemHistory = ctlCredit.GetHistoryByCreditID(Convert.ToInt32(hfCreditID.Value));
                editItemHistory.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                editItemHistory.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value);
                editItemHistory.ProductID = prodID;
                editItemHistory.ApprovedPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                editItemHistory.ApprovedRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value);
                //editItemHistory.PartnerCompanyID = GroupCode;
                ctlCredit.HistoryUpd(editItemHistory);
                /*----------------------------------------------------*/
                PartnersHistory editparthis = new PartnersHistory();
                editparthis = ctlCredit.GetPartnerHistoryByCreditID(Convert.ToInt32(hfCreditID.Value));
                editparthis.SumV = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                ctlCredit.PartnerHistoriesUpdItem(editparthis);
                /*----------------------------------------------------*/
                HistoriesCustomer editItemHistoriesCustomer = new HistoriesCustomer();
                editItemHistoriesCustomer = ctlCredit.GetHistoriesCustomerByCreditID(Convert.ToInt32(hfCreditID.Value));
                editItemHistoriesCustomer.CreditPurpose = tbCreditPurpose.Text;
                editItemHistoriesCustomer.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                editItemHistoriesCustomer.ApprovedSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                editItemHistoriesCustomer.CustomerID = Convert.ToInt32(hfCustomerID.Value);
                ctlCredit.HistoriesCustomerUpd(editItemHistoriesCustomer);
                /*----------------------------------------------------*/
                HistoriesEarlyPaymentComission editItemHistoriesEarlyPaymentComission = new HistoriesEarlyPaymentComission();
                editItemHistoriesEarlyPaymentComission = ctlCredit.GetHistoriesEarlyPaymentComissionByCreditID(Convert.ToInt32(hfCreditID.Value));
                editItemHistoriesEarlyPaymentComission.Period = 1;
                editItemHistoriesEarlyPaymentComission.RequestPartialComissionType = 2; //непонятно
                editItemHistoriesEarlyPaymentComission.RequestFullPaymentComissionType = 2;
                ctlCredit.HistoriesEarlyPaymentComissionUpd(editItemHistoriesEarlyPaymentComission);

                /*----------------------------------------------------*/

                //PartnersHistory parthis = new PartnersHistory();
                //parthis.SumV = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                //ctlCredit.PartnerHistoriesUpd(parthis);

                /*----------------------------------------------------*/
                //HistoriesOfficer editItemHistoriesOfficer = new HistoriesOfficer();
                //editItemHistoriesOfficer = ctlCredit.GetHistoriesOfficerByCreditID(Convert.ToInt32(hfCreditID.Value));
                //editItemHistoriesOfficer.OfficerTypeID = 1; //уточнить
                //editItemHistoriesOfficer.UserID = usrID; ////StartDate = , уточнить 
                //ctlCredit.HistoriesOfficerUpd(editItemHistoriesOfficer);
                /*----------------------------------------------------*/
                string actdate = tbActualDate.Text.Substring(6, 4) + "." + tbActualDate.Text.Substring(3, 2) + "." + tbActualDate.Text.Substring(0, 2);
                //IncomesStructuresActualDate editItemIncomesStructuresActualDate = new IncomesStructuresActualDate();
                //editItemIncomesStructuresActualDate = ctlCredit.GetIncomesStructuresActualDateByCreditID(Convert.ToInt32(hfCreditID.Value));
                //editItemIncomesStructuresActualDate.ActualDate = Convert.ToDateTime(actdate);
                //ctlCredit.IncomesStructuresActualDateUpd(editItemIncomesStructuresActualDate);
                /*----------------------------------------------------*/
                //IncomesStructure editItemIncomesStructures = new IncomesStructure();
                //editItemIncomesStructures = ctlCredit.GetIncomesStructureByCreditID(Convert.ToInt32(hfCreditID.Value));
                //editItemIncomesStructures.ActualDate = Convert.ToDateTime(actdate);
                //ctlCredit.IncomesStructuresUpd(editItemIncomesStructures);
                /*----------------------------------------------------*/
                Request editRequest = new Request();
                ItemController ctlItem = new ItemController();
                editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                editRequest.CreditPurpose = tbCreditPurpose.Text;
                editRequest.CreditProduct = ddlProduct.SelectedValue;
                editRequest.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                editRequest.ProductPrice = Convert.ToDecimal(RadNumTbTotalPrice.Text);
                editRequest.AmountDownPayment = Convert.ToDecimal(RadNumTbAmountOfDownPayment.Text);
                editRequest.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                editRequest.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value);
                //editRequest.RequestGrantComission = Convert.ToDecimal(lblCommission.Text);
                editRequest.ActualDate = Convert.ToDateTime(actdate);
                editRequest.Surname = tbSurname2.Text;
                editRequest.CustomerName = tbCustomerName2.Text;
                editRequest.Otchestvo = tbOtchestvo2.Text;
                editRequest.IdentificationNumber = tbINN2.Text;
                editRequest.OrganizationINN = tbINNOrg.Text;
                editRequest.SumMonthSalary = (RadNumTbSumMonthSalary.Text != "") ? Convert.ToDecimal(RadNumTbSumMonthSalary.Text) : 0;
                editRequest.CountMonthSalary = Convert.ToByte(ddlMonthCount.SelectedValue);
                editRequest.AverageMonthSalary = (RadNumTbSumMonthSalary.Text != "") ? Convert.ToDecimal(RadNumTbSumMonthSalary.Text) / Convert.ToByte(ddlMonthCount.SelectedValue) : 0;
                //editRequest.MonthlyInstallment = (RadNumTbMonthlyInstallment.Text != "") ? Convert.ToDecimal(RadNumTbMonthlyInstallment.Text) : 0;
                //editRequest.Revenue = (RadNumTbRevenue.Text != "") ? Convert.ToDecimal(RadNumTbRevenue.Text) : 0;
                editRequest.MinRevenue = (RadNumTbMinRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMinRevenue.Text) : 0;
                editRequest.MaxRevenue = (RadNumTbMaxRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMaxRevenue.Text) : 0;
                editRequest.Revenue = ((MinRevenue != 0) && (MaxRevenue != 0)) ? (MinRevenue + MaxRevenue) / 2 : MinRevenue + MaxRevenue;
                editRequest.CountWorkDay = Convert.ToByte(ddlCountWorkDay.SelectedValue);
                editRequest.СostPrice = (RadNumTbСostPrice.Text != "") ? Convert.ToDecimal(RadNumTbСostPrice.Text) : 0;
                editRequest.Overhead = (RadNumTbOverhead.Text != "") ? Convert.ToDecimal(RadNumTbOverhead.Text) : 0;
                editRequest.FamilyExpenses = fexp;
                editRequest.OtherLoans = (RadNumOtherLoans.Text != "") ? Convert.ToDecimal(RadNumOtherLoans.Text) : 0;
                editRequest.Bussiness = rbtnBusiness.SelectedIndex;
                editRequest.BusinessComment = comment;
                editRequest.AdditionalIncome = (RadNumTbAdditionalIncome.Text != "") ? Convert.ToDecimal(RadNumTbAdditionalIncome.Text) : 0;
                editRequest.IsEmployer = chbEmployer.Checked ? true : false;
                editRequest.RevenueAgro = (RadNumTbRevenueAgro.Text != "") ? Convert.ToDecimal(RadNumTbRevenueAgro.Text) : 0;
                editRequest.RevenueMilk = (RadNumTbRevenueMilk.Text != "") ? Convert.ToDecimal(RadNumTbRevenueMilk.Text) : 0;
                editRequest.OverheadAgro = (RadNumTbOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbOverheadAgro.Text) : 0;
                editRequest.AddOverheadAgro = (RadNumTbAddOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbAddOverheadAgro.Text) : 0;
                editRequest.MaritalStatus = Convert.ToInt32(rbtnMaritalStatus.SelectedItem.Value);
                editRequest.OrganizationINN = tbINNOrg.Text;
                ctlItem.RequestUpd(editRequest);
                /*****************************/
                /*RequestHistory*//*----------------------------------------------------*/
                CreditController ctx = new CreditController();
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = usrID,
                    CreditID = Convert.ToInt32(hfCreditID.Value),
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow,
                    Status = "Редактирование",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value)
                };
                ctx.ItemRequestHistoriesAddItem(newItem);
                /********Update Salary Customers**********/
                if (rbtnBusiness.SelectedItem.Value == "Работа по найму")
                {
                    int CustID;
                    SysController ctx2 = new SysController();
                    CustID = Convert.ToInt32(hfCustomerID.Value);
                    Customer cust = ctx2.CustomerGetItem(CustID);
                    cust.WorkSalary = Convert.ToDouble(RadNumTbAverageMonthSalary.Text);
                    ctx2.CustomerUpdItem(cust);
                }
                else { }
                /******************************************************/
                refreshGrid();
                clearEditControlsRequest();
                hfRequestAction.Value = "";
                if (Convert.ToInt32(Session["RoleID"]) == 1)
                { Response.Redirect("/AgentPage"); }
                if (Convert.ToInt32(Session["RoleID"]) == 2)
                { Response.Redirect("/ExpertBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 3)
                { Response.Redirect("/AgentTour"); }
                if (Convert.ToInt32(Session["RoleID"]) == 4)
                { Response.Redirect("/AdminPage"); }
                if (Convert.ToInt32(Session["RoleID"]) == 5)
                { Response.Redirect("/ExpertBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 6)
                { Response.Redirect("/AgentSvetofor"); }
                if (Convert.ToInt32(Session["RoleID"]) == 7)
                { Response.Redirect("/AdminSvetofor"); }
                if (Convert.ToInt32(Session["RoleID"]) == 8)
                { Response.Redirect("/AgentBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 9)
                { Response.Redirect("/AdminBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 10)
                { Response.Redirect("/ExpertBeeline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 12)
                { Response.Redirect("/NurOnline"); }
                if (Convert.ToInt32(Session["RoleID"]) == 13)
                { Response.Redirect("/AgentTechnodom"); }
                if (Convert.ToInt32(Session["RoleID"]) == 14)
                { Response.Redirect("/AdminTechnodom"); }
                if (Convert.ToInt32(Session["RoleID"]) == 16)
                { Response.Redirect("/AgentPlaneta"); }
                if (Convert.ToInt32(Session["RoleID"]) == 17)
                { Response.Redirect("/AdminPlaneta"); }

                pnlNewRequest.Visible = false;
            }
        }
        protected void SendMailTo(int? branchID, string strsubj, string fullname, bool boolagent, bool booladmin, bool boolekspert)
        {
            /*****************************/
            //string strsubj;
            string stremailto = "";
            string emailfrom = "";
            string templateAdm = "";
            string templateUsr = "";
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            string domname = DotNetNuke.Common.Globals.GetDomainName(Request);
            try
            {
                // load data from settings...
                if (!string.IsNullOrEmpty((string)Settings["emailfrom"]))
                    emailfrom = (string)Settings["emailfrom"];
                if (!string.IsNullOrEmpty((string)Settings["emailto"]))
                    stremailto = (string)Settings["emailto"];
                if (System.IO.File.Exists(MapPath(ControlPath + "Templates\\TemplateForAdmin_RR.html")))
                    templateAdm = System.IO.File.ReadAllText(MapPath(ControlPath + "Templates\\TemplateForAdmin_RR.html"));
                // template for admin...
                templateAdm = templateAdm.Replace("[CustomerName]", tbCustomerName2.Text);
                templateAdm = templateAdm.Replace("[Otchestvo]", tbOtchestvo2.Text);
                templateAdm = templateAdm.Replace("[Surname]", tbSurname2.Text);
                templateAdm = templateAdm.Replace("[UserName]", Session["UserName"].ToString());
                templateAdm = templateAdm.Replace("[FIO]", Session["FIO"].ToString());
                //send email to admin
                string mailto = "";
                if (boolagent) //отправка сообщ агенту
                {
                    mailto = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().EMail;
                    //SendMail(templateAdm, mailto, emailfrom, "", strsubj);
                }
                foreach (RequestsUsersRole requserrole in dbRWZ.RequestsUsersRoles)
                {
                    if (requserrole.RoleID == 9) //сообщ всем админам о новой заявке
                    {
                        if (booladmin)
                        {
                            mailto = dbRWZ.Users2s.Where(r => r.UserID == requserrole.UserID).FirstOrDefault().EMail;
                            //SendMail(templateAdm, mailto, emailfrom, "", strsubj);
                        }
                    };
                    if (requserrole.RoleID == 2) //сообщ экспертам филиала о новой заявке
                    {
                        if (boolekspert)
                        {
                            int officeIDeks = dbRWZ.Users2s.Where(r => r.UserID == requserrole.UserID).FirstOrDefault().OfficeID;
                            int branchIDeks = dbR.Offices.Where(r => r.ID == officeIDeks).FirstOrDefault().BranchID;
                            if (branchID == branchIDeks)
                            {
                                mailto = dbRWZ.Users2s.Where(r => (r.UserID == requserrole.UserID)).FirstOrDefault().EMail;
                                // SendMail(templateAdm, mailto, emailfrom, "", strsubj);
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.Message + "<br>1.Настройки модуля не определены.", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
            }
            /*****************************/
        }

        protected void SendMail(string body, string to, string efrom, string replyto, string subject)
        {
            try
            {
                if (String.Empty == replyto || replyto.Trim().Length == 0) replyto = "";
                if (efrom == "") efrom = Host.HostEmail;
                efrom = "zamat@doscredobank.kg"; //
                if (to != "")
                {
                    List<Attachment> la = new List<Attachment> { };
                    string strMessage2 = DotNetNuke.Services.Mail.Mail.SendMail(efrom, to, "", "", replyto, MailPriority.Normal, subject, MailFormat.Html, System.Text.Encoding.UTF8, body, la, Host.SMTPServer, Host.SMTPAuthentication, Host.SMTPUsername, Host.SMTPPassword, Host.EnableSMTPSSL);
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "2." + ex.Message, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
            }
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            hfRequestID.Value = id.ToString();


            if (hfRequestsRowID.Value != "")
            {
                GridViewRow gvtold = gvRequests.Rows[Convert.ToInt32(hfRequestsRowID.Value)];
                gvtold.BackColor = System.Drawing.Color.Empty;
            }

            if (e.CommandName == "Sel")
            {
                editcommand(id);
                refreshfiles();
                refreshProducts(2);
                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow gvr = lb.Parent.Parent as GridViewRow;
                //gvr.BackColor = System.Drawing.Color.Empty;
                string hex = "#cbceea";
                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                gvr.BackColor = _color;
                hfRequestsRowID.Value = gvr.RowIndex.ToString();
            }
        }

        public void enableUpoadFiles()
        {
            AsyncUpload1.Enabled = true;
            tbFileDescription.Enabled = true;
            btnUploadFiles.Enabled = true;
        }

        public void disableUpoadFiles()
        {
            AsyncUpload1.Enabled = false;
            tbFileDescription.Enabled = false;
            btnUploadFiles.Enabled = false;
            gvRequestsFiles.DataSource = null;
            gvRequestsFiles.DataBind();
        }

        public void productspriceupdate()
        {
            if (hfRequestAction.Value == "edit")
            {
                Request editRequest = new Request();
                ItemController ctlItem = new ItemController();
                editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                editRequest.ProductPrice = Convert.ToDecimal(RadNumTbTotalPrice.Text);
                editRequest.AmountDownPayment = Convert.ToDecimal(RadNumTbAmountOfDownPayment.Text);
                editRequest.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                ctlItem.RequestUpd(editRequest);
                /*******************/
                CreditController ctlCredit = new CreditController();
                HistoriesCustomer editItemHistoriesCustomer = new HistoriesCustomer();
                editItemHistoriesCustomer = ctlCredit.GetHistoriesCustomerByCreditID(Convert.ToInt32(hfCreditID.Value));
                editItemHistoriesCustomer.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                ctlCredit.HistoriesCustomerUpd(editItemHistoriesCustomer);
            }
        }

        public void refreshfiles()
        {


            /*RequestsFiles*/
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            //var lstRequestFiles = dbRWZ.RequestsFiles.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList();
            var lstRequestFiles = dbRWZ.RequestsFiles.Where(r => (r.RequestID == Convert.ToInt32(hfRequestID.Value) && (r.IsPhoto == false))).ToList();
            if (lstRequestFiles != null)
            {
                gvRequestsFiles.DataSource = lstRequestFiles;
                gvRequestsFiles.DataBind();
            }

            var lstRequestFilesPhoto = dbRWZ.RequestsFiles.Where(r => (r.RequestID == Convert.ToInt32(hfRequestID.Value) && (r.IsPhoto == true))).ToList();
            if (lstRequestFilesPhoto != null)
            {
                gvRequestsFilesPhoto.DataSource = lstRequestFilesPhoto;
                gvRequestsFilesPhoto.DataBind();
            }

            //using (WebClient client = new WebClient())
            //{
            //    string username = "admin";
            //    string password = "{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W";
            //    client.Credentials = new NetworkCredential(username, password);
            //    client.BaseAddress = "http://feeds.itunes.apple.com/feeds/epf/v3/full/current/itunes20110511.tbz.md5";
            //    // client.DownloadFile("http://feeds.itunes.apple.com/feeds/epf/v3/full/current/itunes20110511.tbz.md5", @"C:\folder\file.md5");
            //    LinkButton1.PostBackUrl = client.BaseAddress;
            //}
        }

        public void refreshProducts(int i)
        {
            int amoundp = 10;
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lstRequestsProducts = dbRWZ.RequestsProducts.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList();
            if (lstRequestsProducts.Count > 0)     // != null)
            {
                gvProducts.DataSource = lstRequestsProducts;
                gvProducts.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductID");
                dt.Columns.Add("ProductMark");
                dt.Columns.Add("ProductSerial");
                dt.Columns.Add("ProductImei");
                dt.Columns.Add("ProductImei2");
                dt.Columns.Add("Price");
                dt.Columns.Add("TarifName");
                dt.Columns.Add("PriceTarif");
                dt.Columns.Add("Note");
                dt.Columns.Add("Операции");
                dt.Columns.Add("Выбрать");
                dt.Columns.Add("PriceWithTarif");
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
            var SumRequestProducts = from lst in lstRequestsProducts select lst.Price;
            if (i == 1)
            {
                RadNumTbTotalPrice.Text = SumRequestProducts.Sum().ToString();

                if (ddlRequestRate.Text == "0.00")
                {
                    //if (ddlRequestPeriod.SelectedValue == "3") amoundp = 0;
                    //if (ddlRequestPeriod.SelectedValue == "6") amoundp = 0;
                    //if (ddlRequestPeriod.SelectedValue == "12") amoundp = 0;
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) > 50000) amoundp = 10;
                    else amoundp = 0;
                }
                if (ddlRequestRate.Text == "29.90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) > 50000) amoundp = 10;
                    else amoundp = 0;
                    //amoundp = 10;
                }

                RadNumTbAmountOfDownPayment.Text = (amoundp * Convert.ToDouble(RadNumTbTotalPrice.Text) / 100).ToString();
                RadNumTbRequestSumm.Text = (Convert.ToDouble(RadNumTbTotalPrice.Text) - Convert.ToDouble(RadNumTbAmountOfDownPayment.Text)).ToString();

                double s = Convert.ToDouble(RadNumTbRequestSumm.Text);
                double n = Convert.ToDouble(ddlRequestPeriod.Text);
                double stavka = Convert.ToDouble(ddlRequestRate.SelectedItem.Value);
                double ii = stavka / 12 / 100;
                double k = (((Math.Pow((1 + ii), n)) * (ii)) * s) / ((Math.Pow((1 + (ii)), n)) - 1);
                RadNumTbMonthlyInstallment.Text = Math.Round((k), 0).ToString();
            }
        }

        public void history(int id)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var history = from his in dbRWZ.RequestsHistories
                          where (his.RequestID == id)
                          join usr in dbRWZ.Users2s on his.AgentID equals usr.UserID
                          orderby his.StatusDate
                          select new
                          {
                              Status = his.Status,
                              Date = his.StatusDate,
                              Note = his.note,
                              FIO = usr.UserName
                          };
            gvHistory.DataSource = history;
            gvHistory.DataBind();
        }
        public void editcommand(int id)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);



            try
            {
                pnlNewRequest.Visible = true;
                lblAccount.Text = "";
                var lst = dbRWZ.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                var account = dbR.CreditsAccounts.Where(a => ((a.CreditID == lst.CreditID) && (a.TypeID == 3))).FirstOrDefault();
                if (account != null) lblAccount.Text = account.AccountNo.ToString();
                tbCreditPurpose.Text = lst.CreditPurpose;

                /**/
                int groupID = Convert.ToInt32(lst.GroupID);
                int orgID = Convert.ToInt32(lst.OrgID);
                databindDDLProduct(orgID, groupID);

                /***/


                ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByValue(lst.CreditProduct.ToString()));
                ddlProductIndChg();
                ddlRequestRate.SelectedIndex = ddlRequestRate.Items.IndexOf(ddlRequestRate.Items.FindByValue(lst.RequestRate.ToString()));
                ddlRequestPeriod.SelectedIndex = ddlRequestPeriod.Items.IndexOf(ddlRequestPeriod.Items.FindByValue(lst.RequestPeriod.ToString()));

                RadNumTbTotalPrice.Text = lst.ProductPrice.ToString();
                RadNumTbRequestSumm.Text = lst.RequestSumm.ToString();
                RadNumOtherLoans.Text = lst.OtherLoans.ToString();
                RadNumTbAdditionalIncome.Text = lst.AdditionalIncome.ToString();
                txtBusinessComment.Text = lst.BusinessComment;

                lblCommission.Text = lst.RequestGrantComission.ToString();
                tbActualDate.Text = Convert.ToDateTime(lst.ActualDate).ToString("dd.MM.yyyy");
                RadNumTbMonthlyInstallment.Text = lst.MonthlyInstallment.ToString();
                RadNumTbAmountOfDownPayment.Text = lst.AmountDownPayment.ToString();
                hfCustomerID.Value = lst.CustomerID.ToString();
                hfCreditID.Value = lst.CreditID.ToString();
                hfRequestID.Value = id.ToString();
                tbSurname2.Text = lst.Surname;
                tbCustomerName2.Text = lst.CustomerName;
                tbOtchestvo2.Text = lst.Otchestvo;
                tbINN2.Text = lst.IdentificationNumber;
                tbINNOrg.Text = lst.OrganizationINN;
                chbEmployer.Checked = lst.IsEmployer;
                if (lst.MaritalStatus == 0) { rbtnMaritalStatus.SelectedIndex = 0; }
                else { rbtnMaritalStatus.SelectedIndex = 1; }
                if (chbEmployer.Checked)
                { pnlEmployment.Visible = false; }
                else { pnlEmployment.Visible = true; }
                lblStatusRequest.Text = "&nbsp;&nbsp;" + lst.RequestStatus + "&nbsp;&nbsp; ";
                string hexRed = "#E47E11", hexOrange = "#8C5E40", hexYellow = "#fdf404", hexGreen = "#7cfa84", hexBlue = "#227128", hexBlack = "#878787", hexReceive = "#11ACE4";
                Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
                Color _colorOrange = System.Drawing.ColorTranslator.FromHtml(hexOrange);
                Color _colorYellow = System.Drawing.ColorTranslator.FromHtml(hexYellow);
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                Color _colorBlue = System.Drawing.ColorTranslator.FromHtml(hexBlue);
                Color _colorBlack = System.Drawing.ColorTranslator.FromHtml(hexBlack);
                Color _colorReceive = System.Drawing.ColorTranslator.FromHtml(hexReceive);
                if (lst.RequestStatus == "Отменено") { lblStatusRequest.BackColor = _colorBlack; hfRequestStatus.Value = "Отменено"; };
                if (lst.RequestStatus == "Отказано") { lblStatusRequest.BackColor = _colorBlack; hfRequestStatus.Value = "Отказано"; };
                if (lst.RequestStatus == "Новая заявка") { lblStatusRequest.BackColor = _colorRed; hfRequestStatus.Value = "Новая заявка"; };
                if (lst.RequestStatus == "В обработке") { lblStatusRequest.BackColor = _colorRed; hfRequestStatus.Value = "В обработке"; };
                if (lst.RequestStatus == "Исправлено") { lblStatusRequest.BackColor = _colorOrange; hfRequestStatus.Value = "Исправлено"; };
                if (lst.RequestStatus == "Не подтверждено") { lblStatusRequest.BackColor = _colorOrange; hfRequestStatus.Value = "Не подтверждено"; };
                if (lst.RequestStatus == "Подтверждено") { lblStatusRequest.BackColor = _colorYellow; hfRequestStatus.Value = "Подтверждено"; };
                if (lst.RequestStatus == "Утверждено") { lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "Утверждено"; };
                if (lst.RequestStatus == "К подписи") { lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "К подписи"; };
                if (lst.RequestStatus == "На выдаче") { lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "На выдаче"; };
                if (lst.RequestStatus == "Выдано") { lblStatusRequest.BackColor = _colorBlue; hfRequestStatus.Value = "Выдан"; };
                if (lst.RequestStatus == "Принято") { lblStatusRequest.BackColor = _colorReceive; hfRequestStatus.Value = "Принято"; };
                lblStatusRequest.ForeColor = System.Drawing.Color.Black;
                hfRequestAction.Value = "edit";
                enableUpoadFiles();
                /**************************/
                if (lst.Bussiness == 0)
                {
                    rbtnBusiness.SelectedIndex = 0;
                    pnlBusiness.Visible = false;
                    pnlAgro.Visible = false;
                    RadNumTbAverageMonthSalary.Text = lst.AverageMonthSalary.ToString();
                    RadNumTbSumMonthSalary.Text = lst.SumMonthSalary.ToString();
                    ddlMonthCount.SelectedIndex = ddlMonthCount.Items.IndexOf(ddlMonthCount.Items.FindByValue(lst.CountMonthSalary.ToString()));
                }
                if (lst.Bussiness == 1)
                {
                    rbtnBusiness.SelectedIndex = 1;
                    pnlBusiness.Visible = true;
                    pnlEmployment.Visible = false;
                    pnlAgro.Visible = false;
                    //RadNumTbRevenue.Text = lst.Revenue.ToString();
                    RadNumTbMaxRevenue.Text = lst.MaxRevenue.ToString();
                    RadNumTbMinRevenue.Text = lst.MinRevenue.ToString();
                    ddlCountWorkDay.SelectedIndex = ddlCountWorkDay.Items.IndexOf(ddlCountWorkDay.Items.FindByValue(lst.CountWorkDay.ToString()));
                    RadNumTbСostPrice.Text = lst.СostPrice.ToString();
                    RadNumTbOverhead.Text = lst.Overhead.ToString();
                    RadNumTbFamilyExpenses.Text = lst.FamilyExpenses.ToString();
                }
                if (lst.Bussiness == 2)
                {
                    rbtnBusiness.SelectedIndex = 2;
                    pnlBusiness.Visible = false;
                    pnlEmployment.Visible = false;
                    pnlAgro.Visible = true;
                    //RadNumTbRevenue.Text = lst.Revenue.ToString();
                    RadNumTbRevenueAgro.Text = lst.RevenueAgro.ToString();
                    RadNumTbRevenueMilk.Text = lst.RevenueMilk.ToString();
                    RadNumTbOverheadAgro.Text = lst.OverheadAgro.ToString();
                    RadNumTbAddOverheadAgro.Text = lst.AddOverheadAgro.ToString();
                    RadNumTbFamilyExpensesAgro.Text = lst.FamilyExpenses.ToString();
                }
                /***********************/
                history(id);
                btnCustomerSearch.Enabled = false;
                btnCloseRequest.Visible = true; //закры форму заявки
                btnCalculator.Visible = true; //калькулятор
                btnNewRequest.Visible = true; //новая заявка
                btnProffer.Visible = false; //предложение зп
                btnNoConfirm.Visible = false; //статус не подтвержден
                btnFixed.Visible = false; //статус исправлено
                btnFix.Visible = false; //статус исправить
                btnConfirm.Visible = false; //статус подтвердить
                btnIssue.Visible = false; //статус выдать
                btnCancelReq.Visible = false; //статус отменить
                btnCancelReqExp.Visible = false; //статус отказать
                btnReceived.Visible = false; //статус принято
                btnSendCreditRequest.Visible = false; //Сохранить
                pnlEmployment.Visible = false; // зп
                btnAgreement.Visible = false; //договор
                chbEmployer.Enabled = false; //Сотрудник
                btnInProcess.Visible = false; //
                btnApproved.Visible = false; //
                btnSignature.Visible = false;




                if ((Convert.ToInt32(Session["RoleID"]) == 2) || (Convert.ToInt32(Session["RoleID"]) == 5)) //Эксперты Доскредо)
                {
                    tbINNOrg.Visible = true;
                    lblOrgINN.Visible = true;
                    var blackListCust = dbRWZ.BlackLists.Where(r => (r.IdentificationNo == tbINN2.Text) && (r.CustomerTypeID == 1)).ToList();
                    if (blackListCust.Count > 0)
                    {
                        gvBlackListCustomers.DataSource = blackListCust;
                        pnlBlackList.Visible = true; //pnlBlackListOrg.Visible = true; 
                        //BlackListShow();

                    }
                    else
                    {
                        gvBlackListCustomers.DataSource = null;
                        pnlBlackList.Visible = false; ///pnlBlackListOrg.Visible = false;
                    }
                    gvBlackListCustomers.DataBind();

                    var blackListOrg = dbRWZ.BlackLists.Where(r => (r.IdentificationNo == tbINNOrg.Text) && (r.CustomerTypeID == 2)).ToList();
                    if (blackListOrg.Count > 0)
                    {
                        gvBlackListOrg.DataSource = blackListOrg;
                        pnlBlackListOrg.Visible = true;
                        //lblBlacListOrg.Visible = true;
                        //BlackListShow();
                    }
                    else
                    {
                        pnlBlackListOrg.Visible = false;
                        gvBlackListOrg.DataSource = null;
                        //lblBlacListOrg.Visible = false;
                    }
                    gvBlackListOrg.DataBind();


                }
                else
                {
                    tbINNOrg.Visible = false;
                    lblOrgINN.Visible = false;
                    pnlBlackList.Visible = false;
                    pnlBlackListOrg.Visible = false;
                    //BlackListHide();
                }

                if (lst.GroupID == 110) { btnProfileNano.Visible = true; }
                else { btnProfileNano.Visible = false; }

                /**********************************************/
                if (lst.RequestStatus == "Новая заявка")
                {
                    gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    chbEmployer.Enabled = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnCancelReq.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 9) { btnCancelReq.Visible = true; btnNoConfirm.Visible = true; btnConfirm.Visible = true; btnInProcess.Visible = true; } //Админы Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) { btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = true; btnApproved.Visible = true; btnFix.Visible = true; } //Эксперты
                    if (Convert.ToInt32(Session["RoleID"]) == 5) { btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = false; btnApproved.Visible = true; btnFix.Visible = true; } //Эксперты ГБ
                }
                if (lst.RequestStatus == "В обработке")
                {
                    gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnCancelReq.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        btnCancelReqExp.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = false; btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; btnConfirm.Visible = true; btnInProcess.Visible = true;
                    }
                }

                if (lst.RequestStatus == "Исправить")
                {
                    gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnFixed.Visible = true; //Исправлено
                    }

                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFixed.Visible = true; //Исправлено

                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        btnCancelReqExp.Visible = true;
                        btnProffer.Visible = true;
                        btnConfirm.Visible = false;
                        btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; btnConfirm.Visible = true; btnInProcess.Visible = true;
                    }
                }

                if (lst.RequestStatus == "Исправлено")
                {
                    gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnCancelReq.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = false; btnFix.Visible = true; btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = false; btnApproved.Visible = true; btnFix.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; btnConfirm.Visible = true; btnInProcess.Visible = true;
                    }
                }
                if (lst.RequestStatus == "Не подтверждено")
                {
                    gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnFixed.Visible = true; btnCancelReq.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) { btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; } //Эксперты 
                    if (Convert.ToInt32(Session["RoleID"]) == 5) { btnCancelReqExp.Visible = true; btnApproved.Visible = true; } //Эксперты ГБ
                    if (Convert.ToInt32(Session["RoleID"]) == 9) { btnConfirm.Visible = true; btnCancelReq.Visible = true; btnInProcess.Visible = true; btnApproved.Visible = true; } //Админы Билайн
                }
                if (lst.RequestStatus == "Отменено")
                {
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                }
                if (lst.RequestStatus == "Отказано")
                {
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                }
                if (lst.RequestStatus == "Подтверждено")
                {
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    { }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        gvProducts.Enabled = true;
                        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                        btnSendCreditRequest.Enabled = true; btnNoConfirm.Visible = true; btnCancelReq.Visible = true; btnSendCreditRequest.Visible = true;
                    }
                }
                if (lst.RequestStatus == "Утверждено")
                {
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnSignature.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        gvProducts.Enabled = true;
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnProffer.Visible = true;
                        btnSendCreditRequest.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        btnSendCreditRequest.Visible = true; btnProffer.Visible = true;
                        btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }

                if (lst.RequestStatus == "К подписи")
                {
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        gvProducts.Enabled = true;
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnProffer.Visible = true; //btnCancelReqExp.Visible = true;
                        btnSendCreditRequest.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }
                if (lst.RequestStatus == "На выдаче")
                {
                    gvProducts.Enabled = false;
                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnIssue.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        gvProducts.Enabled = true;
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnProffer.Visible = true; //btnCancelReqExp.Visible = true;
                        btnSendCreditRequest.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        btnSendCreditRequest.Visible = true; btnSendCreditRequest.Visible = true; btnProffer.Visible = true;
                        btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }

                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }
                if (lst.RequestStatus == "Выдано")
                {
                    gvProducts.Enabled = false;
                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        btnProffer.Visible = true; btnReceived.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        btnProffer.Visible = true; btnReceived.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }
                if (lst.RequestStatus == "Принято")
                {
                    gvProducts.Enabled = false;
                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Доскредо
                    {
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        btnProffer.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Доскредо
                    {
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        btnProffer.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Error: " + ex.Message + "<hr>" + ex.Source + "<hr>" + ex.StackTrace, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
            }
        }


        //public void BlackListShow()
        //{
        //    pnlBlackList.Visible = true; pnlBlackListOrg.Visible = true;
        //    tbINNOrg.Visible = true; lblOrgINN.Visible = true;
        //    RequiredFieldValidator5.Visible = true;
        //    RegularExpressionValidator14.Visible = true;
        //    lblBlacListCust.Visible = true;
        //}

        public void BlackListHide()
        {
            pnlBlackList.Visible = false; pnlBlackListOrg.Visible = false;
            tbINNOrg.Visible = false; lblOrgINN.Visible = false;
            //RequiredFieldValidator5.Visible = false;
            RegularExpressionValidator14.Visible = false;
            lblBlacListCust.Visible = false;
        }


        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            hfCustomerID.Value = "noselect";
            btnCredit.Text = "Выбрать клиента";
            tbSurname2.Text = "";
            tbCustomerName2.Text = "";
            tbOtchestvo2.Text = "";
            tbINN2.Text = "";
            tbINNOrg.Text = "";
            clearEditControls();
            lblMessageClient.Text = "";
            tbSearchINN.Text = "";
            tbContactPhone.Text = "";
            btnSaveCustomer.Enabled = true;
            tbSerialN.Text = "";
        }

        protected void btnGuarantorSearch_Click(object sender, EventArgs e)
        {
        }

        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            byte[] gb = Guid.NewGuid().ToByteArray();
            int i = BitConverter.ToInt32(gb, 0);
            if (i > 0) { i = i * (-1); }
            /*удаляем продукты без заявок*/
            //var prodDels = (from t in db.RequestsProductsDels select t.RequestID);
            //var prod = (from r in db.RequestsProducts where( (!prodDels.Contains(r.RequestID)) || (r.RequestID<0)) select r).ToList();
            //foreach (RequestsProduct pr in prod)
            //{
            //    db.RequestsProducts.DeleteOnSubmit(pr);
            //}
            /**/
            /*врем таблица*/
            hfRequestID.Value = i.ToString();
            dateNowServer = dbR.GetTable<SysInfo>().FirstOrDefault().DateOD;
            RequestsProductsDel reqprdel = new RequestsProductsDel()
            {
                RequestID = i,
                RequestDate = dateNowServer
            };
            ItemController itx = new ItemController();
            itx.ItemProductsDelAddItem(reqprdel);
            /**/
            btnAddProductClick = 1; clearEditControls();
            clearEditControlsRequest();
            hfRequestAction.Value = "new";
            hfRequestStatus.Value = "";
            btnSendCreditRequest.Enabled = true;
            pnlCalculator.Visible = false;
            btnAgreement.Visible = false;
            btnPledgeAgreement.Visible = false;
            btnProffer.Visible = false;
            btnReceptionAct.Visible = false;
            btnIssue.Visible = false;
            pnlNewRequest.Visible = true;
            btnCloseRequest.Visible = true;
            btnCalculator.Visible = true;
            btnSendCreditRequest.Visible = true;
            chbEmployer.Checked = false;
            btnNewRequest.Visible = false;
            disableUpoadFiles();
            /**/
            /**/
            int reqIDDel = Convert.ToInt32(hfRequestIDDel.Value);
            var RequestProduct = (from v in dbRWZ.RequestsProducts where (v.RequestID == reqIDDel) select v);
            foreach (RequestsProduct rq in RequestProduct)
            {
                dbRWZ.RequestsProducts.DeleteOnSubmit(rq);
            }
            try
            {
                dbRWZ.SubmitChanges();
            }
            catch (Exception)
            {

            }
            /**/
            var RequestFiles = (from v in dbRWZ.RequestsFiles where (v.RequestID == reqIDDel) select v);
            foreach (RequestsFile rf in RequestFiles)
            {
                dbRWZ.RequestsFiles.DeleteOnSubmit(rf);
            }
            try
            {
                dbRWZ.SubmitChanges();
            }
            catch (Exception)
            {
            }
            /**/
            hfRequestIDDel.Value = hfRequestID.Value;
            refreshProducts(1);
            refreshfiles();
            btnCustomerEdit.Enabled = false;
            gvHistory.DataSource = null;
            gvHistory.DataBind();
            gvProducts.Enabled = true;

            //pnlBlackList.Visible = false;
            //pnlBlackListOrg.Visible = false;
            BlackListHide();
        }

        public void clearEditControlsRequest()
        {
            tbCreditPurpose.Text = "";
            RadNumTbTotalPrice.Text = "";
            RadNumTbAmountOfDownPayment.Text = "";
            RadNumTbRequestSumm.Text = "";
            tbActualDate.Text = "";
            tbSurname2.Text = "";
            tbCustomerName2.Text = "";
            tbOtchestvo2.Text = "";
            tbINN2.Text = "";
            tbINNOrg.Text = "";
            RadNumTbAverageMonthSalary.Text = "";
            //RadNumTbRevenue.Text = "";
            RadNumTbMinRevenue.Text = "";
            RadNumTbMaxRevenue.Text = "";
            ddlCountWorkDay.SelectedIndex = 0;
            RadNumTbСostPrice.Text = "";
            RadNumTbOverhead.Text = "";
            RadNumTbFamilyExpenses.Text = "";
            RadNumTbAdditionalIncome.Text = "";
            txtBusinessComment.Text = "";
            rbtnBusiness.SelectedIndex = 0;
            pnlEmployment.Visible = true;
            pnlBusiness.Visible = false;
            pnlAgro.Visible = false;
            hfCustomerID.Value = "noselect";
            hfGuarantorID.Value = "noselect";
            btnCustomerSearch.Enabled = true;
        }

        protected void btnSearchRequest_Click(object sender, EventArgs e)
        {
            hfRequestsRowID.Value = "";
            clearEditControlsRequest();
            pnlNewRequest.Visible = false;
            //refreshGrid();
        }

        protected void gvRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnAgreement_Click(object sender, EventArgs e)
        {
            string worktype = "0";
            if (rbtnBusiness.SelectedIndex == 0) worktype = "0";
            if (rbtnBusiness.SelectedIndex == 1) worktype = "1";
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["UserID"] = hfUserID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Session["WorkType"] = worktype;
            //Response.Redirect(this.EditUrl("", "", "rptAgrees"));
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.GroupID == 110)
            {
                Response.Redirect(this.EditUrl("", "", "rptAgreesNano"));
            }

            if (ddlRequestRate.SelectedValue == "0.00")
            {
                Response.Redirect(this.EditUrl("", "", "rptAgreesInst"));
            }
            else
            {
                Response.Redirect(this.EditUrl("", "", "rptAgrees"));
            }
        }

        protected void rbtnBusiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnBusiness.SelectedIndex == 0) { pnlEmployment.Visible = true; pnlBusiness.Visible = false; pnlAgro.Visible = false; }
            if (rbtnBusiness.SelectedIndex == 1) { pnlBusiness.Visible = true; pnlEmployment.Visible = false; pnlAgro.Visible = false; }
            if (rbtnBusiness.SelectedIndex == 2) { pnlAgro.Visible = true; pnlBusiness.Visible = false; pnlEmployment.Visible = false; }
        }

        protected void gvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            Color redColor = Color.FromArgb(255, 0, 0);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string hexRed = "#E47E11", hexOrange = "#8C5E40", hexYellow = "#fdf404", hexGreen = "#7cfa84", hexBlue = "#227128", hexBlack = "#878787", hexReceive = "#11ACE4";
                Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
                Color _colorOrange = System.Drawing.ColorTranslator.FromHtml(hexOrange);
                Color _colorYellow = System.Drawing.ColorTranslator.FromHtml(hexYellow);
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                Color _colorBlue = System.Drawing.ColorTranslator.FromHtml(hexBlue);
                Color _colorBlack = System.Drawing.ColorTranslator.FromHtml(hexBlack);
                Color _colorReceive = System.Drawing.ColorTranslator.FromHtml(hexReceive);
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Отменено") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorBlack; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Отказано") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorBlack; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Новая заявка") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorRed; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("В обработке") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorRed; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Исправлено") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorRed; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Не подтверждено") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorOrange; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Подтверждено") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorYellow; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Утверждено") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorGreen; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("К подписи") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorGreen; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("На выдаче") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorGreen; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Выдано") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorBlue; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Принято") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorReceive; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Исправить") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorOrange; }
            }
            pnlMenuRequest.Visible = true;
        }

        protected void Upload(object sender, EventArgs e)
        {
        }

        protected void DeleteFile(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.RequestsFiles.DeleteAllOnSubmit(from v in dbRWZ.RequestsFiles where (v.ID == id) select v);
            dbRWZ.RequestsFiles.Context.SubmitChanges();
            refreshfiles();
        }

        protected void DeleteProduct(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.RequestsProducts.DeleteAllOnSubmit(from v in dbRWZ.RequestsProducts where (v.ProductID == id) select v);
            dbRWZ.RequestsProducts.Context.SubmitChanges();
            refreshProducts(1);
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            string constr = connectionStringR;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name, Data, ContentType from RequestsFiles where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Data"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["Name"].ToString();
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }

        protected string filedir = "Beecredits";

        protected void btnUploadFiles_Click(object sender, EventArgs e)
        {
            string filename = "", fullfilename = "";
            tbFileDescription.Text = "";
            Telerik.Web.UI.UploadedFile file = AsyncUpload1.UploadedFiles[0];
            filename = file.FileName;

            if (file.FileName != null)
            {
                fullfilename = UploadImageAndSave(true, file.FileName);

                string contentType = AsyncUpload1.UploadedFiles[0].ContentType;
                {
                    {
                        RequestsFile newRequestFile = new RequestsFile
                        {
                            Name = filename,
                            RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
                            ContentType = contentType,
                            //Data = bytes,
                            FullName = PortalSettings.HomeDirectory + filedir + "\\" + fullfilename,
                            FileDescription = tbFileDescription.Text + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                            IsPhoto = false
                        };
                        ItemController ctl = new ItemController();
                        ctl.ItemRequestFilesAddItem(newRequestFile);
                    }
                }
                System.Threading.Thread.Sleep(1000);
                refreshfiles();
            }
        }

        protected string UploadImageAndSave(bool hasfile, string filename) //main function
        {
            if (hasfile)
            {
                CheckImageDirs();

                string filepath = PortalSettings.HomeDirectoryMapPath + filedir + "\\" + filename;
                int temp_ext = 0;
                while (System.IO.File.Exists(filepath))
                {
                    temp_ext = DateTime.Now.Millisecond;
                    string ext_name = System.IO.Path.GetExtension(filepath);
                    string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + temp_ext;
                    filename = filename_no_ext + temp_ext + ext_name;
                    filepath = PortalSettings.HomeDirectoryMapPath + filedir + "\\" + filename;
                }
                string path = System.IO.Path.GetFileName(filename);
                AsyncUpload1.UploadedFiles[0].SaveAs(filepath);
            }
            return filename;
        }

        protected void CheckImageDirs()
        {
            if (!System.IO.Directory.Exists(PortalSettings.HomeDirectoryMapPath + filedir))
                System.IO.Directory.CreateDirectory(PortalSettings.HomeDirectoryMapPath + filedir);

            if (!System.IO.Directory.Exists(PortalSettings.HomeDirectoryMapPath + filedir))
                System.IO.Directory.CreateDirectory(PortalSettings.HomeDirectoryMapPath + filedir);
        }

        protected void btnCloseRequest_Click(object sender, EventArgs e)
        {
            pnlNewRequest.Visible = false;
            btnCloseRequest.Visible = false;
            btnSendCreditRequest.Visible = false;
            btnAgreement.Visible = false;
            btnPledgeAgreement.Visible = false;
            btnProffer.Visible = false;
            btnReceptionAct.Visible = false;
            btnIssue.Visible = false;
            btnNewRequest.Visible = true;

            btnNoConfirm.Visible = false; //статус не подтвержден

            btnConfirm.Visible = false; //статус подтвердить
            btnIssue.Visible = false; //статус выдать
            btnCancelReq.Visible = false; //статус отменить
            btnCancelReqExp.Visible = false; //статус отказать
            btnReceived.Visible = false; //статус принято
            btnFixed.Visible = false; //статус исправлено
        }

        protected void btnCalculatorClose_Click(object sender, EventArgs e)
        {
            pnlCalculator.Visible = false;
        }



        protected async void btnIssue_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.RequestStatus == "На выдаче")
            {
                if (lst.GroupID == 110)
                {
                    //********меняем статус в ОБ ***Begin**/
                    //string strOB = await IssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                    //********меняем статус в ОБ ***End**/

                    //********меняем статус в ОБ выдано*****//
                    string strOB1 = "", strNur = "";
                    var statusOB = dbR.HistoriesStatuses.Where(h => ((h.CreditID == lst.CreditID) && (h.StatusID == 5))).ToList();
                    if (statusOB.Count > 0)
                    {
                        strOB1 = "200";
                    }
                    else
                    {
                        try
                        {
                            //strOB = await AtIssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                            strOB1 = await IssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                        }
                        catch (Exception ex)
                        {
                            //TextBox1.Text = TextBox1.Text + ex.Message;
                            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB1 + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        }
                        finally
                        {
                            ////TextBox1.Text = TextBox1.Text + Response.ToString();

                        }
                        if (strOB1 == "200")
                        {
                            /*RequestHistory*/
                            //var reqs = dbW.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList().FirstOrDefault();
                            //reqs.RequestStatus = "На выдаче";
                            //dbW.Requests.Context.SubmitChanges();
                            //SignatureRequest();
                            //AtIssueRequest();
                        }
                        else
                        {
                            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса На выдаче в АБС" + strOB1, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        }
                    }
                    /**********************************/





                    //********отправляем статус в нуртелеком ****Begin ***/
                    strNur = await SendStatusBee(lst.CreditID.ToString(), "ISSUED_BY");
                    if (strNur == "200")
                    {

                    }
                    else
                    {
                        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса ISSUED_BY в Билайн", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    }
                    //********отправляем статус в нуртелеком ****End ***/

                    if ((strNur == "200") && (strOB1 == "200"))
                    {
                        IssueRequest();
                    }

                }
                else
                {
                    //********меняем статус в скоринге если не нано
                    IssueRequest();
                }
            }




            //Отправка сообщ для МТ-Онлайн {
            string body = "", to = "rtentiev@doscredobank.kg", efrom = "", replyto = "", subject = "Выдано", strOB = "200";
            if (lst != null)
            {
                body = "Заявка№: " + hfRequestID.Value + ", ФИО клиента: " + lst.Surname + " " + lst.CustomerName + " " + lst.Otchestvo;
            }
            if (lst.OrgID == 7) // МТ 
            {
                //SendMail2(body, to, efrom, replyto, subject);
                try
                {
                    int reqID = dbRWZ.Requests2s.Where(r => r.RequestID2 == lst.RequestID).FirstOrDefault().RequestID;
                    strOB = await SendStatusMT(reqID.ToString(), "Выдано");
                }
                catch (Exception ex)
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                }
                finally
                {
                }
            }
            //Отправка сообщ для МТ-Онлайн }
        }

        public async System.Threading.Tasks.Task<string> IssueRequestOB(string CustomerID, string CreditID)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://10.64.17.85/");
                    client.BaseAddress = new Uri("https://" + connectionStringOBAPIAddress + "/");
                    string json = "";
                    //var response = await client.PostAsync("https://10.64.17.85/OnlineBank.IntegrationService/api/Loans/IssueLoanRequest?customerID=" + CustomerID + "&creditID=" + CreditID, new StringContent(json, Encoding.UTF8, "application/json"));
                    var response = await client.PostAsync("https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/IssueLoanRequest?customerID=" + CustomerID + "&creditID=" + CreditID, new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    string result3 = "res:" + result;
                    //TextBox2.Text = result.ToString();
                    //reslt res = JsonConvert.DeserializeObject<reslt>(json);
                    //JObject jResults = JObject.Parse(result);
                    ///var res = JsonConvert.DeserializeObject(json);

                    string state = getstat2(result);

                    if ((response.StatusCode == HttpStatusCode.OK) && (state == "0"))
                    {
                        result = "200";
                    }
                    if (result != "200")
                    {
                        result = result3;
                        //var statusCode = data["statusCode"];
                        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, result, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                return ex.ToString();
            }
            finally
            {
                //TextBox1.Text = TextBox1.Text + Response.ToString();
                //return result;

            }
        }

        public class structgetstat2
        {
            public int State { get; set; }
            public string Message { get; set; }
            public string MessageCode { get; set; }
        }

        public string getstat2(string result)
        {
            try
            {

                structgetstat2 dez = JsonConvert.DeserializeObject<structgetstat2>(result.ToString());
                return dez.State.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public void IssueRequest()
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            var lst5 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Выдано";
                dbRWZ.Requests.Context.SubmitChanges();
                System.Threading.Thread.Sleep(1000);
                refreshGrid();
                string hexBlue = "#227128";
                Color _colorBlue = System.Drawing.ColorTranslator.FromHtml(hexBlue);
                lblStatusRequest.BackColor = _colorBlue; hfRequestStatus.Value = "Выдано";
                /*RequestHistory*/
                DateTime dateTimeServer = dateNow;
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    CreditID = Convert.ToInt32(hfCreditID.Value),
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeServer, //Convert.ToDateTime(DateTime.Now),
                    Status = "Выдано",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value)
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
            }
        }


        public static decimal tarif(int ddlrequestperiod, string ddltarifname)
        {
            decimal pr = 0;
            if (ddlrequestperiod < 7)
            {
                switch (ddltarifname)
                {
                    case "«Переходи на О!+ (-50%)»": pr = 827.5M; break;
                    case "«Переходи на О! Light+ (-50%)»": pr = 402.5M; break;
                    case "«Переходи на О! Light+ TV Базовый (-50%)»": pr = 607.5M; break;
                    case "«Переходи на О! Безлимит+ TV Стандарт+ (-50%)»": pr = 982.5M; break;
                    case "«Переходи на О! Юг + (-50%)»": pr = 180M; break;
                    case "0": pr = 0M; break;
                    default: break;
                }
            }
            else
            {
                switch (ddltarifname)
                {
                    case "«Переходи на О!+ (-50%)»": pr = 1317.5M; break;
                    case "«Переходи на О! Light+ (-50%)»": pr = 667.5M; break;
                    case "«Переходи на О! Light+ TV Базовый (-50%)»": pr = 927.5M; break;
                    case "«Переходи на О! Безлимит+ TV Стандарт+ (-50%)»": pr = 1352.5M; break;
                    case "«Переходи на О! Юг + (-50%)»": pr = 230M; break;
                    case "«Для сотрудников»": pr = 0M; break;
                    default: break;
                }
            }
            return pr;
        }

        protected void AddNewProduct(object sender, EventArgs e)
        {
            TextBox ProductMark = ((TextBox)gvProducts.FooterRow.FindControl("txtProductMark"));
            TextBox ProductSerial = ((TextBox)gvProducts.FooterRow.FindControl("txtProductSerial"));
            TextBox ProductImei = ((TextBox)gvProducts.FooterRow.FindControl("txtProductImei"));
            TextBox ProductImei2 = ((TextBox)gvProducts.FooterRow.FindControl("txtProductImei2"));
            Telerik.Web.UI.RadNumericTextBox Price = ((Telerik.Web.UI.RadNumericTextBox)gvProducts.FooterRow.FindControl("txtPrice"));

            TextBox Note = ((TextBox)gvProducts.FooterRow.FindControl("txtNote"));
            if ((hfRequestStatus.Value != "Выдано") && (hfRequestStatus.Value != "На выдаче") && (hfRequestStatus.Value != "Отменено") && (hfRequestStatus.Value != "Отказано") && (hfRequestStatus.Value != "Подтверждено") && (hfRequestStatus.Value != "Принято") && (hfRequestStatus.Value != "Утверждено") && (hfRequestStatus.Value != "К подписи"))
            {
                ItemController ctl = new ItemController();
                /*Тариф*/

                RequestsProduct newRequestProduct = new RequestsProduct
                {
                    RequestID = Convert.ToInt32(hfRequestID.Value),
                    ProductMark = ProductMark.Text,
                    ProductSerial = ProductSerial.Text,
                    ProductImei = ProductImei.Text,
                    ProductImei2 = ProductImei2.Text,
                    Note = Note.Text,
                    Price = Convert.ToDecimal(Price.Text),
                    PriceWithTarif = Convert.ToDecimal(Price.Text),
                };
                ctl.ItemRequestProductAddItem(newRequestProduct);
                System.Threading.Thread.Sleep(1000);
                refreshProducts(1);
            }
            productspriceupdate();
            refreshGrid();
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if ((hfRequestStatus.Value != "Выдано") && (hfRequestStatus.Value != "На выдаче") && (hfRequestStatus.Value != "Отменено") && (hfRequestStatus.Value != "Отказано") && (hfRequestStatus.Value != "Подтверждено") && (hfRequestStatus.Value != "Принято") && (hfRequestStatus.Value != "Утверждено") && (hfRequestStatus.Value != "К подписи"))
            {
                if (e.CommandName == "Del")
                {
                    ItemController ctl = new ItemController();
                    ctl.DeleteProduct(id);
                    System.Threading.Thread.Sleep(1000);
                    refreshProducts(1);
                    btnAddProductClick = 1;
                }
                productspriceupdate();
            }
            refreshGrid();
        }

        protected void gvRequestsFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Del")
            {
                ItemController ctl = new ItemController();
                ctl.DeleteRequestsFiles(id);
                System.Threading.Thread.Sleep(1000);
                refreshProducts(1);
            }
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session["Check"] = null;
            Session["UserName"] = null;
            Session["UserID"] = null;
            Session["FIO"] = null;
            Session.Clear();
            Response.Redirect("/Home");
        }

        private void UpdateStockPrice()
        {
            refreshGrid();
        }

        protected void MyTimer_Tick(object sender, EventArgs e)
        {
            UpdateStockPrice();
        }

        protected void btnReceptionAct_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Response.Redirect(this.EditUrl("", "", "rptReceptionAct"));

        }

        protected void btnProffer_Click(object sender, EventArgs e)
        {
            //string worktype = "0";

            if (rbtnBusiness.SelectedIndex == 0) //Зарплатный
            {
                Session["CustomerID"] = hfCustomerID.Value;
                Session["CreditID"] = hfCreditID.Value;
                Session["RequestID"] = hfRequestID.Value;
                Session["RadNumTbAverageMonthSalary"] = RadNumTbAverageMonthSalary.Text;
                //Session["RadNumTbRevenue"] = (Revenue.ToString() == "") ? "0" : Revenue.ToString();
                Session["CountWorkDay"] = (ddlCountWorkDay.Text == "") ? "0" : ddlCountWorkDay.Text;
                Session["RadNumTbСostPrice"] = (RadNumTbСostPrice.Text == "") ? "0" : RadNumTbСostPrice.Text;
                Session["RadNumTbOverhead"] = (RadNumTbOverhead.Text == "") ? "0" : RadNumTbOverhead.Text;
                Session["RadNumTbFamilyExpenses"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : RadNumTbFamilyExpenses.Text;
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : RadNumOtherLoans.Text;
                Response.Redirect(this.EditUrl("", "", "rptProffer"));
            };
            if (rbtnBusiness.SelectedIndex == 1) // Патент
            {
                decimal MinRevenue = Convert.ToDecimal(RadNumTbMinRevenue.Text);
                decimal MaxRevenue = Convert.ToDecimal(RadNumTbMaxRevenue.Text);
                decimal Revenue = ((MinRevenue != 0) && (MaxRevenue != 0)) ? (MinRevenue + MaxRevenue) / 2 : MinRevenue + MaxRevenue;
                Session["CustomerID"] = hfCustomerID.Value;
                Session["CreditID"] = hfCreditID.Value;
                Session["RequestID"] = hfRequestID.Value;
                //Session["RadNumTbAverageMonthSalary"] = RadNumTbAverageMonthSalary.Text;
                Session["RadNumTbRevenue"] = (Revenue.ToString() == "") ? "0" : Revenue.ToString();
                Session["CountWorkDay"] = (ddlCountWorkDay.Text == "") ? "0" : ddlCountWorkDay.Text;
                Session["RadNumTbСostPrice"] = (RadNumTbСostPrice.Text == "") ? "0" : RadNumTbСostPrice.Text;
                Session["RadNumTbOverhead"] = (RadNumTbOverhead.Text == "") ? "0" : RadNumTbOverhead.Text;
                Session["RadNumTbFamilyExpenses"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : RadNumTbFamilyExpenses.Text;
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : RadNumOtherLoans.Text;
                Response.Redirect(this.EditUrl("", "", "rptProfferBusiness"));
            };
            if (rbtnBusiness.SelectedIndex == 2) // Агро
            {
                decimal RevenueAgro = Convert.ToDecimal(RadNumTbRevenueAgro.Text);
                decimal RevenueMilk = Convert.ToDecimal(RadNumTbRevenueMilk.Text);
                decimal OverheadAgro = Convert.ToDecimal(RadNumTbOverheadAgro.Text);
                decimal AddOverheadAgro = Convert.ToDecimal(RadNumTbAddOverheadAgro.Text);

                Session["CustomerID"] = hfCustomerID.Value;
                Session["CreditID"] = hfCreditID.Value;
                Session["RequestID"] = hfRequestID.Value;
                Session["RevenueAgro"] = (RevenueAgro.ToString() == "") ? "0" : RevenueAgro.ToString();
                Session["RevenueMilk"] = (RevenueMilk.ToString() == "") ? "0" : RevenueMilk.ToString();
                Session["OverheadAgro"] = (OverheadAgro.ToString() == "") ? "0" : OverheadAgro.ToString();
                Session["AddOverheadAgro"] = (AddOverheadAgro.ToString() == "") ? "0" : AddOverheadAgro.ToString();
                Session["RadNumTbFamilyExpensesAgro"] = (RadNumTbFamilyExpensesAgro.Text == "") ? "0" : RadNumTbFamilyExpensesAgro.Text;
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : RadNumOtherLoans.Text;
                Response.Redirect(this.EditUrl("", "", "rptProfferAgro"));
            };
        }

        protected void chkbxAsRegistration_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxAsRegistration.Checked)
            {
                tbResidenceStreet.Text = tbRegistrationStreet.Text;
                tbResidenceHouse.Text = tbRegistrationHouse.Text;
                tbResidenceFlat.Text = tbRegistrationFlat.Text;
            }
            else
            {
                if (tbResidenceStreet.Text == tbRegistrationStreet.Text) tbResidenceStreet.Text = "";
                if (tbResidenceHouse.Text == tbRegistrationHouse.Text) tbResidenceHouse.Text = "";
                if (tbResidenceFlat.Text == tbRegistrationFlat.Text) tbResidenceFlat.Text = "";
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            if (RadNumTbRequestSumm2.Text != "")
            {
                double s = Convert.ToDouble(RadNumTbRequestSumm2.Text);
                double n = Convert.ToDouble(ddlPeriod.Text);
                double stavka = Convert.ToDouble(ddlReqRateCalc.SelectedItem.Value);
                double i = stavka / 12 / 100;
                double k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
                lblRequestSum.Text = RadNumTbRequestSumm2.Text + " KGS";
                lblRequestPeriod.Text = n.ToString() + " мес.";
                if (ddlReqRateCalc.SelectedItem.Value == "29.90") { lblInterestRate.Text = "19 %"; }
                else { lblInterestRate.Text = "10-12 %"; }

                lblMonthlyInstallment.Text = Math.Round((k), 0).ToString() + " KGS";
                lblAmountInterestForPeriod.Text = Math.Round(n * k - s, 0).ToString() + " KGS";
                lblSumTotalReturn.Text = Math.Round(n * k, 0).ToString() + " KGS";
            }
            else
            {
            }
        }

        protected void btnPledgeAgreement_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
            Response.Redirect(this.EditUrl("", "", "rptPledgeAgreement"));
        }

        protected void btnCustomerEdit_Click(object sender, EventArgs e)
        {
            CustomerEdit(Convert.ToInt32(hfCustomerID.Value));
            btnCredit.Text = "Выбрать клиента";
        }

        private void CustomerEdit(int custID)
        {
            pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            clearEditControls();
            btnSaveCustomer.Enabled = true;
            SysController ctx = new SysController();
            Customer cust = ctx.CustomerGetItem(custID);
            tbSurname.Text = cust.Surname;
            tbCustomerName.Text = cust.CustomerName;
            tbOtchestvo.Text = cust.Otchestvo;
            tbIdentificationNumber.Text = cust.IdentificationNumber;
            tbDocumentSeries.Text = cust.DocumentSeries + cust.DocumentNo;
            tbIssueDate.Text = Convert.ToDateTime(cust.IssueDate).ToString("dd.MM.yyyy");
            tbValidTill.Text = Convert.ToDateTime(cust.DocumentValidTill).ToString("dd.MM.yyyy");
            tbIssueAuthority.Text = cust.IssueAuthority;
            rbtSex.SelectedIndex = (cust.Sex == true) ? 0 : 1;
            tbDateOfBirth.Text = Convert.ToDateTime(cust.DateOfBirth).ToString("dd.MM.yyyy");
            tbRegistrationStreet.Text = cust.RegistrationStreet;
            tbRegistrationHouse.Text = cust.RegistrationHouse;
            tbRegistrationFlat.Text = cust.RegistrationFlat;
            tbResidenceStreet.Text = cust.ResidenceStreet;
            tbResidenceHouse.Text = cust.ResidenceHouse;
            tbContactPhone.Text = cust.ContactPhone1;
            tbResidenceFlat.Text = cust.ResidenceFlat;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void Confirm()
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            var lst5 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Подтверждено";
                dbRWZ.Requests.Context.SubmitChanges();
                System.Threading.Thread.Sleep(1000);
                refreshGrid();
                string hexYellow = "#fdf404";
                Color _colorYellow = System.Drawing.ColorTranslator.FromHtml(hexYellow);
                lblStatusRequest.BackColor = _colorYellow; hfRequestStatus.Value = "Подтверждено";
                /*RequestHistory*/
                DateTime dateTimeNow = dateNow;
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "Подтверждено",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value) // requestID
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
                //var Customer = dbR.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
                ////SendMailTo(lst5.BranchID, "Подтверждено", Customer.Surname + " " + Customer.Surname + " " + Customer.Otchestvo, false, false, true); //экспертам
            }
        }

        private void ApprovedRequest()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst5 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Утверждено";
                dbRWZ.Requests.Context.SubmitChanges();
                System.Threading.Thread.Sleep(1000);
                refreshGrid();
                string hexGreen = "#7cfa84";
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "Утверждено";
                /*RequestHistory*/
                DateTime dateTimeNow = dateNow;
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "Утверждено",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value) // requestID
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
                string usernameAgent = lst5.AgentUsername;
                string fullnameAgent = lst5.AgentFirstName;
                string fullnameCustomer = lst5.Surname + " " + lst5.CustomerName + " " + lst5.Otchestvo;
                int usrID = Convert.ToInt32(Session["UserID"].ToString());
                int reqID = lst5.RequestID;
                int? groupID = lst5.GroupID;
                ////AgentView.SendMailTo2("Утверждено", true, true, false, connectionString, usrID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                //SendMailTo2("Утверждено", true, true, false, connectionString, usrID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                if (lst5.OrgID == 10) //beeshop 
                {
                    // SendMailToGroup("Утверждено", groupID, connectionStringR, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                }
            }
        }
        private void SignatureRequest()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            var lst5 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "К подписи";
                dbRWZ.Requests.Context.SubmitChanges();
                System.Threading.Thread.Sleep(1000);
                refreshGrid();
                string hexGreen = "#7cfa84";
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "К подписи";
                /*RequestHistory*/
                DateTime dateTimeNow = dateNow;
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "К подписи",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value) // requestID
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
                //var Customer = dbW.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
                ////SendMailTo(lst5.BranchID, "К подписи", Customer.Surname + " " + Customer.Surname + " " + Customer.Otchestvo, false, false, true); //экспертам

            }
        }

        protected void lbHistory_Click(object sender, EventArgs e)
        {
            pnlHistory.Visible = true;
            lbHistory.Visible = false;
            lbBack.Visible = true;
            int id = Convert.ToInt32(hfRequestID.Value);
            history(id);
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            pnlHistory.Visible = false;
            lbHistory.Visible = true;
            lbBack.Visible = false;
        }

        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            ItemController ctl = new ItemController();
            dbRWZ.Products.DeleteAllOnSubmit(dbRWZ.Products);
            dbRWZ.SubmitChanges();
            foreach (DataRow row in dt.Rows)
            {
                Product newProduct = new Product
                {
                    ProductMark = row["model"].ToString(), // ddlProductMark.SelectedValue,
                    ProductSerial = row["serial"].ToString(), //tbProductSerial.Text,
                    ProductImei = row["imei"].ToString(), //tbProductImei.Text,
                    ProductImei2 = row["imei2"].ToString(), //tbProductImei.Text,
                    Price = Convert.ToDecimal(row["price"].ToString()), //Convert.ToDecimal(RadNumTbProductPrice.Text)
                    Sim = row["sim"].ToString(),
                };

                ctl.ItemProductAddItem(newProduct);
            }
        }

        protected void lbSettings_Click(object sender, EventArgs e)
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
            int? shopID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().ShopID;
            int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
            var groupsOrg = dbRWZ.Groups.Where(r => r.OrgID == 7).ToList(); // Мой Телефон
            var groupOrg = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupName;
            var shopName = dbRWZ.Shops.Where(r => r.ShopID == shopID).FirstOrDefault().ShopName;


            if (pnlSettings.Visible == true)
            { pnlSettings.Visible = false; lblMsgPassword.Visible = false; lblMsgPassword.Text = ""; }
            else
            {
                pnlSettings.Visible = true; lblMsgPassword.Text = "";
                if (orgID == 7) // Только для Мой Телефон
                {

                    ddlGroup.DataSource = groupsOrg;
                    ddlGroup.DataTextField = "GroupName";
                    ddlGroup.DataValueField = "GroupID";
                    ddlGroup.DataBind();
                    ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByText(groupOrg));

                    ddlShop.DataSource = dbRWZ.Shops.ToList();
                    ddlShop.DataTextField = "ShopName";
                    ddlShop.DataValueField = "ShopID";
                    ddlShop.DataBind();
                    ddlShop.SelectedIndex = ddlShop.Items.IndexOf(ddlShop.Items.FindByText(shopName));
                    pnlGroup.Visible = true;
                }
                else pnlGroup.Visible = false;
            }
        }

        protected void btnCancelPassword_Click(object sender, EventArgs e)
        {
            pnlSettings.Visible = false;
        }

        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            var psw = tbOldPassword.Text;
            var sha1 = Crypto.SHA1(psw).ToLower();
            string usrLogin = Session["UserName"].ToString();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            Users2 usr = dbRWZ.Users2s.Where(u => (u.UserName == usrLogin) && (u.Password == sha1)).FirstOrDefault();
            if (tbNewPassword.Text == tbConfirmPassword.Text)
            {
                if (usr != null)
                {
                    usr.Password = Crypto.SHA1(tbNewPassword.Text).ToLower();
                    usr.GroupID = Convert.ToInt32(ddlGroup.SelectedItem.Value);
                    dbRWZ.Users2s.Context.SubmitChanges();
                    lblMsgPassword.Text = "Пароль успешно изменен";
                    lblMsgPassword.Visible = true;
                    pnlSettings.Visible = false;
                }
                else
                {
                    lblMsgPassword.Text = "Неправильный пароль";
                    lblMsgPassword.Visible = true;
                }
            }
            else { lblMsgPassword.Text = "Ваш новый пароль и подтверждение пароля не совпадают"; lblMsgPassword.Visible = true; }
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            Request editRequest = new Request();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
            editRequest.RequestStatus = "Не подтверждено";
            ctlItem.RequestUpd(editRequest);
            System.Threading.Thread.Sleep(1000);
            refreshGrid();
            string hexOrange = "#8C5E40";
            Color _colorOrange = System.Drawing.ColorTranslator.FromHtml(hexOrange);
            lblStatusRequest.BackColor = _colorOrange; hfRequestStatus.Value = "Не подтверждено";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = usrID,
                CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Не подтверждено",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }



        protected async void btnCancelReq_Click(object sender, EventArgs e)
        {
            //********
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.GroupID == 110)
            {
                //********меняем статус в ОБ
                //await PostOnIssue(lst.CustomerID.ToString(), lst.CreditID.ToString());
                //********отправляем статус в нуртелеком
                string str = await SendStatusBee(lst.CreditID.ToString(), "REFUSED_BY_CLIENT");
                if (str == "200")
                { CancelRequest(); }
                else
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статусов в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                }

            }
            else
            {
                CancelRequest();
            }

        }


        public async void CancelRequest()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            string usrName = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().UserName;
            DateTime dateTimeNow = dateNow;
            /**/
            Request editRequest = new Request();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
            editRequest.RequestStatus = "Отменено";
            ctlItem.RequestUpd(editRequest);
            System.Threading.Thread.Sleep(1000);
            refreshGrid();
            string hexBlack = "#878787";
            Color _colorBlack = System.Drawing.ColorTranslator.FromHtml(hexBlack);
            lblStatusRequest.BackColor = _colorBlack; hfRequestStatus.Value = "Отменено";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = usrID,
                CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Отменено",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
            /*HistoriesStatuses*//*----------------------------------------------------*/
                                 //HistoriesStatuse hisStat = new HistoriesStatuse()
                                 //{
                                 //    CreditID = Convert.ToInt32(hfCreditID.Value),
                                 //    StatusID = 4, //отмена
                                 //    StatusDate = dateNowServer,
                                 //    OperationDate = dateTimeNow,
                                 //    UserID = usrOBID
                                 //};
                                 //ctx.ItemHistoriesStatuseAddItem(hisStat);
                                 /**/

            //Отправка сообщ для МТ-Онлайн {
            string body = "", to = "rtentiev@doscredobank.kg", efrom = "", replyto = "", subject = "Отменено", strOB = "";

            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst != null)
            {
                body = "Заявка№: " + hfRequestID.Value + ", ФИО клиента: " + lst.Surname + " " + lst.CustomerName + " " + lst.Otchestvo;
            }
            if (lst.OrgID == 7) // МТ 
            {
                //SendMail2(body, to, efrom, replyto, subject);
                try
                {
                    int reqID = dbRWZ.Requests2s.Where(r => r.RequestID2 == lst.RequestID).FirstOrDefault().RequestID;
                    strOB = await SendStatusMT(reqID.ToString(), "Отменено");
                }
                catch (Exception ex)
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                }
                finally
                {
                }
            }
            //Отправка сообщ для МТ-Онлайн }

        }


        protected void btnReceived_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            Request editRequest = new Request();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
            editRequest.RequestStatus = "Принято";
            ctlItem.RequestUpd(editRequest);
            System.Threading.Thread.Sleep(1000);
            refreshGrid();
            string hexReceive = "#11ACE4";
            Color _colorReceive = System.Drawing.ColorTranslator.FromHtml(hexReceive);
            lblStatusRequest.BackColor = _colorReceive; hfRequestStatus.Value = "Принято";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = usrID,
                CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Принято",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }


        protected void gvRequestsFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void ddlRequestPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var Requestprod = (from v in dbRWZ.RequestsProducts where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v);
            foreach (RequestsProduct rp in Requestprod)
            {
                RequestsProduct edititem = new RequestsProduct();
                ItemController ctl = new ItemController();
                edititem = ctl.GetRequestsProductById(Convert.ToInt32(rp.ProductID));
                //   decimal pr = tarif(Convert.ToInt32(ddlRequestPeriod.SelectedValue), rp.TarifName);
                //   edititem.PriceTarif = pr;
                edititem.PriceWithTarif = rp.Price; // + pr;
                ctl.RequestsProductsUpd(edititem);
            }
            refreshProducts(1);
        }

        protected void chbEmployer_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEmployer.Checked) { pnlEmployment.Visible = false; }
            else { pnlEmployment.Visible = true; }
        }


        public static void SendMailToGroup(string strsubj, int? GroupID, string connectionString, string Fullname, string UserName, string FIO)
        {
            /*****************************/
            string stremailto = "Утверждено";
            string emailfrom = "zamat@doscredobank.kg";
            string templateAdm = "";
            string templateUsr = "";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            try
            {
                templateAdm = "<table>" +
            "<tr>" +
                "<td>ФИО:</td>" +
                "<td>[FIO]</td>" +
            "</tr>" +
            "</table>" +
            "<p>" +
                "<br /><br />" +
            "<br />      Ползователь Username: [UserName]" +
            "<br />      ФИО: [Fullname]" +
            "</p>" +
            "</body>" +
            "</html>";
                // template for admin...

                templateAdm = templateAdm.Replace("[Fullname]", Fullname);
                templateAdm = templateAdm.Replace("[UserName]", UserName);
                templateAdm = templateAdm.Replace("[FIO]", FIO);
                //send email to admin
                string mailto = "";

                //foreach (Users2 usr in db.Users2s.Where(r => r.GroupID == GroupID).ToList())
                //{
                //    mailto = usr.EMail;
                //    SendMail2(templateAdm, mailto, emailfrom, "", strsubj);
                //}
                SendMail2(templateAdm, "cc_projectteam@beeline.kg", emailfrom, "", strsubj);
                SendMail2(templateAdm, "asokolov@doscredobank.kg", emailfrom, "", strsubj);
                SendMail2(templateAdm, "KMambetbaeva@beeline.kg", emailfrom, "", strsubj);
                // SendMail2(templateAdm, "rotfu_s@rambler.ru", emailfrom, "", strsubj);
                // SendMail2(templateAdm, "evgeniy@doscredobank.kg", emailfrom, "", strsubj);
                SendMail2(templateAdm, "rtentiev@doscredobank.kg", emailfrom, "", strsubj);
            }
            catch (Exception ex)
            {
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.Message + "<br>1.Настройки модуля не определены.", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
            }
            /*****************************/
        }


        public static void SendMailTo2(string strsubj, bool boolagent, bool booladmin, bool boolekspert, string connectionString, int usrID, int reqID, string Fullname, string UserName, string FIO)
        {
            /*****************************/
            string stremailto = "";
            string emailfrom = "";
            string templateAdm = "";
            string templateUsr = "";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
            int branchID = dbR.Offices.Where(r => r.ID == officeID).FirstOrDefault().BranchID;
            try
            {
                templateAdm = "<table>" +
            "<tr>" +
                "<td>ФИО:</td>" +
                "<td>[FIO]</td>" +
            "</tr>" +
            "</table>" +
            "<p>" +
                "<br /><br />" +
            "<br />      Ползователь Username: [UserName]" +
            "<br />      ФИО: [Fullname]" +
            "</p>" +
            "</body>" +
            "</html>";
                // template for admin...
                templateAdm = templateAdm.Replace("[Fullname]", Fullname);
                templateAdm = templateAdm.Replace("[UserName]", UserName);
                templateAdm = templateAdm.Replace("[FIO]", FIO);
                //send email to admin
                string mailto = "";
                if (boolagent) //отправка сообщ агенту
                {
                    int? agentID = dbRWZ.Requests.Where(r => r.RequestID == reqID).FirstOrDefault().AgentID;
                    mailto = dbRWZ.Users2s.Where(r => r.UserID == agentID).FirstOrDefault().EMail;
                    SendMail2(templateAdm, mailto, emailfrom, "", strsubj);
                }
                foreach (RequestsUsersRole requserrole in dbRWZ.RequestsUsersRoles)
                {

                    if (requserrole.RoleID == 9) //сообщ всем админам о новой заявке
                    {
                        if (booladmin)
                        {
                            mailto = dbRWZ.Users2s.Where(r => r.UserID == requserrole.UserID).FirstOrDefault().EMail;
                            //SendMail2(templateAdm, mailto, emailfrom, "", strsubj);
                        }
                    };
                    if (requserrole.RoleID == 2) //сообщ экспертам филиала о новой заявке
                    {
                        if (boolekspert)
                        {
                            int officeIDeks = dbRWZ.Users2s.Where(r => r.UserID == requserrole.UserID).FirstOrDefault().OfficeID;
                            int branchIDeks = dbRWZ.Offices.Where(r => r.ID == officeIDeks).FirstOrDefault().BranchID;
                            if (branchID == branchIDeks)
                            {
                                mailto = dbRWZ.Users2s.Where(r => (r.UserID == requserrole.UserID)).FirstOrDefault().EMail;
                                //SendMail2(templateAdm, mailto, emailfrom, "", strsubj);
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.Message + "<br>1.Настройки модуля не определены.", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
            }
            /*****************************/
        }

        protected async void btnCancelReqExp_Click(object sender, EventArgs e)
        {
            //********
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.GroupID == 110)
            {
                //********меняем статус в ОБ
                //await PostOnIssue(lst.CustomerID.ToString(), lst.CreditID.ToString());
                //********отправляем статус в нуртелеком
                var result = await SendStatusBee(lst.CreditID.ToString(), "DECLINED");
                //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                //var data = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject(result));
                //dynamic data = JObject.Parse(str);
                //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                //Results data = JsonConvert.DeserializeObject<Results>(result);
                //var data = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject(result));

                //var list = await Task.Run(() => JsonConvert.DeserializeObject<List<MyObject>>(response.Content));
                //string str = data.statusCode.ToString();
                if (result == "200")
                {
                    CancelRequestExp();
                }
                else
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статусов в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                }
            }
            else
            {
                CancelRequestExp();
            }

        }



        public async void CancelRequestExp()
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            Request editRequest = new Request();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
            editRequest.RequestStatus = "Отказано";
            ctlItem.RequestUpd(editRequest);
            System.Threading.Thread.Sleep(1000);
            refreshGrid();
            string hexBlack = "#878787";
            Color _colorBlack = System.Drawing.ColorTranslator.FromHtml(hexBlack);
            lblStatusRequest.BackColor = _colorBlack; hfRequestStatus.Value = "Отказано";


            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            string usrName = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().UserName;

            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = usrID,
                CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Отказано",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
            //HistoriesStatuse hisStat = new HistoriesStatuse()
            //{
            //    CreditID = Convert.ToInt32(hfCreditID.Value),
            //    StatusID = 4, //отмена
            //    StatusDate = dateNowServer,
            //    OperationDate = dateTimeNow,
            //    UserID = usrOBID
            //};

            //Отправка сообщ для МТ-Онлайн {
            string body = "", to = "rtentiev@doscredobank.kg", efrom = "", replyto = "", subject = "Отказано", strOB = "";

            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst != null)
            {
                body = "Заявка№: " + hfRequestID.Value + ", ФИО клиента: " + lst.Surname + " " + lst.CustomerName + " " + lst.Otchestvo;
            }
            if (lst.OrgID == 7) // МТ Онлайн
            {
                //SendMail2(body, to, efrom, replyto, subject);
                try
                {
                    int reqID = dbRWZ.Requests2s.Where(r => r.RequestID2 == lst.RequestID).FirstOrDefault().RequestID;
                    strOB = await SendStatusMT(reqID.ToString(), "Отказано");
                }
                catch (Exception ex)
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                }
                finally
                {
                }
            }
            //Отправка сообщ для МТ-Онлайн }


        }


        protected void gvRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRequests.PageIndex = e.NewPageIndex;
            gvRequests.DataBind();
            //refreshGrid();
            hfRequestsRowID.Value = "";
            refreshGrid();
        }

        protected void btnForPeriod_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
            Session["RoleID"] = Convert.ToInt32(Session["RoleID"]);
            Session["tbDate1"] = tbDate1b.Text;
            Session["tbDate2"] = tbDate2b.Text;
            Session["i0"] = chkbxlistStatus.Items[0].Selected;
            Session["i1"] = chkbxlistStatus.Items[1].Selected;
            Session["i2"] = chkbxlistStatus.Items[2].Selected;
            Session["i3"] = chkbxlistStatus.Items[3].Selected;
            Session["i4"] = chkbxlistStatus.Items[4].Selected;
            Session["i5"] = chkbxlistStatus.Items[5].Selected;
            Session["i6"] = chkbxlistStatus.Items[6].Selected;
            Session["i7"] = chkbxlistStatus.Items[7].Selected;
            Session["i8"] = chkbxlistStatus.Items[8].Selected;
            Session["i9"] = chkbxlistStatus.Items[9].Selected;
            Session["i10"] = chkbxlistStatus.Items[10].Selected;
            Session["i11"] = chkbxlistStatus.Items[11].Selected;
            Session["i12"] = chkbxlistStatus.Items[12].Selected;
            Session["k0"] = chkbxlistKindActivity.Items[0].Selected;
            Session["k1"] = chkbxlistKindActivity.Items[1].Selected;
            Session["k2"] = chkbxlistKindActivity.Items[2].Selected;
            Session["g1"] = chkbxGroup.Items[0].Selected;
            Session["g2"] = chkbxGroup.Items[1].Selected;
            Session["g3"] = chkbxGroup.Items[2].Selected;
            Response.Redirect(this.EditUrl("", "", "rptForPeriod"));
        }

        protected void btnInProcess_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            Request editRequest = new Request();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
            editRequest.RequestStatus = "В обработке";
            ctlItem.RequestUpd(editRequest);
            System.Threading.Thread.Sleep(1000);
            refreshGrid();
            string hexRed = "#E47E11";
            Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
            lblStatusRequest.BackColor = _colorRed; hfRequestStatus.Value = "В обработке";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = usrID,
                CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "В обработке",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }

        protected async void btnApproved_Click(object sender, EventArgs e)
        {

            string strMT = "200";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.GroupID == 110)
            {
                //********меняем статус в ОБ Утверждено*****Begin//
                string strOB = "";
                var statusOB = dbR.HistoriesStatuses.Where(h => ((h.CreditID == lst.CreditID) && (h.StatusID == 2))).ToList();
                if (statusOB.Count > 0)
                {
                    strOB = "200";
                }
                else
                {
                    try
                    {
                        strOB = await ApprovedRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                        //strOB = str;
                    }
                    catch (Exception ex)
                    {
                        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    }
                    finally
                    {

                    }
                    if (strOB == "200")
                    {
                        //ApprovedRequest();
                    }
                    else
                    {
                        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса Утверждено в АБС", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    }
                }
                //********меняем статус в ОБ Утверждено*****End//

                //********отправляем статус в нуртелеком Begin
                string strNur = await SendStatusBee(lst.CreditID.ToString(), "APPROVED");
                if (strNur == "200")
                {
                    //ApprovedRequest(); //Утверждаем если даже в ОБ не отвечает
                }
                else
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED в Билайн", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                }
                //************отправляем статус в нуртелеком End
                if ((strOB == "200") && (strNur == "200")) { ApprovedRequest(); }
            }
            else
            //Отправка сообщ для МТ-Онлайн 
            if (lst.OrgID == 17) // МТ 
            {
                try
                {
                    int reqID = dbRWZ.Requests2s.Where(r => r.RequestID2 == lst.RequestID).FirstOrDefault().RequestID;
                    strMT = await SendStatusMT(reqID.ToString(), "Утверждено");
                    //if (strOB == "200")
                    ApprovedRequest();
                }
                catch (Exception ex)
                {
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strMT + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                }
                finally
                {
                }
            }
            //Отправка сообщ для МТ-Онлайн 
            else
            {
                ApprovedRequest();
            }


        }



        public async System.Threading.Tasks.Task<string> SendStatusBee(string CreditID, string Status)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var byteArray = Encoding.ASCII.GetBytes("admin:{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //string json = "{\"state\": \"ISSUED_BY\"}";
                    string json = "{\"state\": \"" + Status + "\"}";
                    //var response = await client.PostAsync("https://umai-stage.balance.kg/mcm-api/dos/applications/10001/status" + CreditID, new StringContent(json, Encoding.UTF8, "application/json"));
                    //var response = await client.PostAsync("https://umai-stage.balance.kg/mcm-api/dos/applications/10001/status", new StringContent(json, Encoding.UTF8, "application/json"));
                    var response = await client.PostAsync("https://umai-stage.balance.kg/mcm-api/dos/applications/" + CreditID + "/status", new StringContent(json, Encoding.UTF8, "application/json"));

                    var result = await response.Content.ReadAsStringAsync();
                    JObject obj = JObject.Parse(result);
                    string status = obj["status"].ToString();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (status == "SUCCESS")
                            result = "200";
                    }
                    //var status = result.Where;
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                return "error";
            }
            finally
            {
                //TextBox1.Text = TextBox1.Text + Response.ToString();
                //return "";
            }
        }




        public async System.Threading.Tasks.Task<string> ApprovedRequestOB(string CustomerID, string CreditID)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                // var client = HttpClientFactory.Create();

                using (HttpClient client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://10.120.16.95/");
                    client.BaseAddress = new Uri("https://" + connectionStringOBAPIAddress + "/");

                    string json = "";
                    string url2 = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/PromoteLoanStatus?customerID=" + CustomerID + "&creditID=" + CreditID + "&statusID=2";
                    //string url3 = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/PromoteLoanStatus?customerID=" + CustomerID + "&creditID=" + CreditID + "&statusID=3";
                    var response = await client.GetAsync(url2);
                    //var response2 = await client.GetAsync(url3);

                    var result = await response.Content.ReadAsStringAsync();
                    //var result2 = await response2.Content.ReadAsStringAsync();
                    string result3 = "res:" + result;

                    //JObject jResults2 = JObject.Parse(result);
                    //var jArray = JArray.Parse(result);

                    //string state = getstat(result);
                    string state = getstat3(result);

                    if ((response.StatusCode == HttpStatusCode.OK) && (state == "0"))  ///(jResults2["State"].ToString() == "0"))
                    {
                        result = "200";
                    }
                    if (result != "200") result = result3;
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                return "error";

            }
            finally
            {
                ////TextBox1.Text = TextBox1.Text + Response.ToString();

            }
        }

        public class structgetstat3
        {
            public int CreditID { get; set; }
            public int CustomerID { get; set; }
            public int CreditIDStatus { get; set; }
            public bool Result { get; set; }
            public int State { get; set; }
            public string Message { get; set; }
            public string MessageCode { get; set; }
        }


        public string getstat3(string result)
        {
            try
            {

                //structgetstat3 dez = JsonConvert.DeserializeObject<structgetstat3>(result.ToString());

                var str = JsonConvert.DeserializeObject<List<structgetstat3>>(result);

                return str.Select(s => s.State).FirstOrDefault().ToString();
                //return "";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public async System.Threading.Tasks.Task<string> SendStatusMT(string CreditID, string Status)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                // var client = HttpClientFactory.Create();

                using (HttpClient client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://10.120.16.95/");
                    string json = "";
                    //string url3 = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/PromoteLoanStatus?customerID=" + CustomerID + "&creditID=" + CreditID + "&statusID=3";
                    string url3 = "https://myphone.kg/sys/bank/dosstatus/hjg234hgf2hgf234hgf345/" + CreditID + "/" + Status;
                    var response = await client.GetAsync(url3);
                    var result = await response.Content.ReadAsStringAsync();
                    string result3 = "res:" + result;
                    //string state = getstat(result);
                    if ((response.StatusCode == HttpStatusCode.OK)) // && (state == "0"))
                    {
                        result = "200";
                    }
                    if (result != "200") result = result3;
                    return result;
                }
            }
            catch (Exception ex)
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                return "error";

            }
            finally
            {

            }
        }


        //public string getstat(string result)
        //{
        //    JArray a = JArray.Parse(result);
        //    string state = "";
        //    string statevalue = "";
        //    foreach (JObject o in a.Children<JObject>())
        //    {
        //        foreach (JProperty p in o.Properties())
        //        {
        //            if (p.Name == "State")
        //            {
        //                state = p.Name;
        //                statevalue = (string)p.Value;
        //                //Console.WriteLine(state + " -- " + value);
        //            }
        //        }
        //    }
        //    return statevalue;
        //}



        protected async void btnSignature_Click(object sender, EventArgs e)
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.RequestStatus == "Утверждено")
            {
                if (lst.GroupID == 110)
                {
                    //********меняем статус в ОБ На выдаче*****//
                    string strOB = "", strNur = "";
                    var statusOB = dbR.HistoriesStatuses.Where(h => ((h.CreditID == lst.CreditID) && (h.StatusID == 3))).ToList();
                    if (statusOB.Count > 0)
                    {
                        strOB = "200";
                    }
                    else
                    {
                        try
                        {
                            strOB = await AtIssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                        }
                        catch (Exception ex)
                        {
                            //TextBox1.Text = TextBox1.Text + ex.Message;
                            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        }
                        finally
                        {
                            ////TextBox1.Text = TextBox1.Text + Response.ToString();

                        }
                        if (strOB == "200")
                        {
                            /*RequestHistory*/
                            //var reqs = dbW.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList().FirstOrDefault();
                            //reqs.RequestStatus = "На выдаче";
                            //dbW.Requests.Context.SubmitChanges();
                            //SignatureRequest();
                            //AtIssueRequest();
                        }
                        else
                        {
                            DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса На выдаче в АБС", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        }
                    }
                    /**********************************/

                    //********отправляем статус в нуртелеком Begin
                    //string strNur = await SendStatusNur(lst.CreditID.ToString(), "APPROVED");
                    strNur = "200";
                    if (strNur == "200")
                    {
                        //ApprovedRequest(); //Утверждаем если даже в ОБ не отвечает
                    }
                    else
                    {
                        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    }
                    //************отправляем статус в нуртелеком End
                    if ((strOB == "200") && (strNur == "200"))
                    {
                        SignatureRequest();
                        AtIssueRequest();
                    }

                }
                else
                {
                    //********меняем статус в скоринге если не нано
                    SignatureRequest();

                }
            }
            //Signature();
        }


        private void AtIssueRequest()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst5 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "На выдаче";
                dbRWZ.Requests.Context.SubmitChanges();
                System.Threading.Thread.Sleep(1000);
                refreshGrid();
                string hexGreen = "#7cfa84";
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "На выдаче";
                DateTime dateTimeNow = dateNow;
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "На выдаче",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value) // requestID
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
            }
        }



        public async System.Threading.Tasks.Task<string> AtIssueRequestOB(string CustomerID, string CreditID)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                // var client = HttpClientFactory.Create();

                using (HttpClient client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://10.120.16.95/");
                    client.BaseAddress = new Uri("https://" + connectionStringOBAPIAddress + "/");

                    string json = "";
                    //string url2 = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/PromoteLoanStatus?customerID=" + CustomerID + "&creditID=" + CreditID + "&statusID=2";
                    string url3 = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/PromoteLoanStatus?customerID=" + CustomerID + "&creditID=" + CreditID + "&statusID=3";
                    //var response = await client.GetAsync(url2);
                    var response = await client.GetAsync(url3);

                    var result = await response.Content.ReadAsStringAsync();
                    //var result2 = await response2.Content.ReadAsStringAsync();
                    string result3 = "res:" + result;

                    //JObject jResults2 = JObject.Parse(result);

                    //JArray a = JArray.Parse(result);
                    string state = getstat3(result);

                    //string statevalue = "";
                    //foreach (JObject o in a.Children<JObject>())
                    //{
                    //    foreach (JProperty p in o.Properties())
                    //    {
                    //        if (p.Name == "State")
                    //        {
                    //            state = p.Name;
                    //            statevalue = (string)p.Value;
                    //            //Console.WriteLine(state + " -- " + value);
                    //        }
                    //    }
                    //}

                    if ((response.StatusCode == HttpStatusCode.OK) && (state == "0"))
                    {
                        result = "200";
                    }
                    else
                    {
                        result = result3;
                        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, result3, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                return "error";

            }
            finally
            {
                ////TextBox1.Text = TextBox1.Text + Response.ToString();

            }
        }




        protected void btnComment_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = usrID,
                CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Комментарий",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            refreshGrid();
            /******************************************************/
        }

        protected void btnForPeriodWithHistory_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
            Session["RoleID"] = Convert.ToInt32(Session["RoleID"]);
            Session["tbDate1"] = tbDate1b.Text;
            Session["tbDate2"] = tbDate2b.Text;
            Session["i0"] = chkbxlistStatus.Items[0].Selected;
            Session["i1"] = chkbxlistStatus.Items[1].Selected;
            Session["i2"] = chkbxlistStatus.Items[2].Selected;
            Session["i3"] = chkbxlistStatus.Items[3].Selected;
            Session["i4"] = chkbxlistStatus.Items[4].Selected;
            Session["i5"] = chkbxlistStatus.Items[5].Selected;
            Session["i6"] = chkbxlistStatus.Items[6].Selected;
            Session["i7"] = chkbxlistStatus.Items[7].Selected;
            Session["i8"] = chkbxlistStatus.Items[8].Selected;
            Session["i9"] = chkbxlistStatus.Items[9].Selected;
            Session["i10"] = chkbxlistStatus.Items[10].Selected;
            Session["i11"] = chkbxlistStatus.Items[11].Selected;
            Session["i12"] = chkbxlistStatus.Items[12].Selected;
            Session["k0"] = chkbxlistKindActivity.Items[0].Selected;
            Session["k1"] = chkbxlistKindActivity.Items[1].Selected;
            Session["k2"] = chkbxlistKindActivity.Items[2].Selected;
            Session["g1"] = chkbxGroup.Items[0].Selected;
            Session["g2"] = chkbxGroup.Items[1].Selected;
            Session["g3"] = chkbxGroup.Items[2].Selected;
            Response.Redirect(this.EditUrl("", "", "rptForPeriodWithHistory"));
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            refreshProducts(1);
        }

        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            refreshProducts(1);
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label ProductID = (Label)(gvProducts.Rows[e.RowIndex].FindControl("lblProductID"));
            TextBox ProductMark = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtProductMark"));
            TextBox ProductSerial = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtProductSerial"));
            TextBox ProductImei = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtProductImei"));
            TextBox ProductImei2 = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtProductImei2"));
            Telerik.Web.UI.RadNumericTextBox Price = ((Telerik.Web.UI.RadNumericTextBox)gvProducts.Rows[e.RowIndex].FindControl("txtPrice"));
            // DropDownList ddlTarifName = ((DropDownList)gvProducts.Rows[e.RowIndex].FindControl("ddlTarifName"));
            TextBox Note = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtNote"));
            //  decimal pr = 0; // tarif(Convert.ToInt32(ddlRequestPeriod.SelectedValue), ddlTarifName.SelectedItem.Text);
            RequestsProduct edititem = new RequestsProduct();
            ItemController ctl = new ItemController();
            edititem = ctl.GetRequestsProductById(Convert.ToInt32(ProductID.Text));
            edititem.ProductMark = ProductMark.Text;
            edititem.ProductSerial = ProductSerial.Text;
            edititem.ProductImei = ProductImei.Text;
            edititem.ProductImei2 = ProductImei2.Text;
            edititem.Price = Convert.ToDecimal(Price.Text); //Convert.ToDecimal(RadNumTbProductPrice.Text);
                                                            //edititem.TarifName = ddlTarifName.SelectedItem.Text;
                                                            //  edititem.PriceTarif = pr;
            edititem.PriceWithTarif = Convert.ToDecimal(Price.Text);  // + pr;
            edititem.Note = Note.Text;
            ctl.RequestsProductsUpd(edititem);
            System.Threading.Thread.Sleep(1000);
            refreshProducts(1);
            btnAddProductClick = 1;
            gvProducts.EditIndex = -1;

            refreshProducts(1);
        }

        protected void btnForPeriodWithProducts_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
            Session["RoleID"] = Convert.ToInt32(Session["RoleID"]);
            Session["tbDate1"] = tbDate1b.Text;
            Session["tbDate2"] = tbDate2b.Text;
            Session["i0"] = chkbxlistStatus.Items[0].Selected;
            Session["i1"] = chkbxlistStatus.Items[1].Selected;
            Session["i2"] = chkbxlistStatus.Items[2].Selected;
            Session["i3"] = chkbxlistStatus.Items[3].Selected;
            Session["i4"] = chkbxlistStatus.Items[4].Selected;
            Session["i5"] = chkbxlistStatus.Items[5].Selected;
            Session["i6"] = chkbxlistStatus.Items[6].Selected;
            Session["i7"] = chkbxlistStatus.Items[7].Selected;
            Session["i8"] = chkbxlistStatus.Items[8].Selected;
            Session["i9"] = chkbxlistStatus.Items[9].Selected;
            Session["i10"] = chkbxlistStatus.Items[10].Selected;
            Session["i11"] = chkbxlistStatus.Items[11].Selected;
            Session["i12"] = chkbxlistStatus.Items[12].Selected;
            Session["k0"] = chkbxlistKindActivity.Items[0].Selected;
            Session["k1"] = chkbxlistKindActivity.Items[1].Selected;
            Session["k2"] = chkbxlistKindActivity.Items[2].Selected;
            Session["g1"] = chkbxGroup.Items[0].Selected;
            Session["g2"] = chkbxGroup.Items[1].Selected;
            Session["g3"] = chkbxGroup.Items[2].Selected;
            Response.Redirect(this.EditUrl("", "", "rptForPeriodWithProducts"));
        }

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = false;
            if (string.IsNullOrEmpty(email)) valid = false;
            else valid = check.IsMatch(email);
            return valid;
        }

        protected void btnSaveGroup_Click(object sender, EventArgs e)
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            var user2 = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault();
            user2.GroupID = Convert.ToInt32(ddlGroup.SelectedItem.Value);
            user2.ShopID = Convert.ToInt32(ddlShop.SelectedItem.Value);
            dbRWZ.Users2s.Context.SubmitChanges();
            lblSHop.Text = "Данные успешно сохранены";
        }

        protected void ddlDocumentTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbDocumentSeries.Text.Length > 3)
            {
                if (tbDocumentSeries.Text.Substring(0, 2).ToUpper() == "AN") { ddlDocumentTypeID.SelectedIndex = 0; }
                if (tbDocumentSeries.Text.Substring(0, 2).ToUpper() == "ID") { ddlDocumentTypeID.SelectedIndex = 1; }
            }
        }

        protected void tbDocumentSeries_TextChanged(object sender, EventArgs e)
        {
            if (tbDocumentSeries.Text.Length > 3)
            {
                if (tbDocumentSeries.Text.Substring(0, 2).ToUpper() == "AN") { ddlDocumentTypeID.SelectedIndex = 0; }
                if (tbDocumentSeries.Text.Substring(0, 2).ToUpper() == "ID") { ddlDocumentTypeID.SelectedIndex = 1; }
            }
        }

        class Resp
        {
            public revenue response { get; set; }
            public string status_message { get; set; }
        }

        class revenue
        {
            public string revenue_score { get; set; }
        }

        protected void btnGetScoreBee_Click(object sender, EventArgs e)
        {
            int CustID = Convert.ToInt32(hfCustomerID.Value);
            string phone = "", lnk = "";
            SysController stx = new SysController();
            Customer cust = stx.CustomerGetItem(CustID);
            if (cust.ContactPhone1.Length > 14)
            {
                string q = cust.ContactPhone1;
                phone = "0" + cust.ContactPhone1.Substring(5, 3) + cust.ContactPhone1.Substring(9, 6);
                lnk = "http://beeline-income.beeline.kg/api/customer/income?number=" + phone;
                //string s = getScoreBee(phone);
                //cust.ContactPhone1 = tbContactPhone.Text;
                //ctx.CustomerUpdItem(cust);

                string s, s2 = "";
                try
                {
                    //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://beeline-income.beeline.kg/api/customer/income?number=0772944431");
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(lnk);

                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "GET";

                    //            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var response = streamReader.ReadToEnd();
                        var result = JsonConvert.DeserializeObject<Resp>(response);
                        if (result.response != null) s = result.response.revenue_score.ToString();
                        else s = result.status_message;

                        if ((s == "A") || (s == "А")) s2 = "A, личный доход свыше 28 000 сом в месяц";
                        if ((s == "B") || (s == "В") || (s == "Б")) s2 = "B, личный доход 20 001 - 28 000 сом в месяц";
                        if ((s == "C") || (s == "С")) s2 = "C, личный доход 16 001 - 20 000 сом в месяц";
                        if ((s == "D") || (s == "Д")) s2 = "D, личный доход 10 000 – 16 000 сом в месяц";
                        if ((s == "E") || (s == "Е")) s2 = "E, личный доход до 10 000 сом в месяц";
                        if (s == "0") s2 = "0, Нет личного дохода";

                        int usrID = Convert.ToInt32(Session["UserID"].ToString());
                        DateTime dateTimeNow = dateNow;
                        /**/
                        /*RequestHistory*//*----------------------------------------------------*/
                        CreditController ctx = new CreditController();
                        RequestsHistory newItem = new RequestsHistory()
                        {
                            AgentID = usrID,
                            CreditID = Convert.ToInt32(hfCreditID.Value),
                            CustomerID = Convert.ToInt32(hfCustomerID.Value),
                            StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                            Status = s2,
                            note = cust.ContactPhone1,
                            RequestID = Convert.ToInt32(hfRequestID.Value)
                        };
                        ctx.ItemRequestHistoriesAddItem(newItem);
                        refreshGrid();

                    }

                }
                catch (WebException ex)
                {
                    s = ex.Message;
                }

            }
        }

        protected void btnFix_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            Request editRequest = new Request();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
            editRequest.RequestStatus = "Исправить";
            System.Threading.Thread.Sleep(1000);
            ctlItem.RequestUpd(editRequest);
            refreshGrid();
            string hexRed = "#E47E11";
            Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
            lblStatusRequest.BackColor = _colorRed; hfRequestStatus.Value = "Исправить";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = usrID,
                CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Исправить",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }

        protected void btnFixed_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            Request editRequest = new Request();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
            editRequest.RequestStatus = "Исправлено";
            ctlItem.RequestUpd(editRequest);
            System.Threading.Thread.Sleep(1000);
            refreshGrid();
            string hexRed = "#E47E11";
            Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
            lblStatusRequest.BackColor = _colorRed; hfRequestStatus.Value = "Исправлено";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            RequestsHistory newItem = new RequestsHistory()
            {
                AgentID = usrID,
                CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Исправлено",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }

        protected void chkbxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxSelectAll.Checked)
            {
                for (var i = 0; i < 13; i++)
                {
                    chkbxlistStatus.Items[i].Selected = true;
                }
            }
            else
            {
                for (var i = 0; i < 13; i++)
                {
                    chkbxlistStatus.Items[i].Selected = false;
                }
            }
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProductIndChg();
        }

        public void ddlProductIndChg()
        {
            if (ddlProduct.SelectedValue == "Замат")
            {
                ddlRequestRate.Items.Clear();
                ddlRequestRate.Items.Add("29.90");
            }
            else
            {
                ddlRequestRate.Items.Clear();
                ddlRequestRate.Items.Add("0.00");
            }
            databindDDLRatePeriod();
        }

        protected void btnProfileNano_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Response.Redirect(this.EditUrl("", "", "rptProfileNano"));

        }

        protected async void LinkButton1_Click(object sender, EventArgs e)
        {

            //Response.Redirect("https://admin:{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W@https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096");

        }

        //public async System.Threading.Tasks.Task<string> AtIssueRequestOB(string CustomerID, string CreditID)

        private static async System.Threading.Tasks.Task link1()
        {
            const string url = @"https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096"; // adjust the URL accordingly
            const string userName = @"admin";
            const string password = @"{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                AuthenticationSchemes.Basic.ToString(),
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"))
                );
            var response = await httpClient.GetAsync($"https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }



        public void link2()
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                // var client = HttpClientFactory.Create();

                // using (HttpClient client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://10.120.16.95/");
                    // client.BaseAddress = new Uri("https://" + connectionStringOBAPIAddress + "/");
                    const string userName = @"admin";
                    const string password = @"{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W";
                    string json = "";

                    string url3 = "https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096";
                    //var response = await client.GetAsync(url2);
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                AuthenticationSchemes.Basic.ToString(),
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{userName}:{password}"))
                );
                    //var response = await client.GetAsync(url3);
                    //var response = await httpClient.GetAsync($"https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096");
                    var response = httpClient.GetAsync($"https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096");
                    //var result = await response.Content.ReadAsStringAsync();
                    //var result = response.Content.ReadAsStringAsync();
                    //var result2 = await response2.Content.ReadAsStringAsync();
                    //string result3 = "res:" + result;

                    //JObject jResults2 = JObject.Parse(result);

                    //JArray a = JArray.Parse(result);
                    //string state = getstat3(result);


                    //if ((response.StatusCode == HttpStatusCode.OK) && (state == "0"))
                    //{
                    //    result = "200";
                    //}
                    //else
                    //{
                    //    result = result3;
                    //    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, result3, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //}
                    //return "result" ;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //return "error";

            }
            finally
            {
                ////TextBox1.Text = TextBox1.Text + Response.ToString();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://admin:{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W@https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096");
            //Session["userName"] = "admin";
            //Session["password"] = "{bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W";
            //Response.Redirect("https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096");
            //Server.Transfer("https://umai-stage.balance.kg/mcm-api/dos/clients/7626/files/19096?userName=admin&password={bcrypt}$2y$12$2NifkACWpuREnmUu8vOVM.ncKRzHdsMcThijJJHWyvt9b27WNH.2W");
        }

        protected void ddlRequestRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlRequestRate.Text == "0.00")
            //{
            //    ddlRequestPeriod.Items.Clear();
            //    ddlRequestPeriod.Items.Add("3");
            //    ddlRequestPeriod.Items.Add("6");
            //    ddlRequestPeriod.Items.Add("12");
            //}
            //if ((ddlRequestRate.Text == "32.00"))
            //{
            //    ddlRequestPeriod.Items.Clear();
            //    for (int i = 3; i < 13; i++)
            //    {
            //        ddlRequestPeriod.Items.Add(i.ToString());
            //    }
            //}
            databindDDLRatePeriod();
            refreshProducts(1);
        }

        public static void SendMail2(string body, string to, string efrom, string replyto, string subject)
        {
            try
            {
                if (String.Empty == replyto || replyto.Trim().Length == 0) replyto = "";
                if (efrom == "") efrom = Host.HostEmail;
                efrom = "zamat@doscredobank.kg";
                to = to.Trim();
                if (AgentView.IsValidEmail(to))
                {
                    List<Attachment> la = new List<Attachment> { };
                    string strMessage2 = DotNetNuke.Services.Mail.Mail.SendMail(efrom, to, "", "", replyto, MailPriority.Normal, subject, MailFormat.Html, System.Text.Encoding.UTF8, body, la, Host.SMTPServer, Host.SMTPAuthentication, Host.SMTPUsername, Host.SMTPPassword, Host.EnableSMTPSSL);
                }
            }
            catch (Exception ex)
            {
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "2." + ex.Message, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
            }
        }


    }
}