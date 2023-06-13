using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat.Data.Microcredit;

namespace Zamat.Microcredit
{
    public partial class rptProfferNano : System.Web.UI.Page
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["connectionStringZ"].ToString(); dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
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
            Double RadNumTbAverageMonthSalary = Convert.ToDouble(Session["RadNumTbAverageMonthSalary"].ToString());
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
            var requests = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();
            var journal = dbRWZ.JournalNanoBeeline.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();

            if (journal != null)
            {
                lblVerificator.Text = journal.Verificator;
                lblNJournal.Text = journal.Nomer1 + "/" + journal.Nomer2;
                lblDateJournal.Text = Convert.ToDateTime(journal.DateVerif).ToString("dd.MM.yyyy");
            }

            lblCustomerFIO.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerFIO2.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerRegAddress.Text = customers.RegistrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
            lblCustomerResAddress.Text = customers.ResidenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;
            lblContactPhone1.Text = customers.ContactPhone1;
            lblWorkName.Text = customers.WorkName;
            lblWorkPosition.Text = customers.WorkPosition;

            //if (requests.GroupID == 91)
            //{
            //    //string scorBall = dbRWZ.RequestsHistories.Where(r => ((r.AgentID == 6133) && (r.RequestID == RequestID))).FirstOrDefault().note;
            //    //if (scorBall.Contains("Скорбалл:A")) { scorBall = "20000"; lblZP.Text = "Скорбалл:A"; }
            //    //if (scorBall.Contains("Скорбалл:B")) { scorBall = "15000"; lblZP.Text = "Скорбалл:B"; }
            //    //if (scorBall.Contains("Скорбалл:C")) { scorBall = "10000"; lblZP.Text = "Скорбалл:C"; }
            //    //if (scorBall.Contains("Скорбалл:0")) { scorBall = "0"; lblZP.Text = "Скорбалл:0"; }

            //    lblZP.Text = requests.NanoScorePoints;
            //    if (requests.NanoScorePoints == "Скорбалл:A")
            //        lblAverageMonthSalary.Text = "20000"; 
            //    if (requests.NanoScorePoints == "Скорбалл:B")
            //        lblAverageMonthSalary.Text = "15000";
            //    if (requests.NanoScorePoints == "Скорбалл:C")
            //        lblAverageMonthSalary.Text = "10000";
            //    if (requests.NanoScorePoints == "Скорбалл:А")
            //        lblAverageMonthSalary.Text = "20000";
            //    if (requests.NanoScorePoints == "Скорбалл:В")
            //        lblAverageMonthSalary.Text = "15000";
            //    if (requests.NanoScorePoints == "Скорбалл:Б")
            //        lblAverageMonthSalary.Text = "15000";
            //    if (requests.NanoScorePoints == "Скорбалл:С")
            //        lblAverageMonthSalary.Text = "10000";

            //    RadNumTbAverageMonthSalary = Convert.ToDouble(lblAverageMonthSalary.Text);
            //    //lblAverageMonthSalary.Text = requests.AverageMonthSalary.ToString();

            //}
            //else
            {
                lblAverageMonthSalary.Text = requests.AverageMonthSalary.ToString();
                lblZP.Text = "Зарплата клиента";
            }
            lblAverageMonthSalary2.Text = (requests.AverageMonthSalary + requests.AdditionalIncome).ToString();
            lblSeller.Text = dbRWZ.Groups.Where(g => g.GroupID == requests.GroupID).FirstOrDefault().GroupName;
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



            lblRequestSumm.Text = requests.RequestSumm.ToString();
            lblRequestPeriod.Text = requests.RequestPeriod.ToString();
            lblRequestSumm2.Text = requests.RequestSumm.ToString();
            lblRequestPeriod2.Text = requests.RequestPeriod.ToString();
            //lblAmountDownPayment.Text = requests.AmountDownPayment.ToString(); собственные средства клиента
            lblYearPercent.Text = requests.RequestRate.ToString();
            lblRate.Text = requests.RequestRate.ToString();
            lblFamilyExpenses.Text = "7000"; //requests.FamilyExpenses.ToString();
            lblTotalСonsumption.Text = "7000"; //requests.FamilyExpenses.ToString();
            lblAdditionalIncomes.Text = requests.AdditionalIncome.ToString();
            //lblComission.Text = "150";
            /**/
            double s = Convert.ToDouble(requests.RequestSumm);
            double n = Convert.ToDouble(requests.RequestPeriod);
            double stavka = Convert.ToDouble(requests.RequestRate);
            double i, k = 0;
            int y22 = 40;

