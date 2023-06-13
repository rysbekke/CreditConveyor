using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace СreditСonveyor.Data.Card
{
    public class ItemController
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();

        public static List<CardRequest> GetRequestsForAgent(int userID, int? agentRoleID, string nRequest, string inn, string surname, string name, string date1, string date2, string connectionString, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, int pageIndex, int pageSize)
        {
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            //DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            var lst0 = ((nRequest != "") || (inn != "") || (surname != "") || (name != "")) ? (from v in dbRWZ.CardRequests select v).ToList() : (from v in dbRWZ.CardRequests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            var lst1 = (inn == "") ? (from v in lst0 where (v.AgentID == userID) select v).ToList() : (from v in lst0 where (v.AgentID == userID) && (v.IdentificationNumber == inn) select v).ToList();
            var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();
            var lst6 = (i0 == false) ? (from v in lst4 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst4;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправлено") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Утверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Выдано") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Принято") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Отменено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отказано") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Активировано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "Исправить") select v).ToList() : lst15;
            if (lst16.Count > 0)
            {
                foreach (var ech in lst16)
                {
                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst16.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;
                    if ((lst16.IndexOf(ech) >= l1) && (lst16.IndexOf(ech) <= l2))
                    {
                        if ((ech.RequestStatus != "Выдано") && (ech.RequestStatus != "На выдаче") && (ech.RequestStatus != "Отменено") && (ech.RequestStatus != "Отказано") && (ech.RequestStatus != "Принято"))
                        {
                            int f = 0;
                            CreditController ctx = new CreditController();
                            string usernameAgent = ech.AgentUsername;
                            string fullnameAgent = ech.AgentFirstName;
                            string fullnameCustomer = ech.Surname + " " + ech.CustomerName + " " + ech.Otchestvo;
                            int reqID = ech.RequestID;
                            var historystatus = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                            //foreach (var hstat in historystatus)
                            if (historystatus.Count > 0)
                            {
                                var hstat = historystatus.Last();
                                var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault();
                                var usrs2 = dbRWZ.Users2s.Where(u => u.UserName == usrs.UserName).ToList().FirstOrDefault();
                                {
                                    if (hstat.StatusID == 3) { f = 3; }
                                    if (hstat.StatusID == 4) { f = 4; }
                                }
                                if (f == 3)
                                {
                                    var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "На выдаче";
                                    dbRWZ.CardRequests.Context.SubmitChanges();

                                    /*RequestHistory*/
                                    CardRequestsHistory newItem = new CardRequestsHistory()
                                    {
                                        AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                        CreditID = hstat.CreditID,
                                        // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                        StatusDate = hstat.StatusDate,
                                        Status = "На выдаче",
                                        note = "",
                                        RequestID = ech.RequestID
                                    };
                                    ctx.ItemRequestHistoriesAddItem(newItem);
                                    //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                }
                                if (f == 4)
                                {
                                    var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "Отказано";
                                    dbRWZ.CardRequests.Context.SubmitChanges();
                                    /*причина отказа*/
                                    string RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                    /*RequestHistory*/
                                    CardRequestsHistory newItem = new CardRequestsHistory()
                                    {
                                        AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                        CreditID = hstat.CreditID,
                                        // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                        StatusDate = hstat.StatusDate,
                                        Status = "Отказано",
                                        note = RejectReason,
                                        RequestID = ech.RequestID
                                    };
                                    ctx.ItemRequestHistoriesAddItem(newItem);
                                    //AgentView.SendMailTo2("Отказано", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); //агентам и админам
                                }
                            }
                        }
                    }
                }
            }
            return lst16;

        }

        public static List<CardRequest> GetRequestsAllForAdmin(int userID, int? agentRoleID, string nRequest, int? orgID, string inn, string surname, string name, string date1, string date2, string connectionString, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, int pageIndex, int pageSize)
        {
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);

            var lst0 = ((nRequest != "") || (inn != "") || (surname != "") || (name != "")) ? (from v in dbRWZ.CardRequests select v).ToList() : (from v in dbRWZ.CardRequests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            var lst1 = (inn == "") ? (from v in lst0 where ((v.AgentRoleID == agentRoleID) && (v.OrgID == orgID)) select v).ToList() : (from v in lst0 where ((v.IdentificationNumber == inn) && ((v.AgentRoleID == agentRoleID) && (v.OrgID == orgID))) select v).ToList();

            var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();
            var lst6 = (i0 == false) ? (from v in lst4 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst4;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправлено") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Утверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Выдано") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Принято") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Отменено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отказано") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Активировано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "Исправить") select v).ToList() : lst15;
            //var lst17 = (i11 == false) ? (from v in lst16 where (v.RequestStatus != "К подписи") select v).ToList() : lst16;
            if (lst16.Count > 0)
            {
                foreach (var ech in lst16)
                {
                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst16.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;
                    if ((lst16.IndexOf(ech) >= l1) && (lst16.IndexOf(ech) <= l2))
                    {
                        //if (ech.RequestStatus != "Выдан")
                        if ((ech.RequestStatus != "Выдано") && (ech.RequestStatus != "На выдаче") && (ech.RequestStatus != "Отменено") && (ech.RequestStatus != "Отказано") && (ech.RequestStatus != "Принято"))
                        {
                            int f = 0;
                            CreditController ctx = new CreditController();
                            string usernameAgent = ech.AgentUsername;
                            string fullnameAgent = ech.AgentFirstName;
                            string fullnameCustomer = ech.Surname + " " + ech.CustomerName + " " + ech.Otchestvo;
                            int reqID = ech.RequestID;
                            var historystatus = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                            //foreach (var hstat in historystatus)
                            if (historystatus.Count > 0)
                            {
                                var hstat = historystatus.Last();
                                var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault();
                                var usrs2 = dbRWZ.Users2s.Where(u => u.UserName == usrs.UserName).ToList().FirstOrDefault();
                                {
                                    if (hstat.StatusID == 3) { f = 3; }
                                    if (hstat.StatusID == 4) { f = 4; }
                                }
                                if (f == 3)
                                {
                                    var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "На выдаче";
                                    dbRWZ.CardRequests.Context.SubmitChanges();


                                    /*RequestHistory*/
                                    CardRequestsHistory newItem = new CardRequestsHistory()
                                    {
                                        AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                        CreditID = hstat.CreditID,
                                        // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                        StatusDate = hstat.OperationDate,
                                        Status = "На выдаче",
                                        note = "",
                                        RequestID = ech.RequestID
                                    };
                                    ctx.ItemRequestHistoriesAddItem(newItem);
                                    //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                }
                                if (f == 4)
                                {
                                    var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "Отказано";
                                    dbRWZ.CardRequests.Context.SubmitChanges();
                                    /*причина отказа*/
                                    string RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                    /*RequestHistory*/
                                    CardRequestsHistory newItem = new CardRequestsHistory()
                                    {
                                        AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                        CreditID = hstat.CreditID,
                                        // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                        StatusDate = hstat.OperationDate,
                                        Status = "Отказано",
                                        note = RejectReason,
                                        RequestID = ech.RequestID
                                    };
                                    ctx.ItemRequestHistoriesAddItem(newItem);
                                    //AgentView.SendMailTo2("Отказано", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); //агентам и админам
                                }

                            }
                        }
                    }
                }
            }
            return lst16;

        }

        public static List<CardRequest> GetRequestsAllForExpert(int userID, int? agentRoleID, string nRequest, string inn, string surname, string name, string date1, string date2, string connectionString, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, int pageIndex, int pageSize)
        {
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst0 = ((nRequest != "") || (inn != "") || (surname != "") || (name != "")) ? (from v in dbRWZ.CardRequests select v).ToList() : (from v in dbRWZ.CardRequests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            var lst1 = (inn == "") ? (from v in lst0 where ((v.AgentRoleID == 1) || (v.AgentRoleID == 8) || (v.AgentRoleID == 23) || (v.AgentRoleID == 19) || (v.AgentRoleID == 2)) select v).ToList() : (from v in lst0 where ((v.IdentificationNumber == inn) && ((v.AgentRoleID == 1) || (v.AgentRoleID == 8) || (v.AgentRoleID == 23) || (v.AgentRoleID == 19) || (v.AgentRoleID == 2))) select v).ToList();
            var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();

            var lst6 = (i0 == false) ? (from v in lst4 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst4;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправлено") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Утверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Выдано") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Принято") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Отменено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отказано") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Активировано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "Исправить") select v).ToList() : lst15;
            //var lst17 = (i11 == false) ? (from v in lst16 where (v.RequestStatus != "К подписи") select v).ToList() : lst16;

            if (lst16.Count > 0)
            {
                foreach (var ech in lst16)
                {
                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst16.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;
                    if ((lst16.IndexOf(ech) >= l1) && (lst16.IndexOf(ech) <= l2))
                    {
                        //if (ech.RequestStatus != "Выдан")
                        if ((ech.RequestStatus != "Выдано") && (ech.RequestStatus != "На выдаче") && (ech.RequestStatus != "Отменено") && (ech.RequestStatus != "Отказано") && (ech.RequestStatus != "Принято"))
                        {
                            int f = 0;
                            CreditController ctx = new CreditController();
                            string usernameAgent = ech.AgentUsername;
                            string fullnameAgent = ech.AgentFirstName;
                            string fullnameCustomer = ech.Surname + " " + ech.CustomerName + " " + ech.Otchestvo;
                            int reqID = ech.RequestID;
                            var historystatus = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                            //foreach (var hstat in historystatus)
                            if (historystatus.Count > 0)
                            {
                                var hstat = historystatus.Last();
                                var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault();
                                var usrs2 = dbRWZ.Users2s.Where(u => u.UserName == usrs.UserName).ToList().FirstOrDefault();
                                {
                                    if (hstat.StatusID == 3) { f = 3; }
                                    if (hstat.StatusID == 4) { f = 4; }
                                }
                                if (f == 3)
                                {
                                    var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "На выдаче";
                                    dbRWZ.CardRequests.Context.SubmitChanges();


                                    /*RequestHistory*/
                                    CardRequestsHistory newItem = new CardRequestsHistory()
                                    {
                                        AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                        CreditID = hstat.CreditID,
                                        // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                        StatusDate = hstat.OperationDate,
                                        Status = "На выдаче",
                                        note = "",
                                        RequestID = ech.RequestID
                                    };
                                    ctx.ItemRequestHistoriesAddItem(newItem);
                                    //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                }
                                if (f == 4)
                                {
                                    var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "Отказано";
                                    dbRWZ.CardRequests.Context.SubmitChanges();
                                    /*причина отказа*/
                                    string RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                    /*RequestHistory*/
                                    CardRequestsHistory newItem = new CardRequestsHistory()
                                    {
                                        AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                        CreditID = hstat.CreditID,
                                        // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                        StatusDate = hstat.OperationDate,
                                        Status = "Отказано",
                                        note = RejectReason,
                                        RequestID = ech.RequestID
                                    };
                                    ctx.ItemRequestHistoriesAddItem(newItem);
                                    //AgentView.SendMailTo2("Отказано", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); //агентам и админам
                                }

                            }
                        }
                    }
                }
            }
            return lst16;

        }


        public static List<CardRequest> GetRequestsForExpert(int userID, int? agentRoleID, string nRequest, string inn, string surname, string name, string date1, string date2, string connectionString, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, int pageIndex, int pageSize)
        {

            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            int branchID = 5;//времмено 
            int officeID = dbRWZ.Users2s.Where(r => r.UserID == userID).FirstOrDefault().OfficeID;
            branchID = dbR.Offices.Where(r => r.ID == officeID).FirstOrDefault().BranchID;
            var lst0 = ((nRequest != "") || (inn != "") || (surname != "") || (name != "")) ? (from v in dbRWZ.CardRequests select v).ToList() : (from v in dbRWZ.CardRequests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            var lst1 = (inn == "") ? (from v in lst0 where ((v.BranchID == branchID)) select v).ToList() : (from v in lst0 where ((v.IdentificationNumber == inn) && (v.BranchID == branchID)) select v).ToList();
            var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();
            var lst6 = (i0 == false) ? (from v in lst4 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst4;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправлено") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Утверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Выдано") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Принято") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Отменено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отказано") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Активировано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "Исправить") select v).ToList() : lst15;

            if (lst16.Count > 0)
            {
                foreach (var ech in lst16)
                {
                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst16.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;
                    if ((lst16.IndexOf(ech) >= l1) && (lst16.IndexOf(ech) <= l2))
                    {
                        //if (ech.RequestStatus != "Выдан")
                        if ((ech.RequestStatus != "Выдано") && (ech.RequestStatus != "На выдаче") && (ech.RequestStatus != "Отменено") && (ech.RequestStatus != "Отказано") && (ech.RequestStatus != "Принято"))
                        {
                            int f = 0;
                            //string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
                            CreditController ctx = new CreditController();
                            string usernameAgent = ech.AgentUsername;
                            string fullnameAgent = ech.AgentFirstName;
                            string fullnameCustomer = ech.Surname + " " + ech.CustomerName + " " + ech.Otchestvo;

                            int reqID = ech.RequestID;
                            var historystatus = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                            //foreach (var hstat in historystatus)
                            if (historystatus.Count > 0)
                            {
                                var hstat = historystatus.Last();
                                var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault();
                                var usrs2 = dbRWZ.Users2s.Where(u => u.UserName == usrs.UserName).ToList().FirstOrDefault();
                                {
                                    if (hstat.StatusID == 3) { f = 3; }
                                    if (hstat.StatusID == 4) { f = 4; }
                                }
                                if (f == 3)
                                {
                                    var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "На выдаче";
                                    dbRWZ.CardRequests.Context.SubmitChanges();


                                    /*RequestHistory*/
                                    CardRequestsHistory newItem = new CardRequestsHistory()
                                    {
                                        AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                        CreditID = hstat.CreditID,
                                        // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                        StatusDate = hstat.OperationDate,
                                        Status = "На выдаче",
                                        note = "",
                                        RequestID = ech.RequestID
                                    };
                                    ctx.ItemRequestHistoriesAddItem(newItem);
                                    //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                }
                                if (f == 4)
                                {
                                    var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "Отказано";
                                    dbRWZ.CardRequests.Context.SubmitChanges();
                                    /*причина отказа*/
                                    string RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                    /*RequestHistory*/
                                    CardRequestsHistory newItem = new CardRequestsHistory()
                                    {
                                        AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                        CreditID = hstat.CreditID,
                                        // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                        StatusDate = hstat.OperationDate,
                                        Status = "Отказано",
                                        note = RejectReason,
                                        RequestID = ech.RequestID
                                    };
                                    ctx.ItemRequestHistoriesAddItem(newItem);
                                    //AgentView.SendMailTo2("Отказано", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); //агентам и админам
                                }

                            }
                        }
                    }
                }
            }
            return lst16;

        }




        public static List<CardRequest> GetRequestsAllForExpert(int userID, int? agentRoleID, string nRequest, string inn, string surname, string name, string date1, string date2, string connectionString)
        {

            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst0 = (from v in dbRWZ.CardRequests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            var lst1 = (inn == "") ? (from v in lst0 select v).ToList() : (from v in lst0 where (v.IdentificationNumber == inn) select v).ToList();
            var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();
            if (lst4.Count > 0)
            {
                foreach (var ech in lst4)
                {
                    //if (ech.RequestStatus != "Выдан")
                    if ((ech.RequestStatus != "Выдано") && (ech.RequestStatus != "На выдаче") && (ech.RequestStatus != "Отменено") && (ech.RequestStatus != "Отказано") && (ech.RequestStatus != "Принято"))
                    {
                        int f = 0;
                        //string connectionString = @"Data Source=DESKTOP-QJB2L76\MSSQLSERVER2012;Initial Catalog=DosCredobank;User ID=sa;Password=Server2012";
                        CreditController ctx = new CreditController();
                        string usernameAgent = ech.AgentUsername;
                        string fullnameAgent = ech.AgentFirstName;
                        string fullnameCustomer = ech.Surname + " " + ech.CustomerName + " " + ech.Otchestvo;
                        int reqID = ech.RequestID;
                        var historystatus = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                        //foreach (var hstat in historystatus)
                        if (historystatus.Count > 0)
                        {
                            var hstat = historystatus.Last();
                            var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault();
                            var usrs2 = dbRWZ.Users2s.Where(u => u.UserName == usrs.UserName).ToList().FirstOrDefault();
                            {
                                if (hstat.StatusID == 3) { f = 3; }
                                if (hstat.StatusID == 4) { f = 4; }
                            }
                            if (f == 3)
                            {
                                var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                reqs.RequestStatus = "На выдаче";
                                dbRWZ.CardRequests.Context.SubmitChanges();


                                /*RequestHistory*/
                                CardRequestsHistory newItem = new CardRequestsHistory()
                                {
                                    AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                    CreditID = hstat.CreditID,
                                    // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                    StatusDate = hstat.OperationDate,
                                    Status = "На выдаче",
                                    note = "",
                                    RequestID = ech.RequestID
                                };
                                ctx.ItemRequestHistoriesAddItem(newItem);
                                //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                            }
                            if (f == 4)
                            {
                                var reqs = dbRWZ.CardRequests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                reqs.RequestStatus = "Отказано";
                                dbRWZ.CardRequests.Context.SubmitChanges();
                                /*причина отказа*/
                                string RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                /*RequestHistory*/
                                CardRequestsHistory newItem = new CardRequestsHistory()
                                {
                                    AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                    CreditID = hstat.CreditID,
                                    // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                    StatusDate = hstat.OperationDate,
                                    Status = "Отказано",
                                    note = RejectReason,
                                    RequestID = ech.RequestID
                                };
                                ctx.ItemRequestHistoriesAddItem(newItem);
                                //AgentView.SendMailTo2("Отказано", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); //агентам и админам
                            }

                        }
                    }
                }
            }
            return lst4;

        }



        public static List<CardRequest> GetRequests(int userID)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = (from v in dbRWZ.CardRequests where ((v.AgentID == userID)) select v).ToList();
            return lst;
        }

        public int ItemRequestAddItem(CardRequest newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.CardRequests.InsertOnSubmit(newItem);
            dbRWZ.CardRequests.Context.SubmitChanges();
            return (newItem.RequestID);
        }
        public void ItemRequestFilesAddItem(CardRequestsFile newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.CardRequestsFiles.InsertOnSubmit(newItem);
            dbRWZ.CardRequestsFiles.Context.SubmitChanges();
        }
        public void ItemRequestProductAddItem(RequestsProduct newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.RequestsProducts.InsertOnSubmit(newItem);
            dbRWZ.RequestsProducts.Context.SubmitChanges();
        }

        public void ItemProductAddItem(Product newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.Products.InsertOnSubmit(newItem);
            dbRWZ.Products.Context.SubmitChanges();
        }

        public void DeleteProduct(int itemid)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.RequestsProducts.DeleteAllOnSubmit(from v in dbRWZ.RequestsProducts where (v.ProductID == itemid) select v);
            dbRWZ.RequestsProducts.Context.SubmitChanges();
        }

        public void DeleteRequestsFiles(int itemid)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.CardRequestsFiles.DeleteAllOnSubmit(from v in dbRWZ.CardRequestsFiles where (v.ID == itemid) select v);
            dbRWZ.CardRequestsFiles.Context.SubmitChanges();
        }

        public CardRequest GetRequestByCreditID(int itemid)
        {
            CardRequest itm = new CardRequest();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            itm = dbRWZ.CardRequests.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }

        public CardRequest GetRequestByRequestID(int reqid)
        {
            CardRequest itm = new CardRequest();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            itm = dbRWZ.CardRequests.Where(g => g.RequestID == reqid).FirstOrDefault();
            return itm;
        }


        public void RequestUpd(CardRequest item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = dbRWZ.CardRequests.Where(r => r.RequestID == item.RequestID).FirstOrDefault();
            lst.Surname = item.Surname;
            lst.CustomerName = item.CustomerName;
            lst.Otchestvo = item.Otchestvo;
            lst.IdentificationNumber = item.IdentificationNumber;
            lst.RequestDate = item.RequestDate;
            lst.AgentID = item.AgentID;
            lst.AgentUsername = item.AgentUsername;
            lst.AgentFirstName = item.AgentFirstName;
            lst.AgentLastName = item.AgentLastName;
            lst.CustomerID = item.CustomerID;
            lst.RequestStatus = item.RequestStatus;
            lst.CardN = item.CardN;
            lst.AccountN = item.AccountN;
            lst.StatusDate = item.StatusDate;
            dbRWZ.CardRequests.Context.SubmitChanges();
        }

        public void AgentRequestUpd(CardRequest item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = dbRWZ.CardRequests.Where(r => r.RequestID == item.RequestID).FirstOrDefault();
            lst.Surname = item.Surname;
            lst.CustomerName = item.CustomerName;
            lst.Otchestvo = item.Otchestvo;
            lst.IdentificationNumber = item.IdentificationNumber;
            lst.RequestDate = item.RequestDate;
            lst.AgentID = item.AgentID;
            lst.AgentUsername = item.AgentUsername;
            lst.AgentFirstName = item.AgentFirstName;
            lst.AgentLastName = item.AgentLastName;
            lst.CustomerID = item.CustomerID;
            lst.RequestStatus = item.RequestStatus;
            lst.CardN = item.CardN;
            //lst.AccountN = item.AccountN;
            lst.StatusDate = item.StatusDate;
            dbRWZ.CardRequests.Context.SubmitChanges();
        }
        public void ExpertRequestUpd(CardRequest item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var lst = dbRWZ.CardRequests.Where(r => r.RequestID == item.RequestID).FirstOrDefault();
            lst.Surname = item.Surname;
            lst.CustomerName = item.CustomerName;
            lst.Otchestvo = item.Otchestvo;
            lst.IdentificationNumber = item.IdentificationNumber;
            lst.RequestDate = item.RequestDate;
            lst.AgentID = item.AgentID;
            lst.AgentUsername = item.AgentUsername;
            lst.AgentFirstName = item.AgentFirstName;
            lst.AgentLastName = item.AgentLastName;
            lst.CustomerID = item.CustomerID;
            lst.RequestStatus = item.RequestStatus;
            //lst.CardN = item.CardN;
            lst.AccountN = item.AccountN;
            dbRWZ.CardRequests.Context.SubmitChanges();
        }

        public RequestsProduct GetRequestsProductById(int itemid)
        {
            RequestsProduct itm = new RequestsProduct();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            itm = dbRWZ.RequestsProducts.Where(g => g.ProductID == itemid).FirstOrDefault();
            return itm;
        }

        public void RequestsProductsUpd(RequestsProduct item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            var ctx = dbRWZ.RequestsProducts.Where(r => r.ProductID == item.ProductID).FirstOrDefault();
            ctx.ProductMark = item.ProductMark;
            ctx.ProductSerial = item.ProductSerial;
            ctx.ProductImei = item.ProductImei;
            ctx.Price = item.Price;
            // ctx.TarifName = item.TarifName;
            // ctx.PriceTarif = item.PriceTarif;
            ctx.PriceWithTarif = item.PriceWithTarif;
            ctx.Note = item.Note;
            dbRWZ.RequestsProducts.Context.SubmitChanges();
        }

        /*******/
        public void ItemProductsDelAddItem(RequestsProductsDel newItem)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            dbRWZ.RequestsProductsDels.InsertOnSubmit(newItem);
            dbRWZ.RequestsProductsDels.Context.SubmitChanges();
        }


        /***********************************************Получить роль пользователя******************/
        public static int getRoleIDfromOB(string username)
        {
            //var psw = password;
            //var sha1 = System.Security.Cryptography.SHA1.Create(psw);
            //string sha1 = tosha(password);
            //string sha1 = SHA1HashStringForUTF8String(password);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringRWZ);
            var usr = (from u in dbR.Users where (u.UserName == username) select u).ToList();
            if (usr.Count > 0)
            {
                var roles = dbR.UsersRoles.Where(ur => ur.UserID == usr.FirstOrDefault().UserID).ToList();
                int userRoleID = -1;
                foreach (var rle in roles)
                {
                    //int userRoleID = dbR.UsersRoles.Where(ur => ur.UserID == usr.FirstOrDefault().UserID).FirstOrDefault().RoleID;
                    if (rle.RoleID == 1229) userRoleID = 1229;
                    if (rle.RoleID == 1230) userRoleID = 1230;
                    if (userRoleID != 1230)
                    {
                        if (rle.RoleID == 1262) userRoleID = 1262;
                    }
                    if (rle.RoleID == 1310) userRoleID = 1310;
                }
                return userRoleID;
            }


            else return -1;
        }
        /**********************************************************************/

        public static int getRoleIDfromСreditСonveyor(string username)
        {
            //var psw = password;
            //var sha1 = System.Security.Cryptography.SHA1.Create(psw);
            //string sha1 = tosha(password);
            //string sha1 = SHA1HashStringForUTF8String(password);
            dbdataDataContext dbZ = new dbdataDataContext(connectionStringRWZ);
            var usr = (from u in dbZ.Users2s where (u.UserName == username) select u).ToList();
            if (usr.Count > 0)
            {
                var roles = dbZ.RequestsUsersRoles.Where(ur => ur.UserID == usr.FirstOrDefault().UserID).ToList();
                int userRoleID = -1;
                userRoleID = Convert.ToInt32(roles.FirstOrDefault().RoleID);
                return userRoleID;
            }


            else return -1;
        }
        /*************************************************************************/






    }




}