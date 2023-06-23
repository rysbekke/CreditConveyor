using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat;
using СreditСonveyor.Data.Partners2;

namespace СreditСonveyor.Partners2
{
    public partial class rptAgreesCola : System.Web.UI.Page
    {
        string WorkType;
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        protected void Page_Load(object sender, EventArgs e)
        {
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            hfGuarID.Value = Session["GuarantorID"] as string;
            hfUserID.Value = Session["UserID"] as string;
            hfRequestID.Value = Session["RequestID"] as string;
            WorkType = Session["WorkType"] as string;
            

            int GuarantorID = Convert.ToInt32(Session["GuarantorID"] as string);


            CreditController crdCtr = new CreditController();

            SysController sysCtrl = new SysController();
            NumByWords num2words = new NumByWords();
            mod43 mod = new mod43();

            var req = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
            int? groupID = req.GroupID;

            //try { 

            //var lstGraphics = crdCtr.GraphicsGetItem(Convert.ToInt32(hfCreditID.Value));
            //if (lstGraphics.Count > 0)
            {
                /************************************************/
                CreditController creditCtrl = new CreditController();
                var historyCustomerItem = creditCtrl.HistoriesCustomerGetItem(Convert.ToInt32(hfCreditID.Value));
                var historyItem = creditCtrl.HistoriesGetItem(Convert.ToInt32(hfCreditID.Value));

                var historyStat = creditCtrl.ItemHistoriesStatuseGetItem(Convert.ToInt32(hfCreditID.Value));
                DateTime issueDate = historyStat.Where(r => r.StatusID == 3).FirstOrDefault().StatusDate;

                int RequestID = Convert.ToInt32(Session["RequestID"].ToString());
                decimal reqPeriod = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault().RequestPeriod.Value;

                decimal rate = Convert.ToDecimal(historyItem.ApprovedRate);
                decimal rate1 = Math.Truncate(rate); //целая часть
                decimal rate2 = rate - rate1; //дробная часть
                int rate1int = Convert.ToInt32(rate1);
                int rate2int = Convert.ToInt32(rate2 * 10);

                string productName = "", productCode = "", nameOfPercent = "Проценты", productNameSulpak = "$MT DCB 0%RASROC ", mes = "";
                if (rate == 30) { productCode = "KGDKSTZ3-36"; productName = "КапиталБанк"; }
                if (rate == 29) { productCode = "KGDKSTL3-36"; productName = "Потребительский"; }
                if ((rate == 0) && (reqPeriod == 3)) { productCode = "KGDKRS0-0-3"; productName = "0-0-3"; }
                if ((rate == 0) && (reqPeriod == 6)) { productCode = "KGDKRS0-0-6"; productName = "0-0-6"; }
                if ((rate == 0) && (reqPeriod == 9)) { productCode = "KGDKRS0-0-9"; productName = "0-0-9"; }
                if ((rate == 0) && (reqPeriod == 10)) { productCode = "KGDKRS10-10-10"; productName = "10-10-10"; nameOfPercent = "Комиссия за обслуживания кредита"; }
                if ((rate == 0) && (reqPeriod == 12)) { productCode = "KGDKRS0-0-12"; productName = "0-0-12"; }
                if ((rate == 0) && (reqPeriod == 15)) { productCode = "KGDKRS0-0-15"; productName = "0-0-15"; nameOfPercent = "Комиссия за обслуживания кредитного счета"; }
                if ((rate == 0) && (reqPeriod == 27)) { productCode = "KGDKRS0-0-27"; productName = "0-0-27"; }




                lblRequestPeriod.Text = reqPeriod.ToString();


                productNameSulpak = productNameSulpak + mes;
                string nameOfCredit = mod.getCode39Mod43(productNameSulpak);
                //barcodeValue.Text = nameOfCredit;
                

                string agrNo = mod.getCode39Mod43(historyItem.AgreementNo);
                


                lblCreditNomer.Text = historyItem.AgreementNo;
                lblCreditNomer6.Text = historyItem.AgreementNo;
                //lblCreditNomer11.Text = historyItem.AgreementNo;
                lblAgreementDate.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                //lblAgreementDate5.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate6.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate7.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate8.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate9.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                //lblAgreementDate10.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate11.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");



                lblEndDate.Text = (Convert.ToDateTime(issueDate).AddMonths(12)).Date.ToString("dd.MM.yyyy");

                var customerItem = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
                lblCustomerFIO.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO2.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO3.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO11.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO11.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO18.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO19.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;

                lblDocumentSeries.Text = customerItem.DocumentSeries;
                lblDocumentSeries2.Text = customerItem.DocumentSeries;

                lblDocumentNo.Text = customerItem.DocumentNo;
                lblDocumentNo2.Text = customerItem.DocumentNo;
                lblIssueAuthority.Text = customerItem.IssueAuthority;
                lblIssueAuthority2.Text = customerItem.IssueAuthority;
                lblIssueDate.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");           //customerItem.IssueDate.ToString(); 
                lblIssueDate2.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIdentificationNumber.Text = customerItem.IdentificationNumber;

                Office officeItem = sysCtrl.OficcesGetItem(historyItem.OfficeID);
                Branch branchItem = sysCtrl.BranchGetItem(officeItem.BranchID);
                //var branchItem = dbRWZ.Branches2s.Where(b => b.ID == req.BranchID).FirstOrDefault();
                City cityItem = sysCtrl.CityGetItem(branchItem.CityID);
                lblCity.Text = cityItem.CityName;
                var branchCustCustomersID = dbR.BranchesCustomers.Where(v => v.BranchID == branchItem.ID).FirstOrDefault().CustomerID;
                Customer companyItem = sysCtrl.CustomerGetItem(branchCustCustomersID);
                lblCompanyName.Text = companyItem.CompanyName;
                string regCompanyCity = dbR.Cities.Where(v => v.CityID == companyItem.RegistrationCityID).ToList().FirstOrDefault().CityName;
                string regCompanyStreet = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
                string regCompanyHouse = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
                string regCompanyFlat = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
                lblCompanyAddress.Text = regCompanyCity + " " + regCompanyStreet + " " + regCompanyHouse + " " + regCompanyFlat;
                lblCompanyINN.Text = companyItem.IdentificationNumber;
                lblCompanyOKPO.Text = companyItem.OKPO;
                //lblCompanyWorkPosition3.Text = custCustomerManagersItem.WorkPosition;
                //lblCompanyWorkPosition4.Text = custCustomerManagersItem.WorkPosition;
                string regCustomerCountry = (customerItem.RegistrationCountryID != null) ? dbR.Countries.Where(v => v.CountryID == customerItem.RegistrationCountryID).ToList().FirstOrDefault().ShortName : "";
                string regCustomerCity = (customerItem.RegistrationCityID != null) ? dbR.Cities.Where(v => v.CityID == customerItem.RegistrationCityID).ToList().FirstOrDefault().CityName : "";
                string regCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
                string regCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
                string regCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
                lblRegCustomerAddress.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
                lblRegCustomerAddress2.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
                string resCustomerCountry = (customerItem.ResidenceCountryID != null) ? dbR.Countries.Where(v => v.CountryID == customerItem.ResidenceCountryID).ToList().FirstOrDefault().ShortName : "";
                string resCustomerCity = (customerItem.ResidenceCityID != null) ? dbR.Cities.Where(v => v.CityID == customerItem.ResidenceCityID).ToList().FirstOrDefault().CityName : "";
                string resCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceStreet;
                string resCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceHouse;
                string resCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceFlat;
                lblResCustomerAddress.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;
                lblRequestSumm.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblRequestSumm6.Text = historyCustomerItem.ApprovedSumm.ToString();
                //lblRequestSumm2.Text = historyCustomerItem.ApprovedSumm.ToString();
                Decimal ApprovedSumm = Convert.ToDecimal(historyCustomerItem.ApprovedSumm);
                string SummWord = num2words.KgsPhrase(ApprovedSumm);
                lblRequestSummWord.Text = SummWord + " т."; //SummWord.Substring(0, (SummWord.Length - 3));


                

                double approvedRate = Math.Round(Convert.ToDouble(historyItem.ApprovedRate), 1);
                lblRequestRate.Text = approvedRate.ToString();
                lblRequestRate5.Text = approvedRate.ToString();
                lblRequestRate6.Text = approvedRate.ToString();
                //lblRequestRate7.Text = approvedRate.ToString();
                lblRequestRate10.Text = approvedRate.ToString();

                string RateWord = num2words.KgsPhrase(Convert.ToDecimal(approvedRate));
                lblRequestRateWord.Text = RateWord;

                string rate1str = num2words.KgsPhrase(Convert.ToDecimal(rate1int));
                string rate2str = num2words.KgsPhrase(Convert.ToDecimal(rate2int)).ToLower();

                if (rate2int > 0)
                {
                    //lblRequestRateWord.Text = rate1str.Substring(0, (rate1str.Length - 3)) + " и " + rate2str.Substring(0, (rate2str.Length - 3));
                    //lblRequestRateWord2.Text = rate1str.Substring(0, (rate1str.Length - 3)) + " и " + rate2str.Substring(0, (rate2str.Length - 3));
                }
                else
                {
                    //lblRequestRateWord.Text = rate1str.Substring(0, (rate1str.Length - 3));
                    //lblRequestRateWord2.Text = rate1str.Substring(0, (rate1str.Length - 3));
                }


                var effrate = dbR.CalculateEIR(Convert.ToInt32(hfCreditID.Value), null);
            
                lblApprovedSumm2.Text = historyCustomerItem.ApprovedSumm.ToString();
            
                //lblApprovedSumm3.Text = historyCustomerItem.ApprovedSumm.ToString();
                //lblApprovedSummWord3.Text = num2words.KgsPhrase(ApprovedSumm) + " т.";

                //lblApprovedSummWord.Text = lblApprovedSummWord.Text.Substring(0, (lblApprovedSummWord.Text.Length - 3));
                lblCurrencyName.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
            
                lblCurrencyName6.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
            
                var request = dbRWZ.Requests.Where(r => r.CreditID == Convert.ToInt32(Session["CreditID"].ToString())).FirstOrDefault();
                //int? usrID = dbRWZ.Requests.Where(r => r.CreditID == Convert.ToInt32(Session["CreditID"].ToString())).FirstOrDefault().AgentID;
            
                // int? ManagerID = dbserver.RequestsRoles.Where(r => r.RoleID == usrRoleID).FirstOrDefault().ManagerID;
                int? ManagerID = Convert.ToInt32(hfUserID.Value);
                var mngrItem = dbR.Customers.Where(r => r.CustomerID == ManagerID).FirstOrDefault();
                string docN = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == ManagerID).FirstOrDefault().AttorneyDocName;
                //string docN = branchItem.NumberOfAttorney;
                DateTime docDate2 = Convert.ToDateTime(dbRWZ.RequestsUsersRoles.Where(r => r.UserID == ManagerID).FirstOrDefault().AttorneyDocDate);
                //DateTime docDate2 = Convert.ToDateTime(branchItem.DateOfAttorney); // Convert.ToDateTime(dbRWZ.RequestsUsersRoles.Where(r => r.UserID == ManagerID).FirstOrDefault().AttorneyDocDate);
                string docDate = docDate2.ToString("dd.MM.yyyy");

                if (request.MaritalStatus == 1)
                {
                    maritalStatus0.Visible = false;
                    maritalStatus1.Visible = true;
                }
                else
                {
                    maritalStatus0.Visible = true;
                    maritalStatus1.Visible = false;
                }

            

                var lstRequestsProducts = dbRWZ.RequestsProducts.Where(r => r.RequestID == RequestID);
                if (lstRequestsProducts != null)
                {
                  
                }


                //lblRequestSumm2.Text = reqs.FirstOrDefault().ProductPrice.ToString();
                Decimal RequestSumm2 = 0;
                foreach (var reqpr in lstRequestsProducts)
                {
                    RequestSumm2 = RequestSumm2 + Convert.ToDecimal(reqpr.Price);
                }
                
                string SummWord2 = num2words.KgsPhrase(RequestSumm2);
             
              



                hfCustomerID.Value = Session["CustomerID"] as string;
                hfCreditID.Value = Session["CreditID"] as string;
                int usrID2 = Convert.ToInt32(Session["UserID"].ToString());
                //int RequestID = Convert.ToInt32(Session["RequestID"].ToString());

                //CreditController creditCtrl = new CreditController();
                //SysController sysCtrl = new SysController();
                var requestsUsersRoles = sysCtrl.RequestsUsersRoleGetItem(usrID2);
                var users = sysCtrl.UsersGetItem(usrID2);
                var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));