            i = (stavka != 0) ? stavka / 12 / 100 : 0;
            if ((stavka == 0)) k = s / n;
            if (stavka != 0) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);
            //k = 5500;
            k = Convert.ToInt32(requests.RequestSumm);
            bool f = false;
            bool zarplat = true;

            if (zarplat)
            {
                zp = Convert.ToDouble(RadNumTbAverageMonthSalary);
                if ((zp > 50000) || (zp == 50000)) y22 = 50;
                if (zp < 50000) y22 = 40;
                OtherLoans = Convert.ToDouble(RadNumOtherLoans);
                chp = (zp + Convert.ToDouble(requests.AdditionalIncome)) - 7000;
                cho = chp - k;
                cho = cho - OtherLoans;
                y1 = 100 * cho / chp;
                if ((cho < 0) && (chp < 0)) y1 = y1 * (-1);
                y2 = 100 * (k + OtherLoans) / (zp + Convert.ToDouble(requests.AdditionalIncome));

                if ((y1 >= 25) && (y2 < y22)) f = true;
                if (y1 >= 25) lblIssuanceOfCredit.Text = "да, выдача кредита возможно";
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
            lblBranch.Text = dbR.Branches.Where(r => r.ID == requests.BranchID).FirstOrDefault().Name;
            lblRequestN.Text = requests.RequestID.ToString();
            lblOffice.Text = dbRWZ.Offices.Where(o => o.ID == requests.OfficeID).FirstOrDefault().ShortName;

            if (f) { lblCreditRating.Text = "Положительный"; }
            else
            { lblCreditRating.Text = "Отрицательный"; };

            { lblStatus.Text = "Отказано"; lblDescription.Text = "Не хватает дохода"; }

            if ((f) && (requests.RequestStatus == "К Подписи"))
            { lblStatus.Text = "Утверждено"; lblDescription.Text = ""; lblDescription.Text = ""; }
            //  else { lblStatus.Text = "Отказано"; lblDescription.Text = "Не хватает дохода"; }

            if ((f) && (requests.RequestStatus == "На выдаче"))
            { lblStatus.Text = "Утверждено"; lblDescription.Text = ""; lblDescription.Text = ""; }
            //else { lblStatus.Text = "Отказано"; lblDescription.Text = "Не хватает дохода"; }

            if ((f) && (requests.RequestStatus == "Принято"))
            { lblStatus.Text = "Утверждено"; lblDescription.Text = ""; lblDescription.Text = ""; }
            //else { lblStatus.Text = "Отказано"; lblDescription.Text = "Не хватает дохода"; }

            if ((f) && (requests.RequestStatus == "Выдано"))
            { lblStatus.Text = "Утверждено"; lblDescription.Text = ""; lblDescription.Text = ""; }
            //else { lblStatus.Text = "Отказано"; lblDescription.Text = "Не хватает дохода"; }


            if ((f) && (requests.RequestStatus == "Утверждено"))
            { lblStatus.Text = "Утверждено"; lblDescription.Text = ""; lblDescription.Text = ""; }
            //else { lblStatus.Text = "Отказано"; lblDescription.Text = "Не хватает дохода"; }

            if ((f) && (requests.RequestStatus == "Подтверждено")) { lblStatus.Text = ""; lblDescription.Text = ""; lblDescription.Text = ""; lblCreditRating.Text = ""; }
            if ((!f) && (requests.RequestStatus == "Подтверждено")) { lblStatus.Text = ""; lblDescription.Text = ""; lblCreditRating.Text = ""; }

            if ((f) && (requests.RequestStatus == "Отменено")) { lblStatus.Text = ""; lblDescription.Text = ""; lblDescription.Text = ""; lblCreditRating.Text = ""; }
            if ((!f) && (requests.RequestStatus == "Отменено")) { lblStatus.Text = ""; lblDescription.Text = ""; lblCreditRating.Text = ""; }

            if ((f) && (requests.RequestStatus == "Отказано")) { lblStatus.Text = ""; lblDescription.Text = ""; lblDescription.Text = ""; lblCreditRating.Text = ""; }
            if ((!f) && (requests.RequestStatus == "Отказано")) { lblStatus.Text = ""; lblDescription.Text = ""; lblCreditRating.Text = ""; }


            if ((usrID == 6075) || //Соколов
                                   //(usrID == 6521) || //Жумашева
                (usrID == 6536) ||  //Молдогазиева
                (usrID == 6977) || //Сейдилда уулу
                (usrID == 6249) || //Сандиева
                (usrID == 7552) || //Гриценко
                (usrID == 4562) ||//Абдыкасымова
                (usrID == 7156) ||//Орозбаева
                (usrID == 4445)) //Халикова
            {
                //lblReqDecision.Text = "РЕШЕНИЕ УПОЛНОМОЧЕННОГО ЛИЦА ";
                //lblDecision1.Text = "Уполномоченное лицо";
                //lblDecision2.Text = "";
                //lblDecision3.Text = "";
                //lblDecision4.Text = "";
            }
            else
            {
                //lblReqDecision.Text = "РЕШЕНИЕ _______________ ";
                lblDecision1.Text = "Скоринг программа";
                //lblDecision2.Text = "Член Кредитного комитета";
                //lblDecision3.Text = "Член Кредитного комитета";
                //lblDecision4.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Секретарь КК&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ФИО";
            }
        }
    }
}