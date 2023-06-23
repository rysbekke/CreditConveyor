using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat;
using СreditСonveyor.Data.Partners;

namespace СreditСonveyor.Partners
{
    public partial class rptAgreesNano : System.Web.UI.Page
    {
        string WorkType;
        DateTime dateNow;
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["connectionStringZ"].ToString();
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        protected void Page_Load(object sender, EventArgs e)
        {
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            hfGuarID.Value = Session["GuarantorID"] as string;
            hfUserID.Value = Session["UserID"] as string;
            hfRequestID.Value = Session["RequestID"] as string;
            WorkType = Session["WorkType"] as string;

            dateNow = Convert.ToDateTime(DateTime.Now);

            CreditController crdCtr = new CreditController();
            var lstGraphics = crdCtr.GraphicsGetItem(Convert.ToInt32(hfCreditID.Value));
            //if (lstGraphics.Count > 0)
            {
                /************************************************/
                CreditController creditCtrl = new CreditController();
                var historyCustomerItem = creditCtrl.HistoriesCustomerGetItem(Convert.ToInt32(hfCreditID.Value));
                var historyItem = creditCtrl.HistoriesGetItem(Convert.ToInt32(hfCreditID.Value));

                var historyStat = creditCtrl.ItemHistoriesStatuseGetItem(Convert.ToInt32(hfCreditID.Value));
                DateTime issueDate = historyStat.Where(r => r.StatusID == 3).FirstOrDefault().StatusDate;
                //DateTime issueDate = historyStat.Last().StatusDate;
                lblCreditNomer.Text = historyItem.AgreementNo;
                lblCreditNomer2.Text = historyItem.AgreementNo;
                lblCreditNomer3.Text = historyItem.AgreementNo;
                lblCreditNomer4.Text = historyItem.AgreementNo;
                lblCreditNomer5.Text = historyItem.AgreementNo;

                //lblAgreementDate.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                //lblAgreementDate2.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy"); //historyItem.IssueDate.ToString();
                //lblAgreementDate3.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy"); //historyItem.IssueDate.ToString();
                //lblAgreementDate4.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                //lblAgreementDate5.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                //lblAgreementDate6.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                //lblAgreementDate8.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                //lblAgreementDate9.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate.Text = (Convert.ToDateTime(dateNow)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate2.Text = (Convert.ToDateTime(dateNow)).Date.ToString("dd.MM.yyyy"); //historyItem.IssueDate.ToString();
                lblAgreementDate3.Text = (Convert.ToDateTime(dateNow)).Date.ToString("dd.MM.yyyy"); //historyItem.IssueDate.ToString();
                lblAgreementDate4.Text = (Convert.ToDateTime(dateNow)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate5.Text = (Convert.ToDateTime(dateNow)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate6.Text = (Convert.ToDateTime(dateNow)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate8.Text = (Convert.ToDateTime(dateNow)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate9.Text = (Convert.ToDateTime(dateNow)).Date.ToString("dd.MM.yyyy");
                lblEndDate.Text = ((Convert.ToDateTime(dateNow.AddYears(1))).Date).ToString("dd.MM.yyyy");
                lblEndDate2.Text = ((Convert.ToDateTime(dateNow.AddYears(1))).Date).ToString("dd.MM.yyyy");

                SysController sysCtrl = new SysController();
                var customerItem = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
                lblCustomerFIO.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO2.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO3.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;

                lblCustomerFIO6.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO7.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO8.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO9.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;

                lblCustomerFIO11.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;

                lblDocumentSeries.Text = customerItem.DocumentSeries;
                lblDocumentSeries2.Text = customerItem.DocumentSeries;
                lblDocumentSeries3.Text = customerItem.DocumentSeries;
                lblDocumentSeries4.Text = customerItem.DocumentSeries;
                lblDocumentSeries5.Text = customerItem.DocumentSeries;
                lblDocumentNo.Text = customerItem.DocumentNo;
                lblDocumentNo2.Text = customerItem.DocumentNo;
                lblDocumentNo3.Text = customerItem.DocumentNo;
                lblDocumentNo4.Text = customerItem.DocumentNo;
                lblDocumentNo5.Text = customerItem.DocumentNo;
                lblIssueAuthority.Text = customerItem.IssueAuthority;
                lblIssueAuthority2.Text = customerItem.IssueAuthority;
                lblIssueAuthority3.Text = customerItem.IssueAuthority;
                lblIssueAuthority4.Text = customerItem.IssueAuthority;
                lblIssueAuthority5.Text = customerItem.IssueAuthority;
                lblIssueDate.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");           //customerItem.IssueDate.ToString(); 
                lblIssueDate2.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIssueDate3.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIssueDate4.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIssueDate5.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIdentificationNumber.Text = customerItem.IdentificationNumber;
                Office officeItem = sysCtrl.OficcesGetItem(historyItem.OfficeID);
                Branch branchItem = sysCtrl.BranchGetItem(officeItem.BranchID);
                City cityItem = sysCtrl.CityGetItem(branchItem.CityID);
                lblCity.Text = cityItem.CityName;
                lblCity2.Text = cityItem.CityName;
                var branchCustCustomersID = dbR.BranchesCustomers.Where(v => v.BranchID == branchItem.ID).FirstOrDefault().CustomerID;
                Customer companyItem = sysCtrl.CustomerGetItem(branchCustCustomersID);
                lblCompanyName.Text = companyItem.CompanyName;
                lblCompanyName2.Text = companyItem.CompanyName;
                string regCompanyCity = dbR.Cities.Where(v => v.CityID == companyItem.RegistrationCityID).ToList().FirstOrDefault().CityName;
                string regCompanyStreet = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
                string regCompanyHouse = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
                string regCompanyFlat = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
                lblCompanyAddress.Text = regCompanyCity + " " + regCompanyStreet + " " + regCompanyHouse + " " + regCompanyFlat;
                lblCompanyAddress2.Text = regCompanyCity + " " + regCompanyStreet + " " + regCompanyHouse + " " + regCompanyFlat;
                lblCompanyINN.Text = companyItem.IdentificationNumber;
                lblCompanyINN2.Text = companyItem.IdentificationNumber;
                lblCompanyOKPO.Text = companyItem.OKPO;
                lblCompanyOKPO2.Text = companyItem.OKPO;
                //lblDateNow.Text = DateTime.Today.Date.ToString("dd.MM.yyyy");
                //var customerManagersPerson = dbserver.CustomerManagers.Where(v => v.CompanyID == companyItem.CustomerID).FirstOrDefault();
                // var customerManagersPerson = dbserver.CustomerManagers.Where(v => v.CompanyID == companyItem.CustomerID).FirstOrDefault();
                // var custCustomerManagersItem = sysCtrl.CustomerGetItem(customerManagersPerson.PersonID);
                //lblCompanyCustomerManagersFIO3.Text = custCustomerManagersItem.Surname + " " + custCustomerManagersItem.CustomerName + " " + custCustomerManagersItem.Otchestvo;
                //  lblCompanyWorkPosition.Text = custCustomerManagersItem.WorkPosition;
                //lblCompanyWorkPosition2.Text = custCustomerManagersItem.WorkPosition;
                //lblCompanyWorkPosition3.Text = custCustomerManagersItem.WorkPosition;
                //lblCompanyWorkPosition4.Text = custCustomerManagersItem.WorkPosition;
                string regCustomerCountry = (customerItem.RegistrationCountryID != null) ? dbR.Countries.Where(v => v.CountryID == customerItem.RegistrationCountryID).ToList().FirstOrDefault().ShortName : "";
                string regCustomerCity = (customerItem.RegistrationCityID != null) ? dbR.Cities.Where(v => v.CityID == customerItem.RegistrationCityID).ToList().FirstOrDefault().CityName : "";
                string regCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
                string regCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
                string regCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
                lblRegCustomerAddress.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " д." + regCustomerHouse + ", кв." + regCustomerFlat;
                lblRegCustomerAddress2.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " д." + regCustomerHouse + ", кв." + regCustomerFlat;
                lblRegCustomerAddress3.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " д." + regCustomerHouse + ", кв." + regCustomerFlat;
                lblRegCustomerAddress4.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " д." + regCustomerHouse + ", кв." + regCustomerFlat;
                lblRegCustomerAddress5.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " д." + regCustomerHouse + ", кв." + regCustomerFlat;
                string resCustomerCountry = (customerItem.ResidenceCountryID != null) ? dbR.Countries.Where(v => v.CountryID == customerItem.ResidenceCountryID).ToList().FirstOrDefault().ShortName : "";
                string resCustomerCity = (customerItem.ResidenceCityID != null) ? dbR.Cities.Where(v => v.CityID == customerItem.ResidenceCityID).ToList().FirstOrDefault().CityName : "";
                string resCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceStreet;
                string resCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceHouse;
                string resCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceFlat;
                lblResCustomerAddress.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " д." + resCustomerHouse + ", кв." + resCustomerFlat;
                lblResCustomerAddress2.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " д." + resCustomerHouse + ", кв." + resCustomerFlat;
                lblResCustomerAddress3.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " д." + resCustomerHouse + ", кв." + resCustomerFlat;
                lblResCustomerAddress4.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " д." + resCustomerHouse + ", кв." + resCustomerFlat;

                lblRequestSumm.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblRequestSumm3.Text = historyCustomerItem.ApprovedSumm.ToString();
                //lblRequestSumm2.Text = historyCustomerItem.ApprovedSumm.ToString();
                //lblRequestSumm3.Text = historyCustomerItem.ApprovedSumm.ToString();
                //lblRequestSumm4.Text = historyCustomerItem.ApprovedSumm.ToString();
                NumByWords num2words = new NumByWords();
                Decimal ApprovedSumm = Convert.ToDecimal(historyCustomerItem.ApprovedSumm);
                string SummWord = num2words.KgsPhrase(ApprovedSumm);
                lblRequestSummWord.Text = SummWord + " т."; //SummWord.Substring(0, (SummWord.Length - 3));
                lblRequestSummWord3.Text = SummWord + " т.";
                //lblRequestSummWord2.Text = SummWord.Substring(0, (SummWord.Length - 3));
                //lblRequestSummWord3.Text = SummWord.Substring(0, (SummWord.Length - 3));
                //lblRequestSummWord4.Text = SummWord.Substring(0, (SummWord.Length - 3));

                lblApprovedPeriod.Text = historyItem.ApprovedPeriod.ToString();

                //lblCreditPurpose2.Text = historyCustomerItem.CreditPurpose;

                double approvedRate = Math.Round(Convert.ToDouble(historyItem.ApprovedRate), 1);


                decimal rate = Convert.ToDecimal(historyItem.ApprovedRate);
                decimal rate1 = Math.Truncate(rate); //целая часть
                decimal rate2 = rate - rate1; //дробная часть
                int rate1int = Convert.ToInt32(rate1);
                int rate2int = Convert.ToInt32(rate2 * 10);

                string rate1str = num2words.KgsPhrase(Convert.ToDecimal(rate1int));
                string penalty = num2words.KgsPhrase(Convert.ToDecimal(30));
                string rate2str = num2words.KgsPhrase(Convert.ToDecimal(rate2int)).ToLower();

                if (rate2int > 0)
                {
                    //lblRequestRateWord.Text = rate1str.Substring(0, (rate1str.Length - 3)) + " и " + rate2str.Substring(0, (rate2str.Length - 3));
                    //lblRequestRateWord2.Text = rate1str.Substring(0, (rate1str.Length - 3)) + " и " + rate2str.Substring(0, (rate2str.Length - 3));
                    //lblRequestRateWord2.Text = penalty.Substring(0, (penalty.Length - 3)) + " и " + penalty.Substring(0, (penalty.Length - 3));
                }
                else
                {
                    //lblRequestRateWord.Text = rate1str.Substring(0, (rate1str.Length - 3));
                    //lblRequestRateWord2.Text = rate1str.Substring(0, (rate1str.Length - 3));
                    //lblRequestRateWord2.Text = penalty.Substring(0, (penalty.Length - 3));
                }


                var effrate = dbR.CalculateEIR(Convert.ToInt32(hfCreditID.Value), null);
                //lblRequestRateEffect.Text = "0"; // String.Format("{0:0.00}", effrate.Value); //.ToString(); //(historyItem.ApprovedRate + 2).ToString(); //Уточнить
                //lblRequestRateYear.Text = "0";// String.Format("{0:0.00}", effrate.Value); //.ToString();
                /*Поручитель*/
                //var guarantorID = creditCtrl.GuarantorGetItem(Convert.ToInt32(hfCreditID.Value));
                //if (guarantorID != -1)
                //{
                //    var guarantItem = sysCtrl.CustomerGetItem(guarantorID);
                //    lblCreditNomer2.Text = historyItem.AgreementNo;
                //    lblCreditNomer3.Text = historyItem.AgreementNo;
                //    lblCreditNomer4.Text = historyItem.AgreementNo;
                //}
                /************************************************/
                //var lastItemGraphics = lstGraphics.Last();



                lblApprovedSumm2.Text = historyCustomerItem.ApprovedSumm.ToString();

                lblCurrencyName.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;

                lblCurrencyName6.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;

                int? usrID = dbRWZ.Requests.Where(r => r.CreditID == Convert.ToInt32(Session["CreditID"].ToString())).FirstOrDefault().AgentID;
                //if (usrID == -10) usrID = Convert.ToInt32(hfUserID.Value);
                usrID = Convert.ToInt32(hfUserID.Value);
                int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
                var roles = dbRWZ.RequestsRoles.Where(r => r.RoleID == usrRoleID).FirstOrDefault();

                DateTime docDate2 = Convert.ToDateTime(dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().AttorneyDocDate);
                string docDate = docDate2.ToString("dd.MM.yyyy");
                string docN = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().AttorneyDocName;


                // int? ManagerID = dbserver.RequestsRoles.Where(r => r.RoleID == usrRoleID).FirstOrDefault().ManagerID;
                int? ManagerID = Convert.ToInt32(Session["UserID"].ToString());
                var mngrItem = dbR.Customers.Where(r => r.CustomerID == ManagerID).FirstOrDefault();
                lblManagerDocNum.Text = roles.ManagerDocNum;
                lblManagerDocNum2.Text = roles.ManagerDocNum;
                lblManagerDocNum3.Text = roles.ManagerDocNum;
                //lblManagerDocNum4.Text = roles.ManagerDocNum;
                lblManagerDocNum5.Text = roles.ManagerDocNum;
                lblManagerDocNum6.Text = roles.ManagerDocNum;
                lblManagerDocDate.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");// roles.ManagerDocDate.ToString();
                lblManagerDocDate2.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                lblManagerDocDate3.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                //lblManagerDocDate4.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                lblManagerDocDate5.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                lblManagerDocDate6.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                if (mngrItem != null)
                {
                    lblManagerFIO.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                    lblManagerFIO2.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                    //lblManagerFIO3.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                    lblManagerFIO4.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                    lblManagerFIO5.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                    lblManagerFIO6.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                }




                lblManagerDocNum.Text = docN;
                lblManagerDocNum2.Text = docN;
                lblManagerDocNum3.Text = docN;

                lblManagerDocNum5.Text = docN;
                lblManagerDocNum6.Text = docN;
                lblManagerDocDate.Text = docDate.ToString();
                lblManagerDocDate2.Text = docDate.ToString();
                lblManagerDocDate3.Text = docDate.ToString();

                lblManagerDocDate5.Text = docDate.ToString();
                lblManagerDocDate6.Text = docDate.ToString();
                var request = dbRWZ.Requests.Where(r => r.CreditID == Convert.ToInt32(Session["CreditID"].ToString())).FirstOrDefault();


                //var guarantor = sysCtrl.CustomerGetItem(Convert.ToInt32(hfGuarID.Value));
                //lblGuarFIO.Text = guarantor.Surname + " " + guarantor.CustomerName + " " + guarantor.Otchestvo;
                //lblDocumentSNGuar.Text = guarantor.DocumentSeries + guarantor.DocumentNo;
                //lblIssueDateGuar.Text = (Convert.ToDateTime(guarantor.IssueDate)).Date.ToString("dd.MM.yyyy");
                //lblIssueAuthorityGuar.Text = guarantor.IssueAuthority;
                int RequestID = Convert.ToInt32(Session["RequestID"].ToString());
                //dbdataDataContext db = new dbdataDataContext(connectionString);

                //if (WorkType == "0") { pril6.Visible = false; }
                //if (WorkType == "1")
                //{
                //    pril6.Visible = true;
                //    var request = db.Requests.Where(r => r.RequestID == RequestID).FirstOrDefault();
                //    if (request != null)
                //    {
                //        lblRevenueDay.Text = request.Revenue.ToString();
                //        lblCostPrice.Text = request.СostPrice.ToString();
                //        lblOverhead.Text = request.Overhead.ToString();
                //        lblCountWorkDay.Text = request.CountWorkDay.ToString();
                //        lblFamilyExpenses.Text = request.FamilyExpenses.ToString();

                //        double costprice = Convert.ToDouble(lblCostPrice.Text);
                //        double v = Convert.ToDouble(Convert.ToDouble(lblRevenueDay.Text) * request.CountWorkDay);
                //        double valp = (costprice == 0) ? v : (v * costprice) / (100 + costprice);
                //        double chp = valp - Convert.ToDouble(lblOverhead.Text) - Convert.ToDouble(lblOverhead.Text);


                //        //double v = Convert.ToDouble(lblRevenueDay.Text);
                //        //double valp = (v * Convert.ToDouble(costprice)) / (100 + Convert.ToDouble(costprice));
                //        //double chp = valp - Convert.ToDouble(lblOverhead.Text) - Convert.ToDouble(lblFamilyExpenses.Text);

                //        lblChp.Text = chp.ToString();
                //        lblValP.Text = valp.ToString();
                //        //if (chp > 0)
                //        //{
                //        //    double cho = chp - k;
                //        //    double y1 = 100 * cho / chp;
                //        //    double y2 = 100 * k / valp;
                //        //    if ((y1 > 24) && (y2 < 49)) f = true;
                //        //}
                //    }
                //}




                //var reqs = db.Requests.Where(r => r.RequestID == RequestID);
                //lblRequestSumm2.Text = reqs.FirstOrDefault().ProductPrice.ToString();
                Decimal RequestSumm2 = 0;


                string SummWord2 = num2words.KgsPhrase(RequestSumm2);




                /*Поручитель*/
                var guarantorID = creditCtrl.GuarantorGetItem(Convert.ToInt32(hfCreditID.Value));
                if (guarantorID != -1)
                {
                    var guarantItem = sysCtrl.CustomerGetItem(guarantorID);
                }

                hfCustomerID.Value = Session["CustomerID"] as string;
                hfCreditID.Value = Session["CreditID"] as string;
                int usrID2 = Convert.ToInt32(Session["UserID"].ToString());
                //int RequestID = Convert.ToInt32(Session["RequestID"].ToString());
                //dbdataDataContext db = new dbdataDataContext(connectionString);
                //CreditController creditCtrl = new CreditController();
                //SysController sysCtrl = new SysController();
                var requestsUsersRoles = sysCtrl.RequestsUsersRoleGetItem(usrID2);
                var users = sysCtrl.UsersGetItem(usrID2);
                var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));



                lblManagerFIO.Text = users.Fullname;
                lblManagerFIO2.Text = users.Fullname;

                lblManagerFIO4.Text = users.Fullname;
                lblManagerFIO5.Text = users.Fullname;
                lblManagerFIO6.Text = users.Fullname;


                //decimal numtoword = dbR.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault().ProductPrice.Value;



                var requests = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();


            }
            //else
            //{
            //    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "График погашений не построен", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
            //}

        }
    }
}
    
