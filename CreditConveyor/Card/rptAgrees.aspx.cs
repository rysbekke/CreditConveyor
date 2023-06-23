using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat;
using СreditСonveyor.Data.Card;

namespace СreditСonveyor.Card
{
    public partial class rptAgrees : System.Web.UI.Page
    {
        string WorkType;
        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();


        protected void Page_Load(object sender, EventArgs e)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            hfGuarID.Value = Session["GuarantorID"] as string;
            hfUserID.Value = Session["UserID"] as string;
            hfRequestID.Value = Session["RequestID"] as string;
            WorkType = Session["WorkType"] as string;

            // int GuarantorID = Convert.ToInt32(Session["GuarantorID"] as string);






            /************************************************/




            SysController sysCtrl = new SysController();
            var customerItem = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));
            lblCustomerFIO.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
            lblCustomerFIO2.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
            lblCustomerFIO3.Text = customerItem.Surname + " " + customerItem.CustomerName + " " + customerItem.Otchestvo;
            lblDocumentSeries.Text = customerItem.DocumentSeries;
            lblDocumentSeries2.Text = customerItem.DocumentSeries;
            lblDocumentNo.Text = customerItem.DocumentNo;
            lblDocumentNo2.Text = customerItem.DocumentNo;
            lblIssueAuthority.Text = customerItem.IssueAuthority;
            lblIssueAuthority2.Text = customerItem.IssueAuthority;
            lblIssueDate.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");           //customerItem.IssueDate.ToString(); 
            lblIssueDate2.Text = (Convert.ToDateTime(customerItem.IssueDate)).Date.ToString("dd.MM.yyyy");
            lblIdentificationNumber.Text = customerItem.IdentificationNumber;
            lblINN.Text = customerItem.IdentificationNumber;



            int RequestID = Convert.ToInt32(Session["RequestID"].ToString());


            string productName = "", productCode = "", nameOfPercent = "Комиссия";

            int? usrID = dbRWZ.CardRequests.Where(r => r.RequestID == Convert.ToInt32(Session["RequestID"].ToString())).FirstOrDefault().AgentID;
            int? usrRoleID = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().RoleID;
            var roles = dbRWZ.RequestsRoles.Where(r => r.RoleID == usrRoleID).FirstOrDefault();

            DateTime docDate2 = Convert.ToDateTime(dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().AttorneyDocDate);
            string docDate = docDate2.ToString("dd.MM.yyyy");
            string docN = dbRWZ.RequestsUsersRoles.Where(r => r.UserID == usrID).FirstOrDefault().AttorneyDocName;


            int? ManagerID = Convert.ToInt32(Session["UserID"].ToString());
            var mngrItem = dbR.Customers.Where(r => r.CustomerID == ManagerID).FirstOrDefault();

            lblManagerDocNum.Text = docN;
            lblManagerDocNum3.Text = docN;
            lblManagerDocDate.Text = docDate.ToString();
            lblManagerDocDate3.Text = docDate.ToString();


            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            int usrID2 = Convert.ToInt32(Session["UserID"].ToString());
            var requestsUsersRoles = sysCtrl.RequestsUsersRoleGetItem(usrID2);
            var users = sysCtrl.UsersGetItem(usrID2);
            var customers = sysCtrl.CustomerGetItem(Convert.ToInt32(hfCustomerID.Value));

            lblManagerFIO.Text = users.Fullname;
            lblManagerFIO2.Text = users.Fullname;
            //lblRegCustomerAddress.Text = customers.RegistrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
            //lblRegCustomerAddress2.Text = customers.RegistrationCityName + " " + customers.RegistrationStreet + " " + customers.RegistrationHouse + " " + customers.RegistrationFlat;
            //lblResCustomerAddress.Text = customers.ResidenceCityName + " " + customers.ResidenceStreet + " " + customers.ResidenceHouse + " " + customers.ResidenceFlat;


            string regCustomerCity = (customerItem.RegistrationCityID != null) ? dbR.Cities.Where(v => v.CityID == customerItem.RegistrationCityID).ToList().FirstOrDefault().CityName : "";
            string regCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
            string regCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
            string regCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
            lblRegCustomerAddress.Text = regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;
            lblRegCustomerAddress2.Text = regCustomerCity + " " + regCustomerStreet + " " + regCustomerHouse + " " + regCustomerFlat;

            string resCustomerCity = (customerItem.ResidenceCityID != null) ? dbR.Cities.Where(v => v.CityID == customerItem.ResidenceCityID).ToList().FirstOrDefault().CityName : "";
            string resCustomerStreet = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceStreet;
            string resCustomerHouse = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceHouse;
            string resCustomerFlat = dbR.Customers.Where(v => v.CustomerID == customerItem.CustomerID).ToList().FirstOrDefault().ResidenceFlat;

            lblResCustomerAddress.Text = resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;
            lblResCustomerAddress2.Text = resCustomerCity + " " + resCustomerStreet + " " + resCustomerHouse + " " + resCustomerFlat;




            CreditController creditCtrl = new CreditController();
            //var historyItem = creditCtrl.HistoriesGetItem(Convert.ToInt32(hfCreditID.Value));

            // Office officeItem = sysCtrl.OficcesGetItem(historyItem.OfficeID);
            Office officeItem = sysCtrl.OficcesGetItem(users.OfficeID);
            Branch branchItem = sysCtrl.BranchGetItem(officeItem.BranchID);

            City cityItem = sysCtrl.CityGetItem(branchItem.CityID);
            var branchCustCustomersID = dbR.BranchesCustomers.Where(v => v.BranchID == branchItem.ID).FirstOrDefault().CustomerID;
            Customer companyItem = sysCtrl.CustomerGetItem(branchCustCustomersID);
            string regCompanyCity = dbR.Cities.Where(v => v.CityID == companyItem.RegistrationCityID).ToList().FirstOrDefault().CityName;
            string regCompanyStreet = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationStreet;
            string regCompanyHouse = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationHouse;
            string regCompanyFlat = dbR.Customers.Where(v => v.CustomerID == companyItem.CustomerID).ToList().FirstOrDefault().RegistrationFlat;
            lblCompanyAddress.Text = regCompanyCity + " " + regCompanyStreet + " " + regCompanyHouse + " " + regCompanyFlat;
            lblCompanyINN.Text = companyItem.IdentificationNumber;
            lblCompanyOKPO.Text = companyItem.OKPO;
            lblCity.Text = cityItem.CityName;
            lblAgreementDate.Text = (Convert.ToDateTime(DateTime.Now)).Date.ToString("dd.MM.yyyy");
        }
    }
}