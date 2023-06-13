using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat.Data.Card;
using System.Web.Helpers;
//using System.Windows.Forms;

namespace Zamat.Card
{
    public partial class CardP : System.Web.UI.Page
    {
        public int AgentID, OrgID;
        public static int btnAddProductClick;
        public string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        public string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        public string connectionStringZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        //OleDbConnection oledbConn;
        DateTime dateNowServer, dateNow;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if ((Session["Check"] == null) || (Session["Check"].ToString() != "true")) Response.Redirect("/Home");
                if (Session["FIO"] != null) { lblUserName.Text = Session["FIO"].ToString(); }
                dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
                dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
                dateNowServer = dbR.GetTable<SysInfo>().FirstOrDefault().DateOD;
                dateNow = Convert.ToDateTime(DateTime.Now);
                {

                    if (!IsPostBack)
                    {
                        databindDDL();
                        tbDate1b.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
                        tbDate2b.Text = DateTime.Today.Date.ToString("dd.MM.yyyy"); //"yyyy-MM-dd"
                        hfRequestAction.Value = "new";

                        btnAddProductClick = 1;
                        UpdateStockPrice();

                        isRole();

                        /**/
                    }
                    else
                    {
                        /*-----*/
                        isRole();
                    }
                    refreshGrid();

                }
            }
            catch (Exception exc) //Module failed to load
            {
                //Exceptions.ProcessModuleLoadException(this, exc);
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

            databindDDLRatePeriod();

        }

        public void databindDDLRatePeriod()
        {
            /**********/
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            //int usrID = Convert.ToInt32(Session["UserID"].ToString());
            var usr = dbRWZ.Users2.Where(r => r.UserName == Session["UserName"].ToString()).FirstOrDefault();
            int usrID = usr.UserID;
            int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
            int officeID = dbRWZ.Users2.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
            int? groupID = dbRWZ.Users2.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
            /***********/
        }

        private void UpdateStockPrice()
        {
            refreshGrid();
        }