                //lblNameAgencyPoint.Text = requestsUsersRoles.NameAgencyPoint2;
                //lblNameAgencyPoint2.Text = requestsUsersRoles.NameAgencyPoint2;
                ////lblNameAgencyPoint2_2.Text = requestsUsersRoles.NameAgencyPoint2;
                //lblNameAgencyPointAddress.Text = requestsUsersRoles.AddressAgencyPoint2;
                //lblNameAgencyPointAddress2_2.Text = requestsUsersRoles.AddressAgencyPoint2;

                //lblUserFIO.Text = users.Fullname;
                //lblUserFIO2.Text = users.Fullname;


                lblManagerFIO.Text = users.Fullname; //branchItem.Director;  
                lblManagerFIO2.Text = users.Fullname; // branchItem.Director; 

                lblManagerFIO6.Text = users.Fullname;

                lblManagerDocNum.Text = docN;
                lblManagerDocNum3.Text = docN;
                lblManagerDocDate.Text = docDate.ToString();
                lblManagerDocDate3.Text = docDate.ToString();

                lblCustomerPassport3.Text = customers.DocumentSeries + customers.DocumentNo + " " + customers.IssueAuthority + " " + Convert.ToDateTime(customers.IssueDate).Date.ToString("dd.MM.yyyy");
                lblCustomerPassport4.Text = customers.DocumentSeries + customers.DocumentNo + " " + customers.IssueAuthority + " " + Convert.ToDateTime(customers.IssueDate).Date.ToString("dd.MM.yyyy");
                decimal numtoword = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault().ProductPrice.Value;
                decimal amountDownPayment = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault().AmountDownPayment.Value;
                lblDateOfBirth.Text = Convert.ToDateTime(customers.DateOfBirth).Date.ToString("dd.MM.yyyy");

            
                //if ((groupID == 30) || (groupID == 51)) //ОсОО Планета Трейд-Сервис 30  и Сулпак 51
                //{
                //    lblComOpenAcc12.Text = "Отсутствует";
                //}


                lblCompanyINN.Text = companyItem.IdentificationNumber;
                lblCompanyOKPO.Text = companyItem.OKPO;
                //lblCompanyBIK.Text = companyItem.bi

                /*Поручитель*/


            }
            //else
            //{
            //    //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "График погашений не построен", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
            //    //System.Windows.Forms.MessageBox.Show("График погашений не построен");
            //    MsgBox("График погашений не построен", this.Page, this);
            //}

           


        }

        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }
}