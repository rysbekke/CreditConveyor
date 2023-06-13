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
    public partial class rptProffer : System.Web.UI.Page
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        double chp, cho, y1, y2, zp, v, valp, OtherLoans;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int RequestID = Convert.ToInt32(Session["RequestID"].ToString());

            CreditController creditCtrl = new CreditController();
            SysController sysCtrl = new SysController();
            var requestsUsersRoles = sysCtrl.RequestsUsersRoleGetItem(usrID);
            var user = sysCtrl.UsersGetItem(usrID);
            var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
            var request = dbRWZ.CardRequests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();

            double RadNumTbRevenue = Convert.ToDouble(request.Revenue);
            double additionalIncome = Convert.ToDouble(request.AdditionalIncome);
            lblCustomerSurname.Text = customers.Surname;
            lblCustomerName.Text = customers.CustomerName;
            lblOtchestvo.Text = customers.Otchestvo;

            lblFIO2.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;

            lblFIO1.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;

            lblFIO6.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;

            lblFioUser.Text = user.Fullname;

            //FIOAgent.Text = users.Fullname;
            lblDateOfBirth.Text = (Convert.ToDateTime(customers.DateOfBirth)).Date.ToString("dd.MM.yyyy");
            lblResidenceCityName.Text = customers.BirthCityName;
            //lblResidenceCountry.Text
            lblContactPhone.Text = customers.ContactPhone1;
            lblDocumentSeries.Text = customers.DocumentSeries + customers.DocumentNo;
            lblIssueAuthority.Text = customers.IssueAuthority;
            lblINN.Text = customers.IdentificationNumber;
            lblIssueDate.Text = (Convert.ToDateTime(customers.IssueDate)).Date.ToString("dd.MM.yyyy");
            lblValidTill.Text = (Convert.ToDateTime(customers.DocumentValidTill)).Date.ToString("dd.MM.yyyy");

            //lblCardN.Text = request.CardN.ToString();
            string RegistrationCityName = "";
            string ResidenceCityName = "";
            try
            {
                 RegistrationCityName = dbR.Cities.Where(r => r.CityID == customers.RegistrationCityID).FirstOrDefault().CityName;
                 ResidenceCityName = dbR.Cities.Where(r => r.CityID == customers.ResidenceCityID).FirstOrDefault().CityName;
            }
            catch (Exception ex)
            {
                MsgBox("В базе Банка не указан город по прописке и по проживанию, обратитесь специалисту Банка" + ex.ToString(), this.Page, this);
            }

            lblCustomerRegAddress.Text = RegistrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;

            lblCustomerResAddress.Text = ResidenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;
            lblCustomerResAddress2.Text = ResidenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;
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