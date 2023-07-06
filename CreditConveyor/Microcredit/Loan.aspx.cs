using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using СreditСonveyor.Data;
using СreditСonveyor.Data.Microcredit;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;
//using Telerik;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Web.Services;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO.Compression;
using iTextSharp.text;
using ListItem = System.Web.UI.WebControls.ListItem;
using Zamat;
using Region = Zamat.Region;
using System.Windows.Forms;

namespace СreditСonveyor.Microcredit
{
    public partial class Loan : System.Web.UI.Page
    {
        public int AgentID, OrgID; public decimal fexp;
        public static int btnAddProductClick;

        public string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        public string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        public string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        static string connectionStringOBAPIAddress = ConfigurationManager.ConnectionStrings["connectionStringOBAPIAddress"].ToString();
        public string fileupl = ConfigurationManager.ConnectionStrings["fileupl"].ToString();
        public string connectionStringActualDate = ConfigurationManager.ConnectionStrings["connectionStringActualDate"].ToString();
        public string connectionStringKIB = ConfigurationManager.ConnectionStrings["connectionStringKIB"].ToString();

        OleDbConnection oledbConn;
        DateTime dateNowServer, dateNow;
        protected string partnerdir = "UploadFiles\\MicroCredits";
        string actdate = ""; //88
        
        //string actdate = "2021-09-20T11:28:42"; //86
        //string actdate = "2021-11-25T11:28:42"; //Кола
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Image1.ImageUrl = "data:image/jpeg;base64," + hfPhoto2.Value;
                if (hfPhoto2.Value != "") pnlPhoto.Visible = false;
                check_security();
                InitializeCulture();
                //if ((Session["Check"] == null) || (Session["Check"].ToString() != "true")) Response.Redirect("/Home");
                //RadNumTbTotalPrice.Culture.NumberFormat.CurrencySymbol = "";

