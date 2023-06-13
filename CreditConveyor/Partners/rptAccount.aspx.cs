using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using СreditСonveyor.Data.Partners;

namespace СreditСonveyor.Partners
{
    public partial class rptAccount : System.Web.UI.Page
    {
        string WorkType;
        public static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        public static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        public static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["connectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
        //public string connectionString = @"Data Source=Database2.doscredobank.kg;Initial Catalog=DoscredoBank20170310;User ID=sa;Password=MartinOderskyScala11235813";
        //public string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";

        protected void Page_Load(object sender, EventArgs e)
        {
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            hfGuarID.Value = Session["GuarantorID"] as string;
            hfUserID.Value = Session["UserID"] as string;
            hfRequestID.Value = Session["RequestID"] as string;
            WorkType = Session["WorkType"] as string;

            SysController sysCtrl = new SysController();
            var customerItem = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
            lblCustomerFIO1.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;

            lblCustomerName.Text = customerItem.Surname;
            lblSurname.Text = customerItem.CustomerName;
            lblOtchestvo.Text = customerItem.Otchestvo;
            lblCustomerName2.Text = customerItem.Surname;
            lblSurname2.Text = customerItem.CustomerName;
            lblOtchestvo2.Text = customerItem.Otchestvo;

            lblINN.Text = customerItem.IdentificationNumber;
            
            lblSeriesOfPasp.Text = customerItem.DocumentSeries;
            lblNoOfPasp.Text = customerItem.DocumentNo;
            lblSeriesOfPasp2.Text = customerItem.DocumentSeries;
            lblNoOfPasp2.Text = customerItem.DocumentNo;
           
            lblIssueDate.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
            lblValidDate.Text = (Convert.ToDateTime(customerItem.DocumentValidTill)).Date.ToString("dd.MM.yyyy");
            lblIssueDate2.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
            lblValidDate2.Text = (Convert.ToDateTime(customerItem.DocumentValidTill)).Date.ToString("dd.MM.yyyy");


            lblIssueAuthority.Text = customerItem.IssueAuthority;
            lblIssueAuthority2.Text = customerItem.IssueAuthority;

            lblPhone.Text = customerItem.ContactPhone1;

            lblAddressRegistration.Text = customerItem.RegistrationCityName + " " + customerItem.RegistrationStreet + " " + customerItem.RegistrationHouse + " " + customerItem.RegistrationFlat;
            lblAddressResidence.Text = customerItem.RegistrationCityName + " " + customerItem.ResidenceStreet +" "+ customerItem.ResidenceHouse + " " + customerItem.ResidenceFlat;
            lblAddressRegistration2.Text = customerItem.RegistrationCityName + " " + customerItem.RegistrationStreet + " " + customerItem.RegistrationHouse + " " + customerItem.RegistrationFlat;
            lblAddressResidence2.Text = customerItem.RegistrationCityName + " " + customerItem.ResidenceStreet + " " + customerItem.ResidenceHouse + " " + customerItem.ResidenceFlat;


            lblBirthDate.Text = (Convert.ToDateTime(customerItem.DateOfBirth)).Date.ToString("dd.MM.yyyy");
            lblBirthDate2.Text = (Convert.ToDateTime(customerItem.DateOfBirth)).Date.ToString("dd.MM.yyyy");
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