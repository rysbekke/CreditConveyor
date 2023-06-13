using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using СreditСonveyor.Data.Partners2;

namespace СreditСonveyor.Partners2
{
    public partial class rptAgreesInst : System.Web.UI.Page
    {
        string WorkType;
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        protected void Page_Load(object sender, EventArgs e)
        {
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            hfGuarID.Value = Session["GuarantorID"] as string;
            hfUserID.Value = Session["UserID"] as string;
            hfRequestID.Value = Session["RequestID"] as string;
            WorkType = Session["WorkType"] as string;

            lblCreditID.Text = hfCreditID.Value;
            lblCreditID2.Text = hfCreditID.Value;

            CreditController crdCtr = new CreditController();
            NumByWords num2words = new NumByWords();
            mod43 mod = new mod43();

            //int GuarantorID = Convert.ToInt32(Session["GuarantorID"] as string);
            var req = dbRWZ.Requests.Where(r => r.RequestID == Convert.ToInt32(hfRequestID.Value)).FirstOrDefault();
            int? groupID = req.GroupID;

            var lstGraphics = crdCtr.GraphicsGetItem(Convert.ToInt32(hfCreditID.Value));
            if (lstGraphics.Count > 0)
            {
                /************************************************/
                CreditController creditCtrl = new CreditController();
                var historyCustomerItem = creditCtrl.HistoriesCustomerGetItem(Convert.ToInt32(hfCreditID.Value));
                var historyItem = creditCtrl.HistoriesGetItem(Convert.ToInt32(hfCreditID.Value));

                var historyStat = creditCtrl.ItemHistoriesStatuseGetItem(Convert.ToInt32(hfCreditID.Value));
                DateTime issueDate = historyStat.Where(r => r.StatusID == 3).FirstOrDefault().StatusDate;

                int RequestID = Convert.ToInt32(Session["RequestID"].ToString());
                var request = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();

                decimal reqPeriod = request.RequestPeriod.Value;
                decimal rate = Convert.ToDecimal(historyItem.ApprovedRate);
                decimal rate1 = Math.Truncate(rate); //целая часть
                decimal rate2 = rate - rate1; //дробная часть
                int rate1int = Convert.ToInt32(rate1);
                int rate2int = Convert.ToInt32(rate2 * 10);

                string productName = "", productCode = "", nameOfPercent = "Проценты", productNameSulpak = "$MT DCB 0%RASROC ", mes = "";
                if (rate == 30) { productCode = "KGDKSTZ3-36"; productName = "КапиталБанк"; }
                if (rate == 29) { productCode = "KGDKSTL3-36"; productName = "Потребительский"; }
                if ((rate == 0) && (reqPeriod == 3)) { productCode = "KGDKRS0-0-3"; productName = "0-0-3"; mes = "003"; }
                if ((rate == 0) && (reqPeriod == 6)) { productCode = "KGDKRS0-0-6"; productName = "0-0-6"; mes = "006"; }
                if ((rate == 0) && (reqPeriod == 9)) { productCode = "KGDKRS0-0-9"; productName = "0-0-9"; mes = "009"; }
                if ((rate == 0) && (reqPeriod == 12)) { productCode = "KGDKRS0-0-12"; productName = "0-0-12"; mes = "012"; }

                if (request.GroupID == 30) //Технодом
                {
                    if ((rate == 0) && (reqPeriod == 10)) { productCode = "KGDKRS10-10-10"; productName = "10-10-10"; nameOfPercent = "Комиссия за обслуживания кредита"; mes = "010"; }
                    if ((rate == 0) && (reqPeriod == 15)) { productCode = "KGDKRS0-0-15"; productName = "0-0-15"; nameOfPercent = "Комиссия за обслуживания кредитного счета"; mes = "015"; }
                    if ((rate == 0) && (reqPeriod == 24)) { productCode = "KGDKRS0-0-24"; productName = "0-0-24"; nameOfPercent = "Комиссия за обслуживания кредитного счета"; mes = "024"; }
                    lblCommission.Text = "Комиссия за обслуживания кредитного счета: 1% (от суммы кредита ежемесячно)";
                }
                if ((rate == 0) && (reqPeriod == 27)) { productCode = "KGDKRS0-0-27"; productName = "0-0-27"; mes = "027"; }
                productNameSulpak = productNameSulpak + mes;
                string nameOfCredit = mod.getCode39Mod43(productNameSulpak);
                //barcodeValue.Text = nameOfCredit;
                if (groupID == 51) lblIsSulpak.Text = "1"; else lblIsSulpak.Text = "0";

                try
                {
                    string agrNo = mod.getCode39Mod43(historyItem.AgreementNo);
                    barcodeValue2.Text = agrNo;
                    barcodeValue.Text = nameOfCredit;
                }
                catch (Exception ex)
                {
                    MsgBox("Нет номера договора в ОБ", this.Page, this);
                }
                




                string rate1str = num2words.KgsPhrase(Convert.ToDecimal(rate1int));
                string rate2str = num2words.KgsPhrase(Convert.ToDecimal(rate2int)).ToLower();


                if (rate2int > 0)
                {
                    lblRequestRateWord.Text = rate1str.Substring(0, (rate1str.Length - 3)) + " и " + rate2str.Substring(0, (rate2str.Length - 3));
                    lblRequestRateWord2.Text = "Тридцать"; //rate1str.Substring(0, (rate1str.Length - 3)) + " и " + rate2str.Substring(0, (rate2str.Length - 3));
                }
                else
                {
                    lblRequestRateWord.Text = rate1str.Substring(0, (rate1str.Length - 3));
                    lblRequestRateWord2.Text = "Тридцать";  //rate1str.Substring(0, (rate1str.Length - 3));
                }


                var effrate = dbR.CalculateEIR(Convert.ToInt32(hfCreditID.Value), null);
                lblRequestRateEffect.Text = "0"; // String.Format("{0:0.00}", effrate.Value); //.ToString(); //(historyItem.ApprovedRate + 2).ToString(); //Уточнить
                lblRequestRateYear.Text = "0"; // String.Format("{0:0.00}", effrate.Value); //.ToString();
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



                gvGraphic.Columns[2].HeaderText = nameOfPercent;

                lblCodeProduct.Text = productCode;
                lblNameProduct.Text = productName;


                lblCreditNomer.Text = historyItem.AgreementNo;
                lblCreditNomer2.Text = historyItem.AgreementNo;
                lblCreditNomer3.Text = historyItem.AgreementNo;
                lblCreditNomer4.Text = historyItem.AgreementNo;
                lblCreditNomer5.Text = historyItem.AgreementNo;
                lblCreditNomer6.Text = historyItem.AgreementNo;
                //lblCreditNomer7.Text = historyItem.AgreementNo;
                //lblCreditNomer8.Text = historyItem.AgreementNo;
                lblCreditNomer9.Text = historyItem.AgreementNo;
                lblCreditNomer10.Text = historyItem.AgreementNo;
                lblCreditNomer11.Text = historyItem.AgreementNo;
                lblAgreementDate.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate2.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy"); //historyItem.IssueDate.ToString();
                lblAgreementDate3.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate4.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate5.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate6.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate7.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate8.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate9.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate10.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate11.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate12.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate13.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");
                lblAgreementDate14.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy");


                SysController sysCtrl = new SysController();
                var customerItem = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
                lblCustomerFIO.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO2.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO3.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO4.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO5.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO6.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO7.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO8.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                //lblCustomerFIO9.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                //lblCustomerFIO10.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO11.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO11.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO12.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO13.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO14.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO15.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO16.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO17.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO18.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
                lblCustomerFIO19.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;


                lblDocumentSeries.Text = customerItem.DocumentSeries;
                lblDocumentSeries2.Text = customerItem.DocumentSeries;
                lblDocumentSeries3.Text = customerItem.DocumentSeries;
                lblDocumentSeries4.Text = customerItem.DocumentSeries;
                lblDocumentSeries5.Text = customerItem.DocumentSeries;
                lblDocumentSeries6.Text = customerItem.DocumentSeries;
                lblDocumentNo.Text = customerItem.DocumentNo;
                lblDocumentNo2.Text = customerItem.DocumentNo;
                lblDocumentNo3.Text = customerItem.DocumentNo;
                lblDocumentNo4.Text = customerItem.DocumentNo;
                lblDocumentNo5.Text = customerItem.DocumentNo;
                lblDocumentNo6.Text = customerItem.DocumentNo;
                lblIssueAuthority.Text = customerItem.IssueAuthority;
                lblIssueAuthority2.Text = customerItem.IssueAuthority;
                lblIssueAuthority3.Text = customerItem.IssueAuthority;
                lblIssueAuthority4.Text = customerItem.IssueAuthority;
                lblIssueAuthority5.Text = customerItem.IssueAuthority;
                lblIssueAuthority6.Text = customerItem.IssueAuthority;
                lblIssueDate.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");           //customerItem.IssueDate.ToString(); 
                lblIssueDate2.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIssueDate3.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIssueDate4.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIssueDate5.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIssueDate6.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
                lblIdentificationNumber.Text = customerItem.IdentificationNumber;
                lblIdentificationNumber2.Text = customerItem.IdentificationNumber;
                Office officeItem = sysCtrl.OficcesGetItem(historyItem.OfficeID);
                Branch branchItem = sysCtrl.BranchGetItem(officeItem.BranchID);
                City cityItem = sysCtrl.CityGetItem(branchItem.CityID);
                lblCity.Text = cityItem.CityName;
                lblCity2.Text = cityItem.CityName;
                lblCity3.Text = cityItem.CityName;
                var branchCustCustomersID = dbR.BranchesCustomers.Where(v => v.BranchID == branchItem.ID).FirstOrDefault().CustomerID;
                Customer companyItem = sysCtrl.CustomerGetItem(branchCustCustomersID);
                lblCompanyName.Text = companyItem.CompanyName;
                lblCompanyName2.Text = companyItem.CompanyName;
                lblCompanyName3.Text = companyItem.CompanyName;
                string regCompanyCity = dbR.Cities.Where(v => v.CityID == companyItem.RegistrationCityID).ToList().FirstOrDefault().CityName;
                string regCompanyStreet = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
                string regCompanyHouse = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
                string regCompanyFlat = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
                lblCompanyAddress.Text = regCompanyCity + " " + regCompanyStreet + " " + regCompanyHouse + " " + regCompanyFlat;
                lblCompanyAddress2.Text = regCompanyCity + " " + regCompanyStreet + " " + regCompanyHouse + " " + regCompanyFlat;
                lblCompanyAddress3.Text = regCompanyCity + " " + regCompanyStreet + " " + regCompanyHouse + " " + regCompanyFlat;
                lblCompanyINN.Text = companyItem.IdentificationNumber;
                lblCompanyINN2.Text = companyItem.IdentificationNumber;
                lblCompanyINN3.Text = companyItem.IdentificationNumber;
                lblCompanyOKPO.Text = companyItem.OKPO;
                lblCompanyOKPO2.Text = companyItem.OKPO;
                lblCompanyOKPO3.Text = companyItem.OKPO;
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
                lblRegCustomerAddress.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
                lblRegCustomerAddress2.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
                lblRegCustomerAddress3.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
                lblRegCustomerAddress4.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
                lblRegCustomerAddress5.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
                lblRegCustomerAddress6.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
                string resCustomerCountry = (customerItem.ResidenceCountryID != null) ? dbR.Countries.Where(v => v.CountryID == customerItem.ResidenceCountryID).ToList().FirstOrDefault().ShortName : "";
                string resCustomerCity = (customerItem.ResidenceCityID != null) ? dbR.Cities.Where(v => v.CityID == customerItem.ResidenceCityID).ToList().FirstOrDefault().CityName : "";
                string resCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceStreet;
                string resCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceHouse;
                string resCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceFlat;
                lblResCustomerAddress.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;
                lblResCustomerAddress2.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;
                lblResCustomerAddress3.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;
                //lblResCustomerAddress4.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;
                lblResCustomerAddress5.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;
                lblRequestSumm.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblRequestSumm3.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblRequestSumm4.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblRequestSumm5.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblRequestSumm6.Text = historyCustomerItem.ApprovedSumm.ToString();
                //lblRequestSumm2.Text = historyCustomerItem.ApprovedSumm.ToString();
                //lblRequestSumm3.Text = historyCustomerItem.ApprovedSumm.ToString();
                //lblRequestSumm4.Text = historyCustomerItem.ApprovedSumm.ToString();

                Decimal ApprovedSumm = Convert.ToDecimal(historyCustomerItem.ApprovedSumm);
                string SummWord = num2words.KgsPhrase(ApprovedSumm);
                lblRequestSummWord.Text = SummWord + " т."; //SummWord.Substring(0, (SummWord.Length - 3));
                lblRequestSummWord3.Text = SummWord + " т.";
                lblRequestSummWord4.Text = SummWord + " т.";
                lblRequestSummWord5.Text = SummWord + " т.";
                //lblRequestSummWord2.Text = SummWord.Substring(0, (SummWord.Length - 3));
                //lblRequestSummWord3.Text = SummWord.Substring(0, (SummWord.Length - 3));
                //lblRequestSummWord4.Text = SummWord.Substring(0, (SummWord.Length - 3));

                lblApprovedPeriod.Text = historyItem.ApprovedPeriod.ToString();
                lblApprovedPeriod2.Text = historyItem.ApprovedPeriod.ToString();
                lblApprovedPeriod3.Text = historyItem.ApprovedPeriod.ToString();
                lblApprovedPeriod4.Text = historyItem.ApprovedPeriod.ToString();
                lblCreditPurpose.Text = historyCustomerItem.CreditPurpose;
                //lblCreditPurpose2.Text = historyCustomerItem.CreditPurpose;

                double approvedRate = Math.Round(Convert.ToDouble(historyItem.ApprovedRate), 1);
                lblRequestRate.Text = approvedRate.ToString();
                lblRequestRate2.Text = "30";//approvedRate.ToString();
                lblRequestRate3.Text = approvedRate.ToString();
                lblRequestRate4.Text = approvedRate.ToString();
                lblRequestRate5.Text = approvedRate.ToString();
                //lblRequestRate6.Text = "30";//approvedRate.ToString();
                //lblRequestRate7.Text = "30";//approvedRate.ToString();
                lblRequestRate8.Text = approvedRate.ToString();
                lblRequestRate9.Text = approvedRate.ToString();
                lblRequestRate10.Text = approvedRate.ToString();





                var lastItemGraphics = lstGraphics.Last();
                lblEndDate.Text = (Convert.ToDateTime(lastItemGraphics.PayDate)).Date.ToString("dd.MM.yyyy"); //lastItemGraphics.PayDate.ToString();
                if (lstGraphics.Count > 0)
                {
                    gvGraphic.DataSource = lstGraphics;
                    gvGraphic.DataBind();
                }

                lblMainSum.Text = lstGraphics.Sum(r => r.MainSumm).ToString();
                lblPercentsSumm.Text = lstGraphics.Sum(r => r.PercentsSumm).ToString();
                lblTotalSumm.Text = lstGraphics.Sum(r => r.TotalSumm).ToString();
                lblApprovedSumm.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblApprovedSumm2.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblApprovedSummWord.Text = num2words.KgsPhrase(ApprovedSumm) + " т.";
                lblApprovedSumm3.Text = historyCustomerItem.ApprovedSumm.ToString();
                lblApprovedSummWord3.Text = num2words.KgsPhrase(ApprovedSumm) + " т.";

                //lblApprovedSummWord.Text = lblApprovedSummWord.Text.Substring(0, (lblApprovedSummWord.Text.Length - 3));
                lblCurrencyName.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
                lblCurrencyName2.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
                lblCurrencyName3.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
                lblCurrencyName4.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
                lblCurrencyName5.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
                lblCurrencyName6.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
                lblCurrencyName7.Text = sysCtrl.CurrencyGetItem(historyItem.ApprovedCurrencyID).CurrencyName;
                lblCreditIssueDate.Text = (Convert.ToDateTime(issueDate)).Date.ToString("dd.MM.yyyy"); // historyItem.IssueDate.ToString();
                //var request = dbRWZ.Requests.Where(r => r.CreditID == Convert.ToInt32(Session["CreditID"].ToString())).FirstOrDefault();
                int? usrID = dbRWZ.Requests.Where(r => r.CreditID == Convert.ToInt32(Session["CreditID"].ToString())).FirstOrDefault().AgentID;
                int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
                var roles = dbRWZ.RequestsRoles.Where(r => r.RoleID == usrRoleID).FirstOrDefault();

                DateTime docDate2 = Convert.ToDateTime(dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().AttorneyDocDate);
                string docDate = docDate2.ToString("dd.MM.yyyy");
                string docN = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().AttorneyDocName;



                int? ManagerID = Convert.ToInt32(Session["UserID"].ToString());
                var mngrItem = dbR.Customers.Where(r => r.CustomerID == ManagerID).FirstOrDefault();
                //lblManagerDocNum.Text = roles.ManagerDocNum;
                //lblManagerDocNum2.Text = roles.ManagerDocNum;
                //lblManagerDocNum3.Text = roles.ManagerDocNum;
                //lblManagerDocNum4.Text = roles.ManagerDocNum;
                //lblManagerDocNum5.Text = roles.ManagerDocNum;
                //lblManagerDocNum6.Text = roles.ManagerDocNum;
                //lblManagerDocDate.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");// roles.ManagerDocDate.ToString();
                //lblManagerDocDate2.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                //lblManagerDocDate3.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                //lblManagerDocDate4.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                //lblManagerDocDate5.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                //lblManagerDocDate6.Text = (Convert.ToDateTime(roles.ManagerDocDate)).Date.ToString("dd.MM.yyyy");
                //if (mngrItem != null)
                //{
                //    lblManagerFIO.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                //    lblManagerFIO2.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                //    lblManagerFIO3.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                //    lblManagerFIO4.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                //    lblManagerFIO5.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                //    lblManagerFIO6.Text = mngrItem.Surname + ' ' + mngrItem.CustomerName + ' ' + mngrItem.Otchestvo;
                //}

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

                if (request.GroupID == 30)
                //{ lblSellerAccount.Text = " на рассчетный счет Продавца "; }
                { lblSellerAccount.Text = " в кассу Продавца "; }
                else
                { lblSellerAccount.Text = " в кассу Продавца "; }

                lblManagerDocNum.Text = docN;
                lblManagerDocNum2.Text = docN;
                lblManagerDocNum3.Text = docN;
                lblManagerDocNum4.Text = docN;
                lblManagerDocNum5.Text = docN;
                //lblManagerDocNum6.Text = docN;
                lblManagerDocNum7.Text = docN;
                lblManagerDocDate.Text = docDate.ToString();
                lblManagerDocDate2.Text = docDate.ToString();
                lblManagerDocDate3.Text = docDate.ToString();
                lblManagerDocDate4.Text = docDate.ToString();
                lblManagerDocDate5.Text = docDate.ToString();
                //lblManagerDocDate6.Text = docDate.ToString();
                lblManagerDocDate7.Text = docDate.ToString();


                //var guarantor = sysCtrl.CustomerGetItem(Convert.ToInt32(hfGuarID.Value));
                //lblGuarFIO.Text = guarantor.Surname + " " + guarantor.CustomerName + " " + guarantor.Otchestvo;
                //lblDocumentSNGuar.Text = guarantor.DocumentSeries + guarantor.DocumentNo;
                //lblIssueDateGuar.Text = (Convert.ToDateTime(guarantor.IssueDate)).Date.ToString("dd.MM.yyyy");
                //lblIssueAuthorityGuar.Text = guarantor.IssueAuthority;



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

                var lstRequestsProducts = dbRWZ.RequestsProducts.Where(r => r.RequestID == RequestID);
                if (lstRequestsProducts != null)
                {
                    gvProducts.DataSource = lstRequestsProducts;
                    gvProducts.DataBind();
                    gvProducts2.DataSource = lstRequestsProducts;
                    gvProducts2.DataBind();
                    gvProducts3.DataSource = lstRequestsProducts;
                    gvProducts3.DataBind();
                }


                //lblRequestSumm2.Text = reqs.FirstOrDefault().ProductPrice.ToString();
                Decimal RequestSumm2 = 0;
                foreach (var reqpr in lstRequestsProducts)
                {
                    RequestSumm2 = RequestSumm2 + Convert.ToDecimal(reqpr.Price);
                }
                lblRequestSumm2.Text = RequestSumm2.ToString();

                lblRequestSumm4.Text = RequestSumm2.ToString();
                string SummWord2 = num2words.KgsPhrase(RequestSumm2);
                //lblRequestSummWord2.Text = SummWord2.Substring(0, (SummWord.Length - 3));
                //lblRequestSummWord3.Text = SummWord2.Substring(0, (SummWord.Length - 3));
                //lblRequestSummWord4.Text = SummWord2.Substring(0, (SummWord.Length - 3));
                lblRequestSummWord2.Text = SummWord2 + " т."; //SummWord2.Substring(0, (SummWord.Length - 3));
                //lblRequestSummWord3.Text = SummWord2 + " т."; //SummWord2.Substring(0, (SummWord.Length - 3));
                lblRequestSummWord4.Text = SummWord2 + " т."; //SummWord2.Substring(0, (SummWord.Length - 3));



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


                lblManagerFIO.Text = users.Fullname;
                lblManagerFIO2.Text = users.Fullname;
                lblManagerFIO3.Text = users.Fullname;
                lblManagerFIO4.Text = users.Fullname;
                lblManagerFIO5.Text = users.Fullname;
                lblManagerFIO6.Text = users.Fullname;
                lblManagerFIO7.Text = users.Fullname;
                lblManagerFIO8.Text = users.Fullname;

                //lblCustomerAddress.Text = customers.RegistrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
                //lblCustomerPassport.Text = customers.DocumentSeries + customers.DocumentNo + " " + customers.IssueAuthority + " " + Convert.ToDateTime(customers.IssueDate).Date.ToString("dd.MM.yyyy");
                lblCustomerPassport2.Text = customers.DocumentSeries + customers.DocumentNo + " " + customers.IssueAuthority + " " + Convert.ToDateTime(customers.IssueDate).Date.ToString("dd.MM.yyyy");
                lblCustomerPassport3.Text = customers.DocumentSeries + customers.DocumentNo + " " + customers.IssueAuthority + " " + Convert.ToDateTime(customers.IssueDate).Date.ToString("dd.MM.yyyy");
                lblCustomerPassport4.Text = customers.DocumentSeries + customers.DocumentNo + " " + customers.IssueAuthority + " " + Convert.ToDateTime(customers.IssueDate).Date.ToString("dd.MM.yyyy");
                decimal numtoword = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault().ProductPrice.Value;
                decimal amountDownPayment = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault().AmountDownPayment.Value;
                lblDateOfBirth.Text = Convert.ToDateTime(customers.DateOfBirth).Date.ToString("dd.MM.yyyy");

                lblAmountDownPayment.Text = amountDownPayment.ToString();
                lblAmountDownPaymentSumWord.Text = num2words.KgsPhrase(amountDownPayment) + " т.";



                if (groupID == 30) //Технодом
                {
                    //Технодом - 0015 и 0024 - Комиссия за открытие и обслуживание счета по кредиту / банковской гарантии - 1 % (от суммы кредита ежемесячно)
                    if ((request.RequestRate == 0) && ((request.RequestPeriod == 15) || (request.RequestPeriod == 24)))
                    {
                        lblComOpenAcc11.Text = "Комиссия за открытие и обслуживание счета по кредиту/ банковской гарантии";
                        lblComOpenAcc12.Text = "1 % (от суммы кредита ежемесячно)";
                        lblComOpenAcc21.Text = "";
                        lblComOpenAcc22.Text = "";

                    }

                    //if ((request.RequestRate == 0) && (request.RequestPeriod == 24)) { lblComOpenAcc12.Text = "1 % (от суммы кредита ежемесячно)"; }
                }
                if (groupID == 50) //ОсОО Планета Трейд-Сервис - ПЭ
                {
                    lblComOpenAcc12.Text = "500,00(Пятьсот сом, 00 тыйын)";
                }
                if (groupID == 51) //ОсОО "ИнтерКейДжи" - Sulpak
                {
                    lblComOpenAcc12.Text = "Отсутствует";
                }








                //string barCode = "sadsadsadsa";
                //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                //using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                //{
                //    using (Graphics graphics = Graphics.FromImage(bitMap))
                //    {
                //        Font oFont = new Font("IDAHC39M Code 39 Barcode", 16);
                //        PointF point = new PointF(2f, 2f);
                //        SolidBrush blackBrush = new SolidBrush(Color.Black);
                //        SolidBrush whiteBrush = new SolidBrush(Color.White);
                //        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                //        graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                //    }
                //    using (MemoryStream ms = new MemoryStream())
                //    {
                //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //        byte[] byteImage = ms.ToArray();

                //        Convert.ToBase64String(byteImage);
                //        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);

                //        Image1.ImageUrl = Convert.ToBase64String(byteImage);
                //        //bitmap.Save(ms, ImageFormat.Png);
                //        //pictureBox1.Image = bitmap;
                //        //pictureBox1.Height = bitmap.Height;
                //        //pictureBox1.Width = bitmap.Width;
                //    }
                //    PlaceHolder1.Controls.Add(imgBarCode);
                //}


                //string barcode = textBox1.Text;
                //Bitmap bitmap = new Bitmap(barcode.Length * 40, 150);
                //using (Graphics graphics = Graphics.FromImage(bitmap))
                //{
                //    Font oFont = new System.Drawing.Font("IDAHC39M Code 39 Barcode", 20);
                //    PointF point = new PointF(2f, 2f);
                //    SolidBrush Black = new SolidBrush(Color.Black);
                //    SolidBrush White = new SolidBrush(Color.White);
                //    graphics.FillRectangle(White, 0, 0, bitmap.Width, bitmap.Height);
                //    graphics.DrawString("*" + barcode + "*", oFont, Black, point);
                //}
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    bitmap.Save(ms, ImageFormat.Png);
                //    pictureBox1.Image = bitmap;
                //    pictureBox1.Height = bitmap.Height;
                //    pictureBox1.Width = bitmap.Width;

                //}



                //lblTotalSum.Text = numtoword.ToString();
                //lblSumToWord.Text = num2words.KgsPhrase(numtoword) + " т.";


                /*Поручитель*/
                var guarantorID = creditCtrl.GuarantorGetItem(Convert.ToInt32(hfCreditID.Value));
                if (guarantorID != -1)
                {
                    guarant.Visible = true;
                    var guarantItem = sysCtrl.CustomerGetItem(guarantorID);

                    //var guarantItem = sysCtrl.CustomerGetItem(GuarantorID);
                    lblIdentificationNumberGua.Text = guarantItem.IdentificationNumber;
                    lblGuaranterFIO.Text = guarantItem.Surname + " " + guarantItem.CustomerName + " " + guarantItem.Otchestvo;
                    lblGuaranterFIO2.Text = guarantItem.Surname + " " + guarantItem.CustomerName + " " + guarantItem.Otchestvo;
                    lblGuaranterFIO3.Text = guarantItem.Surname + " " + guarantItem.CustomerName + " " + guarantItem.Otchestvo;
                    lblDocumentSeriesGua.Text = guarantItem.DocumentSeries;
                    lblDocumentSeriesGua2.Text = guarantItem.DocumentSeries;
                    lblDocumentNoGua.Text = guarantItem.DocumentNo;
                    lblDocumentNoGua2.Text = guarantItem.DocumentNo;
                    lblIssueAuthorityGua.Text = guarantItem.IssueAuthority;
                    lblIssueAuthorityGua2.Text = guarantItem.IssueAuthority;
                    lblIssueDateGua.Text = (Convert.ToDateTime(guarantItem.IssueDate)).Date.ToString("dd.MM.yyyy"); //guarantItem.IssueDate.ToString();
                    lblIssueDateGua2.Text = (Convert.ToDateTime(guarantItem.IssueDate)).Date.ToString("dd.MM.yyyy"); //guarantItem.IssueDate.ToString();
                                                                                                                     //lblCreditNomer2.Text = historyItem.AgreementNo;
                    string regCustomerCountryGua = (guarantItem.RegistrationCountryID != null) ? dbR.Countries.Where(v => v.CountryID == guarantItem.RegistrationCountryID).ToList().FirstOrDefault().ShortName : "";
                    string regCustomerCityGua = (guarantItem.RegistrationCityID != null) ? dbR.Cities.Where(v => v.CityID == guarantItem.RegistrationCityID).ToList().FirstOrDefault().CityName : "";
                    string regCustomerStreetGua = dbR.Customers.Where(v => v.CustomerID == guarantItem.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
                    string regCustomerHouseGua = dbR.Customers.Where(v => v.CustomerID == guarantItem.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
                    string regCustomerFlatGua = dbR.Customers.Where(v => v.CustomerID == guarantItem.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
                    lblRegCustomerAddressGua.Text = regCustomerCountryGua + " " + regCustomerCityGua + " " + regCustomerStreetGua + " " + regCustomerHouseGua + " " + regCustomerFlatGua;
                    lblRegCustomerAddressGua2.Text = regCustomerCountryGua + " " + regCustomerCityGua + " " + regCustomerStreetGua + " " + regCustomerHouseGua + " " + regCustomerFlatGua;
                    string resCustomerCountryGua = (guarantItem.ResidenceCountryID != null) ? dbR.Countries.Where(v => v.CountryID == guarantItem.ResidenceCountryID).ToList().FirstOrDefault().ShortName : "";
                    string resCustomerCityGua = (guarantItem.ResidenceCityID != null) ? dbR.Cities.Where(v => v.CityID == guarantItem.ResidenceCityID).ToList().FirstOrDefault().CityName : "";
                    string resCustomerStreetGua = dbR.Customers.Where(v => v.CustomerID == guarantItem.CustomerID).ToList().FirstOrDefault().ResidenceStreet;
                    string resCustomerHouseGua = dbR.Customers.Where(v => v.CustomerID == guarantItem.CustomerID).ToList().FirstOrDefault().ResidenceHouse;
                    string resCustomerFlatGua = dbR.Customers.Where(v => v.CustomerID == guarantItem.CustomerID).ToList().FirstOrDefault().ResidenceFlat;
                    lblResCustomerAddressGua.Text = resCustomerCountryGua + " " + resCustomerCityGua + " " + resCustomerStreetGua + " " + resCustomerHouseGua + " " + resCustomerFlatGua;


                    decimal commision = 0; string NameOfCredit = "КапиталБанк";
                    if (request.RequestSumm > 99999) NameOfCredit = "Потребительский";
                    if ((request.RequestRate == 0) && (request.RequestPeriod == 3)) { commision = 300; NameOfCredit = "0-0-3"; }
                    if ((request.RequestRate == 0) && (request.RequestPeriod == 6)) { commision = 300; NameOfCredit = "0-0-6"; }
                    if ((request.RequestRate == 0) && (request.RequestPeriod == 9)) { commision = 300; NameOfCredit = "0-0-9"; }
                    if ((request.RequestRate == 0) && (request.RequestPeriod == 12)) { commision = 300; NameOfCredit = "0-0-12"; }
                    if ((request.RequestRate == 0) && (request.RequestPeriod == 15)) { commision = 0; NameOfCredit = "0-0-15"; }
                    if ((request.RequestRate == 0) && (request.RequestPeriod == 10)) { commision = 300; NameOfCredit = "10-10-10"; }
                    if ((request.RequestRate == 0) && (request.RequestPeriod == 27)) { commision = 300; NameOfCredit = "0-0-27"; }
                    if (request.IsEmployer == true) commision = 0;
                    //lblComission.Text = commision.ToString();





                }
                else { guarant.Visible = false; }
            }
            else
            {
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "График погашений не построен", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                //System.Windows.Forms.MessageBox.Show("График погашений не построен");
                MsgBox("График погашений не построен", this.Page, this);
            }
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