                //if (Session["FIO"] != null) { lblUserName.Text = Session["FIO"].ToString(); }

                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                dateNowServer = dbR.GetTable<SysInfo>().FirstOrDefault().DateOD;
                dateNow = Convert.ToDateTime(DateTime.Now);
                actdate = connectionStringActualDate;
                if (connectionStringActualDate == "") actdate = dateNowServer.ToString("yyyy-MM-ddT00:00:00");
                {

                    if (!IsPostBack)
                    {
                        //databindDDL();
                        //tbDate1b.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
                        //tbDate2b.Text = DateTime.Today.Date.ToString("dd.MM.yyyy"); //"yyyy-MM-dd"
                        hfRequestAction.Value = "new";
                        btnAddProductClick = 1;
                        
                        hfUserID.Value = Session["UserID"].ToString();
                        isRole();

                        //if (e.CommandName == "Sel")
                        {
                            string strRequestID = Request.QueryString["RequestID"];
                            hfRequestID.Value = strRequestID;
                            if (strRequestID == "0") 
                            { 
                                btnNewRequest_Click(new object(), new EventArgs());
                                //lblDocuments.Visible = false;
                                pnlMenuRequest.Visible = false;
                                hideDcbService();
                                hideSmsService();
                                //databindDDL();
                            } 
                            else
                            if (!String.IsNullOrEmpty(strRequestID))
                            {
                                pnlMenuRequest.Visible = true;
                                Page.Title = strRequestID;
                                int RequestID = Convert.ToInt32(strRequestID);
                                //lblDocuments.Visible = true; 
                                
                                editcommand(RequestID);
                                refreshfiles();
                                refreshGuarantees();
                                lbHistory_Click(new object(), new EventArgs());
                            }
                            else
                            {
                                //btnNewRequest_Click(sender)
                                //btnNewRequest_Click(new object(), new EventArgs());
                            }
                            //downloadFiles(id);
                            //refreshfiles();
                            //if (req.GroupID != 91) refreshProducts(1);
                            //LinkButton lb = e.CommandSource as LinkButton;
                            //GridViewRow gvr = lb.Parent.Parent as GridViewRow;

                            //string hex = "#cbceea";
                            //Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                            //gvr.BackColor = _color;
                            //hfRequestsRowID.Value = gvr.RowIndex.ToString();
                            //lbHistory_Click(new object(), new EventArgs());

                        }

                        /**/
                    }
                    else
                    {
                        /*-----*/
                        isRole();
                    }

                    if (IsPostBack)
                    {

                        string textboxval = Request.Form["AverageMonthSalary"];
                        
                    }

                    //string embed = "<embed src = \"{0}\" width = \"auto\" height = \"900\" alt = \"pdf\" pluginspage = \"http://www.adobe.com/products/acrobat/readstep2.html\">";
                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";
                    //ltEmbed.Text = string.Format(embed, ResolveUrl("https://documentcloud.adobe.com/view-sdk-demo/PDFs/Bodea%20Brochure.pdf"));

                    //refreshGrid();

                }
            }
            catch (Exception exc) //Module failed to load
            {
                //Exceptions.ProcessModuleLoadException(this, exc);
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

        public void isRole()
        {
            if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
            {
                RadNumOtherLoans.Visible = false;
                lblOtherLoans.Visible = false;
                txtBusinessComment.Visible = false;
                lblBusinessComment.Visible = false;
                btnProffer.Visible = false;
                btnActAssessment.Visible = false;
                btnIssue.Visible = false;
                btnCancelIssue.Visible = false;
                btnConfirm.Visible = false;
                btnNoConfirm.Visible = false;
                //tbDate2b.Visible = true;
                btnFix.Visible = false;
                btnFixed.Visible = false;
                //btnSozfondAgree.Visible = true;
                //chkbxlistStatus.Visible = true;
                //chkbxGroup.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты
            {
                //chkbxlistKindActivity.Visible = true;
                RadNumOtherLoans.Visible = true;
                lblOtherLoans.Visible = true;
                txtBusinessComment.Visible = true;
                lblBusinessComment.Visible = true;
                btnProffer.Visible = false;
                btnActAssessment.Visible = false;
                btnIssue.Visible = false;
                btnCancelIssue.Visible = false;
                btnConfirm.Visible = false;
                btnNoConfirm.Visible = false;
                btnFix.Visible = false;
                btnFixed.Visible = false;
                //btnSozfondAgree.Visible = true;
                //tbDate2b.Visible = true;
                //chkbxlistStatus.Visible = true;
                //chkbxGroup.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
            {
                RadNumOtherLoans.Visible = false;
                lblOtherLoans.Visible = false;
                txtBusinessComment.Visible = false;
                lblBusinessComment.Visible = false;
                btnProffer.Visible = false;
                btnActAssessment.Visible = false;
                btnIssue.Visible = false;
                btnCancelIssue.Visible = false;
                btnFix.Visible = false;
                btnFixed.Visible = false;
                //tbDate2b.Visible = true;
                //chkbxlistStatus.Visible = true;
                //chkbxlistKindActivity.Visible = true;
                //chkbxGroup.Visible = true;
                //btnForPeriod.Visible = true;
                //btnForPeriodWithHistory.Visible = true;
                //btnSozfondAgree.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ
            {
                RadNumOtherLoans.Visible = true;
                lblOtherLoans.Visible = true;
                txtBusinessComment.Visible = true;
                lblBusinessComment.Visible = true;
                btnProffer.Visible = false;
                btnActAssessment.Visible = false;
                btnIssue.Visible = false;
                btnCancelIssue.Visible = false;
                btnConfirm.Visible = false;
                btnNoConfirm.Visible = false;
                btnFix.Visible = false;
                btnFixed.Visible = false;
                //tbDate2b.Visible = true;
                //chkbxlistStatus.Visible = true;
                //chkbxGroup.Visible = true;
                //chkbxlistKindActivity.Visible = true;
                //btnForPeriod.Visible = true;
                //btnForPeriodWithHistory.Visible = true;
                //btnSozfondAgree.Visible = true;
            }
        }

       

        public string getCorrectDatetxt(object o)
        {
            return Convert.ToDateTime(o).Date.ToString("yyyy.MM.dd");
        }

        protected void btnSearchClient_Click(object sender, EventArgs e)
        {
            SearchClient_Click();
        }
        protected void SearchClient_Click()
        {
            btnSaveCustomer.Text = "Прикрепить";
            disableCustomerFields();
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //var query = (from u in dbR.Customers where ((u.IdentificationNumber == tbSearchINN.Text) && (u.DocumentSeries == tbSerialN.Text.Substring(0, 5)) && (u.DocumentNo == tbSerialN.Text.Substring(5, 4))) select u).ToList().FirstOrDefault();
            var query = (from u in dbR.Customers where ((u.IdentificationNumber.Trim() == tbSearchINN.Text.Trim()) && (String.Concat(u.DocumentSeries, u.DocumentNo).Trim().ToUpper().Replace(" ", "") == tbSerialN.Text.Trim().ToUpper().Replace(" ", ""))) select u).ToList().FirstOrDefault();
            if (query != null)
            {
                pnlCustomer.Visible = true;
                //pnlCredit.Visible = false;
                //btnSaveCustomer.Enabled = false;
                btnCredit.Enabled = true;
                if (hfChooseClient.Value == "Выбрать клиента") hfCustomerID.Value = query.CustomerID.ToString();
                if (hfChooseClient.Value == "Выбрать поручителя") hfGuarantorID.Value = query.CustomerID.ToString();
                if (hfChooseClient.Value == "Выбрать залогодателя") hfPledgerID.Value = query.CustomerID.ToString();
                //hfCustomerID.Value = query.CustomerID.ToString();
                tbSurname.Text = query.Surname;
                tbCustomerName.Text = query.CustomerName;
                tbOtchestvo.Text = query.Otchestvo;
                if (query.IsResident == true) { rbtIsResident.SelectedIndex = 0; } else { rbtIsResident.SelectedIndex = 1; }
                tbIdentificationNumber.Text = query.IdentificationNumber;
                tbDocumentSeries.Text = query.DocumentSeries + query.DocumentNo;
                tbIssueDate.Text = Convert.ToDateTime(query.IssueDate).ToString("dd.MM.yyyy");
                // tbValidTill.Text = Convert.ToDateTime(query.DocumentValidTill).ToString("yyyy-MM-dd");
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
                    var region = dbR.GetTable<Zamat.Region>().Where(reg => reg.RegionID == birthCity.RegionID).FirstOrDefault();
                    if (region.RegionName != null)
                    {
                    }
                }
                if (ddlRegistrationCityName.Items.Count > 0 && !string.IsNullOrEmpty(query.RegistrationCityID.ToString()))
                    ddlRegistrationCityName.SelectedIndex = ddlRegistrationCityName.Items.IndexOf(ddlRegistrationCityName.Items.FindByValue(query.RegistrationCityID.ToString()));
                var RegistrationCity = (from u in dbR.Cities where ((u.CityID == query.RegistrationCityID)) select u).ToList().FirstOrDefault();
                //ddlRegistrationCityName.Items.Add(RegistrationCity.CityName);
                ddlRegistrationCityName.Items.Add(new ListItem { Text = RegistrationCity.CityName, Value = RegistrationCity.CityID.ToString() });
                var registrationCity = dbR.GetTable<City>().Where(c => c.CityID == query.RegistrationCityID).FirstOrDefault();
                if (registrationCity != null)
                {
                    var region = dbR.GetTable<Region>().Where(reg => reg.RegionID == registrationCity.RegionID).FirstOrDefault();
                }
                if (ddlResidenceCityName.Items.Count > 0 && !string.IsNullOrEmpty(query.ResidenceCityID.ToString()))
                    ddlResidenceCityName.SelectedIndex = ddlResidenceCityName.Items.IndexOf(ddlResidenceCityName.Items.FindByValue(query.ResidenceCityID.ToString()));
                var ResidenceCity = (from u in dbR.Cities where ((u.CityID == query.ResidenceCityID)) select u).ToList().FirstOrDefault();
                //ddlResidenceCityName.Items.Add(ResidenceCity.CityName);
                ddlResidenceCityName.Items.Add(new ListItem { Text = ResidenceCity.CityName, Value = ResidenceCity.CityID.ToString() });

                var residenceCity = dbR.GetTable<City>().Where(c => c.CityID == query.ResidenceCityID).FirstOrDefault();
                if (residenceCity != null)
                {
                    var region = dbR.GetTable<Region>().Where(reg => reg.RegionID == residenceCity.RegionID).FirstOrDefault();
                }
            }
            else
            {
                pnlCustomer.Visible = true;
                //pnlCredit.Visible = false;
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
            /**/
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
            int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
            int groupID = Convert.ToInt32(dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID);
            int orgID = Convert.ToInt32(dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID);
            databindDDLProduct(orgID, groupID);
            /***/

            ddlProductIndChg();
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


            if (orgID == 7)
            {
                if (ddlRequestRate.Text == "0,00")
                {

                    if (groupID == 36) //Пролинк
                    {
                        ddlRequestPeriod.Items.Clear();
                        //ddlRequestPeriod.Items.Add("3");
                        ddlRequestPeriod.Items.Add("6");
                        ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 38) //ИП Мазниченко СМ
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                        ddlRequestPeriod.Items.Add("12");
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
                        ddlRequestPeriod.Items.Add("12");
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
                    //if (groupID == 60) //ИП Волин Константин Петрович
                    //{
                    //    ddlRequestPeriod.Items.Clear();
                    //    ddlRequestPeriod.Items.Add("6");
                    //    //ddlRequestPeriod.Items.Add("12");
                    //}
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

                    if (groupID == 140) //ИП Дворниченко Д.В.
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                        ddlRequestPeriod.Items.Add("12");

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
                        ddlRequestPeriod.Items.Add("12");
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
                    if (groupID == 129) //ИП Кузьмина М.П.
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                    }
                    if (groupID == 132) //ИП Кыдыралиева А.А.
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 133) //ИП Имашева Г.И.
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("12");
                    }
                    if (groupID == 139) //ИП ВАСИЛЬЕВА ЕКАТЕРИНА ГЕННАДЬЕВНА
                    {
                        ddlRequestPeriod.Items.Clear();
                        ddlRequestPeriod.Items.Add("6");
                        ddlRequestPeriod.Items.Add("12");
                    }
                }

                if (ddlRequestRate.Text == "29,90")
                {
                    ddlRequestPeriod.Items.Clear();
                    for (int i = 3; i < 13; i++)
                    {
                        ddlRequestPeriod.Items.Add(i.ToString());
                    }
                }

                //btnGetScoreBee.Visible = false;
            }
            else
            {
                //btnGetScoreBee.Visible = false;
            }

            /***********/
        }


        public void databindDDLRatePeriod()
        {
            if (ddlProduct.SelectedValue == "КапиталБанк")
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
                ddlProduct.Items.Add("КапиталБанк");
                ddlProduct.Items.Add("003");
                ddlProduct.Items.Add("006");
                ddlProduct.Items.Add("0012");


                if (groupID == 121) //Пролинк
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
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
                ddlProduct.Items.Add("КапиталБанк");
                ddlProduct.Items.Add("003");
                ddlProduct.Items.Add("006");
                //ddlProduct.Items.Add("0012");

                if (groupID == 36) //Пролинк
                {

                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("003");
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("0012");

                }
                if (groupID == 38) //ИП Мазниченко СМ
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("0012");

                }
                if (groupID == 41) //ИП Рыжова Ирина Игоревна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("0012");
                }
                if (groupID == 40) //ИП Ким Андрей Аванасьевич
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("0012");
                }
                if (groupID == 53) //ИП Юмакаева Литфие Мельсовна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                }
                if (groupID == 54) //ИП Масловец Андрей Викторович
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                }
                if (groupID == 57) //ОсОО "EPSILON LTD"
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("0012");
                }
                if (groupID == 60) //ИП Волин Константин Петрович
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("006");
                }
                if (groupID == 77) //ОсОО "Азия Сеть Ритейл"
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                }
                if (groupID == 81) //ИП Маметова Зумрад Акбаровна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("КапиталБанк");
                }
                if (groupID == 93) //ИП Волженко Елена Анатольевна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("КапиталБанк");
                }


                if (groupID == 140) //ИП Дворниченко Д.В.
                {
                    //ddlRequestPeriod.Items.Clear();
                    //ddlRequestPeriod.Items.Add("6");
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("0012");
                    ddlProduct.Items.Add("КапиталБанк");
                }

                if (groupID == 96) //ИП Байоглиева София Закировна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                }
                if (groupID == 107) //ИП Черкащенко Елена Сергеевна
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("006");
                }
                if (groupID == 112) //ИП Раенко Дарья
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("006");
                }
                if (groupID == 113) //ИП Субанбеков Нуртай
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("006");
                }

                if (groupID == 129) //ИП Кузьмина М.П.
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("0012");
                }
                if (groupID == 133) //ИП Имашева Г.И.
                {
                    ddlProduct.Items.Clear();
                    //ddlProduct.Items.Add("КапиталБанк");
                    //ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("0012");
                }
                if (groupID == 139) //ИП ВАСИЛЬЕВА ЕКАТЕРИНА ГЕННАДЬЕВНА
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Add("КапиталБанк");
                    ddlProduct.Items.Add("006");
                    ddlProduct.Items.Add("0012");
                }





                //if (groupID == 129) //ИП Кузьмина М.П.
                //{
                //    ddlRequestPeriod.Items.Clear();
                //    ddlRequestPeriod.Items.Add("6");
                //}
                //if (groupID == 132) //ИП Кыдыралиева А.А.
                //{
                //    ddlRequestPeriod.Items.Clear();
                //    ddlRequestPeriod.Items.Add("12");
                //}
                //if (groupID == 133) //ИП Имашева Г.И.
                //{
                //    ddlRequestPeriod.Items.Clear();
                //    ddlRequestPeriod.Items.Add("12");
                //}

                //if (ddlRequestRate.Text == "29.90")
                //{
                //    ddlRequestPeriod.Items.Clear();
                //    for (int i = 3; i < 13; i++)
                //    {
                //        ddlRequestPeriod.Items.Add(i.ToString());
                //    }
                //}

                //btnGetScoreBee.Visible = false;
            }
            else
            {
                //btnGetScoreBee.Visible = false;
            }

            /***********/

        }

        protected void btnNewCustomer_Click(object sender, EventArgs e)
        {
            btnSaveCustomer.Text = "Сохранить";
            databindDDL();
            enableCustomerFields();
            clearEditControls();
            btnSaveCustomer.Enabled = true;
            btnCredit.Enabled = false;
            pnlCustomer.Visible = true;
            //pnlCredit.Visible = false;
            hfCustomerID.Value = "noselect";
            ddlDocumentTypeID.SelectedIndex = 0;
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
            tbDateOfBirth.Text = "";
            tbContactPhone.Text = "";
            tbCustomerName.Text = "";
            ddlRegistrationCityName.SelectedIndex = 0;
            ddlResidenceCityName.SelectedIndex = 0;
            //RadNumTbSumMonthSalary.Text = "";
            //RadNumTbRevenueAgro.Text = "";
            //RadNumTbMinRevenue.Text = "";
            //RadNumTbMaxRevenue.Text = "";
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
                    //DateTime DocumentValidTill = Convert.ToDateTime(tbValidTill.Text); 
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
                    if ((hfCustomerID.Value == "noselect") && (hfGuarantorID.Value == "noselect") && (hfPledgerID.Value == "noselect"))
                    {
                        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                        //var cust = dbR.Customers.Where(c => ((c.IdentificationNumber == tbIdentificationNumber.Text) && (c.DocumentSeries == tbDocumentSeries.Text.Substring(0, 5) && (c.DocumentNo == tbDocumentSeries.Text.Substring(5, 4))))).ToList();
                        
                        
                        
                        var cust = dbR.Customers.Where(c => ((c.IdentificationNumber == tbIdentificationNumber.Text.Trim()) && (String.Concat(c.DocumentSeries, c.DocumentNo) == tbDocumentSeries.Text.Trim()))).ToList();
                        if (cust.Count == 0)
                        {
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
                            //    DocumentValidTill = Convert.ToDateTime(tbValidTill.Text),
                            //    //DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4)),
                            //    IssueAuthority = tbIssueAuthority.Text,
                            //    Sex = (rbtSex.SelectedIndex == 0) ? true : false,
                            //    DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text),
                            //    //DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text.Substring(3, 2) + "." + tbDateOfBirth.Text.Substring(0, 2) + "." + tbDateOfBirth.Text.Substring(6, 4)),
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
                            //dCustomer.Data.IssueDate = Convert.ToDateTime(tbIssueDate.Text); 
                            dCustomer.Data.IssueDate = DateTime.ParseExact(tbIssueDate.Text, "dd.MM.yyyy", null);
                            //dCustomer.Data.DocumentValidTill = Convert.ToDateTime(tbValidTill.Text);
                            if (rbtnlistValidTill.Items[0].Selected == true)
                            {
                                //dCustomer.Data.DocumentValidTill = Convert.ToDateTime(tbValidTill.Text); 
                                dCustomer.Data.DocumentValidTill = DateTime.ParseExact(tbValidTill.Text, "dd.MM.yyyy", null);
                                isDocUnlimited = false;
                            }
                            else
                            {
                                isDocUnlimited = true;
                            }
                            dCustomer.Data.IsDocUnlimited = isDocUnlimited;
                            dCustomer.Data.IssueAuthority = tbIssueAuthority.Text;
                            dCustomer.Data.Sex = (rbtSex.SelectedIndex == 0) ? true : false;
                            //dCustomer.Data.DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text); 
                            dCustomer.Data.DateOfBirth = DateTime.ParseExact(tbDateOfBirth.Text, "dd.MM.yyyy", null);
                            dCustomer.Data.RegistrationStreet = tbRegistrationStreet.Text;
                            dCustomer.Data.RegistrationHouse = tbRegistrationHouse.Text;
                            dCustomer.Data.RegistrationFlat = tbRegistrationFlat.Text;
                            dCustomer.Data.ResidenceStreet = tbResidenceStreet.Text;
                            dCustomer.Data.ResidenceHouse = tbResidenceHouse.Text;
                            dCustomer.Data.ResidenceFlat = tbResidenceFlat.Text;
                            dCustomer.Data.DocumentTypeID = Convert.ToInt32(ddlDocumentTypeID.SelectedValue);
                            //dCustomer.Data.NationalityID = Convert.ToInt32(ddlNationalityID.SelectedItem.Value);
                            //dCustomer.Data.BirthCountryID = Convert.ToInt32(ddlBirthCountryID.SelectedItem.Value);
                            
                            //dCustomer.Data.RegistrationCountryID = Convert.ToInt32(ddlRegistrationCountryID.SelectedItem.Value);
                            //dCustomer.Data.ResidenceCountryID = Convert.ToInt32(ddlResidenceCountryID.SelectedItem.Value);
                            //dCustomer.Data.BirthCityID = Convert.ToInt32(ddlBirthCityName.SelectedItem.Value);
                            dCustomer.Data.RegistrationCityID = Convert.ToInt32(ddlRegistrationCityName.SelectedItem.Value);
                            dCustomer.Data.RegistrationCityName = ddlRegistrationCityName.SelectedItem.Text;
                            dCustomer.Data.ResidenceCityID = Convert.ToInt32(ddlResidenceCityName.SelectedItem.Value);
                            dCustomer.Data.ResidenceCityName = ddlResidenceCityName.SelectedItem.Text;
                            dCustomer.Data.ContactPhone1 = tbContactPhone.Text;

                            //dRequest.Partners.Add(partner);
                            string result = gctx.CreateCustomerWithAPI(dCustomer);

                            //hfCustomerID.Value = newItem.CustomerID.ToString();
                            //hfCustomerID.Value = result.ToString();
                            btnCredit.Enabled = true;
                            btnSaveCustomer.Enabled = false;
                            /*btnSave_click*/
                            pnlMenuCustomer.Visible = false;
                            //pnlCustomer.Visible = false;
                            pnlCredit.Visible = true;
                            if (hfChooseClient.Value == "Выбрать клиента")
                            {
                                hfCustomerID.Value = result.ToString();
                                tbSurname2.Text = tbSurname.Text;
                                tbCustomerName2.Text = tbCustomerName.Text;
                                tbOtchestvo2.Text = tbOtchestvo.Text;
                                //tbINN2.Text = tbIdentificationNumber.Text;
                                lblFIO.Text = tbSurname2.Text + " " + tbCustomerName2.Text + " " + tbOtchestvo2.Text;
                                lblINN.Text = tbIdentificationNumber.Text;
                            }
                            if (hfChooseClient.Value == "Выбрать поручителя")
                            {
                                hfGuarantorID.Value = result.ToString(); 
                                tbGuarantorSurname.Text = tbSurname.Text;
                                tbGuarantorName.Text = tbCustomerName.Text;
                                tbGuarantorOtchestvo.Text = tbOtchestvo.Text;
                                tbGuarantorINN.Text = tbIdentificationNumber.Text;
                            }
                            if (hfChooseClient.Value == "Выбрать залогодателя")
                            {
                                hfPledgerID.Value = result.ToString(); 
                                tbPledgerSurname.Text = tbSurname.Text;
                                tbPledgerName.Text = tbCustomerName.Text;
                                tbPledgerOtchestvo.Text = tbOtchestvo.Text;
                                tbPledgerINN.Text = tbIdentificationNumber.Text;
                            }
                        }
                        else
                        {
                            //*****/ DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Клиент есть в базе, сделайте поиск по ИНН", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            if (btnSaveCustomer.Text == "Прикрепить")
                            {
                                hfChooseClient.Value = "Выбрать клиента";
                                if (hfChooseClient.Value == "Выбрать клиента")
                                {
                                    hfCustomerID.Value = cust.FirstOrDefault().CustomerID.ToString();
                                    tbSurname2.Text = tbSurname.Text;
                                    tbCustomerName2.Text = tbCustomerName.Text;
                                    tbOtchestvo2.Text = tbOtchestvo.Text;
                                    //tbINN2.Text = tbIdentificationNumber.Text;
                                    lblFIO.Text = tbSurname2.Text + " " + tbCustomerName2.Text + " " + tbOtchestvo2.Text;
                                    lblINN.Text = tbIdentificationNumber.Text;
                                }
                                if (hfChooseClient.Value == "Выбрать поручителя")
                                {
                                    hfGuarantorID.Value = cust.FirstOrDefault().CustomerID.ToString();
                                    tbGuarantorSurname.Text = tbSurname.Text;
                                    tbGuarantorName.Text = tbCustomerName.Text;
                                    tbGuarantorOtchestvo.Text = tbOtchestvo.Text;
                                    tbGuarantorINN.Text = tbIdentificationNumber.Text;
                                }
                                if (hfChooseClient.Value == "Выбрать залогодателя")
                                {
                                    hfPledgerID.Value = cust.FirstOrDefault().CustomerID.ToString();
                                    tbPledgerSurname.Text = tbSurname.Text;
                                    tbPledgerName.Text = tbCustomerName.Text;
                                    tbPledgerOtchestvo.Text = tbOtchestvo.Text;
                                    tbPledgerINN.Text = tbIdentificationNumber.Text;
                                }
                            }
                            else 
                            MsgBox("Клиент есть в базе, сделайте поиск по ИНН", this.Page, this);
                            //MsgBox("Выдача кредита не возможна, возрастное ограничение 23-70 (не должно превышать 70 на момент окончания кредита клиенту)", this.Page, this);
                        }
                        /*btnSave_click*/
                    }
                    else
                    {
                        /*Редактирование*/
                        int CustID=0;
                        //DataContext db = new DataContext(connectionString);
                        SysController ctx = new SysController();
                        if (hfChooseClient.Value == "Выбрать клиента") { CustID = Convert.ToInt32(hfCustomerID.Value); }
                        if (hfChooseClient.Value == "Выбрать поручителя") { CustID = Convert.ToInt32(hfGuarantorID.Value); }
                        if (hfChooseClient.Value == "Выбрать залогодателя") { CustID = Convert.ToInt32(hfPledgerID.Value); }
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
                        //pnlCredit.Visible = true;
                        //pnlMenuCustomer.Visible = false;
                        //pnlCustomer.Visible = false;
                        //if (hfChooseClient.Value == "Выбрать клиента")
                        //{
                        //    hfCustomerID.Value = hfCustomerID.Value;
                        //    tbSurname2.Text = tbSurname.Text;
                        //    tbCustomerName2.Text = tbCustomerName.Text;
                        //    tbOtchestvo2.Text = tbOtchestvo.Text;
                        //    tbINN2.Text = tbIdentificationNumber.Text;
                        //}

                        if (hfChooseClient.Value == "Выбрать клиента")
                        {
                            hfCustomerID.Value = CustID.ToString();
                            tbSurname2.Text = tbSurname.Text;
                            tbCustomerName2.Text = tbCustomerName.Text;
                            tbOtchestvo2.Text = tbOtchestvo.Text;
                            //tbINN2.Text = tbIdentificationNumber.Text;
                            lblFIO.Text = tbSurname2.Text + " " + tbCustomerName2.Text + " " + tbOtchestvo2.Text;
                            lblINN.Text = tbIdentificationNumber.Text;
                        }
                        if (hfChooseClient.Value == "Выбрать поручителя")
                        {
                            hfGuarantorID.Value = CustID.ToString(); 
                            tbGuarantorSurname.Text = tbSurname.Text;
                            tbGuarantorName.Text = tbCustomerName.Text;
                            tbGuarantorOtchestvo.Text = tbOtchestvo.Text;
                            tbGuarantorINN.Text = tbIdentificationNumber.Text;
                        }
                        if (hfChooseClient.Value == "Выбрать залогодателя")
                        {
                            hfPledgerID.Value = CustID.ToString();
                            tbPledgerSurname.Text = tbSurname.Text;
                            tbPledgerName.Text = tbCustomerName.Text;
                            tbPledgerOtchestvo.Text = tbOtchestvo.Text;
                            tbPledgerINN.Text = tbIdentificationNumber.Text;
                        }
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            disableCustomerFields();
            clearEditControls();
            btnCredit.Enabled = false;
            //pnlMenuCustomer.Visible = false;
            //pnlCustomer.Visible = false;
            pnlCredit.Visible = true;
        }

        protected void btnCredit_Click(object sender, EventArgs e)
        {
            pnlMenuCustomer.Visible = false;
            pnlCustomer.Visible = false;
            pnlCredit.Visible = true;
            //if (btnCredit.Text == "Выбрать клиента")
            //{
            //    hfCustomerID.Value = hfCustomerID.Value;
            //    tbSurname2.Text = tbSurname.Text;
            //    tbCustomerName2.Text = tbCustomerName.Text;
            //    tbOtchestvo2.Text = tbOtchestvo.Text;
            //    tbINN2.Text = tbIdentificationNumber.Text;
            //    btnCustomerEdit.Enabled = true;
            //}

            if (btnCredit.Text == "Выбрать клиента")
            {
                hfCustomerID.Value = hfCustomerID.Value;
                tbSurname2.Text = tbSurname.Text;
                tbCustomerName2.Text = tbCustomerName.Text;
                tbOtchestvo2.Text = tbOtchestvo.Text;
                //tbINN2.Text = tbIdentificationNumber.Text;
                lblINN.Text = tbIdentificationNumber.Text;
                lblFIO.Text = tbSurname2.Text + " " + tbCustomerName2.Text + " " + tbOtchestvo2.Text;
                //btnCustomerEdit.Enabled = true;
            }
            if (btnCredit.Text == "Выбрать поручителя")
            {
                hfGuarantorID.Value = hfGuarantorID.Value;
                tbGuarantorSurname.Text = tbSurname.Text;
                tbGuarantorName.Text = tbCustomerName.Text;
                tbGuarantorOtchestvo.Text = tbOtchestvo.Text;
                tbGuarantorINN.Text = tbIdentificationNumber.Text;
                //btnGuarantorEdit.Enabled = true;
            }
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
            if (stavka == 30.00) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            int f = 0; int y22 = 50;
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
                //if ((zp > 50000) || (zp == 50000)) y22 = 50;
                //if (zp < 50000) y22 = 40;

                if ((y1 >= 40) && (y2 < y22)) f = 1;
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
                //if ((valp > 50000) || (valp == 50000)) y22 = 50;
                //if (valp < 50000) y22 = 40;
                if (chp > 0)
                {
                    double cho = chp - k - OtherLoans;
                    double y1 = 100 * cho / chp;
                    if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                    double y2 = 100 * (k + OtherLoans) / (valp + additionalIncome);
                    if ((y1 >= 40) && (y2 < y22)) f = 1;
                }
                if (stavka == 0) { if (s > 100000) f = -100000; }
                else { if (s > 100000) f = -100000; }
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
                double v = Convert.ToDouble(RevenueAgro) / 3 + (Convert.ToDouble(RevenueMilk) * 25);
                double valp = v - OverheadAgro - AddOverheadAgro;
                double chp = (valp + additionalIncome) - Convert.ToDouble(FamilyExpensesAgro);
                //if ((valp > 50000) || (valp == 50000)) y22 = 50;
                //if (valp < 50000) y22 = 40;
                if (chp > 0)
                {
                    double cho = chp - k - OtherLoans;
                    double y1 = 100 * cho / chp;
                    if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                    double y2 = 100 * (k + OtherLoans) / (valp + additionalIncome);
                    if ((y1 >= 40) && (y2 < y22)) f = 1;
                }
                if (s > 50000) f = -50000;
            }
            return f;
        }

        protected void btnSendCreditRequest_Click(object sender, EventArgs e)
        {
            if ((chkbxTypeOfCollateral.Items[0].Selected == true) ||
                (chkbxTypeOfCollateral.Items[1].Selected == true) ||
                (chkbxTypeOfCollateral.Items[2].Selected == true))
            {

                if ((hfCustomerID.Value != "noselect" && Page.IsValid)) //условие для поруч
                {
                    byte b = 1;
                    if ((chkbxTypeOfCollateral.Items[1].Selected == true) && (hfGuarantorID.Value == "noselect")) b = 2;
                    if ((chkbxTypeOfCollateral.Items[2].Selected == true) && (hfPledgerID.Value == "noselect")) b = 3;


                    if (b == 1)
                    {
                        if (chbEmployer.Checked) // если сотрудник
                        {
                            if ((Convert.ToInt32(Session["RoleID"]) == 8) || (Convert.ToInt32(Session["RoleID"]) == 9)) { AddRequest(); } //и (агент и админу то без скорринга)
                            else //не агент значить проверяем скорринг
                            {
                                int f = issuanceOfCredit();
                                if (Convert.ToDouble(RadNumTbFamilyExpenses.Text) < 7000) f = -7000;
                                if (f == 1)
                                {
                                    AddRequest();
                                }
                                if (f == 0)
                                {
                                    //*****//MsgBox("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита", this.Page, this);
                                    //System.Windows.Forms.MessageBox.Show("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита");
                                    MsgBox("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита", this.Page, this);
                                }
                                if (f == -50000)
                                {
                                    //*****/MsgBox("Максимальная сумма кредита 20000 сом", this.Page, this);
                                    //System.Windows.Forms.MessageBox.Show("Максимальная сумма кредита 20000 сом");
                                    MsgBox("Максимальная сумма кредита 50000 сом", this.Page, this);
                                }
                                if (f == -100000)
                                {
                                    //*****//MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this);
                                    //System.Windows.Forms.MessageBox.Show("Максимальная сумма кредита 100000 сом");
                                    MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this);
                                }
                                if (f == -7000) { MsgBox("Минимальная сумма семейных расходов - 7000 сом", this.Page, this); }
                            }
                        }
                        else // если не сотрудник, то скорринг
                        {

                            int age = GetCustomerAge(Convert.ToInt32(hfCustomerID.Value));
                            //MsgBox(age.ToString(), this.Page, this);
                            bool fage = false;
                            if (rbtnBusiness.SelectedIndex == 2) //Агро
                            {
                                if ((age > 19) && (age < 70))
                                {
                                    fage = true;
                                }
                                else
                                {
                                    //*****// MsgBox("Выдача кредита не возможна, возрастное ограничение 25-65, клиенту " + age.ToString() + " лет", this.Page, this);
                                    //System.Windows.Forms.MessageBox.Show("Выдача кредита не возможна, возрастное ограничение 25-65, клиенту " + age.ToString() + " лет");
                                    MsgBox("Выдача кредита не возможна, возрастное ограничение 20 - 70, клиенту " + age.ToString() + " лет", this.Page, this);
                                    fage = false;
                                }
                            }
                            else
                            {
                                if ((age > 19) && (age < 70))
                                {
                                    fage = true;
                                }
                                else
                                {
                                    fage = false;
                                    //*****//  MsgBox("Выдача кредита не возможна, возрастное ограничение 23-65, клиенту " + age.ToString() + " лет", this.Page, this);
                                    //System.Windows.Forms.MessageBox.Show("Выдача кредита не возможна, возрастное ограничение 23-65, клиенту " + age.ToString() + " лет");
                                    MsgBox("Выдача кредита не возможна, возрастное ограничение 20 - 70, клиенту " + age.ToString() + " лет", this.Page, this);
                                }
                            }

                            byte ageEndCredit = GetCustomerAgeWhenEndCredit(Convert.ToInt32(hfCustomerID.Value), Convert.ToInt32(ddlRequestPeriod.SelectedValue));
                            if (ageEndCredit == 0) { MsgBox("Выдача кредита не возможна, возрастное ограничение 23-70 (не должно превышать 70 на момент окончания кредита клиенту)", this.Page, this); }
                            if (ageEndCredit == 2) { MsgBox("Не полностью заполнены паспортные данные клиента в базе Банка, обратитесь к кредитному специалисту", this.Page, this); }

                            int f = issuanceOfCredit();
                            if (Convert.ToDouble(RadNumTbFamilyExpenses.Text) < 7000) f = -7000;
                            if ((f == 1) && (fage) && (ageEndCredit == 1))
                            {
                                AddRequest();
                            }
                            if (f == 0)
                            {
                                //*****// MsgBox("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита", this.Page, this);
                                //System.Windows.Forms.MessageBox.Show("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита");
                                MsgBox("Выдача кредита не возможна, попробуйте увеличить первоначальный взнос и срок кредита", this.Page, this);
                            }
                            if (f == -50000)
                            {
                                //*****//MsgBox("Максимальная сумма кредита 20000 сом, если доход от агро", this.Page, this); 
                                //System.Windows.Forms.MessageBox.Show("Максимальная сумма кредита 20000 сом, если доход от агро");
                                MsgBox("Максимальная сумма кредита 50000 сом, если доход от агро", this.Page, this);
                            }
                            if (f == -100000)
                            {
                                //*****// MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this);
                                //System.Windows.Forms.MessageBox.Show("Максимальная сумма кредита 100000 сом");
                                MsgBox("Максимальная сумма кредита 100000 сом", this.Page, this);
                            }
                            if (f == -7000) { MsgBox("Минимальная сумма семейных расходов - 7000 сом", this.Page, this); }
                        }
                    }
                    else
                    {
                        if (b == 2) { MsgBox("Необходимо выбрать поручителя", this.Page, this); }
                        if (b == 3) { MsgBox("Необходимо выбрать залогодателя", this.Page, this); }
                    }



                }
                else
                {
                    lblMessageClient.Text = "Перед сохранением заявки нужно выбрать клиента";
                }
            }
            else
            {
                MsgBox("Необходимо выбрать вид обеспечения", this.Page, this);
            }
        }

        protected int GetCustomerAge(int CustomerId)
        {
            //dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //DateTime birthDate = dbR.Customers.Where(c => c.CustomerID == CustomerId).FirstOrDefault().DateOfBirth.Value;
            //DateTime nowDate = DateTime.Today;
            //int age = nowDate.Year - birthDate.Year;
            //if (birthDate > nowDate.AddYears(-age)) age--;
            //return age;

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
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            if (hfRequestAction.Value == "new")
            {
                string str = RadNumTbAverageMonthSalary.Text;
                string textboxval = Request.Form["AverageMonthSalary"];


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
                credOfficerID = dbRWZ.RequestsRedirect.Where(r => r.branchID == branchID).FirstOrDefault().creditOfficerID;
                if ((officeID == 1133) || (officeID == 1134)) credOfficerID = dbRWZ.RequestsRedirect.Where(r => r.officeID == officeID).FirstOrDefault().creditOfficerID;
                hfAgentRoleID.Value = usrRoleID.ToString();
                DateTime dateTimeServer = dateNowServer;
                DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now);

                decimal commision = 0; string NameOfCredit = "КапиталБанк";
                //int prodID = 1152; //КапиталБанк
                //if (chbEmployer.Checked) prodID = 1153; else prodID = 1152; //КапиталБанк для сотруд иначе КапиталБанк
                int prodID = Convert.ToInt32(ddlProduct.SelectedValue);
                decimal rate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);
                byte period = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                bool isEmployer = chbEmployer.Checked ? true : false;

                ////if (Convert.ToDecimal(RadNumTbRequestSumm.Text) > 100000) { NameOfCredit = "Потребительский"; prodID = 109; }
                //if ((rate == 0) && (period == 3)) { commision = 0; NameOfCredit = "0-0-3"; prodID = 1163; }
                //if ((rate == 0) && (period == 6)) { commision = 0; NameOfCredit = "0-0-6"; prodID = 1164; }
                ////if ((rate == 0) && (period == 9)) { commision = 0; NameOfCredit = "0-0-9"; prodID = 1165; }
                //if ((rate == 0) && (period == 12)) { commision = 0; NameOfCredit = "0-0-12"; prodID = 1177; }
                
                //if (ddlProduct.SelectedValue == "003") { commision = 0; NameOfCredit = "0-0-3"; prodID = 1163; }
                //if (ddlProduct.SelectedValue == "006") { commision = 0; NameOfCredit = "0-0-6"; prodID = 1164; }
                //if (ddlProduct.SelectedValue == "006(Honor S7)") { commision = 0; NameOfCredit = "0-0-6 (Honor S7)"; prodID = 1202; }
                ////if (ddlProduct.SelectedValue == "006(Honor S7)") { commision = 0; NameOfCredit = "0-0-6 (Honor S7)"; prodID = 1193; }
                //if (ddlProduct.SelectedValue == "009") { commision = 0; NameOfCredit = "0-0-9"; prodID = 1165; }
                //if (ddlProduct.SelectedValue == "0012") { commision = 0; NameOfCredit = "0-0-12"; prodID = 1177; }
                //if (ddlProduct.SelectedValue == "0012(Honor S7)") { commision = 0; NameOfCredit = "0-0-12 (Honor S7)"; prodID = 1201; }
                ////if (ddlProduct.SelectedValue == "0012(Honor S7)") { commision = 0; NameOfCredit = "0-0-12 (Honor S7)"; prodID = 1189; }
                //if (ddlProduct.SelectedValue == "0018") { commision = 0; NameOfCredit = "0-0-18"; prodID = 1190; }

                if (isEmployer == true) commision = 0;

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
                //    //PartnerCompanyID = 1511854 // Для Нуртел - 1511854
                //    //PartnerCompanyID = GroupCode,
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
                //    ActualDate = dateTimeServer,//Convert.ToDateTime(actdate), // dateTimeServer, //Convert.ToDateTime(actdate),
                //    CurrencyID = 417,
                //    TotalPercents = 100
                //};
                //ctx.ItemIncomesStructuresAddItem(newItemIncomesStructures);
                ///*----------------------------------------------------*/ //save to dnn
                ///

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
                    ActualDate = Convert.ToDateTime(actdate), //Convert.ToDateTime(tbActualDate.Text),
                    IncomesStructures = new List<GeneralController.IncomesStructure>(),
                };

                GeneralController.Picture picture = new GeneralController.Picture()
                {
                    //FileName = "https://credit.doscredobank.kg/Portals/0/Credits/Nurcredits/2021/10/28/okmasro4/Screenshot_1.jpg",
                    //ChangeDate = Convert.ToDateTime(actdate),
                    File = hfPhoto2.Value
                };

                //GeneralController.Partner partner = new GeneralController.Partner()
                //{
                //    PartnerCompanyID = (int)GroupCode,
                //    LoanPartnerSumV = Convert.ToDecimal(RadNumTbRequestSumm.Text), //0,//50000.0,
                //    CommissionSum = 0
                //    //IssueComissionPaymentTypeID = null
                //};

                GeneralController.Guarantor guarantor = new GeneralController.Guarantor()
                {
                    CustomerID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0,
                    GuaranteeAmount = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    StartDate = Convert.ToDateTime(actdate),
                    //EndDate = 
                    Status = 1,
                };

                //DateTime creditOfficerStartDate = (connectionStringActualDate == "") ? Convert.ToDateTime(dateNow).AddDays(-1) : Convert.ToDateTime(connectionStringActualDate).AddDays(-1);
                DateTime creditOfficerStartDate = (connectionStringActualDate == "") ? Convert.ToDateTime(dateNow).AddDays(-1) : Convert.ToDateTime(connectionStringActualDate).AddDays(-1);

                int mortrageTypeID = 2;
                if (chkbxTypeOfCollateral.Items[0].Selected == true) mortrageTypeID = 13;
                if (chkbxTypeOfCollateral.Items[1].Selected == true) mortrageTypeID = 14;
                if (chkbxTypeOfCollateral.Items[2].Selected == true) mortrageTypeID = 2;
                if ((chkbxTypeOfCollateral.Items[1].Selected == true) && (chkbxTypeOfCollateral.Items[2].Selected == true)) mortrageTypeID = 14;


                root3 root = new root3()
                {
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    ProductID = prodID, //1164, //prodID,
                                        //LoanStatus = 0,
                    CreditID = 0,
                    MortrageTypeID = mortrageTypeID,  //Вид обеспечения 16-приобретаемая имущество
                    IncomeApproveTypeID = 1,
                    RequestCurrencyID = 417,
                    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    MarketingSourceID = 1,
                    RequestDate = Convert.ToDateTime(actdate), //dateTimeNow, 
                                                               //IssueAccountNo = "",
                                                               //OfficerUserName = "",
                    CreditOfficerTypeID = 1,
                    CreditOfficerStartDate = Convert.ToDateTime(actdate).AddDays(-1), // creditOfficerStartDate, //Convert.ToDateTime(dateNow).AddDays(-1),  //Convert.ToDateTime("2021-09-19T11:28:42"),  //Convert.ToDateTime(actdate), //dateTimeNow, 
                                                                                      //CreditOfficerEndDate = null, // Convert.ToDateTime(actdate), //dateTimeNow, 
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
                    CreditPurpose = 2,
                    LoanPurposeTypeID = 2,
                    IssueAccountNo = "",
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




                //GeneralController.Root root = new GeneralController.Root()
                //{

                //    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                //    ProductID = prodID, //1164, //prodID,
                //    //LoanStatus = 0,
                //    CreditID = 0,
                //    MortrageTypeID = mortrageTypeID,  //Вид обеспечения 16-приобретаемая имущество
                //    //IncomeApproveTypeID = 1,
                //    RequestCurrencyID = 417,
                //    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                //    //MarketingSourceID = 5,
                //    RequestDate = Convert.ToDateTime(actdate), //dateTimeNow, 
                //    //IssueAccountNo = "",
                //    //OfficerUserName = "",
                //    CreditOfficerTypeID = 1,
                //    CreditOfficerStartDate = creditOfficerStartDate, //Convert.ToDateTime(dateNow).AddDays(-1),  //Convert.ToDateTime("2021-09-19T11:28:42"),  //Convert.ToDateTime(actdate), //dateTimeNow, 
                //    //CreditOfficerEndDate = null, // Convert.ToDateTime(actdate), //dateTimeNow, 
                //    //OfficeID = officeID, //1105,
                //    OfficerID = Convert.ToInt32(credOfficerID), //6804,

                //    IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>(),
                //    Guarantors = new List<GeneralController.Guarantor>(),
                //    Pictures = new List<GeneralController.Picture>(),
                //    Partners = new List<GeneralController.Partner>(),


                //    RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                //    RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text), //0.0, // Convert.ToDouble(ddlRequestRate.SelectedItem.Text),
                //    PaymentSourceID = 1,
                //    //NonPaymentRisk = null,
                //    //CreditFraudStatus = null,
                //    //InformationID = null,
                //    //ParallelRelativeCredit = null,
                //    CreditPurpose = "Потребительская",
                //    LoanPurposeTypeID = 2,
                //    LoanLocation = "", //
                //    //Options = null,
                //    //ReasonRefinancing = null,
                //    //ExternalProgramID = null,
                //    //ExternalCashDeskID = null,
                //    //RedealType = null,
                //    //IsGroup = false,
                //    //GroupName = "",
                //    //CreditGroupCode = null,
                //    //RequestGrantComission = null,
                //    //RequestGrantComissionType = 1,
                //    //RequestReturnComission = null,
                //    //RequestReturnComissionType = null,
                //    //RequestTrancheIssueComission = null,
                //    //RequestTrancheIssueComissionType = null
                //};


                incomesstructuresactualdate.ActualDate = Convert.ToDateTime(actdate); //Convert.ToDateTime(dateTimeNow);
                incomesstructuresactualdate.IncomesStructures.Add(incomesstructure);



                root.IncomesStructuresActualDates.Add(incomesstructuresactualdate);
                //root.Partners.Add(partner);
                root.Pictures.Add(picture);
                //if (hfGuarantorID.Value != "noselect") 
                if (chkbxTypeOfCollateral.Items[1].Selected == true)
                    root.Guarantors.Add(guarantor);


                //string str = SendPostOBCreateRequest(root);

                string result = gctx.CreateRequestWithAPI(root);  //кафка
                int CreditsHistoriesID = 0;
                try
                {
                    CreditsHistoriesID = Convert.ToInt32(gctx.getCreditID(result)); //кафка
                }
                catch (Exception ex)
                {
                    MsgBox("Ошибка:" + result, this.Page, this); //кафка
                    lblError.Text = result;  //кафка
                    
                }

                if (CreditsHistoriesID != 0)  //кафка
                {
                    savePhoto("new", CreditsHistoriesID);
                    string comment = "";
                    //if (rbtnBusiness.SelectedIndex == 1) { fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 0; comment = txtBusinessComment.Text; }
                    //if (rbtnBusiness.SelectedIndex == 2) { fexp = (RadNumTbFamilyExpensesAgro.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpensesAgro.Text) : 0; comment = txtAgroComment.Text; }

                    fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 7000;
                    //comment = txtBusinessComment.Text;
                    if (rbtnBusiness.SelectedIndex == 1) comment = txtBusinessComment.Text;
                    if (rbtnBusiness.SelectedIndex == 2) comment = txtAgroComment.Text;
                    Request newRequest = new Request
                    {
                        CreditPurpose = tbCreditPurpose.Text,
                        RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                        CreditProduct = ddlProduct.SelectedValue,
                        SerialNumber = tbDocumentSeries.Text,
                        RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                        RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text),
                        //RequestGrantComission = Convert.ToDecimal(lblCommission.Text),
                        ActualDate = Convert.ToDateTime(dateFirstPayment),
                        Surname = tbSurname2.Text,
                        CustomerName = tbCustomerName2.Text,
                        Otchestvo = tbOtchestvo2.Text,
                        //IdentificationNumber = tbINN2.Text,
                        IdentificationNumber = lblINN.Text,
                        GuarantorID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0,
                        GuarantorSurname = tbGuarantorSurname.Text,
                        GuarantorName = tbGuarantorName.Text,
                        GuarantorOtchestvo = tbGuarantorOtchestvo.Text,
                        GuarantorIdentificationNumber = tbGuarantorINN.Text,

                        PledgerID = (hfPledgerID.Value != "noselect") ? Convert.ToInt32(hfPledgerID.Value) : 0,
                        PledgerSurname = tbPledgerSurname.Text,
                        PledgerName = tbPledgerName.Text,
                        PledgerOtchestvo = tbPledgerOtchestvo.Text,
                        PledgerIdentificationNumber = tbPledgerINN.Text,

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
                        RevenueMilkProd = (RadNumTbRevenueMilkProd.Text != "") ? Convert.ToDecimal(RadNumTbRevenueMilkProd.Text) : 0,
                        OverheadAgro = (RadNumTbOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbOverheadAgro.Text) : 0,
                        AddOverheadAgro = (RadNumTbAddOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbAddOverheadAgro.Text) : 0,
                        IsEmployer = chbEmployer.Checked ? true : false,
                        MaritalStatus = Convert.ToInt32(rbtnMaritalStatus.SelectedItem.Value),
                        GroupID = groupID,
                        OrgID = orgID,
                        //OrganizationINN = tbINNOrg.Text,
                        OfficeID = officeID,
                        isNoPledge = chkbxTypeOfCollateral.Items[0].Selected,
                        isGuarantor = chkbxTypeOfCollateral.Items[1].Selected,
                        isPledger = chkbxTypeOfCollateral.Items[2].Selected,
                        CardNumber = txtCardNumber.Text,
                        TypeOfIssue = rbtnTypeOfIssue.SelectedIndex,
                        MortrageTypeID = mortrageTypeID,
                        ContactPhone1 = tbContactPhone.Text,
                        KindOfAgriculture = txtbxKindOfAgriculture.Text,
                        Bulls = String.IsNullOrEmpty(txtBulls.Text) ? 0 : Convert.ToInt32(txtBulls.Text),
                        BairyCows = String.IsNullOrEmpty(txtbxBairyCows.Text) ? 0 : Convert.ToInt32(txtbxBairyCows.Text),
                        Sheeps = String.IsNullOrEmpty(txtbxSheeps.Text) ? 0 : Convert.ToInt32(txtbxSheeps.Text),
                        Horse = String.IsNullOrEmpty(txtbxHorse.Text) ? 0 : Convert.ToInt32(txtbxHorse.Text),
                        ExperienceAnimals = String.IsNullOrEmpty(txtbxExperienceAnimals.Text) ? 0 : Convert.ToInt32(txtbxExperienceAnimals.Text)
                        
                    };
                    

                    ItemController ctl = new ItemController();
                    int requestID = ctl.ItemRequestAddItem(newRequest);

                    //general.SaveToLoanRequest(root, requestID); //кафка

                    /*RequestHistory*//*----------------------------------------------------*/
                    string rolename = "";
                    if (usrRoleID == 2) rolename = "Кред.спец.";
                    if (usrRoleID == 19) rolename = "Опер.";

                    RequestsHistory newItem = new RequestsHistory()
                    {
                        AgentID = usrID,
                        CreditID = CreditsHistoriesID,
                        CustomerID = Convert.ToInt32(hfCustomerID.Value),
                        StatusDate = dateTimeNow,
                        Status = "Новая заявка",
                        note = rolename + ": " +  hfNote.Value, //tbNote.Text,
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
                    var RequestGuarantee = (from v in dbRWZ.Guarantees where (v.RequestID == reqID) select v);
                    foreach (Guarantee rq in RequestGuarantee)
                    {
                        rq.RequestID = requestID;
                    }
                    dbRWZ.SubmitChanges();

                    /*****************************/
                    //dbRWZ.RequestsProductsDels.DeleteAllOnSubmit(from v in dbRWZ.RequestsProductsDels where (v.RequestID == reqID) select v);
                    //dbRWZ.RequestsProductsDels.Context.SubmitChanges();
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
                    //refreshGrid();
                    clearEditControlsRequest();
                    hfRequestAction.Value = "";
                    pnlNewRequest.Visible = false;
                    btnNewRequest.Visible = true;
                    //if (Convert.ToInt32(Session["RoleID"]) == 1)
                    //{ Response.Redirect("/AgentPage"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 2)
                    //{ Response.Redirect("/ExpertBeeline"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 3)
                    //{ Response.Redirect("/AgentTour"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 4)
                    //{ Response.Redirect("/AdminPage"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 5)
                    //{ Response.Redirect("/ExpertBeeline"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 6)
                    //{ Response.Redirect("/AgentSvetofor"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 7)
                    //{ Response.Redirect("/AdminSvetofor"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 8)
                    //{ Response.Redirect("/AgentBeeline"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 19)
                    //{ Response.Redirect("/AgentCards"); }
                    //if (Convert.ToInt32(Session["RoleID"]) == 20)
                    //{ Response.Redirect("/AgentCards"); }
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

                    clearEditControlsRequest();
                    hfRequestAction.Value = "";
                    enableUpoadFiles();
                    Response.Redirect("/Microcredit/Loans");
                    
                    
                    /*****************************/
                }
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

                CreditController ctlCredit = new CreditController();
                int prodID = Convert.ToInt32(ddlProduct.SelectedValue);
                decimal commision = 0; string NameOfCredit = "КапиталБанк"; 
                //int prodID = 1152; //КапиталБанк
                //if (chbEmployer.Checked) prodID = 1153; else prodID = 1152; //КапиталБанк для сотруд иначе КапиталБанк
                decimal rate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text);
                byte period = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                bool isEmployer = chbEmployer.Checked ? true : false;
                //prodID = 109;

                ////if (Convert.ToDecimal(RadNumTbRequestSumm.Text) > 100000) { NameOfCredit = "Потребительский"; prodID = 109; }
                //if ((rate == 0) && (period == 3)) { commision = 0; NameOfCredit = "0-0-3"; prodID = 1163; }
                //if ((rate == 0) && (period == 6)) { commision = 0; NameOfCredit = "0-0-6"; prodID = 1164; }
                ////if ((rate == 0) && (period == 9)) { commision = 0; NameOfCredit = "0-0-9"; prodID = 1165; }
                //if ((rate == 0) && (period == 12)) { commision = 0; NameOfCredit = "0-0-12"; prodID = 1177; }

                //if (ddlProduct.SelectedValue == "003") { commision = 0; NameOfCredit = "0-0-3"; prodID = 1163; }
                //if (ddlProduct.SelectedValue == "006") { commision = 0; NameOfCredit = "0-0-6"; prodID = 1164; }
                //if (ddlProduct.SelectedValue == "006(Honor S7)") { commision = 0; NameOfCredit = "0-0-6 (Honor S7)"; prodID = 1202; }
                ////if (ddlProduct.SelectedValue == "006(Honor S7)") { commision = 0; NameOfCredit = "0-0-6 (Honor S7)"; prodID = 1193; }
                //if (ddlProduct.SelectedValue == "009") { commision = 0; NameOfCredit = "0-0-9"; prodID = 1165; }
                //if (ddlProduct.SelectedValue == "0012") { commision = 0; NameOfCredit = "0-0-12"; prodID = 1177; }
                //if (ddlProduct.SelectedValue == "0012(Honor S7)") { commision = 0; NameOfCredit = "0-0-12 (Honor S7)"; prodID = 1201; }
                ////if (ddlProduct.SelectedValue == "0012(Honor S7)") { commision = 0; NameOfCredit = "0-0-12 (Honor S7)"; prodID = 1189; }
                //if (ddlProduct.SelectedValue == "0018") { commision = 0; NameOfCredit = "0-0-18"; prodID = 1190; }

                if (isEmployer == true) commision = 0;
                string comment = "";
                //if (rbtnBusiness.SelectedIndex == 1) { fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 0; comment = txtBusinessComment.Text; }
                //if (rbtnBusiness.SelectedIndex == 2) { fexp = (RadNumTbFamilyExpensesAgro.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpensesAgro.Text) : 0; comment = txtAgroComment.Text; }

                fexp = (RadNumTbFamilyExpenses.Text != "") ? Convert.ToDecimal(RadNumTbFamilyExpenses.Text) : 7000;
                //comment = txtBusinessComment.Text;
                if (rbtnBusiness.SelectedIndex == 1) comment = txtBusinessComment.Text;
                if (rbtnBusiness.SelectedIndex == 2) comment = txtAgroComment.Text;

                /*----------------------------------------------------*/
                //HistoriesCustomer editItemHistoryCustomer = new HistoriesCustomer();
                //editItemHistoryCustomer = ctlCredit.GetHistoriesCustomerByCreditID(Convert.ToInt32(hfCreditID.Value));
                //editItemHistoryCustomer.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                //editItemHistoryCustomer.ApprovedSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);

                ////editItemHistory.PartnerCompanyID = GroupCode;
                //ctlCredit.HistoriesCustomerUpd(editItemHistoryCustomer);


                //History editItemHistory = new History();
                //editItemHistory = ctlCredit.GetHistoryByCreditID(Convert.ToInt32(hfCreditID.Value));
                //editItemHistory.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                //editItemHistory.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value);
                //editItemHistory.ProductID = prodID;
                //editItemHistory.ApprovedPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);
                //editItemHistory.ApprovedRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value);
                ////editItemHistory.PartnerCompanyID = GroupCode;
                //ctlCredit.HistoryUpd(editItemHistory);
                /*----------------------------------------------------*/
                string dateFirstPayment = tbActualDate.Text.Substring(6, 4) + "." + tbActualDate.Text.Substring(3, 2) + "." + tbActualDate.Text.Substring(0, 2);
                var reqs = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
                //int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                int? groupID = reqs.GroupID;
                int? GroupCode = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupCode;
                int mortrageTypeID = 2;
                //if (reqs.GroupID != 110)
                //{
                    GeneralController gctx = new GeneralController();


                    GeneralController.IncomesStructure incomesstructure = new GeneralController.IncomesStructure()
                    {
                        CurrencyID = 417,
                        TotalPercents = 100,
                    };

                    GeneralController.IncomesStructuresActualDate incomesstructuresactualdate = new GeneralController.IncomesStructuresActualDate()
                    {
                        ActualDate = Convert.ToDateTime(actdate), //Convert.ToDateTime(tbActualDate.Text),
                        IncomesStructures = new List<GeneralController.IncomesStructure>(),
                    };

                    GeneralController.Picture picture = new GeneralController.Picture()
                    {
                        //FileName = "https://credit.doscredobank.kg/Portals/0/Credits/Nurcredits/2021/10/28/okmasro4/Screenshot_1.jpg",
                        //ChangeDate = Convert.ToDateTime(actdate),
                        File = hfPhoto2.Value
                    };

                    //GeneralController.Partner partner = new GeneralController.Partner()
                    //{
                    //    PartnerCompanyID = (int)GroupCode,
                    //    LoanPartnerSumV = Convert.ToDecimal(RadNumTbRequestSumm.Text), //0,//50000.0,
                    //    CommissionSum = Convert.ToDecimal(0.0),
                    //    //IssueComissionPaymentTypeID = 1
                    //};

                    //GeneralController.Guarantor guarantor = new GeneralController.Guarantor()
                    //{
                    //    CustomerID = 1397375,
                    //    GuaranteeAmount = 50000,
                    //    StartDate = Convert.ToDateTime(actdate),
                    //    //EndDate = 
                    //    Status = 1,
                    //};



                    GeneralController.Guarantor guarantor = new GeneralController.Guarantor()
                    {
                        CustomerID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0,
                        GuaranteeAmount = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                        StartDate = Convert.ToDateTime(actdate),
                        //EndDate = 
                        Status = 1,
                    };

                    byte loanStatus = 0;
                    if ((reqs.RequestStatus == "Утверждено") || (reqs.RequestStatus == "К подписи") || (reqs.RequestStatus == "На выдаче")) loanStatus = 2;

                    
                    if (chkbxTypeOfCollateral.Items[0].Selected == true) mortrageTypeID = 17;
                    if (chkbxTypeOfCollateral.Items[1].Selected == true) mortrageTypeID = 14;
                    if (chkbxTypeOfCollateral.Items[2].Selected == true) mortrageTypeID = 2;
                    if ((chkbxTypeOfCollateral.Items[1].Selected == true) && (chkbxTypeOfCollateral.Items[2].Selected == true)) mortrageTypeID = 14;
                    mortrageTypeID = 13;

                    //root3update root = new root3update()
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
                        MarketingSourceID = 1,
                        //RequestDate = Convert.ToDateTime(actdate, //dateTimeNow, 
                        //IssueAccountNo = null,
                        //OfficerUserName = null,
                        //CreditOfficerTypeID = 1,
                        //CreditOfficerStartDate = Convert.ToDateTime("2021-09-15T11:28:42"), //dateTimeNow, 
                        // CreditOfficerEndDate = null, // Convert.ToDateTime(actdate), //dateTimeNow, 
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
                        IssueAccountNo = String.IsNullOrEmpty(reqs.CardAccount) ? null : reqs.CardAccount,
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

                    //GeneralController.RootUpdate root = new GeneralController.RootUpdate()
                    //{

                    //    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    //    ProductID = prodID, //1164, //prodID,
                    //    LoanStatus = loanStatus,
                    //    CreditID = Convert.ToInt32(hfCreditID.Value),
                    //    MortrageTypeID = mortrageTypeID,  //Вид обеспечения 16-приобретаемая имущество
                    //    IncomeApproveTypeID = 1,
                    //    RequestCurrencyID = 417,
                    //    RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text),
                    //    //MarketingSourceID = 5,
                    //    //RequestDate = Convert.ToDateTime(actdate, //dateTimeNow, 
                    //    //IssueAccountNo = null,
                    //    //OfficerUserName = null,
                    //    //CreditOfficerTypeID = 1,
                    //    //CreditOfficerStartDate = Convert.ToDateTime("2021-09-15T11:28:42"), //dateTimeNow, 
                    //    // CreditOfficerEndDate = null, // Convert.ToDateTime(actdate), //dateTimeNow, 
                    //    OfficeID = Convert.ToInt32(officeID), //1105,
                    //                                          //OfficerID = Convert.ToInt32(credOfficerID), //6804,

                    //    IncomesStructuresActualDates = new List<GeneralController.IncomesStructuresActualDate>(),
                    //    Guarantors = new List<GeneralController.Guarantor>(),
                    //    Pictures = new List<GeneralController.Picture>(),
                    //    Partners = new List<GeneralController.Partner>(),


                    //    RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue),
                    //    RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Text), //0.0, // Convert.ToDouble(ddlRequestRate.SelectedItem.Text),

                    //    //PaymentSourceID = 1,
                    //    //NonPaymentRisk = null,
                    //    //CreditFraudStatus = null,
                    //    //InformationID = null,
                    //    //ParallelRelativeCredit = null,
                    //    CreditPurpose = "Потребительская",
                    //    LoanPurposeTypeID = 2,
                    //    //LoanLocation = "",
                    //    //Options = null,
                    //    //ReasonRefinancing = null,
                    //    //ExternalProgramID = null,
                    //    //ExternalCashDeskID = null,
                    //    //RedealType = null,
                    //    //IsGroup = false,
                    //    //GroupName = "",
                    //    //CreditGroupCode = null,
                    //    //RequestGrantComission = null,
                    //    //RequestGrantComissionType = 1,
                    //    //RequestReturnComission = null,
                    //    //RequestReturnComissionType = null,
                    //    //RequestTrancheIssueComission = null,
                    //    //RequestTrancheIssueComissionType = null
                    //};


                    incomesstructuresactualdate.ActualDate = Convert.ToDateTime(actdate); //Convert.ToDateTime(dateTimeNow);
                    incomesstructuresactualdate.IncomesStructures.Add(incomesstructure);



                    root.IncomesStructuresActualDates.Add(incomesstructuresactualdate);
                    //root.Partners.Add(partner);
                    root.Pictures.Add(picture);
                    //root.Guarantors.Add(guarantor);
                    //if (hfGuarantorID.Value != "noselect")
                    if (chkbxTypeOfCollateral.Items[1].Selected == true)
                        root.Guarantors.Add(guarantor);


                    //string str = SendPostOBCreateRequest(root);

                    string result = gctx.UpdateRequestWithAPI(root);  //кафка

                    //try
                    //{

                    //    respCreateReq dez = JsonConvert.DeserializeObject<respCreateReq>(result.ToString());
                    //    if (dez.CreditID != null) { }
                    //    //return dez.CreditID.ToString();
                    //    else
                    //    {
                    //        //return result; 
                    //        MsgBox("Ошибка:" + result, this.Page, this);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    //return ex.ToString();
                    //    MsgBox("Ошибка:" + ex.ToString(), this.Page, this);
                    //}


                    //GeneralController general = new GeneralController();
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
                        return;
                    }
                    //savePhoto("edit", CreditsHistoriesID);
                    savePhoto("edit", Convert.ToInt32(hfCreditID.Value));
                    

                //}




                if (CreditsHistoriesID != 0)
                {




                    /*******************************************************************************************/


                    Request editRequest = new Request();
                    ItemController ctlItem = new ItemController();
                    editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    editRequest.CreditPurpose = tbCreditPurpose.Text;
                    editRequest.CreditProduct = ddlProduct.SelectedValue;
                    editRequest.RequestPeriod = Convert.ToByte(ddlRequestPeriod.SelectedValue);

                    editRequest.RequestSumm = Convert.ToDecimal(RadNumTbRequestSumm.Text);
                    editRequest.RequestRate = Convert.ToDecimal(ddlRequestRate.SelectedItem.Value);
                    //editRequest.RequestGrantComission = Convert.ToDecimal(lblCommission.Text);
                    editRequest.ActualDate = Convert.ToDateTime(dateFirstPayment);
                    editRequest.Surname = tbSurname2.Text;
                    editRequest.CustomerName = tbCustomerName2.Text;
                    editRequest.Otchestvo = tbOtchestvo2.Text;
                    //editRequest.IdentificationNumber = tbINN2.Text;
                    editRequest.IdentificationNumber = lblINN.Text;
                    if (chkbxTypeOfCollateral.Items[1].Selected == true)
                    {
                        editRequest.GuarantorID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0;
                        editRequest.GuarantorSurname = tbGuarantorSurname.Text;
                        editRequest.GuarantorName = tbGuarantorName.Text;
                        editRequest.GuarantorOtchestvo = tbGuarantorOtchestvo.Text;
                        editRequest.GuarantorIdentificationNumber = tbGuarantorINN.Text;
                    }
                    else
                    {
                        editRequest.GuarantorID = null;
                        editRequest.GuarantorSurname = "";
                        editRequest.GuarantorName = "";
                        editRequest.GuarantorOtchestvo = "";
                        editRequest.GuarantorIdentificationNumber = "";
                    }

                    if (chkbxTypeOfCollateral.Items[2].Selected == true)
                    {
                        editRequest.PledgerID = (hfPledgerID.Value != "noselect") ? Convert.ToInt32(hfPledgerID.Value) : 0;
                        editRequest.PledgerSurname = tbPledgerSurname.Text;
                        editRequest.PledgerName = tbPledgerName.Text;
                        editRequest.PledgerOtchestvo = tbPledgerOtchestvo.Text;
                        editRequest.PledgerIdentificationNumber = tbPledgerINN.Text;
                    }
                    else
                    {
                        editRequest.PledgerID = null;
                        editRequest.PledgerSurname = "";
                        editRequest.PledgerName = "";
                        editRequest.PledgerOtchestvo = "";
                        editRequest.PledgerIdentificationNumber = "";
                    }


                    //editRequest.OrganizationINN = tbINNOrg.Text;
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
                    editRequest.RevenueMilkProd = (RadNumTbRevenueMilkProd.Text != "") ? Convert.ToDecimal(RadNumTbRevenueMilkProd.Text) : 0;
                    editRequest.OverheadAgro = (RadNumTbOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbOverheadAgro.Text) : 0;
                    editRequest.AddOverheadAgro = (RadNumTbAddOverheadAgro.Text != "") ? Convert.ToDecimal(RadNumTbAddOverheadAgro.Text) : 0;
                    editRequest.MaritalStatus = Convert.ToInt32(rbtnMaritalStatus.SelectedItem.Value);
                    //editRequest.OrganizationINN = tbINNOrg.Text;
                    editRequest.isNoPledge = chkbxTypeOfCollateral.Items[0].Selected;
                    editRequest.isGuarantor = chkbxTypeOfCollateral.Items[1].Selected;
                    editRequest.isPledger = chkbxTypeOfCollateral.Items[2].Selected;
                    editRequest.CardNumber = txtCardNumber.Text;
                    editRequest.TypeOfIssue = rbtnTypeOfIssue.SelectedIndex;
                    editRequest.MortrageTypeID = mortrageTypeID;
                    editRequest.KindOfAgriculture = txtbxKindOfAgriculture.Text;
                    editRequest.Bulls = String.IsNullOrEmpty(txtBulls.Text) ? 0 : Convert.ToInt32(txtBulls.Text);
                    editRequest.BairyCows = String.IsNullOrEmpty(txtbxBairyCows.Text) ? 0 : Convert.ToInt32(txtbxBairyCows.Text);
                    editRequest.Sheeps = String.IsNullOrEmpty(txtbxSheeps.Text) ? 0 : Convert.ToInt32(txtbxSheeps.Text);
                    editRequest.Horse = String.IsNullOrEmpty(txtbxHorse.Text) ? 0 : Convert.ToInt32(txtbxHorse.Text);
                    editRequest.ExperienceAnimals = String.IsNullOrEmpty(txtbxExperienceAnimals.Text) ? 0 : Convert.ToInt32(txtbxExperienceAnimals.Text);
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
                        note = hfNote.Value,   //tbNote.Text,
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
                }

                clearEditControlsRequest();
                hfRequestAction.Value = "";
                Response.Redirect("/Microcredit/Loans");
            }
                /******************************************************/
                //refreshGrid();
                
                //clearEditControlsRequest();
                //hfRequestAction.Value = "";
                
            
                //if (Convert.ToInt32(Session["RoleID"]) == 1)
                //{ Response.Redirect("/AgentPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 2)
                //{ Response.Redirect("/ExpertBeeline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 3)
                //{ Response.Redirect("/AgentTour"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 4)
                //{ Response.Redirect("/AdminPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 5)
                //{ Response.Redirect("/ExpertBeeline"); }
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
                

                //pnlNewRequest.Visible = false;
            
        }

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
                    Base64ToImage().Save(Server.MapPath("~/") + partnerdir +"\\"+ dateRanmdodir + "\\" + destinationFile);
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

        public class respCreateReq
        {
            public string CreditID { get; set; }
            public string CreditStatus { get; set; }
        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

      

        public void enableUpoadFiles()
        {
            //****// AsyncUpload1.Enabled = true;
            tbFileDescription.Enabled = true;
            btnUploadFiles.Enabled = true;
            FileUploadControl.Enabled = true;

            //lblNameDcbServ.Visible = true;
            //btnPassport.Visible = true;
            //btnKIB.Visible = true;
            //btnKIB2.Visible = true;
            //btnAnimals.Visible = true;
            //btnGns.Visible = true;
            //btnInitPerm.Visible = true;
            //txtCodePerm.Visible = true;
            //btnConfPerm.Visible = true;
            //btnSalary.Visible = true;
            //btnPension.Visible = true;
        }

        public void disableUpoadFiles()
        {
            //****// AsyncUpload1.Enabled = false;
            tbFileDescription.Enabled = false;
            btnUploadFiles.Enabled = false;
            FileUploadControl.Enabled = false;

            //lblNameDcbServ.Visible = false;
            //btnPassport.Visible = false;
            //btnKIB.Visible = false;
            //btnKIB2.Visible = false;

            //btnAnimals.Visible = false;
            //btnGns.Visible = false;
            //btnInitPerm.Visible = false;
            //txtCodePerm.Visible = false;
            //btnConfPerm.Visible = false;
            //btnSalary.Visible = false;
            //btnPension.Visible = false;

            gvRequestsFiles.DataSource = null;
            gvRequestsFiles.DataBind();

            gvLogDcbService.DataSource = null;
            gvLogDcbService.DataBind();


            //gvRequestsFiles2.DataSource = null;
            //gvRequestsFiles2.DataBind();

            
        }

        public void productspriceupdate()
        {
            if (hfRequestAction.Value == "edit")
            {
                Request editRequest = new Request();
                ItemController ctlItem = new ItemController();
                editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));

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
            var lstRequestFiles = dbRWZ.RequestsFiles.Where(r => (r.RequestID == Convert.ToInt32(hfRequestID.Value) && ((r.IsPhoto == false) || (r.IsPhoto == null)))).ToList();
            if (lstRequestFiles != null)
            {
                gvRequestsFiles.DataSource = lstRequestFiles;
                gvRequestsFiles.DataBind();
                //gvRequestsFiles2.DataSource = lstRequestFiles;
                //gvRequestsFiles2.DataBind();
            }

            var lstRequestFilesPhoto = dbRWZ.RequestsFiles.Where(r => (r.RequestID == Convert.ToInt32(hfRequestID.Value) && (r.IsPhoto == true))).ToList();
            if (lstRequestFilesPhoto != null)
            {
                gvRequestsFilesPhoto.DataSource = lstRequestFilesPhoto;
                gvRequestsFilesPhoto.DataBind();
                //gvRequestsFilesPhoto2.DataSource = lstRequestFilesPhoto;
                //gvRequestsFilesPhoto2.DataBind();
            }
            try
            {
                Image1.ImageUrl = (lstRequestFilesPhoto.Count != 0) ? fileupl + lstRequestFilesPhoto.LastOrDefault().FullName : "";
            }
            catch { }
            refreshLogDcbServ();

        }


        public void refreshLogDcbServ()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lstLogDcbService = dbRWZ.LogDcbServices.Where(r => (r.RequestID == Convert.ToInt32(hfRequestID.Value))).ToList();
            if (lstLogDcbService != null)
            {
                gvLogDcbService.DataSource = lstLogDcbService;
                gvLogDcbService.DataBind();
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


        public void downloadFiles(int id)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            try
            {
                var reqs = dbRWZ.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                var reqsfiles = dbRWZ.RequestsFiles.Where(r => r.RequestID == id).ToList();
                foreach (var rf in reqsfiles)
                {
                    if (rf.FullName.Contains("balance.kg"))
                    {
                        //GeneralController gtx = new GeneralController();
                        //string destinationFolder = getDestinationFolder();
                        //string dateRandodir = gtx.DateRandodir(destinationFolder);
                        //string destinationFile = gtx.copyFile(rf.FullName, destinationFolder, dateRandodir);
                        updFileToRequest(rf.ID);


                    }
                }

            }
            catch (Exception ex)
            {

            }
        }


        public void updFileToRequest(int n)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var rf = dbRWZ.RequestsFiles.Where(r => r.ID == n).FirstOrDefault();

            GeneralController gtx = new GeneralController();
            string destinationFolder = getDestinationFolder();
            string dateRandodir = gtx.DateRandodir(destinationFolder);
            string destinationFile = gtx.copyFileBee(rf.FullName, destinationFolder, dateRandodir);

            string destinationFile2 = "\\" + partnerdir + "\\" + dateRandodir + "\\" + destinationFile;

            //TableController tblCtrl2 = new TableController();

            rf.Name = destinationFile;
            rf.FullName2 = rf.FullName;
            rf.Name2 = rf.FullName;
            rf.FullName = destinationFile2;
            //rf.Name = destinationFile;
            dbRWZ.RequestsFiles.Context.SubmitChanges();

        }


        public void hideDcbService()
        {
            lblNameDcbServ.Visible = false;
            btnPassport.Visible = false; //ГРС
            btnKIB.Visible = false;
            btnKIB2.Visible = false;

            btnAnimals.Visible = false; //Аймак
            btnGns.Visible = false; //ГНС
            btnInitPerm.Visible = false; //Запрос на разрешения
            txtCodePerm.Visible = false; //Текст Кода запроса на разрешения
            btnConfPerm.Visible = false; //Подтверждение запроса
            btnSalary.Visible = false; //Зарплата
            btnPension.Visible = false; //Пенсия
           
        }

        public void hideSmsService()
        {
           
            lblNameSMS.Visible = false;
            btnShowSMS.Visible = false; //Показать СМС
            btnShowSMS2.Visible = false; //Показать СМС
            lblSMS.Visible = false; //Текст СМС
            btnSendSMS.Visible = false; //Отправить СМС
            btnCheckSMS.Visible = false; //Проверить СМС
            lblCheckSMS.Visible = false; //Текст проверки СМС
        }

        public void showDcbService()
        {
            //lblNameDcbServ.Visible = true;
            //btnPassport.Visible = true; //ГРС
            btnKIB.Visible = true;
            btnKIB2.Visible = true;

            //btnAnimals.Visible = true; //Аймак
            //btnGns.Visible = true; //ГНС
            //btnInitPerm.Visible = true; //Запрос на разрешения
            //txtCodePerm.Visible = true; //Текст Кода запроса на разрешения
            //btnConfPerm.Visible = true; //Подтверждение запроса
            //btnSalary.Visible = true; //Зарплата
            //btnPension.Visible = true; //Пенсия
           
        }

        public void showSmsService()
        {
           
            //lblNameSMS.Visible = true;
            //btnShowSMS.Visible = true; //Показать СМС
            //btnShowSMS2.Visible = true; //Показать СМС
            //lblSMS.Visible = true; //Текст СМС
            //btnSendSMS.Visible = true; //Отправить СМС
            //btnCheckSMS.Visible = true; //Проверить СМС
            //lblCheckSMS.Visible = true; //Текст проверки СМС
        }

        public void editcommand(int id)
        {
            divDocuments.Visible = true;
            hideDcbService();
            refreshLogDcbServ();
            txtCodePerm.Text = "";
            tbNoteCancelReq.Text = "";
            tbNoteCancelReqExp.Text = "";
            lblError.Text = "";
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            try
            {
                pnlNewRequest.Visible = true;
                //lblAccount.Text = "";
                var lst = dbRWZ.Requests.Where(r => r.RequestID == id).FirstOrDefault();
                //var account = dbR.CreditsAccounts.Where(a => ((a.CreditID == lst.CreditID) && (a.TypeID == 3))).FirstOrDefault();
                //if (account != null) lblAccount.Text = account.AccountNo.ToString();
                tbCreditPurpose.Text = lst.CreditPurpose;

                /**/
                int groupID = Convert.ToInt32(lst.GroupID);
                int orgID = Convert.ToInt32(lst.OrgID);
                databindDDLProduct(orgID, groupID);
                /***/
                if ((lst.RequestStatus == "Выдано") || (lst.RequestStatus == "Принято"))
                {
                    btnUpdFIO.Visible = false;
                }
                else
                {
                    btnUpdFIO.Visible = true;
                }

                pnlPhoto.Visible = false;
                ddlOffice.Visible = false;
                btnSaveOffice.Visible = false;
                if (lst.GroupID == 110)
                {
                    btnApproved.Text = "Верификация пройдена";

                    //ddlOffice.Visible = true;
                    ddlOffice.SelectedIndex = ddlOffice.Items.IndexOf(ddlOffice.Items.FindByValue(lst.OfficeID.ToString()));

                    if ((lst.RequestStatus == "Новая заявка") || (lst.RequestStatus == "Подтверждено"))
                    {
                        if ((Convert.ToInt32(Session["RoleID"]) == 2) || (Convert.ToInt32(Session["RoleID"]) == 5))
                        {
                            ddlOffice.Visible = true;
                            btnSaveOffice.Visible = true;
                        }
                    }
                }
                else { btnApproved.Text = "Утверждено"; }

                if (lst.GroupID == 95)
                {
                    //btnApproved.Text = "Верификация пройдена";

                    //ddlOffice.Visible = true;
                    ddlOffice.SelectedIndex = ddlOffice.Items.IndexOf(ddlOffice.Items.FindByValue(lst.OfficeID.ToString()));

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
                    //    btnApproved.Text = "Утверждено"; 
                }

                string prod = (lst.CreditProduct == null) ? "" : lst.CreditProduct.ToString();
                ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByValue(prod));
                ddlProductIndChg();
                ddlRequestRate.SelectedIndex = ddlRequestRate.Items.IndexOf(ddlRequestRate.Items.FindByValue(lst.RequestRate.ToString()));
                ddlRequestPeriod.SelectedIndex = ddlRequestPeriod.Items.IndexOf(ddlRequestPeriod.Items.FindByValue(lst.RequestPeriod.ToString()));


                RadNumTbRequestSumm.Text = lst.RequestSumm.ToString();
                RadNumOtherLoans.Text = lst.OtherLoans.ToString();
                RadNumTbAdditionalIncome.Text = lst.AdditionalIncome.ToString();
                txtBusinessComment.Text = lst.BusinessComment;
                txtAgroComment.Text = lst.BusinessComment;
                btnComment.Visible = true;

                //btnForPeriod.Visible = false;
                //btnForPeriodWithHistory.Visible = false;
                //btnForPeriodWithProducts.Visible = false;

                //lblCommission.Text = lst.RequestGrantComission.ToString();
                tbActualDate.Text = Convert.ToDateTime(lst.ActualDate).ToString("dd.MM.yyyy");
                RadNumTbMonthlyInstallment.Text = lst.MonthlyInstallment.ToString();
                txtbxKindOfAgriculture.Text = lst.KindOfAgriculture;
                txtBulls.Text = lst.Bulls.ToString();
                txtbxBairyCows.Text = lst.BairyCows.ToString();
                txtbxSheeps.Text = lst.Sheeps.ToString();
                txtbxHorse.Text = lst.Horse.ToString();
                
                txtbxExperienceAnimals.Text = lst.ExperienceAnimals.ToString();

                

                hfCustomerID.Value = lst.CustomerID.ToString();
                hfCreditID.Value = lst.CreditID.ToString();
                hfRequestID.Value = id.ToString();
                tbSurname2.Text = lst.Surname;
                tbCustomerName2.Text = lst.CustomerName;
                tbOtchestvo2.Text = lst.Otchestvo;
                //tbINN2.Text = lst.IdentificationNumber;
                lblINN.Text = lst.IdentificationNumber;
                lblFIO.Text = tbSurname2.Text + " " + tbCustomerName2.Text + " " + tbOtchestvo2.Text;
                //tbINNOrg.Text = lst.OrganizationINN;
                chbEmployer.Checked = lst.IsEmployer;
                lblPhone.Text = lst.ContactPhone1;
                
                if (lst.isGuarantor == true)
                {
                    hfGuarantorID.Value = lst.GuarantorID.ToString();
                    tbGuarantorSurname.Text = lst.GuarantorSurname;
                    tbGuarantorName.Text = lst.GuarantorName;
                    tbGuarantorOtchestvo.Text = lst.GuarantorOtchestvo;
                    tbGuarantorINN.Text = lst.GuarantorIdentificationNumber;
                }
                
                if (lst.isPledger == true)
                {
                    hfPledgerID.Value = lst.PledgerID.ToString();
                    tbPledgerSurname.Text = lst.PledgerSurname;
                    tbPledgerName.Text = lst.PledgerName;
                    tbPledgerOtchestvo.Text = lst.PledgerOtchestvo;
                    tbPledgerINN.Text = lst.PledgerIdentificationNumber;
                }

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
                    RadNumTbFamilyExpenses.Text = lst.FamilyExpenses.ToString();
                    btnSozfondAgree.Enabled = true;
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
                    btnSozfondAgree.Enabled = true;
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
                //btnCloseRequest.Visible = true; //закры форму заявки
                //btnCalculator.Visible = true; //калькулятор
                btnNewRequest.Visible = true; //новая заявка
                btnProffer.Visible = false; //предложение зп
                btnActAssessment.Visible = false;
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
                pnlEmployment.Visible = false; // зп
                btnAgreement.Visible = false; //договор
                chbEmployer.Enabled = false; //Сотрудник
                btnInProcess.Visible = false; //
                btnApproved.Visible = false; //
                btnSignature.Visible = false;
                /**********************/

                tbSearchINN.Text = lst.IdentificationNumber;
                tbSerialN.Text = lst.SerialNumber;
                btnSaveCustomer.Enabled = false;
                btnCancel.Enabled = false;
                SearchClient_Click();
                /***********************/


                txtCardNumber.Text = lst.CardNumber;

                if (lst.TypeOfIssue == 0) { rbtnTypeOfIssue.Items[0].Selected = true; lblCardNumber.Visible = false; txtCardNumber.Visible = false; txtCardNumber.Text = ""; }
                if (lst.TypeOfIssue == 1) { rbtnTypeOfIssue.Items[1].Selected = true; lblCardNumber.Visible = true; txtCardNumber.Visible = true; }

                //if (rbtnTypeOfIssue.SelectedValue == "Касса") { lblCardNumber.Visible = false; txtCardNumber.Visible = false; txtCardNumber.Text = ""; }
                //if (rbtnTypeOfIssue.SelectedValue == "Карта") { lblCardNumber.Visible = true; txtCardNumber.Visible = true; }



                chkbxTypeOfCollateral.Items[0].Selected = (lst.isNoPledge != null) ? (bool)lst.isNoPledge : false;
                chkbxTypeOfCollateral.Items[1].Selected = (lst.isGuarantor != null) ? (bool)lst.isGuarantor : false;
                chkbxTypeOfCollateral.Items[2].Selected = (lst.isPledger != null) ? (bool)lst.isPledger : false;


                if (chkbxTypeOfCollateral.Items[1].Selected == true) { pnlGuarantor.Visible = true; } else { pnlGuarantor.Visible = false; }
                if (chkbxTypeOfCollateral.Items[2].Selected == true) { pnlGuarantees.Visible = true; } else { pnlGuarantees.Visible = false; }


                if (chkbxTypeOfCollateral.Items[0].Selected == true)
                {
                    chkbxTypeOfCollateral.Items[0].Enabled = true;
                    chkbxTypeOfCollateral.Items[1].Enabled = false;
                    chkbxTypeOfCollateral.Items[2].Enabled = false;
                }
                if ((chkbxTypeOfCollateral.Items[1].Selected == true) || (chkbxTypeOfCollateral.Items[2].Selected == true))
                {
                    chkbxTypeOfCollateral.Items[0].Enabled = false;
                    chkbxTypeOfCollateral.Items[1].Enabled = true;
                    chkbxTypeOfCollateral.Items[2].Enabled = true;
                }
                if ((chkbxTypeOfCollateral.Items[1].Selected == true) && (chkbxTypeOfCollateral.Items[2].Selected == true))
                {
                    chkbxTypeOfCollateral.Items[0].Enabled = false;
                    chkbxTypeOfCollateral.Items[1].Enabled = true;
                    chkbxTypeOfCollateral.Items[2].Enabled = true;
                }
                /*************/
                if (lst.MortrageTypeID == 17) chkbxTypeOfCollateral.Items[0].Selected = true;
                //else { chkbxTypeOfCollateral.Items[0].Selected = false; chkbxTypeOfCollateral.Items[1].Selected = false; chkbxTypeOfCollateral.Items[2].Selected = false; }

                if (lst.MortrageTypeID == 14) chkbxTypeOfCollateral.Items[1].Selected = true;
                //else { chkbxTypeOfCollateral.Items[0].Selected = false; chkbxTypeOfCollateral.Items[1].Selected = false; chkbxTypeOfCollateral.Items[2].Selected = false; }

                if (lst.MortrageTypeID == 2) chkbxTypeOfCollateral.Items[2].Selected = true;
                //else { chkbxTypeOfCollateral.Items[0].Selected = false; chkbxTypeOfCollateral.Items[1].Selected = false; chkbxTypeOfCollateral.Items[2].Selected = false; }


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

                if (lst.GroupID == 110) { btnProfileNano.Visible = true; }
                else { btnProfileNano.Visible = false; }


                hideDcbService();
                hideSmsService();

                /**********************************************/

                if (lst.RequestStatus == "Новая заявка")
                {
                    btnSozfondAgree.Visible = true;

                    btnSendCreditRequest.Visible = true;
                    chbEmployer.Enabled = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnCancelReq.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 9)
                    {
                        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; //btnConfirm.Visible = true; 
                        btnInProcess.Visible = true;
                    } //Админы Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2)
                    {
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true;
                        btnProffer.Visible = true; // btnConfirm.Visible = true;
                        btnActAssessment.Visible = true;
                       
                        showDcbService();
                        hideSmsService();

                    } //Эксперты
                    if (Convert.ToInt32(Session["RoleID"]) == 5)
                    {
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; //btnConfirm.Visible = false; 
                        btnActAssessment.Visible = true;
                        btnActAssessment.Visible = true;
                        btnApproved.Visible = true;
                        btnApproved.Visible = true;
                        if (lst.GroupID == 110) btnFix.Visible = false;
                        else btnFix.Visible = true;


                        
                        showDcbService();
                        hideSmsService();

                    } //Эксперты ГБ
                }
                if (lst.RequestStatus == "В обработке")
                {
                    btnSozfondAgree.Visible = true;

                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnCancelReq.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = true;
                        btnActAssessment.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnCancelReqExp.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; //btnConfirm.Visible = false; 
                        btnActAssessment.Visible = true;
                        btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; //btnConfirm.Visible = true; 
                        btnInProcess.Visible = true;
                    }
                }

                if (lst.RequestStatus == "Исправить")
                {
                    btnSozfondAgree.Visible = true;

                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFixed.Visible = true; //Исправлено
                    }

                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFixed.Visible = true; //Исправлено

                        btnProffer.Visible = true; //Предложение
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameDcbServ.Visible = true;
                        //btnPassport.Visible = true; //ГРС
                        //btnKIB.Visible = true;
                        //btnKIB2.Visible = true;
                        //btnAnimals.Visible = true; //Аймак
                        //btnGns.Visible = true; //ГНС
                        //btnInitPerm.Visible = true; //Запрос на разрешения
                        //txtCodePerm.Visible = true; //Текст Кода запроса на разрешения
                        //btnConfPerm.Visible = true; //Подтверждение запроса
                        //btnSalary.Visible = true; //Зарплата
                        //btnPension.Visible = true; //Пенсия
                        showDcbService();
                        hideSmsService();

                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnCancelReqExp.Visible = true; //Отказано
                        btnProffer.Visible = true;
                        //btnConfirm.Visible = false;
                        btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameDcbServ.Visible = true;
                        //btnPassport.Visible = true; //ГРС
                        //btnKIB.Visible = true;
                        //btnKIB2.Visible = true;
                        //btnAnimals.Visible = true; //Аймак
                        //btnGns.Visible = true; //ГНС
                        //btnInitPerm.Visible = true; //Запрос на разрешения
                        //txtCodePerm.Visible = true; //Текст Кода запроса на разрешения
                        //btnConfPerm.Visible = true; //Подтверждение запроса
                        //btnSalary.Visible = true; //Зарплата
                        //btnPension.Visible = true; //Пенсия
                        showDcbService();
                        hideSmsService();
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; //btnConfirm.Visible = true; 
                        btnInProcess.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Oper
                    {
                        btnFixed.Visible = true; 
                    }
                }

                if (lst.RequestStatus == "Исправлено")
                {
                    btnSozfondAgree.Visible = true;

                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnCancelReq.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true;  
                        btnActAssessment.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameDcbServ.Visible = true;
                        //btnPassport.Visible = true; //ГРС
                        //btnKIB.Visible = true;
                        //btnKIB2.Visible = true;
                        //btnAnimals.Visible = true; //Аймак
                        //btnGns.Visible = true; //ГНС
                        //btnInitPerm.Visible = true; //Запрос на разрешения
                        //txtCodePerm.Visible = true; //Текст Кода запроса на разрешения
                        //btnConfPerm.Visible = true; //Подтверждение запроса
                        //btnSalary.Visible = true; //Зарплата
                        //btnPension.Visible = true; //Пенсия
                        showDcbService();
                        hideSmsService();
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnApproved.Visible = true; btnFix.Visible = true;
                        btnActAssessment.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameDcbServ.Visible = true;
                        //btnPassport.Visible = true; //ГРС
                        //btnKIB.Visible = true;
                        //btnKIB2.Visible = true;
                        //btnAnimals.Visible = true; //Аймак
                        //btnGns.Visible = true; //ГНС
                        //btnInitPerm.Visible = true; //Запрос на разрешения
                        //txtCodePerm.Visible = true; //Текст Кода запроса на разрешения
                        //btnConfPerm.Visible = true; //Подтверждение запроса
                        //btnSalary.Visible = true; //Зарплата
                        //btnPension.Visible = true; //Пенсия
                        showDcbService();
                        hideSmsService();
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; btnNoConfirm.Visible = true; //btnConfirm.Visible = true; 
                        btnInProcess.Visible = true;
                    }
                }
                if (lst.RequestStatus == "Не подтверждено")
                {
                    btnSozfondAgree.Visible = true;

                    btnSendCreditRequest.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnFixed.Visible = true; btnCancelReq.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) { btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; } //Эксперты 
                    if (Convert.ToInt32(Session["RoleID"]) == 5) { btnCancelReqExp.Visible = true; btnApproved.Visible = true; } //Эксперты ГБ
                    if (Convert.ToInt32(Session["RoleID"]) == 9) { btnConfirm.Visible = true; btnCancelReq.Visible = true; btnInProcess.Visible = true; btnApproved.Visible = true; } //Админы Билайн
                }
                if (lst.RequestStatus == "Отменено")
                {
                    btnSozfondAgree.Visible = true;

                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }

                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnProffer.Visible = true;
                        btnActAssessment.Visible = true;

                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnProffer.Visible = true;
                        btnActAssessment.Visible = true;
                    }
                }
                if (lst.RequestStatus == "Отказано")
                {
                    btnSozfondAgree.Visible = true;

                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }

                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnProffer.Visible = true;
                        btnActAssessment.Visible = true;

                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnProffer.Visible = true;
                        btnActAssessment.Visible = true;
                    }
                }
                if (lst.RequestStatus == "Подтверждено")
                {
                    btnSozfondAgree.Visible = true;

                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    { }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {

                        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnActAssessment.Visible = true;
                        btnApproved.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnActAssessment.Visible = true;
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
                    btnSozfondAgree.Visible = true;

                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnSignature.Visible = true;
                        btnFixed.Visible = false;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {

                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnProffer.Visible = true;
                        btnActAssessment.Visible = true;
                        btnSendCreditRequest.Visible = true;
                       // btnSendSMS.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameSMS.Visible = true;
                        //btnShowSMS.Visible = true; //Показать СМС
                        //btnShowSMS2.Visible = true; //Показать СМС

                        //lblSMS.Visible = true; //Текст СМС
                        //btnSendSMS.Visible = true; //Отправить СМС
                        //btnCheckSMS.Visible = true; //Проверить СМС
                        //lblCheckSMS.Visible = true; //Текст проверки СМС
                        showSmsService();
                        showDcbService();
                        //btnSignature.Visible = true;

                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {

                        btnSendCreditRequest.Visible = true; btnProffer.Visible = true;
                        btnActAssessment.Visible = true;
                        btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        //btnSendSMS.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameDcbServ.Visible = true;
                        //btnPassport.Visible = true; //ГРС
                        //btnKIB.Visible = true;
                        //btnKIB2.Visible = true;
                        //btnAnimals.Visible = true; //Аймак
                        //btnGns.Visible = true; //ГНС
                        //lblNameSMS.Visible = true;
                        //btnShowSMS.Visible = true; //Показать СМС
                        //btnShowSMS2.Visible = true; //Показать СМС
                        //lblSMS.Visible = true; //Текст СМС
                        //btnSendSMS.Visible = true; //Отправить СМС
                        //btnCheckSMS.Visible = true; //Проверить СМС
                        //lblCheckSMS.Visible = true; //Текст проверки СМС
                        showDcbService();
                        showSmsService();
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }

                if (lst.RequestStatus == "К подписи")
                {
                    btnSozfondAgree.Visible = true;

                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {

                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnProffer.Visible = true; //btnCancelReqExp.Visible = true;
                        btnActAssessment.Visible = true;
                        btnSendCreditRequest.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameSMS.Visible = true;
                        //btnShowSMS.Visible = true; //Показать СМС
                        //btnShowSMS2.Visible = true; //Показать СМС
                        //lblSMS.Visible = true; //Текст СМС
                        //btnSendSMS.Visible = true; //Отправить СМС
                        //btnCheckSMS.Visible = true; //Проверить СМС
                        //lblCheckSMS.Visible = true; //Текст проверки СМС
                        hideDcbService();
                        showSmsService();
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {

                        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnActAssessment.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameSMS.Visible = true;
                        //btnShowSMS.Visible = true; //Показать СМС
                        //btnShowSMS2.Visible = true; //Показать СМС
                        //lblSMS.Visible = true; //Текст СМС
                        //btnSendSMS.Visible = true; //Отправить СМС
                        //btnCheckSMS.Visible = true; //Проверить СМС
                        //lblCheckSMS.Visible = true; //Текст проверки СМС
                        showSmsService();
                        showDcbService();
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }
                if (lst.RequestStatus == "На выдаче")
                {
                    btnSozfondAgree.Visible = true;

                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) { btnIssue.Visible = true; } //Агенты Билайн
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {

                        btnCancelReq.Visible = true; btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        btnProffer.Visible = true; //btnCancelReqExp.Visible = true;
                        btnActAssessment.Visible = true;
                        btnSendCreditRequest.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameSMS.Visible = true;
                        //btnShowSMS.Visible = true; //Показать СМС
                        //btnShowSMS2.Visible = true; //Показать СМС
                        //lblSMS.Visible = true; //Текст СМС
                        //btnSendSMS.Visible = true; //Отправить СМС
                        //btnCheckSMS.Visible = true; //Проверить СМС
                        //lblCheckSMS.Visible = true; //Текст проверки СМС
                        showSmsService();
                        hideDcbService();
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {

                        btnSendCreditRequest.Visible = true; btnSendCreditRequest.Visible = true; btnProffer.Visible = true;
                        btnActAssessment.Visible = true;
                        btnCancelReqExp.Visible = true; btnCancelReq.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }

                        //lblNameSMS.Visible = true;
                        //btnShowSMS.Visible = true; //Показать СМС
                        //btnShowSMS2.Visible = true; //Показать СМС
                        //lblSMS.Visible = true; //Текст СМС
                        //btnSendSMS.Visible = true; //Отправить СМС
                        //btnCheckSMS.Visible = true; //Проверить СМС
                        //lblCheckSMS.Visible = true; //Текст проверки СМС
                        //btnIssue.Visible = true;
                        showSmsService();
                        showDcbService();
                    }

                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }
                if (lst.RequestStatus == "Выдано")
                {

                    hideDcbService();
                    hideSmsService();

                    btnSozfondAgree.Visible = true;

                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        btnProffer.Visible = true; btnReceived.Visible = true;
                        btnActAssessment.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        btnProffer.Visible = true; btnReceived.Visible = true;
                        btnActAssessment.Visible = true;
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        if (lst.GroupID != 110)
                        {
                            if (hfUserID.Value == "6075") btnCancelIssue.Visible = true;
                            if (hfUserID.Value == "6248") btnCancelIssue.Visible = true;
                        }
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }
                if (lst.RequestStatus == "Принято")
                {
                    hideDcbService();
                    hideSmsService();

                    btnSozfondAgree.Visible = true;

                    btnAgreement.Visible = true;
                    if (lst.Bussiness == 0) { if (chbEmployer.Checked) { pnlEmployment.Visible = false; } else { pnlEmployment.Visible = true; } }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 2) //Эксперты Капитал
                    {
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        btnProffer.Visible = true;
                        btnActAssessment.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 5) //Эксперты ГБ Капитал
                    {
                        if (lst.Bussiness == 0) { pnlEmployment.Visible = true; }
                        btnProffer.Visible = true;
                        btnActAssessment.Visible = true;
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                // DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Error: " + ex.Message + "<hr>" + ex.Source + "<hr>" + ex.StackTrace, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
            }
        }

     

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            disableCustomerFields();
            //pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            hfCustomerID.Value = "noselect";
            //hfGuarantorID.Value = "noselect";
            btnCredit.Text = "Выбрать клиента";
            hfChooseClient.Value = "Выбрать клиента";
            tbSurname2.Text = "";
            tbCustomerName2.Text = "";
            tbOtchestvo2.Text = "";
            //tbINN2.Text = "";
            lblINN.Text = "";
            lblFIO.Text = "";
            //tbINNOrg.Text = "";
            clearEditControls();
            lblMessageClient.Text = "";
            tbSearchINN.Text = "";
            tbContactPhone.Text = "";
            btnSaveCustomer.Enabled = true;
            tbSerialN.Text = "";
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

        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            disableCustomerFields();
            btnUpdFIO.Visible = false;
            divDocuments.Visible = false;
            hfPhoto2.Value = "";
            btnPhotoClose.Visible = false;
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
            hfCustomerID.Value = "noselect";
            hfCustomerID.Value = "noselect";
            btnAgreement.Visible = false;
            btnPledgeAgreement.Visible = false;
            btnProffer.Visible = false;
            btnActAssessment.Visible = false;
            btnReceptionAct.Visible = false;
            btnIssue.Visible = false;
            btnCancelIssue.Visible = false;
            pnlNewRequest.Visible = true;
            //btnCloseRequest.Visible = true;
            btnSozfondAgree.Visible = false;
            btnComment.Visible = false;
            //btnForPeriod.Visible = false;
            //btnForPeriodWithHistory.Visible = false;
            //btnForPeriodWithProducts.Visible = false;
            RadNumTbFamilyExpenses.Text = "7000";
            btnSendCreditRequest.Visible = true;
            chbEmployer.Checked = false;
            btnNewRequest.Visible = false;
            ddlOffice.Visible = false;
            btnSaveOffice.Visible = false;
            chkbxTypeOfCollateral.Items[0].Selected = false;
            chkbxTypeOfCollateral.Items[1].Selected = false;
            chkbxTypeOfCollateral.Items[2].Selected = false;
            chkbxTypeOfCollateral.Items[0].Enabled = true;
            chkbxTypeOfCollateral.Items[1].Enabled = true;
            chkbxTypeOfCollateral.Items[2].Enabled = true;

            //lblNameSMS.Visible = false;
            //btnShowSMS.Visible = false;
            //btnShowSMS2.Visible = false; 
            //btnSendSMS.Visible = false;
            //btnCheckSMS.Visible = false;
            hideSmsService();


            pnlGuarantor.Visible = false;
            tbGuarantorSurname.Text = "";
            tbGuarantorName.Text = "";
            tbGuarantorOtchestvo.Text = "";
            tbGuarantorINN.Text = "";

            pnlGuarantees.Visible = false;
            tbPledgerSurname.Text = "";
            tbPledgerName.Text = "";
            tbPledgerOtchestvo.Text = "";
            tbPledgerINN.Text = "";

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

            refreshfiles();
            //btnCustomerEdit.Enabled = false;
            gvHistory.DataSource = null;
            gvHistory.DataBind();


            //pnlBlackList.Visible = false;
            //pnlBlackListOrg.Visible = false;
            //BlackListHide();
            btnUpdFIO.Visible = false;

            /**/
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            //dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
            int officeID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
            int groupID = Convert.ToInt32(dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID);
            int orgID = Convert.ToInt32(dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID);
            databindDDLProduct(orgID, groupID);
            /***/

            refreshGuarantees();
            pnlGuarantees.Visible = false;
            pnlGuarantor.Visible = false;
            lblgvGuaranteesError.Text = "";
        }

        public void clearEditControlsRequest()
        {
            tbCreditPurpose.Text = "";

            RadNumTbRequestSumm.Text = "";
            tbActualDate.Text = "";
            tbSurname2.Text = "";
            tbCustomerName2.Text = "";
            tbOtchestvo2.Text = "";
            //tbINN2.Text = "";
            lblINN.Text = "";
            lblFIO.Text = "";
            //tbINNOrg.Text = "";
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
            hfPledgerID.Value = "noselect";
            btnCustomerSearch.Enabled = true;

            RadNumTbSumMonthSalary.Text = "";
            ddlMonthCount.Text = "1";
            RadNumOtherLoans.Text = "";
            RadNumTbRevenueAgro.Text = "";
            RadNumTbRevenueMilk.Text = "";
            RadNumTbRevenueMilkProd.Text = "";
            RadNumTbOverheadAgro.Text = "";
            RadNumTbAddOverheadAgro.Text = "";
            RadNumTbFamilyExpenses.Text = "";



        }

        protected void btnSearchRequest_Click(object sender, EventArgs e)
        {
            hfRequestsRowID.Value = "";
            clearEditControlsRequest();
            pnlNewRequest.Visible = false;

            //btnForPeriod.Visible = true;
            //btnForPeriodWithHistory.Visible = true;
            //btnForPeriodWithProducts.Visible = true;
            pnlNewRequest.Visible = false;
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

            CreditController crdCtr = new CreditController();
            var lstGraphics = crdCtr.GraphicsGetItem(Convert.ToInt32(hfCreditID.Value));
            if (lstGraphics.Count > 0)
            {

                dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
                var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
                if (lst.GroupID == 110)
                {
                    Response.Redirect("rptAgreesNano", true);
                }

                if (ddlRequestRate.SelectedValue == "0,00")
                {
                    Response.Redirect("rptAgreesInst", true);
                }
                else
                {
                    Response.Redirect("rptAgrees", true);
                }
            }
            else
            {
                MsgBox("График погашений не построен, обратитесь к специалисту Банка", this.Page, this);
            }
        }

        protected void rbtnBusiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnBusiness.SelectedIndex == 0) { pnlEmployment.Visible = true; pnlBusiness.Visible = false; pnlAgro.Visible = false; btnSozfondAgree.Enabled = true; }
            if (rbtnBusiness.SelectedIndex == 1) { pnlBusiness.Visible = true; pnlEmployment.Visible = false; pnlAgro.Visible = false; btnSozfondAgree.Enabled = true; }
            if (rbtnBusiness.SelectedIndex == 2) { pnlAgro.Visible = true; pnlBusiness.Visible = false; pnlEmployment.Visible = false; btnSozfondAgree.Enabled = true; }
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

        //protected void Upload(object sender, EventArgs e)
        //{
        //}

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

      
        protected void btnUploadFiles_Click(object sender, EventArgs e)
        {
            //string filename = "";
            //fullfilename = "";
            
            string destinationFile = "";
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
                                string destinationFolder = getDestinationFolder();
                                GeneralController gctl = new GeneralController();
                                string dateRandomdir = gctl.DateRandodir(destinationFolder);
                                destinationFile = gctl.fileNameAddExt(FileUploadControl.FileName, destinationFolder, dateRandomdir);

                                //fullfilename = UploadImageAndSave(true, FileUploadControl.FileName);


                                try
                                {
                                    FileUploadControl.SaveAs(destinationFolder + "\\" + dateRandomdir + "\\" + destinationFile);
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
                                            FullName = "\\" + partnerdir + "\\" + dateRandomdir + "\\" + destinationFile,
                                            FullName2 = fileupl + "\\" + partnerdir + "\\" + dateRandomdir + "\\" + destinationFile,
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
                            StatusLabel.Text = "Статус загрузки: Максимальный размер файла 10мб!";
                    }
                    //else StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Статус загрузки: Файл не загружен. Ошибка: " + ex.Message;
                }
            }

            tbFileDescription.Text = "";
            Session["upldoc"] = "1";
        }

   

        protected void CheckImageDirs()
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/") + partnerdir))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/") + partnerdir);

            if (!System.IO.Directory.Exists(Server.MapPath("~/") + partnerdir))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/") + partnerdir);
        }

        protected void btnCloseRequest_Click(object sender, EventArgs e)
        {
            pnlNewRequest.Visible = false;
            //btnCloseRequest.Visible = false;
            btnSendCreditRequest.Visible = false;
            btnAgreement.Visible = false;
            btnPledgeAgreement.Visible = false;
            btnProffer.Visible = false;
            btnActAssessment.Visible = false;
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



        protected async void btnIssue_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.RequestStatus == "На выдаче")
            {
                if (lst.GroupID == 9)
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
                            //*****//  DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB1 + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //System.Windows.Forms.MessageBox.Show(strOB1 + " " + ex.ToString());
                            MsgBox(strOB1 + " " + ex.ToString(), this.Page, this);
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
                            //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса На выдаче в АБС" + strOB1, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса На выдаче в АБС" + strOB1);
                            MsgBox("Ошибка при отправке статуса На выдаче в АБС" + strOB1, this.Page, this);
                        }
                    }
                    /**********************************/





                    //********отправляем статус в нуртелеком ****Begin ***/
                    //strNur = await SendStatusBee(lst.CreditID.ToString(), "ISSUED_BY");
                    //if (strNur == "200")
                    //{

                    //}
                    //else
                    //{
                    //    //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса ISSUED_BY в Билайн", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //    //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса ISSUED_BY в Билайн");
                    //    MsgBox("Ошибка при отправке статуса ISSUED_BY к Билайн", this.Page, this);
                    //}
                    //********отправляем статус в нуртелеком ****End ***/

                    //if ((strNur == "200") && (strOB1 == "200"))
                    if (strOB1 == "200")
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
            //string body = "", to = "rtentiev@doscredobank.kg", efrom = "", replyto = "", subject = "Выдано", strOB = "200";
            //if (lst != null)
            //{
            //    body = "Заявка№: " + hfRequestID.Value + ", ФИО клиента: " + lst.Surname + " " + lst.CustomerName + " " + lst.Otchestvo;
            //}
            //if (lst.OrgID == 7) // МТ 
            //{
            //    //SendMail2(body, to, efrom, replyto, subject);
            //    try
            //    {
            //        int reqID = dbRWZ.Requests2s.Where(r => r.RequestID2 == lst.RequestID).FirstOrDefault().RequestID;
            //        strOB = await SendStatusMT(reqID.ToString(), "Выдано");
            //    }
            //    catch (Exception ex)
            //    {
            //        //*****//  DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
            //        //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
            //        MsgBox(strOB + " " + ex.ToString(), this.Page, this);
            //    }
            //    finally
            //    {
            //    }
            //}
            //Отправка сообщ для МТ-Онлайн }
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
                        //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, result, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        //System.Windows.Forms.MessageBox.Show(result);
                        MsgBox(result, this.Page, this);
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


        public async System.Threading.Tasks.Task<string> KIBRequestOB(string CustomerID, string requestSumm, byte number)
        {
            
            int reqsum = (int)Convert.ToDecimal(requestSumm);
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("https://10.64.17.85/");
                    client.BaseAddress = new Uri("https://" + connectionStringOBAPIAddress + "/");
                    string json = ""; string url = "";

                    if (number == 1) url = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/OnlineBankCustomers/MakeRequestToCib?customerID=" + CustomerID + "&isMainCustomer=true&pdfReportType=1&requestSumm=" + reqsum.ToString() + "&reportLog=true";
                    if (number == 2) url = "https://" + connectionStringOBAPIAddress + "/OnlineBank.IntegrationService/api/OnlineBankCustomers/MakeRequestToCib?customerID=" + CustomerID + "&isMainCustomer=true&pdfReportType=2&requestSumm=" + reqsum.ToString() + "&reportLog=true";
                    var response = await client.GetAsync(url);
                    //var response = await client.GetAsync("https://10.64.17.86/OnlineBank.IntegrationService/api/OnlineBankCustomers/MakeRequestToCib?customerID=1920037&isMainCustomer=true&pdfReportType=1&requestSumm=10000&reportLog=true");
                    var result = await response.Content.ReadAsStringAsync();


                    var result2 = await response.Content.ReadAsByteArrayAsync(); // ReadAsStringAsync();

                    /************/
                    string username = Session["UserName"].ToString();
                    string servRequest_id = getRequest_id2(result);
                    //histServ(servRequest_id, "КИБ", username);
                    refreshLogDcbServ();
                    /************/
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    if (number == 1)
                    {
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + "MyFile.pdf");
                    }
                    if (number == 2)
                    {
                        Response.ContentType = "application/xls";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + "MyFile.xlsx");
                    }
                    //byte[] bitmapData = Encoding.UTF8.GetBytes(result);
                    //ToImage(result2).Save(Server.MapPath("~/") + "MyFile.pdf");
                    Response.BinaryWrite(result2);
                    Response.Flush();
                    //Response.TransmitFile( Server.MapPath("~/Files/MyFile.pdf"));
                    // Response.End();
                    this.Context.ApplicationInstance.CompleteRequest();
                    Response.Redirect("/Microcredit/Loan");

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

        public class structgetstatInitPerm
        {
            public string status { get; set; }
            public string message { get; set; }
        }

        public string getstatInitPerm(string result)
        {
            try
            {

                structgetstatInitPerm dez = JsonConvert.DeserializeObject<structgetstatInitPerm>(result.ToString());
                return dez.status.ToString();
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
                //refreshGrid();
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
                    note = hfNote.Value, //tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value)
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
            }
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
                //refreshGrid();
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
                    note = hfNote.Value, //tbNote.Text,
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





        protected void gvRequestsFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Del")
            {
                ItemController ctl = new ItemController();
                ctl.DeleteRequestsFiles(id);
                System.Threading.Thread.Sleep(1000);

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
            Response.Redirect("rptReceptionAct", true);
        }

        protected void btnProffer_Click(object sender, EventArgs e)
        {
            //string worktype = "0";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.GroupID == 110) //Нано
            {
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
                Response.Redirect("rptProfferNano", true);
            };


            if (rbtnBusiness.SelectedIndex == 0) //Зарплатный
            {
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
                Response.Redirect("rptProffer", true);
            };
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
                Response.Redirect("rptProfferBusiness", true);
            };
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
                Session["RadNumTbFamilyExpensesAgro"] = (RadNumTbFamilyExpenses.Text == "") ? "0" : Convert.ToDouble(RadNumTbFamilyExpenses.Text).ToString();
                Session["RadNumOtherLoans"] = (RadNumOtherLoans.Text == "") ? "0" : Convert.ToDouble(RadNumOtherLoans.Text).ToString();
                Response.Redirect("rptProfferAgro", true);
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
            Response.Redirect("rptPledgeAgreement", true);
        }

        //protected void btnCustomerEdit_Click(object sender, EventArgs e)
        //{
        //    CustomerEdit(Convert.ToInt32(hfCustomerID.Value));
        //    btnCredit.Text = "Выбрать клиента";
        //}

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
            pnlNewRequest.Visible = false;
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
                //refreshGrid();
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
                    note = hfNote.Value, //tbNote.Text,
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
                int agentID = 7578;
                if (lst5.GroupID != 110)
                {
                    agentID = Convert.ToInt32(Session["UserID"].ToString());
                }

                lst5.RequestStatus = "Утверждено";
                dbRWZ.Requests.Context.SubmitChanges();
                System.Threading.Thread.Sleep(1000);
                //refreshGrid();
                string hexGreen = "#7cfa84";
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "Утверждено";
                /*RequestHistory*/
                DateTime dateTimeNow = dateNow;
                RequestsHistory newItem = new RequestsHistory()
                {
                    AgentID = agentID, //Convert.ToInt32(Session["UserID"].ToString()),
                    CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "Утверждено",
                    note = hfNote.Value, //tbNote.Text,
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
                //refreshGrid();
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
                    note = hfNote.Value, //tbNote.Text,
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

        //protected void lbSettings_Click(object sender, EventArgs e)
        //{
        //    dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        //    int usrID = Convert.ToInt32(Session["UserID"].ToString());
        //    int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
        //    int? shopID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().ShopID;
        //    int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
        //    var groupsOrg = dbRWZ.Groups.Where(r => r.OrgID == 7).ToList(); // Мой Телефон
        //    var groupOrg = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupName;

        //    var shopName = (shopID != null) ? dbRWZ.Shops.Where(r => r.ShopID == shopID).FirstOrDefault().ShopName : "";


        //    if (pnlSettings.Visible == true)
        //    { pnlSettings.Visible = false; lblMsgPassword.Visible = false; lblMsgPassword.Text = ""; }
        //    else
        //    {
        //        pnlSettings.Visible = true; lblMsgPassword.Text = "";
        //        if (orgID == 7) // Только для Мой Телефон
        //        {

        //            ddlGroup.DataSource = groupsOrg;
        //            ddlGroup.DataTextField = "GroupName";
        //            ddlGroup.DataValueField = "GroupID";
        //            ddlGroup.DataBind();
        //            ddlGroup.SelectedIndex = ddlGroup.Items.IndexOf(ddlGroup.Items.FindByText(groupOrg));

        //            ddlShop.DataSource = dbRWZ.Shops.ToList();
        //            ddlShop.DataTextField = "ShopName";
        //            ddlShop.DataValueField = "ShopID";
        //            ddlShop.DataBind();
        //            ddlShop.SelectedIndex = ddlShop.Items.IndexOf(ddlShop.Items.FindByText(shopName));
        //            pnlGroup.Visible = true;
        //        }
        //        else pnlGroup.Visible = false;
        //    }
        //}

        //protected void btnCancelPassword_Click(object sender, EventArgs e)
        //{
        //    pnlSettings.Visible = false;
        //}

        //protected void btnSavePassword_Click(object sender, EventArgs e)
        //{
        //    var psw = tbOldPassword.Text;
        //    var sha1 = Crypto.SHA1(psw).ToLower();
        //    string usrLogin = Session["UserName"].ToString();
        //    dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        //    Users2 usr = dbRWZ.Users2s.Where(u => (u.UserName == usrLogin) && (u.Password == sha1)).FirstOrDefault();
        //    if (tbNewPassword.Text == tbConfirmPassword.Text)
        //    {
        //        if (usr != null)
        //        {
        //            usr.Password = Crypto.SHA1(tbNewPassword.Text).ToLower();
        //            usr.GroupID = Convert.ToInt32(ddlGroup.SelectedItem.Value);
        //            dbRWZ.Users2s.Context.SubmitChanges();
        //            lblMsgPassword.Text = "Пароль успешно изменен";
        //            lblMsgPassword.Visible = true;
        //            pnlSettings.Visible = false;
        //        }
        //        else
        //        {
        //            lblMsgPassword.Text = "Неправильный пароль";
        //            lblMsgPassword.Visible = true;
        //        }
        //    }
        //    else { lblMsgPassword.Text = "Ваш новый пароль и подтверждение пароля не совпадают"; lblMsgPassword.Visible = true; }
        //}


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
            //refreshGrid();
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
                note = hfNote.Value, //tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
            pnlNewRequest.Visible = false;
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
            //tbNote.Text = tbNoteCancelReq.Text;
            hfNote.Value = tbNoteCancelReq.Text;
            string strOB = "";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.GroupID == 110)
            {
                //********меняем статус в ОБ
                //await PostOnIssue(lst.CustomerID.ToString(), lst.CreditID.ToString());
                //********отправляем статус в нуртелеком

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
                    MsgBox(strOB + " " + ex.ToString(), this.Page, this);
                    //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                }
                finally
                {

                }
                if (strOB == "200")
                {

                }
                else
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(" ");
                    MsgBox(strOB, this.Page, this);
                    //System.Windows.Forms.MessageBox.Show(strOB);
                }
                //********отправляем статус в билайн
                //string str = await SendStatusBee(lst.CreditID.ToString(), "REFUSED_BY_CLIENT");

                //if (str == "200")
                {
                    CancelRequest();
                }
                //else
                //{
                //    //****//  DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статусов в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //    //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статусов в Билайн");
                //    MsgBox("Ошибка при отправке статусов в Билайн", this.Page, this);
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
                    //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
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
                    //System.Windows.Forms.MessageBox.Show(" ");
                    MsgBox("Ошибка при отправке статуса к ОБ", this.Page, this);
                    //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса к ОБ");
                }

                //CancelRequest();
            }
            pnlNewRequest.Visible = false;

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
            //refreshGrid();
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
                note = hfNote.Value, //tbNote.Text,
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
            if (lst.OrgID == 17) // МТ c сайта
            {
                //SendMail2(body, to, efrom, replyto, subject);
                try
                {
                    int reqID = dbRWZ.Requests2s.Where(r => r.RequestID2 == lst.RequestID).FirstOrDefault().RequestID;
                    strOB = await SendStatusMT(reqID.ToString(), "Отменено");
                }
                catch (Exception ex)
                {
                    //*****//  DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                    MsgBox(strOB + " " + ex.ToString(), this.Page, this);
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
            //refreshGrid();
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
                note = hfNote.Value, //tbNote.Text,
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

        }

        protected void chbEmployer_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEmployer.Checked) { pnlEmployment.Visible = false; }
            else { pnlEmployment.Visible = true; }
        }

        protected async void btnCancelReqExp_Click(object sender, EventArgs e)
        {
            //tbNote.Text = tbNoteCancelReqExp.Text;
            hfNote.Value = tbNoteCancelReqExp.Text;
            //********
            string strOB = "";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst.GroupID == 110)
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
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(" ");
                    MsgBox("", this.Page, this);
                }

                //********отправляем статус в билайн
                //var result = await SendStatusBee(lst.CreditID.ToString(), "DECLINED");
                //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                //var data = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject(result));
                //dynamic data = JObject.Parse(str);
                //var data = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                //Results data = JsonConvert.DeserializeObject<Results>(result);
                //var data = await Task.Run(() => Newtonsoft.Json.JsonConvert.DeserializeObject(result));

                //var list = await Task.Run(() => JsonConvert.DeserializeObject<List<MyObject>>(response.Content));
                //string str = data.statusCode.ToString();
                //if (result == "200")
                //if ((result == "200") && (strOB == "200"))
                {
                    AddRowToJournal(lst, "Отказано");
                    CancelRequestExp();
                }
                //else
                //{
                //    //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статусов в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
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
                }
                else
                {
                    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(" ");
                    MsgBox(strOB, this.Page, this);
                }
                //CancelRequestExp();
            }
            pnlNewRequest.Visible = false;
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
            //refreshGrid();
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
                note = hfNote.Value, //tbNote.Text,
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
            if (lst.OrgID == 17) // МТ Онлайн с сайта
            {
                //SendMail2(body, to, efrom, replyto, subject);
                try
                {
                    int reqID = dbRWZ.Requests2s.Where(r => r.RequestID2 == lst.RequestID).FirstOrDefault().RequestID;
                    strOB = await SendStatusMT(reqID.ToString(), "Отказано");
                }
                catch (Exception ex)
                {
                    //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                    //System.Windows.Forms.MessageBox.Show(strOB + " " + ex.ToString());
                    MsgBox(strOB + " " + ex.ToString(), this.Page, this);
                }
                finally
                {
                }
            }
            //Отправка сообщ для МТ-Онлайн }


        }

        //protected void gvRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvRequests.PageIndex = e.NewPageIndex;
        //    gvRequests.DataBind();
        //    //refreshGrid();
        //    hfRequestsRowID.Value = "";
        //}

        //protected void btnForPeriod_Click(object sender, EventArgs e)
        //{
        //    Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
        //    Session["RoleID"] = Convert.ToInt32(Session["RoleID"]);
        //    Session["tbDate1"] = tbDate1b.Text;
        //    Session["tbDate2"] = tbDate2b.Text;
        //    Session["i0"] = chkbxlistStatus.Items[0].Selected;
        //    Session["i1"] = chkbxlistStatus.Items[1].Selected;
        //    Session["i2"] = chkbxlistStatus.Items[2].Selected;
        //    Session["i3"] = chkbxlistStatus.Items[3].Selected;
        //    Session["i4"] = chkbxlistStatus.Items[4].Selected;
        //    Session["i5"] = chkbxlistStatus.Items[5].Selected;
        //    Session["i6"] = chkbxlistStatus.Items[6].Selected;
        //    Session["i7"] = chkbxlistStatus.Items[7].Selected;
        //    Session["i8"] = chkbxlistStatus.Items[8].Selected;
        //    Session["i9"] = chkbxlistStatus.Items[9].Selected;
        //    Session["i10"] = chkbxlistStatus.Items[10].Selected;
        //    Session["i11"] = chkbxlistStatus.Items[11].Selected;
        //    Session["i12"] = chkbxlistStatus.Items[12].Selected;
        //    Session["k0"] = chkbxlistKindActivity.Items[0].Selected;
        //    Session["k1"] = chkbxlistKindActivity.Items[1].Selected;
        //    Session["k2"] = chkbxlistKindActivity.Items[2].Selected;
        //    Session["g1"] = chkbxGroup.Items[0].Selected;
        //    Session["g2"] = chkbxGroup.Items[1].Selected;
        //    //Session["g3"] = chkbxGroup.Items[2].Selected;
        //    Session["o0"] = chkbxOffice.Items[0].Selected;
        //    Session["o1"] = chkbxOffice.Items[1].Selected;
        //    Session["o2"] = chkbxOffice.Items[2].Selected;
        //    Session["o3"] = chkbxOffice.Items[3].Selected;
        //    Session["o4"] = chkbxOffice.Items[4].Selected;
        //    Session["o5"] = chkbxOffice.Items[5].Selected;
        //    Session["o6"] = chkbxOffice.Items[6].Selected;
        //    Session["o7"] = chkbxOffice.Items[7].Selected;
        //    Session["o8"] = chkbxOffice.Items[8].Selected;
        //    Session["o9"] = chkbxOffice.Items[9].Selected;
        //    Response.Redirect("rptForPeriod", true);
        //}

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
            //refreshGrid();
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
                note = hfNote.Value, //tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
            pnlNewRequest.Visible = false;
        }

        protected async void btnApproved_Click(object sender, EventArgs e)
        {
            string strMT = "200";
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            var cust = dbR.Customers.Where(r => r.CustomerID == lst.CustomerID).ToList().FirstOrDefault();

            if ((lst.RequestStatus == "Новая заявка") || (lst.RequestStatus == "Сделано") || (lst.RequestStatus == "Исправить") || (lst.RequestStatus == "Исправлено") || (lst.RequestStatus == "Подтверждено"))
            {

                if (Page.IsValid)
                {
                    //DateTime DocumentValidTill = Convert.ToDateTime(cust.DocumentValidTill); //Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4));
                    //string DateOfBirth = tbDateOfBirth.Text.Substring(0, 2) + (tbDateOfBirth.Text.Substring(3, 2) + tbDateOfBirth.Text.Substring(6, 4));
                    //string DateOfBirthINN = (tbIdentificationNumber.Text.Substring(1, 8)); bool f = true;
                    //if (DocumentValidTill.Date < System.DateTime.Now.Date.AddDays(1)) { MsgBox("Паспорт проссрочен", this.Page, this); f = false; }
                    //if (DateOfBirth != DateOfBirthINN) { MsgBox("Неправильно введены паспортные данные", this.Page, this); f = false; }
                    bool f = true;
                    if (f)
                    {
                        //if (lst.GroupID == 110)
                        {

                            int age = GetCustomerAge(Convert.ToInt32(hfCustomerID.Value.ToString()));
                            if ((age > 22) && (age < 66))
                            {
                                f = true;
                            }
                            else
                            {
                                f = false;
                                //customerID = -3;
                                //MsgBox("Выдача кредита не возможна, возрастное ограничение 23-65, клиенту " + age.ToString() + " лет", this.Page, this);
                            }

                            if (f)
                            {
                                bool fs = scor();
                                if (fs == true)
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
                                            //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса Утверждено в АБС", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                                            //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса Утверждено в АБС");
                                            MsgBox("Ошибка при отправке статуса Утверждено к АБС", this.Page, this);

                                        }
                                    }
                                    //********меняем статус в ОБ Утверждено*****End//

                                    //********отправляем статус в билайн Begin
                                    //string strNur = await SendStatusBee(lst.CreditID.ToString(), "APPROVED");
                                    //if (strNur == "200")
                                    //{
                                    //    //ApprovedRequest(); //Утверждаем если даже в ОБ не отвечает
                                    //}
                                    //else
                                    //{
                                    //    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED в Билайн", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                                    //    //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса APPROVED в Билайн");
                                    //    MsgBox("Ошибка при отправке статуса APPROVED в Билайн", this.Page, this);

                                    //}
                                    //************отправляем статус в билайн End
                                    if ((strOB == "200")) { VerificationRequest(); ApprovedRequest(); }


                                }
                                else
                                {
                                    VerificationRequest();
                                    btnCancelReqExp_Click(new object(), new EventArgs());
                                    AddRowToJournal(lst, "Отказано");
                                }
                            }
                            else
                            {
                                MsgBox("Выдача кредита не возможна, возрастное ограничение 23-65, клиенту " + age.ToString() + " лет", this.Page, this);
                            }







                            //}
                            //else
                            ////Отправка сообщ для МТ-Онлайн 
                            //if (lst.OrgID == 17) // МТ 
                            //{
                            //    try
                            //    {
                            //        int reqID = dbRWZ.Requests2s.Where(r => r.RequestID2 == lst.RequestID).FirstOrDefault().RequestID;
                            //        strMT = await SendStatusMT(reqID.ToString(), "Утверждено");
                            //        //if (strOB == "200")
                            //        ApprovedRequest();
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        //otNetNuke.UI.Skins.Skin.AddModuleMessage(this, strMT + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //        //System.Windows.Forms.MessageBox.Show(strMT + " " + ex.ToString());
                            //        MsgBox(strMT + " " + ex.ToString(), this.Page, this);
                            //    }
                            //    finally
                            //    {
                            //    }
                            //}
                            ////Отправка сообщ для МТ-Онлайн 
                            //else
                            //{
                            //ApprovedRequest();
                            if (lst.TypeOfIssue == 1)
                            createCardRequest((int)lst.RequestID, (int)lst.AgentID, (int)lst.AgentRoleID, (int)lst.BranchID, (int)lst.OfficeID, lst.CardNumber);
                        }
                    }
                }

            }
            //pnlNewRequest.Visible = false;
            Response.Redirect("/Microcredit/Loans.aspx");
        }


        private void createCardRequest(int reqID, int agentID, int agentRoleID, int branchID, int officeID, string cardNumber)
        {

            DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now);
            СreditСonveyor.Data.Card.CreditController ctx = new СreditСonveyor.Data.Card.CreditController();
            CardRequest newRequest = new CardRequest
            {
                //CardN = tbCardN.Text,
                CardN = cardNumber,
                //AccountN = tbAccountN.Text,
                AccountN = "",
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                Surname = tbSurname2.Text,
                CustomerName = tbCustomerName2.Text,
                Otchestvo = tbOtchestvo2.Text,
                //IdentificationNumber = tbINN2.Text,
                IdentificationNumber = lblINN.Text,
                RequestDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                AgentID = agentID,
                AgentRoleID = agentRoleID,
                AgentUsername = Session["UserName"].ToString(),
                AgentFirstName = Session["FIO"].ToString(),
                AgentLastName = Session["FIO"].ToString(),
                RequestStatus = "Подтверждено",
                //CreditID = CreditsHistoriesID,
                BranchID = branchID,
                GroupID = 9,
                OrgID = 1,
                StatusDate = dateTimeNow,
                OfficeID = officeID,
                CreditRequestID = reqID

            };
            СreditСonveyor.Data.Card.ItemController ctl = new СreditСonveyor.Data.Card.ItemController();
            int requestID = ctl.ItemRequestAddItem(newRequest);
            /*RequestHistory*//*----------------------------------------------------*/
            CardRequestsHistory newItem = new CardRequestsHistory()
            {
                AgentID = agentID,
                //CreditID = CreditsHistoriesID,
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow,
                Status = "Подтверждено",
                note = "Для кредита:" + reqID,
                RequestID = requestID
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var requestFiles = (from v in dbRWZ.RequestsFiles where (v.RequestID == reqID) select v).ToList();
            foreach(var reqFile in requestFiles)
            {

                CardRequestsFile newRequestFile = new CardRequestsFile
                {
                    Name = reqFile.Name,
                    RequestID = requestID,
                    ContentType = reqFile.ContentType,
                    //Data = bytes,
                    //FullName = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + fullfilename,
                    //FullName = "\\Portals\\0\\" + partnerdir + "\\" + fullfilename,
                    //FullName2 = "https://credit.doscredobank.kg\\" + "Portals\\0\\" + partnerdir + "\\" + fullfilename,
                    FullName = reqFile.FullName,
                    FullName2 = reqFile.FullName2,
                    FileDescription = reqFile.FileDescription
                    //IsPhoto = false
                };

                СreditСonveyor.Data.Card.ItemController ctl2 = new СreditСonveyor.Data.Card.ItemController();
                ctl2.ItemRequestFilesAddItem(newRequestFile);


            }
            
        }

        private void AddRowToJournal(Request req, string status)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.Requests where (v.RequestID == req.RequestID) select v).ToList().FirstOrDefault();
            var usr = dbRWZ.Users2s.Where(z => z.UserID == Convert.ToInt32(hfUserID.Value)).FirstOrDefault();
            int N1 = 0, N2 = 0;
            if (lst != null)
            {
                var dateNow = DateTime.Now.Date;
                //var yestedey = DateTime.Now.AddDays(-1).Date;


                var jourAll = (from v in dbRWZ.JournalNanoBeeline orderby v.DateVerif select v).ToList();
                var jourNow = (from v in dbRWZ.JournalNanoBeeline where (v.DateVerif == dateNow) orderby v.DateVerif select v).ToList();

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

                JournalNanoBeeline jourCurr = (from v in dbRWZ.JournalNanoBeeline where v.RequestID == req.RequestID select v).FirstOrDefault();
                if (jourCurr != null)
                {
                    //JournalNano Item = new JournalNano();
                    ItemController ctlItem = new ItemController();
                    //Item = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    //Item = jourCurr.FirstOrDefault();
                    jourCurr.Status = status;
                    ctlItem.ItemJournalUpd(jourCurr);
                }
                else
                {
                    if (lst.RequestStatus == "Утверждено")
                    {
                        JournalNanoBeeline newItem = new JournalNanoBeeline()
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
                        itm.ItemJournalAdd(newItem);
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
                //refreshGrid();
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
                    note = hfNote.Value, //tbNote.Text,
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

        public bool scor()
        {
            //dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            //var requests = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList().FirstOrDefault();
            //double AverageMonthSalary, OtherLoans, chp, cho, y1, y2;
            //double s = Convert.ToDouble(requests.RequestSumm);
            //double n = Convert.ToDouble(requests.RequestPeriod);
            //double stavka = Convert.ToDouble(requests.RequestRate);
            //double i, k = 0;
            //int y22 = 40;
            //i = (stavka != 0) ? stavka / 12 / 100 : 0;
            //if ((stavka == 0)) k = s / n;
            //if (stavka != 0) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            //k = 5500;
            //bool f = false, b1, b2;

            //{
            //    var zp = Convert.ToDouble(RadNumTbAverageMonthSalary.Text);
            //    if ((zp > 50000) || (zp == 50000)) y22 = 50;
            //    if (zp < 50000) y22 = 40;
            //    OtherLoans = Convert.ToDouble(requests.OtherLoans);//Convert.ToDouble(RadNumOtherLoans);
            //    chp = (zp + Convert.ToDouble(requests.AdditionalIncome)) - 7000;
            //    cho = chp - k;
            //    cho = cho - OtherLoans;
            //    y1 = 100 * cho / chp;
            //    if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
            //    y2 = 100 * (k + OtherLoans) / (zp + Convert.ToDouble(requests.AdditionalIncome));

            //    if ((y1 >= 20) && (y2 < y22)) f = true;
            //    if (y1 >= 20) b1 = true; //lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
            //    else b1 = false; //lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
            //    if (y2 < y22) b2 = true; // lblIssuanceOfCredit2.Text = "да, выдача кредита возможно";
            //    else b2 = false; // lblIssuanceOfCredit2.Text = "нет, выдача кредита невозможно";
            //}
            //if (b1 && b2) return true;
            //else return false;

            double i, k = 0;
            double s = Convert.ToDouble(RadNumTbRequestSumm.Text);
            double n = Convert.ToDouble(ddlRequestPeriod.Text);
            double stavka = Convert.ToDouble(ddlRequestRate.SelectedItem.Value);
            //double i = stavka / 12 / 100;
            i = (stavka != 0) ? stavka / 12 / 100 : 0;
            if (stavka == 0) k = s / n;
            if (stavka == 30.00) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            int f = 0; int y22 = 50;
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
                //if ((zp > 50000) || (zp == 50000)) y22 = 50;
                //if (zp < 50000) y22 = 40;

                if ((y1 >= 40) && (y2 < y22)) f = 1;
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
                //if ((valp > 50000) || (valp == 50000)) y22 = 50;
                //if (valp < 50000) y22 = 40;
                if (chp > 0)
                {
                    double cho = chp - k - OtherLoans;
                    double y1 = 100 * cho / chp;
                    if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                    double y2 = 100 * (k + OtherLoans) / (valp + additionalIncome);
                    if ((y1 >= 40) && (y2 < y22)) f = 1;
                }
                if (stavka == 0) { if (s > 100000) f = -100000; }
                else { if (s > 100000) f = -100000; }
            }
            if (f == 1) return true;
            else return false;
        }



        //public async System.Threading.Tasks.Task<string> SendStatusBee(string CreditID, string Status)
        //{
        //    ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            var byteArray = Encoding.ASCII.GetBytes(connectionStringBeeKey);
        //            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            //string json = "{\"state\": \"ISSUED_BY\"}";
        //            string json = "{\"state\": \"" + Status + "\"}";
        //            //var response = await client.PostAsync("https://umai-stage.balance.kg/mcm-api/dos/applications/10001/status" + CreditID, new StringContent(json, Encoding.UTF8, "application/json"));
        //            //var response = await client.PostAsync("https://umai-stage.balance.kg/mcm-api/dos/applications/10001/status", new StringContent(json, Encoding.UTF8, "application/json"));
        //            var response = await client.PostAsync(connectionStringBee + CreditID + "/status", new StringContent(json, Encoding.UTF8, "application/json"));

        //            var result = await response.Content.ReadAsStringAsync();
        //            JObject obj = JObject.Parse(result);
        //            string status = obj["status"].ToString();
        //            if (response.StatusCode == HttpStatusCode.OK)
        //            {
        //                if (status == "SUCCESS")
        //                    result = "200";
        //            }
        //            //var status = result.Where;
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //TextBox1.Text = TextBox1.Text + ex.Message;
        //        return "error";
        //    }
        //    finally
        //    {
        //        //TextBox1.Text = TextBox1.Text + Response.ToString();
        //        //return "";
        //    }
        //}

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
                //****//DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
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
                //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
                return "error";

            }
            finally
            {

            }
        }


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
                            //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strOB + " " + ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
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
                            //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса На выдаче в АБС", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                            //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса На выдаче в АБС");
                            MsgBox("Ошибка при отправке статуса На выдаче к АБС", this.Page, this);

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
                        //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Ошибка при отправке статуса APPROVED в Нуртелеком", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        //System.Windows.Forms.MessageBox.Show("Ошибка при отправке статуса APPROVED в Нуртелеком");
                        MsgBox("Ошибка при отправке статуса APPROVED к Нуртелеком", this.Page, this);

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
                //refreshGrid();
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
                    note = hfNote.Value, //tbNote.Text,
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
                        //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, result3, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                        //System.Windows.Forms.MessageBox.Show(result3);
                        MsgBox(result3, this.Page, this);

                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TextBox1.Text = TextBox1.Text + ex.Message;
                //*****// DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
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
                note = hfNote.Value, //tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            //refreshGrid();
            /******************************************************/
        }

        //protected void btnForPeriodWithHistory_Click(object sender, EventArgs e)
        //{
        //    Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
        //    Session["RoleID"] = Convert.ToInt32(Session["RoleID"]);
        //    Session["tbDate1"] = tbDate1b.Text;
        //    Session["tbDate2"] = tbDate2b.Text;
        //    Session["i0"] = chkbxlistStatus.Items[0].Selected;
        //    Session["i1"] = chkbxlistStatus.Items[1].Selected;
        //    Session["i2"] = chkbxlistStatus.Items[2].Selected;
        //    Session["i3"] = chkbxlistStatus.Items[3].Selected;
        //    Session["i4"] = chkbxlistStatus.Items[4].Selected;
        //    Session["i5"] = chkbxlistStatus.Items[5].Selected;
        //    Session["i6"] = chkbxlistStatus.Items[6].Selected;
        //    Session["i7"] = chkbxlistStatus.Items[7].Selected;
        //    Session["i8"] = chkbxlistStatus.Items[8].Selected;
        //    Session["i9"] = chkbxlistStatus.Items[9].Selected;
        //    Session["i10"] = chkbxlistStatus.Items[10].Selected;
        //    Session["i11"] = chkbxlistStatus.Items[11].Selected;
        //    Session["i12"] = chkbxlistStatus.Items[12].Selected;
        //    Session["k0"] = chkbxlistKindActivity.Items[0].Selected;
        //    Session["k1"] = chkbxlistKindActivity.Items[1].Selected;
        //    Session["k2"] = chkbxlistKindActivity.Items[2].Selected;
        //    Session["g1"] = chkbxGroup.Items[0].Selected;
        //    Session["g2"] = chkbxGroup.Items[1].Selected;
        //    //Session["g3"] = chkbxGroup.Items[2].Selected;
        //    Session["o0"] = chkbxOffice.Items[0].Selected;
        //    Session["o1"] = chkbxOffice.Items[1].Selected;
        //    Session["o2"] = chkbxOffice.Items[2].Selected;
        //    Session["o3"] = chkbxOffice.Items[3].Selected;
        //    Session["o4"] = chkbxOffice.Items[4].Selected;
        //    Session["o5"] = chkbxOffice.Items[5].Selected;
        //    Session["o6"] = chkbxOffice.Items[6].Selected;
        //    Session["o7"] = chkbxOffice.Items[7].Selected;
        //    Session["o8"] = chkbxOffice.Items[8].Selected;
        //    Session["o9"] = chkbxOffice.Items[9].Selected;
        //    Response.Redirect("rptForPeriodWithHistory", true);
        //}





        //protected void btnForPeriodWithProducts_Click(object sender, EventArgs e)
        //{
        //    Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
        //    Session["RoleID"] = Convert.ToInt32(Session["RoleID"]);
        //    Session["tbDate1"] = tbDate1b.Text;
        //    Session["tbDate2"] = tbDate2b.Text;
        //    Session["i0"] = chkbxlistStatus.Items[0].Selected;
        //    Session["i1"] = chkbxlistStatus.Items[1].Selected;
        //    Session["i2"] = chkbxlistStatus.Items[2].Selected;
        //    Session["i3"] = chkbxlistStatus.Items[3].Selected;
        //    Session["i4"] = chkbxlistStatus.Items[4].Selected;
        //    Session["i5"] = chkbxlistStatus.Items[5].Selected;
        //    Session["i6"] = chkbxlistStatus.Items[6].Selected;
        //    Session["i7"] = chkbxlistStatus.Items[7].Selected;
        //    Session["i8"] = chkbxlistStatus.Items[8].Selected;
        //    Session["i9"] = chkbxlistStatus.Items[9].Selected;
        //    Session["i10"] = chkbxlistStatus.Items[10].Selected;
        //    Session["i11"] = chkbxlistStatus.Items[11].Selected;
        //    Session["i12"] = chkbxlistStatus.Items[12].Selected;
        //    Session["k0"] = chkbxlistKindActivity.Items[0].Selected;
        //    Session["k1"] = chkbxlistKindActivity.Items[1].Selected;
        //    Session["k2"] = chkbxlistKindActivity.Items[2].Selected;
        //    Session["g1"] = chkbxGroup.Items[0].Selected;
        //    Session["g2"] = chkbxGroup.Items[1].Selected;
        //    //Session["g3"] = chkbxGroup.Items[2].Selected;
        //    Session["o0"] = chkbxOffice.Items[0].Selected;
        //    Session["o1"] = chkbxOffice.Items[1].Selected;
        //    Session["o2"] = chkbxOffice.Items[2].Selected;
        //    Session["o3"] = chkbxOffice.Items[3].Selected;
        //    Session["o4"] = chkbxOffice.Items[4].Selected;
        //    Session["o5"] = chkbxOffice.Items[5].Selected;
        //    Session["o6"] = chkbxOffice.Items[6].Selected;
        //    Session["o7"] = chkbxOffice.Items[7].Selected;
        //    Session["o8"] = chkbxOffice.Items[8].Selected;
        //    Session["o9"] = chkbxOffice.Items[9].Selected;
        //    Response.Redirect("rptForPeriodWithProducts", true);
        //}

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[-a-zA-Z0-9][-.a-zA-Z0-9]*@[-.a-zA-Z0-9]+(\.[-.a-zA-Z0-9]+)*\.(com|edu|info|gov|int|mil|net|org|biz|name|museum|coop|aero|pro|tv|[a-zA-Z]{2})$";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = false;
            if (string.IsNullOrEmpty(email)) valid = false;
            else valid = check.IsMatch(email);
            return valid;
        }

        //protected void btnSaveGroup_Click(object sender, EventArgs e)
        //{
        //    dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        //    int usrID = Convert.ToInt32(Session["UserID"].ToString());
        //    var user2 = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault();
        //    user2.GroupID = Convert.ToInt32(ddlGroup.SelectedItem.Value);
        //    user2.ShopID = Convert.ToInt32(ddlShop.SelectedItem.Value);
        //    dbRWZ.Users2s.Context.SubmitChanges();
        //    lblSHop.Text = "Данные успешно сохранены";
        //}

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

        //protected void btnGetScoreBee_Click(object sender, EventArgs e)
        //{
        //    int CustID = Convert.ToInt32(hfCustomerID.Value);
        //    string phone = "", lnk = "";
        //    SysController stx = new SysController();
        //    Customer cust = stx.CustomerGetItem(CustID);
        //    if (cust.ContactPhone1.Length > 14)
        //    {
        //        string q = cust.ContactPhone1;
        //        phone = "0" + cust.ContactPhone1.Substring(5, 3) + cust.ContactPhone1.Substring(9, 6);
        //        lnk = "http://beeline-income.beeline.kg/api/customer/income?number=" + phone;
        //        //string s = getScoreBee(phone);
        //        //cust.ContactPhone1 = tbContactPhone.Text;
        //        //ctx.CustomerUpdItem(cust);

        //        string s, s2 = "";
        //        try
        //        {
        //            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://beeline-income.beeline.kg/api/customer/income?number=0772944431");
        //            var httpWebRequest = (HttpWebRequest)WebRequest.Create(lnk);

        //            httpWebRequest.ContentType = "application/json";
        //            httpWebRequest.Method = "GET";

        //            //            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
        //            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //            {
        //                var response = streamReader.ReadToEnd();
        //                var result = JsonConvert.DeserializeObject<Resp>(response);
        //                if (result.response != null) s = result.response.revenue_score.ToString();
        //                else s = result.status_message;

        //                if ((s == "A") || (s == "А")) s2 = "A, личный доход свыше 28 000 сом в месяц";
        //                if ((s == "B") || (s == "В") || (s == "Б")) s2 = "B, личный доход 20 001 - 28 000 сом в месяц";
        //                if ((s == "C") || (s == "С")) s2 = "C, личный доход 16 001 - 20 000 сом в месяц";
        //                if ((s == "D") || (s == "Д")) s2 = "D, личный доход 10 000 – 16 000 сом в месяц";
        //                if ((s == "E") || (s == "Е")) s2 = "E, личный доход до 10 000 сом в месяц";
        //                if (s == "0") s2 = "0, Нет личного дохода";

        //                int usrID = Convert.ToInt32(Session["UserID"].ToString());
        //                DateTime dateTimeNow = dateNow;
        //                /**/
        //                /*RequestHistory*//*----------------------------------------------------*/
        //                CreditController ctx = new CreditController();
        //                RequestsHistory newItem = new RequestsHistory()
        //                {
        //                    AgentID = usrID,
        //                    CreditID = Convert.ToInt32(hfCreditID.Value),
        //                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
        //                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
        //                    Status = s2,
        //                    note = cust.ContactPhone1,
        //                    RequestID = Convert.ToInt32(hfRequestID.Value)
        //                };
        //                ctx.ItemRequestHistoriesAddItem(newItem);
        //                //refreshGrid();

        //            }

        //        }
        //        catch (WebException ex)
        //        {
        //            s = ex.Message;
        //        }

        //    }
        //}



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
            //refreshGrid();
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
                note = hfNote.Value, //tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
            pnlNewRequest.Visible = false;
            Response.Redirect("/Microcredit/Loans");
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
            //refreshGrid();
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
                note = hfNote.Value, //tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
            pnlNewRequest.Visible = false;
            Response.Redirect("/Microcredit/Loans");
        }

        //protected void chkbxSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkbxSelectAll.Checked)
        //    {
        //        for (var i = 0; i < 13; i++)
        //        {
        //            chkbxlistStatus.Items[i].Selected = true;
        //        }
        //    }
        //    else
        //    {
        //        for (var i = 0; i < 13; i++)
        //        {
        //            chkbxlistStatus.Items[i].Selected = false;
        //        }
        //    }
        //}

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProductIndChg();
        }

        public void ddlProductIndChg()
        {
            if (ddlProduct.SelectedValue == "МК потреб кредит")
            {
                ddlRequestRate.Items.Clear();
                ddlRequestRate.Items.Add("29,90");
            }
            else
            {
                //ddlRequestRate.Items.Clear();
                //ddlRequestRate.Items.Add("0,00");
            }
            databindDDLRatePeriod();

        }

        protected void btnProfileNano_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Response.Redirect("rptProfileNano", true);
        }

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
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                MsgBox(ex.ToString(), this.Page, this);
                //return "error";

            }
            finally
            {
                ////TextBox1.Text = TextBox1.Text + Response.ToString();

            }
        }

        protected void btnSozfondAgree_Click(object sender, EventArgs e)
        {
            Session["RequestID"] = hfRequestID.Value;
            Response.Redirect("rptConsentSozfond", true);
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
            req.ContactPhone1 = cust.ContactPhone1;
            dbRWZ.Requests.Context.SubmitChanges();
            //refreshGrid(); 
            pnlNewRequest.Visible = false;
        }

        protected void btnSaveOffice_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
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
                ActualDate = Convert.ToDateTime(actdate), //Convert.ToDateTime(tbActualDate.Text),
                IncomesStructures = new List<GeneralController.IncomesStructure>(),
            };

            GeneralController.Picture picture = new GeneralController.Picture()
            {
                //FileName = "https://credit.doscredobank.kg/Portals/0/Credits/Nurcredits/2021/10/28/okmasro4/Screenshot_1.jpg",
                //ChangeDate = Convert.ToDateTime(actdate),
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
            //    StartDate = Convert.ToDateTime(actdate"),
            //    //EndDate = 
            //    Status = 1,
            //};



            dynamic dRequest = new System.Dynamic.ExpandoObject();
            //dynRoot.Name = "Tom";
            //dynRoot.Age = 46;



            //GeneralController.Root root = new GeneralController.Root()
            {

                dRequest.CustomerID = Convert.ToInt32(hfCustomerID.Value);
                dRequest.ProductID = 1185;

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

                dRequest.CreditPurpose = "Потребительская";
                dRequest.LoanPurposeTypeID = 2;



            }


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

        //protected void chkbxSelectAllOffice_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkbxSelectAllOffice.Checked)
        //    {
        //        for (var i = 0; i < 10; i++)
        //        {
        //            chkbxOffice.Items[i].Selected = true;
        //        }
        //    }
        //    else
        //    {
        //        for (var i = 0; i < 10; i++)
        //        {
        //            chkbxOffice.Items[i].Selected = false;
        //        }
        //    }
        //}

        protected void ddlRequestRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            databindDDLRatePeriod();

        }

        protected void gvRequestsFiles_DataBound(object sender, EventArgs e)
        {

        }



        protected void tbINNOrg_TextChanged(object sender, EventArgs e)
        {
            if (tbIdentificationNumber.Text.Length > 13)
            {
                if (tbIdentificationNumber.Text.Substring(0, 1).ToUpper() == "1") { rbtSex.SelectedIndex = 1; }
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

        protected void tbIdentificationNumber_TextChanged(object sender, EventArgs e)
        {
            if (tbIdentificationNumber.Text.Length > 13)
            {
                if (tbIdentificationNumber.Text.Substring(0, 1).ToUpper() == "1") { rbtSex.SelectedIndex = 1; }
                if (tbIdentificationNumber.Text.Substring(0, 1).ToUpper() == "2") { rbtSex.SelectedIndex = 0; }
            }
        }

        //protected void btnCancelSaveGroup_Click(object sender, EventArgs e)
        //{
        //    pnlSettings.Visible = false;
        //}

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

        protected void btnPhoto_Click(object sender, EventArgs e)
        {
            //if (pnlPhoto.Visible == true) pnlPhoto.Visible = false;
            //else pnlPhoto.Visible = true;
            pnlPhoto.Visible = true;
            hfPhoto2.Value = "";
            Image1.ImageUrl = "";
            btnPhoto.Text = "Фото клиента";
            btnPhotoClose.Visible = true;
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



        protected void chkbxTypeOfCollateral_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            if (chkbxTypeOfCollateral.Items[0].Selected == true) 
            {
                chkbxTypeOfCollateral.Items[0].Enabled = true;
                chkbxTypeOfCollateral.Items[1].Enabled = false;
                chkbxTypeOfCollateral.Items[2].Enabled = false;
            }
            else
            {
                chkbxTypeOfCollateral.Items[0].Enabled = true;
                chkbxTypeOfCollateral.Items[1].Enabled = true;
                chkbxTypeOfCollateral.Items[2].Enabled = true;
            }

            if ((chkbxTypeOfCollateral.Items[1].Selected == false) &&
                (chkbxTypeOfCollateral.Items[2].Selected == false))
            {
                chkbxTypeOfCollateral.Items[0].Enabled = true;
            }

            if (chkbxTypeOfCollateral.Items[1].Selected == true) 
            { 
                pnlGuarantor.Visible = true;
                chkbxTypeOfCollateral.Items[0].Enabled = false;
                chkbxTypeOfCollateral.Items[1].Enabled = true;
                chkbxTypeOfCollateral.Items[2].Enabled = true;
            } 
            else 
            { 
                pnlGuarantor.Visible = false;
                tbGuarantorSurname.Text = "";
                tbGuarantorName.Text = "";
                tbGuarantorOtchestvo.Text = "";
                tbGuarantorINN.Text = "";
            }
            if (chkbxTypeOfCollateral.Items[2].Selected == true) 
            { 
                pnlGuarantees.Visible = true;
                chkbxTypeOfCollateral.Items[0].Enabled = false;
                chkbxTypeOfCollateral.Items[1].Enabled = true;
                chkbxTypeOfCollateral.Items[2].Enabled = true;
            } 
            else 
            { 
                pnlGuarantees.Visible = false;
                tbPledgerSurname.Text = "";
                tbPledgerName.Text = "";
                tbPledgerOtchestvo.Text = "";
                tbPledgerINN.Text = "";
                /*Products*//*----------------------------------------------------*/
                int reqID = Convert.ToInt32(hfRequestID.Value);
                var RequestGuarantee = (from v in dbRWZ.Guarantees where (v.RequestID == reqID) select v);
                dbRWZ.Guarantees.DeleteAllOnSubmit(RequestGuarantee);
                //var RequestGuarantee = (from v in dbRWZ.Guarantees where (v.RequestID == reqID) select v);
                //foreach (Guarantee rq in RequestGuarantee)
                //{
                //    rq.RequestID = requestID;
                //}
                dbRWZ.SubmitChanges();
                refreshGuarantees();
                /*****************************/
            }

        }

        protected void btnGuarantSearch_Click(object sender, EventArgs e)
        {
            pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            //hfCustomerID.Value = "noselect";
            hfGuarantorID.Value = "noselect";
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
            //clear_positions();
            //customerFieldEnable();
            disableCustomerFields();
            btnSearchClient.Visible = true;
            btnNewCustomer.Visible = true;
        }

        protected string textSMS()
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var req = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            var cust = dbR.Customers.Where(r => r.CustomerID == req.CustomerID).ToList().FirstOrDefault();
            decimal paysum = 0;
            int payday = 0;


            CreditController crdCtr = new CreditController();

           
            //var lstGraphics = crdCtr.GraphicsGetItem(Convert.ToInt32(hfCreditID.Value));
            //if (lstGraphics.Count != 0)
            {
               // lstGraphics.Remove(lstGraphics.Last());
                //var days = new List<int>();
                //int day;
               // payday = lstGraphics.FirstOrDefault().PayDate.Day;

                //foreach (var lst in lstGraphics)
                //{
                //    //days.Add(lst.PayDate.Day);
                //    if (payday > lst.PayDate.Day)
                //    {
                //        payday = lst.PayDate.Day;
                //    }
                //}



                //if (lstGraphics.Count > 0)
                {
                  //  paysum = lstGraphics.FirstOrDefault().TotalSumm;
                    //payday = day;
                }



                string strtxt = "Уважаемый(ая) " + cust.Surname + " " + cust.CustomerName + " " + cust.Otchestvo +  "\n" +
                "Вам одобрен кредит в размере " + req.RequestSumm.ToString() + " сом на " + req.RequestPeriod + " месяцев под " + req.RequestRate + " % годовых." +  //Ежемесячный платеж в размере " + paysum.ToString() + " сом по " + payday.ToString() + " - ым числам.Credit ID для погашения " + req.CreditID.ToString() + ". ";
                "Для подтверждения Вашей кредитной заявки просим отправить код " + req.RequestID.ToString() + " на номер 996701202552. " + "\n" +
                "Отправкой СМС вы подтверждаете, что соглашаетесь с публичной офертой[https://www.dcb.kg/media/Оферта_на_сайт_2709_.pdf] и даете согласие проверку и обработку персональных данных " +
                "в том числе проверку сведений кредитной истории в ЗАО Кредитное Бюро «Ишеним» либо ином кредитном бюро, на сбор и обработку персональных данных, " +
                "в том числе в Государственной регистрационной Службе КР ГПЭУ «Тундук» и других государственных органах и иных организациях.";


                return strtxt;

            }
            //else return "0";

        }


        protected string textSMS2()
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var req = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            var cust = dbR.Customers.Where(r => r.CustomerID == req.CustomerID).ToList().FirstOrDefault();
            decimal paysum = 0;
            int payday = 0;


            CreditController crdCtr = new CreditController();


            var lstGraphics = crdCtr.GraphicsGetItem(Convert.ToInt32(hfCreditID.Value));
            if (lstGraphics.Count != 0)
            {
                lstGraphics.Remove(lstGraphics.Last());
                //var days = new List<int>();
                //int day;
                payday = lstGraphics.FirstOrDefault().PayDate.Day;

                foreach (var lst in lstGraphics)
                {
                    //days.Add(lst.PayDate.Day);
                    if (payday > lst.PayDate.Day)
                    {
                        payday = lst.PayDate.Day;
                    }
                }



                if (lstGraphics.Count > 0)
                {
                    paysum = lstGraphics.FirstOrDefault().TotalSumm;
                    //payday = day;
                }



                string strtxt = "Уважаемый клиент! " + "\n" +
                //"Для подтверждения Вашей кредитной заявки просим отправить код " + req.RequestID.ToString() + " на номер 996701202552. " + "\n" +
                "Ежемесячный платеж в размере " + paysum.ToString() + " сом по " + payday.ToString() + " - ым числам.Credit ID для погашения " + req.CreditID.ToString() + ". "+
                "Кредитные средства поступят на вашу карту в течении 5 минут.";


                return strtxt;

            }
            else return "0";

        }

        protected void btnSendSMS_Click(object sender, EventArgs e)
        {
            if (lblSMS.Text != "")
            {
                string strtxt = "";
                if (lblSMS.Text.Substring(0,1)=="1")
                strtxt = textSMS(); 
                else 
                strtxt = textSMS2();
                if (strtxt != "0")
                {
                    dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
                    dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                    var req = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
                    var cust = dbR.Customers.Where(r => r.CustomerID == req.CustomerID).ToList().FirstOrDefault();
                    string username = Session["UserName"].ToString();

                    if (cust.ContactPhone1 != null)
                    {
                        string cphone1 = Regex.Replace(cust.ContactPhone1, @"[\s]", "");
                        string cphone = Regex.Replace(cphone1, @"[+]", "");
                        int n = cphone.Length;
                        if (n == 12)
                        {
                            var sms = sendSmsWithDevino(cphone, strtxt);
                            string codeSMS = sms.Result.Substring(2, 18);
                        }
                        //Console.WriteLine(strtxt);
                        histServ("", "СМС", username);
                    }
                    lblSMS.Text = "";
                    refreshLogDcbServ();
                }
                else
                {
                    //lblSMS.Text = "График не построен";
                    MsgBox("График не построен", this.Page, this);
                }
            }
        }

        static async Task<string> sendSmsWithDevino(string Phone, string Message)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://integrationapi.net/rest/v2/Sms/Send");

            string postData = "Login=doscredo_soft&Password=Asdf1234!@dos_soft&SourceAddress=DosCredo&DestinationAddress=" + Phone + "&Data=" + Message + "&Validity=0";
            byte[] data = Encoding.UTF8.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //string ok = responseString.ToString();
            return responseString;
            //Response.Write(responseString.ToString());
        }

        static bool MySslCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // If there are no errors, then everything went smoothly.
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // Note: MailKit will always pass the host name string as the `sender` argument.
            var host = (string)sender;

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != 0)
            {
                // This means that the remote certificate is unavailable. Notify the user and return false.
                Console.WriteLine("The SSL certificate was not available for {0}", host);
                return false;
            }

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) != 0)
            {
                // This means that the server's SSL certificate did not match the host name that we are trying to connect to.
                var certificate2 = certificate as X509Certificate2;
                var cn = certificate2 != null ? certificate2.GetNameInfo(X509NameType.SimpleName, false) : certificate.Subject;

                Console.WriteLine("The Common Name for the SSL certificate did not match {0}. Instead, it was {1}.", host, cn);
                return false;
            }

            // The only other errors left are chain errors.
            Console.WriteLine("The SSL certificate for the server could not be validated for the following reasons:");

            // The first element's certificate will be the server's SSL certificate (and will match the `certificate` argument)
            // while the last element in the chain will typically either be the Root Certificate Authority's certificate -or- it
            // will be a non-authoritative self-signed certificate that the server admin created. 
            foreach (var element in chain.ChainElements)
            {
                // Each element in the chain will have its own status list. If the status list is empty, it means that the
                // certificate itself did not contain any errors.
                if (element.ChainElementStatus.Length == 0)
                    continue;

                Console.WriteLine("\u2022 {0}", element.Certificate.Subject);
                foreach (var error in element.ChainElementStatus)
                {
                    // `error.StatusInformation` contains a human-readable error string while `error.Status` is the corresponding enum value.
                    Console.WriteLine("\t\u2022 {0}", error.StatusInformation);
                }
            }

            return false;
        }

        protected void btnCheckSMS_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var req = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            var cust = dbR.Customers.Where(r => r.CustomerID == req.CustomerID).ToList().FirstOrDefault();

            bool b = false;

            //ServicePointManager.ServerCertificateValidationCallback = (a, ) => true;
            
            using (Pop3Client client = new Pop3Client())
            {
                ServicePointManager.ServerCertificateValidationCallback = MySslCertificateValidationCallback;
                client.Connect("zimbra.doscredobank.kg", 995, true);
                //client.Connect("172.27.210.16", 995, true);

                //client.Connect("10.120.210.16", 995, true);
                //client.Authenticate("scoring917", "5tgbNHY^7ujm", OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);
                client.Authenticate("scoring", "SIuFJX9gAlmtTpOND3jv", OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);

                var count = client.GetMessageCount();

                for (int i = 1; i <= count; i++)
                {
                    Console.WriteLine("--------------------------");
                    OpenPop.Mime.Message message = client.GetMessage(i);
                    //Console.WriteLine(message.Headers.Subject);
                    int d = message.Headers.Subject.Length;
                    string mailphone = "", custphone = "";
                    custphone = cust.ContactPhone1;
                    custphone = "+996 557 999450";
                    if (d > 23) mailphone = message.Headers.Subject.Substring(9, 13);


                    if (mailphone == custphone.Replace(" ", "").Trim())
                    {
                        var bodytext = message.FindFirstPlainTextVersion() != null
                            ? message.FindFirstPlainTextVersion().GetBodyAsText()
                            : message.FindAllTextVersions()[0].GetBodyAsText();
                        //Console.WriteLine(bodytext);
                        if (req.RequestID.ToString().Trim() == bodytext.Trim())
                        {
                            b = true;
                            lblCheckSMS.Text = bodytext.Trim();
                        }
                    }


                }
            }

        }

        public class structToken
        {
            public string token { get; set; }
            //public string token_type { get; set; }
        }

        public string getToken(string result)
        {
            try
            {

                structToken dez = JsonConvert.DeserializeObject<structToken>(result.ToString());
                return dez.token.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public class Address
        {
            public string country_id { get; set; }
            public string region_id { get; set; }
            public string district_id { get; set; }
            public string aymak_id { get; set; }
            public string village_id { get; set; }
            public string street_id { get; set; }
            public string house_id { get; set; }
            public string house_txt { get; set; }
            public string flat_id { get; set; }
            public string flat_txt { get; set; }
            public object code { get; set; }
            public string post { get; set; }
        }

        public class Data
        {
            public string photo { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string middle_name { get; set; }
            public string family_status { get; set; }
            public string marital_status { get; set; }
            public string gender { get; set; }
            public string date_of_birth { get; set; }
            public string tin { get; set; }
            public string passport_status { get; set; }
            public string passport_series { get; set; }
            public string passport_number { get; set; }
            public string passport_issued_date { get; set; }
            public string passport_expiry_date { get; set; }
            public string passport_authority { get; set; }
            public object passport_authority_code { get; set; }
            public string address_of_registration { get; set; }
            public string address_region { get; set; }
            public string address_locality { get; set; }
            public string address_street { get; set; }
            public string address_house { get; set; }
            public object address_building { get; set; }
            public string address_apartment { get; set; }
            public Address address { get; set; }
        }

        public class Data2
        { 
        
        }

            public class Meta
        {
            public bool is_from_cache { get; set; }
            public string request_id { get; set; }
            public DateTime requested_at { get; set; }
            public string requested_by { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public Meta meta { get; set; }
        }

        public class Root2
        {
            public Data data2 { get; set; }
            public Meta meta { get; set; }
        }

        public class Root3
        {
            public string status { get; set; }
            public string message { get; set; }
            public string requestId { get; set; }
        }

        public class Root4
        {
            public string status { get; set; }
            public string message { get; set; }
            
        }

        public string getRequest_id(string result)
        {
            try
            {

                Root dez = JsonConvert.DeserializeObject<Root>(result.ToString());
                return dez.meta.request_id.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public string getRequest_id2(string result)
        {
            try
            {

                Root2 dez = JsonConvert.DeserializeObject<Root2>(result.ToString());
                return dez.meta.request_id.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public Root3 getStatMessageRoot3(string result)
        {
            try
            {

                Root3 dez = JsonConvert.DeserializeObject<Root3>(result.ToString());
                return dez;
            }
            catch (Exception ex)
            {
                Root3 dez = new Root3();
                dez.message = ex.ToString();
                return dez;
            }
        }

        public Root4 getStatMessageRoot4(string result)
        {
            try
            {

                Root4 dez = JsonConvert.DeserializeObject<Root4>(result.ToString());
                return dez;
            }
            catch (Exception ex)
            {
                Root4 dez = new Root4();
                dez.message = ex.ToString();
                return dez;
            }
        }

        public void histServ(string servRequest_id, string kindServ, string username)
        {
            //string username = Session["UserName"].ToString();
            LogDcbService newItem = new LogDcbService()
            {
                RequestID = Convert.ToInt32(hfRequestID.Value),
                ServiceRequestID = servRequest_id,
                DateCheck = DateTime.Now,
                Username = username,
                KindService = kindServ
            };

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.LogDcbServices.InsertOnSubmit(newItem);
            dbRWZ.LogDcbServices.Context.SubmitChanges();
        }

        public async Task<string> getDocFromCIB(string IdNumber, string FullName, string DateOfBirth, string InternalPassport, string typeReport)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    /*******Берем токен********/
                    client.BaseAddress = new Uri(connectionStringKIB + "/api/Account/Login");
                    //string json = "{\"userName\": \"user01\" , \"password\": \"password123\"}";


                    //string json = "{\"userName\": \"" + Session["UserName"].ToString() + "\" , \"password\": \"" + Session["Password"].ToString() + "\"}";
                    string json = "{\"userName\": \"" + "r.tentiev" + "\" , \"password\": \"" + "Password1@3$" + "\"}";

                    var response = await client.PostAsync(connectionStringKIB + "/api/Account/Login", new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    //string result3 = "res:" + result;
                    string token = getToken(result);
                    client.Dispose();
                    /**********Запрос КИБ*********/
                

                    HttpClient client2 = new HttpClient();
                    client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client2.BaseAddress = new Uri(connectionStringKIB + "/api/Cib/SmartSearchIndividual");

                    var u = connectionStringKIB + "/api/Cib/SoapSmartSearchIndividual";
                    //string json2 = "{\"IdNumber\": \"12303199400514\" , \"FullName\": \"Жоомарт кызы Зинат\", \"DateOfBirth\": \"1994-03-23\", \"InternalPassport\": \"ID1081023\"}";

                    //var builder = new UriBuilder(u);
                    //builder.Query = "IdNumber="+ IdNumber + "&FullName=" + FullName + "&DateOfBirth=" + DateOfBirth + "&InternalPassport=" + InternalPassport;
                    //var url = builder.ToString();

                    //var response2 = await client2.PostAsync(url, new StringContent(json2, Encoding.UTF8, "application/json"));
                    //var result2 = await response2.Content.ReadAsStringAsync();

                    //client2.Dispose();


                    var builder = new UriBuilder(u);
                    builder.Query = "IdNumber="+ IdNumber + "&FullName=" + FullName + "&DateOfBirth=" + DateOfBirth + "&InternalPassport=" + InternalPassport + "&userName=" + Session["UserName"].ToString();
                    var url = builder.ToString();

                    var response2 = await client2.PostAsync(url, new StringContent("", Encoding.UTF8, "application/json"));
                    var result2 = await response2.Content.ReadAsStringAsync();

                    client2.Dispose();



                    if (result2 != "SubjectNotFound")
                    {


                        //    /****************************/


                        HttpClient client3 = new HttpClient();
                        client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        client3.BaseAddress = new Uri(connectionStringKIB + "/api/Cib/GetPdfReport");

                        var u3 = connectionStringKIB + "/api/Cib/GetPdfReport";
                        var json3 = "{\"IdNumber\": \"12303199400514\", \"typeReport\": \"CondensedReportPlus\"}";

                        var builder3 = new UriBuilder(u3);
                        builder3.Query = "IDNumber=" + result2 + "&typeReport=" + typeReport + "&userName=" + Session["UserName"].ToString();
                        //builder3.Query = "typeReport=" + "CondensedReportPlus";
                        var url3 = builder3.ToString();

                        var response3 = await client3.PostAsync(url3, new StringContent("", Encoding.UTF8, "application/json"));
                        var result3byte = await response3.Content.ReadAsByteArrayAsync();
                        string result3string = await response3.Content.ReadAsStringAsync();
                        string res = JsonConvert.ToString(result3string);
                        client3.Dispose();
                        result3string = result3string.Replace("\"", "");

                        byte[] byteArray = Convert.FromBase64String(result3string);


                        //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
                        //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                        //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                        //embed += "</object>";

                        string embed = "<embed src = \"{0}\" width = \"auto\" height = \"900\" alt = \"pdf\" pluginspage = \"http://www.adobe.com/products/acrobat/readstep2.html\">";

                        ltEmbed.Text = string.Format(embed, ResolveUrl("https://documentcloud.adobe.com/view-sdk-demo/PDFs/Bodea%20Brochure.pdf"));

                        //string Base64String = "data:application/pdf;base64," + result3string;


                        //Скачивание файла
                        //Response.Clear();
                        //Response.Buffer = true;
                        //Response.Charset = "";
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //Response.ContentType = "application/pdf";
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + "MyFile.pdf");
                        //Response.BinaryWrite(byteArray);
                        //Response.Flush();


                        string destinationFile = "";
                        string destinationFolder = getDestinationFolder();
                        GeneralController gctl = new GeneralController();
                        string dateRandomdir = gctl.DateRandodir(destinationFolder);
                        destinationFile = gctl.fileNameAddExt(IdNumber + "_KIB" + DateTime.Today.Date.ToString("_ddMMyyyy") + ".pdf", destinationFolder, dateRandomdir);

                        File.WriteAllBytes(destinationFolder + "\\" + dateRandomdir + "\\" + destinationFile, Convert.FromBase64String(result3string));



                        RequestsFile newRequestFile = new RequestsFile
                        {
                            Name = destinationFile,
                            RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
                            ContentType = "pdf",
                            //Data = bytes,
                            //FullName = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + fullfilename,
                            //FullName = "\\Portals\\0\\" + partnerdir + "\\" + destinationFile,
                            //FullName2 = fileupl + "\\" + "Portals\\0\\" + partnerdir + "\\" + destinationFile,
                            FullName = "\\" + partnerdir + "\\" + dateRandomdir + "\\" + destinationFile,
                            FullName2 = fileupl + "\\" + partnerdir + "\\" + dateRandomdir + "\\" + destinationFile,
                            FileDescription = tbFileDescription.Text + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                            IsPhoto = false

                        };
                        ItemController ctl = new ItemController();
                        ctl.ItemRequestFilesAddItem(newRequestFile);
                        return result3string;

                    }
                    else { 
                        MsgBox("Клиент не найден в базе КИБ", this.Page, this);
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
                return null;
            }
            //finally
            //{

            //}
        }


        public async Task<string> getDocFromSucfund(string Pin, string Service)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                
                    //if (result2 != "SubjectNotFound")
                    {
                        HttpClient client = new HttpClient();

                        client.BaseAddress = new Uri(connectionStringKIB + "/api/Account/Login");
                        //string json = "{\"userName\": \"user01\" , \"password\": \"password123\"}";


                        //string json = "{\"userName\": \"" + Session["UserName"].ToString() + "\" , \"password\": \"" + Session["Password"].ToString() + "\"}";
                        string json = "{\"userName\": \"" + "r.tentiev" + "\" , \"password\": \"" + "Password1@3$" + "\"}";

                        var response = await client.PostAsync(connectionStringKIB + "/api/Account/Login", new StringContent(json, Encoding.UTF8, "application/json"));
                        var result = await response.Content.ReadAsStringAsync();
                        //string result3 = "res:" + result;
                        string token = getToken(result);
                        client.Dispose();
                        //    /****************************/


                        HttpClient client3 = new HttpClient();
                        client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        client3.BaseAddress = new Uri(connectionStringKIB + Service);

                        var u3 = connectionStringKIB + Service;
                        //var json3 = "{\"IdNumber\": \"12303199400514\", \"typeReport\": \"CondensedReportPlus\"}";

                        var builder3 = new UriBuilder(u3);
                        builder3.Query = "Pin=" + Pin + "&userName=" + Session["UserName"].ToString();
                        //builder3.Query = "typeReport=" + "CondensedReportPlus";
                        var url3 = builder3.ToString();

                        var response3 = await client3.PostAsync(url3, new StringContent("", Encoding.UTF8, "application/json"));
                        var result3byte = await response3.Content.ReadAsByteArrayAsync();
                        string result3string = await response3.Content.ReadAsStringAsync();
                        string res = JsonConvert.ToString(result3string);
                        client3.Dispose();
                        result3string = result3string.Replace("\"", "");

                        byte[] byteArray = Convert.FromBase64String(result3string);


                        //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
                        //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                        //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                        //embed += "</object>";

                        string embed = "<embed src = \"{0}\" width = \"auto\" height = \"900\" alt = \"pdf\" pluginspage = \"http://www.adobe.com/products/acrobat/readstep2.html\">";

                        ltEmbed.Text = string.Format(embed, ResolveUrl("https://documentcloud.adobe.com/view-sdk-demo/PDFs/Bodea%20Brochure.pdf"));

                        //string Base64String = "data:application/pdf;base64," + result3string;


                        //Скачивание файла
                        //Response.Clear();
                        //Response.Buffer = true;
                        //Response.Charset = "";
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //Response.ContentType = "application/pdf";
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + "MyFile.pdf");
                        //Response.BinaryWrite(byteArray);
                        //Response.Flush();


                        string destinationFile = "";
                        string destinationFolder = getDestinationFolder();
                        GeneralController gctl = new GeneralController();
                        string dateRandomdir = gctl.DateRandodir(destinationFolder);
                        destinationFile = gctl.fileNameAddExt(Pin + "_KIB" + DateTime.Today.Date.ToString("_ddMMyyyy") + ".pdf", destinationFolder, dateRandomdir);

                        File.WriteAllBytes(destinationFolder + "\\" + dateRandomdir + "\\" + destinationFile, Convert.FromBase64String(result3string));



                        RequestsFile newRequestFile = new RequestsFile
                        {
                            Name = destinationFile,
                            RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
                            ContentType = "pdf",
                            //Data = bytes,
                            //FullName = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + fullfilename,
                            //FullName = "\\Portals\\0\\" + partnerdir + "\\" + destinationFile,
                            //FullName2 = fileupl + "\\" + "Portals\\0\\" + partnerdir + "\\" + destinationFile,
                            FullName = "\\" + partnerdir + "\\" + dateRandomdir + "\\" + destinationFile,
                            FullName2 = fileupl + "\\" + partnerdir + "\\" + dateRandomdir + "\\" + destinationFile,
                            FileDescription = tbFileDescription.Text + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                            IsPhoto = false

                        };
                        ItemController ctl = new ItemController();
                        ctl.ItemRequestFilesAddItem(newRequestFile);
                        return result3string;

                    }
                    
                   
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
                return null;
            }
            //finally
            //{

            //}
        }

        public async Task<string> getDocFromSucfund2(string Pin)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {

                //if (result2 != "SubjectNotFound")
                {
                    HttpClient client = new HttpClient();

                    client.BaseAddress = new Uri(connectionStringKIB + "/api/Account/Login");
                    //string json = "{\"userName\": \"user01\" , \"password\": \"password123\"}";


                    //string json = "{\"userName\": \"" + Session["UserName"].ToString() + "\" , \"password\": \"" + Session["Password"].ToString() + "\"}";
                    string json = "{\"userName\": \"" + "r.tentiev" + "\" , \"password\": \"" + "Password1@3$" + "\"}";

                    var response = await client.PostAsync(connectionStringKIB + "/api/Account/Login", new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    //string result3 = "res:" + result;
                    string token = getToken(result);
                    client.Dispose();
                    //    /****************************/


                    HttpClient client3 = new HttpClient();
                    client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client3.BaseAddress = new Uri(connectionStringKIB + "/api/Socfund/SoapGetWorkPeriodInfoWithSumPdf");

                    var u3 = connectionStringKIB + "/api/Socfund/SoapGetWorkPeriodInfoWithSumPdf";
                    //var json3 = "{\"IdNumber\": \"12303199400514\", \"typeReport\": \"CondensedReportPlus\"}";

                    var builder3 = new UriBuilder(u3);
                    builder3.Query = "Pin=" + Pin + "&userName=" + Session["UserName"].ToString();
                    //builder3.Query = "typeReport=" + "CondensedReportPlus";
                    var url3 = builder3.ToString();

                    var response3 = await client3.PostAsync(url3, new StringContent("", Encoding.UTF8, "application/json"));
                    var result3byte = await response3.Content.ReadAsByteArrayAsync();
                    string result3string = await response3.Content.ReadAsStringAsync();
                    string res = JsonConvert.ToString(result3string);
                    client3.Dispose();
                    result3string = result3string.Replace("\"", "");

                    byte[] byteArray = Convert.FromBase64String(result3string);


                    //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
                    //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    //embed += "</object>";

                    string embed = "<embed src = \"{0}\" width = \"auto\" height = \"900\" alt = \"pdf\" pluginspage = \"http://www.adobe.com/products/acrobat/readstep2.html\">";

                    ltEmbed.Text = string.Format(embed, ResolveUrl("https://documentcloud.adobe.com/view-sdk-demo/PDFs/Bodea%20Brochure.pdf"));

                    //string Base64String = "data:application/pdf;base64," + result3string;


                    //Скачивание файла
                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.Charset = "";
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.ContentType = "application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + "MyFile.pdf");
                    //Response.BinaryWrite(byteArray);
                    //Response.Flush();


                    string destinationFile = "";
                    string destinationFolder = getDestinationFolder();
                    GeneralController gctl = new GeneralController();
                    string dateRandomdir = gctl.DateRandodir(destinationFolder);
                    destinationFile = gctl.fileNameAddExt(Pin + "_KIB" + DateTime.Today.Date.ToString("_ddMMyyyy") + ".pdf", destinationFolder, dateRandomdir);

                    File.WriteAllBytes(destinationFolder + "\\" + dateRandomdir + "\\" + destinationFile, Convert.FromBase64String(result3string));



                    RequestsFile newRequestFile = new RequestsFile
                    {
                        Name = destinationFile,
                        RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
                        ContentType = "pdf",
                        //Data = bytes,
                        //FullName = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + fullfilename,
                        //FullName = "\\Portals\\0\\" + partnerdir + "\\" + destinationFile,
                        //FullName2 = fileupl + "\\" + "Portals\\0\\" + partnerdir + "\\" + destinationFile,
                        FullName = "\\" + partnerdir + "\\" + dateRandomdir + "\\" + destinationFile,
                        FullName2 = fileupl + "\\" + partnerdir + "\\" + dateRandomdir + "\\" + destinationFile,
                        FileDescription = tbFileDescription.Text + " " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                        IsPhoto = false

                    };
                    ItemController ctl = new ItemController();
                    ctl.ItemRequestFilesAddItem(newRequestFile);
                    return result3string;

                }


            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
                return null;
            }
            //finally
            //{

            //}
        }
        public async Task<string> initPerm(string Pin, string PhoneNumber, string LastName, string FirstName, string Patronymic, string EndDate, string BirthDate,
            string PassportAddress, string FactAddress, string PassportNumberAndSeries, string PassportIssuedDate, string PassportIssuedBy, string Email, string userName)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                
                    //if (result2 != "SubjectNotFound")
                    {
                        HttpClient client = new HttpClient();

                        client.BaseAddress = new Uri(connectionStringKIB + "/api/Account/Login");
                    //string json = "{\"userName\": \"user01\" , \"password\": \"password123\"}";

                    
                    //string json = "{\"userName\": \"" + Session["UserName"].ToString() + "\" , \"password\": \"" + Session["Password"].ToString() + "\"}";
                    string json = "{\"userName\": \"" + "r.tentiev" + "\" , \"password\": \"" + "Password1@3$" + "\"}";

                        var response = await client.PostAsync(connectionStringKIB + "/api/Account/Login", new StringContent(json, Encoding.UTF8, "application/json"));
                        var result = await response.Content.ReadAsStringAsync();
                        //string result3 = "res:" + result;
                        string token = getToken(result);
                        client.Dispose();
                        //    /****************************/


                        HttpClient client3 = new HttpClient();
                        client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        client3.BaseAddress = new Uri(connectionStringKIB + "/api/Socfund/SoapInitReqForPerm");

                        var u3 = connectionStringKIB + "/api/Socfund/SoapInitReqForPerm";
                    //var json3 = "{\"IdNumber\": \"12303199400514\", \"typeReport\": \"CondensedReportPlus\"}";



                    var form = new MultipartFormDataContent();


                    var fileStreamContent1 = new StreamContent(FileUpload1.FileContent);
                    fileStreamContent1.Headers.ContentType = MediaTypeHeaderValue.Parse(FileUpload1.PostedFile.ContentType);
                    form.Add(fileStreamContent1, name: "filePassportFrontSideImage", FileUpload1.FileName);

                    var fileStreamContent2 = new StreamContent(FileUpload2.FileContent);
                    fileStreamContent2.Headers.ContentType = MediaTypeHeaderValue.Parse(FileUpload2.PostedFile.ContentType);
                    form.Add(fileStreamContent2, name: "filePassportBackSideImage", FileUpload2.FileName);

                    //var fileStreamContent3 = new StreamContent(model.SelfieWithPassportImage.OpenReadStream());
                    //fileStreamContent3.Headers.ContentType = MediaTypeHeaderValue.Parse(model.SelfieWithPassportImage.ContentType);
                    //form.Add(fileStreamContent3, name: "fileSelfieWithPassportImage", model.SelfieWithPassportImage.FileName);

                    var fileStreamContent3 = new StreamContent(FileUpload3.FileContent);
                    fileStreamContent3.Headers.ContentType = MediaTypeHeaderValue.Parse(FileUpload3.PostedFile.ContentType);
                    form.Add(fileStreamContent3, name: "fileSelfieWithPassportImage", FileUpload3.FileName);

                    Email = "rysbekke@gmail.com";
                    PhoneNumber = PhoneNumber.Trim().Replace(" ", "");
                    PhoneNumber = PhoneNumber.Substring(1, 12);
                    var builder3 = new UriBuilder(u3);
                    builder3.Query = "Pin=" + Pin + "&PhoneNumber=" + PhoneNumber + "&LastName=" + LastName + "&FirstName=" + FirstName + "&Patronymic=" + Patronymic +
                    "&EndDate=" + EndDate + "&BirthDate=" + BirthDate + "&PassportAddress=" + PassportAddress + "&FactAddress=" + FactAddress +
                    "&PassportNumberAndSeries=" + PassportNumberAndSeries + "&PassportIssuedDate=" + PassportIssuedDate + "&PassportIssuedBy=" + PassportIssuedBy +
                    "&Email=" + Email + "&userName=" + userName;
                        //builder3.Query = "typeReport=" + "CondensedReportPlus";
                        var url3 = builder3.ToString();

                    //var response3 = await client3.PostAsync(url3, new StringContent("", Encoding.UTF8, "application/json"));
                    var response3 = await client3.PostAsync(url3, form);
                    var result3byte = await response3.Content.ReadAsByteArrayAsync();
                        string result3string = await response3.Content.ReadAsStringAsync();
                        string res = JsonConvert.ToString(result3string);
                        client3.Dispose();
                        result3string = result3string.Replace("\"", "");

                        //byte[] byteArray = Convert.FromBase64String(result3string);


                                            
                        return result3string;

                    }
                    
                   
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
                return null;
            }
            //finally
            //{

            //}
        }



        public async Task<string> sendCodeForPerm(string Pin, string PhoneNumber, string LastName, string FirstName, string Patronymic, string EndDate, string BirthDate,
            string PassportAddress, string FactAddress, string PassportNumberAndSeries, string PassportIssuedDate, string PassportIssuedBy, string Email, string userName)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {

                //if (result2 != "SubjectNotFound")
                {
                    HttpClient client = new HttpClient();

                    client.BaseAddress = new Uri(connectionStringKIB + "/api/Account/Login");
                    //string json = "{\"userName\": \"user01\" , \"password\": \"password123\"}";


                    //string json = "{\"userName\": \"" + Session["UserName"].ToString() + "\" , \"password\": \"" + Session["Password"].ToString() + "\"}";
                    string json = "{\"userName\": \"" + "r.tentiev" + "\" , \"password\": \"" + "Password1@3$" + "\"}";

                    var response = await client.PostAsync(connectionStringKIB + "/api/Account/Login", new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    //string result3 = "res:" + result;
                    string token = getToken(result);
                    client.Dispose();
                    //    /****************************/


                    HttpClient client3 = new HttpClient();
                    client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client3.BaseAddress = new Uri(connectionStringKIB + "/api/Socfund/SoapSendConfirmationCodeForPermission");

                    var u3 = connectionStringKIB + "/api/Socfund/SoapSendConfirmationCodeForPermission";
                    //var json3 = "{\"IdNumber\": \"12303199400514\", \"typeReport\": \"CondensedReportPlus\"}";

                    var form = new MultipartFormDataContent();




                    var builder3 = new UriBuilder(u3);
                    builder3.Query = "Pin=" + Pin + "&PhoneNumber=" + PhoneNumber.Trim().Replace(" ", "") + "&LastName=" + LastName + "&FirstName=" + FirstName + "&Patronymic=" + Patronymic +
                    "&EndDate=" + EndDate + "&BirthDate=" + BirthDate + "&PassportAddress=" + PassportAddress + "&FactAddress=" + FactAddress +
                    "&PassportNumberAndSeries=" + PassportNumberAndSeries + "&PassportIssuedDate=" + PassportIssuedDate + "&PassportIssuedBy=" + PassportIssuedBy +
                    "&Email=" + Email + "&userName=" + userName;
                    //builder3.Query = "typeReport=" + "CondensedReportPlus";
                    var url3 = builder3.ToString();

                    //var response3 = await client3.PostAsync(url3, new StringContent("", Encoding.UTF8, "application/json"));
                    var response3 = await client3.PostAsync(url3, form);
                    var result3byte = await response3.Content.ReadAsByteArrayAsync();
                    string result3string = await response3.Content.ReadAsStringAsync();
                    string res = JsonConvert.ToString(result3string);
                    client3.Dispose();
                    result3string = result3string.Replace("\"", "");

                    //byte[] byteArray = Convert.FromBase64String(result3string);



                    return result3string;

                }


            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
                return null;
            }
            //finally
            //{

            //}
        }



        public async void getDocFromServDCB(string kindServ)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    /*******Берем токен********/
                    client.BaseAddress = new Uri("http://services.dcb.kg/api/auth/token/");
                    string json = "{\"username\": \"scoring\" , \"password\": \"#xQb#zk3ghCJA3tG\"}";
                    var response = await client.PostAsync("http://services.dcb.kg/api/auth/token/", new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    string result3 = "res:" + result;
                    string token = getToken(result);
                    /**********Запрос ГРС*********/
                    //client.BaseAddress = new Uri("http://10.120.101.165:4000/api/auth/token/");
                    //string json = "{\"username\": \"scoring\" , \"password\": \"#xQb#zk3ghCJA3tG\"}";
                    //var response = await client.PostAsync("http://10.120.101.165:4000/api/auth/token/", new StringContent(json, Encoding.UTF8, "application/json"));
                    int custID = Convert.ToInt32(hfCustomerID.Value);
                    SysController ctx = new SysController();
                    Customer cust = ctx.CustomerGetItem(custID);
                    string DocumentSeriesNo = (cust.DocumentSeries + cust.DocumentNo).Replace(" ", "").Trim();
                    string series = DocumentSeriesNo.Substring(0, 2);
                    string number = DocumentSeriesNo.Substring(2, 7); ;
                    string username = Session["UserName"].ToString();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    string url1 = "http://services.dcb.kg/api/tunduk/scoring/";

                    string url2 = "";
                    if (kindServ == "ГРС") url2 = "passport?tin=" + cust.IdentificationNumber + "&series=" + series + "&number=" + number + "&check_cache=true&username=" + username;
                    //if (kindServ == "Вет инспекция") url2 = "animals?tin=" + cust.IdentificationNumber + "&check_cache=true&username=" + username;
                    if (kindServ == "Аймак") url2 = "aymak?tin=" + cust.IdentificationNumber + "&check_cache=true&username=" + username;
                    if (kindServ == "Зарплата") url2 = "salary?tin=" + cust.IdentificationNumber + "&check_cache=true&username=" + username;
                    if (kindServ == "Пенсия") url2 = "pension?tin=" + cust.IdentificationNumber + "&check_cache=true&username=" + username;
                    if (kindServ == "ГНС") url2 = "taxpayer?tin=" + cust.IdentificationNumber + "&check_cache=true&username=" + username;
                    //string url = "http://services.dcb.kg/api/tunduk/scoring/animals?tin=" + cust.IdentificationNumber + "&check_cache=true&username=" + username;

                    string url = url1 + url2;
                    response = await client.GetAsync(url);
                    result = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string servRequest_id = getRequest_id2(result);
                        histServ(servRequest_id, kindServ, username);
                        refreshLogDcbServ();

                        /****************************/

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        url = "http://services.dcb.kg/api/tunduk/pdf?request_id=" + servRequest_id;
                        response = await client.GetAsync(url);

                        var result2 = await response.Content.ReadAsByteArrayAsync(); // ReadAsStringAsync();


                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + "MyFile.pdf");
                        //byte[] bitmapData = Encoding.UTF8.GetBytes(result2);
                        //ToImage(result2).Save(Server.MapPath("~/") + "MyFile.pdf");
                        Response.BinaryWrite(result2);
                        Response.Flush();
                        //Response.TransmitFile( Server.MapPath("~/Files/MyFile.pdf"));
                        // Response.End();
                        this.Context.ApplicationInstance.CompleteRequest();
                        Response.Redirect("/Microcredit/Loan");
                    }
                    else MsgBox(result, this.Page, this);

                }
            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
            }
            finally
            {

            }
        }


       

        public async void postInitPermFromServDCB(string kindServ)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    /*******Берем токен********/
                    client.BaseAddress = new Uri("http://services.dcb.kg/api/auth/token/");
                    string json = "{\"username\": \"scoring\" , \"password\": \"#xQb#zk3ghCJA3tG\"}";
                    var response = await client.PostAsync("http://services.dcb.kg/api/auth/token/", new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    string result3 = "res:" + result;
                    string token = getToken(result);
                    /**********Запрос ГРС*********/
                    //client.BaseAddress = new Uri("http://10.120.101.165:4000/api/auth/token/");
                    //string json = "{\"username\": \"scoring\" , \"password\": \"#xQb#zk3ghCJA3tG\"}";
                    //var response = await client.PostAsync("http://10.120.101.165:4000/api/auth/token/", new StringContent(json, Encoding.UTF8, "application/json"));
                    int custID = Convert.ToInt32(hfCustomerID.Value);
                    SysController ctx = new SysController();
                    Customer cust = ctx.CustomerGetItem(custID);
                    string DocumentSeriesNo = (cust.DocumentSeries + cust.DocumentNo).Replace(" ", "").Trim();
                    string series = DocumentSeriesNo.Substring(0, 2);
                    string number = DocumentSeriesNo.Substring(2, 7); ;
                    string username = Session["UserName"].ToString();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    string url1 = "http://services.dcb.kg/api/tunduk/scoring/";

                    string reqIdDcbServPerm = "";
                   


                    string url2 = "";
                    if (kindServ == "Запрос на разрешение")
                    {
                        url2 = "permission/initialise?username=" + username;
                        json = "{\"tin\": \"" +
                       cust.IdentificationNumber.Trim() +
                       "\", \"phone_number\": \"" +
                       cust.ContactPhone1.Trim().Replace(" ", "") +
                       "\", \"last_name\": \"" +
                       cust.Surname +
                       "\", \"first_name\": \"" +
                       cust.CustomerName +
                        "\", \"middle_name\": \"" +
                       cust.Otchestvo +
                        "\", \"end_date\": \"" +
                       Convert.ToDateTime(cust.DocumentValidTill).ToString("yyyy-MM-dd") +
                        "\", \"dob\": \"" +
                       Convert.ToDateTime(cust.DateOfBirth).ToString("yyyy-MM-dd") +
                       "\", \"passport_address\": \"" +
                       cust.RegistrationCityName + ", " + cust.RegistrationStreet + ", " + cust.RegistrationHouse + ", " + cust.RegistrationFlat +
                    "\", \"accommodation_address\": \"" +
                       cust.ResidenceCityName + ", " + cust.ResidenceStreet + ", " + cust.ResidenceHouse + ", " + cust.ResidenceFlat +
                    "\", \"passport_no\": \"" +
                       cust.DocumentSeries + cust.DocumentNo +
                       "\", \"passport_issued_dt\": \"" +
                       Convert.ToDateTime(cust.IssueDate).ToString("yyyy-MM-dd") +
                       "\", \"passport_issued_by\": \"" +
                       cust.IssueAuthority +
                       "\", \"email\": \"" +
                       "\"" +
                       "}";
                    }
                    if (kindServ == "Подтверждение запроса")
                    {
                        var lst = dbRWZ.AdditionRequests.Where(x => x.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList();
                        if (lst.Count > 0) reqIdDcbServPerm = dbRWZ.AdditionRequests.Where(x => x.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault().RequestIdDcbServConfPerm;

                        url2 = "permission/confirm?username=" + username;
                        json = "{\"request_id\": \"" +
                        reqIdDcbServPerm +
                        "\", \"sms_code\": \"" +
                       txtCodePerm.Text.Trim() +
                       "\", \"permission_id\": " +
                       "0" +
                       "}";
                    }
                   

                    string url = url1 + url2;
                    //response = await client.GetAsync(url);
                    //result = await response.Content.ReadAsStringAsync();


                    //client.BaseAddress = new Uri("https://" + connectionStringOBAPIAddress + "/");

                    if ((kindServ == "Подтверждение запроса") && (reqIdDcbServPerm == ""))
                    {
                        MsgBox("Необходимо отправить запрос на разрешение", this.Page, this);
                    }
                    else
                    {
                        response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                        result = await response.Content.ReadAsStringAsync();
                        
                        //result = await response.Content.ReadAsStringAsync();


                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            string status = getstatInitPerm(result);
                            if (status != "InternalError")
                            {
                                if (kindServ == "Запрос на разрешение")
                                {
                                    Root3 statMessage = getStatMessageRoot3(result);
                                    //requestIdForConfPerm = statMessage.requestId;
                                    var addreq = dbRWZ.AdditionRequests.Where(x => x.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList().FirstOrDefault();

                                    if (addreq is null)
                                    {
                                        AdditionRequest newItem = new AdditionRequest()
                                        {
                                            RequestID = Convert.ToInt32(hfRequestID.Value),
                                            RequestIdDcbServConfPerm = statMessage.requestId.ToString()
                                        };

                                        dbRWZ.AdditionRequests.InsertOnSubmit(newItem);
                                        dbRWZ.AdditionRequests.Context.SubmitChanges();
                                    }
                                    else
                                    {
                                        addreq.RequestIdDcbServConfPerm = statMessage.requestId.ToString();
                                        dbRWZ.AdditionRequests.Context.SubmitChanges();
                                    }
                                    if (statMessage.status == "InternalError")
                                    {
                                        MsgBox(statMessage.message, this.Page, this);
                                    }
                                    else
                                    {
                                        MsgBox(statMessage.message, this.Page, this);
                                        histServ("", kindServ, username);
                                        refreshLogDcbServ();
                                    }
                                }
                                if (kindServ == "Подтверждение запроса")
                                {
                                    Root3 statMessage = getStatMessageRoot3(result);
                                    MsgBox(statMessage.message, this.Page, this);
                                }



                                /****************************/

                                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                                //url = "http://services.dcb.kg/api/tunduk/pdf?request_id=" + servRequest_id;
                                //response = await client.GetAsync(url);

                                //var result2 = await response.Content.ReadAsByteArrayAsync(); // ReadAsStringAsync();

                                //this.Context.ApplicationInstance.CompleteRequest();
                                //Response.Redirect("/Microcredit/AgentView");
                            }
                            else
                            {
                                btnInitPerm.Enabled = true;
                                MsgBox(result, this.Page, this);
                            }
                        }
                        else
                        {
                            //btnInitPerm.Enabled = true;
                            MsgBox(result, this.Page, this);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }

        //protected async System.Threading.Tasks.Task<string> btnPassport_Click(object sender, EventArgs e)
        protected async void btnPassport_Click(object sender, EventArgs e)
        {
            getDocFromServDCB("ГРС");
        }


        public System.Drawing.Image ToImage(byte[] imageBytes)
        {

            //byte[] imageBytes = Convert.FromBase64String(hfPhoto2.Value);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }

      

        protected async void gvLogDcbService_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "Open")
            {
                ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        /*******Берем токен********/
                        client.BaseAddress = new Uri("http://services.dcb.kg/api/auth/token/");
                        string json = "{\"username\": \"scoring\" , \"password\": \"#xQb#zk3ghCJA3tG\"}";
                        var response = await client.PostAsync("http://services.dcb.kg/api/auth/token/", new StringContent(json, Encoding.UTF8, "application/json"));
                        var result = await response.Content.ReadAsStringAsync();
                        string result3 = "res:" + result;
                        string token = getToken(result);
                        /**********Запрос ГРС*********/

                        string request_id = id;
                        /****************************/

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        string url = "http://services.dcb.kg/api/tunduk/pdf?request_id=" + request_id;
                        response = await client.GetAsync(url);

                        var result2 = await response.Content.ReadAsByteArrayAsync(); // ReadAsStringAsync();

                        //Response.ContentType = "application/pdf";
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=MyFile.pdf");
                        //Response.TransmitFile(Server.MapPath(result));
                        //Response.End();


                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + "MyFile.pdf");
                        Response.BinaryWrite(result2);
                        Response.Flush();
                        this.Context.ApplicationInstance.CompleteRequest();

                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {

                }
                // lbHistory_Click(new object(), new EventArgs());
            }
        }




        protected void btnShowSMS_Click(object sender, EventArgs e)
        {
            string strtxt = textSMS();
            if (strtxt != "0")
            {
                dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                var req = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
                var cust = dbR.Customers.Where(r => r.CustomerID == req.CustomerID).ToList().FirstOrDefault();
                //MsgBox(strtxt, this.Page, this);
                //MsgBox("Неправильно введены паспортные данные", this.Page, this);
                lblSMS.Text = "1.Номер клиента:" + cust.ContactPhone1 + ", Текст СМС:" + strtxt;
            } 
            else
            {
                //lblSMS.Text = "График не построен"; 
                MsgBox("График не построен", this.Page, this);

            }
        }

        protected void btnShowSMS2_Click(object sender, EventArgs e)
        {
            
            string strtxt = textSMS2();
            if (strtxt != "0")
            {
                dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                var req = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
                var cust = dbR.Customers.Where(r => r.CustomerID == req.CustomerID).ToList().FirstOrDefault();
                //MsgBox(strtxt, this.Page, this);
                //MsgBox("Неправильно введены паспортные данные", this.Page, this);
                lblSMS.Text = "2.Номер клиента:" + cust.ContactPhone1 + ", Текст СМС:" + strtxt;
            }
            else
            {
                //lblSMS.Text = "График не построен"; 
                MsgBox("График не построен", this.Page, this);

            }
        }

        protected void btnSendSMS2_Click(object sender, EventArgs e)
        {

            string strtxt = "";
            if (lblSMS.Text.Substring(0, 1) == "1") strtxt = textSMS();
            else strtxt = textSMS2();
            if (strtxt != "0")
            {
                dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                var req = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
                var cust = dbR.Customers.Where(r => r.CustomerID == req.CustomerID).ToList().FirstOrDefault();

                if (cust.ContactPhone1 != null)
                {
                    string cphone1 = Regex.Replace(cust.ContactPhone1, @"[\s]", "");
                    string cphone = Regex.Replace(cphone1, @"[+]", "");
                    int n = cphone.Length;
                    if (n == 12)
                    {
                        var sms = sendSmsWithDevino(cphone, strtxt);
                        string codeSMS = sms.Result.Substring(2, 18);
                    }
                    //Console.WriteLine(strtxt);
                }
                lblSMS.Text = "";
            }
            else
            {
                //lblSMS.Text = "График не построен"; 
                MsgBox("График не построен", this.Page, this);
            }
        }

        protected async void btnSalary_Click(object sender, EventArgs e)
        {
            getDocFromServDCB("Зарплата");

          
        }

        protected async void btnPension_Click(object sender, EventArgs e)
        {
            getDocFromServDCB("Пенсия");
          
        }

        protected async void btnAnimals_Click(object sender, EventArgs e)
        {
            getDocFromServDCB("Аймак");
       
        }

        protected void btnGns_Click(object sender, EventArgs e)
        {
            getDocFromServDCB("ГНС");
        }

        protected void btnInitPerm_Click(object sender, EventArgs e)
        {
            btnInitPerm.Enabled = false;
            postInitPermFromServDCB("Запрос на разрешение");
            
        }

        protected void btnConfPerm_Click(object sender, EventArgs e)
        {
            postInitPermFromServDCB("Подтверждение запроса");
            txtCodePerm.Text = "";
        }

        protected void btnPledgerSearch_Click(object sender, EventArgs e)
        {
            pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            //hfCustomerID.Value = "noselect";
            hfPledgerID.Value = "noselect";
            btnCredit.Text = "Выбрать залогодателя";
            hfChooseClient.Value = "Выбрать залогодателя";
            tbPledgerSurname.Text = "";
            tbPledgerName.Text = "";
            tbPledgerOtchestvo.Text = "";
            tbPledgerINN.Text = "";
            clearEditControls();
            lblMessageClient.Text = "";
            tbContactPhone.Text = "";
            tbSearchINN.Text = "";
            btnSaveCustomer.Enabled = true;
            tbSerialN.Text = "";
            //clear_positions();
            //customerFieldEnable();
            disableCustomerFields();
            btnSearchClient.Visible = true;
            btnNewCustomer.Visible = true;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            //if (FileUploadControl.HasFile)
            //{
            //    try
            //    {
            //        //if (FileUploadControl.PostedFile.ContentType == "image/jpeg")
            //        {
            //            if (FileUploadControl.PostedFile.ContentLength < 10240000)
            //            {
            //                string filename = Path.GetFileName(FileUploadControl.FileName);
            //                FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
            //                StatusLabel.Text = "Upload status: File uploaded!";
            //            }
            //            else
            //                StatusLabel.Text = "Upload status: The file has to be less than 10 mb!";
            //        }
            //        //else StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
            //    }
            //    catch (Exception ex)
            //    {
            //        StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            //    }
            //}
        }
     


        public void refreshGuarantees()
        {
            int amoundp = 10;
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lstRequestsProducts = dbRWZ.Guarantees.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList();
            if (lstRequestsProducts.Count > 0)     // != null)
            {
                gvGuarantees.DataSource = lstRequestsProducts;
                gvGuarantees.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Name");
                dt.Columns.Add("Count");
                dt.Columns.Add("Description");
                dt.Columns.Add("Address");
                dt.Columns.Add("MarketPrice");
                dt.Columns.Add("AssessedPrice");
                dt.Columns.Add("Coefficient");
               
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                gvGuarantees.DataSource = dt;
                gvGuarantees.DataBind();
            }
        }

        protected void gvGuarantees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGuarantees.EditIndex = -1;
            refreshGuarantees();
        }

        protected void gvGuarantees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGuarantees.EditIndex = e.NewEditIndex;
            refreshGuarantees();
        }

        protected void gvGuarantees_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "Del")
                {
                    ItemController ctl = new ItemController();
                    ctl.ItemDeleteGuarantees(id);
                    refreshGuarantees();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvGuarantees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            System.Web.UI.WebControls.Label lblID = (System.Web.UI.WebControls.Label)(gvGuarantees.Rows[e.RowIndex].FindControl("lblID"));
            System.Web.UI.WebControls.TextBox txtName = ((System.Web.UI.WebControls.TextBox)gvGuarantees.Rows[e.RowIndex].FindControl("txtName"));
            System.Web.UI.WebControls.TextBox txtCount = ((System.Web.UI.WebControls.TextBox)gvGuarantees.Rows[e.RowIndex].FindControl("txtCount"));
            System.Web.UI.WebControls.TextBox txtDescription = ((System.Web.UI.WebControls.TextBox)gvGuarantees.Rows[e.RowIndex].FindControl("txtDescription"));
            System.Web.UI.WebControls.TextBox txtAddress = ((System.Web.UI.WebControls.TextBox)gvGuarantees.Rows[e.RowIndex].FindControl("txtAddress"));
            System.Web.UI.WebControls.TextBox txtMarketPrice = ((System.Web.UI.WebControls.TextBox)gvGuarantees.Rows[e.RowIndex].FindControl("txtMarketPrice"));
            System.Web.UI.WebControls.TextBox txtAssessedPrice = ((System.Web.UI.WebControls.TextBox)gvGuarantees.Rows[e.RowIndex].FindControl("txtAssessedPrice"));
            System.Web.UI.WebControls.TextBox txtCoefficient = ((System.Web.UI.WebControls.TextBox)gvGuarantees.Rows[e.RowIndex].FindControl("txtCoefficient"));
            
            
            Guarantee edititem = new Guarantee();
           
            ItemController ctl = new ItemController();
            edititem = ctl.GetGuaranteeById(Convert.ToInt32(lblID.Text));
            edititem.Name = txtName.Text;
            edititem.Count = Convert.ToInt32(txtCount.Text);
            edititem.Description = txtDescription.Text;
            edititem.Address = txtAddress.Text;
            edititem.MarketPrice = Convert.ToDecimal(txtMarketPrice.Text);
            edititem.AssessedPrice = Convert.ToDecimal(txtAssessedPrice.Text);
            edititem.Coefficient  = Convert.ToDouble(txtCoefficient.Text); //Convert.ToDecimal(RadNumTbProductPrice.Text);
            ctl.GuaranteeUpd(edititem);

            //btnAddProductClick = 1;
            gvGuarantees.EditIndex = -1;
            refreshGuarantees();
        }

        protected void btnActAssessment_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Response.Redirect("actAssessment", true);
        }

        protected async void btnKIB_Click(object sender, EventArgs e)
        {
            

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //var res = await KIBRequestOB(hfCustomerID.Value, RadNumTbRequestSumm.Text, 1);
            var cust = dbR.Customers.Where(x => x.CustomerID == Convert.ToInt32(hfCustomerID.Value)).FirstOrDefault();
            if (cust != null)
            {
                //getDocFromCIB(cust.IdentificationNumber, cust.Surname.Trim() + " " + cust.CustomerName.Trim() + " " + cust.Otchestvo.Trim(), Convert.ToDateTime(cust.DateOfBirth).ToString("yyyy-MM-dd"), cust.DocumentSeries.Trim() + cust.DocumentNo.Trim(), "CreditinfoReportPlus");
                //string st = await getDocFromCIB2(cust.IdentificationNumber, cust.Surname.Trim() + " " + cust.CustomerName.Trim() + " " + cust.Otchestvo.Trim(), Convert.ToDateTime(cust.DateOfBirth).ToString("yyyy-MM-dd"), cust.DocumentSeries.Trim() + cust.DocumentNo.Trim(), "CreditinfoReportPlus");
                //getDocFromCIB("12303199400514", "Жоомарт кызы Зинат", "1994-03-23", "ID1081023");

                string pdfFile = await getDocFromCIB(cust.IdentificationNumber, cust.Surname.Trim() + " " + cust.CustomerName.Trim() + " " + cust.Otchestvo.Trim(), Convert.ToDateTime(cust.DateOfBirth).ToString("yyyy-MM-dd"), cust.DocumentSeries.Trim() + cust.DocumentNo.Trim(), "CreditinfoReportPlus");
                byte[] data = Convert.FromBase64String(pdfFile);

                string embed = "<embed src = \"{0}\" width = \"100%\" height = \"900\" alt = \"pdf\" pluginspage = \"http://www.adobe.com/products/acrobat/readstep2.html\">";
                //ltEmbed.Text = "sdsadsa";

                ltEmbed.Text = string.Format(embed, "data:application/pdf;base64," + Convert.ToBase64String(data, 0, data.Length));
            }
        }

        protected async void btnKIB2_Click(object sender, EventArgs e)
        {
            //var res = await KIBRequestOB(hfCustomerID.Value, RadNumTbRequestSumm.Text, 2);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //var res = await KIBRequestOB(hfCustomerID.Value, RadNumTbRequestSumm.Text, 1);
            var cust = dbR.Customers.Where(x => x.CustomerID == Convert.ToInt32(hfCustomerID.Value)).FirstOrDefault();
            if (cust != null)
            {
                string pdfFile = await getDocFromCIB(cust.IdentificationNumber, cust.Surname.Trim() + " " + cust.CustomerName.Trim() + " " + cust.Otchestvo.Trim(), Convert.ToDateTime(cust.DateOfBirth).ToString("yyyy-MM-dd"), cust.DocumentSeries.Trim() + cust.DocumentNo.Trim(), "CondensedReportPlus");
                byte[] data = Convert.FromBase64String(pdfFile);

                string embed = "<embed src = \"{0}\" width = \"100%\" height = \"900\" alt = \"pdf\" pluginspage = \"http://www.adobe.com/products/acrobat/readstep2.html\">";
                //ltEmbed.Text = "sdsadsa";

                ltEmbed.Text = string.Format(embed, "data:application/pdf;base64," + Convert.ToBase64String(data, 0, data.Length));
            }
        }

        protected void rbtnTypeOfIssue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnTypeOfIssue.SelectedValue == "Касса") { lblCardNumber.Visible = false; txtCardNumber.Visible = false; txtCardNumber.Text = "";  }
            if (rbtnTypeOfIssue.SelectedValue == "Карта") 
            { 
                lblCardNumber.Visible = true; txtCardNumber.Visible = true; 
                //editcommand(Convert.ToInt32(hfRequestID.Value)); 
            }
        }

        protected void AddNewGuarant(object sender, EventArgs e)
        {
            try
            {
                System.Web.UI.WebControls.TextBox Name = ((System.Web.UI.WebControls.TextBox)gvGuarantees.FooterRow.FindControl("txtName"));
                System.Web.UI.WebControls.TextBox Count = ((System.Web.UI.WebControls.TextBox)gvGuarantees.FooterRow.FindControl("txtCount"));
                System.Web.UI.WebControls.TextBox Description = ((System.Web.UI.WebControls.TextBox)gvGuarantees.FooterRow.FindControl("txtDescription"));
                System.Web.UI.WebControls.TextBox Address = ((System.Web.UI.WebControls.TextBox)gvGuarantees.FooterRow.FindControl("txtAddress"));
                System.Web.UI.WebControls.TextBox MarketPrice = ((System.Web.UI.WebControls.TextBox)gvGuarantees.FooterRow.FindControl("txtMarketPrice"));
                System.Web.UI.WebControls.TextBox AssessedPrice = ((System.Web.UI.WebControls.TextBox)gvGuarantees.FooterRow.FindControl("txtAssessedPrice"));
                System.Web.UI.WebControls.TextBox Coefficient = ((System.Web.UI.WebControls.TextBox)gvGuarantees.FooterRow.FindControl("txtCoefficient"));

                if ((!string.IsNullOrEmpty(Name.Text)) &&
                    (!string.IsNullOrEmpty(Count.Text)) &&
                    (!string.IsNullOrEmpty(Description.Text)) &&
                    (!string.IsNullOrEmpty(Address.Text)) &&
                    (!string.IsNullOrEmpty(MarketPrice.Text)) &&
                    //(!string.IsNullOrEmpty(AssessedPrice.Text)) &&
                    (!string.IsNullOrEmpty(Coefficient.Text)))
                {
                    if ((hfRequestStatus.Value != "Выдано") || (hfRequestStatus.Value != "На выдаче") && (hfRequestStatus.Value != "Отменено") || (hfRequestStatus.Value != "Отказано") && (hfRequestStatus.Value != "Подтверждено") || (hfRequestStatus.Value != "Принято") && (hfRequestStatus.Value != "Утверждено") || (hfRequestStatus.Value != "К подписи"))
                    {
                        ItemController ctl = new ItemController();
                        /*Тариф*/

                        Guarantee newGuarantee = new Guarantee
                        {
                            RequestID = Convert.ToInt32(hfRequestID.Value),
                            Name = Name.Text,
                            Count = Convert.ToInt32(Count.Text),
                            Description = Description.Text,
                            Address = Address.Text,
                            MarketPrice = Convert.ToDecimal(MarketPrice.Text),
                            AssessedPrice = Convert.ToDecimal(Convert.ToDecimal(MarketPrice.Text) * Convert.ToDecimal(Coefficient.Text)),
                            Coefficient = Convert.ToDouble(Coefficient.Text),

                        };
                        ctl.ItemGuaranteeAddItem(newGuarantee);
                        refreshGuarantees();
                        lblgvGuaranteesError.Text = "";
                    }
                    //refreshGrid();
                }
                else
                { lblgvGuaranteesError.Text = "Необходимо заполнить все поля"; }

            }
            catch (Exception ex)
            {
                lblgvGuaranteesError.Text = "Необходимо заполнить в числовом формате столбец (Количество)";
            }
        }




        [WebMethod]
        public string getDestinationFolder() //Возврат
        {
            string destinationFolder = Server.MapPath("~/") + partnerdir; // 1-вариант
            //string destinationFolder = @"E:\Uploadfiles\Credits\Dcb"; // 2-вариант
            //string destinationFolder = @"C:\Uploadfiles\Credits\Dcb"; // 2-вариант
            return destinationFolder;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Microcredit/Loans");
        }

        protected void btnNewRequest_Click1(object sender, EventArgs e)
        {
            //Response.Redirect("/Microcredit/Loan");
            //btnCustomerSearch_Click(new object(), new EventArgs());
            Response.Redirect("/Microcredit/Loan?RequestID=0");
            
        }

        protected void btnPhotoClose_Click(object sender, EventArgs e)
        {
            pnlPhoto.Visible = false;
            hfPhoto2.Value = "";
            Image1.ImageUrl = "";
            btnPhotoClose.Visible = false;
        }

        //protected void btnBack_Click1(object sender, EventArgs e)
        //{

        //}

        protected void btnBackToCreditForm_Click(object sender, EventArgs e)
        {
            pnlCredit.Visible = true;
            pnlMenuCustomer.Visible = false;
        }

        protected async void btnGetWorkPeriodInfoWithSum_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //var res = await KIBRequestOB(hfCustomerID.Value, RadNumTbRequestSumm.Text, 1);
            var cust = dbR.Customers.Where(x => x.CustomerID == Convert.ToInt32(hfCustomerID.Value)).FirstOrDefault();
            if (cust != null)
            {
                string pdfFile = await getDocFromSucfund(cust.IdentificationNumber, "/api/Socfund/SoapGetWorkPeriodInfoWithSumPdf");
                byte[] data = Convert.FromBase64String(pdfFile);

                string embed = "<embed src = \"{0}\" width = \"100%\" height = \"900\" alt = \"pdf\" pluginspage = \"http://www.adobe.com/products/acrobat/readstep2.html\">";
                //ltEmbed.Text = "sdsadsa";

                ltEmbed.Text = string.Format(embed, "data:application/pdf;base64," + Convert.ToBase64String(data, 0, data.Length));
            }
            
        }

        protected async void btnInitPermSF_Click(object sender, EventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            DateTime endDate = dt1.AddYears(1);
            endDate = endDate.AddDays(10);



            //btnInitPermSF.Enabled = false;
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);





           
            var cust = dbR.Customers.Where(x => x.CustomerID == Convert.ToInt32(hfCustomerID.Value)).FirstOrDefault();


            string regCustomerCountry = (cust.RegistrationCountryID != null) ? dbR.Countries.Where(v => v.CountryID == cust.RegistrationCountryID).ToList().FirstOrDefault().ShortName : "";
            string regCustomerCity = (cust.RegistrationCityID != null) ? dbR.Cities.Where(v => v.CityID == cust.RegistrationCityID).ToList().FirstOrDefault().CityName : "";
            string regCustomerStreet = dbR.Customers.Where(v => v.CustomerID == cust.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
            string regCustomerHouse = dbR.Customers.Where(v => v.CustomerID == cust.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
            string regCustomerFlat = dbR.Customers.Where(v => v.CustomerID == cust.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
            string RegCustomerAddress = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
            
            string resCustomerCountry = (cust.ResidenceCountryID != null) ? dbR.Countries.Where(v => v.CountryID == cust.ResidenceCountryID).ToList().FirstOrDefault().ShortName : "";
            string resCustomerCity = (cust.ResidenceCityID != null) ? dbR.Cities.Where(v => v.CityID == cust.ResidenceCityID).ToList().FirstOrDefault().CityName : "";
            string resCustomerStreet = dbR.Customers.Where(v => v.CustomerID == cust.CustomerID).ToList().FirstOrDefault().ResidenceStreet;
            string resCustomerHouse = dbR.Customers.Where(v => v.CustomerID == cust.CustomerID).ToList().FirstOrDefault().ResidenceHouse;
            string resCustomerFlat = dbR.Customers.Where(v => v.CustomerID == cust.CustomerID).ToList().FirstOrDefault().ResidenceFlat;
            string ResCustomerAddress = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;
           


            if (cust != null)
            {
                var res = await initPerm(cust.IdentificationNumber, cust.ContactPhone1, cust.Surname, cust.CustomerName, cust.Otchestvo, endDate.Date.ToString("yyyy-MM-dd"),
                ((DateTime)cust.DateOfBirth).ToString("yyyy-MM-dd"), RegCustomerAddress, ResCustomerAddress, cust.DocumentSeries + cust.DocumentNo,
                ((DateTime)cust.IssueDate).ToString("yyyy-MM-dd"), cust.IssueAuthority, cust.EMail, Session["UserName"].ToString());
                 

                MsgBox(res, this.Page, this);
                
                lblReqForInitPerm.Text = getText(res, "requestId:", ",oneTimePasswordRequired");
            }
            else lblReqForInitPerm.Text = "";


        }

        protected async void btnConfPermSF_Click(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            try
            {

                //if (result2 != "SubjectNotFound")
                {
                    HttpClient client = new HttpClient();

                    client.BaseAddress = new Uri(connectionStringKIB + "/api/Account/Login");
                    //string json = "{\"userName\": \"user01\" , \"password\": \"password123\"}";


                    //string json = "{\"userName\": \"" + Session["UserName"].ToString() + "\" , \"password\": \"" + Session["Password"].ToString() + "\"}";
                    string json = "{\"userName\": \"" + "r.tentiev" + "\" , \"password\": \"" + "Password1@3$" + "\"}";

                    var response = await client.PostAsync(connectionStringKIB + "/api/Account/Login", new StringContent(json, Encoding.UTF8, "application/json"));
                    var result = await response.Content.ReadAsStringAsync();
                    //string result3 = "res:" + result;
                    string token = getToken(result);
                    client.Dispose();
                    //    /****************************/


                    HttpClient client3 = new HttpClient();
                    client3.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client3.BaseAddress = new Uri(connectionStringKIB + "/api/Socfund/SoapSendConfirmationCodeForPermission");

                    var u3 = connectionStringKIB + "/api/Socfund/SoapSendConfirmationCodeForPermission";
                    //var json3 = "{\"IdNumber\": \"12303199400514\", \"typeReport\": \"CondensedReportPlus\"}";

                    var builder3 = new UriBuilder(u3);
                    builder3.Query = "RequestId=" + lblReqForInitPerm.Text  + "&Code=" + txtCodePermSF.Text + "&userName=" + Session["UserName"].ToString();
                    //builder3.Query = "typeReport=" + "CondensedReportPlus";
                    var url3 = builder3.ToString();

                    var response3 = await client3.PostAsync(url3, new StringContent("", Encoding.UTF8, "application/json"));
                    var result3byte = await response3.Content.ReadAsByteArrayAsync();
                    string result3string = await response3.Content.ReadAsStringAsync();
                    string res = JsonConvert.ToString(result3string);
                    client3.Dispose();
                    result3string = result3string.Replace("\"", "");
                    MsgBox(result3string, this.Page, this);
                    //byte[] byteArray = Convert.FromBase64String(result3string);


                    //return result3string;

                }


            }
            catch (Exception ex)
            {
                MsgBox(ex.ToString(), this.Page, this);
                //return null;
            }
        }

        class root3 : GeneralController.Root
        {
            public string IssueAccountNo { get; set; } = "";
        }

        protected async void btnGetPensionInfoWithSum_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //var res = await KIBRequestOB(hfCustomerID.Value, RadNumTbRequestSumm.Text, 1);
            var cust = dbR.Customers.Where(x => x.CustomerID == Convert.ToInt32(hfCustomerID.Value)).FirstOrDefault();
            if (cust != null)
            {
                string pdfFile = await getDocFromSucfund(cust.IdentificationNumber, "/api/Socfund/SoapGetPensionInfoWithSumPdf");
                byte[] data = Convert.FromBase64String(pdfFile);

                string embed = "<embed src = \"{0}\" width = \"100%\" height = \"900\" alt = \"pdf\" pluginspage = \"http://www.adobe.com/products/acrobat/readstep2.html\">";
                //ltEmbed.Text = "sdsadsa";

                ltEmbed.Text = string.Format(embed, "data:application/pdf;base64," + Convert.ToBase64String(data, 0, data.Length));
            }
        }

        class root3update : GeneralController.RootUpdate
        {
            public string IssueAccountNo { get; set; } = "";
        }


        protected string getText(string str, string str1, string str2)
        {
            int position1 = str.IndexOf(str1) + str1.Length;
            int position2 = str.IndexOf(str2);
            var conts = str.Substring(position1, position2 - position1);
            return conts;
        }


    }

}