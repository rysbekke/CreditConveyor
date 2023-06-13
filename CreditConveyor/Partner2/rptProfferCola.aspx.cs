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
    public partial class rptProfferCola : System.Web.UI.Page
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        double chp, cho, y1, y2, zp, v, valp, OtherLoans, OtherLoans2;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int RequestID = Convert.ToInt32(Session["RequestID"].ToString());
            //Double RadNumTbRevenue = Convert.ToDouble(Session["RadNumTbRevenue"].ToString());

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
            Double RadNumTbAverageMonthSalary = Convert.ToDouble(request.AverageMonthSalary);
            double additionalIncome = Convert.ToDouble(request.AdditionalIncome);

            int registrationCityID = (customers.RegistrationCityID != null) ? (int)customers.RegistrationCityID : 0;
            int residenceCityID = (customers.ResidenceCityID != null) ? (int)customers.ResidenceCityID : 0;

            string registrationCityName = (registrationCityID != 0) ? dbR.Cities.Where(x => x.CityID == registrationCityID).FirstOrDefault().CityName : customers.RegistrationCityName;
            string residenceCityName = (residenceCityID != 0) ? dbR.Cities.Where(x => x.CityID == residenceCityID).FirstOrDefault().CityName : customers.ResidenceCityName;

            lblCustomerFIO.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerFIO2.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerRegAddress.Text = registrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
            lblCustomerResAddress.Text = residenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;
            lblContactPhone1.Text = customers.ContactPhone1;

            lblManagerFIO.Text = users.Fullname;
            lblCostPrice.Text = RadNumTbСostPrice.ToString();
            Decimal numtoword = 0;
            lblOffice.Text = dbRWZ.Offices.Where(o => o.ID == request.OfficeID).FirstOrDefault().ShortName;
            lblRequestSumm.Text = request.RequestSumm.ToString();
            lblRequestSumm2.Text = request.RequestSumm.ToString();
            lblRequestPeriod.Text = request.RequestPeriod.ToString();
           
            lblRequestPeriod2.Text = request.RequestPeriod.ToString();
            //lblAmountDownPayment.Text = requests.AmountDownPayment.ToString(); //Собственнные средства клиента
            lblYearPercent.Text = request.RequestRate.ToString();
            lblRate.Text = request.RequestRate.ToString();
            lblFamilyExpenses.Text = request.FamilyExpenses.ToString();
            lblAdditionalIncomes.Text = request.AdditionalIncome.ToString();
            if (lblAdditionalIncomes.Text == "") lblAdditionalIncomes.Text = "0";
            lblBusinessComment.Text = request.BusinessComment;
            //lblNameOfCredit.Text = "NameOfCredit";


            //var findate = dbRWZ.FinDataColas.Where(r => r.INN == customers.IdentificationNumber).OrderBy(u => u.DateLoad).Last();
            var findate = dbRWZ.FinDataColas.Where(r => r.Number == request.NumberCola).FirstOrDefault();
            lblShop.Text = findate.Name;
            lblSummCredit.Text = request.RequestSumm.ToString();
            lblCategory.Text = findate.Segment;
            lblCategory2.Text = findate.Segment;
            switch (findate.Segment)
            {
                case "BASIC": { lblMinBuy.Text = "9423"; lblMaxBuy.Text = "127976"; lblAverageBuy.Text = "100138"; lblMonthBuy.Text = "17323921"; OtherLoans2 = 70000; lblRevenueMonth.Text = "1335177"; } break;
                case "GOLD": { lblMinBuy.Text = "6251"; lblMaxBuy.Text = "67841"; lblAverageBuy.Text = "59627"; lblMonthBuy.Text = "19199979"; OtherLoans2 = 70000; lblRevenueMonth.Text = "795030"; } break;
                case "SILVER": { lblMinBuy.Text = "3206"; lblMaxBuy.Text = "47983"; lblAverageBuy.Text = "34445"; lblMonthBuy.Text = "44124049"; OtherLoans2 = 50000; lblRevenueMonth.Text = "459267"; } break;
                case "BRONZE": { lblMinBuy.Text = "5020"; lblMaxBuy.Text = "24967"; lblAverageBuy.Text = "16985"; lblMonthBuy.Text = "32271871"; OtherLoans2 = 20000; lblRevenueMonth.Text = "339704"; } break;
            }


            //lblRevenueMonth.Text = lblMonthBuy.Text; //(RadNumTbRevenue * CountWorkDay).ToString();
            //lblRevenueMonth.Text = (Convert.ToInt32(lblMonthBuy.Text) * 10).ToString();
            lblAverageCost.Text = (Convert.ToInt32(lblRevenueMonth.Text) / 1.25).ToString();
            

            /**/
            double s = Convert.ToDouble(request.RequestSumm);
            double n = Convert.ToDouble(request.RequestPeriod);
            double i, k = Convert.ToDouble(request.RequestSumm);
            //double stavka = Convert.ToDouble(request.RequestRate);
            //i = (stavka != 0) ? stavka / 12 / 100 : 0;
            //if (stavka == 0) k = s / n;
            //if ((stavka == 0) && (n == 15)) k = s / n + s / 100;
            //if ((stavka == 0) && (n == 24)) k = s / n + s / 100;
            //if ((stavka != 0)) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            OtherLoans = Convert.ToDouble(RadNumOtherLoans);
            bool f = false;
            bool zarplat = false;
            //int y22 = 40;
            //if (zarplat)
            //{
            //    zp = Convert.ToDouble(RadNumTbAverageMonthSalary);
            //    chp = (zp + Convert.ToDouble(request.AdditionalIncome)) - 7000;
            //    cho = chp - k;
            //    cho = cho - OtherLoans;
            //    y1 = 100 * cho / chp;
            //    if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
            //    y2 = 100 * (k + OtherLoans) / (zp + additionalIncome);
            //    if ((zp > 50000) || (zp == 50000)) y22 = 50;
            //    if (zp < 50000) y22 = 40;

            //    if ((y1 > 24) && (y2 < y22)) f = true;
            //}
            if (!zarplat)
            {
                double costprice = Convert.ToDouble(RadNumTbСostPrice);
                //double v = Convert.ToDouble(Convert.ToDouble(RadNumTbRevenue) * CountWorkDay);
                //double valp = (costprice == 0) ? v : (v * costprice) / (100 + costprice);
                double v = Convert.ToDouble(lblRevenueMonth.Text);
                valp = v - Convert.ToDouble(lblAverageCost.Text);
                //lblValP.Text = valp.ToString();

                lblOverHead.Text = request.Overhead.ToString();
                lblValP.Text = String.Format("{0:0.00}", valp);
                lblBusinessProfit.Text = String.Format("{0:0.00}", Convert.ToDouble(valp - Convert.ToDouble(request.Overhead)));
                lblTotalСonsumption.Text = (request.Overhead + request.FamilyExpenses).ToString();

                lblTotalIncome.Text = String.Format("{0:0.00}", Convert.ToDouble(lblBusinessProfit.Text) + Convert.ToDouble(lblAdditionalIncomes.Text));


                //chp = (valp + Convert.ToDouble(request.AdditionalIncome)) - Convert.ToDouble(RadNumTbOverhead) - Convert.ToDouble(RadNumTbFamilyExpenses);
                chp = (Convert.ToDouble(lblTotalIncome.Text) - Convert.ToDouble(RadNumTbFamilyExpenses));
                //cho = cho - OtherLoans;
                //if ((valp > 50000) || (valp == 50000)) y22 = 50;
                //if (valp < 50000) y22 = 40;

                //if (chp > 0)
                {
                    cho = Convert.ToDouble(lblRevenueMonth.Text) - k - OtherLoans - Convert.ToDouble(lblTotalСonsumption.Text);
                    y1 = 100 * cho / chp;
                    if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                    //y2 = 100 * (k + OtherLoans) / valp;
                    //if ((y1 > 24) && (y2 < y22)) f = true;
                    if (y1 > 25) f = true;

                    if (OtherLoans <= OtherLoans2) { f = true; lblOtherLoansComment.Text = "да, выдача кредита возможно"; }
                    else { lblOtherLoansComment.Text = "нет, выдача кредита невозможно"; }

                    if ((y1 > 25) && (f==true))lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
                    else lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
                    //if (y2 < y22) lblIssuanceOfCredit2.Text = "да, выдача кредита возможно";
                    //else lblIssuanceOfCredit2.Text = "нет, выдача кредита невозможно";

                }

            }

            //if ((y1 > 24) && (y2 < y22)) f = true;
            if (y1 > 25) f = true;

            /**/
            lblChp.Text = Math.Round(chp, 0).ToString();
            lblCho.Text = Math.Round(cho, 0).ToString();
            lblK.Text = Math.Round(k, 0).ToString();
            //lblK2.Text = Math.Round(k, 0).ToString();
            lblY1.Text = Math.Round(y1, 2).ToString();
            //lblY2.Text = Math.Round(y2, 2).ToString();
            lblOtherLoans.Text = Math.Round(OtherLoans, 2).ToString();
            //if (f) lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
            //else lblIssuanceOfCredit.Text = "нет, выдача кредита невозможно";
            lblBranch.Text = dbRWZ.Branches.Where(r => r.ID == request.BranchID).FirstOrDefault().Name;
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
        }
    }
}