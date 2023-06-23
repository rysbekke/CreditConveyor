﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat;
using СreditСonveyor.Data.Microcredit;

namespace СreditСonveyor.Microcredit
{
    public partial class rptProffer : System.Web.UI.Page
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["connectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);

        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        double chp, cho, y1, y2, zp, v, valp, OtherLoans;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int RequestID = Convert.ToInt32(Session["RequestID"].ToString());
            //Double RadNumTbAverageMonthSalary = Convert.ToDouble(Session["RadNumTbAverageMonthSalary"].ToString());
            //Double RadNumTbRevenue = Convert.ToDouble(Session["RadNumTbRevenue"].ToString());
            Double RadNumTbСostPrice = Convert.ToDouble(Session["RadNumTbСostPrice"].ToString());
            Double RadNumTbOverhead = Convert.ToDouble(Session["RadNumTbOverhead"].ToString());
            Double RadNumTbFamilyExpenses = Convert.ToDouble(Session["RadNumTbFamilyExpenses"].ToString());
            Double RadNumOtherLoans = Convert.ToDouble(Session["RadNumOtherLoans"].ToString());
            CreditController creditCtrl = new CreditController();
            SysController sysCtrl = new SysController();
            var requestsUsersRoles = sysCtrl.RequestsUsersRoleGetItem(usrID);
            var users = sysCtrl.UsersGetItem(usrID);
            var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
            var request = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();

            int registrationCityID = (customers.RegistrationCityID != null) ? (int)customers.RegistrationCityID : 0;
            int residenceCityID = (customers.ResidenceCityID != null) ? (int)customers.ResidenceCityID : 0;

            string registrationCityName = (registrationCityID != 0) ? dbR.Cities.Where(x => x.CityID == registrationCityID).FirstOrDefault().CityName : customers.RegistrationCityName;
            string residenceCityName = (residenceCityID != 0) ? dbR.Cities.Where(x => x.CityID == residenceCityID).FirstOrDefault().CityName : customers.ResidenceCityName;

            chkbxTypeOfCollateral.Items[0].Selected = (request.isNoPledge != null) ? (bool)request.isNoPledge : false;
            chkbxTypeOfCollateral.Items[1].Selected = (request.isGuarantor != null) ? (bool)request.isGuarantor : false;
            chkbxTypeOfCollateral.Items[2].Selected = (request.isPledger != null) ? (bool)request.isPledger : false;

            if (chkbxTypeOfCollateral.Items[1].Selected == true) 
            { 
                pnlGuarantor.Visible = true; 
            } 
            else 
            { 
                pnlGuarantor.Visible = false; 
            }

            if (chkbxTypeOfCollateral.Items[2].Selected == true) 
            { 
                pnlGuarantees.Visible = true; 
            } 
            else 
            { 
                pnlGuarantees.Visible = false; 
            }


            if (request.isGuarantor == true)
            {

                lblGuarantorFIO.Text = request.GuarantorSurname + " " + request.GuarantorName + " " + request.GuarantorOtchestvo;
                lblGuarantorINN.Text = request.GuarantorIdentificationNumber;
                //lblGurantorPhone.Text = lst.GuarantorOtchestvo;
                //lblGurantorRegAddress.Text = lst.GuarantorIdentificationNumber;
                //lblGurantorResAddress.Text = lst.GuarantorIdentificationNumber;
            }

            if (request.isPledger == true)
            {
                lblPledgerFIO.Text = request.PledgerSurname + " " + request.PledgerName + " " + request.PledgerOtchestvo;
                lblPledgerINN.Text = request.PledgerIdentificationNumber;
                
            }


            lblCustomerFIO.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerFIO2.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerRegAddress.Text = registrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
            lblCustomerResAddress.Text = residenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;
            lblContactPhone1.Text = customers.ContactPhone1;
            lblWorkName.Text = customers.WorkName;
            lblWorkPosition.Text = customers.WorkPosition;
            lblAverageMonthSalary.Text = dbRWZ.Requests.Where(r => r.RequestID == RequestID).FirstOrDefault().AverageMonthSalary.ToString();
            lblAverageMonthSalary2.Text = dbRWZ.Requests.Where(r => r.RequestID == RequestID).FirstOrDefault().AverageMonthSalary.ToString();
            Double RadNumTbAverageMonthSalary = Convert.ToDouble(request.AverageMonthSalary);

            //var lstRequestsProducts = dbRWZ.RequestsProducts.Where(r => r.RequestID == RequestID);
            //if (lstRequestsProducts != null)
            //{
            //    gvProducts.DataSource = lstRequestsProducts;
            //    gvProducts.DataBind();
            //}

            //Decimal numtoword = 0;
            //foreach (var reqpr in lstRequestsProducts)
            //{
            //    numtoword = numtoword + Convert.ToDecimal(reqpr.Price);
            //}
            ////decimal numtoword = requests.ProductPrice.Value;
            //lblTotalSum.Text = numtoword.ToString();
            //lblTotalSum.Text = request.RequestSumm.ToString();


            lblRequestSumm.Text = request.RequestSumm.ToString();
            lblRequestPeriod.Text = request.RequestPeriod.ToString();
            lblRequestSumm2.Text = request.RequestSumm.ToString();
            lblRequestPeriod2.Text = request.RequestPeriod.ToString();
            //lblAmountDownPayment.Text = requests.AmountDownPayment.ToString(); собственные средства клиента
            lblYearPercent.Text = request.RequestRate.ToString();
            lblRate.Text = request.RequestRate.ToString();
            lblFamilyExpenses.Text = "7000"; //requests.FamilyExpenses.ToString();
            lblTotalСonsumption.Text = "7000"; //requests.FamilyExpenses.ToString();
            lblAdditionalIncom.Text = request.AdditionalIncome.ToString();
            lblSeller.Text = dbRWZ.Groups.Where(g => g.GroupID == request.GroupID).FirstOrDefault().GroupName;

            lblOffice.Text = dbRWZ.Offices.Where(o => o.ID == request.OfficeID).FirstOrDefault().ShortName;

            decimal commision = 0; string NameOfCredit = "КапиталБанк";
            if (request.RequestSumm > 99999) NameOfCredit = "Потребительский";
            if ((request.RequestRate == 0) && (request.RequestPeriod == 3)) { commision = 0; NameOfCredit = "0-0-3"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 6)) { commision = 0; NameOfCredit = "0-0-6"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 9)) { commision = 0; NameOfCredit = "0-0-9"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 12)) { commision = 0; NameOfCredit = "0-0-12"; }
            if (request.IsEmployer == true) commision = 0;
            lblComission.Text = commision.ToString();
            lblNameOfCredit.Text = NameOfCredit;
            /**/

            double s = Convert.ToDouble(request.RequestSumm);
            double n = Convert.ToDouble(request.RequestPeriod);
            double i, k = 0;
            int y22 = 40;

            double stavka = Convert.ToDouble(request.RequestRate);
            i = (stavka != 0) ? stavka / 12 / 100 : 0;
            if ((stavka == 0)) k = s / n;
            if (stavka != 0) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            bool f = false;
            bool zarplat = true;

            if (zarplat)
            {
                zp = Convert.ToDouble(RadNumTbAverageMonthSalary);
                if ((zp > 50000) || (zp == 50000)) y22 = 50;
                if (zp < 50000) y22 = 40;
                OtherLoans = Convert.ToDouble(RadNumOtherLoans);
                chp = (zp + Convert.ToDouble(request.AdditionalIncome)) - 7000;
                cho = chp - k;
                cho = cho - OtherLoans;
                y1 = 100 * cho / chp;
                if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                y2 = 100 * (k + OtherLoans) / (zp + Convert.ToDouble(request.AdditionalIncome));
                if ((y1 >= 20) && (y2 < y22)) f = true;
                if (y1 >= 20) lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
                else lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
                if (y2 < y22) lblIssuanceOfCredit2.Text = "да, выдача кредита возможно";
                else lblIssuanceOfCredit2.Text = "нет, выдача кредита невозможно";
            }
            //else  
            //{
            //    v = Convert.ToDouble(RadNumTbRevenue);
            //    valp = (v * Convert.ToDouble(RadNumTbСostPrice)) / (100 + Convert.ToDouble(RadNumTbСostPrice));
            //    chp = valp - Convert.ToDouble(RadNumTbOverhead) - Convert.ToDouble(RadNumTbFamilyExpenses);
            //    if (chp > 0)
            //    {
            //        cho = chp - k;
            //        y1 = 100 * cho / chp;
            //        y2 = 100 * k / v;
            //        if ((y1 > 29) && (y2 < 49)) f = true;
            //    }
            //}
            /**/
            lblChp.Text = Math.Round(chp, 0).ToString();
            lblCho.Text = Math.Round(cho, 0).ToString();
            lblK.Text = Math.Round(k, 0).ToString();
            lblK2.Text = Math.Round(k, 0).ToString();
            lblY1.Text = Math.Round(y1, 2).ToString();
            lblY2.Text = Math.Round(y2, 2).ToString();
            lblOtherLoans.Text = Math.Round(OtherLoans, 2).ToString();
            //if (f) lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
            //else lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
            lblBranch.Text = dbR.Branches.Where(r => r.ID == request.BranchID).FirstOrDefault().Name;
            lblRequestN.Text = request.RequestID.ToString();
            // Соколов, Жумашева, Молдогазиева, Сейдилда уулу Амантур, Сандиева
            if ((usrID == 6075) || //Соколов
                                   //(usrID == 6521) || //Жумашева
                (usrID == 6536) ||  //Молдогазиева
                (usrID == 6977) || //Сейдилда уулу
                //(usrID == 6249) || //Сандиева
                (usrID == 7552) || //Гриценко
                (usrID == 4562) ||//Абдыкасымова
                (usrID == 7141) || //Сыдыкова
                (usrID == 4445)) //Халикова
            {
                lblReqDecision.Text = "РЕШЕНИЕ УПОЛНОМОЧЕННОГО ЛИЦА ";
                lblDecision1.Text = "Уполномоченное лицо";
                lblDecision2.Text = "";
                lblDecision3.Text = "";
                lblDecision4.Text = "";
            }
            else
            {
                lblReqDecision.Text = "РЕШЕНИЕ КРЕДИТНОГО КОМИТЕТА _______________ ";
                lblDecision1.Text = "Председатель Кредитного комитета";
                lblDecision2.Text = "Член Кредитного комитета";
                lblDecision3.Text = "Член Кредитного комитета";
                lblDecision4.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Секретарь КК&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ФИО";
            }

            refreshGuarantees(request.RequestID);
        }

        public void refreshGuarantees(int requestId)
        {
            int amoundp = 10;
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lstRequestsProducts = dbRWZ.Guarantees.Where(r => r.RequestID == Convert.ToInt32(requestId)).ToList();
            decimal totalSumAssessedPrice = Convert.ToDecimal(lstRequestsProducts.Sum(r => r.AssessedPrice));
            decimal totalSumMarketPrice = Convert.ToDecimal(lstRequestsProducts.Sum(r => r.MarketPrice));
            //lblTotalSum.Text = totalSum.ToString();
            if (lstRequestsProducts.Count > 0)     // != null)
            {
                List<Guarantee> drLastRow = new List<Guarantee>() { new Guarantee { Name = "Итого:",  Description = "", Address = "",  AssessedPrice = totalSumAssessedPrice, MarketPrice = totalSumMarketPrice, Coefficient = null } };
                //drLastRow.Name = "Итого залоговая стоимость:";
                //drLastRow.AssessedPrice = totalSum;

                lstRequestsProducts.Add(drLastRow[0]);
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
                dt.Columns.Add("Сoefficient");

                //DataRow dr = dt.NewRow();
                //dt.Rows.Add(dr);
                gvGuarantees.DataSource = dt;
                gvGuarantees.DataBind();
            }
        }
    }
}