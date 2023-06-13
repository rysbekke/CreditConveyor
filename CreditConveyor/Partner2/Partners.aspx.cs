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
using СreditСonveyor.Data.Partners2;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Threading;
using System.Net.Http.Headers;
using СreditСonveyor.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace СreditСonveyor.Partners2
{
    public partial class Partners: System.Web.UI.Page
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
        public string connectionStringActualDate = ConfigurationManager.ConnectionStrings["connectionStringActualDate"].ToString();

        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        //public string connectionStringDNN = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=dnn803;User ID=sa;Password=Server2012";
        OleDbConnection oledbConn;
        DateTime dateNowServer, dateNow;
        //protected string partnerdir = "Partcredits";
        protected string partnerdir = "Credits\\Partcredits";

        //string actdate = "2021-11-05T11:28:42"; //88
        //string actdate = "2021-09-20T11:28:42"; //86
        string actdate = ""; //Кола
        public double Overhead = 0; // monthBuy = 0, 
        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                check_security();
                InitializeCulture();
                //if ((Session["Check"] == null) || (Session["Check"].ToString() != "true")) Response.Redirect("/Home");
                //RadNumTbTotalPrice.Culture.NumberFormat.CurrencySymbol = "";
                RadNumTbTotalPrice.ToolTip = "Введите цену";
                if (Session["FIO"] != null) { lblUserName.Text = Session["FIO"].ToString(); }
                //dbdataDataContext db = new dbdataDataContext(connectionString);
                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                dateNowServer = dbR.GetTable<SysInfo>().FirstOrDefault().DateOD;
                dateNow = Convert.ToDateTime(DateTime.Now);
                actdate = connectionStringActualDate;
                if (connectionStringActualDate == "") actdate = dateNowServer.ToString("yyyy-MM-ddT00:00:00");

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
                        //databindDDL();
                        tbDate1b.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
                        tbDate2b.Text = DateTime.Today.Date.ToString("dd.MM.yyyy"); //"yyyy-MM-dd"
                        hfRequestAction.Value = "new";
                        /*-----*/
                        //Session["PostID"] = "1001";
                        //ViewState["PostID"] = Session["PostID"].ToString();
                        /*-----*/
                        btnAddProductClick = 1;
                        
                        
                        isRole();
                        
                        hfUserID.Value = Session["UserID"].ToString();
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
                    //otherdata();
                    //tbNoteCancelReq.Text = "";
                    //tbNoteCancelReqExp.Text = "";
                }
            }
            catch (Exception exc) //Module failed to load
            {
                //Exceptions.ProcessModuleLoadException(this, exc);
                //System.Windows.Forms.MessageBox.Show(exc.ToString());
                MsgBox(exc.ToString(), this.Page, this);
            }
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

        protected override void InitializeCulture()
        {
            CultureInfo CI = new CultureInfo("pt-PT");
            CI.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";

            Thread.CurrentThread.CurrentCulture = CI;
            Thread.CurrentThread.CurrentUICulture = CI;
            base.InitializeCulture();
        }

        public void otherdata(int id)
        {
            //static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["DosCredobankConnectionStringRWZ"].ToString();
            //int RequestID = Convert.ToInt32(Session["RequestID"].ToString());
            //int RequestID = Convert.ToInt32(hfRequestID.Value);

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int commision = 1;
            string NameOfCredit = ""; lblComission.Text = "";
            var requests = dbRWZ.Requests.Where(r => r.RequestID == id).ToList().FirstOrDefault();
            if (requests.OrgID == 27)
            {

                if ((requests.RequestRate == 0) && (requests.RequestPeriod == 3)) { commision = 3; NameOfCredit = "0-0-3"; lblComission.Text = ((requests.RequestSumm * commision) / 100).ToString(); }
                if ((requests.RequestRate == 0) && (requests.RequestPeriod == 6)) { commision = 3; NameOfCredit = "0-0-6"; lblComission.Text = ((requests.RequestSumm * commision) / 100).ToString(); }
                if ((requests.RequestRate == 0) && (requests.RequestPeriod == 12)) { commision = 2; NameOfCredit = "0-0-12"; lblComission.Text = ((requests.RequestSumm * commision) / 100).ToString(); }
                if (requests.RequestRate == decimal.Parse("29.90")) { commision = 300; NameOfCredit = "КапиталБанк"; lblComission.Text = "300"; }
                //lblCommisionType.Text = "%";

            }
        }
        public void isRole()
        {
            if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
            {
                RadNumOtherLoans.Visible = true;
                lblOtherLoans.Visible = true;
                txtBusinessComment.Visible = true;
                lblBusinessComment.Visible = true;
                chkbxlistStatus.Visible = true;
                btnProffer.Visible = false;
                btnIssue.Visible = false;
                btnCancelIssue.Visible = false;
                chkbxGroup.Visible = true;
                btnConfirm.Visible = false;
                btnNoConfirm.Visible = false;
                tbDate2b.Visible = true;
                btnFixed.Visible = false;
                int usrID = Convert.ToInt32(Session["UserID"].ToString());
                if ((usrID == 7420) || (usrID == 7512)) { btnProffer.Visible = true; }
            }
            if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты
            {
                RadNumOtherLoans.Visible = true;
                lblOtherLoans.Visible = true;
                txtBusinessComment.Visible = true;
                lblBusinessComment.Visible = true;
                btnApplicationForm.Visible = true;
                chkbxlistStatus.Visible = true;
                chkbxGroup.Visible = true;
                btnProffer.Visible = true;
                btnApplicationForm.Visible = true;
                btnIssue.Visible = false;
                btnCancelIssue.Visible = false;
                btnConfirm.Visible = false;
                btnNoConfirm.Visible = false;
                btnFix.Visible = false;
                tbDate2b.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
            {
                RadNumOtherLoans.Visible = true;
                lblOtherLoans.Visible = true;
                txtBusinessComment.Visible = true;
                lblBusinessComment.Visible = true;

                chkbxGroup.Visible = true;
                btnProffer.Visible = false;
                btnIssue.Visible = false;
                btnCancelIssue.Visible = false;
                btnFixed.Visible = false;
                tbDate2b.Visible = true;
                chkbxlistStatus.Visible = true;
                chkbxlistKindActivity.Visible = true;
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
                btnApplicationForm.Visible = true;
                btnIssue.Visible = false;
                btnCancelIssue.Visible = false;
                btnConfirm.Visible = false;
                chkbxGroup.Visible = true;
                btnNoConfirm.Visible = false;
                btnFix.Visible = false;
                tbDate2b.Visible = true;
                chkbxlistStatus.Visible = true;
                chkbxlistKindActivity.Visible = true;
                btnForPeriod.Visible = true;
                btnForPeriodWithHistory.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 24) //Эксперты Бизнес-Кола
            {
                tbDate2b.Visible = true;
            }
        }

        public void refreshGrid()
        {
            string dt1 = "", dt2 = "", surname = "", inn = "", name = "", nRequest = "", creditID = ""; 

            if (tbRequestID.Text != "") { nRequest = tbRequestID.Text; }
            if (tbSearchRequestINN.Text != "") { inn = tbSearchRequestINN.Text; }
            if (tbCreditID.Text != "") { creditID = tbCreditID.Text; }
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
            int? agentRoleID = 13; //int? agentRoleID = db.RequestsUsersRoles.Where(r => (r.UserID == usrID)).FirstOrDefault().RoleID;
                                   //1-все заявки Ошка
                                   //3-Тур
                                   //6-все заявки Светофор
                                   //8-Билайн
            var rle = Convert.ToInt32(Session["RoleID"]);
            var users2 = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault();
            int? groupID = users2.GroupID;
            int? orgID = dbRWZ.Groups.Where(g => g.GroupID == groupID).FirstOrDefault().OrgID;
            List<Request> lst;
            if (rle == 5) { lst = ItemController.GetRequestsAllForExpert(usrID, agentRoleID, nRequest, inn, creditID, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize, chkbxOffice.Items[0].Selected, chkbxOffice.Items[1].Selected, chkbxOffice.Items[2].Selected, chkbxOffice.Items[3].Selected, chkbxOffice.Items[4].Selected, chkbxOffice.Items[5].Selected, chkbxOffice.Items[6].Selected, chkbxOffice.Items[7].Selected, chkbxOffice.Items[8].Selected, chkbxOffice.Items[9].Selected); } //Эсперты ГБ Капитал
            else if (rle == 2) { lst = ItemController.GetRequestsForExpert(usrID, agentRoleID, nRequest, inn, creditID, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize, chkbxOffice.Items[0].Selected, chkbxOffice.Items[1].Selected, chkbxOffice.Items[2].Selected, chkbxOffice.Items[3].Selected, chkbxOffice.Items[4].Selected, chkbxOffice.Items[5].Selected, chkbxOffice.Items[6].Selected, chkbxOffice.Items[7].Selected, chkbxOffice.Items[8].Selected, chkbxOffice.Items[9].Selected); } //Эксперты филиала Капитал
            else if (rle == 24) { lst = ItemController.GetRequestsForExpertBusiness(usrID, agentRoleID, nRequest, inn, creditID, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize, chkbxOffice.Items[0].Selected, chkbxOffice.Items[1].Selected, chkbxOffice.Items[2].Selected, chkbxOffice.Items[3].Selected, chkbxOffice.Items[4].Selected, chkbxOffice.Items[5].Selected, chkbxOffice.Items[6].Selected, chkbxOffice.Items[7].Selected, chkbxOffice.Items[8].Selected, chkbxOffice.Items[9].Selected); } //Эксперты филиала Капитал
            else if (rle == 14) { lst = ItemController.GetRequestsAllForAdmin(usrID, agentRoleID, orgID, nRequest, inn, creditID, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize, chkbxOffice.Items[0].Selected, chkbxOffice.Items[1].Selected, chkbxOffice.Items[2].Selected, chkbxOffice.Items[3].Selected, chkbxOffice.Items[4].Selected, chkbxOffice.Items[5].Selected, chkbxOffice.Items[6].Selected, chkbxOffice.Items[7].Selected, chkbxOffice.Items[8].Selected, chkbxOffice.Items[9].Selected); } //Админы Билайн
            else { lst = ItemController.GetRequestsForAgent(usrID, agentRoleID, nRequest, inn, creditID, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected, chkbxlistStatus.Items[11].Selected, chkbxlistStatus.Items[12].Selected, chkbxlistKindActivity.Items[0].Selected, chkbxlistKindActivity.Items[1].Selected, chkbxlistKindActivity.Items[2].Selected, chkbxGroup.Items[0].Selected, chkbxGroup.Items[1].Selected, chkbxGroup.Items[2].Selected, gvRequests.PageIndex, gvRequests.PageSize, chkbxOffice.Items[0].Selected, chkbxOffice.Items[1].Selected, chkbxOffice.Items[2].Selected, chkbxOffice.Items[3].Selected, chkbxOffice.Items[4].Selected, chkbxOffice.Items[5].Selected, chkbxOffice.Items[6].Selected, chkbxOffice.Items[7].Selected, chkbxOffice.Items[8].Selected, chkbxOffice.Items[9].Selected); } //Агенты Нуртелеком
            if (lst.Count > 0)
            {
                var lst4 = (from r in lst
                            join o in dbRWZ.Offices on r.OfficeID equals o.ID
                            select new { r.RequestID, r.CreditID, OfficeShortName = o.ShortName, r.Surname, r.CustomerName, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, r.GroupID, r.OrgID, r.StatusOB, r.RequestRate, r.OfficeID, r.BranchID }).ToList();

                var lst5 = (from r in lst4
                            join b in dbRWZ.Branches on r.BranchID equals b.ID
                            select new { r.RequestID, r.CreditID, b.ShortName, r.OfficeShortName, r.Surname, r.CustomerName, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, r.GroupID, r.OrgID, r.StatusOB, r.RequestRate }).ToList();
                var lst6 = (from r in lst5
                            join g in dbRWZ.Groups on r.GroupID equals g.GroupID
                            select new { r.RequestID, r.CreditID, r.ShortName, r.OfficeShortName, r.Surname, r.CustomerName, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, g.GroupName, r.OrgID, r.StatusOB, r.RequestRate }).ToList();
                var lst7 = (from r in lst6
                            join o in dbRWZ.Organizations on r.OrgID equals o.OrgID
                            orderby r.RequestDate
                            select new { r.RequestID, r.CreditID, r.ShortName, r.OfficeShortName, r.Surname, r.CustomerName, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, r.GroupName, o.OrgName, r.StatusOB, r.RequestRate }).ToList();
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
                gvRequests.Columns[16].Visible = true;
            }
            else
            {
                // gvRequests.Columns[13].Visible = true;
                gvRequests.Columns[16].Visible = false;
            }

        }

        public string getCorrectDatetxt(object o)
        {
            return Convert.ToDateTime(o).Date.ToString("yyyy.MM.dd");
        }


        public void disableCustomerFields()
        {
            tbSurname.Enabled = false;
            tbCustomerName.Enabled = false;
            tbOtchestvo.Enabled = false;
            rbtIsResident.Enabled = false;
            tbIdentificationNumber.Enabled = false;
            tbDocumentSeries.Enabled = false;
            tbIssueDate.Enabled = false;
            tbValidTill.Enabled = false;
            tbIssueAuthority.Enabled = false;
            rbtSex.Enabled = false;
            tbDateOfBirth.Enabled = false;
            tbRegistrationStreet.Enabled = false;
            tbRegistrationHouse.Enabled = false;
            tbRegistrationFlat.Enabled = false;
            tbResidenceStreet.Enabled = false;
            tbResidenceHouse.Enabled = false;
            tbContactPhone.Enabled = false;
            tbResidenceFlat.Enabled = false;
            ddlDocumentTypeID.Enabled = false;
                ddlNationalityID.Enabled = false;
                ddlBirthCountryID.Enabled = false;
                ddlRegistrationCountryID.Enabled = false;
                ddlResidenceCountryID.Enabled = false;
                ddlBirthCityName.Enabled = false;
                ddlRegistrationCityName.Enabled = false;
                ddlResidenceCityName.Enabled = false;

            revContactPhone.Enabled = false;
            rfvContactPhone.Enabled = false;
            revResidenceFlat.Enabled = false;
            rfvResidenceHouse.Enabled = false;
            revResidenceHouse.Enabled = false;
            rfvResidenceStreet.Enabled = false;
            rfvResidenceCityName.Enabled = false;
            revRegistrationHouse.Enabled = false;
            rfvRegistrationHouse.Enabled = false;
            rfvRegistrationStreet.Enabled = false;
            rfvRegistrationCityName.Enabled = false;
            rfvRegistrationCountryID.Enabled = false;
            revDocumentSeries.Enabled = false;

            rbtnlistValidTill.Enabled = false;
            rfvValidTill.Enabled = false;
            revValidTill.Enabled = false;
        }

        public void enableCustomerFields()
        {
            tbSurname.Enabled = true;
            tbCustomerName.Enabled = true;
            tbOtchestvo.Enabled = true;
            rbtIsResident.Enabled = true;
            tbIdentificationNumber.Enabled = true;
            tbDocumentSeries.Enabled = true;
            tbIssueDate.Enabled = true;
            tbValidTill.Enabled = true;
            tbIssueAuthority.Enabled = true;
            rbtSex.Enabled = true;
            tbDateOfBirth.Enabled = true;
            tbRegistrationStreet.Enabled = true;
            tbRegistrationHouse.Enabled = true;
            tbRegistrationFlat.Enabled = true;
            tbResidenceStreet.Enabled = true;
            tbResidenceHouse.Enabled = true;
            tbContactPhone.Enabled = true;
            tbResidenceFlat.Enabled = true;
            ddlDocumentTypeID.Enabled = true;
            ddlNationalityID.Enabled = true;
            ddlBirthCountryID.Enabled = true;
            ddlRegistrationCountryID.Enabled = true;
            ddlResidenceCountryID.Enabled = true;
            ddlBirthCityName.Enabled = true;
            ddlRegistrationCityName.Enabled = true;
            ddlResidenceCityName.Enabled = true;

            revContactPhone.Enabled = true;
            rfvContactPhone.Enabled = true;
            revResidenceFlat.Enabled = true;
            rfvResidenceHouse.Enabled = true;
            revResidenceHouse.Enabled = true;
            rfvResidenceStreet.Enabled = true;
            rfvResidenceCityName.Enabled = true;
            revRegistrationHouse.Enabled = true;
            rfvRegistrationHouse.Enabled = true;
            rfvRegistrationStreet.Enabled = true;
            rfvRegistrationCityName.Enabled = true;
            rfvRegistrationCountryID.Enabled = true;
            revDocumentSeries.Enabled = true;

            //rbtnlistValidTill.Enabled = true;
            rfvValidTill.Enabled = true;
            revValidTill.Enabled = true;
        }


        protected void btnSearchClient_Click(object sender, EventArgs e)
        {
            disableCustomerFields();
            hfIsNewCustomer.Value = "edit";
            clear_positions();
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //var query = (from u in dbR.Customers where ((u.IdentificationNumber == tbSearchINN.Text) && (u.DocumentSeries == tbSerialN.Text.Substring(0, 5)) && (u.DocumentNo == tbSerialN.Text.Substring(5, 4))) select u).ToList().FirstOrDefault();
            var query = (from u in dbR.Customers where ((u.IdentificationNumber.Trim() == tbSearchINN.Text.Trim()) && (String.Concat(u.DocumentSeries, u.DocumentNo).Trim().ToUpper().Replace(" ", "") == tbSerialN.Text.Trim().ToUpper().Replace(" ", ""))) select u).ToList().FirstOrDefault();
            if (query != null)
            {
                pnlCustomer.Visible = true;
                pnlCredit.Visible = false;
                //btnSaveCustomer.Enabled = false;
                btnCredit.Enabled = true;
                if (hfChooseClient.Value == "Выбрать клиента") hfCustomerID.Value = query.CustomerID.ToString();
                else hfGuarantorID.Value = query.CustomerID.ToString();
                tbSurname.Text = query.Surname; 
                tbCustomerName.Text = query.CustomerName;
                tbOtchestvo.Text = query.Otchestvo;
                if (query.IsResident == true) { rbtIsResident.SelectedIndex = 0; } else { rbtIsResident.SelectedIndex = 1; }
                tbIdentificationNumber.Text = query.IdentificationNumber;
                tbDocumentSeries.Text = query.DocumentSeries + query.DocumentNo;
                tbIssueDate.Text = Convert.ToDateTime(query.IssueDate).ToString("dd.MM.yyyy");
                //tbValidTill.Text = Convert.ToDateTime(query.DocumentValidTill).ToString("dd.MM.yyyy");
                if ((query.IsDocUnlimited == false) || (query.IsDocUnlimited is null))
                {
                    tbValidTill.Text = Convert.ToDateTime(query.DocumentValidTill).ToString("dd.MM.yyyy");

                    rbtnlistValidTill.Items[0].Selected = true;
                    rbtnlistValidTill.Items[1].Selected = false;
                }
                else
                {
                    rbtnlistValidTill.Items[0].Selected = false;
                    rbtnlistValidTill.Items[1].Selected = true;
                }
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
                //btnSaveCustomer.Enabled = true;
                btnCredit.Enabled = false;
            }
            //show_positions();
        }


        public void show_positions()
        {
            //Positions     
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            var pos = (from p in dbR.Positions
                       join s in dbR.Schedules on p.SchedID equals s.ID
                       where (p.EmployeeID == Convert.ToInt32(hfCustomerID.Value) && (p.StatusID == 2))
                       select new { s.Shortname, p.DepFullName, p.OrderDate, p.ActualDate }).ToList();
            gvPositions.DataSource = pos;
            gvPositions.DataBind();
            gvPositions2.DataSource = pos;
            gvPositions2.DataBind();
            //  
        }

        public void clear_positions()
        {
            //Positions     
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            gvPositions.DataSource = null;
            gvPositions.DataBind();
            gvPositions2.DataSource = null;
            gvPositions2.DataBind();
            //  
        }

        public void databindDDL()
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
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
                ddlBirthCityName.Items.Add(new System.Web.UI.WebControls.ListItem(rowCity.CityName + ", " + region.RegionName, rowCity.CityID.ToString()));
                ddlRegistrationCityName.Items.Add(new System.Web.UI.WebControls.ListItem(rowCity.CityName + ", " + region.RegionName, rowCity.CityID.ToString()));
                ddlResidenceCityName.Items.Add(new System.Web.UI.WebControls.ListItem(rowCity.CityName + ", " + region.RegionName, rowCity.CityID.ToString()));
            }
            databindDDLRatePeriod(1);
        }

        public void databindDDLRatePeriod(int k)
        {
            /**********/

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int? groupID, orgID;
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int usrRoleID = Convert.ToInt32(dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID);



            if ((usrRoleID == 2) || (usrRoleID == 5))
            {
                if ((hfRequestID.Value != "") && (Convert.ToInt32(hfRequestID.Value) > 0))
                {
                    int reqID = Convert.ToInt32(hfRequestID.Value); //Convert.ToInt32(Session["RequestID"].ToString());
                    groupID = dbRWZ.Requests.Where(r => r.RequestID == reqID).FirstOrDefault().GroupID;
                    orgID = dbRWZ.Requests.Where(r => r.RequestID == reqID).FirstOrDefault().OrgID;
                }
                else
                {
                    groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                    orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
                }
            }
            
            else
            {
                groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
            }





            if (groupID == 30) //Техно - ЦФ
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    // ddlRequestRate.Items.Add("25.00");
                    // ddlRequestRate.Items.Add("29.00");
                    //ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    //ddlRequestPeriod.Items.Add("9");
                    //ddlRequestPeriod.Items.Add("10");
                    //ddlRequestPeriod.Items.Add("12");
                    ddlRequestPeriod.Items.Add("15");
                    ddlRequestPeriod.Items.Add("24");

                }

                if ((ddlRequestRate.Text == "29,90") || (ddlRequestRate.Text == "25,00"))
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 19; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
                if (ddlRequestRate.Text == "29,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 25; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
                //ddlRequestRate.SelectedIndex = 2; 
            }


            if (groupID == 50) // Планета - Берекет
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    // ddlRequestRate.Items.Add("25.00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    //ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");


                }

                //if ((ddlRequestRate.Text == "29,90") || (ddlRequestRate.Text == "25,00"))
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    //try
                    //{
                    //    //if (Convert.ToInt32(RadNumTbRequestSumm.Text) > 100000)
                    //    //{
                    //    //    for (int i = 3; i < 24; i++)
                    //    //    {
                    //    //        ddlRequestPeriod.Items.Add(i.ToString());
                    //    //    }
                    //    //}
                    //    //else
                    //    //{
                    //    //    for (int i = 3; i < 13; i++)
                    //    //    {
                    //    //        ddlRequestPeriod.Items.Add(i.ToString());
                    //    //    }
                    //    //}
                    //}
                    //catch 
                    {
                        for (int i = 3; i < 25; i++)
                        {
                            ddlRequestPeriod.Items.Add(i.ToString());
                        }
                    }


                }


                //if (ddlRequestRate.Text == "29,90")
                //{
                //    ddlRequestPeriod.Items.Clear();
                //    for (int i = 3; i < 25; i++)
                //    {
                //        ddlRequestPeriod.Items.Add(i.ToString());
                //    }
                //}
                //ddlRequestRate.SelectedIndex = 2;f 
            }


            if (groupID == 51) //Сулпак - Берекет
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    //ddlRequestRate.Items.Add("27,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                    ddlRequestPeriod.Items.Add("18");
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    //for (int i = 3; i < 37; i++)
                    //{
                    //    ddlRequestPeriod.Items.Add(i.ToString());
                    //}

                    //try
                    //{
                    //    if (Convert.ToInt32(RadNumTbRequestSumm.Text) > 100000)
                    //    {
                    //        for (int i = 3; i < 24; i++)
                    //        {
                    //            ddlRequestPeriod.Items.Add(i.ToString());
                    //        }
                    //    }
                    //    else
                    //    {
                    //        for (int i = 3; i < 13; i++)
                    //        {
                    //            ddlRequestPeriod.Items.Add(i.ToString());
                    //        }
                    //    }
                    //}
                    //catch
                    {
                        for (int i = 3; i < 25; i++)
                        {
                            ddlRequestPeriod.Items.Add(i.ToString());
                        }
                    }


                }
                if (ddlRequestRate.Text == "27,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 25; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
                //ddlRequestRate.SelectedIndex = 2;
            }

            if (groupID == 62) //Беко - Берекет
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                }
                ddlRequestPeriod.Items.Clear();
                ddlRequestPeriod.Items.Add("6");
                ddlRequestPeriod.Items.Add("12");

            }

            if (orgID == 66) //Гергерт - Берекет
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                }
                ddlRequestPeriod.Items.Clear();
                ddlRequestPeriod.Items.Add("3");
                ddlRequestPeriod.Items.Add("6");
                ddlRequestPeriod.Items.Add("9");
                ddlRequestPeriod.Items.Add("12");
            }


            if (groupID == 10) //Светофор - ЦФ
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("29,90");
                }
                ddlRequestPeriod.Items.Clear();
                for (int i = 3; i < 13; i++)
                {
                    ddlRequestPeriod.Items.Add(i.ToString());
                }
            }




            if (orgID == 16) //Евролюкс- ЦБТ - ЦФ
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    //ddlRequestPeriod.Items.Add("12");
                    ddlRequestPeriod.Items.Add("18");
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }

            if (orgID == 18) // ОсОО КРИК Ирбис - ЦФ
            {
                ddlRequestRate.Items.Clear();
                if (k == 1)
                {
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("12");
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }

            if (orgID == 20) //Аквадом - Берекет
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }

            if (orgID == 21) //Смарт тех - ЦФ
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                    ddlRequestPeriod.Items.Add("18");
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }

            if (orgID == 22) //ИП Байысбеков Доолот Байысбекович - ЦФ
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                    ddlRequestPeriod.Items.Add("18");
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }

            if (orgID == 25) //ИП Мечат - Берекет
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");

                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                }
            }

            //if (orgID == 23) //ИП АСКАРОВ АДЫЛ ЖЕНИШОВИЧ - Токмок
            //{
            //    if (k == 1)
            //    {
            //        ddlRequestRate.Items.Clear();
            //        ddlRequestRate.Items.Add("0.00");
            //        ddlRequestRate.Items.Add("29.90");
            //    }
            //    if (ddlRequestRate.Text == "0.00")
            //    {
            //        ddlRequestPeriod.Items.Clear();
            //        ddlRequestPeriod.Items.Add("3");
            //        ddlRequestPeriod.Items.Add("6");
            //        ddlRequestPeriod.Items.Add("12");
            //    }
            //    if (ddlRequestRate.Text == "29.90")
            //    {
            //        ddlRequestPeriod.Items.Clear();
            //        for (int i = 3; i < 13; i++)
            //        {
            //            ddlRequestPeriod.Items.Add(i.ToString());
            //        }
            //    }
            //}

            if (orgID == 14) //ИП Токтосунова Самара Зайырбековна - Берекет
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }

            if (orgID == 31) //ИП Чокморов Логин кг
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {

                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }


            if (groupID == 75) //Идеа
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }

            if (groupID == 97) //ИП Мищенко Венера Рамильевна
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                    ddlRequestPeriod.Items.Add("18");
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
            }


            //if (groupID == 104) //Мегаком
            //{
            //    if (k == 1)
            //    {
            //        ddlRequestRate.Items.Clear();
            //        ddlRequestRate.Items.Add("0.00");
            //        ddlRequestRate.Items.Add("29.90");
            //    }
            //    if (ddlRequestRate.Text == "0.00")
            //    {
            //        ddlRequestPeriod.Items.Clear();
            //        ddlRequestPeriod.Items.Add("3");
            //        ddlRequestPeriod.Items.Add("6");
            //        ddlRequestPeriod.Items.Add("12");
            //    }
            //    if (ddlRequestRate.Text == "29.90")
            //    {
            //        ddlRequestPeriod.Items.Clear();
            //        for (int i = 3; i < 13; i++)
            //        {
            //            ddlRequestPeriod.Items.Add(i.ToString());
            //        }
            //    }
            //}


            if (groupID == 1) //Капитал
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    // ddlRequestRate.Items.Add("25.00");
                    ddlRequestRate.Items.Add("27,00");
                    // ddlRequestRate.Items.Add("29.00");
                    ddlRequestRate.Items.Add("29,90");
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                    ddlRequestPeriod.Items.Add("15");
                    ddlRequestPeriod.Items.Add("18");
                    ddlRequestPeriod.Items.Add("24");
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 37; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
                if (ddlRequestRate.Text == "27,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 25; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
                //ddlRequestRate.SelectedIndex = 2;
            }

            if (groupID == 127) //Два прораба
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 1; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }

                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                }
            }


            if (groupID == 128) //Сайт ДСБ
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    // ddlRequestRate.Items.Add("20.00");
                    ddlRequestRate.Items.Add("29,90");
                }

                //if (ddlRequestRate.Text == "29.90")
                {
                    ddlRequestPeriod.Items.Clear();
                    //for (int i = 1; i < 7; i++)
                    //{
                    //    ddlRequestPeriod.Items.Add(i.ToString());
                    //}
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                    ddlRequestPeriod.Items.Add("12");
                }
            }

            if (groupID == 134) //Сайт Кока-Колы
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    // ddlRequestRate.Items.Add("20.00");
                    ddlRequestRate.Items.Add("0,0");
                }

                //if (ddlRequestRate.Text == "29.90")
                {
                    ddlRequestPeriod.Items.Clear();
                    //for (int i = 1; i < 7; i++)
                    //{
                    //    ddlRequestPeriod.Items.Add(i.ToString());
                    //}
                    ddlRequestPeriod.Items.Add("12");
                    
                }
            }


            if (orgID == 36) //Тойота
            {
                if (k == 1)
                {
                    ddlRequestRate.Items.Clear();
                    ddlRequestRate.Items.Add("0,00");
                    ddlRequestRate.Items.Add("29,90");
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i <= 9; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }
                if (ddlRequestRate.Text == "0,00")
                {
                    ddlRequestPeriod.Items.Clear();
                    ddlRequestPeriod.Items.Add("3");
                    ddlRequestPeriod.Items.Add("6");
                    ddlRequestPeriod.Items.Add("9");
                }

            }





            /***********/
        }

        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            databindDDL();
            enableCustomerFields();
            hfIsNewCustomer.Value = "new";
            clearEditControls();
            btnSaveCustomer.Enabled = true;
            btnCredit.Enabled = false;
            pnlCustomer.Visible = true;
            pnlCredit.Visible = false;
            hfCustomerID.Value = "noselect";
            ddlDocumentTypeID.SelectedIndex = 0;
            //customerFieldEnable();
            //enableCustomerFields();
            rbtnlistValidTill.Enabled = false;
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
            tbContactPhone.Text = "";
            tbDateOfBirth.Text = "";
            tbCustomerName.Text = "";
        }

        protected void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //DateTime DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4));
                //DateTime DocumentValidTill = Convert.ToDateTime(tbValidTill.Text);
                string DateOfBirth = tbDateOfBirth.Text.Substring(0, 2) + (tbDateOfBirth.Text.Substring(3, 2) + tbDateOfBirth.Text.Substring(6, 4));
                string DateOfBirthINN = (tbIdentificationNumber.Text.Substring(1, 8)); bool f = true;
                //if (DocumentValidTill.Date < System.DateTime.Now.Date.AddDays(1)) { MsgBox("Паспорт проссрочен", this.Page, this); f = false; }
                if (rbtnlistValidTill.Items[0].Selected == true)
                {
                    //DateTime DocumentValidTill = Convert.ToDateTime(tbValidTill.Text); //DateTime.ParseExact(Text, "dd.MM.yyyy", null);
                    DateTime DocumentValidTill = DateTime.ParseExact(tbValidTill.Text, "dd.MM.yyyy", null);
                    if (DocumentValidTill.Date < System.DateTime.Now.Date.AddDays(1)) { MsgBox("Паспорт проссрочен", this.Page, this); f = false; }
                }
                if (DateOfBirth != DateOfBirthINN)
                {
                    if (hfCustomerID.Value == "noselect") { MsgBox("Неправильно введены паспортные данные", this.Page, this); f = false; }
                    else 
                    {
                        if (hfIsNewCustomer.Value == "edit") MsgBox("Не полностью заполнены паспортные данные клиента в базе Банка, обратитесь к кредитному специалисту", this.Page, this); f = false;
                        if (hfIsNewCustomer.Value == "new") MsgBox("Неправильно введены паспортные данные", this.Page, this); f = false;
                    }
                }

                if ((tbIdentificationNumber.Text.Trim().Substring(0, 1) == "1") && (rbtSex.SelectedIndex == 0)) { MsgBox("ИНН и пол клиента не совпадают", this.Page, this); f = false; }
                if ((tbIdentificationNumber.Text.Trim().Substring(0, 1) == "2") && (rbtSex.SelectedIndex == 1)) { MsgBox("ИНН и пол клиента не совпадают", this.Page, this); f = false; }

                if (f)
                {
                    if ((hfCustomerID.Value == "noselect") && (hfGuarantorID.Value == "noselect"))
                    {
                        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                        var cust = dbR.Customers.Where(c => ((c.IdentificationNumber == tbIdentificationNumber.Text) && (c.DocumentSeries == tbDocumentSeries.Text.Substring(0, 5) && (c.DocumentNo == tbDocumentSeries.Text.Substring(5, 4))))).ToList();
                        if (cust.Count == 0)
                        {

                            //string Surname = tbSurname.Text.Trim();
                            //string CustomerName = tbCustomerName.Text.Trim();
                            //string Otchestvo = tbOtchestvo.Text.Trim();
                            //int CustomerTypeID = 1;
                            //int PersonActivityTypeID = 19;
                            //int WorkTypeID = 0;
                            //bool IsResident = (rbtIsResident.SelectedIndex == 0) ? true : false;
                            //string IdentificationNumber = tbIdentificationNumber.Text;
                            //string DocumentSeries = tbDocumentSeries.Text.Substring(0, 5);
                            //string DocumentNo = tbDocumentSeries.Text.Substring(5, 4); //tbDocumentNo.Text,
                            //                                                           //IssueDate = Convert.ToDateTime(tbIssueDate.Text.Substring(3, 2) + "." + tbIssueDate.Text.Substring(0, 2) + "." + tbIssueDate.Text.Substring(6, 4)),
                            //DateTime IssueDate = Convert.ToDateTime(tbIssueDate.Text);
                            ////DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4)),
                            //DateTime DocumentValidTill2 = Convert.ToDateTime(tbValidTill.Text);
                            //string IssueAuthority = tbIssueAuthority.Text;
                            //bool Sex = (rbtSex.SelectedIndex == 0) ? true : false;
                            ////DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text.Substring(3, 2) + "." + tbDateOfBirth.Text.Substring(0, 2) + "." + tbDateOfBirth.Text.Substring(6, 4)),
                            //DateTime DateOfBirth2 = Convert.ToDateTime(tbDateOfBirth.Text);
                            //string RegistrationStreet = tbRegistrationStreet.Text;
                            //string RegistrationHouse = tbRegistrationHouse.Text;
                            //string RegistrationFlat = tbRegistrationFlat.Text;
                            //string ResidenceStreet = tbResidenceStreet.Text;
                            //string ResidenceHouse = tbResidenceHouse.Text;
                            //string ResidenceFlat = tbResidenceFlat.Text;
                            //int DocumentTypeID = Convert.ToInt32(ddlDocumentTypeID.SelectedValue);
                            //int NationalityID = Convert.ToInt32(ddlNationalityID.SelectedItem.Value);
                            //int BirthCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value);
                            // int RegistrationCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value);
                            //int ResidenceCountryID = Convert.ToInt32(ddlResidenceCountryID.SelectedItem.Value);
                            //    //BirthCityID = Convert.ToInt32(ddlBirthCityName.SelectedItem.Value),
                            //int RegistrationCityID = Convert.ToInt32(ddlRegistrationCityName.SelectedItem.Value);
                            //int ResidenceCityID = Convert.ToInt32(ddlResidenceCityName.SelectedItem.Value);
                            // string ContactPhone1 = tbContactPhone.Text;


                            //Customer newItem = new Customer
                            //{
                            //    Surname = tbSurname.Text.Trim(),
                            //    CustomerName = tbCustomerName.Text.Trim(),
                            //    Otchestvo = tbOtchestvo.Text.Trim(),
                            //    CustomerTypeID = 1,
                            //    PersonActivityTypeID = 19,
                            //    WorkTypeID = 0,
                            //    IsResident = (rbtIsResident.SelectedIndex == 0) ? true : false,
                            //    IdentificationNumber = tbIdentificationNumber.Text,
                            //    DocumentSeries = tbDocumentSeries.Text.Substring(0, 5),
                            //    DocumentNo = tbDocumentSeries.Text.Substring(5, 4), //tbDocumentNo.Text,
                            //    //IssueDate = Convert.ToDateTime(tbIssueDate.Text.Substring(3, 2) + "." + tbIssueDate.Text.Substring(0, 2) + "." + tbIssueDate.Text.Substring(6, 4)),
                            //    IssueDate = Convert.ToDateTime(tbIssueDate.Text),
                            //    //DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4)),
                            //    DocumentValidTill = Convert.ToDateTime(tbValidTill.Text),
                            //    IssueAuthority = tbIssueAuthority.Text,
                            //    Sex = (rbtSex.SelectedIndex == 0) ? true : false,
                            //    //DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text.Substring(3, 2) + "." + tbDateOfBirth.Text.Substring(0, 2) + "." + tbDateOfBirth.Text.Substring(6, 4)),
                            //    DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text),
                            //    RegistrationStreet = tbRegistrationStreet.Text,
                            //    RegistrationHouse = tbRegistrationHouse.Text,
                            //    RegistrationFlat = tbRegistrationFlat.Text,
                            //    ResidenceStreet = tbResidenceStreet.Text,
                            //    ResidenceHouse = tbResidenceHouse.Text,
                            //    ResidenceFlat = tbResidenceFlat.Text,
                            //    DocumentTypeID = Convert.ToInt32(ddlDocumentTypeID.SelectedValue),
                            //    NationalityID = Convert.ToInt32(ddlNationalityID.SelectedItem.Value),
                            //    BirthCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value),
                            //    RegistrationCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value),
                            //    ResidenceCountryID = Convert.ToInt32(ddlResidenceCountryID.SelectedItem.Value),
                            //    //BirthCityID = Convert.ToInt32(ddlBirthCityName.SelectedItem.Value),
                            //    RegistrationCityID = Convert.ToInt32(ddlRegistrationCityName.SelectedItem.Value),
                            //    ResidenceCityID = Convert.ToInt32(ddlResidenceCityName.SelectedItem.Value),
                            //    ContactPhone1 = tbContactPhone.Text,
                            //};
                            //SysController ctx = new SysController();
                            //ctx.CustomerAddItem(newItem);

                            GeneralController gctx = new GeneralController();

                            dynamic dCustomer = new System.Dynamic.ExpandoObject();
                            {
                                dCustomer.Data = new System.Dynamic.ExpandoObject();
                                dCustomer.CustomerValidatorTypeId = 0;
                            }

                            //dCustomer.Data.Add(data);
                            bool isDocUnlimited = false;

                            dCustomer.Data.Surname = tbSurname.Text.Trim();
                            dCustomer.Data.CustomerName = tbCustomerName.Text.Trim();
                            dCustomer.Data.Otchestvo = tbOtchestvo.Text.Trim();
                            dCustomer.Data.CustomerTypeID = 1;
                            //    PersonActivityTypeID = 19,
                            //    WorkTypeID = 0,
                            dCustomer.Data.IsResident = (rbtIsResident.SelectedIndex == 0) ? true : false;
                            dCustomer.Data.IdentificationNumber = tbIdentificationNumber.Text;
                            dCustomer.Data.DocumentSeries = tbDocumentSeries.Text.Substring(0, 5);
                            dCustomer.Data.DocumentNo = tbDocumentSeries.Text.Substring(5, 4);
                            dCustomer.Data.IssueDate = Convert.ToDateTime(tbIssueDate.Text); 
                            //dCustomer.Data.DocumentValidTill = Convert.ToDateTime(tbValidTill.Text);
                            if (rbtnlistValidTill.Items[0].Selected == true)
                            {
                                dCustomer.Data.DocumentValidTill = Convert.ToDateTime(tbValidTill.Text);
                                isDocUnlimited = false;
                            }
                            else
                            {
                                isDocUnlimited = true;
                            }
                            dCustomer.Data.IsDocUnlimited = isDocUnlimited;
                            dCustomer.Data.IssueAuthority = tbIssueAuthority.Text;
                            dCustomer.Data.Sex = (rbtSex.SelectedIndex == 0) ? true : false;
                            //dCustomer.Data.DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text); //DateTime.ParseExact(Text, "dd.MM.yyyy", null);
                            dCustomer.Data.DateOfBirth = DateTime.ParseExact(tbDateOfBirth.Text, "dd.MM.yyyy", null);
                            dCustomer.Data.RegistrationStreet = tbRegistrationStreet.Text;
                            dCustomer.Data.RegistrationHouse = tbRegistrationHouse.Text;
                            dCustomer.Data.RegistrationFlat = tbRegistrationFlat.Text;
                            dCustomer.Data.ResidenceStreet = tbResidenceStreet.Text;
                            dCustomer.Data.ResidenceHouse = tbResidenceHouse.Text;
                            dCustomer.Data.ResidenceFlat = tbResidenceFlat.Text;
                            dCustomer.Data.DocumentTypeID = Convert.ToInt32(ddlDocumentTypeID.SelectedValue);
                            dCustomer.Data.NationalityID = Convert.ToInt32(ddlNationalityID.SelectedItem.Value);
                            dCustomer.Data.BirthCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value);
                            dCustomer.Data.RegistrationCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value);
                            dCustomer.Data.ResidenceCountryID = Convert.ToInt32(ddlResidenceCountryID.SelectedItem.Value);
                            dCustomer.Data.BirthCityID = Convert.ToInt32(ddlBirthCityName.SelectedItem.Value);
                            dCustomer.Data.RegistrationCityID = Convert.ToInt32(ddlRegistrationCityName.SelectedItem.Value);
                            dCustomer.Data.ResidenceCityID = Convert.ToInt32(ddlResidenceCityName.SelectedItem.Value);
                            dCustomer.Data.ContactPhone1 = tbContactPhone.Text;

                            //dRequest.Partners.Add(partner);
                            string result = gctx.CreateCustomerWithAPI(dCustomer);


                            //hfCustomerID.Value = newItem.CustomerID.ToString();
                            hfCustomerID.Value = result.ToString();
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
                            //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Клиент есть в базе, сделайте поиск по ИНН", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //System.Windows.Forms.MessageBox.Show("Клиент есть в базе, сделайте поиск по ИНН");
                            MsgBox("Клиент есть в базе, сделайте поиск по ИНН", this.Page, this);
                        }
                        /*btnSave_click*/
                    }
                    else
                    {
                        /*Редактирование*/
                        int CustID;
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
                        //ctx.CustomerUpdItem(cust);
                        pnlCredit.Visible = true;
                        pnlMenuCustomer.Visible = false;
                        pnlCustomer.Visible = false;
                        if (hfChooseClient.Value == "Выбрать клиента")
                        {
                            hfCustomerID.Value = hfCustomerID.Value;
                            tbSurname2.Text = tbSurname.Text;
                            tbCustomerName2.Text = tbCustomerName.Text;
                            tbOtchestvo2.Text = tbOtchestvo.Text;
                            tbINN2.Text = tbIdentificationNumber.Text;
                        }
                        if (hfChooseClient.Value == "Выбрать поручителя")
                        {
                            hfCustomerID.Value = hfCustomerID.Value;
                            tbGuarantorSurname.Text = tbSurname.Text;
                            tbGuarantorName.Text = tbCustomerName.Text;
                            tbGuarantorOtchestvo.Text = tbOtchestvo.Text;
                            tbGuarantorINN.Text = tbIdentificationNumber.Text;
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
                btnCustomerEdit.Enabled = false;
            }
            if (btnCredit.Text == "Выбрать поручителя")
            {
                hfGuarantorID.Value = hfGuarantorID.Value;
                tbGuarantorSurname.Text = tbSurname.Text;
                tbGuarantorName.Text = tbCustomerName.Text;
                tbGuarantorOtchestvo.Text = tbOtchestvo.Text;
                tbGuarantorINN.Text = tbIdentificationNumber.Text;
                btnGuarantorEdit.Enabled = true;
            }
            //clear_positions();
        }



        //public int issuanceOfCredit()
        //{
        //    //int f = 0; int y22 = 40;
        //    ///****************/
        //    //dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        //    //var request = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
        //    ////if ((request != null)) && (request.GroupID == 128) && ((Convert.ToInt32(RadNumTbRequestSumm.Value) > 15000) || (Convert.ToInt32(RadNumTbRequestSumm.Value) < 3000)))
        //    //if ((request != null))
        //    //{
        //    //    f = issuanceOfCreditScor();
        //    //}
        //    //else
        //    //{
        //    //    f = issuanceOfCreditScor();
        //    //}
        //    int f = issuanceOfCreditScor();
        //    return f;
        //}


        public int issuanceOfCreditSum(double stavka, int groupID, double s, int f)
        {
            if (stavka == 0)
            {
                if ((groupID == 50) || (groupID == 51)) //Сулпак, Планета
                {
                    if (s > 250000) f = -250000;
                }
                else
                if ((groupID == 135) || (groupID == 136) || (groupID == 137) || (groupID == 138) || (groupID == 141)) //Тойота
                {
                    if (s > 200000) f = -200000;
                }
                else
                {
                    if (s > 100000) f = -100000;
                }
            }
            else
            {
                if ((groupID == 50) || (groupID == 51)) //Сулпак, Планета
                {
                    if (s > 250000) f = -250000;
                }
                else
                 if ((groupID == 135) || (groupID == 136) || (groupID == 137) || (groupID == 138) || (groupID == 141)) //Тойота
                {
                    if (s > 200000) f = -200000;
                }
                else
                {
                    if (s > 100000) f = -100000;
                }
            }
            return f;
        }



        public int issuanceOfCredit()
        {

            int f = 0; int y22 = 40, groupID = 0;
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var request = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
            if (request != null)
            { groupID = Convert.ToInt32(request.GroupID); }
            else
            { groupID = Convert.ToInt32(dbRWZ.Users2s.Where(u => u.UserID == Convert.ToInt32(hfUserID.Value)).FirstOrDefault().GroupID); }

            //if ((request != null) && (request.GroupID == 128)) //онлайн
            if (groupID == 128) //онлайн
            {
                //if (request.GroupID == 128) //онлайн
                {
                    if ((Convert.ToDouble(RadNumTbRequestSumm.Text) > 50000) || (Convert.ToDouble(RadNumTbRequestSumm.Text) < 3000))
                        f = -50001;
                    else f = 0;
                }
            }
            //else //кроме онлайн 
            {
                /**************/
                double i, k = 0;
                double s = Convert.ToDouble(RadNumTbRequestSumm.Text);
                double n = Convert.ToDouble(ddlRequestPeriod.Text);
                double stavka = Convert.ToDouble(ddlRequestRate.SelectedItem.Value);
                i = (stavka != 0) ? stavka / 12 / 100 : 0;
                if (stavka == 0) k = s / n;
                if ((stavka == 0) && (n == 10)) k = s / n + s / 100;
                if ((stavka == 0) && (n == 15)) k = s / n + s / 100;
                if ((stavka == 25) || (stavka == 29) || (stavka == 29.90)) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);

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
                    if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                    double y2 = 100 * (k + OtherLoans) / (zp + additionalIncome);
                    if ((zp > 50000) || (zp == 50000)) y22 = 50;
                    if (zp < 50000) y22 = 40;

                    if ((y1 >= 20) && (y2 < y22)) f = 1;
                    f = issuanceOfCreditSum(stavka, groupID, s, f);
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
                        if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                        double y2 = 100 * (k + OtherLoans) / (valp + additionalIncome);
                        if ((y1 >= 20) && (y2 < y22)) f = 1;
                    }
                    f = issuanceOfCreditSum(stavka, groupID, s, f);
                }
                if (rbtnBusiness.SelectedIndex == 2) //Агро
                {
                    double RevenueAgro = Convert.ToDouble(RadNumTbRevenueAgro.Text);
                    double RevenueMilk = Convert.ToDouble(RadNumTbRevenueMilk.Text);
                    double RevenueMilkProd = Convert.ToDouble(RadNumTbRevenueMilkProd.Text);
                    double OverheadAgro = Convert.ToDouble(RadNumTbOverheadAgro.Text);
                    OverheadAgro = (OverheadAgro > 0) ? OverheadAgro / 3 : 0;
                    double AddOverheadAgro = Convert.ToDouble(RadNumTbAddOverheadAgro.Text);
                    double FamilyExpensesAgro = Convert.ToDouble(RadNumTbFamilyExpenses.Text);
                    double v = Convert.ToDouble(RevenueAgro) / 3 + (Convert.ToDouble(RevenueMilk) * 30) + (Convert.ToDouble(RevenueMilkProd) * 30);
                    double valp = v - OverheadAgro - AddOverheadAgro;
                    double chp = (valp + additionalIncome) - Convert.ToDouble(FamilyExpensesAgro);
                    if ((valp > 50000) || (valp == 50000)) y22 = 50;
                    if (valp < 50000) y22 = 40;
//                    if (chp > 0)
                    {
                        double cho = chp - k - OtherLoans;
                        double y1 = 100 * cho / chp;
                        if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                        double y2 = 100 * (k + OtherLoans) / (valp + additionalIncome);
                        if ((y1 >= 20) && (y2 < y22)) f = 1;
                    }
                    if (s > 50000) f = -50000;
                }
                /***********/
                if (groupID == 134)
                {
                    k = Convert.ToDouble(request.RequestSumm);
                    double revenueMonth = Convert.ToDouble(hfRevenueMonth.Value);
                        double costprice = Convert.ToDouble(RadNumTbСostPrice.Text);
                        double v = Convert.ToDouble(revenueMonth);
                        double averageCost = (Convert.ToDouble(revenueMonth) / 1.25);
                        double valp = v - Convert.ToDouble(averageCost);
                        double overHead = Convert.ToDouble(request.Overhead);
                        //lblValP.Text = String.Format("{0:0.00}", valp);
                        double businessProfit = Convert.ToDouble(valp - Convert.ToDouble(request.Overhead));
                        double totalСonsumption = Convert.ToDouble(request.Overhead + request.FamilyExpenses);
                        double totalIncome =Convert.ToDouble(businessProfit) + Convert.ToDouble(RadNumTbAdditionalIncome.Text);
                        double chp = (Convert.ToDouble(totalIncome) - Convert.ToDouble(RadNumTbFamilyExpenses.Text));
                        //if (chp > 0)
                        
                            double cho = Convert.ToDouble(revenueMonth - k - OtherLoans - Convert.ToDouble(totalСonsumption));
                            double y1 = 100 * cho / chp;
                            if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                    //if (y1 > 25) f2 = true;
                    // if (OtherLoans <= Convert.ToDouble(hfOtherLoans2.Value)) f2 = true;
                    // if ((y1 > 25) && (f2 == true)) f2 = true;
                    // else lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
                    SysController sysCtrl = new SysController();
                    var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
                    //var findate = dbRWZ.FinDataColas.Where(r => r.INN == customers.IdentificationNumber).FirstOrDefault();
                    var findate = dbRWZ.FinDataColas.Where(r => r.Number == request.NumberCola).FirstOrDefault();
                    int segmentSum = 0;
                    try
                    {

                        switch (findate.Segment)
                        {
                            case "BASIC": segmentSum = 100000; break;
                            case "GOLD": segmentSum = 100000; break;
                            case "SILVER": segmentSum = 70000; break;
                            case "BRONZE": segmentSum = 50000; break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox("Клиент не найден", this.Page, this);

                    }

                    if (y1 > 25) f = 1;
                    //if (s > 100000) f = -100000;
                    if (s > segmentSum) f = -1*segmentSum;
                    if (Convert.ToDouble(RadNumOtherLoans.Text) > Convert.ToDouble(hfOtherLoans2.Value)) f = -70001;
                }
                if ((groupID == 135) || (groupID == 136) || (groupID == 137) || (groupID == 138) || (groupID == 141)) //тойота
                {
                    //if (request.GroupID == 128) //онлайн
                    {
                        if ((Convert.ToDouble(RadNumTbRequestSumm.Text) > 200000))
                            f = -200000;
                        
                    }
                }
            }
            return f;
        }


        protected void btnSendCreditRequest_Click(object sender, EventArgs e)
        {
            int groupID; int ageUpLimit = 0, ageDownLimit = 0;


            if (hfCustomerID.Value != "noselect" && Page.IsValid) //условие для поруч
            {
                groupID = getGroupIDfromUserID(Convert.ToInt32(Session["UserID"].ToString()));
                if (chbEmployer.Checked) // если сотрудник
                {
                    if ((Convert.ToInt32(Session["RoleID"]) == 13) || (Convert.ToInt32(Session["RoleID"]) == 14)) { AddRequest(); } //и (агент и админу то без скорринга)
                    else //не агент значить проверяем скорринг
                    {
                        int f = issuanceOfCredit();
                        if (Convert.ToDouble(RadNumTbFamilyExpenses.Text) < 7000) f = -7000;
                        if (f == 1)
                        {
                            AddRequest();
                        }
                        if (f == 0) { MsgBox("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита", this.Page, this); }
                        if (f == -50000) { MsgBox("Максимальная сумма кредита 50000 сом", this.Page, this); }
                        if (f == -100000) { MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this); }
                        if (f == -7000) { MsgBox("Минимальная сумма семейных расходов - 7000 сом", this.Page, this);
                        }
                    }
                }
                else // если не сотрудник, то скорринг
                {
                    int age = GetCustomerAge(Convert.ToInt32(hfCustomerID.Value));
                    //MsgBox(age.ToString(), this.Page, this);
                    bool fage = false; byte ageEndCredit = 0;


                    if (rbtnBusiness.SelectedIndex == 2) //Агро
                    {
                        ageUpLimit = 70;
                        ageDownLimit = 24;
                        if ((groupID == 135) || (groupID == 136) || (groupID == 137) || (groupID == 138) || (groupID == 141))
                        {
                            ageUpLimit = 66; ageDownLimit = 25;
                        }

                        if ((age > ageDownLimit) && (age < ageUpLimit))
                        {
                            fage = true;
                        }
                        else
                        {
                            if (ageUpLimit == 70) MsgBox("Выдача кредита не возможна, возрастное ограничение 23-70 (не должно превышать 70 на момент окончания кредита клиенту)", this.Page, this);
                            if (ageUpLimit == 66) MsgBox("Выдача кредита не возможна, возрастное ограничение 23-65", this.Page, this);
                            fage = false;
                        }
                    }
                    else
                    {
                        //ageUpLimit = (ageUpLimit == 0) ? 70 : ageUpLimit;
                        //ageDownLimit = (ageDownLimit == 0) ? 22 : ageDownLimit;
                        ageUpLimit = 70;
                        ageDownLimit = 22;
                        if ((groupID == 135) || (groupID == 136) || (groupID == 137) || (groupID == 138) || (groupID == 141))
                        {
                            ageUpLimit = 66; ageDownLimit = 22;
                        }

                        if ((age > ageDownLimit) && (age < ageUpLimit))
                        {
                            fage = true;
                        }
                        else
                        {
                            fage = false;
                            if (ageUpLimit == 70) MsgBox("Выдача кредита не возможна, возрастное ограничение 23-70 (не должно превышать 70 на момент окончания кредита клиенту)", this.Page, this);
                            if (ageUpLimit == 66) MsgBox("Выдача кредита не возможна, возрастное ограничение 23-65", this.Page, this);
                        }
                    }

                    

                     
                    ageEndCredit = GetCustomerAgeWhenEndCredit(Convert.ToInt32(hfCustomerID.Value), Convert.ToInt32(ddlRequestPeriod.SelectedValue));
                    if ((groupID == 135) || (groupID == 136) || (groupID == 137) || (groupID == 138) || (groupID == 141)) ageEndCredit = 1;
                    if (ageEndCredit == 0) { MsgBox("Выдача кредита не возможна, возрастное ограничение 23-70 (не должно превышать 70 на момент окончания кредита клиенту)", this.Page, this); }
                    if (ageEndCredit == 2) { MsgBox("Не полностью заполнены паспортные данные клиента в базе Банка, обратитесь к кредитному специалисту", this.Page, this); }
                    

                    int f = issuanceOfCredit();
                    if (Convert.ToDouble(RadNumTbFamilyExpenses.Text) < 7000) f = -7000;
                    if (f == 1)
                    {
                     
                        if (ddlRequestRate.Text == "29,90")
                            if ((Convert.ToDouble(RadNumTbRequestSumm.Text) <= 100000) && (Convert.ToInt32(ddlRequestPeriod.SelectedValue) > 12))
                            {
                                f = -12;
                            }


                    }
                    
                    if ((f == 1) && (fage) && (ageEndCredit == 1))
                    {
                        AddRequest();
                    }
                    if (f == 0) { MsgBox("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита", this.Page, this); }
                    if (f == -50000) { MsgBox("Максимальная сумма кредита 50000 сом", this.Page, this); }
                    if (f == -100000) { MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this); }
                    if (f == -250000) { MsgBox("Максимальная сумма кредита 250000 сом", this.Page, this); }
                    if (f == -200000) { MsgBox("Максимальная сумма кредита 200000 сом", this.Page, this); }
                    if (f == -50001) { MsgBox("Cумма кредита должна быть от 3000 до 50000 сом", this.Page, this); }
                    if (f == -70001)
                    {
                        if (hfOtherLoans2.Value == "20000") MsgBox("Взносы по другим кредитам не должны превышать 20000", this.Page, this);
                        if (hfOtherLoans2.Value == "50000") MsgBox("Взносы по другим кредитам не должны превышать 50000", this.Page, this);
                        if (hfOtherLoans2.Value == "70000") MsgBox("Взносы по другим кредитам не должны превышать 70000", this.Page, this);
                        if (hfOtherLoans2.Value == "70000") MsgBox("Взносы по другим кредитам не должны превышать 70000", this.Page, this);
                    }
                    if (f == -7000) { MsgBox("Минимальная сумма семейных расходов - 7000 сом", this.Page, this);  }

                    if (f == -12) { MsgBox("Для суммы до 100 000 сом, максимальный срок от 3-12 мес. ", this.Page, this); }
                    
                    if (groupID == 134)
                    {
                        if (f == -100000) { MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this); }
                        if (f == -70000) { MsgBox("Максимальная сумма кредита 70000 сом", this.Page, this); }
                        if (f == -50000) { MsgBox("Максимальная сумма кредита 50000 сом", this.Page, this); }
                    }
                }
            }
            else
            {
                lblMessageClient.Text = "Перед сохранением заявки нужно выбрать клиента";
            }
        }

        protected int GetCustomerAge(int CustomerId)
        {
            int age = 0;
            try
            {
                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                DateTime birthDate = dbR.Customers.Where(c => c.CustomerID == CustomerId).FirstOrDefault().DateOfBirth.Value;
                DateTime nowDate = DateTime.Today;
                age = nowDate.Year - birthDate.Year;
                if (birthDate > nowDate.AddYears(-age)) age--;
            }
            catch (Exception ex)
            {
                MsgBox("Ошибка в паспортных данных в ОБ", this.Page, this);
            }
            return age;
        }



        protected byte GetCustomerAgeWhenEndCredit(int CustomerId, int reqPeriod)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //DateTime birthDate = dbR.Customers.Where(c => c.CustomerID == CustomerId).FirstOrDefault().DateOfBirth.Value;
            //DateTime nowDate = DateTime.Today;
            //int age = (nowDate.Year - birthDate.Year);
            //DateTime birthDateCredit = birthDate.AddMonths(-Convert.ToInt32(reqPeriod));
            //if (birthDate > nowDate.AddYears(-age)) age--;
            //return age;
            byte b = 0;

            //DateTime birthDate = Convert.ToDateTime("01/01/0001"); 
            DateTime birthDate = DateTime.ParseExact("01.01.0001", "dd.MM.yyyy", null);
            try
            {
                birthDate = dbR.Customers.Where(c => c.CustomerID == CustomerId).FirstOrDefault().DateOfBirth.Value;
            }
            catch (Exception ex)
            {
                b = 2;
            }


            DateTime nowDate = DateTime.Today;
            DateTime dateEndCredit = nowDate.AddMonths(Convert.ToInt32(reqPeriod));


            //int age = (nowDate - birthDate).Days;
            int ageWhenEndCredit = (dateEndCredit - birthDate).Days;
            if (ageWhenEndCredit < 25567) b = 1; // 25567 = 70*365 + 17(високосных дней)
            else b = 0;
            return b;
        }

        protected void AddRequest()
        {
            decimal MinRevenue = (RadNumTbMinRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMinRevenue.Text) : 0;
            decimal MaxRevenue = (RadNumTbMaxRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMaxRevenue.Text) : 0;

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            if (hfRequestAction.Value == "new")
            {
                /*Новая заявка*/
                int usrID = Convert.ToInt32(Session["UserID"].ToString());
                int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
                int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
                int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                int? GroupCode = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupCode;
                int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
                int branchID = dbR.Offices.Where(r => r.ID == officeID).FirstOrDefault().BranchID;
                int? credOfficerID = 4583; //по умолч АЙбек
                credOfficerID = dbRWZ.RequestsRedirect.Where(r => r.branchID == branchID).FirstOrDefault().creditOfficerID;
                if ((officeID == 1133) || (officeID == 1134)) credOfficerID = dbRWZ.RequestsRedirect.Where(r => r.officeID == officeID).FirstOrDefault().creditOfficerID;

                hfAgentRoleID.Value = usrRoleID.ToString();
                DateTime dateTimeServer = dateNowServer;
                DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now);

                //int prodID = 1152;
                //if (chbEmployer.Checked) prodID = 1153; else prodID = 1152;

                decimal commision = 0; string NameOfCredit = "КапиталБанк"; int prodID = 1152; //КапиталБанк
                if (chbEmployer.Checked) prodID = 1153; else prodID = 1152; //КапиталБанк для сотруд иначе КапиталБанк
                decimal rate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);
                byte period = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                bool isEmployer = chbEmployer.Checked ? true : false;
                if (Convert.ToDecimal(RadNumTbRequestSumm.Text) > 100000) { NameOfCredit = "Потребительский"; prodID = 109; }
                if ((rate == 0) && (period == 3)) { commision = 300; NameOfCredit = "0-0-3"; prodID = 1163; }
                if ((rate == 0) && (period == 6)) { commision = 300; NameOfCredit = "0-0-6"; prodID = 1164; }
                if ((rate == 0) && (period == 9)) { commision = 300; NameOfCredit = "0-0-9"; prodID = 1165; }
                if ((rate == 0) && (period == 12)) { commision = 300; NameOfCredit = "0-0-12"; prodID = 1177; }
                if ((rate == 0) && (period == 18)) { commision = 300; NameOfCredit = "0-0-18"; prodID = 1190; }

                if (orgID == 11) //Сулпак
                {
                    if ((rate == 0) && (period == 24)) { commision = 300; NameOfCredit = "0-0-24"; prodID = 1181; }
                }
                if (orgID == 6) //Техно
                {
                    if ((rate == 0) && (period == 24)) { commision = 300; NameOfCredit = "0-0-24"; prodID = 1192; }
                }
                //if ((rate == 0) && (period == 27)) { commision = 300; NameOfCredit = "0-0-27"; prodID = 1179; }
                //if ((rate == 0) && (period == 10)) { commision = 300; NameOfCredit = "10-10-10"; prodID = 1168; }
                if ((rate == 0) && (period == 15)) { commision = 0; NameOfCredit = "0-0-15"; prodID = 1169; }
                if (isEmployer == true) commision = 0;
                if (groupID == 128) prodID = 1206;

                //History newItemHistory = new History
                //{
                //    ProductID = prodID, // КапиталБанк для сотрудников
                //    RequestDate = dateTimeServer, // Convert.ToDateTime(DateTime.Now),
                //    RequestCurrencyID = 417,
                //    RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                //    RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value),
                //    PaymentSourceID = 1, //1-зп 2-бизнес
                //    LanguageID = 1,
                //    //RequestMortrageTypeID = 14, //14-поручительство, 1-недвижимость
                //    IncomeApproveTypeID = 1, //непонятно
                //                             //LoanLocation = 
                //                             //MarketingSourceID = 
                //                             //RequestGrantComission
                //                             //RequestGrantComissionType
                //    OfficeID = officeID,  // 1105-центральный // officeID,
                //    LoanPurposeTypeID = 2,
                //    CalculationTypeID = 1,
                //    //PartnerCompanyID =  GroupCode, //1866336, 
                //    ApprovedCurrencyID = 417,
                //    ApprovedPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                //    ApprovedRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value),
                //    //ApprovedGrantComission = commision,
                //    ApprovedGrantComissionType = 1

                //};
                //CreditController ctx = new CreditController();
                //int CreditsHistoriesID = ctx.HistoriesAddItem(newItemHistory);

                ///*----------------------------------------------------*/

                //PartnersHistory parthis = new PartnersHistory()
                //{
                //    CompanyID = (int)GroupCode,
                //    CreditID = CreditsHistoriesID,
                //    TranchID = 1,
                //    SumV = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                //    ComissionSum = 0,
                //    IssueComissionPaymentTypeID = 1
                //};
                //ctx.PartnerHistoriesAddItem(parthis);
                ///*----------------------------------------------------*/

                //HistoriesCustomer newItemHistoriesCustomer = new HistoriesCustomer
                //{
                //    CreditID = CreditsHistoriesID,
                //    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                //    IsLeader = true,
                //    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                //    ApprovedSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                //    CreditPurpose = tbCreditPurpose.Text,
                //    //ApprovedSumm = /непонятно
                //};
                //ctx.HistoriesCustomerAddItem(newItemHistoriesCustomer);
                ///*----------------------------------------------------*/
                //var ProductsEarlyPaymentComissions = dbR.ProductsEarlyPaymentComissions.Where(r => r.ProductID == prodID).ToList().OrderBy(o => o.ChangeDate);
                //decimal? RequestPartialComissionType = ProductsEarlyPaymentComissions.LastOrDefault().PartialComission;
                //decimal? RequestFullPaymentComissionType = ProductsEarlyPaymentComissions.LastOrDefault().FullPaymentComission;
                //HistoriesEarlyPaymentComission newItemHistoriesEarlyPaymentComission = new HistoriesEarlyPaymentComission
                //{
                //    CreditID = CreditsHistoriesID,
                //    Period = 1,
                //    RequestPartialComissionType = 2, //непонятно
                //    RequestFullPaymentComissionType = 2,
                //    ApprovedPartialComissionType = 2,
                //    ApprovedFullPaymentComissionType = 2,
                //    RequestPartialComission = Convert.ToByte(RequestPartialComissionType),
                //    ApprovedPartialComission = Convert.ToByte(RequestPartialComissionType),
                //    RequestFullPaymentComission = Convert.ToByte(RequestFullPaymentComissionType),
                //    ApprovedFullPaymentComission = Convert.ToByte(RequestFullPaymentComissionType),
                //};
                //ctx.HistoriesEarlyPaymentComissionAddItem(newItemHistoriesEarlyPaymentComission);
                ///*----------------------------------------------------*/
                //HistoriesOfficer newItemHistoriesOfficer = new HistoriesOfficer
                //{
                //    CreditID = CreditsHistoriesID,
                //    OfficerTypeID = 1, //уточнить
                //    StartDate = dateTimeServer, //уточнить
                //    UserID = credOfficerID, // 4583- Код Айбека  //usrID
                //};
                //ctx.HistoriesOfficerAddItem(newItemHistoriesOfficer);
                ///*----------------------------------------------------*/
                //string actdate = tbActualDate.Text.Substring(6, 4) + "." + tbActualDate.Text.Substring(3, 2) + "." + tbActualDate.Text.Substring(0, 2);
                //IncomesStructuresActualDate newItemIncomesStructuresActualDate = new IncomesStructuresActualDate
                //{
                //    CreditID = CreditsHistoriesID,
                //    ActualDate = dateTimeServer //Convert.ToDateTime(actdate), // dateTimeServer //Convert.ToDateTime(actdate),
                //};
                //ctx.IncomesStructuresActualDateAddItem(newItemIncomesStructuresActualDate);
                ///*----------------------------------------------------*/
                //IncomesStructure newItemIncomesStructures = new IncomesStructure
                //{
                //    CreditID = CreditsHistoriesID,
                //    ActualDate = dateTimeServer, //Convert.ToDateTime(actdate), // dateTimeServer, //Convert.ToDateTime(actdate),
                //    CurrencyID = 417,
                //    TotalPercents = 100
                //};
                //ctx.ItemIncomesStructuresAddItem(newItemIncomesStructures);

                CreditController ctx = new CreditController();
                GeneralController gctx = new GeneralController();

                //string dateFirstPayment = Convert.ToDateTime(tbActualDate.Text).ToString("dd.MM.yyyy");
                string dateFirstPayment = DateTime.ParseExact(tbActualDate.Text, "dd.MM.yyyy", null).ToString("dd.MM.yyyy");

                GeneralController.IncomesStructure incomesstructure = new GeneralController.IncomesStructure()
                {
                    CurrencyID = 417,
                    TotalPercents = 100,
                };

                GeneralController.IncomesStructuresActualDate incomesstructuresactualdate = new GeneralController.IncomesStructuresActualDate()
                {
                    //ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"), //Convert.ToDateTime(tbActualDate.Text),
                    ActualDate = Convert.ToDateTime(actdate), //Convert.ToDateTime(tbActualDate.Text),
                    IncomesStructures = new List<GeneralController.IncomesStructure>(),
                };

                GeneralController.Picture picture = new GeneralController.Picture()
                {
                    //FileName = "https://credit.doscredobank.kg/Portals/0/Credits/Nurcredits/2021/10/28/okmasro4/Screenshot_1.jpg",
                    //ChangeDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                    File = hfPhoto2.Value
                };

                GeneralController.Partner partner = new GeneralController.Partner()
                {
                    PartnerCompanyID = (int)GroupCode,
                    LoanPartnerSumV = Convert.ToDecimal(RadNumTbRequestSumm.Text), //0,//50000.0,
                    CommissionSum = 0
                    //IssueComissionPaymentTypeID = null
                };

                //GeneralController.Guarantor guarantor = new GeneralController.Guarantor()
                //{
                //    CustomerID = 1397375,
                //    GuaranteeAmount = 50000,
                //    StartDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                //    //EndDate = 
                //    Status = 1,
                //};

                DateTime creditOfficerStartDate = (connectionStringActualDate == "") ? Convert.ToDateTime(dateNow).AddDays(-1) : Convert.ToDateTime(connectionStringActualDate).AddDays(-1);

                int mortrageTypeID = 16;
                if (orgID == 36) mortrageTypeID=17;
                GeneralController.Root root = new GeneralController.Root()
                {

                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    ProductID = prodID, //1164, //prodID,
                    //LoanStatus = 0,
                    CreditID = 0,
                    MortrageTypeID = mortrageTypeID,  //Вид обеспечения 16-приобретаемая имущество
                    //IncomeApproveTypeID = 1,
                    RequestCurrencyID = 417,
                    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    //MarketingSourceID = 5,
                    //RequestDate = Convert.ToDateTime("2021-11-05T11:28:42"), //dateTimeNow, 
                    RequestDate = Convert.ToDateTime(actdate), //dateTimeNow, 
                    //IssueAccountNo = "",
                    //OfficerUserName = "",
                    CreditOfficerTypeID = 1,
                    //CreditOfficerStartDate = Convert.ToDateTime("2021-11-04T11:28:42"), //Тест 28
                    //CreditOfficerStartDate = Convert.ToDateTime("2021-11-24T11:28:42"), //dateTimeNow, Кола
                    CreditOfficerStartDate = creditOfficerStartDate, // Convert.ToDateTime(dateNow).AddDays(-1), //Convert.ToDateTime("2021-09-19T11:28:42"), //dateTimeNow, 
                    //CreditOfficerEndDate = null, // Convert.ToDateTime("2021-11-05T11:28:42"), //dateTimeNow, 
                    //OfficeID = officeID, //1105,
                    OfficerID = Convert.ToInt32(credOfficerID), //6804,

                    IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>(),
                    Guarantors = new List<GeneralController.Guarantor>(),
                    Pictures = new List<GeneralController.Picture>(),
                    Partners = new List<GeneralController.Partner>(),


                    RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                    RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text), //0.0, // Convert.ToDouble(ddlRequestRate.SelectedItem.Text),
                    PaymentSourceID = 1,
                    //NonPaymentRisk = null,
                    //CreditFraudStatus = null,
                    //InformationID = null,
                    //ParallelRelativeCredit = null,
                    CreditPurpose = "Потребительская",
                    LoanPurposeTypeID = 2,
                    LoanLocation = "", //
                    //Options = null,
                    //ReasonRefinancing = null,
                    //ExternalProgramID = null,
                    //ExternalCashDeskID = null,
                    //RedealType = null,
                    //IsGroup = false,
                    //GroupName = "",
                    //CreditGroupCode = null,
                    //RequestGrantComission = null,
                    //RequestGrantComissionType = 1,
                    //RequestReturnComission = null,
                    //RequestReturnComissionType = null,
                    //RequestTrancheIssueComission = null,
                    //RequestTrancheIssueComissionType = null
                };


                //incomesstructuresactualdate.ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"); //Convert.ToDateTime(dateTimeNow);
                incomesstructuresactualdate.ActualDate = Convert.ToDateTime(actdate); //Convert.ToDateTime(dateTimeNow);
                incomesstructuresactualdate.IncomesStructures.Add(incomesstructure);



                root.IncomesStructuresActualDates.Add(incomesstructuresactualdate);
                root.Partners.Add(partner);
                root.Pictures.Add(picture);
                //root.Guarantors.Add(guarantor);


                //string str = SendPostOBCreateRequest(root);

                string result = gctx.CreateRequestWithAPI(root); //кафка
                int CreditsHistoriesID = 0;
                try
                {
                    CreditsHistoriesID = Convert.ToInt32(gctx.getCreditID(result)); //кафка
                }
                catch (Exception ex)
                {
                    MsgBox("Ошибка:" + result, this.Page, this); //кафка
                    lblError.Text = result; //кафка
                }


                /*----------------------------------------------------*/ //save to dnn
                                                                         //string Rate = Convert.ToDouble(ddlRequestRate.SelectedItem.Value);

                if (CreditsHistoriesID != 0) //кафка
                {
                    savePhoto("new", CreditsHistoriesID);

                    string comment = "";
                    //if (rbtnBusiness.SelectedIndex == 1) { fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 0; comment = txtBusinessComment.Text; }
                    //if (rbtnBusiness.SelectedIndex == 2) { fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 0; comment = txtAgroComment.Text; }
                    fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 7000;
                    //comment = txtBusinessComment.Text;
                    if (rbtnBusiness.SelectedIndex == 1) comment = txtBusinessComment.Text;
                    if (rbtnBusiness.SelectedIndex == 2) comment = txtAgroComment.Text;

                    string creditProduct = "";
                    if (ddlRequestRate.SelectedItem.Text == "29,90") creditProduct = "КапиталБанк";
                    if (ddlRequestRate.SelectedItem.Text == "0,00")
                    {
                        creditProduct = "00";
                        creditProduct = creditProduct + ddlRequestPeriod.SelectedValue;
                    }
                    if (groupID == 134) creditProduct = "Кола";
                    if (groupID == 128) creditProduct = "Онлайн";

                    Request newRequest = new Request
                    {
                        CreditProduct = creditProduct,
                        CreditPurpose = tbCreditPurpose.Text,
                        RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                        ProductPrice = Convert.ToDecimal(RadNumTbTotalPrice.Text),
                        AmountDownPayment = Convert.ToDecimal(TbAmountOfDownPayment.Text),
                        RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                        RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text), //Convert.ToDecimal("0.00"), //Convert.ToDecimal(ddlRequestRate.SelectedItem.Text), // (ddlRequestRate.SelectedItem.Text == "0,00") ? Convert.ToDecimal("0,00") : Convert.ToDecimal(ddlRequestRate.SelectedItem.Text),
                        RequestGrantComission = (lblCommission.Text == "") ? Convert.ToDecimal(lblCommission.Text) : 0,
                        ActualDate = Convert.ToDateTime(dateFirstPayment),
                        CustomerID = Convert.ToInt32(hfCustomerID.Value),
                        Surname = tbSurname2.Text,
                        CustomerName = tbCustomerName2.Text,
                        Otchestvo = tbOtchestvo2.Text,
                        IdentificationNumber = tbINN2.Text,
                        GuarantorID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0,
                        GuarantorSurname = tbGuarantorSurname.Text,
                        GuarantorName = tbGuarantorName.Text,
                        GuarantorOtchestvo = tbGuarantorOtchestvo.Text,
                        GuarantorIdentificationNumber = tbGuarantorINN.Text,
                        RequestDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                        AgentID = usrID,
                        AgentRoleID = usrRoleID,
                        AgentUsername = Session["UserName"].ToString(),
                        AgentFirstName = Session["FIO"].ToString(),
                        AgentLastName = Session["FIO"].ToString(),
                        RequestStatus = "Новая заявка",
                        CreditID = CreditsHistoriesID,
                        BranchID = branchID,
                        AverageMonthSalary = (RadNumTbSumMonthSalary.Text != "") ? Convert.ToDecimal(RadNumTbSumMonthSalary.Text) / Convert.ToByte(ddlMonthCount.SelectedValue) : 0,
                        SumMonthSalary = (RadNumTbSumMonthSalary.Text != "") ? Convert.ToDecimal(RadNumTbSumMonthSalary.Text) : 0,
                        CountMonthSalary = Convert.ToByte(ddlMonthCount.SelectedValue),
                        ////MonthlyInstallment = (RadNumTbMonthlyInstallment.Text != "") ? Convert.ToDecimal(RadNumTbMonthlyInstallment.Text) : 0,
                        ////editRequest.Revenue = (RadNumTbRevenue.Text != "") ? Convert.ToDecimal(RadNumTbRevenue.Text) : 0;
                        MinRevenue = (RadNumTbMinRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMinRevenue.Text) : 0,
                        MaxRevenue = (RadNumTbMaxRevenue.Text != "") ? Convert.ToDecimal(RadNumTbMaxRevenue.Text) : 0,
                        Revenue = ((MinRevenue != 0) && (MaxRevenue != 0)) ? (MinRevenue + MaxRevenue) / 2 : MinRevenue + MaxRevenue,
                        CountWorkDay = Convert.ToByte(ddlCountWorkDay.SelectedValue),
                        СostPrice = (RadNumTbСostPrice.Text != "") ? Convert.ToDecimal(RadNumTbСostPrice.Text) : 0,
                        Overhead = (RadNumTbOverhead.Text != "") ? Convert.ToDecimal(RadNumTbOverhead.Text) : 0,
                        FamilyExpenses = fexp,
                        Bussiness = rbtnBusiness.SelectedIndex,
                        OtherLoans = (RadNumOtherLoans.Text != "") ? Convert.ToDecimal(RadNumOtherLoans.Text) : 0,
                        BusinessComment = txtBusinessComment.Text,
                        AdditionalIncome = (RadNumTbAdditionalIncome.Text != "") ? Convert.ToDecimal(RadNumTbAdditionalIncome.Text) : 0,
                        RevenueAgro = (RadNumTbRevenueAgro.Text != "") ? Convert.ToDecimal(RadNumTbRevenueAgro.Text) : 0,
                        RevenueMilk = (RadNumTbRevenueMilk.Text != "") ? Convert.ToDecimal(RadNumTbRevenueMilk.Text) : 0,
                        RevenueMilkProd = (RadNumTbRevenueMilkProd.Text != "") ? Convert.ToDecimal(RadNumTbRevenueMilkProd.Text) : 0,
                        OverheadAgro = (RadNumTbOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbOverheadAgro.Text) : 0,
                        AddOverheadAgro = (RadNumTbAddOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbAddOverheadAgro.Text) : 0,

                        IsEmployer = chbEmployer.Checked ? true : false,
                        MaritalStatus = Convert.ToInt32(rbtnMaritalStatus.SelectedItem.Value),
                        GroupID = groupID,
                        OrgID = orgID,
                        OrganizationINN = tbINNOrg.Text,
                        OfficeID = officeID
                    };
                    ItemController ctl = new ItemController();
                    int requestID = ctl.ItemRequestAddItem(newRequest);

                    //general.SaveToLoanRequest(root, requestID); //кафка

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
                        //ctx2.CustomerUpdItem(cust);
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
                    refreshProducts(1); firstAmount();
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
                    //if (Convert.ToInt32(Session["RoleID"]) == 1)
                    //{ Response.Redirect("/AgentPage"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 2)
                    //{ Response.Redirect("/ExpertPartners"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 3)
                    //{ Response.Redirect("/AgentTour"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 4)
                    //{ Response.Redirect("/AdminPage"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 5)
                    //{ Response.Redirect("/ExpertPartners"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 6)
                    //{ Response.Redirect("/AgentSvetofor"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 7)
                    //{ Response.Redirect("/AdminSvetofor"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 8)
                    //{ Response.Redirect("/AgentBeeline"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 9)
                    //{ Response.Redirect("/AdminBeeline"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 10)
                    //{ Response.Redirect("/ExpertBeeline"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 12)
                    //{ Response.Redirect("/NurOnline"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 13)
                    //{ Response.Redirect("/AgentPartners"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 14)
                    //{ Response.Redirect("/AdminPartners"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 16)
                    //{ Response.Redirect("/AgentPlaneta"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 17)
                    //{ Response.Redirect("/AdminPlaneta"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 18)
                    //{ Response.Redirect("/BeelineOnline"); }
                    Response.Redirect("/Partners/Partners");
                    enableUpoadFiles();
                }
                /*****************************/
            }
            if (hfRequestAction.Value == "edit")
            {
                if (pnlPhoto.Visible == false) hfPhoto2.Value = "";
                DateTime dateTimeServer = dateNowServer;
                DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now);
                /*Edit*/
                int usrID = Convert.ToInt32(Session["UserID"].ToString());
                //int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
                int? officeID = dbRWZ.Requests.Where(r => r.CreditID == Convert.ToInt32(hfCreditID.Value)).FirstOrDefault().OfficeID;
                //int? usrRoleID = dbR.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
                //int officeID = dbR.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;

                //int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                //int? GroupCode = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupCode;

                //string dateFirstPayment = tbActualDate.Text.Substring(6, 4) + "." + tbActualDate.Text.Substring(3, 2) + "." + tbActualDate.Text.Substring(0, 2);
                var reqs = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
                //int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                int? groupID = reqs.GroupID;
                int? GroupCode = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupCode;

                int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;

                int reqGroupID = Convert.ToInt32(dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault().GroupID);


                CreditController ctlCredit = new CreditController();





                decimal commision = 0; string NameOfCredit = "КапиталБанк"; int prodID = 1152; //КапиталБанк
                if (chbEmployer.Checked) prodID = 1153; else prodID = 1152; //КапиталБанк для сотруд иначе КапиталБанк
                decimal rate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);
                byte period = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                bool isEmployer = chbEmployer.Checked ? true : false;
                if (Convert.ToDecimal(RadNumTbRequestSumm.Text) > 100000) { NameOfCredit = "Потребительский"; prodID = 109; }
                if ((rate == 0) && (period == 3)) { commision = 300; NameOfCredit = "0-0-3"; prodID = 1163; }
                if ((rate == 0) && (period == 6)) { commision = 300; NameOfCredit = "0-0-6"; prodID = 1164; }
                if ((rate == 0) && (period == 9)) { commision = 300; NameOfCredit = "0-0-9"; prodID = 1165; }
                if ((rate == 0) && (period == 12)) { commision = 300; NameOfCredit = "0-0-12"; prodID = 1177; }
                if ((rate == 0) && (period == 18)) { commision = 300; NameOfCredit = "0-0-18"; prodID = 1190; }
                if (orgID == 11) //Сулпак
                {
                    if ((rate == 0) && (period == 24)) { commision = 300; NameOfCredit = "0-0-24"; prodID = 1181; }
                }
                if (orgID == 6) //Техно
                {
                    if ((rate == 0) && (period == 24)) { commision = 300; NameOfCredit = "0-0-24"; prodID = 1192; }
                }
                //if ((rate == 0) && (period == 27)) { commision = 300; NameOfCredit = "0-0-27"; prodID = 1179; }
                //if ((rate == 0) && (period == 10)) { commision = 300; NameOfCredit = "10-10-10"; prodID = 1168; }
                if ((rate == 0) && (period == 15)) { commision = 0; NameOfCredit = "0-0-15"; prodID = 1169; }
                if (reqGroupID == 128)
                { //commision = 0; NameOfCredit = "0-0-15"; 
                    prodID = 1206;
                    RadNumTbTotalPrice.Text = RadNumTbRequestSumm.Text;
                }
                if (reqGroupID == 134)
                { //commision = 0; NameOfCredit = "0-0-15"; 
                    prodID = 1224;
                    RadNumTbTotalPrice.Text = RadNumTbRequestSumm.Text;
                }

                if (isEmployer == true) commision = 0;
                string comment = "";
                //if (rbtnBusiness.SelectedIndex == 1) { fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 0; comment = txtBusinessComment.Text; }
                //if (rbtnBusiness.SelectedIndex == 2) { fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 0; comment = txtAgroComment.Text; }
                fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 7000;
                //comment = txtBusinessComment.Text;
                if (rbtnBusiness.SelectedIndex == 1) comment = txtBusinessComment.Text;
                if (rbtnBusiness.SelectedIndex == 2) comment = txtAgroComment.Text;


                /*----------------------------------------------------*/
                //History editItemHistory = new History();
                //editItemHistory = ctlCredit.GetHistoryByCreditID(Convert.ToInt32(hfCreditID.Value));
                //editItemHistory.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                //editItemHistory.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value);
                //editItemHistory.ProductID = prodID;
                //editItemHistory.ApprovedPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                //editItemHistory.ApprovedRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value);
                //ctlCredit.HistoryUpd(editItemHistory);
                /*----------------------------------------------------*/

                string dateFirstPayment = tbActualDate.Text.Substring(6, 4) + "." + tbActualDate.Text.Substring(3, 2) + "." + tbActualDate.Text.Substring(0, 2);

                if ((reqGroupID != 134) && (reqGroupID != 128))//Сайт ДКБ Для КапиталБанк
                {
                    
                    GeneralController gctx = new GeneralController();


                    GeneralController.IncomesStructure incomesstructure = new GeneralController.IncomesStructure()
                    {
                        CurrencyID = 417,
                        TotalPercents = 100,
                    };

                    GeneralController.IncomesStructuresActualDate incomesstructuresactualdate = new GeneralController.IncomesStructuresActualDate()
                    {
                        //ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"), //Convert.ToDateTime(tbActualDate.Text),
                        ActualDate = Convert.ToDateTime(actdate), //Convert.ToDateTime(tbActualDate.Text),
                        IncomesStructures = new List<GeneralController.IncomesStructure>(),
                    };

                    GeneralController.Picture picture = new GeneralController.Picture()
                    {
                        //FileName = "https://credit.doscredobank.kg/Portals/0/Credits/Nurcredits/2021/10/28/okmasro4/Screenshot_1.jpg",
                        //ChangeDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                        File = hfPhoto2.Value
                    };

                    GeneralController.Partner partner = new GeneralController.Partner()
                    {
                        PartnerCompanyID = (int)GroupCode,
                        LoanPartnerSumV = Convert.ToDecimal(RadNumTbRequestSumm.Text), //0,//50000.0,
                        CommissionSum = Convert.ToDecimal(0.0),
                        //IssueComissionPaymentTypeID = 1
                    };

                    //GeneralController.Guarantor guarantor = new GeneralController.Guarantor()
                    //{
                    //    CustomerID = 1397375,
                    //    GuaranteeAmount = 50000,
                    //    StartDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                    //    //EndDate = 
                    //    Status = 1,
                    //};
                    int mortrageTypeID = 16;
                    if (orgID == 36) mortrageTypeID = 17;


                    byte loanStatus = 0;
                    if ((reqs.RequestStatus == "Утверждено") || (reqs.RequestStatus == "К подписи") || (reqs.RequestStatus == "На выдаче")) loanStatus = 2;

                    GeneralController.RootUpdate root = new GeneralController.RootUpdate()
                    {

                        CustomerID = Convert.ToInt32(hfCustomerID.Value),
                        ProductID = prodID, //1164, //prodID,
                        LoanStatus = loanStatus,
                        CreditID = Convert.ToInt32(hfCreditID.Value),
                        MortrageTypeID = mortrageTypeID,  //Вид обеспечения 16-приобретаемая имущество
                        IncomeApproveTypeID = 1,
                        RequestCurrencyID = 417,
                        RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                        //MarketingSourceID = 5,
                        //RequestDate = Convert.ToDateTime("2021-11-05T11:28:42"), //dateTimeNow, 
                        //IssueAccountNo = null,
                        //OfficerUserName = null,
                        //CreditOfficerTypeID = 1,
                        //CreditOfficerStartDate = Convert.ToDateTime("2021-09-15T11:28:42"), //dateTimeNow, 
                        // CreditOfficerEndDate = null, // Convert.ToDateTime("2021-11-05T11:28:42"), //dateTimeNow, 
                        OfficeID = Convert.ToInt32(officeID), //1105,
                                             //OfficerID = Convert.ToInt32(credOfficerID), //6804,

                        IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>(),
                        Guarantors = new List<GeneralController.Guarantor>(),
                        Pictures = new List<GeneralController.Picture>(),
                        Partners = new List<GeneralController.Partner>(),


                        RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                        RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text), //0.0, // Convert.ToDouble(ddlRequestRate.SelectedItem.Text),

                        //PaymentSourceID = 1,
                        //NonPaymentRisk = null,
                        //CreditFraudStatus = null,
                        //InformationID = null,
                        //ParallelRelativeCredit = null,
                        CreditPurpose = "Потребительская",
                        LoanPurposeTypeID = 2,
                        //LoanLocation = "",
                        //Options = null,
                        //ReasonRefinancing = null,
                        //ExternalProgramID = null,
                        //ExternalCashDeskID = null,
                        //RedealType = null,
                        //IsGroup = false,
                        //GroupName = "",
                        //CreditGroupCode = null,
                        //RequestGrantComission = null,
                        //RequestGrantComissionType = 1,
                        //RequestReturnComission = null,
                        //RequestReturnComissionType = null,
                        //RequestTrancheIssueComission = null,
                        //RequestTrancheIssueComissionType = null
                    };


                    //incomesstructuresactualdate.ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"); //Convert.ToDateTime(dateTimeNow);
                    incomesstructuresactualdate.ActualDate = Convert.ToDateTime(actdate); //Convert.ToDateTime(dateTimeNow);
                    incomesstructuresactualdate.IncomesStructures.Add(incomesstructure);



                    root.IncomesStructuresActualDates.Add(incomesstructuresactualdate);
                    root.Partners.Add(partner);
                    root.Pictures.Add(picture);
                    //root.Guarantors.Add(guarantor);


                    //string str = SendPostOBCreateRequest(root);

                    string result = gctx.UpdateRequestWithAPI(root); //кафка
                    GeneralController general = new GeneralController();
                    //general.UpdateToLoanRequest(root, reqs.RequestID); //кафка


                    int CreditsHistoriesID = 0;
                    try
                    {
                        CreditsHistoriesID = Convert.ToInt32(gctx.getCreditID(result));
                    }
                    catch (Exception ex)
                    {
                        MsgBox("Ошибка:" + result, this.Page, this);
                        lblError.Text = result;
                    }

                    //if (CreditsHistoriesID != 0)


                    savePhoto("edit", CreditsHistoriesID);
                }

                if (reqGroupID == 134) //для колы
                {
                    var lst = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
                    var office = dbRWZ.Offices.Where(r => r.ID == Convert.ToInt32(ddlOffice.SelectedValue)).FirstOrDefault();
                    var creditOfficerID = dbRWZ.RequestsRedirect.Where(r => r.officeID == office.ID).FirstOrDefault().creditOfficerID;
                    lst.OfficeID = Convert.ToInt32(ddlOffice.SelectedValue);
                    lst.BranchID = office.BranchID;
                    dbRWZ.Requests.Context.SubmitChanges();

                    GeneralController gctx = new GeneralController();
                    //GeneralController.Root root = new GeneralController.Root();

                    GeneralController.IncomesStructure incomesstructure = new GeneralController.IncomesStructure()
                    {
                        CurrencyID = 417,
                        TotalPercents = 100,
                    };

                    GeneralController.IncomesStructuresActualDate incomesstructuresactualdate = new GeneralController.IncomesStructuresActualDate()
                    {
                        //ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"), //Convert.ToDateTime(tbActualDate.Text),
                        ActualDate = Convert.ToDateTime(actdate), //Convert.ToDateTime(tbActualDate.Text),
                        IncomesStructures = new List<GeneralController.IncomesStructure>(),
                    };

                    GeneralController.Picture picture = new GeneralController.Picture()
                    {
                        //FileName = "https://credit.doscredobank.kg/Portals/0/Credits/Nurcredits/2021/10/28/okmasro4/Screenshot_1.jpg",
                        //ChangeDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                        File = ""
                    };



                    GeneralController.Partner partner = new GeneralController.Partner()
                    {
                        //PartnerCompanyID = partnerCode,
                        //LoanPartnerSumV = Convert.ToDecimal(RadNumTbRequestSumm.Text), //0,//50000.0,
                        //CommissionSum = 0
                        //IssueComissionPaymentTypeID = null
                    };

                    //GeneralController.Guarantor guarantor = new GeneralController.Guarantor()
                    //{
                    //    CustomerID = 1397375,
                    //    GuaranteeAmount = 50000,
                    //    StartDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                    //    //EndDate = 
                    //    Status = 1,
                    //};



                    dynamic dRequest = new System.Dynamic.ExpandoObject();
                    //dynRoot.Name = "Tom";
                    //dynRoot.Age = 46;
                    //GeneralController.RootUpdate2 root = new GeneralController.RootUpdate2()
                    {

                        dRequest.CustomerID = Convert.ToInt32(hfCustomerID.Value);
                        dRequest.ProductID = 1224; //1224 - кола

                        dRequest.CreditID = Convert.ToInt32(lst.CreditID);
                        dRequest.MortrageTypeID = 17;  //Вид обеспечения 16-приобретаемая имущество
                        //dRequest.IncomeApproveTypeID = 1;
                        dRequest.RequestCurrencyID = 417;
                        dRequest.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                        //dRequest.IssueAccountNo = lst.CardAccount;
                        //dRequest.OfficeID = office.ID;
                        dRequest.OfficerID = Convert.ToInt32(creditOfficerID);


                        dRequest.IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>();
                        dRequest.Guarantors = new List<GeneralController.Guarantor>();
                        dRequest.Pictures = new List<GeneralController.Picture>();
                        dRequest.Partners = new List<GeneralController.Partner>();


                        dRequest.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                        dRequest.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);

                        dRequest.CreditPurpose = "Закуп товара";
                        dRequest.LoanPurposeTypeID = 1;

                    };


                    //GeneralController.RootUpdate2 root = new GeneralController.RootUpdate2()
                    //{

                    //    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    //    ProductID = 1206,

                    //    CreditID = Convert.ToInt32(lst.CreditID),
                    //    MortrageTypeID = 17,  //Вид обеспечения 16-приобретаемая имущество
                    //    IncomeApproveTypeID = 1,
                    //    RequestCurrencyID = 417,
                    //    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    //    //dRequest.IssueAccountNo = lst.CardAccount;
                    //    //dRequest.OfficeID = office.ID;
                    //    OfficerID = Convert.ToInt32(creditOfficerID),


                    //    IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>(),
                    //    Guarantors = new List<GeneralController.Guarantor>(),
                    //    Pictures = new List<GeneralController.Picture>(),
                    //    Partners = new List<GeneralController.Partner>(),


                    //    RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                    //    RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text),

                    //    CreditPurpose = 2,
                    //    LoanPurposeTypeID = 2

                    //};


                    //incomesstructuresactualdate.ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"); //Convert.ToDateTime(dateTimeNow);
                    incomesstructuresactualdate.ActualDate = Convert.ToDateTime(actdate); //Convert.ToDateTime(dateTimeNow);
                    incomesstructuresactualdate.IncomesStructures.Add(incomesstructure);



                    dRequest.IncomesStructuresActualDates.Add(incomesstructuresactualdate);
                    //dRequest.Partners.Add(partner);
                    dRequest.Pictures.Add(picture);
                    //root.Guarantors.Add(guarantor);


                    //string str = SendPostOBCreateRequest(root);

                    string result = gctx.UpdateRequestDynWithAPI(dRequest);

                    /*********************/

                    //refreshGrid();
                }
                if (reqGroupID == 128) //Сайт ДКБ
                {
                    //dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
                    var req = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
                    var office = dbRWZ.Offices.Where(r => r.ID == Convert.ToInt32(ddlOffice.SelectedValue)).FirstOrDefault();
                    var creditOfficerID = dbRWZ.RequestsRedirect.Where(r => r.officeID == office.ID).FirstOrDefault().creditOfficerID;
                    req.OfficeID = Convert.ToInt32(ddlOffice.SelectedValue);
                    req.BranchID = office.BranchID;
                    dbRWZ.Requests.Context.SubmitChanges();

                    //var acc = dbR.Accounts.Where(r => r.AccountNo == CARD_ACCT).ToList().FirstOrDefault();
                    //var acc = dbR.Accounts.Where(r => r.CustomerID  == req.CustomerID).ToList().FirstOrDefault();


                    GeneralController gctx = new GeneralController();
                    //GeneralController.Root root = new GeneralController.Root();

                    GeneralController.IncomesStructure incomesstructure = new GeneralController.IncomesStructure()
                    {
                        CurrencyID = 417,
                        TotalPercents = 100,
                    };

                    GeneralController.IncomesStructuresActualDate incomesstructuresactualdate = new GeneralController.IncomesStructuresActualDate()
                    {
                        //ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"), //Convert.ToDateTime(tbActualDate.Text),
                        ActualDate = Convert.ToDateTime(actdate), //Convert.ToDateTime(tbActualDate.Text),
                        IncomesStructures = new List<GeneralController.IncomesStructure>(),
                    };

                    GeneralController.Picture picture = new GeneralController.Picture()
                    {
                        //FileName = "https://credit.doscredobank.kg/Portals/0/Credits/Nurcredits/2021/10/28/okmasro4/Screenshot_1.jpg",
                        //ChangeDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                        File = ""
                    };



                    GeneralController.Partner partner = new GeneralController.Partner()
                    {
                        //PartnerCompanyID = partnerCode,
                        //LoanPartnerSumV = Convert.ToDecimal(RadNumTbRequestSumm.Text), //0,//50000.0,
                        //CommissionSum = 0
                        //IssueComissionPaymentTypeID = null
                    };

                    //GeneralController.Guarantor guarantor = new GeneralController.Guarantor()
                    //{
                    //    CustomerID = 1397375,
                    //    GuaranteeAmount = 50000,
                    //    StartDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                    //    //EndDate = 
                    //    Status = 1,
                    //};



                    dynamic dRequest = new System.Dynamic.ExpandoObject();
                    //dynRoot.Name = "Tom";
                    //dynRoot.Age = 46;
                    //GeneralController.RootUpdate2 root = new GeneralController.RootUpdate2()
                    {

                        dRequest.CustomerID = Convert.ToInt32(hfCustomerID.Value);
                        dRequest.ProductID = 1206;

                        dRequest.CreditID = Convert.ToInt32(req.CreditID);
                        dRequest.MortrageTypeID = 17;  //Вид обеспечения 16-приобретаемая имущество
                        dRequest.IncomeApproveTypeID = 1;
                        dRequest.RequestCurrencyID = 417;
                        dRequest.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                        //dRequest.IssueAccountNo = lst.CardAccount;
                        //dRequest.OfficeID = office.ID;
                        dRequest.OfficerID = Convert.ToInt32(creditOfficerID);


                        dRequest.IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>();
                        dRequest.Guarantors = new List<GeneralController.Guarantor>();
                        dRequest.Pictures = new List<GeneralController.Picture>();
                        dRequest.Partners = new List<GeneralController.Partner>();


                        dRequest.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                        dRequest.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);

                        dRequest.CreditPurpose = "Потребительская";
                        dRequest.LoanPurposeTypeID = 2;
                        dRequest.IssueAccountNo = req.CardAccount;

                    };


                    //GeneralController.RootUpdate2 root = new GeneralController.RootUpdate2()
                    //{

                    //    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    //    ProductID = 1206,

                    //    CreditID = Convert.ToInt32(lst.CreditID),
                    //    MortrageTypeID = 17,  //Вид обеспечения 16-приобретаемая имущество
                    //    IncomeApproveTypeID = 1,
                    //    RequestCurrencyID = 417,
                    //    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    //    //dRequest.IssueAccountNo = lst.CardAccount;
                    //    //dRequest.OfficeID = office.ID;
                    //    OfficerID = Convert.ToInt32(creditOfficerID),


                    //    IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>(),
                    //    Guarantors = new List<GeneralController.Guarantor>(),
                    //    Pictures = new List<GeneralController.Picture>(),
                    //    Partners = new List<GeneralController.Partner>(),


                    //    RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                    //    RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text),

                    //    CreditPurpose = 2,
                    //    LoanPurposeTypeID = 2

                    //};


                    //incomesstructuresactualdate.ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"); //Convert.ToDateTime(dateTimeNow);
                    incomesstructuresactualdate.ActualDate = Convert.ToDateTime(actdate); //Convert.ToDateTime(dateTimeNow);
                    incomesstructuresactualdate.IncomesStructures.Add(incomesstructure);



                    dRequest.IncomesStructuresActualDates.Add(incomesstructuresactualdate);
                    //dRequest.Partners.Add(partner);
                    dRequest.Pictures.Add(picture);
                    //root.Guarantors.Add(guarantor);


                    //string str = SendPostOBCreateRequest(root);

                    string result = gctx.UpdateRequestDynWithAPI(dRequest);

                    /*********************/

                    //refreshGrid();
                }
                /*******************************************************************************************/
                 
                

                    Request editRequest = new Request();
                    ItemController ctlItem = new ItemController();
                    editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
                    editRequest.CreditPurpose = tbCreditPurpose.Text;
                    editRequest.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                    editRequest.ProductPrice = Convert.ToDecimal(RadNumTbTotalPrice.Text);
                    editRequest.AmountDownPayment = Convert.ToDecimal(TbAmountOfDownPayment.Text);
                    editRequest.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                    editRequest.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);  //Convert.ToDecimal("0,00");  // Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);
                    editRequest.RequestGrantComission = (lblCommission.Text != "") ? Convert.ToDecimal(lblCommission.Text) : 0;
                    editRequest.ActualDate = Convert.ToDateTime(dateFirstPayment);
                    editRequest.Surname = tbSurname2.Text;
                    editRequest.CustomerName = tbCustomerName2.Text;
                    editRequest.Otchestvo = tbOtchestvo2.Text;
                    editRequest.IdentificationNumber = tbINN2.Text;
                    editRequest.GuarantorID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0;
                    editRequest.GuarantorSurname = tbGuarantorSurname.Text;
                    editRequest.GuarantorName = tbGuarantorName.Text;
                    editRequest.GuarantorOtchestvo = tbGuarantorOtchestvo.Text;
                    editRequest.GuarantorIdentificationNumber = tbGuarantorINN.Text;
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
                    editRequest.BusinessComment = txtBusinessComment.Text;
                    editRequest.AdditionalIncome = (RadNumTbAdditionalIncome.Text != "") ? Convert.ToDecimal(RadNumTbAdditionalIncome.Text) : 0;
                    editRequest.IsEmployer = chbEmployer.Checked ? true : false;
                    editRequest.RevenueAgro = (RadNumTbRevenueAgro.Text != "") ? Convert.ToDecimal(RadNumTbRevenueAgro.Text) : 0;
                    editRequest.RevenueMilk = (RadNumTbRevenueMilk.Text != "") ? Convert.ToDecimal(RadNumTbRevenueMilk.Text) : 0;
                    editRequest.RevenueMilkProd = (RadNumTbRevenueMilkProd.Text != "") ? Convert.ToDecimal(RadNumTbRevenueMilkProd.Text) : 0;
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
                        //ctx2.CustomerUpdItem(cust);
                    }
                    else { }
                
                /******************************************************/
                refreshGrid();
                clearEditControlsRequest();
                hfRequestAction.Value = "";
                //if (Convert.ToInt32(Session["RoleID"]) == 1)
                //{ Response.Redirect("/AgentPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 2)
                //{ Response.Redirect("/ExpertPartners"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 3)
                //{ Response.Redirect("/AgentTour"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 4)
                //{ Response.Redirect("/AdminPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 5)
                //{ Response.Redirect("/ExpertPartners"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 6)
                //{ Response.Redirect("/AgentSvetofor"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 7)
                //{ Response.Redirect("/AdminSvetofor"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 8)
                //{ Response.Redirect("/AgentBeeline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 9)
                //{ Response.Redirect("/AdminBeeline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 10)
                //{ Response.Redirect("/ExpertBeeline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 12)
                //{ Response.Redirect("/NurOnline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 13)
                //{ Response.Redirect("/AgentPartners"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 14)
                //{ Response.Redirect("/AdminPartners"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 16)
                //{ Response.Redirect("/AgentPlaneta"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 17)
                //{ Response.Redirect("/AdminPlaneta"); }
                Response.Redirect("/Partners/Partners");
                pnlNewRequest.Visible = false;
            }
        }
        //protected void SendMailTo(int? branchID, string strsubj, string fullname, bool boolagent, bool booladmin, bool boolekspert)
        //{
        //    /*****************************/
        //    //string strsubj;
        //    string stremailto = "";
        //    string emailfrom = "";
        //    string templateAdm = "";
        //    string templateUsr = "";
        //    dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        //    dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        //    int usrID = Convert.ToInt32(Session["UserID"].ToString());
        //    string domname = DotNetNuke.Common.Globals.GetDomainName(Request);
        //    try
        //    {
        //        // load data from settings...
        //        if (!string.IsNullOrEmpty((string)Settings["emailfrom"]))
        //            emailfrom = (string)Settings["emailfrom"];
        //        if (!string.IsNullOrEmpty((string)Settings["emailto"]))
        //            stremailto = (string)Settings["emailto"];
        //        if (System.IO.File.Exists(MapPath(ControlPath + "Templates\\TemplateForAdmin_RR.html")))
        //            templateAdm = System.IO.File.ReadAllText(MapPath(ControlPath + "Templates\\TemplateForAdmin_RR.html"));
        //        // template for admin...
        //        templateAdm = templateAdm.Replace("[CustomerName]", tbCustomerName2.Text);
        //        templateAdm = templateAdm.Replace("[Otchestvo]", tbOtchestvo2.Text);
        //        templateAdm = templateAdm.Replace("[Surname]", tbSurname2.Text);
        //        templateAdm = templateAdm.Replace("[UserName]", Session["UserName"].ToString());
        //        templateAdm = templateAdm.Replace("[FIO]", Session["FIO"].ToString());
        //        //send email to admin
        //        string mailto = "";
        //        if (boolagent) //отправка сообщ агенту
        //        {
        //            mailto = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().EMail;
        //            SendMail(templateAdm, mailto, emailfrom, "", strsubj);
        //        }
        //        foreach (RequestsUsersRole requserrole in dbRWZ.RequestsUsersRoles)
        //        {
        //            if (requserrole.RoleID == 9) //сообщ всем админам о новой заявке
        //            {
        //                if (booladmin)
        //                {
        //                    mailto = dbRWZ.Users2s.Where(r => r.UserID == requserrole.UserID).FirstOrDefault().EMail;
        //                    //SendMail(templateAdm, mailto, emailfrom, "", strsubj);
        //                }
        //            };
        //            if (requserrole.RoleID == 2) //сообщ экспертам филиала о новой заявке
        //            {
        //                if (boolekspert)
        //                {
        //                    int officeIDeks = dbRWZ.Users2s.Where(r => r.UserID == requserrole.UserID).FirstOrDefault().OfficeID;
        //                    int branchIDeks = dbR.Offices.Where(r => r.ID == officeIDeks).FirstOrDefault().BranchID;
        //                    if (branchID == branchIDeks)
        //                    {
        //                        mailto = dbRWZ.Users2s.Where(r => (r.UserID == requserrole.UserID)).FirstOrDefault().EMail;
        //                        //SendMail(templateAdm, mailto, emailfrom, "", strsubj);
        //                    }
        //                }
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.Message + "<br>1.Настройки модуля не определены.", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
        //        System.Windows.Forms.MessageBox.Show(ex.ToString());
        //    }
        //    /*****************************/
        //}

        //protected void SendMail(string body, string to, string efrom, string replyto, string subject)
        //{
        //    try
        //    {
        //        if (String.Empty == replyto || replyto.Trim().Length == 0) replyto = "";
        //        if (efrom == "") efrom = Host.HostEmail;
        //        efrom = "СreditСonveyor@doscredobank.kg"; //
        //        if (to != "")
        //        {
        //            List<Attachment> la = new List<Attachment> { };
        //            string strMessage2 = DotNetNuke.Services.Mail.Mail.SendMail(efrom, to, "", "", replyto, MailPriority.Normal, subject, MailFormat.Html, System.Text.Encoding.UTF8, body, la, Host.SMTPServer, Host.SMTPAuthentication, Host.SMTPUsername, Host.SMTPPassword, Host.EnableSMTPSSL);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "2." + ex.Message, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
        //    }
        //}


        public System.Drawing.Image Base64ToImage()
        {

            byte[] imageBytes = Convert.FromBase64String(hfPhoto2.Value);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }


        public void savePhoto(string st, int reqid)
        {
            string destinationFile = "";
            string destinationFolder = getDestinationFolder();
            GeneralController gctl = new GeneralController();
            string dateRanmdodir = gctl.DateRandodir(destinationFolder);
            destinationFile = gctl.fileNameAddExt("Photo" + reqid.ToString() + DateTime.Today.Date.ToString("_ddMMyyyy_") + ".jpg", destinationFolder, dateRanmdodir);

            try
            {
                //FileUploadControl.SaveAs(destinationFolder + "\\" + dateRandodir + "\\" + destinationFile);
                //Base64ToImage().Save(Server.MapPath("~/") + "\\" + dateRandodir + "\\" + destinationFile);
                if (hfPhoto2.Value != "")
                    Base64ToImage().Save(Server.MapPath("~/") + "\\Credits\\Partcredits\\" + dateRanmdodir + "\\" + destinationFile);
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }


            string contentType = "";//FileUploadControl.PostedFile.ContentType.ToLower();
            if (hfPhoto2.Value != "")
            {
                {
                    string descr = ""; // (chkbxAgree.Checked) ? "Договор " : "";
                    RequestsFile newRequestFile = new RequestsFile
                    {
                        Name = destinationFile, //FileUploadControl.FileName,
                        RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
                        ContentType = contentType,
                        //Data = bytes,
                        //FullName = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + fullfilename,
                        //FullName = "\\Portals\\0\\" + partnerdir + "\\" + fullfilename,
                        //FullName2 = fileupl + "\\" + "Portals\\0\\" + partnerdir + "\\" + fullfilename,
                        //FullName = "\\Portals\\0\\" + partnerdir + "\\" + dateRanmdodir + "\\" + destinationFile,
                        //FullName2 = fileupl + "\\" + "Portals\\0\\" + partnerdir + "\\" + dateRanmdodir + "\\" + destinationFile,
                        FullName = "\\" + partnerdir + "\\" + dateRanmdodir + "\\" + destinationFile,
                        FullName2 = fileupl + "\\" + partnerdir + "\\" + dateRanmdodir + "\\" + destinationFile,
                        //FileDescription = tbFileDescription.Text,
                        FileDescription = descr + " " + tbFileDescription.Text + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                        IsPhoto = true
                    };
                    ItemController ctl = new ItemController();
                    ctl.ItemRequestFilesAddItem(newRequestFile);

                    /********************************/
                    //if (chkbxAgree.Checked)
                    //{
                    //    Requests editRequest = new Requests();
                    //    ItemController ctlItem = new ItemController();
                    //    editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    //    editRequest.IsLoadAgree = true;
                    //    ctlItem.RequestUpd(editRequest);
                    //}
                    /*****************************/
                }
            }



            //string filedir = "Credits\\Nurcredits";

            //string temp_ext = DateTime.Now.Millisecond.ToString();
            //string filename = "Photo" + reqid.ToString() + DateTime.Today.Date.ToString("_ddMMyyyy_") + temp_ext + ".jpg", fullfilename = "";
            //fullfilename = UploadImageAndSave(true, filedir, filename);
            //Base64ToImage().Save(Server.MapPath("~/") + "\\" + filedir + "\\" + fullfilename);
            //RequestsFile newRequestFile = new RequestsFile
            //{
            //    Name = filename,
            //    RequestID = Convert.ToInt32(Convert.ToInt32(reqid)),
            //    ContentType = "",
            //    FullName = "\\Portals\\0\\" + filedir + "\\" + fullfilename,
            //    FullName2 = fileupl + "\\" + "Portals\\0\\" + filedir + "\\" + fullfilename,
            //    FileDescription = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
            //    IsPhoto = true
            //};

            //ItemRequestFilesAddItem(newRequestFile);
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
                //lblError.Text = ""; lblError.Visible = false;

                databindDDLRatePeriod(1);
                editcommand(id);
                refreshfiles();
                refreshProducts(2); //firstAmount();
                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow gvr = lb.Parent.Parent as GridViewRow;
                //gvr.BackColor = System.Drawing.Color.Empty;    
                string hex = "#cbceea";
                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                gvr.BackColor = _color;
                show_positions();
                hfRequestsRowID.Value = gvr.RowIndex.ToString();
                otherdata(id);
                //btnGetScoreBeeStatus();
                lbHistory_Click(new object(), new EventArgs());
            }
        }

        public void btnGetScoreBeeStatus()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            var users2 = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault();
            if (users2.isDCB == true) btnGetScoreBee.Visible = true;
            else btnGetScoreBee.Visible = false;
        }


        public void enableUpoadFiles()
        {
            //AsyncUpload1.Enabled = true;
            FileUploadControl.Enabled = true;
            FileUploadControl.Enabled = true;
            tbFileDescription.Enabled = true;
            btnUploadFiles.Enabled = true;
        }

        public void disableUpoadFiles()
        {
            //AsyncUpload1.Enabled = false;
            FileUploadControl.Enabled = true;
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
                editRequest.AmountDownPayment = Convert.ToDecimal(TbAmountOfDownPayment.Text);
                editRequest.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                ctlItem.RequestUpd(editRequest);

                /*******************/
                CreditController ctlCredit = new CreditController();
                HistoriesCustomer editItemHistoriesCustomer = new HistoriesCustomer();
                editItemHistoriesCustomer = ctlCredit.GetHistoriesCustomerByCreditID(Convert.ToInt32(hfCreditID.Value));
                editItemHistoriesCustomer.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                ctlCredit.HistoriesCustomerUpd(editItemHistoriesCustomer);
            }

            //if (Convert.ToDecimal(RadNumTbTotalPrice.Text) > 99999)
            //{
            //    bool f = false;
            //    foreach (ListItem li in ddlRequestRate.Items)
            //    {
            //        if (li.Value == "27.00") f = true;
            //    }
            //    if (f == false) ddlRequestRate.Items.Add("27.00");
            //}
            //else
            //{
            //    //myDropDown.Items.Remove(myDropDown.Items.FindByValue("TextToFind"));
            //    ddlRequestRate.Items.Remove(ddlRequestRate.Items.FindByValue("27.00"));
            //}

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
                if (SumRequestProducts.Sum() != 0)
                    RadNumTbTotalPrice.Text = SumRequestProducts.Sum().ToString();
                //if (ddlRequestRate.Text == "0.00")
                //{
                //    if (ddlRequestPeriod.SelectedValue == "3") amoundp = 0;
                //    if (ddlRequestPeriod.SelectedValue == "6") amoundp = 0;
                //    if (ddlRequestPeriod.SelectedValue == "9") amoundp = 0;
                //    //if (ddlRequestPeriod.SelectedValue == "10") amoundp = 10;
                //    if (ddlRequestPeriod.SelectedValue == "12") amoundp = 0;
                //    // if (ddlRequestPeriod.SelectedValue == "26") amoundp = 0;
                //    if (ddlRequestPeriod.SelectedValue == "15") amoundp = 0;
                //    //if (ddlRequestPeriod.SelectedValue == "27") amoundp = 0;
                //}
                //if ((ddlRequestRate.Text == "25.00") || (ddlRequestRate.Text == "30.00"))
                //{
                //    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amoundp = 0;
                //    else amoundp = 10;
                //}
                //if (ddlRequestRate.Text == "29.00") amoundp = 10;

                if (i == 1)
                {
                    //TbAmountOfDownPayment.Text = (Math.Round(amoundp * Convert.ToDouble(RadNumTbTotalPrice.Text) / 100)).ToString();
                    //RadNumTbRequestSumm.Text = (Convert.ToDouble(RadNumTbTotalPrice.Text) - Convert.ToDouble(TbAmountOfDownPayment.Text)).ToString();

                    //double s = Convert.ToDouble(RadNumTbRequestSumm.Text);
                    //double n = Convert.ToDouble(ddlRequestPeriod.Text);
                    //double stavka = Convert.ToDouble(ddlRequestRate.SelectedItem.Value);
                    //double ii = stavka / 12 / 100;
                    //double k = (((Math.Pow((1 + ii), n)) * (ii)) * s) / ((Math.Pow((1 + (ii)), n)) - 1);
                    //RadNumTbMonthlyInstallment.Text = Math.Round((k), 0).ToString();
                }
                //firstAmount();
            }
        }

        public void history(int id)
        {
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
            try
            {
                gvHistory.DataSource = history;
                gvHistory.DataBind();
            }
            catch (Exception ex)
            {
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Error: " + ex.Message + "<hr>" + ex.Source + "<hr>" + ex.StackTrace, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                MsgBox("Ошибка " + ex, this.Page, this);
            }
        }
        public void editcommand(int id)
        {
            tbNoteCancelReq.Text = "";
            tbNoteCancelReqExp.Text = "";
            lblError.Text = "";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            try
            {

                pnlNewRequest.Visible = true;
                var lst = dbRWZ.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                var account = dbR.CreditsAccounts.Where(a => ((a.CreditID == lst.CreditID) && (a.TypeID == 3))).FirstOrDefault();

                //if (lst.GroupID == 128)
                //{ btnApproved.Text = "Верификация пройдена"; btnApproved.Enabled = false; TbAmountOfDownPayment.Enabled = false; btnFix.Visible = false; btnGetMPZ.Visible = true; }
                //else { btnApproved.Text = "Утверждено"; btnApproved.Enabled = true; btnFix.Visible = false; btnGetMPZ.Visible = false; }


                //************отправляем статус в DCB End
                //string st = "";
                //var historystatus = (from v in dbR.HistoriesStatuses where v.CreditID == lst.CreditID orderby v.OperationDate select v).ToList();
                //if (historystatus.Count > 0)
                //{
                //    var hstat = historystatus.Last();

                //    if ((hstat.StatusID == 3) || (hstat.StatusID == 5))
                //    {
                //        if (lst.GroupID == 134)
                //        {
                //            respDCB360 resp = null;

                //            //********отправляем статус на сервер DCB Begin
                //            try
                //            {

                //                resp = await SendStatusDCB360(lst.CreditID.ToString(), "ISSUED_BY");

                //            }
                //            catch (Exception ex)
                //            {
                //                //lblError.Text = ex.ToString();
                //                //lblError.Visible = true;

                //            }
                //            //respDCB360 resp = await SendStatusDCB360(532646.ToString(), "APPROVED");
                //            //lblError.Text = resp.error;

                //            if ((resp.statusCode == 200) || (resp.statusCode == 400))
                //            {
                //                //VerificationRequest(); 
                //                //sms begin
                //                //string str = await SendSMSWithOurRoute("", "");
                //                //if (str == "0: Accepted for delivery")
                //                //{
                //                //    //Console.WriteLine("send");
                //                //}
                //                //sms end
                //                //Approved();
                //                //AddRowToJournalDCBCola(lst, "Утверждено");
                //            }
                //            else
                //            {

                //                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED на сервер DCB360", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //                if (resp.statusCode == 404)
                //                {
                //                    MsgBox("Ответ от DCB360: 404 Credit id не найден", this.Page, this);
                //                    //System.Windows.Forms.MessageBox.Show("Ответ от DCB360: 404 Credit id не найден");
                //                    lblError.Text = "Ответ от DCB360: 404 Credit id не найден-" + resp.error;
                //                    lblError.Visible = true;
                //                }
                //                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED на сервер ОБ", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);

                //            }
                //        }
                //        //************отправляем статус на сервер DCB End

                //    }
                //}





                if (account != null) lblAccount.Text = account.AccountNo.ToString();
                lblOtherLoansComment.Text = "";
                lblCreditN.Text = "";
                lblCreditN.Text = lst.CreditID.ToString();
                tbCreditPurpose.Text = lst.CreditPurpose;
                ddlRequestRate.SelectedIndex = ddlRequestRate.Items.IndexOf(ddlRequestRate.Items.FindByValue(lst.RequestRate.ToString()));
                ddlRequestRateSelectedIndexChanged();
                ddlRequestPeriod.SelectedIndex = ddlRequestPeriod.Items.IndexOf(ddlRequestPeriod.Items.FindByValue(lst.RequestPeriod.ToString()));
                RadNumTbTotalPrice.Text = lst.ProductPrice.ToString();
                RadNumTbRequestSumm.Text = lst.RequestSumm.ToString();
                RadNumOtherLoans.Text = lst.OtherLoans.ToString();
                RadNumTbAdditionalIncome.Text = lst.AdditionalIncome.ToString();
                txtBusinessComment.Text = lst.BusinessComment;
                txtAgroComment.Text = lst.BusinessComment;
                hfCardNumber.Value = lst.CardNumber;
                lblCommission.Text = "";
                lblCommission.Text = lst.RequestGrantComission.ToString();
                tbActualDate.Text = Convert.ToDateTime(lst.ActualDate).ToString("dd.MM.yyyy"); 
                RadNumTbMonthlyInstallment.Text = lst.MonthlyInstallment.ToString();
                TbAmountOfDownPayment.Text = lst.AmountDownPayment.ToString();
                hfCustomerID.Value = lst.CustomerID.ToString();
                hfCreditID.Value = lst.CreditID.ToString();
                hfRequestID.Value = id.ToString();

                btnComment.Visible = true;
                pnlPhoto.Visible = false;

                btnForPeriod.Visible = false;
                btnForPeriodWithHistory.Visible = false;
                btnForPeriodWithProducts.Visible = false;

                tbSurname2.Text = lst.Surname;
                tbCustomerName2.Text = lst.CustomerName;
                tbOtchestvo2.Text = lst.Otchestvo;
                tbINN2.Text = lst.IdentificationNumber;
                tbINNOrg.Text = lst.OrganizationINN;
                tbGuarantorSurname.Text = lst.GuarantorSurname;
                tbGuarantorName.Text = lst.GuarantorName;
                tbGuarantorOtchestvo.Text = lst.GuarantorOtchestvo;
                tbGuarantorINN.Text = lst.GuarantorIdentificationNumber;
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
                    pnlEmployment.Visible = true;
                    RadNumTbAverageMonthSalary.Text = lst.AverageMonthSalary.ToString();
                    RadNumTbSumMonthSalary.Text = lst.SumMonthSalary.ToString();
                    ddlMonthCount.SelectedIndex = ddlMonthCount.Items.IndexOf(ddlMonthCount.Items.FindByValue(lst.CountMonthSalary.ToString()));
                    btnSozfondAgree.Enabled = true;
                    RadNumTbFamilyExpenses.Text = lst.FamilyExpenses.ToString();
                }
                if (lst.Bussiness == 1)
                {
                    rbtnBusiness.SelectedIndex = 1;
                    pnlBusiness.Visible = true;
                    pnlAgro.Visible = false;
                    pnlEmployment.Visible = false;
                    //RadNumTbRevenue.Text = lst.Revenue.ToString();
                    RadNumTbMaxRevenue.Text = lst.MaxRevenue.ToString();
                    RadNumTbMinRevenue.Text = lst.MinRevenue.ToString();
                    ddlCountWorkDay.SelectedIndex = ddlCountWorkDay.Items.IndexOf(ddlCountWorkDay.Items.FindByValue(lst.CountWorkDay.ToString()));
                    RadNumTbСostPrice.Text = lst.СostPrice.ToString();
                    RadNumTbOverhead.Text = lst.Overhead.ToString();
                    RadNumTbFamilyExpenses.Text = lst.FamilyExpenses.ToString();
                    btnSozfondAgree.Enabled = true;

                    if (lst.GroupID == 134)
                    {
                        SysController sysCtrl = new SysController();
                        var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
                        //var findate = dbRWZ.FinDataColas.Where(r => r.INN == customers.IdentificationNumber).OrderBy(u => u.DateLoad).Last();
                        //var findate = dbRWZ.FinDataColas.Where(r => r.INN == customers.IdentificationNumber).FirstOrDefault();
                        var findate = dbRWZ.FinDataColas.Where(r => r.Number == lst.NumberCola).FirstOrDefault();
                        //lblShop.Text = findate.Name;
                        //lblSummCredit.Text = request.RequestSumm.ToString();
                        //lblCategory.Text = findate.Segment;
                        gvProducts.Enabled = false;
                        try
                        {

                            switch (findate.Segment)
                            {
                                case "BASIC":
                                    {
                                         Overhead = 108500; lblOtherLoansComment.Text = "Максимально допустимая сумма - 70000"; hfOtherLoans2.Value = "70000";
                                        hfRevenueMonth.Value = "2861094"; RadNumTbFamilyExpenses.Text = "30000";
                                        //revRadNumOtherLoans.ValidationExpression = "^[7-9][0-9](?!000$)[0-9][0-9][1-9]?\\d+$"; revRadNumOtherLoans.ErrorMessage = "70000"; 
                                    }
                                    break;
                                case "GOLD":
                                    {
                                         Overhead = 59500; lblOtherLoansComment.Text = "Максимально допустимая сумма - 70000"; hfOtherLoans2.Value = "70000";
                                        hfRevenueMonth.Value = "1703636"; RadNumTbFamilyExpenses.Text = "25000";
                                        //revRadNumOtherLoans.ValidationExpression = "^[7-9][0-9](?!000$)[0-9][0-9][1-9]?\\d+$"; revRadNumOtherLoans.ErrorMessage = "70000"; 
                                    }
                                    break;
                                case "SILVER":
                                    { 
                                        Overhead = 43500; lblOtherLoansComment.Text = "Максимально допустимая сумма - 50000"; hfOtherLoans2.Value = "50000";
                                        hfRevenueMonth.Value = "984143"; RadNumTbFamilyExpenses.Text = "20000";
                                        //   revRadNumOtherLoans.ValidationExpression = "^[3-9][5-9](?!000$)[0-9][0-9][1-9]?\\d+$"; revRadNumOtherLoans.ErrorMessage = "35000"; 
                                    }
                                    break;
                                case "BRONZE":
                                    {
                                        Overhead = 30500; lblOtherLoansComment.Text = "Максимально допустимая сумма - 20000"; hfOtherLoans2.Value = "20000";
                                        hfRevenueMonth.Value = "485291"; RadNumTbFamilyExpenses.Text = "20000";
                                        //  revRadNumOtherLoans.ValidationExpression = "^[3-9][0-9](?!000$)[0-9][0-9][1-9]?\\d+$"; revRadNumOtherLoans.ErrorMessage = "30000"; 
                                    }
                                    break;
                            }
                       

                        RadNumTbMaxRevenue.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(hfRevenueMonth.Value) / 30), 2).ToString();  // (Convert.ToDouble(monthBuy / 30) * 10).ToString();
                        RadNumTbMinRevenue.Text = Math.Round(Convert.ToDouble(Convert.ToDouble(hfRevenueMonth.Value) / 30), 2).ToString();   //(Convert.ToDouble(monthBuy / 30) * 10).ToString(); ;

                        }
                        catch (Exception ex)
                        {
                            MsgBox("Клиента нет в списке ", this.Page, this);
                        }
                        ddlCountWorkDay.SelectedIndex = ddlCountWorkDay.Items.IndexOf(ddlCountWorkDay.Items.FindByValue("30"));
                        RadNumTbСostPrice.Text = "25";
                        RadNumTbOverhead.Text = Overhead.ToString();
                        

                        RadNumTbMaxRevenue.Enabled = false;
                        RadNumTbMinRevenue.Enabled = false;
                        ddlCountWorkDay.Enabled = false;
                        RadNumTbСostPrice.Enabled = false;
                        RadNumTbOverhead.Enabled = false;
                        RadNumTbFamilyExpenses.Enabled = false;
                        rbtnBusiness.Enabled = false;
                        /***/
                        tbActualDate.Visible = false;
                        //RegularExpressionValidator14.Visible = false;
                        /***/

                        lblColaComment.Visible = true;
                        txtColaComment.Visible = true;
                        txtColaComment.Text = findate.Number + ", " + findate.Name + ", " + findate.Address + ", " + findate.Segment;

                    }
                    else 
                    {
                        lblColaComment.Visible = false;
                        txtColaComment.Visible = false;
                        /***/
                        tbActualDate.Visible = true;
                        //RegularExpressionValidator14.Visible = true;
                        /***/

                        RadNumTbMaxRevenue.Enabled = true;
                        RadNumTbMinRevenue.Enabled = true;
                        ddlCountWorkDay.Enabled = true;
                        RadNumTbСostPrice.Enabled = true;
                        RadNumTbOverhead.Enabled = true;
                        RadNumTbFamilyExpenses.Enabled = true;
                        rbtnBusiness.Enabled = true;

                    }

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
                    RadNumTbRevenueMilkProd.Text = lst.RevenueMilkProd.ToString();
                    RadNumTbOverheadAgro.Text = lst.OverheadAgro.ToString();
                    RadNumTbAddOverheadAgro.Text = lst.AddOverheadAgro.ToString();
                    RadNumTbFamilyExpenses.Text = lst.FamilyExpenses.ToString();
                    btnSozfondAgree.Enabled = true;
                }
                /***********************/
                history(id);
                btnCustomerSearch.Enabled = false;
                btnCloseRequest.Visible = true; //закры форму заявки
                //btnCalculator.Visible = true; //калькулятор
                btnNewRequest.Visible = true; //новая заявка
                btnProffer.Visible = false; //предложение зп
                btnApplicationForm.Visible = true;
                btnNoConfirm.Visible = false; //статус не подтвержден
                btnFixed.Visible = false; //статус исправлено
                btnFix.Visible = false; //статус исправить
                btnConfirm.Visible = false; //статус подтвердить
                btnIssue.Visible = false; //статус выдать
                btnCancelIssue.Visible = false;
                btnCancelReq.Visible = false; //статус отменить
                btnCancelReqExp.Visible = false; //статус отказать
                btnReceived.Visible = false; //статус принято
                btnSendCreditRequest.Visible = false; //Сохранить
                //pnlEmployment.Visible = false; // зп
                btnAgreement.Visible = false; //договор
                chbEmployer.Enabled = false; //Сотрудник
                btnInProcess.Visible = false; //
                btnApproved.Visible = false; //

                btnSignature.Visible = false;

                ddlOffice.Visible = false;
                btnSaveOffice.Visible = false;


                if ((lst.RequestStatus == "Выдано") || (lst.RequestStatus == "Принято"))
                {
                    btnUpdFIO.Visible = false;
                }
                else
                {
                    btnUpdFIO.Visible = true;
                }


                if (lst.GroupID == 128)
                {
                    btnApproved.Text = "Верификация пройдена"; btnApproved.Enabled = false; TbAmountOfDownPayment.Enabled = false;
                    btnFix.Visible = false; btnGetMPZ.Visible = true;
                    btnGetScoreBee.Visible = false;

                    ddlOffice.SelectedIndex = ddlOffice.Items.IndexOf(ddlOffice.Items.FindByValue(lst.OfficeID.ToString()));

                    if (lst.IsReceivedMPZ == true) { btnApproved.Enabled = true; }

                    if (lst.RequestStatus == "Новая заявка")
                    {
                        if ((Convert.ToInt32(Session["RoleID"]) == 2) || (Convert.ToInt32(Session["RoleID"]) == 5))
                        {
                            ddlOffice.Visible = true;
                            btnSaveOffice.Visible = true;
                        }
                    }
                }
                else
                {
                    btnApproved.Text = "Утверждено"; btnApproved.Enabled = true; btnFix.Visible = false; btnGetMPZ.Visible = false;
                    btnGetScoreBee.Visible = false;
                }


                if ((lst.GroupID == 128) && ((Convert.ToInt32(Session["RoleID"]) == 2) || (Convert.ToInt32(Session["RoleID"]) == 5)) && (lst.RequestStatus == "Новая заявка"))
                {
                    // btnApproved.Text = "Верификация пройдена";
                    RadNumTbRequestSumm.ReadOnly = false;
                    //btnGetMPZ.Visible = true;
                }
                else
                if ((lst.GroupID == 134) && ((Convert.ToInt32(Session["RoleID"]) == 5)) && ((lst.RequestStatus == "Подтверждено") || (lst.RequestStatus == "Утверждено")))
                {
                    // btnApproved.Text = "Верификация пройдена";
                    RadNumTbRequestSumm.ReadOnly = false;
                    //btnGetMPZ.Visible = true;
                }
                else
                {
                    // btnApproved.Text = "Верификация пройдена";
                    RadNumTbRequestSumm.ReadOnly = true;
                    //btnGetMPZ.Visible = false;
                }

                //if ((Convert.ToInt32(Session["RoleID"]) == 2) || (Convert.ToInt32(Session["RoleID"]) == 5)) //Эксперты Капитал)
                //{
                //    tbINNOrg.Visible = true;
                //    lblOrgINN.Visible = true;
                //    var blackListCust = dbRWZ.BlackLists.Where(r => (r.IdentificationNo == tbINN2.Text) && (r.CustomerTypeID == 1)).ToList();
                //    if (blackListCust.Count > 0)
                //    {
                //        gvBlackListCustomers.DataSource = blackListCust;
                //        pnlBlackList.Visible = true; //pnlBlackListOrg.Visible = true; 
                //        //BlackListShow();

                //    }
                //    else
                //    {
                //        gvBlackListCustomers.DataSource = null;
                //        pnlBlackList.Visible = false; ///pnlBlackListOrg.Visible = false;
                //    }
                //    gvBlackListCustomers.DataBind();

                //    var blackListOrg = dbRWZ.BlackLists.Where(r => (r.IdentificationNo == tbINNOrg.Text) && (r.CustomerTypeID == 2)).ToList();
                //    if (blackListOrg.Count > 0)
                //    {
                //        gvBlackListOrg.DataSource = blackListOrg;
                //        pnlBlackListOrg.Visible = true;
                //        //lblBlacListOrg.Visible = true;
                //        //BlackListShow();
                //    }
                //    else
                //    {
                //        pnlBlackListOrg.Visible = false;
                //        gvBlackListOrg.DataSource = null;
                //        //lblBlacListOrg.Visible = false;
                //    }
                //    gvBlackListOrg.DataBind();


                //}
                //else
                //{
                //    tbINNOrg.Visible = false;
                //    lblOrgINN.Visible = false;
                //    pnlBlackList.Visible = false;
                //    pnlBlackListOrg.Visible = false;
                //    //BlackListHide();
                //}



                if (lst.RequestStatus == "Новая заявка")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    chbEmployer.Enabled = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        if ((lst.GroupID == 51) //Cулпак
                            || (lst.GroupID == 50) //Планета
                            || (lst.GroupID == 30) //Технодом
                            || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnNoConfirm.Visible = true; //Не подтверждено
                        btnConfirm.Visible = true; //Подтверждено
                        btnInProcess.Visible = true; //В обработке
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnApproved.Visible = true; //Утверждено
                        btnFix.Visible = true; // Исправить

                        btnProffer.Visible = true; //Предложение

                        if (lst.GroupID == 128) { btnFix.Visible = false; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnApproved.Visible = true; //Утверждено
                        btnFix.Visible = true; // Исправить
                        if (lst.GroupID == 128) { btnFix.Visible = false; }
                        btnProffer.Visible = true; //Предложение

                        //if ((Convert.ToInt32(Session["RoleID"]) == 2) || (Convert.ToInt32(Session["RoleID"]) == 5))
                       

                    }


                }
                //if (lst.RequestStatus == "В обработке")
                //{
                //    gvProducts.Enabled = true;
                //    btnSendCreditRequest.Visible = true;
                //    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                //    if (Convert.ToInt32(Session["RoleID"]) == 13) { btnCancelReq.Visible = true; } //Агенты Билайн
                //    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                //    {
                //        btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = true;
                //        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                //    }
                //    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                //    {
                //        btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = false; btnApproved.Visible = true;
                //        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                //    }
                //    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                //    {
                //        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; btnConfirm.Visible = true; btnInProcess.Visible = true;
                //    }
                //}

                if (lst.RequestStatus == "Исправить")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnFixed.Visible = true; //Исправлено
                        if ((lst.GroupID == 51) //Cулпак
                           || (lst.GroupID == 50) //Планета
                           || (lst.GroupID == 30) //Технодом
                           || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }

                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFixed.Visible = true; //Исправлено

                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = false; btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; btnConfirm.Visible = true; btnInProcess.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 24) //Эксперты бизнес
                    { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                }

                if (lst.RequestStatus == "Исправлено")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        if ((lst.GroupID == 51) //Cулпак
                           || (lst.GroupID == 50) //Планета
                           || (lst.GroupID == 30) //Технодом
                           || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFix.Visible = true; //Исправить
                        btnApproved.Visible = true; //Утверждено

                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFix.Visible = true; //Исправить
                        btnApproved.Visible = true; //Утверждено

                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; btnFix.Enabled = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                        //btnCancelReq.Visible = true; btnNoConfirm.Visible = true; btnConfirm.Visible = true; btnInProcess.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 24) //Эксперты бизнес
                    { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                }
                //if (lst.RequestStatus == "Не подтверждено")
                //{
                //    gvProducts.Enabled = true;
                //    btnSendCreditRequest.Visible = true;
                //    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                //    if (Convert.ToInt32(Session["RoleID"]) == 13) { btnFixed.Visible = true; btnCancelReq.Visible = true; } //Агенты Билайн
                //    if (Convert.ToInt32(Session["RoleID"]) == 2) { btnCancelReqExp.Visible = true; } //Эксперты 
                //    if (Convert.ToInt32(Session["RoleID"]) == 5) { btnCancelReqExp.Visible = true; btnApproved.Visible = true; } //Эксперты ГБ
                //    if (Convert.ToInt32(Session["RoleID"]) == 14) { btnConfirm.Visible = true; btnCancelReq.Visible = true; btnInProcess.Visible = true; btnApproved.Visible = true; } //Админы Билайн
                //}
                if (lst.RequestStatus == "Отменено")
                {
                    btnSozfondAgree.Visible = true;
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        if ((lst.GroupID == 51) //Cулпак
                          || (lst.GroupID == 50) //Планета
                          || (lst.GroupID == 30) //Технодом
                          || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {

                        btnProffer.Visible = true; //Предложение
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnProffer.Visible = true; //Предложение
                    }
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                }
                if (lst.RequestStatus == "Отказано")
                {
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {

                        btnProffer.Visible = true; //Предложение
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnProffer.Visible = true; //Предложение
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        if ((lst.GroupID == 51) //Cулпак
                          || (lst.GroupID == 50) //Планета
                          || (lst.GroupID == 30) //Технодом
                          || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                }
                if (lst.RequestStatus == "Подтверждено")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    { }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnProffer.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true; btnSendCreditRequest.Visible = true;
                        btnApproved.Visible = true; 
                        gvProducts.Enabled = true;
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnProffer.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true; btnSendCreditRequest.Visible = true;
                        btnApproved.Visible = true;
                        gvProducts.Enabled = true;
                        
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        if (lst.GroupID == 134)
                        {
                            ddlOffice.Visible = true;
                            btnSaveOffice.Visible = true;
                            gvProducts.Enabled = false;
                            TbAmountOfDownPayment.Enabled = false;
                            btnFix.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                        btnNoConfirm.Visible = true; btnCancelReq.Visible = true; btnSendCreditRequest.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 24) //Эксперты бизнес
                    { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                }
                if (lst.RequestStatus == "Утверждено")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты 
                    {
                        btnSignature.Visible = true; //К подписи
                        if ((lst.GroupID == 51) //Cулпак
                           || (lst.GroupID == 50) //Планета
                           || (lst.GroupID == 30) //Технодом
                           || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                        btnFixed.Visible = false;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnSendCreditRequest.Visible = true; //Сохранить

                        btnProffer.Visible = true; // Предложение
                        gvProducts.Enabled = true;
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnSendCreditRequest.Visible = true; //Сохранить

                        btnProffer.Visible = true; // Предложение
                        gvProducts.Enabled = true;
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 24) //Эксперты бизнес
                    {
                        if (lst.GroupID == 134)
                        {
                            btnSignature.Visible = true; //К подписи 
                            btnCancelReq.Visible = true; //Отменено
                            btnProffer.Visible = true; //Предложение
                            gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; 
                        }
                    }
                }

                if (lst.RequestStatus == "К подписи")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = false;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        if ((lst.GroupID == 51) //Cулпак
                           || (lst.GroupID == 50) //Планета
                           || (lst.GroupID == 30) //Технодом
                           || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано

                        btnProffer.Visible = true; //Предложние
                        gvProducts.Enabled = true;
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано

                        btnProffer.Visible = true; //Предложние
                        gvProducts.Enabled = true;
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 24) //Эксперты бизнес
                    {
                        
                        if (lst.GroupID == 134)
                        {
                            gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false;
                            btnProffer.Visible = true; //Предложение
                        }
                    }
                }
                if (lst.RequestStatus == "На выдаче")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = false;
                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        btnIssue.Visible = true; //Выдано 
                        if ((lst.GroupID == 51) //Cулпак
                           || (lst.GroupID == 50) //Планета
                           || (lst.GroupID == 30) //Технодом
                           || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано

                        btnProffer.Visible = true; //Предложение
                        gvProducts.Enabled = true;
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //if (lst.GroupID == 134)
                        //{
                        //    btnIssue.Visible = true; //Выдано 
                        //}
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано

                        btnProffer.Visible = true; //Предложение
                        gvProducts.Enabled = true;
                        if (lst.GroupID == 134) { gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; }
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        if (lst.GroupID == 128) { btnIssue.Visible = true; }
                    }

                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 24) //Эксперты бизнес
                    {
                        if (lst.GroupID == 134)
                        {
                            btnIssue.Visible = true; //Выдано 
                            btnProffer.Visible = true; //Предложение
                            gvProducts.Enabled = false; TbAmountOfDownPayment.Enabled = false; 
                        }
                    }
                }
                if (lst.RequestStatus == "Выдано")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = false;
                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        if ((lst.GroupID == 51) //Cулпак
                           || (lst.GroupID == 50) //Планета
                           || (lst.GroupID == 30) //Технодом
                           || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnReceived.Visible = true; //Принято

                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnReceived.Visible = true; //Принято

                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        if (lst.GroupID != 128)
                        {
                            if (hfUserID.Value == "6075") btnCancelIssue.Visible = true;
                            if (hfUserID.Value == "6248") btnCancelIssue.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 24) //Эксперты бизнес
                    {
                        if (lst.GroupID == 134)
                        {
                           
                            btnProffer.Visible = true; //Предложение
                        }
                    }
                }
                if (lst.RequestStatus == "Принято")
                {
                    btnSozfondAgree.Visible = true;
                    gvProducts.Enabled = false;
                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                    {
                        if ((lst.GroupID == 51) //Cулпак
                          || (lst.GroupID == 50) //Планета
                          || (lst.GroupID == 30) //Технодом
                          || (lst.GroupID == 127)) //Два прораба
                        { btnProffer.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Error: " + ex.Message + "<hr>" + ex.Source + "<hr>" + ex.StackTrace, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
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

        //public void BlackListHide()
        //{
        //    pnlBlackList.Visible = false; pnlBlackListOrg.Visible = false;
        //    tbINNOrg.Visible = false; lblOrgINN.Visible = false;
        //    //RequiredFieldValidator5.Visible = false;
        //    //RegularExpressionValidator14.Visible = false;
        //    lblBlacListCust.Visible = false;
        //}

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            databindDDL();
            //customerFieldDisable();
            disableCustomerFields();
            pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            hfCustomerID.Value = "noselect";
            btnCredit.Text = "Выбрать клиента";
            hfChooseClient.Value = "Выбрать клиента";
            tbSurname2.Text = "";
            tbCustomerName2.Text = "";
            tbOtchestvo2.Text = "";
            tbINN2.Text = "";
            clearEditControls();
            lblMessageClient.Text = "";
            tbContactPhone.Text = "";
            tbSearchINN.Text = "";
            btnSaveCustomer.Enabled = true;
            tbSerialN.Text = "";
            clear_positions();
            //customerFieldEnable();

            btnSearchClient.Visible = true;
            btnNewCustomer.Visible = true;
        }

        //public void customerFieldEnable()
        //{
        //    tbSurname.Enabled = true;
        //    tbCustomerName.Enabled = true;
        //    tbOtchestvo.Enabled = true;
        //    tbIdentificationNumber.Enabled = true;
        //    tbDocumentSeries.Enabled = true;
        //    tbIssueDate.Enabled = true;
        //    tbValidTill.Enabled = true;
        //    tbIssueAuthority.Enabled = true;
        //    rbtSex.Enabled = true;
        //    tbDateOfBirth.Enabled = true;
        //    tbRegistrationStreet.Enabled = true;
        //    tbRegistrationHouse.Enabled = true;
        //    tbRegistrationFlat.Enabled = true;
        //    tbResidenceStreet.Enabled = true;
        //    //tbContactPhone.Enabled = true;
        //    tbResidenceHouse.Enabled = true;
        //    tbResidenceFlat.Enabled = true;
        //}


        //public void customerFieldDisable()
        //{
        //    tbSurname.Enabled = false;
        //    tbCustomerName.Enabled = false;
        //    tbOtchestvo.Enabled = false;
        //    tbIdentificationNumber.Enabled = false;
        //    tbDocumentSeries.Enabled = false;
        //    tbIssueDate.Enabled = false;
        //    tbValidTill.Enabled = false;
        //    tbIssueAuthority.Enabled = false;
        //    rbtSex.Enabled = false;
        //    tbDateOfBirth.Enabled = false;
        //    tbRegistrationStreet.Enabled = false;
        //    tbRegistrationHouse.Enabled = false;
        //    tbRegistrationFlat.Enabled = false;
        //    tbResidenceStreet.Enabled = false;
        //    //tbContactPhone.Enabled = false;
        //    tbResidenceHouse.Enabled = false;
        //    tbResidenceFlat.Enabled = false;

        //}


        protected void btnGuarantorSearch_Click(object sender, EventArgs e)
        {
        }

        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            hfPhoto2.Value = "";
            gvPositions.DataSource = null;
            gvPositions.DataBind();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
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
            lblCreditN.Text = "";
            lblComission.Text = "";
            /**/
            btnAddProductClick = 1; clearEditControls();
            clearEditControlsRequest();
            hfRequestAction.Value = "new";
            hfRequestStatus.Value = "";
            btnSendCreditRequest.Enabled = true;

            btnSozfondAgree.Visible = false;
            btnComment.Visible = false;
            RadNumTbFamilyExpenses.Text = "7000";
            btnAgreement.Visible = false;
            btnPledgeAgreement.Visible = false;
            btnProffer.Visible = false;
            btnReceptionAct.Visible = false;
            btnIssue.Visible = false;
            btnCancelIssue.Visible = false;
            pnlNewRequest.Visible = true;
            btnCloseRequest.Visible = true;
            //btnCalculator.Visible = false;
            btnSendCreditRequest.Visible = true;
            chbEmployer.Checked = false;
            btnNewRequest.Visible = false;
            btnGetMPZ.Visible = false;
            btnGetScoreBee.Visible = false;
            ddlOffice.Visible = false;
            btnSaveOffice.Visible = false;
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
            refreshProducts(1); // firstAmount();
            refreshfiles();
            //btnCustomerEdit.Enabled = false;
            gvHistory.DataSource = null;
            gvHistory.DataBind();
            gvProducts.Enabled = true;
            //BlackListHide();

            btnUpdFIO.Visible = false;
        }

        public void clearEditControlsRequest()
        {
            lblOtherLoansComment.Text = "";
            tbCreditPurpose.Text = "";
            RadNumTbTotalPrice.Text = "";
            TbAmountOfDownPayment.Text = "";
            RadNumTbRequestSumm.Text = "";
            tbActualDate.Text = "";
            tbSurname2.Text = "";
            tbCustomerName2.Text = "";
            tbOtchestvo2.Text = "";
            tbINN2.Text = "";
            tbGuarantorName.Text = "";
            tbGuarantorSurname.Text = "";
            tbGuarantorOtchestvo.Text = "";
            tbGuarantorINN.Text = "";
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
            hfCustomerID.Value = "noselect";
            hfGuarantorID.Value = "noselect";
            btnCustomerSearch.Enabled = true;
            RadNumTbSumMonthSalary.Text = "";


            RadNumTbSumMonthSalary.Text = "";
            ddlMonthCount.Text = "1";
            RadNumOtherLoans.Text = "";
            RadNumTbRevenueAgro.Text = "";
            RadNumTbRevenueMilk.Text = "";
            RadNumTbRevenueMilkProd.Text = "";
            RadNumTbOverheadAgro.Text = "";
            RadNumTbAddOverheadAgro.Text = "";
            RadNumTbFamilyExpenses.Text = "7000";
        }

        protected void btnSearchRequest_Click(object sender, EventArgs e)
        {
            hfRequestsRowID.Value = "";
            clearEditControlsRequest();
            pnlNewRequest.Visible = false;
            //refreshGrid();
            btnForPeriod.Visible = true;
            btnForPeriodWithHistory.Visible = true;
            btnForPeriodWithProducts.Visible = true;
            pnlNewRequest.Visible = false;
        }

        protected void gvRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnAgreement_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            string worktype = "0";
            if (rbtnBusiness.SelectedIndex == 0) worktype = "0";
            if (rbtnBusiness.SelectedIndex == 1) worktype = "1";
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["UserID"] = hfUserID.Value; //Convert.ToInt32(Session["UserID"].ToString());
            Session["RequestID"] = hfRequestID.Value;
            Session["WorkType"] = worktype;
            //            Response.Redirect(this.EditUrl("", "", "rptAgrees"));


            CreditController crdCtr = new CreditController();
            var lst = dbRWZ.Requests.Where(r => r.CreditID == Convert.ToInt32(hfCreditID.Value)).ToList().FirstOrDefault();
            if (lst.GroupID == 134)
            {
                Response.Redirect("rptAgreesCola");
            }

            var lstGraphics = crdCtr.GraphicsGetItem(Convert.ToInt32(hfCreditID.Value));
            if (lstGraphics.Count > 0)
            {
                
                if (lst.OrgID == 36)
                {
                    if (ddlRequestRate.SelectedValue == "0,00")
                    {
                        Response.Redirect("rptAgreesToyotaInst");
                    }
                    else
                    {
                        Response.Redirect("rptAgreesToyota");
                    }
                }
                if (ddlRequestRate.SelectedValue == "0,00")
                {
                    Response.Redirect("rptAgreesInst");
                }
                else
                {
                    Response.Redirect("rptAgrees");
                }
            }
            else
            {
                MsgBox("График погашений не построен, обратитесь к специалисту Банка", this.Page, this);
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
            refreshProducts(1); firstAmount();
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

        //protected string filedir = "Partcredits";

        protected void btnUploadFiles_Click(object sender, EventArgs e)
        {
            //string filename = "", 
             string destinationFile = "";
            tbFileDescription.Text = "";
            //dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            //int? orgID = dbRWZ.CardRequests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault().OrgID;

            if (FileUploadControl.HasFile)
            {
                try
                {
                    //if (FileUploadControl.PostedFile.ContentType == "image/jpeg")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 20480000)
                        {
                            //filename = Path.GetFileName(FileUploadControl.FileName);
                            if (FileUploadControl.FileName != null)
                            {
                                //fullfilename = UploadImageAndSave(true, FileUploadControl.FileName);

                                string destinationFolder = getDestinationFolder();
                                GeneralController gctl = new GeneralController();
                                string dateRandodir = gctl.DateRandodir(destinationFolder);
                                destinationFile = gctl.fileNameAddExt(FileUploadControl.FileName, destinationFolder, dateRandodir);

                                //destinationFile = UploadImageAndSave(true, destinationFolder + "\\" + dateRandodir, destinationFile);



                                //string destinationFolderFile = destinationFolder + "\\" + destinationFile;
                                try
                                {
                                    FileUploadControl.SaveAs(destinationFolder + "\\" + dateRandodir + "\\" +  destinationFile);
                                }
                                catch (Exception ex)
                                {
                                    MsgBox(ex.ToString(), this.Page, this);
                                }


                                string contentType = FileUploadControl.PostedFile.ContentType.ToLower();
                                {
                                    {
                                        RequestsFile newRequestFile = new RequestsFile
                                        {
                                            Name = FileUploadControl.FileName,
                                            RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
                                            ContentType = contentType,
                                            //Data = bytes,
                                            //FullName = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + fullfilename,
                                            //FullName = "\\Portals\\0\\" + partnerdir + "\\" + destinationFile,
                                            //FullName2 = fileupl + "\\" + "Portals\\0\\" + partnerdir + "\\" + destinationFile,
                                            FullName = "\\" + partnerdir + "\\" + dateRandodir + "\\" + destinationFile,
                                            FullName2 = fileupl + "\\" + partnerdir + "\\" + dateRandodir + "\\" + destinationFile,
                                            FileDescription = tbFileDescription.Text + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                                            IsPhoto = false
                                        };
                                        ItemController ctl = new ItemController();
                                        ctl.ItemRequestFilesAddItem(newRequestFile);
                                    }
                                }
                                refreshfiles();
                            }

                        }
                        else
                            StatusLabel.Text = "Статус загрузки: Максимальный размер файла 20мб!";
                    }
                    //else StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Статус загрузки: Файл не загружен. Ошибка: " + ex.Message;
                }
            }
        }

        //protected string UploadImageAndSave(bool hasfile, string filename) //main function
        //{
        //    if (hasfile)
        //    {
        //        CheckImageDirs();

        //        string filepath = PortalSettings.HomeDirectoryMapPath + filedir + "\\" + filename;
        //        int temp_ext = 0;
        //        while (System.IO.File.Exists(filepath))
        //        {
        //            temp_ext = DateTime.Now.Millisecond;
        //            string ext_name = System.IO.Path.GetExtension(filepath);
        //            string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + temp_ext;
        //            filename = filename_no_ext + temp_ext + ext_name;
        //            filepath = PortalSettings.HomeDirectoryMapPath + filedir + "\\" + filename;
        //        }
        //        string path = System.IO.Path.GetFileName(filename);
        //        AsyncUpload1.UploadedFiles[0].SaveAs(filepath);
        //    }
        //    return filename;
        //}

        //protected void CheckImageDirs()
        //{
        //    if (!System.IO.Directory.Exists(PortalSettings.HomeDirectoryMapPath + filedir))
        //        System.IO.Directory.CreateDirectory(PortalSettings.HomeDirectoryMapPath + filedir);

        //    if (!System.IO.Directory.Exists(PortalSettings.HomeDirectoryMapPath + filedir))
        //        System.IO.Directory.CreateDirectory(PortalSettings.HomeDirectoryMapPath + filedir);
        //}

        //protected string UploadImageAndSave(bool hasfile, string destinationFolder, string destinationFile) //main function
        //{

                
        //        string destinationFolderFile = destinationFolder + "\\" + destinationFile;

        //        try
        //        {

        //            //MsgBox(filepath.ToString(), this.Page, this);
        //            FileUploadControl.SaveAs(destinationFolderFile);
                    
        //        }
        //        catch (Exception ex)
        //        {
        //            MsgBox(ex.ToString(), this.Page, this);
        //        }
        //    //}
        //    return destinationFile;
        //}

        //protected void CheckImageDirs()
        //{
        //    if (!System.IO.Directory.Exists(Server.MapPath("~/") + partnerdir))
        //        System.IO.Directory.CreateDirectory(Server.MapPath("~/") + partnerdir);

        //    if (!System.IO.Directory.Exists(Server.MapPath("~/") + partnerdir))
        //        System.IO.Directory.CreateDirectory(Server.MapPath("~/") + partnerdir);
        //}


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
            btnFixed.Visible = false; //статус новая
            btnConfirm.Visible = false; //статус подтвердить
            btnIssue.Visible = false; //статус выдать
            btnCancelReq.Visible = false; //статус отменить
            btnCancelReqExp.Visible = false; //статус отказать
            btnReceived.Visible = false; //статус принято
        }

    



        protected async void btnIssue_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.RequestStatus == "На выдаче")
                if (lst.GroupID == 128) //сайт ДКБ
                {
                    //********меняем статус в ОБ ***Begin**/
                    //string strOB = await IssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                    //********меняем статус в ОБ ***End**/

                    //********меняем статус в ОБ выдано*****//
                    string strOB = "", strNur = "";
                    var statusOB = dbR.HistoriesStatuses.Where(h => ((h.CreditID == lst.CreditID) && (h.StatusID == 5))).ToList();
                    if (statusOB.Count > 0)
                    {
                        strOB = "200";
                    }
                    else
                    {
                        try
                        {
                            //strOB = await AtIssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                            strOB = await IssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                        }
                        catch (Exception ex)
                        {
                            //TextBox1.Text = TextBox1.Text + ex.Message;
                            //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                            MsgBox(strOB + " " + ex.ToString(), this.Page, this);
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
                            //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса На выдаче в АБС" + strOB, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса в АБС "+ strOB);
                            MsgBox("Ошибка при отправке статуса на сервер АБС " + strOB, this.Page, this);
                        }
                    }
                    /**********************************/

                    //********отправляем статус в нуртелеком ****Begin ***/
                    //strNur = await SendStatusNur(lst.CreditID.ToString(), "ISSUED_BY");
                    //if (strNur == "200")
                    //{

                    //}
                    //else
                    //{
                    //    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса ISSUED_BY в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //}
                    //********отправляем статус в нуртелеком ****End ***/

                    //if ((strNur == "200") && (strOB == "200"))
                    if (strOB == "200")
                    {
                        IssueRequest();
                    }
                }
                //else
                //if (lst.GroupID == 134)
                //{
                //    string strOB = "", strNur = "";
                //    var statusOB = dbR.HistoriesStatuses.Where(h => ((h.CreditID == lst.CreditID) && (h.StatusID == 5))).ToList();
                //    if (statusOB.Count > 0)
                //        {
                //            strOB = "200";
                //        }
                //    else
                //        try
                //        {
                //            //strOB = await AtIssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                //            strOB = await IssueRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString());
                //        }
                //        catch (Exception ex)
                //        {
                //            //TextBox1.Text = TextBox1.Text + ex.Message;
                //            //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //            //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                //            MsgBox(strOB + " " + ex.ToString(), this.Page, this);
                //        }
                //        finally
                //        {
                //            ////TextBox1.Text = TextBox1.Text + Response.ToString();

                //        }


                //    if (strOB == "200")
                //    {
                //        /*RequestHistory*/
                //        //var reqs = dbW.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList().FirstOrDefault();
                //        //reqs.RequestStatus = "На выдаче";
                //        //dbW.Requests.Context.SubmitChanges();
                //        //SignatureRequest();
                //        //AtIssueRequest();
                //    }
                //    else
                //    {
                //        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса На выдаче в АБС" + strOB, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //        //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса в АБС "+ strOB);
                //        MsgBox("Ошибка при отправке статуса на сервер АБС " + strOB, this.Page, this);
                //    }

                //    //********отправляем статус в DCB ****Begin ***/
                //    respDCB360 resp = null;
                //    try
                //    {
                //        resp = await SendStatusDCB360(lst.CreditID.ToString(), "ISSUED_BY");
                //    }
                //    catch (Exception ex)
                //    {
                //        //lblError.Text = ex.ToString();
                //        //lblError.Visible = true;

                //    }
                //    //respDCB360 resp = await SendStatusDCB360(532646.ToString(), "APPROVED");
                //    //lblError.Text = strOB + "-" + resp.error;

                //    if ((strOB == "200") && ((resp.statusCode == 200) || (resp.statusCode == 400)))
                //    {
                //        //VerificationRequest(); 
                //        //sms begin
                //        //string str = await SendSMSWithOurRoute("", "");
                //        //if (str == "0: Accepted for delivery")
                //        //{
                //        //    //Console.WriteLine("send");
                //        //}
                //        //sms end
                //        IssueRequest(); 
                //        //AddRowToJournalDCBCola(lst, "Утверждено");
                //    }
                //    else
                //    {
                //        if (strOB != "200")
                //        {
                //            MsgBox(strOB + "Ошибка при отправке статуса APPROVED на сервер ОБ", this.Page, this);
                //            //lblError.Text = "Ошибка при отправке статуса APPROVED на сервер ОБ" + resp.error;
                //            //lblError.Visible = true;
                //        }
                //        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED на сервер DCB360", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //        if (resp.statusCode == 404)
                //        {
                //            MsgBox("Ответ от DCB360: 404 Credit id не найден", this.Page, this);
                //            //System.Windows.Forms.MessageBox.Show("Ответ от DCB360: 404 Credit id не найден");
                //            //lblError.Text = "Ответ от DCB360: 404 Credit id не найден-" + resp.error;
                //            //lblError.Visible = true;
                //        }
                //        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED на сервер ОБ", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);

                //    }
                //    //********отправляем статус в DCB ****End ***/
                  

                //}
                else
                {
                    //********меняем статус в скоринге если не нано
                    IssueRequest();
                }
            /**************************************/
            pnlNewRequest.Visible = false;
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
                    string strurl = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/IssueLoanRequest?customerID=" + CustomerID + "&creditID=" + CreditID;
                    var response = await client.PostAsync(strurl, new StringContent(json, Encoding.UTF8, "application/json"));
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
                        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, result, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        //System.Windows.Forms.MessageBox.Show(result.ToString());
                        MsgBox(result.ToString(), this.Page, this);
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
            try
            {
                TextBox ProductMark = ((TextBox)gvProducts.FooterRow.FindControl("txtProductMark"));
            TextBox ProductSerial = ((TextBox)gvProducts.FooterRow.FindControl("txtProductSerial"));
            TextBox ProductImei = ((TextBox)gvProducts.FooterRow.FindControl("txtProductImei"));
            TextBox Price = ((TextBox)gvProducts.FooterRow.FindControl("txtPrice"));

            TextBox Note = ((TextBox)gvProducts.FooterRow.FindControl("txtNote"));
            if ((hfRequestStatus.Value != "Выдано") || (hfRequestStatus.Value != "На выдаче") || (hfRequestStatus.Value != "Отменено") || (hfRequestStatus.Value != "Отказано") || (hfRequestStatus.Value != "Подтверждено") || (hfRequestStatus.Value != "Принято") || (hfRequestStatus.Value != "Утверждено") || (hfRequestStatus.Value != "К подписи"))
            {
                ItemController ctl = new ItemController();
                /*Тариф*/

                RequestsProduct newRequestProduct = new RequestsProduct
                {
                    RequestID = Convert.ToInt32(hfRequestID.Value),
                    ProductMark = ProductMark.Text,
                    ProductSerial = ProductSerial.Text,
                    ProductImei = ProductImei.Text,
                    Note = Note.Text,
                    Price = Convert.ToDecimal(Price.Text),
                    PriceWithTarif = Convert.ToDecimal(Price.Text),
                };
                ctl.ItemRequestProductAddItem(newRequestProduct);
                System.Threading.Thread.Sleep(1000);
                refreshProducts(1); firstAmount();
            }
            productspriceupdate();
            refreshGrid();
            }
            catch (Exception ex)
            {

            }

        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try { 
            int id = Convert.ToInt32(e.CommandArgument);
            if ((hfRequestStatus.Value != "Выдано") || (hfRequestStatus.Value != "На выдаче") || (hfRequestStatus.Value != "Отменено") || (hfRequestStatus.Value != "Отказано") || (hfRequestStatus.Value != "Подтверждено") || (hfRequestStatus.Value != "Принято") || (hfRequestStatus.Value != "Утверждено") || (hfRequestStatus.Value != "К подписи"))
            {
                if (e.CommandName == "Del")
                {
                    ItemController ctl = new ItemController();
                    ctl.DeleteProduct(id);
                    System.Threading.Thread.Sleep(1000);
                    refreshProducts(1); firstAmount();
                    btnAddProductClick = 1;
                }
                productspriceupdate();
            }
            refreshGrid();
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvRequestsFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Del")
            {
                ItemController ctl = new ItemController();
                ctl.DeleteRequestsFiles(id);
                System.Threading.Thread.Sleep(1000);
                refreshProducts(1); firstAmount();
            }
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session["Check"] = null;
            Session["UserName"] = null;
            Session["UserID"] = null;
            Session["FIO"] = null;
            Session.Clear();
            Response.Redirect("/");
        }

      

        

        protected void btnReceptionAct_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Response.Redirect("rptReceptionAct");

        }

        protected void btnProffer_Click(object sender, EventArgs e)
        {
            //string worktype = "0";
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();


            if (lst.GroupID == 134)
            {
                //int usrID = Convert.ToInt32(Session["UserID"].ToString());
                var req = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();

                if (string.IsNullOrEmpty(req.MaxRevenue.ToString()))
                {
                    MsgBox("Необходимо сохранить заявку для открытия Предложения", this.Page, this);
                }
                else
                {
                    Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
                    Session["CustomerID"] = hfCustomerID.Value;
                    Session["CreditID"] = hfCreditID.Value;
                    Session["RequestID"] = hfRequestID.Value;
                    Session["RadNumTbAverageMonthSalary"] = (RadNumTbAverageMonthSalary.Text == "") ? "0" : Convert.ToDouble(RadNumTbAverageMonthSalary.Text).ToString();
                    //Session["RadNumTbRevenue"] = (Revenue.ToString() == "") ? "0" : Revenue.ToString();
                    Session["CountWorkDay"] = (ddlCountWorkDay.Text == "") ? "0" : ddlCountWorkDay.Text;
                    Session["RadNumTbСostPrice"] = (RadNumTbСostPrice.Text == "") ? "0" : Convert.ToDouble(RadNumTbСostPrice.Text).ToString();
                    Session["RadNumTbOverhead"] = (RadNumTbOverhead.Text == "") ? "0" : Convert.ToDouble(RadNumTbOverhead.Text).ToString();
                    Session["RadNumTbFamilyExpenses"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : Convert.ToDouble(RadNumTbFamilyExpenses.Text).ToString();
                    Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : Convert.ToDouble(RadNumOtherLoans.Text).ToString();
                    Response.Redirect("rptProfferCola");
                }
            } else

            if ((lst.GroupID == 128) && (rbtnBusiness.SelectedIndex == 0))
            {
                //int usrID = Convert.ToInt32(Session["UserID"].ToString());
                Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
                Session["CustomerID"] = hfCustomerID.Value;
                Session["CreditID"] = hfCreditID.Value;
                Session["RequestID"] = hfRequestID.Value;
                Session["RadNumTbAverageMonthSalary"] = (RadNumTbAverageMonthSalary.Text == "") ? "0" : Convert.ToDouble(RadNumTbAverageMonthSalary.Text).ToString();
                //Session["RadNumTbRevenue"] = (Revenue.ToString() == "") ? "0" : Revenue.ToString();
                Session["CountWorkDay"] = (ddlCountWorkDay.Text == "") ? "0" : ddlCountWorkDay.Text;
                Session["RadNumTbСostPrice"] = (RadNumTbСostPrice.Text == "") ? "0" : Convert.ToDouble(RadNumTbСostPrice.Text).ToString();
                Session["RadNumTbOverhead"] = (RadNumTbOverhead.Text == "") ? "0" : Convert.ToDouble(RadNumTbOverhead.Text).ToString();
                Session["RadNumTbFamilyExpenses"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : Convert.ToDouble(RadNumTbFamilyExpenses.Text).ToString();
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : Convert.ToDouble(RadNumOtherLoans.Text).ToString();
                Response.Redirect("rptProfferOnline");
            } 
            else
            if ((lst.GroupID == 128) && (rbtnBusiness.SelectedIndex == 1))
            {
                double MinRevenue = Convert.ToDouble(RadNumTbMinRevenue.Text);
                double MaxRevenue = Convert.ToDouble(RadNumTbMaxRevenue.Text);
                double Revenue = ((MinRevenue != 0) && (MaxRevenue != 0)) ? (MinRevenue + MaxRevenue) / 2 : MinRevenue + MaxRevenue;
                Session["CustomerID"] = hfCustomerID.Value;
                Session["CreditID"] = hfCreditID.Value;
                Session["RequestID"] = hfRequestID.Value;
                //Session["RadNumTbAverageMonthSalary"] = RadNumTbAverageMonthSalary.Text;
                Session["RadNumTbRevenue"] = (Revenue.ToString() == "") ? "0" : Revenue.ToString();
                Session["CountWorkDay"] = (ddlCountWorkDay.Text == "") ? "0" : ddlCountWorkDay.Text;
                Session["RadNumTbСostPrice"] = (RadNumTbСostPrice.Text == "") ? "0" : Convert.ToDouble(RadNumTbСostPrice.Text).ToString();
                Session["RadNumTbOverhead"] = (RadNumTbOverhead.Text == "") ? "0" : Convert.ToDouble(RadNumTbOverhead.Text).ToString();
                Session["RadNumTbFamilyExpenses"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : Convert.ToDouble(RadNumTbFamilyExpenses.Text).ToString();
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : Convert.ToDouble(RadNumOtherLoans.Text).ToString();
                
                Response.Redirect("rptProfferOnlineBusiness");
            }

            else

            if (rbtnBusiness.SelectedIndex == 0) //Зарплатный
            {
                Session["CustomerID"] = hfCustomerID.Value;
                Session["CreditID"] = hfCreditID.Value;
                Session["RequestID"] = hfRequestID.Value;
                Session["RadNumTbAverageMonthSalary"] = Convert.ToDouble(RadNumTbAverageMonthSalary.Text).ToString();
                //Session["RadNumTbRevenue"] = (Revenue.ToString() == "") ? "0" : Revenue.ToString();
                Session["CountWorkDay"] = (ddlCountWorkDay.Text == "") ? "0" : ddlCountWorkDay.Text;
                Session["RadNumTbСostPrice"] = (RadNumTbСostPrice.Text == "") ? "0" : Convert.ToDouble(RadNumTbСostPrice.Text).ToString();
                Session["RadNumTbOverhead"] = (RadNumTbOverhead.Text == "") ? "0" : Convert.ToDouble(RadNumTbOverhead.Text).ToString();
                Session["RadNumTbFamilyExpenses"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : Convert.ToDouble(RadNumTbFamilyExpenses.Text).ToString();
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : Convert.ToDouble(RadNumOtherLoans.Text).ToString();
                Response.Redirect("rptProffer");
            } else
            if (rbtnBusiness.SelectedIndex == 1) // Патент
            {
                double MinRevenue = Convert.ToDouble(RadNumTbMinRevenue.Text);
                double MaxRevenue = Convert.ToDouble(RadNumTbMaxRevenue.Text);
                double Revenue = ((MinRevenue != 0) && (MaxRevenue != 0)) ? (MinRevenue + MaxRevenue) / 2 : MinRevenue + MaxRevenue;
                Session["CustomerID"] = hfCustomerID.Value;
                Session["CreditID"] = hfCreditID.Value;
                Session["RequestID"] = hfRequestID.Value;
                //Session["RadNumTbAverageMonthSalary"] = RadNumTbAverageMonthSalary.Text;
                Session["RadNumTbRevenue"] = (Revenue.ToString() == "") ? "0" : Revenue.ToString();
                Session["CountWorkDay"] = (ddlCountWorkDay.Text == "") ? "0" : ddlCountWorkDay.Text;
                Session["RadNumTbСostPrice"] = (RadNumTbСostPrice.Text == "") ? "0" : Convert.ToDouble(RadNumTbСostPrice.Text).ToString();
                Session["RadNumTbOverhead"] = (RadNumTbOverhead.Text == "") ? "0" : Convert.ToDouble(RadNumTbOverhead.Text).ToString();
                Session["RadNumTbFamilyExpenses"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : Convert.ToDouble(RadNumTbFamilyExpenses.Text).ToString();
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : Convert.ToDouble(RadNumOtherLoans.Text).ToString();
                Response.Redirect("rptProfferBusiness");
            } else
            if (rbtnBusiness.SelectedIndex == 2) // Агро
            {
                double RevenueAgro = Convert.ToDouble(RadNumTbRevenueAgro.Text);
                double RevenueMilk = Convert.ToDouble(RadNumTbRevenueMilk.Text);
                double RevenueMilkProd = Convert.ToDouble(RadNumTbRevenueMilkProd.Text);
                double OverheadAgro = Convert.ToDouble(RadNumTbOverheadAgro.Text);
                double AddOverheadAgro = Convert.ToDouble(RadNumTbAddOverheadAgro.Text);

                Session["CustomerID"] = hfCustomerID.Value;
                Session["CreditID"] = hfCreditID.Value;
                Session["RequestID"] = hfRequestID.Value;
                Session["RevenueAgro"] = (RevenueAgro.ToString() == "") ? "0" : RevenueAgro.ToString();
                Session["RevenueMilk"] = (RevenueMilk.ToString() == "") ? "0" : RevenueMilk.ToString();
                Session["RevenueMilkProd"] = (RevenueMilkProd.ToString() == "") ? "0" : RevenueMilkProd.ToString();
                Session["OverheadAgro"] = (OverheadAgro.ToString() == "") ? "0" : OverheadAgro.ToString();
                Session["AddOverheadAgro"] = (AddOverheadAgro.ToString() == "") ? "0" : AddOverheadAgro.ToString();
                Session["RadNumTbFamilyExpenses"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : Convert.ToDouble(RadNumTbFamilyExpenses.Text).ToString();
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : Convert.ToDouble(RadNumOtherLoans.Text).ToString();
                Response.Redirect("rptProfferAgro");
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

  

        protected void btnPledgeAgreement_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
            Response.Redirect("rptPledgeAgreement");
        }

        protected void btnCustomerEdit_Click(object sender, EventArgs e)
        {
            if (hfCustomerID.Value != "noselect")
            {
                CustomerEdit(Convert.ToInt32(hfCustomerID.Value));
                btnCredit.Text = "Выбрать клиента";
                //hfCustomerID.Value = "edit";
                //customerFieldDisable();
                disableCustomerFields();
            }
        }

        private void CustomerEdit(int custID)
        {
            pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            clearEditControls();
            btnSaveCustomer.Enabled = true;

            btnSearchClient.Visible = false;
            btnNewCustomer.Visible = false;

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
            tbContactPhone.Text = cust.ContactPhone1;
            tbResidenceHouse.Text = cust.ResidenceHouse;
            tbResidenceFlat.Text = cust.ResidenceFlat;

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
            pnlNewRequest.Visible = false;
        }

        private void Confirm()
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            var lst5 = (from v in dbW.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Подтверждено";
                dbW.Requests.Context.SubmitChanges();
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
                var Customer = dbW.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
                ////SendMailTo(lst5.BranchID, "Подтверждено", Customer.Surname + " " + Customer.Surname + " " + Customer.Otchestvo, false, false, true); //экспертам
            }
        }

        private void Approved()
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
                int sysUserID = Convert.ToInt32(Session["UserID"].ToString());
                if (lst5.GroupID == 128)
                { sysUserID = dbRWZ.Users2s.Where(r => r.UserName == "SystemUser").FirstOrDefault().UserID; }
                else
                { sysUserID = Convert.ToInt32(Session["UserID"].ToString()); }
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = sysUserID,
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
                ////AgentView.SendMailTo2("Утверждено", true, true, false, connectionString, usrID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
            }
        }
        private void Signature()
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
                var Customer = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
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
            //dbdataDataContext db = new dbdataDataContext(connectionString);
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
                    Price = Convert.ToDecimal(row["price"].ToString()), //Convert.ToDecimal(RadNumTbProductPrice.Text)
                    Sim = row["sim"].ToString(),
                };

                ctl.ItemProductAddItem(newProduct);
            }
        }

        protected void lbSettings_Click(object sender, EventArgs e)
        {
            if (pnlSettings.Visible == true)
            { pnlSettings.Visible = false; lblMsgPassword.Visible = false; lblMsgPassword.Text = ""; }
            else
            { pnlSettings.Visible = true; lblMsgPassword.Text = ""; }
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
            pnlNewRequest.Visible = false;
        }


        public void CancelRequest()
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
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
            HistoriesStatuse hisStat = new HistoriesStatuse()
            {
                CreditID = Convert.ToInt32(hfCreditID.Value),
                StatusID = 4, //отмена
                StatusDate = dateNowServer,
                OperationDate = dateTimeNow,
                UserID = usrID
            };
            //ctx.ItemHistoriesStatuseAddItem(hisStat);
            /**/
        }

        public async System.Threading.Tasks.Task<string> SendStatusRequestOB(string CustomerID, string CreditID, string StatusID)
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
                    string url2 = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/Loans/PromoteLoanStatus?customerID=" + CustomerID + "&creditID=" + CreditID + "&statusID=" + StatusID;
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
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
                return "error";

            }
            finally
            {
                ////TextBox1.Text = TextBox1.Text + Response.ToString();

            }
        }


        protected async void btnCancelReq_Click(object sender, EventArgs e)
        {
            tbNote.Text = tbNoteCancelReq.Text;
            //********
            string strOB = "";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if ((lst.GroupID == 128) || (lst.GroupID == 134))
            {
                //********меняем статус в ОБ
                //await PostOnIssue(lst.CustomerID.ToString(), lst.CreditID.ToString());

                try
                {
                    strOB = await SendStatusRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString(), "4");
                    //strOB = await SendStatusRequestOB(lst.CustomerID.ToString(), "532646", "4");
                    //strOB = str;
                }
                catch (Exception ex)
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                    MsgBox(strOB + " " + ex.ToString(), this.Page, this);
                }
                finally
                {

                }

                //********отправляем статус в DCB360
                string str = "";
                //if (lst.GroupID == 134)
                //{
                //    respDCB360 resp = await SendStatusDCB360(lst.CreditID.ToString(), "REFUSED_BY_CLIENT");
                //    //respDCB360 resp = await SendStatusDCB360(532646.ToString(), "REFUSED_BY_CLIENT");

                //    if ((strOB == "200") && ((resp.statusCode == 200) || (resp.statusCode == 400)))
                //    {
                //        CancelRequest();
                //        //AddRowToJournalDCBCola(lst, "Отменено");
                //    }
                //    else
                //    {
                //        if (resp.statusCode == 404) MsgBox("Ответ от DCB360: 404 Credit id не найден", this.Page, this);
                //        if (strOB != "200") MsgBox(strOB + " Ошибка при отправке статуса на сервер ОБ", this.Page, this);
                //    }

                //}
                /*****************/
                if (lst.GroupID == 128)
                {
                    //str = await SendStatusDCB360(lst.CreditID.ToString(), "REFUSED_BY_CLIENT");

                    if (strOB == "200")
                    {
                        CancelRequest();
                        AddRowToJournalDCBKG(lst, "Отменено");
                    }
                    else
                    {
                        //if (str != "200") MsgBox(strOB + " Ошибка при отправке статуса на сервер DCB360", this.Page, this);
                        if (strOB != "200") MsgBox(strOB + " Ошибка при отправке статуса на сервер ОБ", this.Page, this);
                    }

                }


            }
            else
            {
                try
                {
                    strOB = await SendStatusRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString(), "4");
                    //strOB = str;
                }
                catch (Exception ex)
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(strOB + "" + ex.ToString());
                    MsgBox(strOB + " ", this.Page, this);
                }
                finally
                {

                }
                if (strOB == "200")
                {
                    CancelRequest();
                }
                else
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show("");
                    MsgBox(strOB + " ", this.Page, this);
                }


            }
            pnlNewRequest.Visible = false;
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
            pnlNewRequest.Visible = false;
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
            refreshProducts(0); firstAmount();
        }

        protected void chbEmployer_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEmployer.Checked) { pnlEmployment.Visible = false; }
            else { pnlEmployment.Visible = true; }
        }





        public static void SendMailTo2(string strsubj, bool boolagent, bool booladmin, bool boolekspert, string connectionString, int usrID, int reqID, string Fullname, string UserName, string FIO)
        {
            /*****************************/
            string stremailto = "";
            string emailfrom = "";
            string templateAdm = "";
            string templateUsr = "";
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
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
                    //SendMail2(templateAdm, mailto, emailfrom, "", strsubj);
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
                            int branchIDeks = dbR.Offices.Where(r => r.ID == officeIDeks).FirstOrDefault().BranchID;
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
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                //MsgBox(ex.ToString(), this.Page, this);
            }
            /*****************************/
        }



        public void CancelRequestExp()
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
        }


        protected async void btnCancelReqExp_Click(object sender, EventArgs e)
        {
            tbNote.Text = tbNoteCancelReqExp.Text;
            string strOB = "";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if ((lst.GroupID == 128) || (lst.GroupID == 134))
            {
                //********меняем статус в ОБ
                //await PostOnIssue(lst.CustomerID.ToString(), lst.CreditID.ToString());

                try
                {
                    strOB = await SendStatusRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString(), "4");
                    //strOB = str;
                }
                catch (Exception ex)
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                    MsgBox(strOB + " ", this.Page, this);
                }
                finally
                {

                }


                //********отправляем статус в DCB360

                //if (lst.GroupID == 134)
                //{
                //    respDCB360 resp = await SendStatusDCB360(lst.CreditID.ToString(), "DECLINED");
                //    if ((strOB == "200") && ((resp.statusCode == 200) || (resp.statusCode == 400)))
                //    {
                //        CancelRequestExp();
                //        //AddRowToJournalDCBCola(lst, "Отменено");
                //    }
                //    else
                //    {
                //        if (resp.statusCode == 404) MsgBox("Ответ от DCB360: 404 Credit id не найден", this.Page, this);
                //        if (strOB != "200") MsgBox(strOB + " Ошибка при отправке статуса на сервер ОБ", this.Page, this);
                //    }

                //}
                /*****************/
                //********отправляем статус в DCBKG

                if (lst.GroupID == 128)
                {
                    //string str = await SendStatusDCBKG(lst.CreditID.ToString(), "REFUSED_BY_CLIENT");
                    if (strOB == "200")
                    {
                        CancelRequestExp();
                        AddRowToJournalDCBKG(lst, "Отменено");
                    }
                    else
                    {
                        //if (str != "200") MsgBox(strOB + " Ошибка при отправке статуса на сервер DCB360", this.Page, this);
                        if (strOB != "200") MsgBox(strOB + " Ошибка при отправке статуса на сервер ОБ", this.Page, this);
                    }

                }
                /*****************/





                //if (strOB == "200")
                //{
                //    AddRowToJournal(lst, "Отказано");
                //    CancelRequestExp();
                //}
                //else
                //{
                //    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статусов в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //    //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статусов в Нуртелеком");
                //    MsgBox("Ошибка при отправке статусов в Нуртелеком", this.Page, this);
                //}
            }
            else
            {

                try
                {
                    strOB = await SendStatusRequestOB(lst.CustomerID.ToString(), lst.CreditID.ToString(), "4");
                    //strOB = str;
                }
                catch (Exception ex)
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                    MsgBox(strOB + " " + ex.ToString(), this.Page, this);
                }
                finally
                {

                }
                if (strOB == "200")
                {
                    CancelRequestExp();
                    GeneralController gnrl = new GeneralController();
                    gnrl.AddRowToJournalScoring(lst, "Отказано", Convert.ToInt32(hfUserID.Value));
                }
                else
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show("");
                    MsgBox(strOB + " ", this.Page, this);
                }
            }
            pnlNewRequest.Visible = false;
        }




        protected void gvRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRequests.PageIndex = e.NewPageIndex;
            gvRequests.DataBind();

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
            Session["o0"] = chkbxOffice.Items[0].Selected;
            Session["o1"] = chkbxOffice.Items[1].Selected;
            Session["o2"] = chkbxOffice.Items[2].Selected;
            Session["o3"] = chkbxOffice.Items[3].Selected;
            Session["o4"] = chkbxOffice.Items[4].Selected;
            Session["o5"] = chkbxOffice.Items[5].Selected;
            Session["o6"] = chkbxOffice.Items[6].Selected;
            Session["o7"] = chkbxOffice.Items[7].Selected;
            Session["o8"] = chkbxOffice.Items[8].Selected;
            Session["o9"] = chkbxOffice.Items[9].Selected;
            Response.Redirect("rptForPeriod");
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
            pnlNewRequest.Visible = false;
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
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
                return "error";

            }
            finally
            {
                ////TextBox1.Text = TextBox1.Text + Response.ToString();

            }
        }


        protected async void btnApproved_Click(object sender, EventArgs e)
        {
            //lblError.Text = "утв";

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if ((lst.RequestStatus == "Новая заявка") || (lst.RequestStatus == "Сделано") || (lst.RequestStatus == "Исправить") || (lst.RequestStatus == "Исправлено") || (lst.RequestStatus == "Подтверждено"))
            {
                

                if (Page.IsValid)
                {
                    //DateTime DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4));
                    //string DateOfBirth = tbDateOfBirth.Text.Substring(0, 2) + (tbDateOfBirth.Text.Substring(3, 2) + tbDateOfBirth.Text.Substring(6, 4));
                    //string DateOfBirthINN = (tbIdentificationNumber.Text.Substring(1, 8)); bool f = true;
                    //if (DocumentValidTill.Date < System.DateTime.Now.Date.AddDays(1)) { MsgBox("Паспорт проссрочен", this.Page, this); f = false; }
                    //if (DateOfBirth != DateOfBirthINN) { MsgBox("Неправильно введены паспортные данные", this.Page, this); f = false; }
                
                    bool f = true;
                    if (f)
                    {
                        if ((lst.GroupID == 128) || (lst.GroupID == 134))
                        {

                            if (((!string.IsNullOrEmpty(lst.MaxRevenue.ToString())) && (lst.GroupID == 134)) || (lst.GroupID == 128))
                            {
                
                                int age = GetCustomerAge(Convert.ToInt32(hfCustomerID.Value.ToString()));
                                if ((age > 22) && (age < 70))
                                {
                                    
                                }
                                else
                                {
                                    
                                    f = false;
                                    //customerID = -3;
                                    //MsgBox("Выдача кредита не возможна, возрастное ограничение 23-65, клиенту " + age.ToString() + " лет", this.Page, this);
 
                                        //lblError.Text = "Выдача кредита не возможна, возрастное ограничение 23-65";
                                    
                                }

                                if (f)
                                {
                                    //bool fs = scor(); //fs = true;
                                    int fs = issuanceOfCredit();
                                    if (fs == 1)
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
                                            
                                                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                                                //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                                                //lblError.Text = strOB + " " + ex.ToString();
                                                //lblError.Visible = true;
                                                MsgBox(strOB + " " + ex.ToString(), this.Page, this);

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
                                                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                                                //System.Windows.Forms.MessageBox.Show(strOB);
                                                MsgBox(strOB, this.Page, this);
                                                //lblError.Text = strOB;
                                                //lblError.Visible = true;
                                            }
                                        }
                                        //********меняем статус в ОБ Утверждено*****End//

                                        //********отправляем статус в нуртелеком Begin
                                        //string strNur = await SendStatusNur(lst.CreditID.ToString(), "APPROVED");
                                        //if (strNur == "200")
                                        //{
                                        //    //ApprovedRequest(); //Утверждаем если даже в ОБ не отвечает
                                        //}
                                        //else
                                        //{
                                        //    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                                        //}


                                        //************отправляем статус в нуртелеком End
                                        //if (lst.GroupID == 134)
                                        //{
                                        //    respDCB360 resp = null;

                                        //    //********отправляем статус на сервер DCB Begin
                                        //    try
                                        //    {
                                              
                                        //        resp = await SendStatusDCB360(lst.CreditID.ToString(), "APPROVED");
                                              
                                        //    }
                                        //    catch (Exception ex)
                                        //    {
                                        //        //lblError.Text = ex.ToString();
                                        //        //lblError.Visible = true;

                                        //    }
                                        //    //respDCB360 resp = await SendStatusDCB360(532646.ToString(), "APPROVED");
                                        //    //lblError.Text = strOB + "-" + resp.error;

                                        //    if ((strOB == "200") && ((resp.statusCode == 200) || (resp.statusCode == 400)))
                                        //    {
                                        //        //VerificationRequest(); 
                                        //        //sms begin
                                        //        //string str = await SendSMSWithOurRoute("", "");
                                        //        //if (str == "0: Accepted for delivery")
                                        //        //{
                                        //        //    //Console.WriteLine("send");
                                        //        //}
                                        //        //sms end
                                        //        Approved();
                                        //        //AddRowToJournalDCBCola(lst, "Утверждено");
                                        //    }
                                        //    else
                                        //    {
                                        //        if (strOB != "200")
                                        //        {
                                        //            MsgBox(strOB + "Ошибка при отправке статуса APPROVED на сервер ОБ", this.Page, this);
                                        //            //lblError.Text = "Ошибка при отправке статуса APPROVED на сервер ОБ" + resp.error;
                                        //            //lblError.Visible = true;
                                        //        }
                                        //        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED на сервер DCB360", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                                        //        if (resp.statusCode == 404)
                                        //        {
                                        //            MsgBox(resp.statusCode + " " + resp.message, this.Page, this);
                                        //            //System.Windows.Forms.MessageBox.Show("Ответ от DCB360: 404 Credit id не найден");
                                        //            //lblError.Text = "Ответ от DCB360: 404 Credit id не найден-" + resp.error;
                                        //            //lblError.Visible = true;
                                        //        }
                                        //        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED на сервер ОБ", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);

                                        //    }
                                        //    //************отправляем статус на сервер DCB End

                                        //}
                                        if (lst.GroupID == 128)
                                        {

                                            if ((strOB == "200")) 
                                            { 
                                                VerificationRequest(); 
                                                Approved(); 
                                                AddRowToJournalDCBKG(lst, "Утверждено"); 
                                            }
                                            //if ((strOB == "200")) { VerificationRequest(); ApprovedRequest(); AddRowToJournal(lst, "Утверждено"); }
                                        }

                                    }
                                    else
                                    {
                                        if (lst.GroupID == 128) { 
                                            //VerificationRequest(); 
                                            btnCancelReqExp_Click(new object(), new EventArgs()); 
                                            AddRowToJournalDCBKG(lst, "Отказано"); 
                                        }
                                        //lblError.Text = "Скоринг не проходит";

                                        if (lst.GroupID == 134)
                                        { //AddRowToJournalDCBCola(lst, "Отказано");
                                            MsgBox("Скоринг не проходит", this.Page, this);
                                            //lblError.Text = "Скоринг не проходит";
                                            //lblError.Visible = true;
                                        }

                                    }
                                }
                                else
                                {
                                    MsgBox("Выдача кредита не возможна, возрастное ограничение 23-65, клиенту " + age.ToString() + " лет", this.Page, this);
                                    //lblError.Text = "Выдача кредита не возможна, возрастное ограничение 23 - 65, клиенту " + age.ToString() + " лет";
                                    //lblError.Visible = true;
                                }
                            }
                            else MsgBox("Необходимо сохранить заявку для открытия Предложения", this.Page, this);
                        }
                        else
                        {
                            //********меняем статус в скоринге если не нано
                            Approved();
                            GeneralController gnrl = new GeneralController();
                            gnrl.AddRowToJournalScoring(lst, "Утверждено", Convert.ToInt32(hfUserID.Value));
                        }
                    }
                    else
                    {
                        //lblError.Text = "notvalid";
                    }
                }
                else
                {
                    //lblError.Text = "notvalid";
                }
            }

            //Approved();
            pnlNewRequest.Visible = false;
        }

        public class respDCB360
        {
            public int statusCode;
            public string message;
            public string error;
        }

        private void Somewhere()
        {
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(AlwaysGoodCertificate);
        }

        private static bool AlwaysGoodCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }

        //public async System.Threading.Tasks.Task<respDCB360> SendStatusDCB360(string CreditID, string Status)
        //{
        //    //httpWebRequest.Headers.Add("aftership-api-key:********fdbfd93980b8c5***");
        //    respDCB360 resp = new respDCB360();
          
            

          
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            //client.BaseAddress = new Uri("https://10.120.16.85/");
        //            //client.DefaultRequestHeaders.Accept.Clear();
        //            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("API-SECURE-KEY", "$2y$12$nlegbzkLcsCwg/6qwh0SQuGpcM00mZAtm0gDhNhnvYL.BODZq/qfy");
        //            //client.DefaultRequestHeaders.Add("API-SECURE-KEY", "$2y$12$nlegbzkLcsCwg/6qwh0SQuGpcM00mZAtm0gDhNhnvYL.BODZq/qfy");
        //            //client.DefaultRequestHeaders.Add("API-SECURE-KEY", "$2y$12$nlegbzkLcsCwg/6qwh0SQuGpcM00mZAtm0gDhNhnvYL.BODZq/qfy");
        //            //client.DefaultRequestHeaders.Add("API-SECURE-KEY", connectionStringDCB360Key);
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            string json = "{\"message\": \"string\", \"creditId\": \"" +
        //                CreditID +
        //                "\", \"status\": \"" +
        //                Status + "\"" +
        //                "}";


        //            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
        //            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //            ServicePointManager.Expect100Continue = true;
        //            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(AlwaysGoodCertificate);


        //            //ServicePointManager.Expect100Continue = true;
        //            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        //            //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, error) => { return true; };



        //            //var response = await client.PutAsync("https://proxy.o.kg/api/processing/wa/creditLines/" + CreditID, new StringContent(json, Encoding.UTF8, "application/json"));
        //            //ServicePointManager.Expect100Continue = true;


        //            //MsgBox(connectionStringDCB360, this.Page, this);
        //            try
        //            {
        //                var response2 = await client.PutAsync(connectionStringDCB360, new StringContent(json, Encoding.UTF8, "application/json"));
        //            }
        //            catch (Exception ex)
        //            {
        //                MsgBox("try " + ex.ToString(), this.Page, this);
        //            }
        //            //MsgBox("try", this.Page, this);

        //            var response = await client.PutAsync(connectionStringDCB360, new StringContent(json, Encoding.UTF8, "application/json"));

        //            var result = await response.Content.ReadAsStringAsync();
        //            //TextBox1.Text = result.ToString();

        //            //dynamic data = JObject.Parse(result);
        //            //string statusCode = data.statusCode;

        //            //string url = jsonData["order"]["url"].ToString();
        //            //var data = (JsonObject)JsonConvert.DeserializeObject(result);
        //            //var statusCode = data["statusCode"];

        //            //string timeZone = data["Atlantic/Canary"].Value<string>();

        //            //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);

                   

        //            if (response.StatusCode == HttpStatusCode.OK)
        //            {
        //                //var list = await Task.Run(() => JsonConvert.DeserializeObject<List<MyObject>>(response.Content));
        //                //var data = await Task.Run(() => JsonConvert.DeserializeObject(result));
        //                //var data = JsonConvert.DeserializeObject(result);
        //                //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
        //                //var statusCode = data.ToString();
        //                //TextBox1.Text = statusCode.ToString();
        //                //Results data = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject<Results>(result));
        //                result = "200";
        //                resp.statusCode = 200;
        //            }
        //            else
        //            {
        //                resp = JsonConvert.DeserializeObject<respDCB360>(result);
        //                result = resp.statusCode.ToString();
        //            }
        //            //var statusCode = data["statusCode"];
        //            return resp;


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //TextBox1.Text = TextBox1.Text + ex.Message;
        //        resp.statusCode = 0;
        //        resp.error = ex.ToString();
        //        return resp;
        //    }
        //    finally
        //    {
        //        //TextBox1.Text = TextBox1.Text + Response.ToString();
        //        //return "";
        //    }
        //}


        private void AddRowToJournalDCBCola(Request req, string status)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == req.RequestID) select v).ToList().FirstOrDefault();
            var usr = dbRWZ.Users2s.Where(z => z.UserID == Convert.ToInt32(hfUserID.Value)).FirstOrDefault();
            int N1 = 0, N2 = 0;
            if (lst != null)
            {
                var dateNow = DateTime.Now.Date;
                //var yestedey = DateTime.Now.AddDays(-1).Date;


                var jourAll = (from v in dbRWZ.JournalDCBColas orderby v.DateVerif select v).ToList();
                var jourNow = (from v in dbRWZ.JournalDCBColas where (v.DateVerif == dateNow) orderby v.DateVerif select v).ToList();

                if (jourAll.Count != 0)
                {

                    //var jourNow = (from v in dbRWZ.JournalNanos where (v.DateVerif == dateNow) select v).ToList().FirstOrDefault();

                    if (jourNow.Count == 0)
                    {

                        N1 = Convert.ToInt32(jourAll.Last().Nomer1) + 1;
                        N2 = 1;
                    }
                    else
                    {
                        N1 = Convert.ToInt32(jourNow.Last().Nomer1);
                        N2 = Convert.ToInt32(jourNow.Last().Nomer2) + 1;
                    }

                }
                else
                {
                    N1 = 1;
                    N2 = 1;
                }

                JournalDCBCola jourCurr = (from v in dbRWZ.JournalDCBColas where v.RequestID == req.RequestID select v).FirstOrDefault();
                if (jourCurr != null)
                {
                    //JournalNano Item = new JournalNano();
                    ItemController ctlItem = new ItemController();
                    //Item = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    //Item = jourCurr.FirstOrDefault();
                    jourCurr.Status = status;
                    ctlItem.ItemJournalDCBColaUpd(jourCurr);
                }
                else
                {
                    if (lst.RequestStatus == "Утверждено")
                    {
                        JournalDCBCola newItem = new JournalDCBCola()
                        {
                            RequestID = req.RequestID,
                            Nomer1 = N1,
                            Nomer2 = N2,
                            FIO = req.Surname + " " + req.CustomerName + " " + req.Otchestvo,
                            RequesSum = req.RequestSumm,
                            RequestPeriod = req.RequestPeriod,
                            BranchID = req.BranchID.ToString(),
                            Verificator = usr.Fullname,
                            Status = status,
                            DateVerif = Convert.ToDateTime(DateTime.Now).Date

                            //AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                            //CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                            //CustomerID = Convert.ToInt32(hfCustomerID.Value),
                            //StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                            //Status = "Исправить",
                            //note = tbNote.Text,
                            //RequestID = Convert.ToInt32(hfRequestID.Value) // requestID
                        };
                        ItemController itm = new ItemController();
                        itm.ItemJournalDCBColaAdd(newItem);
                    }
                }
            }

        }



        private void AddRowToJournalDCBKG(Request req, string status)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == req.RequestID) select v).ToList().FirstOrDefault();
            var usr = dbRWZ.Users2s.Where(z => z.UserID == Convert.ToInt32(hfUserID.Value)).FirstOrDefault();
            int N1 = 0, N2 = 0;
            if (lst != null)
            {
                var dateNow = DateTime.Now.Date;
                //var yestedey = DateTime.Now.AddDays(-1).Date;


                var jourAll = (from v in dbRWZ.JournalCardCreditDCBKG orderby v.DateVerif select v).ToList();
                var jourNow = (from v in dbRWZ.JournalCardCreditDCBKG where (v.DateVerif == dateNow) orderby v.DateVerif select v).ToList();

                if (jourAll.Count != 0)
                {

                    //var jourNow = (from v in dbRWZ.JournalNanos where (v.DateVerif == dateNow) select v).ToList().FirstOrDefault();

                    if (jourNow.Count == 0)
                    {

                        N1 = Convert.ToInt32(jourAll.Last().Nomer1) + 1;
                        N2 = 1;
                    }
                    else
                    {
                        N1 = Convert.ToInt32(jourNow.Last().Nomer1);
                        N2 = Convert.ToInt32(jourNow.Last().Nomer2) + 1;
                    }

                }
                else
                {
                    N1 = 1;
                    N2 = 1;
                }

                JournalCardCreditDCBKG jourCurr = (from v in dbRWZ.JournalCardCreditDCBKG where v.RequestID == req.RequestID select v).FirstOrDefault();
                if (jourCurr != null)
                {
                    //JournalNano Item = new JournalNano();
                    ItemController ctlItem = new ItemController();
                    //Item = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    //Item = jourCurr.FirstOrDefault();
                    jourCurr.Status = status;
                    ctlItem.ItemJournalDCBKGUpd(jourCurr);
                }
                else
                {
                    if (lst.RequestStatus == "Утверждено")
                    {
                        JournalCardCreditDCBKG newItem = new JournalCardCreditDCBKG()
                        {
                            RequestID = req.RequestID,
                            Nomer1 = N1,
                            Nomer2 = N2,
                            FIO = req.Surname + " " + req.CustomerName + " " + req.Otchestvo,
                            RequesSum = req.RequestSumm,
                            RequestPeriod = req.RequestPeriod,
                            BranchID = req.BranchID.ToString(),
                            Verificator = usr.Fullname,
                            Status = status,
                            DateVerif = Convert.ToDateTime(DateTime.Now).Date

                            //AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                            //CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                            //CustomerID = Convert.ToInt32(hfCustomerID.Value),
                            //StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                            //Status = "Исправить",
                            //note = tbNote.Text,
                            //RequestID = Convert.ToInt32(hfRequestID.Value) // requestID
                        };
                        ItemController itm = new ItemController();
                        itm.ItemJournalDCBKGAdd(newItem);
                    }
                }
            }

        }



        private void VerificationRequest()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst5 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Верификация";
                dbRWZ.Requests.Context.SubmitChanges();
                System.Threading.Thread.Sleep(1000);
                refreshGrid();
                string hexGreen = "#7cfa84";
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "Верификация";
                /*RequestHistory*/
                DateTime dateTimeNow = dateNow;
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "Верификация",
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
                ////AgentView.SendMailTo2("Утверждено", true, true, false, connectionString, usrID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
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
                        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, result3, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        //System.Windows.Forms.MessageBox.Show(result3);
                        MsgBox(result3, this.Page, this);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
                return "error";

            }
            finally
            {
                ////TextBox1.Text = TextBox1.Text + Response.ToString();

            }
        }


        public bool scor()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var requests = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList().FirstOrDefault();
            double AverageMonthSalary, OtherLoans, chp, cho, y1, y2;
            //if (requests.NanoScorePoints == "Скорбалл:A")
            //    AverageMonthSalary = 20000;
            //if (requests.NanoScorePoints == "Скорбалл:B")
            //    AverageMonthSalary = 15000;
            //if (requests.NanoScorePoints == "Скорбалл:C")
            //    AverageMonthSalary = 10000;
            //if (requests.NanoScorePoints == "Скорбалл:А")
            //    AverageMonthSalary = 20000;
            //if (requests.NanoScorePoints == "Скорбалл:В")
            //    AverageMonthSalary = 15000;
            //if (requests.NanoScorePoints == "Скорбалл:Б")
            //    AverageMonthSalary = 15000;
            //if (requests.NanoScorePoints == "Скорбалл:С")
            //    AverageMonthSalary = 10000;
            double s = Convert.ToDouble(requests.RequestSumm);
            double n = Convert.ToDouble(requests.RequestPeriod);
            double stavka = Convert.ToDouble(requests.RequestRate);
            double i, k = 0;
            int y22 = 40;

            i = (stavka != 0) ? stavka / 12 / 100 : 0;
            if ((stavka == 0)) k = s / n;
            if (stavka != 0) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            //k = 5500;
            bool f = false, b1, b2;

            {
                var zp = Convert.ToDouble(requests.AverageMonthSalary);
                if ((zp > 50000) || (zp == 50000)) y22 = 50;
                if (zp < 50000) y22 = 40;
                OtherLoans = Convert.ToDouble(requests.OtherLoans);//Convert.ToDouble(RadNumOtherLoans);
                chp = (zp + Convert.ToDouble(requests.AdditionalIncome)) - 7000;
                cho = chp - k;
                cho = cho - OtherLoans;
                y1 = 100 * cho / chp;
                if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                y2 = 100 * (k + OtherLoans) / (zp + Convert.ToDouble(requests.AdditionalIncome));

                if ((y1 >= 20) && (y2 < y22)) f = true;
                if (y1 >= 20) b1 = true; //lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
                else b1 = false; //lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
                if (y2 < y22) b2 = true; // lblIssuanceOfCredit2.Text = "да, выдача кредита возможно";
                else b2 = false; // lblIssuanceOfCredit2.Text = "нет, выдача кредита невозможно";
            }
            if (b1 && b2) return true;
            else return false;
        }


        protected async void btnSignature_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.RequestStatus == "Утверждено")
            {
                if (lst.GroupID == 134)
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
                            //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                            MsgBox(strOB + " " + ex.ToString(), this.Page, this);
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
                            // DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса На выдаче в АБС", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса 'На выдаче' на сервер АБС");
                            MsgBox("Ошибка при отправке статуса 'На выдаче' на сервер АБС", this.Page, this);
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
                        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса APPROVED на сервер DCB360");
                    }
                    //************отправляем статус в нуртелеком End
                    if ((strOB == "200") && (strNur == "200"))
                    {
                        Signature();
                        AtIssueRequest();
                    }

                }
                else
                {
                    //********меняем статус в скоринге если не нано
                    Signature();

                }
            }
            pnlNewRequest.Visible = false;
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
            Response.Redirect("rptForPeriodWithHistory");
        }

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            refreshProducts(1); firstAmount();
        }

        protected void gvProducts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            refreshProducts(1); firstAmount();
        }

        protected void gvProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label ProductID = (Label)(gvProducts.Rows[e.RowIndex].FindControl("lblProductID"));
            TextBox ProductMark = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtProductMark"));
            TextBox ProductSerial = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtProductSerial"));
            TextBox ProductImei = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtProductImei"));
            TextBox Price = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtPrice"));
            // DropDownList ddlTarifName = ((DropDownList)gvProducts.Rows[e.RowIndex].FindControl("ddlTarifName"));
            TextBox Note = ((TextBox)gvProducts.Rows[e.RowIndex].FindControl("txtNote"));
            //  decimal pr = 0; // tarif(Convert.ToInt32(ddlRequestPeriod.SelectedValue), ddlTarifName.SelectedItem.Text);
            RequestsProduct edititem = new RequestsProduct();
            ItemController ctl = new ItemController();
            edititem = ctl.GetRequestsProductById(Convert.ToInt32(ProductID.Text));
            edititem.ProductMark = ProductMark.Text;
            edititem.ProductSerial = ProductSerial.Text;
            edititem.ProductImei = ProductImei.Text;
            edititem.Price = Convert.ToDecimal(Price.Text); //Convert.ToDecimal(RadNumTbProductPrice.Text);
                                                            //edititem.TarifName = ddlTarifName.SelectedItem.Text;
                                                            //  edititem.PriceTarif = pr;
            edititem.PriceWithTarif = Convert.ToDecimal(Price.Text);  // + pr;
            edititem.Note = Note.Text;
            ctl.RequestsProductsUpd(edititem);
            System.Threading.Thread.Sleep(1000);
            //refreshProducts(1);
            btnAddProductClick = 1;
            gvProducts.EditIndex = -1;
            refreshProducts(1); firstAmount();
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
            Response.Redirect("rptForPeriodWithProducts");
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

        protected void ddlRequestRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            databindDDLRatePeriod(2);
            refreshProducts(1);
            firstAmount();
        }


        protected void ddlRequestRateSelectedIndexChanged()
        {
            databindDDLRatePeriod(2);
            refreshProducts(1);
            //firstAmount();
        }

        protected void btnGuarantSearch_Click(object sender, EventArgs e)
        {
            pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            hfCustomerID.Value = "noselect";
            btnCredit.Text = "Выбрать поручителя";
            hfChooseClient.Value = "Выбрать поручителя";
            tbGuarantorSurname.Text = "";
            tbGuarantorName.Text = "";
            tbGuarantorOtchestvo.Text = "";
            tbGuarantorINN.Text = "";
            clearEditControls();
            lblMessageClient.Text = "";
            tbContactPhone.Text = "";
            tbSearchINN.Text = "";
            btnSaveCustomer.Enabled = true;
            tbSerialN.Text = "";
            clear_positions();
            //customerFieldEnable();
            disableCustomerFields();
            btnSearchClient.Visible = true;
            btnNewCustomer.Visible = true;

        }

        protected void TbAmountOfDownPayment_TextChanged(object sender, EventArgs e)
        {
            firstAmountforhfAmountOfDownPayment();
            double amount10 = (TbAmountOfDownPayment.Text!="") ? Convert.ToDouble(TbAmountOfDownPayment.Text): 0;
            double hfamount10 = Convert.ToDouble(hfAmountOfDownPayment.Value);
            if (amount10 < hfamount10)
            {
                RadNumTbRequestSumm.Text = "";
                //lblAmountOfDownPayment.Text = "Сумма перв.взноса не может быть меньше суммы тарифов";
            }
            else
            {
                RadNumTbRequestSumm.Text = (Convert.ToDouble(RadNumTbTotalPrice.Text) - amount10).ToString();
                //    lblAmountOfDownPayment.Text = "";
            }
        }

        public void firstAmountforhfAmountOfDownPayment()
        {
            double amount1 = 0, amount10, request;
            double stavka = Convert.ToDouble(ddlRequestRate.SelectedItem.Value);
            double price = Convert.ToDouble(RadNumTbTotalPrice.Text);
            //double sumtarif = Convert.ToDouble(hfSumTarif.Value);
            //if (stavka == 0) amount1 = 0;

            //if (stavka == 29)
            //{
            //    amount1 = 10;
            //}
            //if ((stavka == 25) || (stavka == 30))
            //{
            //    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amount1 = 0;
            //    else amount1 = 10;
            //}
            amount1 = getamount1();
            amount10 = amount1 * price / 100;
            hfAmountOfDownPayment.Value = amount10.ToString();
        }

        public void firstAmount()
        {
            try
            {
                double amount1 = 10, amount10, request;
                double stavka = Convert.ToDouble(ddlRequestRate.SelectedItem.Value);
                double price = Convert.ToDouble(RadNumTbTotalPrice.Text);
                //double sumtarif = Convert.ToDouble(hfSumTarif.Value);


                //if (stavka == 0) amount1 = 0;

                //if (stavka == 29)
                //{
                //    amount1 = 10;
                //}
                //if ((stavka == 25) || (stavka == 30))
                //{
                //    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amount1 = 0;
                //    else amount1 = 10;
                //}

                //if (stavka == 32)
                //{
                //    amount1 = 10;
                //}



                amount1 = getamount1();

                amount10 = amount1 * price / 100;
                //if ((stavka == 25) || (stavka == 30))
                //    if (amount10 < sumtarif) { amount10 = sumtarif; }

                TbAmountOfDownPayment.Text = amount10.ToString();
                hfAmountOfDownPayment.Value = amount10.ToString();
                double amount = Convert.ToDouble(TbAmountOfDownPayment.Text);
                if (stavka == 0)
                {
                    request = price - amount;
                }
                else
                {
                    request = price - amount;
                }
                RadNumTbRequestSumm.Text = request.ToString();
            }
            catch (Exception ex)
            { }

        }


        public int getGroupIDfromUserID(int userID)
        {
            int? groupID;
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            if (hfRequestID.Value == "")
            {
                //usrID = Convert.ToInt32(Session["UserID"].ToString());
                int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == userID).FirstOrDefault().RoleID;
                int officeID = dbRWZ.Users2s.Where(r => r.UserID == userID).FirstOrDefault().OfficeID;
                groupID = dbRWZ.Users2s.Where(r => r.UserID == userID).FirstOrDefault().GroupID;
                //int? OrgID = db.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
            }
            else
            {
                //usrID = Convert.ToInt32(Session["UserID"].ToString());
                int reqID = Convert.ToInt32(hfRequestID.Value); //Convert.ToInt32(Session["RequestID"].ToString());
                if (reqID > 0)
                    groupID = dbRWZ.Requests.Where(r => r.RequestID == reqID).FirstOrDefault().GroupID;
                else groupID = dbRWZ.Users2s.Where(r => r.UserID == userID).FirstOrDefault().GroupID; //groupID = 30;
            }
            return Convert.ToInt32(groupID);
        }



            public int getamount1()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int? groupID;

            groupID = getGroupIDfromUserID(Convert.ToInt32(Session["UserID"].ToString()));
            //int usrID = Convert.ToInt32(Session["UserID"].ToString());
            //int? groupID = db.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;

            //groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
            int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;

            int amount1 = 10;


            if (groupID == 128) //DCB
            {
                amount1 = 0;
            }

            if (groupID == 30) //Техно
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    amount1 = 0;
                }

                if ((ddlRequestRate.Text == "29,90") || (ddlRequestRate.Text == "25,00"))
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,00")
                {
                    amount1 = 10;
                }
            }


            if (groupID == 50) // Планета
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 150000) amount1 = 0;
                    else amount1 = 10;
                }

                if ((ddlRequestRate.Text == "29,90") || (ddlRequestRate.Text == "25,00"))
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 150000) amount1 = 0;
                    else amount1 = 10;
                }


                if (ddlRequestRate.Text == "29,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 150000) amount1 = 0;
                    else amount1 = 10;
                }
            }


            if (groupID == 51) //Сулпак

            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 150000) amount1 = 0;
                    else amount1 = 10;
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 150000) amount1 = 0;
                    else amount1 = 10;
                }
                //if (ddlRequestRate.Text == "27,00")
                //{
                //    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 150000) amount1 = 0;
                //    else amount1 = 10;
                //}
            }

            if (groupID == 62) //Беко
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

            }

            if (groupID == 66) //Гергерт
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
            }


            if (groupID == 10) //Светофор
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
            }


            

            if (groupID == 75) // IDEA
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

            }

            if (orgID == 16) // Евролюкс
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

            }

            if (groupID == 69) // ИП Чокморов Логин кг
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

            }


            if (groupID == 127) // Два Прораба
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

            }


            if (orgID == 18) // ОсОО КРИК Ирбис
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

            }



            if (groupID == 72) // ИП Токтоксунова 
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

            }

            if (orgID == 20) //Аквадом
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
            }

            if (orgID == 21) //Смарт тех
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
            }

            if (orgID == 22) //ИП Байысбеков Доолот Байысбекович
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
            }

            //if (orgID == 23) //ИП АСКАРОВ АДЫЛ ЖЕНИШОВИЧ
            //{
            //    if (ddlRequestRate.Text == "0.00")
            //    {
            //        if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amount1 = 0;
            //        else amount1 = 10;
            //    }

            //    if (ddlRequestRate.Text == "29.90")
            //    {
            //        if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amount1 = 0;
            //        else amount1 = 10;
            //    }
            //}

            if (orgID == 25) //ИП Мечта
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
            }

            if (orgID == 14) //ИП Токтосунова Самара Зайырбековна - Берекет
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
            }

            if (groupID == 97) //ИП Мищенко Венера Рамильевна - Берекет
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 100000) amount1 = 0;
                    else amount1 = 10;
                }
            }



            if (orgID == 36) //Тойота
            {
                if (ddlRequestRate.Text == "0,00")
                {
                    amount1 = 0;
                    
                }

                if (ddlRequestRate.Text == "29,90")
                {
                   amount1 = 0;
                }
            }

            //if (groupID == 104) // Мегаком
            //{
            //    if (ddlRequestRate.Text == "0.00")
            //    {
            //        if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amount1 = 0;
            //        else amount1 = 10;
            //    }

            //    if (ddlRequestRate.Text == "29.90")
            //    {
            //        if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amount1 = 0;
            //        else amount1 = 10;
            //    }
            //}


            //if (groupID == 106) // Мегаком
            //{
            //    if (ddlRequestRate.Text == "0.00")
            //    {
            //        if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amount1 = 0;
            //        else amount1 = 10;
            //    }

            //    if (ddlRequestRate.Text == "29.90")
            //    {
            //        if (Convert.ToDouble(RadNumTbTotalPrice.Text) <= 50000) amount1 = 0;
            //        else amount1 = 10;
            //    }
            //}

            return amount1;
        }

        protected void tbDocumentSeries_TextChanged(object sender, EventArgs e)
        {
            if (tbDocumentSeries.Text.Length > 3)
            {
                if (tbDocumentSeries.Text.Substring(0, 2).ToUpper() == "AN") { ddlDocumentTypeID.SelectedIndex = 0; }
                if (tbDocumentSeries.Text.Substring(0, 2).ToUpper() == "ID") { ddlDocumentTypeID.SelectedIndex = 1; }
            }
        }

        protected void ddlDocumentTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbDocumentSeries.Text.Length > 3)
            {
                if (tbDocumentSeries.Text.Substring(0, 2).ToUpper() == "AN") { ddlDocumentTypeID.SelectedIndex = 0; }
                if (tbDocumentSeries.Text.Substring(0, 2).ToUpper() == "ID") { ddlDocumentTypeID.SelectedIndex = 1; }
            }
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
            System.Threading.Thread.Sleep(1000);
            ctlItem.RequestUpd(editRequest);
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
            pnlNewRequest.Visible = false;
            Response.Redirect("/Partners/Partners");
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
            pnlNewRequest.Visible = false;
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
                        if (s == "B") s2 = "B, личный доход 20 001 - 28 000 сом в месяц";
                        if (s == "C") s2 = "C, личный доход 16 001 - 20 000 сом в месяц";
                        if (s == "D") s2 = "D, личный доход 10 000 – 16 000 сом в месяц";
                        if (s == "E") s2 = "E, личный доход до 10 000 сом в месяц";
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

        public string getScoreBee(string phone)
        {
            string s;
            try
            {
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://beeline-income.beeline.kg/api/customer/income?number=0772944431");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://beeline-income.beeline.kg/api/customer/income?number=" + phone);

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
                }

            }
            catch (WebException ex)
            {
                s = ex.Message;
            }
            return s;
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


        public string GetTransaktionMPZ(string tranName, string requestTextXML)
        {
            //var _url = "https://10.100.101.126:8443/issuingws/services/Issuing";
            ////var _action = "https://10.100.101.126:8443/issuingws/services/Issuing?op=queryTransactionHistory";
            //var _action = "https://10.100.101.126:8443/issuingws/services/Issuing?op=" + tranName;

            var _url = connectionStringMpzApiAddress;
            var _action = connectionStringMpzApiAddress + "?op=" + tranName;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            DateTime endDate = System.DateTime.Today;
            DateTime beginDate = System.DateTime.Today.AddMonths(-12);
            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(hfCardNumber.Value, beginDate.ToString("yyyy-MM-ddTHH:mm:ss"), endDate.ToString("yyyy-MM-ddTHH:mm:ss"), requestTextXML);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }
            return soapResult;
        }


        protected void btnGetMPZ_Click(object sender, EventArgs e)
        {
            //CallWebService(); 
            string // filedir = "Partcredits", 
                filename = "response.pdf", 
                filename2 = "responseTable.pdf";
            DateTime endDate = System.DateTime.Today;
            DateTime beginDate = System.DateTime.Today.AddMonths(-12);

            string requestTextXML =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <soapenv:Envelope
            xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
            xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
            xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
            <soapenv:Body>
            <listAccountsByCard>
            <ConnectionInfo>
            <BANK_C>07</BANK_C>
            <GROUPC>01</GROUPC>
            </ConnectionInfo>
            <Parameters>
            <CARD>" + hfCardNumber.Value + "</CARD>" +
            "<BANK_C>07</BANK_C>" +
            "<GROUPC>01</GROUPC>" +
            "</Parameters>" +
            "</listAccountsByCard>" +
            "</soapenv:Body>" +
            "</soapenv:Envelope>";


            string soapResult = GetTransaktionMPZ("listAccountsByCard", requestTextXML);


           

            XDocument xmlRow = XDocument.Parse(soapResult);
            //decimal s1 = 0, s2 = 0, s3 = 0, s4 = 0, s5 = 0, s6 = 0, s7 = 0, s8 = 0, s9 = 0, s10 = 0, s11 = 0, s12 = 0, sum = 0;
            decimal[] s = new decimal[13] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            decimal k, sum = 0;
            //int k1 = 0, k2 = 0, k3 = 0, k4 = 0, k5 = 0, k6 = 0, k7 = 0, k8 = 0, k9 = 0, k10 = 0, k11 = 0, k12 = 0, k = 0;
            //string dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12;
            DateTime[] dt = new DateTime[12];
            decimal[] dts = new decimal[12];

            //string TRAN_DATE_TIME = "", TRAN_TYPE = "", TRAN_AMT = "", CARD_ACCT = "", AMOUNT_NET = "";
            string CARD_ACCT = "", CARD = "", STATUS = "", CARD_NAME = "", vCARD_ACCT = "", vCARD = "", vSTATUS = "", vCARD_NAME = "";


            var rows = from c in xmlRow.Root.Descendants("row") select c;
            foreach (XElement row in rows)
            {

                XmlSerializer serializer = new XmlSerializer(typeof(List<item>), new XmlRootAttribute("row"));
                StringReader stringReader = new StringReader(row.ToString());
                List<item> rowItems = (List<item>)serializer.Deserialize(stringReader);

                /********************************/
                XDocument xmlItem = XDocument.Parse(row.ToString());
                var items = from c in xmlItem.Root.Descendants("item") select c;
                foreach (XElement item in items)
                {

                    //string name = item.FirstNode.ToString();
                    //string value = item.LastNode.ToString();       
                    XElement Xname = item.Element("name");
                    XElement Xvalue = item.Element("value");
                    //if (Xname.Value == "TRAN_DATE_TIME")
                    //{
                    //    TRAN_DATE_TIME = Xvalue.Value;
                    //}
                    //if (Xname.Value == "TRAN_TYPE")
                    //{
                    //    TRAN_TYPE = Xvalue.Value;
                    //}
                    //if (Xname.Value == "TRAN_AMT")
                    //{
                    //    TRAN_AMT = Xvalue.Value;
                    //}
                    //if (Xname.Value == "CARD_ACCT")
                    //{
                    //    CARD_ACCT = Xvalue.Value;
                    //}
                    //if (Xname.Value == "AMOUNT_NET")
                    //{
                    //    AMOUNT_NET = Xvalue.Value;
                    //}
                    if (Xname.Value == "CARD")
                    {
                        vCARD = Xvalue.Value;
                    }
                    if (Xname.Value == "STATUS")
                    {
                        vSTATUS = Xvalue.Value;
                    }
                    if (Xname.Value == "CARD_ACCT")
                    {
                        vCARD_ACCT = Xvalue.Value;
                    }
                    if (Xname.Value == "CARD_NAME")
                    {
                        vCARD_NAME = Xvalue.Value;
                    }

                }

                if ((vCARD == hfCardNumber.Value) && (vSTATUS == "0"))
                { CARD = vCARD; STATUS = vSTATUS; CARD_ACCT = vCARD_ACCT; CARD_NAME = vCARD_NAME; }

                //if ((TRAN_TYPE == "110") || (TRAN_TYPE == "113") || (TRAN_TYPE == "11d") || (TRAN_TYPE == "11C") || (TRAN_TYPE == "208") || (TRAN_TYPE == "312") || (TRAN_TYPE == "519"))
                //{
                //    DateTime trdt = Convert.ToDateTime(TRAN_DATE_TIME);
                //    int month = trdt.Month;

                //    for (int i = 1; i <= 12; i++)
                //    {
                //        if (month == i) { s[i] = s[i] + Convert.ToDecimal(AMOUNT_NET) / 100; }
                //        sum = sum + s[i];
                //    }

                //}
            };

            if (CARD_ACCT != "")
            {
                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
                var acc = dbR.Accounts.Where(r => r.AccountNo == CARD_ACCT).ToList().FirstOrDefault();
                var cust = dbR.Customers.Where(r => r.CustomerID == acc.CustomerID).FirstOrDefault();
                var customer = dbR.Customers.Where(r => r.CustomerID == Convert.ToInt32(hfCustomerID.Value)).FirstOrDefault();

                /***************/


                //int usrID = Convert.ToInt32(Session["UserID"].ToString());
                ////int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
                //int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
                ////int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                ////int? GroupCode = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupCode;
                ////int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
                //int branchID = dbR.Offices.Where(r => r.ID == officeID).FirstOrDefault().BranchID;
                //int OfficeID;

                //int cardOfficeID = acc.OfficeID;
                //int cardBranchID = dbR.Offices.Where(r => r.ID == cardOfficeID).FirstOrDefault().BranchID;
                //int cardCredOfficerID = Convert.ToInt32(dbRWZ.RequestsRedirects.Where(r => r.branchID == cardBranchID).FirstOrDefault().creditOfficerID);


                //if (branchID != cardBranchID)
                //{
                //    int credOfficerID = Convert.ToInt32(dbRWZ.RequestsRedirects.Where(r => r.branchID == cardBranchID).FirstOrDefault().creditOfficerID);
                //    branchID = cardBranchID;
                //    if (cardBranchID == 15) { OfficeID = 1052; } //15   Карабалта
                //    if (cardBranchID == 1016) { OfficeID = 1052; } //1016    Балыкчынский филиал
                //    if (cardBranchID == 1017) { OfficeID = 1080; } //1017    Ошский филиал
                //    if (cardBranchID == 1018) { OfficeID = 1079; } //1018    Нарынский филиал
                //    if (cardBranchID == 1019) { OfficeID = 1081; } //1019    Токмокский филиал
                //    if (cardBranchID == 1020) { OfficeID = 1078; } //1020    Караколский филиал
                //    if (cardBranchID == 1022) { OfficeID = 1082; } //1022    Филиал "Берекет"
                //    if (cardBranchID == 1023) { OfficeID = 1085; } //1023    Жалалбадский филиал
                //    if (cardBranchID == 1027) { OfficeID = 1105; } //1027    Центральный филиал

                //    dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
                //    var lst = dbW.Histories.Where(r => r.CreditID == Convert.ToInt32(hfCreditID.Value)).FirstOrDefault();
                //    lst.OfficeID = officeID;
                //    lst.CreditsAccounts = CARD_ACCT;
                //    dbW.Histories.Context.SubmitChanges();
                //}

                /*****************/

                if (cust.IdentificationNumber == customer.IdentificationNumber)
                {
                    string soapResultTable = "Период \n" + beginDate.Month.ToString() + "." + beginDate.Year.ToString() + " - " + endDate.Month.ToString() + "." + endDate.Year.ToString() + "\n" + "Месяц  -  Сумма" + "\n";
                    for (int i = 0; i < 12; i++)
                    {
                        dt[i] = beginDate.AddMonths(i);
                        dts[i] = s[Convert.ToInt32(dt[i].Month)];
                        soapResultTable = soapResultTable + dt[i].Month.ToString() + "." + dt[i].Year.ToString() + "  -  " + dts[i].ToString() + "\n";
                    }


                    k = 12;

                    //sum = s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12;
                    //k = k1 + k2 + k3 + k4 + k5 + k6 + k7 + k8 + k9 + k10 + k11 + k12;

                    //             requestTextXML =
                    //    @"<?xml version = ""1.0"" encoding = ""UTF-8""?>
                    //<soapenv:Envelope xmlns:soapenv = ""http://schemas.xmlsoap.org/soap/envelope/""
                    //xmlns:xsd = ""http://www.w3.org/2001/XMLSchema""
                    //xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance"">
                    //<soapenv:Body>
                    //<queryCardInfo>
                    //<ConnectionInfo>
                    //<BANK_C>07</BANK_C>
                    //<GROUPC>01</GROUPC>
                    //</ConnectionInfo>
                    //<Parameters>
                    //<CARD>" + hfCardNumber.Value + "</CARD>" +
                    //    "<BANK_C>07</BANK_C>" +
                    //    "<GROUPC>01</GROUPC>" +
                    //    "</Parameters>" +
                    //    "</queryCardInfo>" +
                    //    "</soapenv:Body>" +
                    //    "</soapenv:Envelope>";

                    //soapResult = GetTransaktionMPZ("queryCardInfo", requestTextXML);

                    //                  requestTextXML =
                    //                      @"<?xml version = ""1.0"" encoding = ""UTF-8""?>
                    // <soapenv:Envelope xmlns:soapenv = ""http://schemas.xmlsoap.org/soap/envelope/""
                    // xmlns:xsd = ""http://www.w3.org/2001/XMLSchema""
                    // xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance"">
                    //<soapenv:Body>
                    //<ListCustomers>
                    //<ConnectionInfo>
                    //<BANK_C>07</BANK_C>
                    //<GROUPC>01</GROUPC>
                    //</ConnectionInfo>
                    //<Parameters>
                    //<BANK_C>07</BANK_C>
                    //<CARD>" + hfCardNumber.Value + "</CARD>" +
                    //       "</Parameters>" +
                    //       "</ListCustomers>" +
                    //       "</soapenv:Body>" +
                    //       "</soapenv:Envelope>";

                    //                  soapResult = GetTransaktionMPZ("ListCustomers", requestTextXML);

                    //                  xmlRow = XDocument.Parse(soapResult);
                    //                  string SURNAME = "", F_NAMES = "";

                    //                  rows = from c in xmlRow.Root.Descendants("row") select c;
                    //                  foreach (XElement row in rows)
                    //                  {

                    //                      XmlSerializer serializer = new XmlSerializer(typeof(List<item>), new XmlRootAttribute("row"));
                    //                      StringReader stringReader = new StringReader(row.ToString());
                    //                      List<item> rowItems = (List<item>)serializer.Deserialize(stringReader);

                    //                      /********************************/
                    //                      XDocument xmlItem = XDocument.Parse(row.ToString());
                    //                      var items = from c in xmlItem.Root.Descendants("item") select c;
                    //                      foreach (XElement item in items)
                    //                      {

                    //                          //string name = item.FirstNode.ToString();
                    //                          //string value = item.LastNode.ToString();       
                    //                          XElement Xname = item.Element("name");
                    //                          XElement Xvalue = item.Element("value");
                    //                          if (Xname.Value == "SURNAME")
                    //                          {
                    //                              SURNAME = Xvalue.Value;
                    //                          }
                    //                          if (Xname.Value == "F_NAMES")
                    //                          {
                    //                              F_NAMES = Xvalue.Value;
                    //                          }
                    //                      }
                    //                  }


                    string destinationFolder = getDestinationFolder();
                    GeneralController gctl = new GeneralController();

                    string dateRandodir = gctl.DateRandodir(destinationFolder);
                    string filename2_ext = gctl.fileNameAddExt(filename2, destinationFolder, dateRandodir);

                    //destinationFile = UploadImageAndSave(true, destinationFolder + "\\" + dateRandodir, destinationFile);


                    string destinationFile = TextToPdfFileSaveAddTable(soapResultTable, destinationFolder, dateRandodir, filename2_ext, beginDate.Month.ToString() + "." + beginDate.Year.ToString() + " по " + endDate.Month.ToString() + "." + endDate.Year.ToString(), dt, dts, CARD_ACCT, acc.OpenDate);
                    {
                        RequestsFile newRequestFile = new RequestsFile
                        {
                            Name = destinationFile,
                            RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
                            ContentType = "application/xhtml+xml",
                            //Data = bytes,
                            //FullName = PortalSettings.HomeDirectory + filedir + "\\" + fullfilename2,
                            FullName = "\\" + partnerdir + "\\" + dateRandodir + "\\" + destinationFile,
                            FullName2 = "https://credit.doscredobank.kg\\" + partnerdir + "\\" + dateRandodir + "\\" + destinationFile,
                            FileDescription = tbFileDescription.Text + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                            IsPhoto = false
                        };
                        ItemController ctl = new ItemController();
                        ctl.ItemRequestFilesAddItem(newRequestFile);
                    }

                    //RadNumTbSumMonthSalary.Text = sum.ToString();
                    //ddlMonthCount.Text = k.ToString();
                    btnApproved.Enabled = true;
                    /*----------------------------------------------------*/
                    CreditController ctlCredit = new CreditController();
                    //HistoriesCustomer editItemHistoriesCustomer = new HistoriesCustomer();
                    //editItemHistoriesCustomer = ctlCredit.GetHistoriesCustomerByCreditID(Convert.ToInt32(hfCreditID.Value));
                    //editItemHistoriesCustomer.RequestSumm = sum;
                    //editItemHistoriesCustomer.ApprovedSumm = sum;
                    //ctlCredit.HistoriesCustomerUpd(editItemHistoriesCustomer);
                    /*----------------------------------------------------*/
                    //History editItemHistory = new History();
                    //editItemHistory = ctlCredit.GetHistoryByCreditID(Convert.ToInt32(hfCreditID.Value));
                    //editItemHistory.RequestPeriod = 12;
                    //editItemHistory.ApprovedRate = 12;
                    //ctlCredit.HistoryUpd(editItemHistory);
                    /*----------------------------------------------------*/
                    Request editRequest = new Request();
                    ItemController ctlItem = new ItemController();

                    editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    //editRequest.AmountDownPayment = Convert.ToDecimal(TbAmountOfDownPayment.Text);
                    //editRequest.SumMonthSalary = sum;
                    editRequest.IsReceivedMPZ = true;
                    editRequest.CardAccount = CARD_ACCT;
                    //editRequest.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);  //Convert.ToDecimal("0,00");  // Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);
                    ctlItem.RequestUpd(editRequest);

                    /*****************************/
                    refreshfiles();
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("Карточные данные указанные в заявке не совпадает с данными в ОБ");
                    MsgBox("Карточные данные указанные в заявке не совпадает с данными в ОБ", this.Page, this); 
                    btnApproved.Enabled = false;
                }
            }
            else
            {
                MsgBox("Карта не активна", this.Page, this);
                //System.Windows.Forms.MessageBox.Show("Нет транзакция по данной карте");
                btnApproved.Enabled = true;
            }

        }
        

      

        //**********************************************************
        protected string TextToPdfFileSaveAddTable(string soapResult, string destinationFolder, string dateRandodir, string filename, string beginEnd, DateTime[] dt, decimal[] dts, string CARD_ACCT, DateTime accOpenDate) //main function
        {
            //if (hasfile)
            //{


            //string destinationFolder = getDestinationFolder();
            //string dateRandodir = DateRandodir(destinationFolder);
            //string destinationFile = fileNameAddExt(filename, destinationFolder + "\\" + dateRandodir);
            //string destinationFolderFile = destinationFolder + "\\" + dateRandodir + "\\" + destinationFile;

            //try
            //{

            //    //MsgBox(filepath.ToString(), this.Page, this);
            //    FileUploadControl.SaveAs(destinationFolderFile);

            //}
            //catch (Exception ex)
            //{
            //    MsgBox(ex.ToString(), this.Page, this);
            //}
            ////}
            //return destinationFile;



            //CheckImageDirs();
            //string filepath1_1 = Server.MapPath("~/") + partnerdir + "\\" + filename;
            //string filepath1_1 = "\\Portals\\0\\"  + filedir + "\\" + filename;
            //string filepath1_2 = "https://credit.doscredobank.kg\\" + filedir + "\\" + filename;
            //int temp_ext = 0;
            //while (System.IO.File.Exists(filepath1_1))
            //{
            //    temp_ext = DateTime.Now.Millisecond;
            //    string ext_name = System.IO.Path.GetExtension(filepath1_1);
            //    string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(filepath1_1) + "_" + temp_ext;
            //    filename = filename_no_ext + temp_ext + ext_name;
            //    //filepath1_1 = "\\Portals\\0\\" + filedir + "\\" + filename;
            //    filepath1_1 = Server.MapPath("~/") + partnerdir + "\\" + filename;
            //}

            //string path = AppDomain.CurrentDomain.BaseDirectory;

            //string filepath2_2 = "https://credit.doscredobank.kg\\" + filedir;

            


               string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                var doc = new Document();
                //PdfWriter.GetInstance(doc, new FileStream(filepath2 + @"\Document.pdf", FileMode.Create));
                PdfWriter.GetInstance(doc, new FileStream(destinationFolder + "\\" + dateRandodir + "\\" + filename, FileMode.Create));
                doc.Open();
              

                PdfPTable table = new PdfPTable(3);

                //PdfPCell cell = new PdfPCell(new Phrase("Выписка c МПЦ \r\n период с " + beginEnd + "\r\n Счет:" + CARD_ACCT + "\r\n Дата открытия счета: " + accOpenDate.ToString("dd.MM.yyyy"),
                PdfPCell cell = new PdfPCell(new Phrase("Данные с МПЦ",
                  new iTextSharp.text.Font(font.BaseFont, 16,
                  iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));

                cell.BackgroundColor = new BaseColor(Color.Wheat);
                /**************/
                cell.Padding = 5;
                cell.Colspan = 3;

                PdfPCell cell2 = new PdfPCell(new Phrase(beginEnd,
                  new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16,
                  iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));

                cell2.BackgroundColor = new BaseColor(Color.Wheat);
                /**************/
                cell2.Padding = 5;
                cell2.Colspan = 3;

                cell.HorizontalAlignment = Element.ALIGN_CENTER;



                table.AddCell(cell);
        
                table.AddCell(new Phrase("№", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase("Наименование", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase("Значение", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));

                table.AddCell(new Phrase("1", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase("Номер счета", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase(CARD_ACCT, new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));

                table.AddCell(new Phrase("2", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase("Дата открытия счета", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase(accOpenDate.ToString("dd.MM.yyyy"), new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));

                table.AddCell(new Phrase("3", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase("Имя на карте", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase("Значение", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));

                table.AddCell(new Phrase("4", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase("Статус карты", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));
                table.AddCell(new Phrase("Активная", new iTextSharp.text.Font(font.BaseFont, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Orange))));

                doc.Add(table);


                doc.Close();


                //**********************************************************
            //}
            return filename;
        }



        public class item
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        public class row
        {
            public List<item> item { get; set; }
        }

        public class Details
        {
            public List<row> row { get; set; }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Credentials = new NetworkCredential(connectionStringMpzUser, connectionStringMpzPassword);
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string cardN, string beginDate, string endDate, string requestTextXML)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(requestTextXML);
            //            soapEnvelopeDocument.LoadXml(
            //@"<?xml version=""1.0"" encoding=""UTF-8""?>
            //<soapenv:Envelope
            // xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/""
            // xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
            // xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
            //<soapenv:Body>
            //<queryTransactionHistory>
            //<ConnectionInfo>
            //<BANK_C>07</BANK_C>
            //<GROUPC>01</GROUPC>
            //</ConnectionInfo>
            //<Parameters>
            //<CARD>" + cardN + "</CARD>" +
            //"<BEGIN_DATE>" + beginDate + "</BEGIN_DATE> " +
            //"<END_DATE>" + endDate + "</END_DATE>" +
            //"<BANK_C>07</BANK_C>" +
            //"<GROUPC>01</GROUPC>" +
            //"<LOCKING_FLAG>1</LOCKING_FLAG>" +
            //"</Parameters>" +
            //"</queryTransactionHistory>" +
            //"</soapenv:Body>" +
            //"</soapenv:Envelope>"
            return soapEnvelopeDocument;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        protected void btnSozfondAgree_Click(object sender, EventArgs e)
        {
            Session["RequestID"] = hfRequestID.Value;
            Response.Redirect("rptConsentSozfond");
        }

        protected void btnSaveOffice_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
            var office = dbRWZ.Offices.Where(r => r.ID == Convert.ToInt32(ddlOffice.SelectedValue)).FirstOrDefault();
            var creditOfficerID = dbRWZ.RequestsRedirect.Where(r => r.officeID == office.ID).FirstOrDefault().creditOfficerID;
            lst.OfficeID = Convert.ToInt32(ddlOffice.SelectedValue);
            lst.BranchID = office.BranchID;
            int product = 0; string purpose="Потребительская"; int loanpurpose = 2;
            if (lst.GroupID == 128) { product = 1206; loanpurpose = 2; } //сайт дкб
            if (lst.GroupID == 134) { product = 1224; purpose = "Закуп товара"; loanpurpose = 1; } //кола 
            //if (lst.GroupID == 134) product = 1224;
            dbRWZ.Requests.Context.SubmitChanges();
            
            
            GeneralController gctx = new GeneralController();
            //GeneralController.Root root = new GeneralController.Root();

            GeneralController.IncomesStructure incomesstructure = new GeneralController.IncomesStructure()
            {
                CurrencyID = 417,
                TotalPercents = 100,
            };

            GeneralController.IncomesStructuresActualDate incomesstructuresactualdate = new GeneralController.IncomesStructuresActualDate()
            {
                //ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"), //Convert.ToDateTime(tbActualDate.Text),
                ActualDate = Convert.ToDateTime(actdate), //Convert.ToDateTime(tbActualDate.Text),
                IncomesStructures = new List<GeneralController.IncomesStructure>(),
            };

            GeneralController.Picture picture = new GeneralController.Picture()
            {
                //FileName = "https://credit.doscredobank.kg/Portals/0/Credits/Nurcredits/2021/10/28/okmasro4/Screenshot_1.jpg",
                //ChangeDate = Convert.ToDateTime("2021-11-05T11:28:42"),
                File = ""
            };



            GeneralController.Partner partner = new GeneralController.Partner()
            {
                //PartnerCompanyID = partnerCode,
                //LoanPartnerSumV = Convert.ToDecimal(RadNumTbRequestSumm.Text), //0,//50000.0,
                //CommissionSum = 0
                //IssueComissionPaymentTypeID = null
            };

            //GeneralController.Guarantor guarantor = new GeneralController.Guarantor()
            //{
            //    CustomerID = 1397375,
            //    GuaranteeAmount = 50000,
            //    StartDate = Convert.ToDateTime("2021-11-05T11:28:42"),
            //    //EndDate = 
            //    Status = 1,
            //};



            dynamic dRequest = new System.Dynamic.ExpandoObject();
            //dynRoot.Name = "Tom";
            //dynRoot.Age = 46;



            //GeneralController.Root root = new GeneralController.Root()
            {

                dRequest.CustomerID = Convert.ToInt32(hfCustomerID.Value);
                dRequest.ProductID = product; //1185-Нано, 1206-Карты кредит, 1224 - Кола

                dRequest.CreditID = lst.CreditID;
                //MortrageTypeID = 16,  //Вид обеспечения 16-приобретаемая имущество

                dRequest.RequestCurrencyID = 417;
                dRequest.RequestSumm = lst.RequestSumm;
                dRequest.OfficeID = office.ID;
                dRequest.OfficerID = creditOfficerID;

                dRequest.IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>();
                dRequest.Guarantors = new List<GeneralController.Guarantor>();
                dRequest.Pictures = new List<GeneralController.Picture>();
                dRequest.Partners = new List<GeneralController.Partner>();


                dRequest.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                dRequest.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);

                dRequest.CreditPurpose = purpose;
                dRequest.LoanPurposeTypeID = loanpurpose;



            }


            //incomesstructuresactualdate.ActualDate = Convert.ToDateTime("2021-11-05T11:28:42"); //Convert.ToDateTime(dateTimeNow);
            incomesstructuresactualdate.ActualDate = Convert.ToDateTime(actdate); //Convert.ToDateTime(dateTimeNow);
            incomesstructuresactualdate.IncomesStructures.Add(incomesstructure);



            dRequest.IncomesStructuresActualDates.Add(incomesstructuresactualdate);
            //dRequest.Partners.Add(partner);
            dRequest.Pictures.Add(picture);
            //root.Guarantors.Add(guarantor);


            //string str = SendPostOBCreateRequest(root);

            string result = gctx.UpdateRequestDynWithAPI(dRequest);

            refreshGrid();
        }

        protected void btnUpdFIO_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            var req = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
            var cust = dbR.Customers.Where(c => c.CustomerID == req.CustomerID).FirstOrDefault();
            req.Surname = cust.Surname;
            req.CustomerName = cust.CustomerName;
            req.Otchestvo = cust.Otchestvo;
            dbRWZ.Requests.Context.SubmitChanges();
            refreshGrid(); pnlNewRequest.Visible = false;
        }

        protected void tbIdentificationNumber_TextChanged(object sender, EventArgs e)
        {
            if (tbIdentificationNumber.Text.Length > 13)
            {
                if (tbIdentificationNumber.Text.Substring(0, 1).ToUpper() == "1") {  rbtSex.SelectedIndex = 1; }
                if (tbIdentificationNumber.Text.Substring(0, 1).ToUpper() == "2") { rbtSex.SelectedIndex = 0; }
            }
        }

        protected void rbtSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbIdentificationNumber.Text.Length > 13)
            {
                if (tbIdentificationNumber.Text.Substring(0, 1).ToUpper() == "1") { rbtSex.SelectedIndex = 1; }
                if (tbIdentificationNumber.Text.Substring(0, 1).ToUpper() == "2") { rbtSex.SelectedIndex = 0; }
            }
        }

        protected void chkbxSelectAllOffice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxSelectAllOffice.Checked)
            {
                for (var i = 0; i < 10; i++)
                {
                    chkbxOffice.Items[i].Selected = true;
                }
            }
            else
            {
                for (var i = 0; i < 10; i++)
                {
                    chkbxOffice.Items[i].Selected = false;
                }
            }

        }

        /***************************************************************************************/


        protected void btnGuarantorEdit_Click(object sender, EventArgs e)
        {
            if (hfCustomerID.Value != "noselect")
            {
                CustomerEdit(Convert.ToInt32(hfCustomerID.Value));
                btnCredit.Text = "Выбрать клиента";
                //hfCustomerID.Value = "edit";
                //customerFieldDisable();
                disableCustomerFields();
            }
        }

        protected void btnCancelIssue_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.RequestStatus == "Выдано")
            {
                if (lst.GroupID == 110)
                {

                }
                else
                {
                    //********меняем статус в скоринге если не нано
                    CancelIssueRequest();
                }
            }
            pnlNewRequest.Visible = false;
        }

        public void CancelIssueRequest()
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            var lst5 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "На выдаче";
                dbRWZ.Requests.Context.SubmitChanges();
                System.Threading.Thread.Sleep(1000);
                refreshGrid();
                string hexBlue = "#227128";
                Color _colorBlue = System.Drawing.ColorTranslator.FromHtml(hexBlue);
                lblStatusRequest.BackColor = _colorBlue; hfRequestStatus.Value = "На выдаче";
                /*RequestHistory*/
                DateTime dateTimeServer = dateNow;
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    CreditID = Convert.ToInt32(hfCreditID.Value),
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeServer, //Convert.ToDateTime(DateTime.Now),
                    Status = "Отмена выдачи",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value)
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
            }
        }

        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            if (pnlPhoto.Visible == true) pnlPhoto.Visible = false;
            else pnlPhoto.Visible = true;
            hfPhoto2.Value = "";
        }

        protected void rbtnlistValidTill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnlistValidTill.SelectedIndex == 0)
            {
                tbValidTill.Enabled = true;
                rfvValidTill.Enabled = true;
                revValidTill.Enabled = true;
            }
            else
            {
                tbValidTill.Text = "";
                tbValidTill.Enabled = false;
                rfvValidTill.Enabled = false;
                revValidTill.Enabled = false;

            }
        }

        protected void btnApplicationForm_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());

            Session["RadNumTbMinRevenue"] = RadNumTbMinRevenue.Text;
            Session["RadNumTbMaxRevenue"] = RadNumTbMaxRevenue.Text;
            Session["ddlCountWorkDay"] = ddlCountWorkDay.Text;
            Session["RadNumTbСostPrice"] = RadNumTbСostPrice.Text;
            Session["RadNumTbOverhead"] = RadNumTbOverhead.Text;
            Session["RadNumTbFamilyExpenses"] = RadNumTbFamilyExpenses.Text;

            Response.Redirect("rptApplicationForm");
        }

        static async System.Threading.Tasks.Task<string> SendSMSWithOurRoute(string Phone, string Message)
        {
            var httpClient = new HttpClient();
            //var someXmlString = "<message><submit><da><number>996708225213</number></da><ud>user datatext</ud><from><username>smsuser</username><password>BzjRqi</password></from></submit></message>";
            var someXmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            "<message>" +
            "<submit>" +
            //"<da><number>996708225213</number></da>" +
            //"<ud>hello</ud>" +
            "<da><number>" + Phone + "</number></da>" +
            "<ud>" + Message + "</ud>" +
            "<dcs>" +
            "<coding>2</coding>" +
            "</dcs>" +
            "<from>" +
            "<username>smsuser</username>" +
            "<password>BzjRqi</password>" +
            "</from>" +
            "</submit>" +
            "</message>";

            var stringContent = new StringContent(someXmlString, Encoding.UTF8, "text/xml");
            //var stringContent = new StringContent(someXmlString);
            var response = await httpClient.PostAsync("https://smsgw.dcb.kg/sendsms", stringContent);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }


        //public static void SendMail2(string body, string to, string efrom, string replyto, string subject)
        //{
        //    try
        //    {
        //        if (String.Empty == replyto || replyto.Trim().Length == 0) replyto = "";
        //        if (efrom == "") efrom = Host.HostEmail;
        //        efrom = "СreditСonveyor@doscredobank.kg";
        //        to = to.Trim();
        //        if (AgentView.IsValidEmail(to))
        //        {
        //            List<Attachment> la = new List<Attachment> { };
        //            string strMessage2 = DotNetNuke.Services.Mail.Mail.SendMail(efrom, to, "", "", replyto, MailPriority.Normal, subject, MailFormat.Html, System.Text.Encoding.UTF8, body, la, Host.SMTPServer, Host.SMTPAuthentication, Host.SMTPUsername, Host.SMTPPassword, Host.EnableSMTPSSL);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "2." + ex.Message, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
        //    }
        //}



        //static string DateRandodir(string destinationFolder)
        //{
        //    //if (!System.IO.Directory.Exists(Server.MapPath("~/") + filedir))
        //    //    System.IO.Directory.CreateDirectory(Server.MapPath("~/") + filedir);

        //    string Year = DateTime.Now.Year.ToString();
        //    string Month = (DateTime.Now.Month < 10) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
        //    string Day = (DateTime.Now.Day < 10) ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
        //    //string Day = DateTime.Now.Day.ToString();
        //    string dateFolder = Year + "\\" + Month + "\\" + Day;
        //    string destinationFolderDate = destinationFolder + "\\" + dateFolder;
        //    if (!System.IO.Directory.Exists(destinationFolderDate))
        //        System.IO.Directory.CreateDirectory(destinationFolderDate);


        //    //string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + temp_ext;
        //    string randomdir = System.IO.Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        //    while (System.IO.Directory.Exists(destinationFolderDate + "\\" + randomdir))
        //    {
        //        //randomdir = DateTime.Now.Millisecond.ToString();
        //        randomdir = Path.GetRandomFileName();
        //        //destinationFolder = destinationFolder + randomdir;
        //    }

        //    if (!System.IO.Directory.Exists(destinationFolderDate + "\\" + randomdir))
        //        System.IO.Directory.CreateDirectory(destinationFolderDate + "\\" + randomdir);
        //    return dateFolder + "\\" + randomdir;
        //}

        public string getDestinationFolder() //Возврат
        {
            string destinationFolder =  Server.MapPath("~/") + partnerdir; // 1-вариант
            //string destinationFolder = @"E:\Uploadfiles\Credits\Dcb"; // 2-вариант
            //string destinationFolder = @"C:\Uploadfiles\Credits\Dcb"; // 2-вариант
            return destinationFolder;
        }


        //public string fileNameAddExt(string destinationFile, string destinationFolder, string dateRandodir) //Возврат файл + ext
        //{
        //    //string destinationFolder = getDestinationFolder();
        //    //int temp_ext = 0;
        //    //string ext_name = System.IO.Path.GetExtension(destinationFile);
        //    //string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(destinationFile) + "_" + temp_ext;
        //    //string fileName = filename_no_ext + ext_name;
        //    //string destinationFoldereFile = destinationFolder + fileName;
        //    string destinationFoldereFile = destinationFolder + "\\" + dateRandodir + "\\" + destinationFile;
        //    string fileName = destinationFile;

        //    while (System.IO.File.Exists(destinationFoldereFile))
        //    {
        //        int temp_ext = DateTime.Now.Millisecond;
        //        string ext_name = System.IO.Path.GetExtension(destinationFile);
        //        string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(destinationFile) + "_" + temp_ext;
        //        fileName = filename_no_ext + temp_ext + ext_name;
        //        destinationFoldereFile = destinationFolder + fileName;
        //    }
        //    return fileName;
        //}



    }
}