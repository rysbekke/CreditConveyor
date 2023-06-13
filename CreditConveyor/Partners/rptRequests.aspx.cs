using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using СreditСonveyor.Data.Partners;
 

namespace СreditСonveyor.Partners
{
    public partial class rptRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            CreditController creditCtrl = new CreditController();
        }
    }
}