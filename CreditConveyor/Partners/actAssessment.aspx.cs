using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat;
using СreditСonveyor.Data.Partners;

namespace СreditСonveyor.Partners
{
    public partial class actAssessment : System.Web.UI.Page
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
           
            CreditController creditCtrl = new CreditController();
            SysController sysCtrl = new SysController();
            var requestsUsersRoles = sysCtrl.RequestsUsersRoleGetItem(usrID);
            var users = sysCtrl.UsersGetItem(usrID);
            var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
            var request = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();

         

            if (request.isPledger == true)
            {
                lblPledgerFIO.Text = request.PledgerSurname + " " + request.PledgerName + " " + request.PledgerOtchestvo;
                lblPledgerINN.Text = request.PledgerIdentificationNumber;
                
            }


     

            decimal commision = 0; string NameOfCredit = "КапиталБанк";
            if (request.RequestSumm > 99999) NameOfCredit = "Потребительский";
            if ((request.RequestRate == 0) && (request.RequestPeriod == 3)) { commision = 0; NameOfCredit = "0-0-3"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 6)) { commision = 0; NameOfCredit = "0-0-6"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 9)) { commision = 0; NameOfCredit = "0-0-9"; }
            if ((request.RequestRate == 0) && (request.RequestPeriod == 12)) { commision = 0; NameOfCredit = "0-0-12"; }
            if (request.IsEmployer == true) commision = 0;
 
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