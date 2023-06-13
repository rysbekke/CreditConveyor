using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using СreditСonveyor.Data.Card;

namespace СreditСonveyor.Card
{
    public partial class rptProfferBusiness : System.Web.UI.Page
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        double chp, cho, y1, y2, zp, v, valp, OtherLoans;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
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
            var request = dbRWZ.CardRequests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();
            Double RadNumTbAverageMonthSalary = Convert.ToDouble(request.AverageMonthSalary);
            double additionalIncome = Convert.ToDouble(request.AdditionalIncome);
            lblCustomerFIO.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerFIO2.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerRegAddress.Text = customers.RegistrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
            lblCustomerResAddress.Text = customers.ResidenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;
            lblContactPhone1.Text = customers.ContactPhone1;
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



            lblRequestSumm.Text = request.RequestSumm.ToString();
            lblRequestPeriod.Text = request.RequestPeriod.ToString();
            lblRequestSumm2.Text = request.RequestSumm.ToString();
            lblRequestPeriod2.Text = request.RequestPeriod.ToString();
            //lblAmountDownPayment.Text = requests.AmountDownPayment.ToString(); //Собственнные средства клиента
            lblYearPercent.Text = request.RequestRate.ToString();
            lblRate.Text = request.RequestRate.ToString();
            lblFamilyExpenses.Text = request.FamilyExpenses.ToString();
            lblAdditionalIncomes.Text = request.AdditionalIncome.ToString();
            lblBusinessComment.Text = request.BusinessComment;

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
            lblComission.Text = commision.ToString();
            lblNameOfCredit.Text = NameOfCredit;

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
            if ((stavka == 0) && (n == 10)) k = s / n + s / 100;
            if ((stavka == 0) && (n == 15)) k = s / n + s / 100;
            if ((stavka == 25) || (stavka == 29) || (stavka == 30)) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);

            OtherLoans = Convert.ToDouble(RadNumOtherLoans);
            bool f = false;
            bool zarplat = false;

            if (zarplat)
            {
                zp = Convert.ToDouble(RadNumTbAverageMonthSalary);

                chp = zp - 5000 + Convert.ToDouble(request.AdditionalIncome);
                cho = chp - k;
                cho = cho - OtherLoans;
                y1 = 100 * cho / chp;
                y2 = 100 * (k + OtherLoans) / (zp + additionalIncome);

                if ((y1 > 24) && (y2 < 49)) f = true;
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
                if (chp > 0)
                {
                    cho = chp - k;
                    cho = cho - OtherLoans;
                    y1 = 100 * cho / chp;
                    y2 = 100 * (k + OtherLoans) / valp;
                    if ((y1 > 24) && (y2 < 49)) f = true;

                    if (y1 > 24) lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
                    else lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
                    if (y2 < 49) lblIssuanceOfCredit2.Text = "да, выдача кредита возможно";
                    else lblIssuanceOfCredit2.Text = "нет, выдача кредита невозможно";

                }

            }

            if ((y1 > 24) && (y2 < 49)) f = true;

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
            lblY1.Text = Math.Round(y1, 0).ToString();
            lblY2.Text = Math.Round(y2, 0).ToString();
            lblOtherLoans.Text = Math.Round(OtherLoans, 0).ToString();
            //if (f) lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
            //else lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
        }
    }
}