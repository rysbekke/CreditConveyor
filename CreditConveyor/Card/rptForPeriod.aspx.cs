﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using СreditСonveyor.Data.Card;

namespace СreditСonveyor.Card
{
    public partial class rptForPeriod : System.Web.UI.Page
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();
        string tbDate1, tbDate2;
        bool i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, k0, k1;

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
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
            //i11 = Convert.ToBoolean(Session["i11"].ToString());
            //k0 = Convert.ToBoolean(Session["k0"].ToString());
            //k1 = Convert.ToBoolean(Session["k1"].ToString());
            refreshGrid();
        }
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Vithal" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvForPeriod.GridLines = GridLines.Both;
            gvForPeriod.HeaderStyle.Font.Bold = true;
            gvForPeriod.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }

        public DataGrid DGrid = new DataGrid();
        public DataGrid DGridHist = new DataGrid();
        public DataSet DSet = new DataSet("qwer");

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




        protected void btnExpToExcel_Click(object sender, EventArgs e)
        {
            ExportGrid(DGrid, "ExportToExcel.xls");
        }

        decimal _RequestSumm = 0, _ProductPrice = 0, _AmountDownPayment = 0, _Count = 0;
        List<Order> bookOrders = new List<Order>{
            new Order{OrderID=0},
        };
        public class Order
        {
            public int OrderID { get; set; }
        }

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


        public void refreshGrid()
        {
            string dt1 = "", dt2 = "", surname = "", inn = "", name = "", nRequestID = "";

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
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            int usrID = Convert.ToInt32(Session["UserID"].ToString());
            int? agentRoleID = 8; //int? agentRoleID = db.RequestsUsersRoles.Where(r => (r.UserID == usrID)).FirstOrDefault().RoleID;
                                  //1-Технодом
                                  //var rle = Convert.ToInt32(Session["RoleID"]);
            var rle = Convert.ToInt32(Session["RoleID"]);
            if (rle == 4) { agentRoleID = 1; }
            if (rle == 9) { agentRoleID = 8; }

            int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;
            int? orgID = dbRWZ.Groups.Where(g => g.GroupID == groupID).FirstOrDefault().OrgID;
            List<CardRequest> lst;
            if (rle == 20) { lst = ItemController.GetRequestsAllForExpert(usrID, agentRoleID, nRequestID, inn, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, 0, 10000); } //Эксперты ГБ
            else if (rle == 19) { lst = ItemController.GetRequestsForExpert(usrID, agentRoleID, nRequestID, inn, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, 0, 10000); } //Эксперты филиала
            else if ((rle == 4) || (rle == 9)) { lst = ItemController.GetRequestsAllForAdmin(usrID, agentRoleID, nRequestID, orgID, inn, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, 0, 10000); } //Админы O!
            else { lst = ItemController.GetRequestsForAgent(usrID, agentRoleID, nRequestID, inn, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, 0, 10000); } //Агенты

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
                                //r.ProductSerial,
                                //r.ProductMark,
                                //r.ProductImei,
                                //r.ProductDescription,
                                r.AgentFirstName,
                                r.AgentLastName,
                                r.AgentUsername,
                                //r.SumMonthSalary,
                                //r.CountMonthSalary,
                                r.IsEmployer
                            }).ToList();

                var lst6 = (from r in lst5
                            join h in dbR.Histories on r.CreditID equals h.CreditID
                            into a
                            from b in a.DefaultIfEmpty()
                            select new
                            {
                                r.RequestID,
                                r.ShortName,
                                r.Surname,
                                r.CustomerName,
                                r.IdentificationNumber,
                                AgreementNo = b == null ? "-" : b.AgreementNo,
                                r.ProductPrice,
                                r.AmountDownPayment,
                                r.RequestSumm,
                                r.RequestDate,
                                r.RequestStatus,
                                r.AgentUsername
                            }).ToList();


                gvForPeriod.DataSource = lst6;
                gvForPeriod.DataBind();



                var lst7 = (from r in lst5
                            join ur in dbRWZ.RequestsUsersRoles on r.AgentID equals ur.UserID
                            join h in dbR.Histories on r.CreditID equals h.CreditID
                            into a
                            from b in a.DefaultIfEmpty()
                            select new
                            {
                                r.RequestID,
                                FilialDOSCredo = r.ShortName,
                                ClientName = r.Surname + ' ' + r.CustomerName + ' ' + r.Otchestvo,
                                IdentificationNumber = "'" + r.IdentificationNumber.ToString() + "'",
                                AgreementNo = b == null ? "-" : b.AgreementNo,
                                ProductPrice = Convert.ToString(r.ProductPrice).Replace(".", ","),
                                AmountDownPayment = Convert.ToString(r.AmountDownPayment).Replace(".", ","),
                                RequestSumm = Convert.ToString(r.RequestSumm).Replace(".", ","),
                                RequestDate = (Convert.ToDateTime(r.RequestDate.ToString()).ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)).Replace(".", "/"),
                                r.RequestStatus,
                                /**/
                                //AgentID = r.AgentID.ToString(),
                                //BranchID = r.BranchID.ToString(),
                                r.CreditPurpose,
                                r.RequestPeriod,
                                r.RequestRate,
                                //r.ProductSerial,
                                //r.ProductMark,
                                //r.ProductImei,
                                //r.ProductDescription,
                                r.AgentUsername,
                                AgentName = r.AgentFirstName,
                                //r.SumMonthSalary,
                                //r.CountMonthSalary,
                                r.IsEmployer,
                                //OfficeAgent = ur.NameAgencyPoint,
                                //OfficeAddress = ur.AddressAgencyPoint,
                            }).ToList();


                DGrid.DataSource = lst7;
                DGrid.DataBind();

                /*reportwithHistory*/
                var lst75 = from users in dbRWZ.Users2s
                            join reqhist in dbRWZ.CardRequestsHistories on users.UserID equals reqhist.AgentID
                            select new
                            {
                                reqhist.ID,
                                reqhist.AgentID,
                                reqhist.CreditID,
                                reqhist.CustomerID,
                                reqhist.Status,
                                reqhist.StatusDate,
                                reqhist.note,
                                reqhist.RequestID,
                                users.UserName
                            };


                var lst8 = (from r in lst5
                            join reqhist in lst75 on r.RequestID equals reqhist.RequestID
                            join ur in dbRWZ.RequestsUsersRoles on r.AgentID equals ur.UserID
                            join h in dbR.Histories on r.CreditID equals h.CreditID
                            into a
                            from b in a.DefaultIfEmpty()
                            select new
                            {
                                RequestID = r.RequestID.ToString(),
                                FilialDOSCredo = r.ShortName,
                                ClientName = r.Surname + ' ' + r.CustomerName + ' ' + r.Otchestvo,
                                IdentificationNumber = "'" + r.IdentificationNumber.ToString() + "'",
                                AgreementNo = b == null ? "-" : b.AgreementNo,
                                ProductPrice = Convert.ToString(r.ProductPrice).Replace(".", ","),
                                AmountDownPayment = Convert.ToString(r.AmountDownPayment).Replace(".", ","),
                                RequestSumm = Convert.ToString(r.RequestSumm).Replace(".", ","),
                                r.RequestStatus,
                                RequestDate = (Convert.ToDateTime(r.RequestDate.ToString()).ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)).Replace(".", "/"),
                                /**/
                                //AgentID = r.AgentID.ToString(),
                                //BranchID = r.BranchID.ToString(),
                                r.CreditPurpose,
                                r.RequestPeriod,
                                r.RequestRate,
                                //r.ProductSerial,
                                //r.ProductMark,
                                //r.ProductImei,
                                //r.ProductDescription,
                                r.AgentUsername,
                                AgentName = r.AgentFirstName,
                                //r.SumMonthSalary,
                                //r.CountMonthSalary,
                                r.IsEmployer,
                                //OfficeAgent = ur.NameAgencyPoint,
                                //OfficeAddress = ur.AddressAgencyPoint,
                                reqhist.UserName,
                                reqhist.Status,
                                DateStatus = (Convert.ToDateTime(reqhist.StatusDate.ToString()).ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)).Replace(".", "/"),
                                Note = reqhist.note,
                            }).ToList();


                DGridHist.DataSource = lst8;
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

                // decimal total = lst5.AsEnumerable().Sum(r => r.RequestSumm.Value);
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