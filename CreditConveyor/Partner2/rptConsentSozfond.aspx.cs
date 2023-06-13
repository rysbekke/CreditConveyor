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
    public partial class rptConsentSozfond : System.Web.UI.Page
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        double chp, cho, y1, y2, zp, v, valp, OtherLoans;
        protected void Page_Load(object sender, EventArgs e)
        {
            int RequestID = Convert.ToInt32(Session["RequestID"].ToString());

            SysController sysCtrl = new SysController();

            var requests = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();
            int? CustomerID = requests.CustomerID;
            var customers = dbR.Customers.Where(c => c.CustomerID == CustomerID).FirstOrDefault();
            lblCustomerFIO.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblCustomerFIO2.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
            lblContactPhone.Text = customers.ContactPhone1;
            //lblCustomerBirth.Text = (Convert.ToDateTime(customers.DateOfBirth)).Date.ToString("dd.MM.yyyy");
            lblINN.Text = customers.IdentificationNumber;
            lblDocumentSeries.Text = customers.DocumentSeries;
            lblDocumentNo.Text = customers.DocumentNo;
            lblIssueAuthority.Text = customers.IssueAuthority;
            lblIssueDate.Text = (Convert.ToDateTime(customers.IssueDate)).Date.ToString("dd.MM.yyyy");
            //var customerItem = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
            string regCustomerCountry = (customers.RegistrationCountryID != null) ? dbR.Countries.Where(v => v.CountryID == customers.RegistrationCountryID).ToList().FirstOrDefault().ShortName : "";
            string regCustomerCity = (customers.RegistrationCityID != null) ? dbR.Cities.Where(v => v.CityID == customers.RegistrationCityID).ToList().FirstOrDefault().CityName : "";
            string regCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customers.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
            string regCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customers.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
            string regCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customers.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
            lblRegCustomerAddress.Text = regCustomerCountry + " " + regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;

            string resCustomerCountry = (customers.ResidenceCountryID != null) ? dbR.Countries.Where(v => v.CountryID == customers.ResidenceCountryID).ToList().FirstOrDefault().ShortName : "";
            string resCustomerCity = (customers.ResidenceCityID != null) ? dbR.Cities.Where(v => v.CityID == customers.ResidenceCityID).ToList().FirstOrDefault().CityName : "";
            string resCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customers.CustomerID).ToList().FirstOrDefault().ResidenceStreet;
            string resCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customers.CustomerID).ToList().FirstOrDefault().ResidenceHouse;
            string resCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customers.CustomerID).ToList().FirstOrDefault().ResidenceFlat;
            lblResCustomerAddress.Text = resCustomerCountry + " " + resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;

            lblDate.Text = System.DateTime.Now.ToString("dd.MM.yyyy");
        }
    }
}