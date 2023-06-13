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
    public partial class rptProfileNano : System.Web.UI.Page
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["connectionStringZ"].ToString();
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
        double chp, cho, y1, y2, zp, v, valp, OtherLoans;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                int RequestID = Convert.ToInt32(Session["RequestID"].ToString());

                SysController sysCtrl = new SysController();


                var requests = dbRWZ.Requests.Where(r => r.RequestID == RequestID).ToList().FirstOrDefault();
                int? CustomerID = requests.CustomerID;
                //var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
                var customers = dbR.Customers.Where(c => c.CustomerID == CustomerID).FirstOrDefault();
                lblCustomerFIO.Text = customers.Surname + " " + customers.CustomerName + " " + customers.Otchestvo;
                lblCustomerBirth.Text = (Convert.ToDateTime(customers.DateOfBirth)).Date.ToString("dd.MM.yyyy");
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




                if (customers.ContactPhone1.Length > 11)
                {
                    lblPhone0.Text = customers.ContactPhone1.Substring(0, 1);
                    lblPhone1.Text = customers.ContactPhone1.Substring(1, 1);
                    lblPhone2.Text = customers.ContactPhone1.Substring(2, 1);
                    lblPhone3.Text = customers.ContactPhone1.Substring(3, 1);

                    lblPhone4.Text = customers.ContactPhone1.Substring(4, 1);
                    lblPhone5.Text = customers.ContactPhone1.Substring(5, 1);
                    lblPhone6.Text = customers.ContactPhone1.Substring(6, 1);

                    lblPhone7.Text = customers.ContactPhone1.Substring(7, 1);
                    lblPhone8.Text = customers.ContactPhone1.Substring(8, 1);
                    lblPhone9.Text = customers.ContactPhone1.Substring(9, 1);
                    lblPhone10.Text = customers.ContactPhone1.Substring(10, 1);
                    lblPhone11.Text = customers.ContactPhone1.Substring(11, 1);

                }


                if (customers.ContactPhone1.Length > 12)
                {
                    lblPhone12.Text = customers.ContactPhone1.Substring(12, 1);
                }
                if (customers.ContactPhone1.Length > 13)
                {
                    lblPhone13.Text = customers.ContactPhone1.Substring(13, 1);
                }
                if (customers.ContactPhone1.Length > 14)
                {
                    lblPhone14.Text = customers.ContactPhone1.Substring(14, 1);
                }
                if (customers.ContactPhone1.Length > 15)
                {
                    lblPhone15.Text = customers.ContactPhone1.Substring(15, 1);
                }
                if (customers.ContactPhone1.Length > 16)
                {
                    lblPhone16.Text = customers.ContactPhone1.Substring(16, 1);
                }


                lblRequestSumm.Text = requests.RequestSumm.ToString();
                lblRequestPeriod.Text = requests.RequestPeriod.ToString();
                lblRequestSumm2.Text = requests.RequestSumm.ToString();
                lblRequestPeriod2.Text = requests.RequestPeriod.ToString();


                CreditController creditCtrl = new CreditController();
                NumByWords num2words = new NumByWords();
                var historyCustomerItem = creditCtrl.HistoriesCustomerGetItem(Convert.ToInt32(requests.CreditID));
                Decimal ApprovedSumm = Convert.ToDecimal(historyCustomerItem.ApprovedSumm);
                string SummWord = num2words.KgsPhrase(ApprovedSumm);
                lblRequestSummWord.Text = SummWord + " т.";



            }
            catch (Exception ex)
            {
                //  Block of code to handle errors
                //DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.ToString(), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

        }
    }
}