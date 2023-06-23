﻿using System;
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
    public partial class rptProfferOnlineBusiness : System.Web.UI.Page
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
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
            // Double RadNumTbAverageMonthSalary = Convert.ToDouble(Session["RadNumTbAverageMonthSalary"].ToString());
            Double RadNumTbRevenue = Convert.ToDouble(Session["RadNumTbRevenue"].ToString());

            Double RadNumTbСostPrice = Convert.ToDouble(Session["RadNumTbСostPrice"].ToString());
            Double RadNumTbOverhead = Convert.ToDouble(Session["RadNumTbOverhead"].ToString());
            Double RadNumTbFamilyExpenses = Convert.ToDouble(Session["RadNumTbFamilyExpenses"].ToString());
            Double RadNumOtherLoans = Convert.ToDouble(Session["RadNumOtherLoans"].ToString());
            int CountWorkDay = Convert.ToInt32(Session["CountWorkDay"].ToString());


            CreditController creditCtrl = new CreditController();
            SysController sysCtrl = new SysController();
            var requestsUsersRoles = sysCtrl.RequestsUsersRoleGetItem(usrID);
            var users = sysCtrl.UsersGetItem(usrID);
            var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));

            var request = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();
            var journal = dbRWZ.JournalCardCreditDCBKG.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();

            int registrationCityID = (customers.RegistrationCityID != null) ? (int)customers.RegistrationCityID : 0;
            int residenceCityID = (customers.ResidenceCityID != null) ? (int)customers.ResidenceCityID : 0;

            string registrationCityName = (registrationCityID != 0) ? dbR.Cities.Where(x => x.CityID == registrationCityID).FirstOrDefault().CityName : customers.RegistrationCityName;
            string residenceCityName = (residenceCityID != 0) ? dbR.Cities.Where(x => x.CityID == residenceCityID).FirstOrDefault().CityName : customers.ResidenceCityName;

            if (journal != null)
            {
                lblVerificator.Text = journal.Verificator;
                lblNJournal.Text = journal.Nomer1 + "/" + journal.Nomer2;
                lblDateJournal.Text = Convert.ToDateTime(journal.DateVerif).ToString("dd.MM.yyyy");
            }

            Double RadNumTbAverageMonthSalary = Convert.ToDouble(request.AverageMonthSalary);
            double additionalIncome = Convert.ToDouble(request.AdditionalIncome);

            lblCustomerFIO.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerFIO2.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerRegAddress.Text = registrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
            lblCustomerResAddress.Text = residenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;
            lblContactPhone1.Text = customers.ContactPhone1;
            lblSeller.Text = dbRWZ.Groups.Where(g => g.GroupID == request.GroupID).FirstOrDefault().GroupName;
            //      lblWorkName.Text = customers.WorkName;
            //      lblWorkPosition.Text = customers.WorkPosition;
            lblRevenueMonth.Text = (RadNumTbRevenue * CountWorkDay).ToString();
            lblCostPrice.Text = RadNumTbСostPrice.ToString();

            var lstRequestsProducts = dbRWZ.RequestsProducts.Where(r => r.RequestID == RequestID);
            if (lstRequestsProducts != null)
            {
                gvProducts.DataSource = lstRequestsProducts;
                gvProducts.DataBind();
            }

            Decimal numtoword = 0;
            foreach (var reqpr in lstRequestsProducts)
            {
                numtoword = numtoword + Convert.ToDecimal(reqpr.Price);
            }
            //decimal numtoword = requests.ProductPrice.Value;
            lblTotalSum.Text = numtoword.ToString();

            lblOffice.Text = dbRWZ.Offices.Where(o => o.ID == request.OfficeID).FirstOrDefault().ShortName;

            lblRequestSumm.Text = request.RequestSumm.ToString();
            lblRequestPeriod.Text = request.RequestPeriod.ToString();
            lblRequestSumm2.Text = request.RequestSumm.ToString();
            lblRequestPeriod2.Text = request.RequestPeriod.ToString();
            //lblAmountDownPayment.Text = requests.AmountDownPayment.ToString(); //Собственнные средства клиента
            lblYearPercent.Text = request.RequestRate.ToString();
            lblCardAccount.Text = request.CardAccount;
            lblRate.Text = request.RequestRate.ToString();
            lblFamilyExpenses.Text = request.FamilyExpenses.ToString();
            lblAdditionalIncomes.Text = request.AdditionalIncome.ToString();
            lblBusinessComment.Text = request.BusinessComment;
            //lblCommisionType.Text = "сом";
            decimal commision = 0; string NameOfCredit = "КапиталБанк";
            if (request.RequestSumm > 99999) NameOfCredit = "Потребительский";
            if ((request.RequestRate == 0) && (request.RequestPeriod == 3)) { commision = 300; NameOfCredit = "0-0-3"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 6)) { commision = 300; NameOfCredit = "0-0-6"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 9)) { commision = 300; NameOfCredit = "0-0-9"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 10)) { commision = 300; NameOfCredit = "10-10-10"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 12)) { commision = 300; NameOfCredit = "0-0-12"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 15)) { commision = 0; NameOfCredit = "0-0-15"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 26)) { commision = 300; NameOfCredit = "0-0-26"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 27)) { commision = 300; NameOfCredit = "0-0-27"; }
            if (request.IsEmployer == true) commision = 0;
            if (request.GroupID == 51) commision = 0;
            //lblComission.Text = commision.ToString();
            //lblNameOfCredit.Text = NameOfCredit;

            if (request.OrgID == 27)
            {

                if ((request.RequestRate == 0) && (request.RequestPeriod == 3)) { commision = 3; NameOfCredit = "0-0-3"; }
                if ((request.RequestRate == 0) && (request.RequestPeriod == 6)) { commision = 3; NameOfCredit = "0-0-6"; }
                if ((request.RequestRate == 0) && (request.RequestPeriod == 12)) { commision = 2; NameOfCredit = "0-0-12"; }
                //lblComission.Text = commision.ToString();
                //lblCommisionType.Text = "%";
            }

            //lblComission.Text = "150";
            /**/
            double s = Convert.ToDouble(request.RequestSumm);
            double n = Convert.ToDouble(request.RequestPeriod);
            //double stavka = Convert.ToDouble(requests.RequestRate);
            //double i = stavka / 12 / 100;
            //double k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            double i, k = 0;

            double stavka = Convert.ToDouble(request.RequestRate);
            i = (stavka != 0) ? stavka / 12 / 100 : 0;
            if (stavka == 0) k = s / n;
            //if ((stavka == 0) && (n == 10)) k = s / n + s / 100;
            //if ((stavka == 0) && (n == 15)) k = s / n + s / 100;
            if ((stavka == 0) && (n == 15)) k = s / n + s / 100;
            if ((stavka == 0) && (n == 24)) k = s / n + s / 100;

            if ((stavka != 0)) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);

            OtherLoans = Convert.ToDouble(RadNumOtherLoans);
            bool f = false;
            bool zarplat = false;
            int y22 = 40;

            if (zarplat)
            {
                zp = Convert.ToDouble(RadNumTbAverageMonthSalary);

                chp = (zp + Convert.ToDouble(request.AdditionalIncome)) - 7000;
                cho = chp - k;
                cho = cho - OtherLoans;
                y1 = 100 * cho / chp;
                if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                y2 = 100 * (k + OtherLoans) / (zp + additionalIncome);
                if ((zp > 50000) || (zp == 50000)) y22 = 50;
                if (zp < 50000) y22 = 40;

                if ((y1 >= 20) && (y2 < y22)) f = true;
            }
            if (!zarplat)
            {
                //zp = Convert.ToDouble(RadNumTbAverageMonthSalary);
                //OtherLoans = Convert.ToDouble(RadNumOtherLoans);
                //chp = zp - 5000;
                //cho = chp - k;
                //cho = cho - OtherLoans;
                //y1 = 100 * cho / chp;
                //y2 = 100 * (k + OtherLoans) / zp;


                double costprice = Convert.ToDouble(RadNumTbСostPrice);
                double v = Convert.ToDouble(Convert.ToDouble(RadNumTbRevenue) * CountWorkDay);
                double valp = (costprice == 0) ? v : (v * costprice) / (100 + costprice);
                //lblValP.Text = valp.ToString();
                lblValP.Text = String.Format("{0:0.00}", valp);
                chp = (valp + Convert.ToDouble(request.AdditionalIncome)) - Convert.ToDouble(RadNumTbOverhead) - Convert.ToDouble(RadNumTbFamilyExpenses);
                //cho = cho - OtherLoans;
                if ((valp > 50000) || (valp == 50000)) y22 = 50;
                if (valp < 50000) y22 = 40;

                //if (chp > 0)
                {
                    cho = chp - k;
                    cho = cho - OtherLoans;
                    y1 = 100 * cho / chp;
                    if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                    y2 = 100 * (k + OtherLoans) / valp;
                    if ((y1 >= 20) && (y2 < y22)) f = true;

                    if (y1 >= 20) lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
                    else lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
                    if (y2 < y22) lblIssuanceOfCredit2.Text = "да, выдача кредита возможно";
                    else lblIssuanceOfCredit2.Text = "нет, выдача кредита невозможно";

                }

            }

            if ((y1 >= 20) && (y2 < y22)) f = true;

            // lblChp.Text = chp.ToString();
            lblOverHead.Text = request.Overhead.ToString();

            lblTotalСonsumption.Text = (request.Overhead + request.FamilyExpenses).ToString();







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
                //lblDecision2.Text = "";
                //lblDecision3.Text = "";
                //lblDecision4.Text = "";
            }
            else
            {
                //lblReqDecision.Text = "РЕШЕНИЕ КРЕДИТНОГО КОМИТЕТА _______________ ";
                //lblDecision1.Text = "Председатель Кредитного комитета";
                //lblDecision2.Text = "Член Кредитного комитета";
                //lblDecision3.Text = "Член Кредитного комитета";
                //lblDecision4.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Секретарь КК&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ФИО";
            }
        }
    }
}