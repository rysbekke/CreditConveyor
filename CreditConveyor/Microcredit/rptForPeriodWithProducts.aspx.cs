﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamat;
using СreditСonveyor.Data.Microcredit;

namespace СreditСonveyor.Microcredit
{
    public partial class rptForPeriodWithProducts : System.Web.UI.Page
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["connectionStringZ"].ToString();
        dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
        dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
        dbdataDataContext dbR = new dbdataDataContext(connectionStringR);

        string tbDate1, tbDate2;
        bool i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, k0, k1, k2, g1, g2, g3, o0, o1, o2, o3, o4, o5, o6, o7, o8, o9;

        protected void btnExpToExcel_Click(object sender, EventArgs e)
        {
            //ExportGrid(DGrid, "ExportToExcel.xls");
            ExportGrid(DGridHist, "ExportToExcelWithProducts.xls");
            //ExportGrid2(gvForPeriodWithHistory);
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

        public DataGrid DGrid = new DataGrid();
        public DataGrid DGridHist = new DataGrid();
        public DataSet DSet = new DataSet("qwer");
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

        public void refreshGrid()
        {
            string dt1 = "", dt2 = "", surname = "", inn = "", name = "", nRequestID = "", creditID = "";

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
            int? groupID = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault().GroupID;

            int? agentRoleID = 8; //int? agentRoleID = db.RequestsUsersRoles.Where(r => (r.UserID == usrID)).FirstOrDefault().RoleID;
                                  //1-все заявки Ошка
                                  //3-Тур
                                  //6-все заявки Светофор
                                  //8-Билайн
            var rle = Convert.ToInt32(Session["RoleID"]);
            rle = 9;
            var users2 = dbRWZ.Users2s.Where(r => r.UserID == usrID).FirstOrDefault();
            int? orgID = dbRWZ.Groups.Where(g => g.GroupID == groupID).FirstOrDefault().OrgID;
            List<Request> lst;
            //if (rle == 5) { lst = ItemController.GetRequestsAllForExpert(usrID, agentRoleID, inn, surname, name, dt1, dt2, connectionString); } //Эксперты ГБ
            if (rle == 2) { lst = ItemController.GetRequestsForExpert(usrID, agentRoleID, nRequestID, inn, creditID, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, k0, k1, k2, g1, g2, 0, 10000, o0, o1, o2, o3, o4, o5, o6, o7, o8, o9); } //Эксперты филиала
            else if (rle == 9) { lst = ItemController.GetRequestsAllForAdmin(usrID, agentRoleID, orgID, nRequestID, inn, creditID, surname, name, dt1, dt2, connectionStringR, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, k0, k1, k2, g1, g2, 0, 10000, o0, o1, o2, o3, o4, o5, o6, o7, o8, o9); } //Админы O!
            else { lst = ItemController.GetRequestsForAgent(usrID, agentRoleID, nRequestID, inn, creditID, surname, name, dt1, dt2, connectionStringR, groupID, i0, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, k0, k1, k2, g1, g2, 0, 10000, o0, o1, o2, o3, o4, o5, o6, o7, o8, o9); } //Агенты






            if (lst.Count > 0)
            {
                var lst5 = (from r in lst
                            join b in dbRWZ.Branches on r.BranchID equals b.ID
                            select new
                            {
                                r.CreditID,
                                r.CustomerID,
                                r.RequestID,
                                r.OrgID,
                                r.GroupID,
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


                var lst55 = (from r in lst5
                             join c in dbR.Customers on r.CustomerID equals c.CustomerID
                             select new
                             {
                                 r.CreditID,
                                 r.RequestID,
                                 r.OrgID,
                                 r.GroupID,
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
                                 r.IsEmployer,
                                 c.ContactPhone1
                             }).ToList();

                var lst56 = (from r in lst55
                             join g in dbRWZ.Groups on r.GroupID equals g.GroupID
                             select new
                             {
                                 r.CreditID,
                                 r.RequestID,
                                 r.OrgID,
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
                                 r.IsEmployer,
                                 r.ContactPhone1,
                                 g.GroupName
                             }).ToList();


                var lst6 = (from r in lst56
                            join p in dbRWZ.RequestsProducts on r.RequestID equals p.RequestID
                            select new
                            {
                                r.CreditID,
                                r.OrgID,
                                //r.RequestID,
                                r.GroupName,
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
                                r.ContactPhone1,
                                /**/
                                p.RequestID,
                                p.ProductMark,
                                p.ProductImei,
                                p.ProductImei2,
                                p.ProductSerial,
                                p.Price,
                                p.TarifName,
                                p.PriceTarif,
                                p.Note,
                                p.PriceWithTarif
                                //cus.CustomerName
                            }).ToList();



                var lst7 = (from r in lst6
                            join h in dbR.Histories on r.CreditID equals h.CreditID
                            select new
                            {
                                //r.CreditID,
                                r.RequestID,
                                r.OrgID,
                                r.GroupName,
                                FilialDOSCredo = r.ShortName,
                                ClientName = r.Surname + ' ' + r.CustomerName + ' ' + r.Otchestvo,  //r.CustomerName,
                                Phone = r.ContactPhone1,
                                r.IdentificationNumber,
                                AgreementNo = h.AgreementNo == null ? "-" : h.AgreementNo,
                                ProductPrice = Convert.ToString(r.ProductPrice).Replace(".", ","),
                                AmountDownPayment = Convert.ToString(r.AmountDownPayment).Replace(".", ","),
                                RequestSumm = Convert.ToString(r.RequestSumm).Replace(".", ","),
                                RequestDate = (Convert.ToDateTime(r.RequestDate.ToString()).ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture)).Replace(".", "/"),
                                r.RequestStatus,
                                //r.RequestStatus,
                                /**/
                                //r.AgentID,
                                //r.BranchID,
                                //r.Otchestvo,
                                r.CreditPurpose,
                                r.RequestPeriod,
                                r.RequestRate,
                                //r.AgentFirstName,
                                //r.AgentLastName,
                                //r.AgentUsername,
                                //r.IsEmployer,
                                r.ContactPhone1,
                                /**/

                                r.ProductMark,
                                r.ProductImei,
                                r.ProductImei2,
                                r.ProductSerial,
                                r.Price,
                                //r.TarifName,
                                //r.PriceTarif,
                                r.Note,
                                r.PriceWithTarif
                            }).ToList();


                var lst75 = (from r in lst6
                             join h in dbR.Histories on r.CreditID equals h.CreditID
                             select new
                             {
                                 //r.CreditID,
                                 RequestID = r.RequestID.ToString(),
                                 OrgID = r.OrgID,
                                 Group = r.GroupName,
                                 FilialDOSCredo = r.ShortName,
                                 ClientName = r.Surname + ' ' + r.CustomerName + ' ' + r.Otchestvo,  //r.CustomerName,
                                 Phone = r.ContactPhone1,
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
                                 //r.AgentID,
                                 //r.BranchID,
                                 //r.Otchestvo,
                                 r.CreditPurpose,
                                 r.RequestPeriod,
                                 r.RequestRate,
                                 //r.AgentFirstName,
                                 //r.AgentLastName,
                                 //r.AgentUsername,
                                 //r.IsEmployer,
                                 /**/
                                 r.ProductMark,
                                 r.ProductImei,
                                 r.ProductImei2,
                                 r.ProductSerial,
                                 r.Price,
                                 //r.TarifName,
                                 //r.PriceTarif,
                                 r.Note,
                                 r.PriceWithTarif
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

        //public void ExportGrid2(GridView gvForPeriodWithHistory)
        //{
        //    // creating Excel Application  
        //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
        //    // creating new WorkBook within Excel application  
        //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
        //    // creating new Excelsheet in workbook  
        //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
        //    // see the excel sheet behind the program  
        //    app.Visible = true;
        //    // get the reference of first sheet. By default its name is Sheet1.  
        //    // store its reference to worksheet  
        //    worksheet = workbook.Sheets["Sheet1"];
        //    worksheet = workbook.ActiveSheet;
        //    // changing the name of active sheet  
        //    worksheet.Name = "Exported from gridview";
        //    // storing header part in Excel  
        //    for (int i = 1; i < gvForPeriodWithHistory.Columns.Count + 1; i++)
        //    {
        //        worksheet.Cells[1, i] = gvForPeriodWithHistory.Columns[i - 1].HeaderText;
        //    }
        //    // storing Each row and column value to excel sheet  
        //    for (int i = 0; i < gvForPeriodWithHistory.Rows.Count - 1; i++)
        //    {
        //        for (int j = 0; j < gvForPeriodWithHistory.Columns.Count; j++)
        //        {
        //            worksheet.Cells[i + 2, j + 1] = gvForPeriodWithHistory.Rows[i].Cells[j].ToString();
        //        }
        //    }
        //    // save the application  
        //    workbook.SaveAs("d:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    // Exit from the application  
        //    app.Quit();
        //}

        decimal _RequestSumm = 0, _ProductPrice = 0, _AmountDownPayment = 0, _Count = 0;
        List<Order> bookOrders = new List<Order>{
            new Order{OrderID=0},
        };
        private object b;

        public class Order
        {
            public int OrderID { get; set; }
        }




    }
}