        public void isRole()
        {
            if ((Convert.ToInt32(Session["RoleID"]) == 8) || (Convert.ToInt32(Session["RoleID"]) == 1)) //Агенты Билайн и Нур
            {

                btnIssue.Visible = false;
                btnConfirm.Visible = false;
                chkbxlistStatus.Visible = true;
                lblCardN.Visible = true;
                tbCardN.Visible = true;
                lblAccountN.Visible = false;
                tbAccountN.Visible = false;
                tbDate2b.Visible = true;

                btnForPeriod.Visible = true;
                btnProffer.Visible = false;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты
            {

                btnIssue.Visible = false;
                btnConfirm.Visible = false;
                btnFixed.Visible = false;
                chkbxlistStatus.Visible = true;
                lblCardN.Visible = false;
                tbCardN.Visible = false;
                lblAccountN.Visible = true;
                tbAccountN.Visible = true;
                tbDate2b.Visible = true;

                btnProffer.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 4) //Админы Нур
            {

                btnIssue.Visible = false;
                btnFixed.Visible = false;

                tbDate2b.Visible = true;
                chkbxlistStatus.Visible = true;
                lblCardN.Visible = true;
                tbCardN.Visible = true;
                lblAccountN.Visible = false;
                tbAccountN.Visible = false;

                btnProffer.Visible = false;
                btnForPeriod.Visible = true;
                btnForPeriodWithHistory.Visible = true;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
            {

                btnIssue.Visible = false;
                btnFixed.Visible = false;

                tbDate2b.Visible = true;
                chkbxlistStatus.Visible = true;
                lblCardN.Visible = true;
                tbCardN.Visible = true;
                lblAccountN.Visible = false;
                tbAccountN.Visible = false;

                btnForPeriod.Visible = true;
                btnForPeriodWithHistory.Visible = true;
                btnProffer.Visible = false;
            }
            if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ
            {

                btnIssue.Visible = false;
                btnConfirm.Visible = false;
                btnFixed.Visible = false;

                tbDate2b.Visible = true;
                chkbxlistStatus.Visible = true;
                lblCardN.Visible = true;
                tbCardN.Visible = true;
                lblAccountN.Visible = true;
                tbAccountN.Visible = true;

                btnProffer.Visible = true;
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
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //int usrID = Convert.ToInt32(Session["UserID"].ToString());
            var usr = dbRWZ.Users2.Where(r => r.UserName == Session["UserName"].ToString()).FirstOrDefault();
            int usrID = usr.UserID;
            int? agentRoleID = 8; //int? agentRoleID = db.RequestsUsersRoles.Where(r => (r.UserID == usrID)).FirstOrDefault().RoleID;
                                  //1-все заявки Ошка
                                  //3-Тур
                                  //6-все заявки Светофор
                                  //8-Билайн

            //var rle = Convert.ToInt32(Session["RoleID"]);
            var rle = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
            if (rle == 4) { agentRoleID = 1; }
            if (rle == 9) { agentRoleID = 8; }
            int? groupID = dbRWZ.Users2.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
            int? orgID = dbRWZ.Groups.Where(g => g.GroupID == groupID).FirstOrDefault().OrgID;
            List<CardRequest> lst;
            if (rle == 20) { lst = ItemController.GetRequestsAllForExpert(usrID, agentRoleID, nRequest, inn, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected); } //Эсперты ГБ Доскредо
            else if (rle == 19) { lst = ItemController.GetRequestsForExpert(usrID, agentRoleID, nRequest, inn, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected); } //Эксперты филиала Доскредо
            else if ((rle == 4) || (rle == 9)) { lst = ItemController.GetRequestsAllForAdmin(usrID, agentRoleID, nRequest, orgID, inn, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected); } //Админы Билайн
            else { lst = ItemController.GetRequestsForAgent(usrID, agentRoleID, nRequest, inn, surname, name, dt1, dt2, connectionStringR, chkbxlistStatus.Items[0].Selected, chkbxlistStatus.Items[1].Selected, chkbxlistStatus.Items[2].Selected, chkbxlistStatus.Items[3].Selected, chkbxlistStatus.Items[4].Selected, chkbxlistStatus.Items[5].Selected, chkbxlistStatus.Items[6].Selected, chkbxlistStatus.Items[7].Selected, chkbxlistStatus.Items[8].Selected, chkbxlistStatus.Items[9].Selected, chkbxlistStatus.Items[10].Selected); } //Агенты Нуртелеком
            if (lst.Count > 0)
            {
                var lst5 = (from r in lst
                            join b in dbR.Branches on r.BranchID equals b.ID
                            select new { r.RequestID, r.CreditID, b.ShortName, r.Surname, r.CustomerName, r.Otchestvo, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, r.GroupID, r.OrgID, r.StatusDate }).ToList();
                var lst6 = (from r in lst5
                            join g in dbRWZ.Groups on r.GroupID equals g.GroupID
                            select new { r.RequestID, r.CreditID, r.ShortName, r.Surname, r.CustomerName, r.Otchestvo, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, g.GroupName, r.OrgID, r.StatusDate }).ToList();
                var lst7 = (from r in lst6
                            join o in dbRWZ.Organizations on r.OrgID equals o.OrgID
                            select new { r.RequestID, r.CreditID, r.ShortName, r.Surname, r.CustomerName, r.Otchestvo, r.IdentificationNumber, r.CreditPurpose, r.ProductPrice, r.AmountDownPayment, r.RequestSumm, r.RequestDate, r.RequestStatus, r.GroupName, o.OrgName, r.StatusDate }).ToList();
                gvRequests.DataSource = lst7;
                gvRequests.DataBind();
            }
            else
            {
                gvRequests.DataSource = new string[] { };
                gvRequests.DataBind();
            }
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


        public void clearEditControlsRequest()
        {
            tbSurname2.Text = "";
            tbCustomerName2.Text = "";
            tbOtchestvo2.Text = "";
            tbINN2.Text = "";
            tbCardN.Text = "";
            tbAccountN.Text = "";
            tbEmail2.Text = "";
            tbCodeName2.Text = "";
            hfCustomerID.Value = "noselect";
            hfGuarantorID.Value = "noselect";
            btnCustomerSearch.Enabled = true;
        }

        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            byte[] gb = Guid.NewGuid().ToByteArray();
            int i = BitConverter.ToInt32(gb, 0);
            if (i > 0) { i = i * (-1); }

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

            /**/
            btnAddProductClick = 1; clearEditControls();
            clearEditControlsRequest();
            hfRequestAction.Value = "new";
            hfRequestStatus.Value = "";
            btnSendCreditRequest.Enabled = true;
            btnAgreement.Visible = false;
            btnPledgeAgreement.Visible = false;
            btnProffer.Visible = false;
            btnReceptionAct.Visible = false;
            btnIssue.Visible = false;
            pnlNewRequest.Visible = true;
            btnCloseRequest.Visible = true;
            btnSendCreditRequest.Visible = true;
            btnNewRequest.Visible = false;
            tbCodeName2.Visible = true;
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
            var CardRequestFiles = (from v in dbRWZ.CardRequestsFiles where (v.RequestID == reqIDDel) select v);
            foreach (CardRequestsFile rf in CardRequestFiles)
            {
                dbRWZ.CardRequestsFiles.DeleteOnSubmit(rf);
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
            //refreshProducts(1);
            refreshfiles();
            btnCustomerEdit.Enabled = false;
            gvHistory.DataSource = null;
            gvHistory.DataBind();
            //gvProducts.Enabled = true;
        }

        protected void btnSearchRequest_Click(object sender, EventArgs e)
        {
            hfRequestsRowID.Value = "";
            clearEditControlsRequest();
            pnlNewRequest.Visible = false;
            //refreshGrid();               
        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            pnlCredit.Visible = false;
            pnlMenuCustomer.Visible = true;
            pnlCustomer.Visible = true;
            hfCustomerID.Value = "noselect";
            btnCredit.Text = "Выбрать клиента";
            tbCardN.Text = "";
            tbAccountN.Text = "";
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
            tbContactPhone.Text = cust.ContactPhone1;
            tbResidenceHouse.Text = cust.ResidenceHouse;
            tbResidenceFlat.Text = cust.ResidenceFlat;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    if (FileUploadControl.PostedFile.ContentType == "image/jpeg")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 102400)
                        {
                            string filename = Path.GetFileName(FileUploadControl.FileName);
                            FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                            StatusLabel.Text = "Upload status: File uploaded!";
                        }
                        else
                            StatusLabel.Text = "Upload status: The file has to be less than 100 kb!";
                    }
                    else
                        StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

        //string portal = "C:\\DNN\\DNN803\\Portals\\0";
        protected string filedir = "Cards";
        protected string partnerdir = "Other";

        protected void btnUploadFiles_Click(object sender, EventArgs e)
        {
            string filename = "", fullfilename = "";
            tbFileDescription.Text = "";



            //if (hfAgentRoleID.Value) == 8) partnerdir = "";
            //if (Convert.ToInt32(Session["RoleID"]) == 8)  partnerdir = "Beeline"; //Агенты Билайн
            //if (Convert.ToInt32(Session["RoleID"]) == 1) partnerdir = "Beeline"; //Агенты Нур
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            //int usrID = Convert.ToInt32(Session["UserID"].ToString());
            //int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
            //if (usrRoleID == 8) partnerdir = "Beeline"; //Агенты Билайн
            //if (usrRoleID == 1) partnerdir = "GreenTelecom"; //Агенты Нур
            int? orgID = dbRWZ.CardRequests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault().OrgID;

            if (orgID == 2) partnerdir = "Greentelecom"; // Нур
            if (orgID == 3) partnerdir = "Beeline\\2"; // Билайн
            if (orgID == 7) partnerdir = "Beeline\\2"; // МТ


            if (FileUploadControl.HasFile)
            {
                try
                {
                    if (FileUploadControl.PostedFile.ContentType == "image/jpeg")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 102400)
                        {
                            filename = Path.GetFileName(FileUploadControl.FileName);
                            //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                            //StatusLabel.Text = "Upload status: File uploaded!";


                            //Telerik.Web.UI.UploadedFile file = AsyncUpload1.UploadedFiles[0];
                            //filename = file.FileName;
                            if (FileUploadControl.FileName != null)
                            {
                                fullfilename = UploadImageAndSave(true, FileUploadControl.FileName);
                                //string contentType = AsyncUpload1.UploadedFiles[0].ContentType;
                                string contentType = FileUploadControl.PostedFile.ContentType.ToLower();
                                {
                                    {
                                        CardRequestsFile newRequestFile = new CardRequestsFile
                                        {
                                            Name = filename,
                                            RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
                                            ContentType = contentType,
                                            //Data = bytes,
                                            //FullName = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + fullfilename,
                                            FullName = "\\Portals\\0\\" + filedir + "\\" + partnerdir + "\\" + fullfilename,
                                            FullName2 = "https://credit.doscredobank.kg\\" + "Portals\\0\\" + filedir + "\\" + partnerdir + "\\" + fullfilename,
                                            FileDescription = tbFileDescription.Text
                                        };
                                        ItemController ctl = new ItemController();
                                        ctl.ItemRequestFilesAddItem(newRequestFile);
                                    }
                                }
                                refreshfiles();
                            }

                        }
                        else
                            StatusLabel.Text = "Upload status: The file has to be less than 100 kb!";
                    }
                    else
                        StatusLabel.Text = "Upload status: Only JPEG files are accepted!";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }


            //Telerik.Web.UI.UploadedFile file = AsyncUpload1.UploadedFiles[0];
            //filename = file.FileName;
            //if (file.FileName != null)
            //{
            //    fullfilename = UploadImageAndSave(true, file.FileName);
            //    string contentType = AsyncUpload1.UploadedFiles[0].ContentType;
            //    {
            //        {
            //            CardRequestsFile newRequestFile = new CardRequestsFile
            //            {
            //                Name = filename,
            //                RequestID = Convert.ToInt32(Convert.ToInt32(hfRequestID.Value)),
            //                ContentType = contentType,
            //                //Data = bytes,
            //                FullName = PortalSettings.HomeDirectory + filedir + "\\" + partnerdir + "\\" + fullfilename,
            //                FileDescription = tbFileDescription.Text
            //            };
            //            ItemController ctl = new ItemController();
            //            ctl.ItemRequestFilesAddItem(newRequestFile);
            //        }
            //    }
            //    refreshfiles();
            //}
        }

        protected string UploadImageAndSave(bool hasfile, string filename) //main function
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            int? orgID = dbRWZ.CardRequests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault().OrgID;

            if (orgID == 2) partnerdir = "GreenTelecom"; // Нур
            if (orgID == 3) partnerdir = "Beeline\\2"; // Билайн
            if (orgID == 7) partnerdir = "Beeline\\2"; // МТ

            if (hasfile)
            {
                CheckImageDirs();

                string filepath = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + filename;
                int temp_ext = 0;
                while (System.IO.File.Exists(filepath))
                {
                    temp_ext = DateTime.Now.Millisecond;
                    string ext_name = System.IO.Path.GetExtension(filepath);
                    string filename_no_ext = System.IO.Path.GetFileNameWithoutExtension(filepath) + "_" + temp_ext;
                    filename = filename_no_ext + temp_ext + ext_name;
                    filepath = Server.MapPath("~/") + filedir + "\\" + partnerdir + "\\" + filename;
                }
                string path = System.IO.Path.GetFileName(filename);
                //AsyncUpload1.UploadedFiles[0].SaveAs(filepath);
                //FileUploadControl.SaveAs(Server.MapPath("~/") + filepath);
                FileUploadControl.SaveAs(filepath);
            }
            return filename;
        }

        protected void CheckImageDirs()
        {
            if (!System.IO.Directory.Exists(Server.MapPath("~/") + filedir + partnerdir))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/") + filedir + partnerdir);

            if (!System.IO.Directory.Exists(Server.MapPath("~/") + filedir + partnerdir))
                System.IO.Directory.CreateDirectory(Server.MapPath("~/") + filedir + partnerdir);
        }


        protected void lbHistory_Click(object sender, EventArgs e)
        {
            pnlHistory.Visible = true;
            lbHistory.Visible = false;
            lbBack.Visible = true;
            int id = Convert.ToInt32(hfRequestID.Value);
            history(id);
        }

        public void history(int id)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var history = from his in dbRWZ.CardRequestsHistories
                          where (his.RequestID == id)
                          join usr in dbRWZ.Users2 on his.AgentID equals usr.UserID
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

        protected void lbBack_Click(object sender, EventArgs e)
        {
            pnlHistory.Visible = false;
            lbHistory.Visible = true;
            lbBack.Visible = false;
        }

        protected void btnFix_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            CardRequest editRequest = new CardRequest();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
            editRequest.RequestStatus = "Исправить";
            editRequest.StatusDate = dateTimeNow;
            ctlItem.RequestUpd(editRequest);
            refreshGrid();
            string hexRed = "#E47E11";
            Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
            lblStatusRequest.BackColor = _colorRed; hfRequestStatus.Value = "Исправить";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            CardRequestsHistory newItem = new CardRequestsHistory()
            {
                AgentID = usrID,
                //CreditID = Convert.ToInt32(hfCreditID.Value),
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
            CardRequest editRequest = new CardRequest();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
            editRequest.RequestStatus = "Исправлено";
            ctlItem.RequestUpd(editRequest);
            refreshGrid();
            string hexRed = "#E47E11";
            Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
            lblStatusRequest.BackColor = _colorRed; hfRequestStatus.Value = "Исправлено";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            CardRequestsHistory newItem = new CardRequestsHistory()
            {
                AgentID = usrID,
                //CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Исправлено",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void Confirm()
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            DateTime dateTimeNow = dateNow;
            var lst5 = (from v in dbRWZ.CardRequests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Подтверждено";
                lst5.StatusDate = dateTimeNow;
                dbRWZ.CardRequests.Context.SubmitChanges();
                refreshGrid();
                string hexYellow = "#fdf404";
                Color _colorYellow = System.Drawing.ColorTranslator.FromHtml(hexYellow);
                lblStatusRequest.BackColor = _colorYellow; hfRequestStatus.Value = "Подтверждено";
                /*RequestHistory*/

                CardRequestsHistory newItem = new CardRequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    //CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "Подтверждено",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value) // requestID
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
                //var Customer = dbR.CardRequests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
                ////SendMailTo(lst5.BranchID, "Подтверждено", Customer.Surname + " " + Customer.Surname + " " + Customer.Otchestvo, false, false, true); //экспертам
            }
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            Approved();
        }

        public void disableUpoadFiles()
        {
            //AsyncUpload1.Enabled = false;
            tbFileDescription.Enabled = false;
            btnUploadFiles.Enabled = false;
            gvRequestsFiles.DataSource = null;
            gvRequestsFiles.DataBind();
        }

        protected void btnActivated_Click(object sender, EventArgs e)
        {
            Activated();
        }


        private void Activated()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            DateTime dateTimeNow = dateNow;
            var lst5 = (from v in dbRWZ.CardRequests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Активировано";
                lst5.StatusDate = dateTimeNow;
                dbRWZ.CardRequests.Context.SubmitChanges();
                refreshGrid();
                string hexGreen = "#7cfa84";
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "Активировано";
                /*RequestHistory*/

                CardRequestsHistory newItem = new CardRequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    //CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "Активировано",
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

        protected void btnCancelReq_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            CardRequest editRequest = new CardRequest();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
            editRequest.RequestStatus = "Отменено";
            editRequest.StatusDate = dateTimeNow;
            ctlItem.RequestUpd(editRequest);
            refreshGrid();
            string hexBlack = "#878787";
            Color _colorBlack = System.Drawing.ColorTranslator.FromHtml(hexBlack);
            lblStatusRequest.BackColor = _colorBlack; hfRequestStatus.Value = "Отменено";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            CardRequestsHistory newItem = new CardRequestsHistory()
            {
                AgentID = usrID,
                //CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Отменено",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }

        protected void btnCancelReqExp_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            CardRequest editRequest = new CardRequest();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
            editRequest.RequestStatus = "Отказано";
            editRequest.StatusDate = dateTimeNow;
            ctlItem.RequestUpd(editRequest);
            refreshGrid();
            string hexBlack = "#878787";
            Color _colorBlack = System.Drawing.ColorTranslator.FromHtml(hexBlack);
            lblStatusRequest.BackColor = _colorBlack; hfRequestStatus.Value = "Отказано";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            CardRequestsHistory newItem = new CardRequestsHistory()
            {
                AgentID = usrID,
                //CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Отказано",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            DateTime dateTimeNow = dateNow;
            var lst5 = (from v in dbRWZ.CardRequests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Выдано";
                lst5.StatusDate = dateTimeNow;
                dbRWZ.CardRequests.Context.SubmitChanges();
                refreshGrid();
                string hexBlue = "#227128";
                Color _colorBlue = System.Drawing.ColorTranslator.FromHtml(hexBlue);
                lblStatusRequest.BackColor = _colorBlue; hfRequestStatus.Value = "Выдано";
                /*RequestHistory*/

                CardRequestsHistory newItem = new CardRequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    //CreditID = Convert.ToInt32(hfCreditID.Value),
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                    Status = "Выдано",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value)
                };
                CreditController ctx = new CreditController();
                ctx.ItemRequestHistoriesAddItem(newItem);
            }
        }

        protected void btnReceived_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            CardRequest editRequest = new CardRequest();
            ItemController ctlItem = new ItemController();
            editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
            editRequest.RequestStatus = "Принято";
            editRequest.StatusDate = dateTimeNow;
            ctlItem.RequestUpd(editRequest);
            refreshGrid();
            string hexReceive = "#11ACE4";
            Color _colorReceive = System.Drawing.ColorTranslator.FromHtml(hexReceive);
            lblStatusRequest.BackColor = _colorReceive; hfRequestStatus.Value = "Принято";
            /*****************************/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            CardRequestsHistory newItem = new CardRequestsHistory()
            {
                AgentID = usrID,
                //CreditID = Convert.ToInt32(hfCreditID.Value),
                CustomerID = Convert.ToInt32(hfCustomerID.Value),
                StatusDate = dateTimeNow, //Convert.ToDateTime(DateTime.Now),
                Status = "Принято",
                note = tbNote.Text,
                RequestID = Convert.ToInt32(hfRequestID.Value)
            };
            ctx.ItemRequestHistoriesAddItem(newItem);
            /******************************************************/
        }

        protected void btnAgreement_Click(object sender, EventArgs e)
        {
            string worktype = "0";
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["UserID"] = Convert.ToInt32(Session["UserID"].ToString());
            Session["RequestID"] = hfRequestID.Value;
            Session["WorkType"] = worktype;
            //Response.Redirect(this.EditUrl("", "", "rptAgrees"));
            Response.Redirect("/Card/rptAgrees");
        }

        protected void btnSendCreditRequest_Click(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var cardN = dbRWZ.CardRequests.Where(r => r.CardN == tbCardN.Text).ToList();

            if (hfRequestAction.Value == "new")
            {
                if ((cardN.Count > 0) && (Convert.ToInt32(Session["RoleID"]) != 19))
                {
                    //****** MsgBox("Проверьте номер карты, карта с таким номером используется", this.Page, this);
                    System.Windows.Forms.MessageBox.Show("Проверьте номер карты, карта с таким номером используется");
                    gvAllReqCust.DataSource = cardN;
                    gvAllReqCust.DataBind();
                }
                if (hfCustomerID.Value != "noselect" && Page.IsValid) //условие для поруч
                {
                    AddRequest();
                }
                else
                {
                    if (hfCustomerID.Value == "noselect") {
                        //******MsgBox("Перед сохранением заявки нужно выбрать клиента", this.Page, this);
                        System.Windows.Forms.MessageBox.Show("Перед сохранением заявки нужно выбрать клиента");
                        lblMessageClient.Text = "Перед сохранением заявки нужно выбрать клиента";
                    }
                    if (!Page.IsValid) {
                        //*****MsgBox("Нужно заполнить все поля заявки", this.Page, this);
                        System.Windows.Forms.MessageBox.Show("Нужно заполнить все поля заявки");
                    }
                }
            }
            else
            {
                if (hfCustomerID.Value != "noselect" && Page.IsValid) //условие для поруч
                {
                    AddRequest();
                }
                else
                {
                    if (hfCustomerID.Value == "noselect") {
                        //*****MsgBox("Перед сохранением заявки нужно выбрать клиента", this.Page, this);
                        System.Windows.Forms.MessageBox.Show("Перед сохранением заявки нужно выбрать клиента");
                        lblMessageClient.Text = "Перед сохранением заявки нужно выбрать клиента"; }
                    if (!Page.IsValid) {
                        //*****MsgBox("Нужно заполнить все поля заявки", this.Page, this);
                        System.Windows.Forms.MessageBox.Show("Нужно заполнить все поля заявки");
                    }
                }
            }
        }


        /***************************/
        protected void AddRequest()
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            if (hfRequestAction.Value == "new")
            {
                /**/
                /*Новая заявка*/
                int usrID = Convert.ToInt32(Session["UserID"].ToString());
                int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
                if (usrRoleID == 4) { usrRoleID = 1; }
                if (usrRoleID == 9) { usrRoleID = 8; }
                int officeID = dbRWZ.Users2.Where(r => r.UserID == usrID).FirstOrDefault().OfficeID;
                int? groupID = dbRWZ.Users2.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
                int? GroupCode = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().GroupCode;
                int? orgID = dbRWZ.Groups.Where(r => r.GroupID == groupID).FirstOrDefault().OrgID;
                int branchID = dbR.Offices.Where(r => r.ID == officeID).FirstOrDefault().BranchID;
                int? credOfficerID = 4583; //по умолч АЙбек
                credOfficerID = dbRWZ.RequestsRedirect.Where(r => r.branchID == branchID).FirstOrDefault().creditOfficerID;
                hfAgentRoleID.Value = usrRoleID.ToString();
                DateTime dateTimeServer = dateNowServer;
                DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now);
                decimal commision = 0; string NameOfCredit = "Замат"; int prodID = 1152; //замат
                string reqStatus = "Новая заявка";
                if (orgID == 2) reqStatus = "Новая заявка";
                if (orgID == 3) reqStatus = "Подтверждено";
                if (orgID == 7) reqStatus = "Подтверждено";
                CreditController ctx = new CreditController();
                CardRequest newRequest = new CardRequest
                {
                    //CardN = tbCardN.Text,
                    CardN = (tbCardN.Visible == true) ? tbCardN.Text : "",
                    //AccountN = tbAccountN.Text,
                    AccountN = (tbAccountN.Visible == true) ? tbAccountN.Text : "",
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
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
                    RequestStatus = reqStatus,
                    //CreditID = CreditsHistoriesID,
                    BranchID = branchID,
                    GroupID = groupID,
                    OrgID = orgID,
                    StatusDate = dateTimeNow
                };
                ItemController ctl = new ItemController();
                int requestID = ctl.ItemRequestAddItem(newRequest);
                /*RequestHistory*//*----------------------------------------------------*/
                CardRequestsHistory newItem = new CardRequestsHistory()
                {
                    AgentID = usrID,
                    //CreditID = CreditsHistoriesID,
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow,
                    Status = "Новая заявка",
                    note = tbNote.Text,
                    RequestID = requestID
                };
                ctx.ItemRequestHistoriesAddItem(newItem);
                /************************************************************************/
                if (Convert.ToInt32(Session["RoleID"]) == 8)
                {
                    SysController ctxl = new SysController();
                    Customer cust = ctxl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
                    cust.EMail = tbEmail2.Text;
                    cust.Codename = tbCodeName2.Text;
                    ctxl.CustomerUpdItem(cust);
                }
                /*Products*//*----------------------------------------------------*/
                int reqID = Convert.ToInt32(hfRequestID.Value);

                /*****************************/
                /*Files*/
                var CardRequestFiles = (from v in dbRWZ.CardRequestsFiles where (v.RequestID == reqID) select v);
                foreach (CardRequestsFile rf in CardRequestFiles)
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
                //{ Response.Redirect("/ExpertPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 3)
                //{ Response.Redirect("/AgentTour"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 4)
                //{ Response.Redirect("/AdminPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 5)
                //{ Response.Redirect("/ExpertPage"); }
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
                //{ Response.Redirect("/AgentTechnodom"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 14)
                //{ Response.Redirect("/AdminTechnodom"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 16)
                //{ Response.Redirect("/AgentPlaneta"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 17)
                //{ Response.Redirect("/AdminPlaneta"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 18)
                //{ Response.Redirect("/BeelineOnline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 19)
                //{ Response.Redirect("/OperCards"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 20)
                //{ Response.Redirect("/OperCards"); }
                Response.Redirect("/Card/CardP");


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


                /*----------------------------------------------------*/
                if (Convert.ToInt32(Session["RoleID"]) == 8)
                {
                    CardRequest editRequest = new CardRequest();
                    ItemController ctlItem = new ItemController();
                    //editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
                    editRequest.Surname = tbSurname2.Text;
                    editRequest.CustomerName = tbCustomerName2.Text;
                    editRequest.Otchestvo = tbOtchestvo2.Text;
                    editRequest.IdentificationNumber = tbINN2.Text;
                    editRequest.GuarantorID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0;
                    editRequest.CardN = tbCardN.Text;
                    //editRequest.AccountN = tbAccountN.Text;
                    editRequest.StatusDate = dateTimeNow;
                    ctlItem.AgentRequestUpd(editRequest);
                }
                if (Convert.ToInt32(Session["RoleID"]) == 9)
                {
                    CardRequest editRequest = new CardRequest();
                    ItemController ctlItem = new ItemController();
                    //editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
                    editRequest.Surname = tbSurname2.Text;
                    editRequest.CustomerName = tbCustomerName2.Text;
                    editRequest.Otchestvo = tbOtchestvo2.Text;
                    editRequest.IdentificationNumber = tbINN2.Text;
                    editRequest.GuarantorID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0;
                    editRequest.CardN = tbCardN.Text;
                    //editRequest.AccountN = tbAccountN.Text;
                    ctlItem.AgentRequestUpd(editRequest);
                }
                if (Convert.ToInt32(Session["RoleID"]) == 19)
                {
                    CardRequest editRequest = new CardRequest();
                    ItemController ctlItem = new ItemController();
                    //editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
                    editRequest.Surname = tbSurname2.Text;
                    editRequest.CustomerName = tbCustomerName2.Text;
                    editRequest.Otchestvo = tbOtchestvo2.Text;
                    editRequest.IdentificationNumber = tbINN2.Text;
                    editRequest.GuarantorID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0;
                    //editRequest.CardN = tbCardN.Text;
                    editRequest.AccountN = tbAccountN.Text;
                    ctlItem.ExpertRequestUpd(editRequest);
                }
                if (Convert.ToInt32(Session["RoleID"]) == 20)
                {
                    CardRequest editRequest = new CardRequest();
                    ItemController ctlItem = new ItemController();
                    //editRequest = ctlItem.GetRequestByCreditID(Convert.ToInt32(hfCreditID.Value));
                    editRequest = ctlItem.GetRequestByRequestID(Convert.ToInt32(hfRequestID.Value));
                    editRequest.Surname = tbSurname2.Text;
                    editRequest.CustomerName = tbCustomerName2.Text;
                    editRequest.Otchestvo = tbOtchestvo2.Text;
                    editRequest.IdentificationNumber = tbINN2.Text;
                    editRequest.GuarantorID = (hfGuarantorID.Value != "noselect") ? Convert.ToInt32(hfGuarantorID.Value) : 0;
                    editRequest.CardN = tbCardN.Text;
                    editRequest.AccountN = tbAccountN.Text;
                    ctlItem.RequestUpd(editRequest);
                }
                /*****************************/
                /*RequestHistory*//*----------------------------------------------------*/
                CreditController ctx = new CreditController();
                CardRequestsHistory newItem = new CardRequestsHistory()
                {
                    AgentID = usrID,
                    //CreditID = Convert.ToInt32(hfCreditID.Value),
                    CustomerID = Convert.ToInt32(hfCustomerID.Value),
                    StatusDate = dateTimeNow,
                    Status = "Редактирование",
                    note = tbNote.Text,
                    RequestID = Convert.ToInt32(hfRequestID.Value)
                };
                ctx.ItemRequestHistoriesAddItem(newItem);
                /********Update Salary Customers**********/
                SysController ctxl = new SysController();
                Customer cust = ctxl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
                cust.EMail = tbEmail2.Text;
                cust.Codename = tbCodeName2.Text;
                ctxl.CustomerUpdItem(cust);
                /******************************************************/
                refreshGrid();
                clearEditControlsRequest();
                hfRequestAction.Value = "";
                //if (Convert.ToInt32(Session["RoleID"]) == 1)
                //{ Response.Redirect("/AgentPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 2)
                //{ Response.Redirect("/ExpertPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 3)
                //{ Response.Redirect("/AgentTour"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 4)
                //{ Response.Redirect("/AdminPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 5)
                //{ Response.Redirect("/ExpertPage"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 6)
                //{ Response.Redirect("/AgentSvetofor"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 7)
                //{ Response.Redirect("/AdminSvetofor"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 8)
                //{ Response.Redirect("/AgentCards"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 9)
                //{ Response.Redirect("/AgentCards"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 10)
                //{ Response.Redirect("/ExpertBeeline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 12)
                //{ Response.Redirect("/NurOnline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 13)
                //{ Response.Redirect("/AgentTechnodom"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 14)
                //{ Response.Redirect("/AdminTechnodom"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 16)
                //{ Response.Redirect("/AgentPlaneta"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 17)
                //{ Response.Redirect("/AdminPlaneta"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 18)
                //{ Response.Redirect("/BeelineOnline"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 19)
                //{ Response.Redirect("/OperCards"); }
                //if (Convert.ToInt32(Session["RoleID"]) == 20)
                //{ Response.Redirect("/OperCards"); }
                Response.Redirect("/Card/CardP");
                pnlNewRequest.Visible = false;
            }
        }
        /****************************/

        public void refreshfiles()
        {
            /*RequestsFiles*/
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var lstRequestFiles = dbRWZ.CardRequestsFiles.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).ToList();
            if (lstRequestFiles != null)
            {
                gvRequestsFiles.DataSource = lstRequestFiles;
                gvRequestsFiles.DataBind();
            }
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

            //btnNoConfirm.Visible = false; //статус не подтвержден
            btnFixed.Visible = false; //статус новая
            btnConfirm.Visible = false; //статус подтвердить
            btnIssue.Visible = false; //статус выдать
            btnCancelReq.Visible = false; //статус отменить
            btnCancelReqExp.Visible = false; //статус отказать
            btnReceived.Visible = false; //статус принято
        }

        protected void btnSearchClient_Click(object sender, EventArgs e)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            var query = (from u in dbR.Customers where ((u.IdentificationNumber == tbSearchINN.Text) && (u.DocumentSeries == tbSerialN.Text.Substring(0, 5)) && (u.DocumentNo == tbSerialN.Text.Substring(5, 4))) select u).ToList().FirstOrDefault();
            if (query != null)
            {
                pnlCustomer.Visible = true;
                pnlCredit.Visible = false;
                btnSaveCustomer.Enabled = false;
                btnCredit.Enabled = true;
                hfCustomerID.Value = query.CustomerID.ToString();
                tbSurname.Text = query.Surname;
                tbCustomerName.Text = query.CustomerName;
                tbOtchestvo.Text = query.Otchestvo;
                if (query.IsResident == true) { rbtIsResident.SelectedIndex = 0; } else { rbtIsResident.SelectedIndex = 1; }
                tbIdentificationNumber.Text = query.IdentificationNumber;
                tbDocumentSeries.Text = query.DocumentSeries + query.DocumentNo;
                tbIssueDate.Text = Convert.ToDateTime(query.IssueDate).ToString("dd.MM.yyyy");
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
                tbEmail.Text = query.EMail;
                tbEmail2.Text = query.EMail;
                tbCodeName.Text = query.Codename;
                tbCodeName2.Text = query.Codename;

                //if ((query.Codename != null) || (query.Codename != ""))
                //{ lblCodeWord.Visible = false; tbCodeName2.Visible = false; }
                //else { lblCodeWord.Visible = true; tbCodeName2.Visible = true; }

                {

                    if (query.Codename == null)
                    { lblCodeWord.Visible = true; tbCodeName2.Visible = true; }
                    else { lblCodeWord.Visible = false; tbCodeName2.Visible = false; }

                    if ((query.Codename == ""))
                    { lblCodeWord.Visible = true; tbCodeName2.Visible = true; }
                    else { lblCodeWord.Visible = false; tbCodeName2.Visible = false; }

                }

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


            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;

            //var cards01 = dbRWZ.CardRequests.Where(r => ((r.IdentificationNumber == tbIdentificationNumber.Text) && (usrRoleID == r.AgentRoleID) && ((r.RequestStatus == "Активировано") || (r.RequestStatus == "Выдано") || r.RequestStatus == "Утверждено") || r.RequestStatus == "Подтверждено")).ToList();
            var cards01 = dbRWZ.CardRequests.Where(r => (r.IdentificationNumber == tbIdentificationNumber.Text)).ToList();
            var cards02 = cards01.Where(r => r.AgentRoleID == usrRoleID).ToList();
            var cards03 = cards02.Where(r => ((usrRoleID == r.AgentRoleID) && ((r.RequestStatus == "Активировано") || (r.RequestStatus == "Выдано") || r.RequestStatus == "Утверждено") || r.RequestStatus == "Подтверждено")).ToList();
            gvAllReqCustomer.DataSource = cards03;
            gvAllReqCustomer.DataBind();
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

        protected void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (hfCustomerID.Value == "noselect")
                {
                    //dbdataDataContext db = new dbdataDataContext(connectionString);
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
                            EMail = tbEmail.Text,
                            Codename = tbCodeName.Text
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
                       //**** DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Клиент есть в базе, сделайте поиск по ИНН", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
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
                    cust.DocumentSeries = tbDocumentSeries.Text.Substring(0, 5);
                    cust.DocumentNo = tbDocumentSeries.Text.Substring(5, 4);
                    cust.IssueDate = Convert.ToDateTime(tbIssueDate.Text.Substring(3, 2) + "." + tbIssueDate.Text.Substring(0, 2) + "." + tbIssueDate.Text.Substring(6, 4));
                    cust.DocumentValidTill = Convert.ToDateTime(tbValidTill.Text.Substring(3, 2) + "." + tbValidTill.Text.Substring(0, 2) + "." + tbValidTill.Text.Substring(6, 4));
                    cust.IssueAuthority = tbIssueAuthority.Text;
                    cust.Sex = (rbtSex.SelectedIndex == 0) ? true : false;
                    cust.DateOfBirth = Convert.ToDateTime(tbDateOfBirth.Text.Substring(3, 2) + "." + tbDateOfBirth.Text.Substring(0, 2) + "." + tbDateOfBirth.Text.Substring(6, 4));
                    cust.RegistrationStreet = tbRegistrationStreet.Text;
                    cust.RegistrationHouse = tbRegistrationHouse.Text;
                    cust.RegistrationFlat = tbRegistrationFlat.Text;
                    cust.ResidenceStreet = tbResidenceStreet.Text;
                    cust.ResidenceHouse = tbResidenceHouse.Text;
                    cust.ResidenceFlat = tbResidenceFlat.Text;
                    cust.ContactPhone1 = tbContactPhone.Text;
                    cust.EMail = tbEmail.Text;
                    cust.Codename = tbCodeName.Text;
                    ctx.CustomerUpdItem(cust);
                    pnlCredit.Visible = true;
                    pnlMenuCustomer.Visible = false;
                    pnlCustomer.Visible = false;
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
                //refreshProducts(2);
                LinkButton lb = e.CommandSource as LinkButton;
                GridViewRow gvr = lb.Parent.Parent as GridViewRow;
                //gvr.BackColor = System.Drawing.Color.Empty;
                string hex = "#cbceea";
                Color _color = System.Drawing.ColorTranslator.FromHtml(hex);
                gvr.BackColor = _color;
                hfRequestsRowID.Value = gvr.RowIndex.ToString();
            }
        }

        public void editcommand(int id)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            try
            {
                pnlNewRequest.Visible = true;
                var lst = dbRWZ.CardRequests.Where(r => r.RequestID == id).FirstOrDefault();
                hfCustomerID.Value = lst.CustomerID.ToString();
                hfCreditID.Value = lst.CreditID.ToString();
                hfRequestID.Value = id.ToString();
                tbCardN.Text = lst.CardN;
                tbAccountN.Text = lst.AccountN;
                tbSurname2.Text = lst.Surname;
                tbCustomerName2.Text = lst.CustomerName;
                tbOtchestvo2.Text = lst.Otchestvo;
                tbINN2.Text = lst.IdentificationNumber;

                var query = (from u in dbR.Customers where (u.CustomerID == lst.CustomerID) select u).ToList().FirstOrDefault();
                if (query != null)
                {
                    tbCodeName2.Text = query.Codename;
                    tbEmail2.Text = query.EMail;

                    if (query.Codename == null)
                    { lblCodeWord.Visible = true; tbCodeName2.Visible = true; }
                    else { lblCodeWord.Visible = false; tbCodeName2.Visible = false; }

                    if ((query.Codename == ""))
                    { lblCodeWord.Visible = true; tbCodeName2.Visible = true; }
                    else { lblCodeWord.Visible = false; tbCodeName2.Visible = false; }
                }


                if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                {
                    lblCodeWord.Visible = true; tbCodeName2.Visible = true;
                }

                lblStatusRequest.Text = "&nbsp;&nbsp;" + lst.RequestStatus + "&nbsp;&nbsp; ";
                string hexRed = "#E47E11", hexRed2 = "#E45E11", hexOrange = "#8C5E40", hexYellow = "#fdf404", hexGreen = "#7cfa84", hexBlue = "#227128", hexBlack = "#878787", hexReceive = "#11ACE4";
                Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
                Color _colorRed2 = System.Drawing.ColorTranslator.FromHtml(hexRed2);
                Color _colorOrange = System.Drawing.ColorTranslator.FromHtml(hexOrange);
                Color _colorYellow = System.Drawing.ColorTranslator.FromHtml(hexYellow);
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                Color _colorBlue = System.Drawing.ColorTranslator.FromHtml(hexBlue);
                Color _colorBlack = System.Drawing.ColorTranslator.FromHtml(hexBlack);
                Color _colorReceive = System.Drawing.ColorTranslator.FromHtml(hexReceive);
                if (lst.RequestStatus == "Отменено") { lblStatusRequest.BackColor = _colorBlack; hfRequestStatus.Value = "Отменено"; };
                if (lst.RequestStatus == "Отказано") { lblStatusRequest.BackColor = _colorBlack; hfRequestStatus.Value = "Отказано"; };
                if (lst.RequestStatus == "Новая заявка") { lblStatusRequest.BackColor = _colorRed; hfRequestStatus.Value = "Новая заявка"; };
                if (lst.RequestStatus == "Исправить") { lblStatusRequest.BackColor = _colorRed2; hfRequestStatus.Value = "Исправить"; };
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
                /***********************/
                history(id);
                btnCustomerSearch.Enabled = false;
                btnCloseRequest.Visible = true; //закры форму заявки
                btnNewRequest.Visible = true; //новая заявка
                btnProffer.Visible = false; //предложение зп
                //btnNoConfirm.Visible = false; //статус не подтвержден
                btnFixed.Visible = false; //статус новая
                btnConfirm.Visible = false; //статус подтвердить
                btnIssue.Visible = false; //статус выдать
                btnCancelReq.Visible = false; //статус отменить
                btnCancelReqExp.Visible = false; //статус отказать
                btnReceived.Visible = false; //статус принято
                btnSendCreditRequest.Visible = false; //Сохранить
                //pnlEmployment.Visible = false; // зп
                btnAgreement.Visible = false; //договор
                //chbEmployer.Enabled = false; //Сотрудник
                //btnInProcess.Visible = false; //
                btnApproved.Visible = false; //Утверждено
                btnActivated.Visible = false; //Активировано

                //btnSignature.Visible = false;
                if (lst.RequestStatus == "Новая заявка")
                {
                    btnSendCreditRequest.Visible = true;
                    if (Convert.ToInt32(Session["RoleID"]) == 1) //Агенты Нур
                    {
                        btnCancelReq.Visible = true;  //Отменено
                        btnFixed.Visible = true; //Исправлено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnCancelReq.Visible = true;  //Отменено
                        //btnFixed.Visible = true; //Исправлено
                        btnConfirm.Visible = true; //Подтверждено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }


                    if ((Convert.ToInt32(Session["RoleID"]) == 4) || (Convert.ToInt32(Session["RoleID"]) == 9)) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnFix.Visible = true; //Исправить
                        //btnFixed.Visible = true; //Исправлено
                        btnConfirm.Visible = true; //Подтверждено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор

                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты
                    {
                        btnCancelReqExp.Visible = true;  //Отказано
                        //btnConfirm.Visible = true; //Подтверждено
                        btnFix.Visible = true; //Исправить
                        //btnApproved.Visible = true; //Утверждено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        //btnActivated.Visible = true; //Активировано
                        //btnConfirm.Visible = true; //Подтверждено
                        //btnApproved.Visible = true; //Утверждено
                        btnFix.Visible = true; //Исправить

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                }
                //if (lst.RequestStatus == "В обработке")
                //{
                //    //gvProducts.Enabled = true;
                //    btnSendCreditRequest.Visible = true;
                //    if (Convert.ToInt32(Session["RoleID"]) == 13) { btnCancelReq.Visible = true; } //Агенты Билайн
                //    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                //    {
                //        btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = true;
                //    }
                //    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                //    {
                //        btnCancelReqExp.Visible = true; btnProffer.Visible = true; btnConfirm.Visible = false; btnApproved.Visible = true;
                //    }
                //    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                //    {
                //        btnCancelReq.Visible = true; //btnNoConfirm.Visible = true;
                //        btnConfirm.Visible = true; //btnInProcess.Visible = true;
                //    }
                //}
                if (lst.RequestStatus == "Исправить")
                {
                    btnSendCreditRequest.Visible = true;
                    if (Convert.ToInt32(Session["RoleID"]) == 1) //Агенты Нур
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnFixed.Visible = true; //Исправлено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        //btnFixed.Visible = true; //Исправлено
                        btnConfirm.Visible = true; //Подтверждено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnConfirm.Visible = false; //Подтверждено
                        btnFixed.Visible = true; //Исправлено

                        btnProffer.Visible = true;  //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 4) //Админы Нур
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnConfirm.Visible = true; //Подтверждено
                        btnFixed.Visible = false; //Исправлено

                        btnProffer.Visible = true;  //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        //btnConfirm.Visible = true; //Подтверждено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        //btnApproved.Visible = false; //Утверждено
                        //btnConfirm.Visible = true; //Подтверждено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true;
                    }
                }

                if (lst.RequestStatus == "Исправлено")
                {
                    //gvProducts.Enabled = true;
                    btnSendCreditRequest.Visible = true;
                    if (Convert.ToInt32(Session["RoleID"]) == 1)  //Агенты Нур
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnFix.Visible = true; //Исправить

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 8)  //Агенты Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnFix.Visible = true; //Исправить

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 4) //Админы Нур
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnFix.Visible = true; //Исправить
                        btnConfirm.Visible = true; //Подтверждено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 9) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено
                        btnFix.Visible = true; //Исправить
                        btnConfirm.Visible = true; //Подтверждено

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        //btnConfirm.Visible = true; //Подтверждено
                        btnFix.Visible = true; //Исправить

                        btnAgreement.Visible = true; //Договор
                        btnProffer.Visible = true; //Анкета
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        //btnConfirm.Visible = true; //Подтверждено
                        //btnApproved.Visible = false; //Утверждено
                        btnFix.Visible = true; //Исправить

                        btnAgreement.Visible = true; //Договор
                        btnProffer.Visible = true; //Анкета
                    }

                }
                //if (lst.RequestStatus == "Не подтверждено")
                //{
                //    //gvProducts.Enabled = true;
                //    btnSendCreditRequest.Visible = true;
                //    if (Convert.ToInt32(Session["RoleID"]) == 13) { btnNewReq.Visible = true; btnCancelReq.Visible = true; } //Агенты Билайн
                //    if (Convert.ToInt32(Session["RoleID"]) == 19) { btnCancelReqExp.Visible = true; } //Эксперты 
                //    if (Convert.ToInt32(Session["RoleID"]) == 20) { btnCancelReqExp.Visible = true; btnApproved.Visible = true; } //Эксперты ГБ
                //    if (Convert.ToInt32(Session["RoleID"]) == 14) { btnConfirm.Visible = true; btnCancelReq.Visible = true; //btnInProcess.Visible = true;
                //        btnApproved.Visible = true; } //Админы Билайн
                //}
                //if (lst.RequestStatus == "Отменено")
                //{
                //    //gvProducts.Enabled = false;
                //}
                //if (lst.RequestStatus == "Отказано")
                //{
                //    //gvProducts.Enabled = false;
                //}
                if (lst.RequestStatus == "Подтверждено")
                {
                    if (Convert.ToInt32(Session["RoleID"]) == 1) //Агенты Нур
                    {
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }

                    if ((Convert.ToInt32(Session["RoleID"]) == 9) || (Convert.ToInt32(Session["RoleID"]) == 9)) //Админы Билайн
                    {
                        btnCancelReq.Visible = true; //Отменено

                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFix.Visible = true; //Исправить

                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                        btnApproved.Visible = true; //Утверждено
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFix.Visible = true; //Исправить

                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnAgreement.Visible = true; //Договор
                        btnProffer.Visible = true; //Анкета
                    }
                }
                if (lst.RequestStatus == "Утверждено")
                {
                    //gvProducts.Enabled = false;
                    if (Convert.ToInt32(Session["RoleID"]) == 1) //Агенты Нур
                    {
                        //btnIssue.Visible = true; //Выдано
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }

                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        //btnIssue.Visible = true; //Выдано
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if ((Convert.ToInt32(Session["RoleID"]) == 4) || (Convert.ToInt32(Session["RoleID"]) == 9)) //Админы Билайн
                    {
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFix.Visible = true; //Исправить

                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFix.Visible = true; //Исправить
                        btnActivated.Visible = true; // Активировано

                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                }

                if (lst.RequestStatus == "Активировано")
                {
                    //gvProducts.Enabled = false;
                    if (Convert.ToInt32(Session["RoleID"]) == 1) //Агенты Нур
                    {
                        btnIssue.Visible = true; //Выдано
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnIssue.Visible = true; //Выдано
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if ((Convert.ToInt32(Session["RoleID"]) == 4) || (Convert.ToInt32(Session["RoleID"]) == 9)) //Админы Билайн
                    {
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                    {
                        //btnCancelReqExp.Visible = true; //Отказано
                        btnFix.Visible = true; //Исправить

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                    {
                        btnCancelReqExp.Visible = true; //Отказано
                        btnFix.Visible = true; //Исправить
                        btnActivated.Visible = true; // Активировано

                        btnSendCreditRequest.Visible = true; //Сохранить
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                }

                //if (lst.RequestStatus == "К подписи")
                //{
                //    //gvProducts.Enabled = false;
                //    if (Convert.ToInt32(Session["RoleID"]) == 13) //Агенты Билайн
                //    {
                //    }
                //    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                //    {
                //        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReqExp.Visible = true;
                //    }
                //    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                //    {
                //        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReqExp.Visible = true;
                //    }
                //    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                //    {
                //    }
                //}
                //if (lst.RequestStatus == "На выдаче")
                //{
                //    //gvProducts.Enabled = false;
                //    btnAgreement.Visible = true;
                //    if (Convert.ToInt32(Session["RoleID"]) == 13) { btnIssue.Visible = true; } //Агенты Билайн
                //    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                //    {
                //        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReqExp.Visible = true;
                //    }
                //    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                //    {
                //        btnSendCreditRequest.Visible = true; btnProffer.Visible = true; btnCancelReqExp.Visible = true;
                //    }

                //    if (Convert.ToInt32(Session["RoleID"]) == 14) //Админы Билайн
                //    {
                //    }
                //}
                if (lst.RequestStatus == "Выдано")
                {
                    //gvProducts.Enabled = false;
                    btnAgreement.Visible = true;
                    if (Convert.ToInt32(Session["RoleID"]) == 1) //Агенты Нур
                    {
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnAgreement.Visible = true; //Договор
                    }
                    if ((Convert.ToInt32(Session["RoleID"]) == 4) || (Convert.ToInt32(Session["RoleID"]) == 9)) //Админы Билайн
                    {
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                    {
                        btnReceived.Visible = true; //Принято

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                    {
                        btnReceived.Visible = true; //Принято

                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                }
                if (lst.RequestStatus == "Принято")
                {
                    //gvProducts.Enabled = false;
                    btnAgreement.Visible = true;
                    if (Convert.ToInt32(Session["RoleID"]) == 1) //Агенты Нур
                    {
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 8) //Агенты Билайн
                    {
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 19) //Эксперты Доскредо
                    {
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if (Convert.ToInt32(Session["RoleID"]) == 20) //Эксперты ГБ Доскредо
                    {
                        btnProffer.Visible = true; //Анкета
                        btnAgreement.Visible = true; //Договор
                    }
                    if ((Convert.ToInt32(Session["RoleID"]) == 4) || (Convert.ToInt32(Session["RoleID"]) == 9)) //Админы Билайн
                    {
                        btnAgreement.Visible = true; //Договор
                    }
                }
            }
            catch (Exception ex)
            {
               //**** DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Error: " + ex.Message + "<hr>" + ex.Source + "<hr>" + ex.StackTrace, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
            }
        }

        protected void gvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Color redColor = Color.FromArgb(255, 0, 0);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string hexRed = "#E47E11", hexRed2 = "#E45E11", hexOrange = "#8C5E40", hexYellow = "#fdf404", hexGreen = "#7cfa84", hexBlue = "#227128", hexBlack = "#878787", hexReceive = "#11ACE4";
                Color _colorRed = System.Drawing.ColorTranslator.FromHtml(hexRed);
                Color _colorRed2 = System.Drawing.ColorTranslator.FromHtml(hexRed2);
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
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Исправить") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorRed2; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Не подтверждено") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorOrange; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Подтверждено") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorYellow; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Утверждено") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorGreen; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("К подписи") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorGreen; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("На выдаче") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorGreen; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Выдано") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorBlue; }
                if (e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].Text.CompareTo("Принято") == 0) { e.Row.Cells[GetColumnIndexByName(e.Row, "RequestStatus")].BackColor = _colorReceive; }
            }
            pnlMenuRequest.Visible = true;
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

        

        protected void gvRequestsFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Del")
            {
                ItemController ctl = new ItemController();
                ctl.DeleteRequestsFiles(id);
                //refreshProducts(1);
            }
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

        protected void btnProffer_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = hfCustomerID.Value;
            Session["CreditID"] = hfCreditID.Value;
            Session["RequestID"] = hfRequestID.Value;
            Response.Redirect("/Card/rptProffer");
            //Response.Redirect("/CardP");
        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            DateTime dateTimeNow = dateNow;
            /**/
            /*RequestHistory*//*----------------------------------------------------*/
            CreditController ctx = new CreditController();
            CardRequestsHistory newItem = new CardRequestsHistory()
            {
                AgentID = usrID,
                //CreditID = Convert.ToInt32(hfCreditID.Value),
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
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            Users2 usr = dbRWZ.Users2.Where(u => (u.UserName == usrLogin) && (u.Password == sha1)).FirstOrDefault();
            if (tbNewPassword.Text == tbConfirmPassword.Text)
            {
                if (usr != null)
                {
                    usr.Password = Crypto.SHA1(tbNewPassword.Text).ToLower();
                    dbRWZ.Users2.Context.SubmitChanges();
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

        public void enableUpoadFiles()
        {
            //****AsyncUpload1.Enabled = true;
            tbFileDescription.Enabled = true;
            btnUploadFiles.Enabled = true;
        }

        private void Approved()
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            DateTime dateTimeNow = dateNow;
            var lst5 = (from v in dbRWZ.CardRequests where (v.RequestID == Convert.ToInt32(hfRequestID.Value)) select v).ToList().FirstOrDefault();
            if (lst5 != null)
            {
                lst5.RequestStatus = "Утверждено";
                lst5.StatusDate = dateTimeNow;
                dbRWZ.CardRequests.Context.SubmitChanges();
                refreshGrid();
                string hexGreen = "#7cfa84";
                Color _colorGreen = System.Drawing.ColorTranslator.FromHtml(hexGreen);
                lblStatusRequest.BackColor = _colorGreen; hfRequestStatus.Value = "Утверждено";
                /*RequestHistory*/

                CardRequestsHistory newItem = new CardRequestsHistory()
                {
                    AgentID = Convert.ToInt32(Session["UserID"].ToString()),
                    //CreditID = Convert.ToInt32(hfCreditID.Value), //CreditsHistoriesID,
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

    }
}