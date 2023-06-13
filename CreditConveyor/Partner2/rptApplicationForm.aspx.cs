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
    public partial class rptApplicationForm : System.Web.UI.Page
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);

        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        double chp, cho, y1, y2, zp, v, valp, OtherLoans;

        protected void Page_Load(object sender, EventArgs e)
        {
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int RequestID = Convert.ToInt32(Session["RequestID"].ToString());

            lblMinRevenue.Text = Session["RadNumTbMinRevenue"] as string;
            lblMaxRevenue.Text = Session["RadNumTbMaxRevenue"] as string;
            lblCountWorkDay.Text = Session["ddlCountWorkDay"] as string;
            lblСostPrice.Text = Session["RadNumTbСostPrice"] as string;
            lblOverhead.Text = Session["RadNumTbOverhead"] as string;
            lblFamilyExpenses.Text = Session["RadNumTbFamilyExpenses"] as string;

            //Double RadNumTbAverageMonthSalary = Convert.ToDouble(Session["RadNumTbAverageMonthSalary"].ToString());
            //Double RadNumTbRevenue = Convert.ToDouble(Session["RadNumTbRevenue"].ToString());
            //Double RadNumTbСostPrice = Convert.ToDouble(Session["RadNumTbСostPrice"].ToString());
            //Double RadNumTbOverhead = Convert.ToDouble(Session["RadNumTbOverhead"].ToString());
            //Double RadNumTbFamilyExpenses = Convert.ToDouble(Session["RadNumTbFamilyExpenses"].ToString());
            //Double RadNumOtherLoans = Convert.ToDouble(Session["RadNumOtherLoans"].ToString());


            CreditController creditCtrl = new CreditController();
            SysController sysCtrl = new SysController();
            var requestsUsersRoles = sysCtrl.RequestsUsersRoleGetItem(usrID);
            var users = sysCtrl.UsersGetItem(usrID);
            var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
            var request = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();

            double RadNumTbRevenue = Convert.ToDouble(request.Revenue);
            double additionalIncome = Convert.ToDouble(request.AdditionalIncome);
            lblCustomerFIO.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            
            lblCustomerRegAddress.Text = customers.RegistrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
            lblCustomerResAddress.Text = customers.ResidenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;
            lblContactPhone1.Text = customers.ContactPhone1;
            
            

            Decimal numtoword = 0;
            
            

            //lblOffice.Text = dbRWZ.Offices.Where(o => o.ID == request.OfficeID).FirstOrDefault().ShortName;

            
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
            

            if (request.OrgID == 27)
            {

                if ((request.RequestRate == 0) && (request.RequestPeriod == 3)) { commision = 3; NameOfCredit = "0-0-3"; }
                if ((request.RequestRate == 0) && (request.RequestPeriod == 6)) { commision = 3; NameOfCredit = "0-0-6"; }
                if ((request.RequestRate == 0) && (request.RequestPeriod == 12)) { commision = 2; NameOfCredit = "0-0-12"; }
            
            }

            /**/
            double s = Convert.ToDouble(request.RequestSumm);
            double n = Convert.ToDouble(request.RequestPeriod);
            //double stavka = Convert.ToDouble(requests.RequestRate);
            //double i = stavka / 12 / 100;
            //double k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);

            double i, k = 0;
            int y22 = 40;

            double stavka = Convert.ToDouble(request.RequestRate);
            i = (stavka != 0) ? stavka / 12 / 100 : 0;
            if (stavka == 0) k = s / n;
            //if ((stavka == 0) && (n == 10)) k = s / n + s / 100;
            //if ((stavka == 0) && (n == 15)) k = s / n + s / 100;
            //if ((stavka == 0) && (n == 24)) k = s / n + s / 100;
            if (stavka == 0) k = s / n;
            if ((stavka == 0) && (n == 15)) k = s / n + s / 100;
            if ((stavka == 0) && (n == 24)) k = s / n + s / 100;
            if (stavka != 0) k = (((Math.Pow((1 + i), n)) * (i)) * s) / ((Math.Pow((1 + (i)), n)) - 1);

            bool f = false;
            //bool zarplat = true;

            int? bussines = request.Bussiness;

           
            

           

           
           
          

        }
    }
}