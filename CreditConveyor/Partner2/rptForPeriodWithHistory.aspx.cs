using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat;
using СreditСonveyor.Data.Partners2;

namespace СreditСonveyor.Partners2
{
    public partial class rptForPeriodWithHistory : System.Web.UI.Page
    {

        //string connectionString = ConfigurationManager.ConnectionStrings["DosCredobankConnectionString"].ToString();
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);


        string tbDate1, tbDate2;
        bool i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, k0, k1, k2, g1, g2, g3, o0, o1, o2, o3, o4, o5, o6, o7, o8, o9;

        protected void gvForPeriod_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal RequestSumm = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RequestSumm"));
                _RequestSumm += RequestSumm;
                decimal ProductPrice = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ProductPrice"));
                _ProductPrice += ProductPrice;
                decimal AmountDownPayment = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "AmountDownPayment"));
                _AmountDownPayment += AmountDownPayment;
                _Count = _Count + 1;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblRequestSumm = (Label)e.Row.FindControl("lblRequestSumm");
                lblRequestSumm.Text = _RequestSumm.ToString();
                Label lblProductPrice = (Label)e.Row.FindControl("lblProductPrice");
                lblProductPrice.Text = _ProductPrice.ToString();
                Label lblAmountDownPayment = (Label)e.Row.FindControl("lblAmountDownPayment");
                lblAmountDownPayment.Text = _AmountDownPayment.ToString();
                Label lblShortName = (Label)e.Row.FindControl("lblShortName");
                lblShortName.Text = "Кол-во: ";
                Label lblSurname = (Label)e.Row.FindControl("lblSurname");
                lblSurname.Text = _Count.ToString();
                Label lblAgreementNo = (Label)e.Row.FindControl("lblAgreementNo");
                lblAgreementNo.Text = "Сумма:";
            }
        }

        public DataGrid DGrid = new DataGrid();
        public DataGrid DGridHist = new DataGrid();
        public DataSet DSet = new DataSet("qwer");

        decimal _RequestSumm = 0, _ProductPrice = 0, _AmountDownPayment = 0, _Count = 0;
        List<Order> bookOrders = new List<Order>{
            new Order{OrderID=0},
        };
        private object b;

        public class Order
        {
            public int OrderID { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            hfCustomerID.Value = Session["CustomerID"] as string;
            hfCreditID.Value = Session["CreditID"] as string;
            tbDate1 = Session["tbDate1"].ToString();
            tbDate2 = Session["tbDate2"].ToString();
            lbldt1.Text = tbDate1;
            lbldt2.Text = tbDate2;
            i0 = Convert.ToBoolean(Session["i0"].ToString());
            i1 = Convert.ToBoolean(Session["i1"].ToString());
            i2 = Convert.ToBoolean(Session["i2"].ToString());
            i3 = Convert.ToBoolean(Session["i3"].ToString());
            i4 = Convert.ToBoolean(Session["i4"].ToString());
            i5 = Convert.ToBoolean(Session["i5"].ToString());
            i6 = Convert.ToBoolean(Session["i6"].ToString());
            i7 = Convert.ToBoolean(Session["i7"].ToString());
            i8 = Convert.ToBoolean(Session["i8"].ToString());
            i9 = Convert.ToBoolean(Session["i9"].ToString());
            i10 = Convert.ToBoolean(Session["i10"].ToString());
            i11 = Convert.ToBoolean(Session["i11"].ToString());
            i12 = Convert.ToBoolean(Session["i12"].ToString());
            k0 = Convert.ToBoolean(Session["k0"].ToString());
            k1 = Convert.ToBoolean(Session["k1"].ToString());
            k2 = Convert.ToBoolean(Session["k2"].ToString());
            g1 = Convert.ToBoolean(Session["g1"].ToString());
            g2 = Convert.ToBoolean(Session["g2"].ToString());
            g3 = Convert.ToBoolean(Session["g3"].ToString());
            o0 = Convert.ToBoolean(Session["o0"].ToString());
            o1 = Convert.ToBoolean(Session["o1"].ToString());
            o2 = Convert.ToBoolean(Session["o2"].ToString());
            o3 = Convert.ToBoolean(Session["o3"].ToString());
            o4 = Convert.ToBoolean(Session["o4"].ToString());
            o5 = Convert.ToBoolean(Session["o5"].ToString());
            o6 = Convert.ToBoolean(Session["o6"].ToString());
            o7 = Convert.ToBoolean(Session["o7"].ToString());
            o8 = Convert.ToBoolean(Session["o8"].ToString());
            i9 = Convert.ToBoolean(Session["o9"].ToString());
            refreshGrid();
        }

        private static void ClearControls(Control control)
        {
            //Recursively loop through the controls, calling this method
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }

            //If we have a control that is anything other than a table cell
            if (!(control is TableCell))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                    }
                    catch
                    {
                    }
                    control.Parent.Controls.Remove(control);
                }
                else
                    if (control.GetType().GetProperty("Text") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                    control.Parent.Controls.Remove(control);
                }
            }
            return;
        }



        public static void ExportGrid(DataGrid oGrid, string exportFile)
        {
            //Clear the response, and set the content type and mark as attachment
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + exportFile + "\"");
            //HttpContext.Current.Response.Charset = "windows-1254";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());


            //Clear the character set
            //HttpContext.Current.Response.Charset = "";

            //Create a string and Html writer needed for output
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            //System.IO.TextWriter oStringWriter = new System.IO.TextWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            //Clear the controls from the pased grid
            //ClearControls(oGrid);

            //Show grid lines
            oGrid.GridLines = GridLines.Both;

            //Color header
            oGrid.HeaderStyle.BackColor = System.Drawing.Color.LightGray;

            //Render the grid to the writer
            oGrid.RenderControl(oHtmlTextWriter);

            //Write out the response (file), then end the response
            HttpContext.Current.Response.Write(oStringWriter.ToString());
            HttpContext.Current.Response.End();
        }

        public void refreshGrid()
        {
            string dt1 = "", dt2 = "", surname = "", inn = "", name = "", nRequestID = "", creditID="";

            //if (tbSearchRequestINN.Text != "") { inn = tbSearchRequestINN.Text; }
            //if (tbSearchRequestSurname.Text != "") { surname = tbSearchRequestSurname.Text; }
            //if (tbSearchRequestName.Text != "") { name = tbSearchRequestName.Text; }

            if (tbDate1 != "")
            {
                // if (tbDate2.Text == "") { tbDate2.Text = tbDate1.Text; }
                dt1 = tbDate1.Substring(6, 4) + "." + tbDate1.Substring(3, 2) + "." + tbDate1.Substring(0, 2);
                //dt1 = tbDate1.Text.Substring(0, 4) + "." + tbDate1.Text.Substring(5, 2) + "." + tbDate1.Text.Substring(8, 2);
                //dt1 = tbDate1.Text;
                //dt2 = tbDate2.Text.Substring(6, 4) + "." + tbDate2.Text.Substring(3, 2) + "." + tbDate2.Text.Substring(0, 2);
            }
            if (tbDate2 != "")
            {
                dt2 = tbDate2.Substring(6, 4) + "." + tbDate2.Substring(3, 2) + "." + tbDate2.Substring(0, 2);
            }

            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int? agentRoleID = 8; //int? agentRoleID = db.RequestsUsersRoles.Where(r => (r.UserID == usrID)).FirstOrDefault().RoleID;
                                  //1-Технодом
                                  //var rle = Convert.ToInt32(Session["RoleID"]);
            var rle = 9;
            List<Request> lst;
            var users2 = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault();
            int? orgID = users2.OrgID;
            //if (rle == 5) { lst = ItemController.GetRequestsAllForExpert(usrID, agentRoleID, inn, surname, name, dt1, dt2, connectionString); } //Эксперты ГБ
            if (rle == 2) { lst = ItemController.GetRequestsForExpert(usrID, agentRoleID, nRequestID, inn, creditID, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, k0, k1, k2, g1, g2, g3, 0, 10000, o0, o1, o2, o3, o4, o5, o6, o7, o8, o9); } //Эксперты филиала
            else if (rle == 9) { lst = ItemController.GetRequestsAllForAdmin(usrID, agentRoleID, orgID, nRequestID, inn, creditID, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, k0, k1, k2, g1, g2, g3, 0, 10000, o0, o1, o2, o3, o4, o5, o6, o7, o8, o9); } //Админы O!
            else { lst = ItemController.GetRequestsForAgent(usrID, agentRoleID, nRequestID, inn, creditID, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, k0, k1, k2, g1, g2, g3, 0, 10000, o0, o1, o2, o3, o4, o5, o6, o7, o8, o9); } //Агенты

            if (lst.Count > 0)
            {
                var lst5 = (from r in lst
                            join b in dbRWZ.Branches on r.BranchID equals b.ID
                            select new
                            {
                                r.CreditID,
                                r.RequestID,
                                b.ShortName,
                                r.Surname,
                                r.CustomerName,
                                r.IdentificationNumber,
                                r.ProductPrice,
                                r.AmountDownPayment,
                                r.RequestSumm,
                                r.RequestDate,
                                r.RequestStatus,
                                /**/
                                r.AgentID,
                                r.BranchID,
                                r.Otchestvo,
                                r.CreditPurpose,
                                r.RequestPeriod,
                                r.RequestRate,
                                r.AgentFirstName,
                                r.AgentLastName,
                                r.AgentUsername,
                                //r.SumMonthSalary,
                                //r.CountMonthSalary,
                                r.IsEmployer
                            }).ToList();

                var lst6 = (from r in lst5
                            join h in dbRWZ.RequestsHistories on r.CreditID equals h.CreditID
                            select new
                            {
                                r.CreditID,
                                r.RequestID,
                                r.ShortName,
                                r.Surname,
                                r.CustomerName,
                                r.IdentificationNumber,
                                r.ProductPrice,
                                r.AmountDownPayment,
                                r.RequestSumm,
                                r.RequestDate,
                                r.RequestStatus,
                                /**/
                                //r.AgentID,
                                r.BranchID,
                                r.Otchestvo,
                                r.CreditPurpose,
                                r.RequestPeriod,
                                r.RequestRate,
                                //r.AgentFirstName,
                                //r.AgentLastName,
                                //r.AgentUsername,
                                r.IsEmployer,
                                /**/
                                h.note,
                                h.Status,
                                h.StatusDate,
                                h.CustomerID,
                                h.AgentID,
                                //cus.CustomerName
                            }).ToList();



                var lst7 = (from r in lst6
                            join users in dbRWZ.Users2s on r.AgentID equals users.UserID
                            join h in dbR.Histories on r.CreditID equals h.CreditID
                            select new
                            {
                                //r.CreditID,
                                r.RequestID,
                                FilialDOSCredo = r.ShortName,
                                ClientName = r.Surname + ' ' + r.CustomerName + ' ' + r.Otchestvo,  //r.CustomerName,
                                r.IdentificationNumber,
                                AgreementNo = h.AgreementNo == null ? "-" : h.AgreementNo,
                                ProductPrice = Convert.ToString(r.ProductPrice).Replace(".", ","),
                                AmountDownPayment = Convert.ToString(r.AmountDownPayment).Replace(".", ","),
                                RequestSumm = Convert.ToString(r.RequestSumm).Replace(".", ","),
                                RequestDate = (Convert.ToDateTime(r.RequestDate.ToString()).ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)).Replace(".", "/"),
                                r.RequestStatus,
                                //r.RequestStatus,
                                /**/
                                r.AgentID,
                                r.BranchID,
                                //r.Otchestvo,
                                r.CreditPurpose,
                                r.RequestPeriod,
                                r.RequestRate,
                                //r.AgentFirstName,
                                //r.AgentLastName,
                                //r.AgentUsername,
                                r.IsEmployer,
                                /**/
                                r.note,
                                r.Status,
                                StatusDate = (Convert.ToDateTime(r.StatusDate.ToString()).ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)).Replace(".", "/"), //r.StatusDate,
                                //r.CustomerID,
                                users.UserName,
                                FullUserName = users.Fullname
                            }).ToList();


                var lst75 = (from r in lst6
                             join users in dbRWZ.Users2s on r.AgentID equals users.UserID
                             join h in dbR.Histories on r.CreditID equals h.CreditID
                             select new
                             {
                                 r.CreditID,
                                 RequestID = r.RequestID.ToString(),
                                 FilialDOSCredo = r.ShortName,
                                 ClientName = r.Surname + ' ' + r.CustomerName + ' ' + r.Otchestvo,  //r.CustomerName,
                                 IdentificationNumber = "'" + Convert.ToInt64(r.IdentificationNumber).ToString() + "'",
                                 AgreementNo = h.AgreementNo == null ? "-" : h.AgreementNo,
                                 //AgreementNo = b == null ? "-" : b.AgreementNo,
                                 ProductPrice = Convert.ToString(r.ProductPrice).Replace(".", ","),
                                 AmountDownPayment = Convert.ToString(r.AmountDownPayment).Replace(".", ","),
                                 RequestSumm = Convert.ToString(r.RequestSumm).Replace(".", ","),
                                 RequestDate = (Convert.ToDateTime(r.RequestDate.ToString()).ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)).Replace(".", "/"),
                                 r.RequestStatus,
                                 //r.RequestStatus,
                                 /**/
                                 r.AgentID,
                                 r.BranchID,
                                 //r.Otchestvo,
                                 r.CreditPurpose,
                                 r.RequestPeriod,
                                 r.RequestRate,
                                 //r.AgentFirstName,
                                 //r.AgentLastName,
                                 //r.AgentUsername,
                                 r.IsEmployer,
                                 /**/
                                 r.note,
                                 r.Status,
                                 StatusDate = (Convert.ToDateTime(r.StatusDate.ToString()).ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)).Replace(".", "/"), //r.StatusDate,
                                                                                                                                                                        //r.CustomerID,
                                 users.UserName,
                                 FullUserName = users.Fullname

                             }).ToList();


                gvForPeriodWithHistory.DataSource = lst7;
                gvForPeriodWithHistory.DataBind();

                DGridHist.DataSource = lst75;
                DGridHist.DataBind();
                /**/

                //this.DGrid.Columns("UnitPrice").DefaultCellStyle.Format = "c";
                //DGrid.Columns("ShipDate").DefaultCellStyle.Format = "d";
                //DGrid.Columns[4].st   .DefaultCellStyle.Format = "dd/MM/yyyy";
                //DataGridCellStyle style = new DataGridViewCellStyle();
                //DataGridColumnStyle style = new DataGridColumn();
                //data
                //style.Format = "N2";
                //this.DGrid.Columns[0].DefaultCellStyle = style;

                // Cells[7].Style.Add("mso-number-format", "\\#\\,\\#\\#0\\.00");

                decimal total = lst5.AsEnumerable().Sum(r => r.RequestSumm.Value);
                //gvForPeriod.FooterRow.Cells[1].Text = "Total";
                //gvForPeriod.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                //gvForPeriod.FooterRow.Cells[2].Text = total.ToString("N2");

            }
            else
            {
                gvForPeriod.DataSource = new string[] { };
                gvForPeriod.DataBind();
            }


        }
    }
}