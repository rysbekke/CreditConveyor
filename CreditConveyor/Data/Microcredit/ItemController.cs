using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Zamat;

namespace СreditСonveyor.Data.Microcredit
{

    public class ItemController
    {

        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();

        //public static List<Request> GetRequestsForAgent(int userID, int? agentRoleID, string nRequest, string inn, string surname, string name, string date1, string date2, string connectionString, int? groupID, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, bool i11, bool i12, bool k0, bool k1, bool k2)
        public static List<Request> GetRequestsForAgent(int userID, int? agentRoleID, string nRequest, string inn, string creditID, string surname, string name, string date1, string date2, string connectionString, int? groupID, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, bool i11, bool i12, bool k0, bool k1, bool k2, bool g1, bool g2, int pageIndex, int pageSize, bool o0, bool o1, bool o2, bool o3, bool o4, bool o5, bool o6, bool o7, bool o8, bool o9)
        {
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            int orgID = Convert.ToInt32(dbRWZ.Users2s.Where(r => r.UserID == userID).ToList().FirstOrDefault().OrgID);

            List<Request> lst0 = new List<Request>();
            if ((nRequest != "") || (inn != "") || (creditID != "") || (surname != "") || (name != ""))
            {
                if (inn != "") lst0 = (from v in dbRWZ.Requests where (v.IdentificationNumber == inn) select v).ToList();
                if (nRequest != "") lst0 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
                if (surname != "") lst0 = (from v in dbRWZ.Requests where (v.Surname == surname) select v).ToList();
                if (name != "") lst0 = (from v in dbRWZ.Requests where (v.CustomerName == name) select v).ToList();
                if (inn != "") lst0 = (from v in dbRWZ.Requests where (v.IdentificationNumber == inn) select v).ToList();
                if (creditID != "") lst0 = (from v in dbRWZ.Requests where (v.CreditID == Convert.ToInt32(creditID)) select v).ToList();
                //lst0 = (from v in lst0 where ((v.AgentRoleID == agentRoleID) && (v.BranchID == branchID)) select v).ToList();
            }
            //else lst0 = (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            //else lst0 = (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == Convert.ToDateTime(date1) || (v.RequestDate.Value.Date > Convert.ToDateTime(date1)) && (v.RequestDate.Value.Date < Convert.ToDateTime(date2)) || (v.RequestDate.Value.Date == Convert.ToDateTime(date2)))) select v).ToList();
            else lst0 = (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == Convert.ToDateTime(date1))) select v).ToList();
            //lst0 = (orgID == 7) ? (from v in lst0 where ((v.AgentID == userID) || (v.AgentID == 6950) || (v.GroupID == 110)) select v).ToList() : (from v in lst0 where ((v.AgentID == userID) || (v.AgentID == 6994)) select v).ToList();
            lst0 = (from v in lst0 where (v.AgentID == userID) select v).ToList();



            //var lst0 = ((nRequest != "") || (inn != "") || (creditID != "") || (surname != "") || (name != "")) ? (from v in dbRWZ.Requests select v).ToList() : (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            //var lst1 = (inn == "") ? (from v in lst0 select v).ToList() : (from v in lst0 where (v.IdentificationNumber == inn) select v).ToList();
            //var lst1_2 = (orgID == 7) ? (from v in lst1 where ((v.AgentID == userID) || (v.AgentID == 6950) || (v.GroupID == 110)) select v).ToList() : (from v in lst1 where ((v.AgentID == userID) || (v.AgentID == 6994)) select v).ToList();
            //var lst1_5 = (nRequest == "") ? lst1_2 : (from v in lst1_2 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            //var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            //var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            //var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();
            //var lst5 = (creditID == "") ? lst4 : (from v in lst4 where (v.CreditID == Convert.ToInt32(creditID)) select v).ToList();
            //var lst6 = (i0 == false) ? (from v in lst5 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst5;
            var lst6 = (i0 == false) ? (from v in lst0 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst0;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправить") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Исправлено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Утверждено") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Выдано") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Принято") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отменено") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Отказано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "В обработке") select v).ToList() : lst15;
            var lst17 = (i11 == false) ? (from v in lst16 where (v.RequestStatus != "На выдаче") select v).ToList() : lst16;
            var lst18 = (i12 == false) ? (from v in lst17 where (v.RequestStatus != "К подписи") select v).ToList() : lst17;

            var lst19 = (k0 == false) ? (from v in lst18 where (v.Bussiness != 0) select v).ToList() : lst18;
            var lst20 = (k1 == false) ? (from v in lst19 where (v.Bussiness != 1) select v).ToList() : lst19;
            var lst21 = (k2 == false) ? (from v in lst20 where (v.Bussiness != 2) select v).ToList() : lst20;

            //var lst22 = (g1 == false) ? (from v in lst21 where (v.OrgID != 17) select v).ToList() : lst21;  //beeshop
            var lst23 = (g1 == false) ? (from v in lst21 where ((v.OrgID != 3) && (v.OrgID != 7)) select v).ToList() : lst21;  //Билайн
            var lst24 = (g2 == false) ? (from v in lst23 where (v.GroupID != 110) select v).ToList() : lst23; //нано

            var lst26 = (o0 == false) ? (from v in lst24 where (v.BranchID != 1027) select v).ToList() : lst24; //ЦФ
            var lst27 = (o1 == false) ? (from v in lst26 where (v.OfficeID != 1082) select v).ToList() : lst26; //Берекет
            var lst28 = (o2 == false) ? (from v in lst27 where ((v.OfficeID != 1134) && (v.OfficeID != 1052)) select v).ToList() : lst27; //Карабалта
            var lst29 = (o3 == false) ? (from v in lst28 where ((v.OfficeID != 1133) && (v.OfficeID != 1120)) select v).ToList() : lst28; //Талас
            var lst30 = (o4 == false) ? (from v in lst29 where (v.BranchID != 1016) select v).ToList() : lst29; //Балыкчы
            var lst31 = (o5 == false) ? (from v in lst30 where (v.BranchID != 1017) select v).ToList() : lst30; //Ош
            var lst32 = (o6 == false) ? (from v in lst31 where (v.BranchID != 1018) select v).ToList() : lst31; //Нарын
            var lst33 = (o7 == false) ? (from v in lst32 where (v.BranchID != 1019) select v).ToList() : lst32; //Токмок
            var lst34 = (o8 == false) ? (from v in lst33 where (v.BranchID != 1020) select v).ToList() : lst33; //Каракол
            var lst35 = (o9 == false) ? (from v in lst34 where (v.BranchID != 1023) select v).ToList() : lst34; //Жалалабад

            if (lst35.Count > 0)
            {
                foreach (var ech in lst35)
                {
                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst35.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;

                    if ((lst35.IndexOf(ech) >= l1) && (lst35.IndexOf(ech) <= l2))
                    if ((ech.StatusOB != "Выдан") && (ech.StatusOB != "Отказано"))
                    {
                        if (ech.GroupID != 110)
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
                                    //var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault(); //ОБ
                                    var usrs2 = dbRWZ.Users2s.Where(u => u.UserIDOB == hstat.UserID).ToList().FirstOrDefault(); //скоринг
                                    {
                                        if (hstat.StatusID == 3) { f = 3; }
                                        if (hstat.StatusID == 4) { f = 4; }
                                    }
                                    if (f == 3)
                                    {
                                        //Request edititem = new Request();
                                        //edititem = ;
                                        //edititem.portalid = PortalId;

                                        //ech.RequestStatus = "На выдаче";
                                        //db.Requests.Context.SubmitChanges();
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        if (reqs.RequestStatus == "К подписи")
                                        {
                                            reqs.RequestStatus = "На выдаче";
                                            dbRWZ.Requests.Context.SubmitChanges();




                                            /*RequestHistory*/
                                            RequestsHistory newItem = new RequestsHistory()
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
                                        }
                                        //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                    }
                                    if (f == 4)
                                    {
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        reqs.RequestStatus = "Отказано";
                                        dbRWZ.Requests.Context.SubmitChanges();

                                        /*причина отказа*/
                                        string RejectReason = "";
                                        try
                                        {
                                            RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                        }
                                        catch
                                        {
                                            RejectReason = "";
                                        }
                                        finally
                                        {

                                        }
                                        /*RequestHistory*/
                                        RequestsHistory newItem = new RequestsHistory()
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
            }
            return lst35;

        }

        public static List<Request> GetRequestsAllForAdmin(int userID, int? agentRoleID, int? orgID, string nRequest, string inn, string creditID, string surname, string name, string date1, string date2, string connectionString, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, bool i11, bool i12, bool k0, bool k1, bool k2, bool g1, bool g2, int pageIndex, int pageSize, bool o0, bool o1, bool o2, bool o3, bool o4, bool o5, bool o6, bool o7, bool o8, bool o9)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);

            List<Request> lst0 = new List<Request>();
            if ((nRequest != "") || (inn != "") || (creditID != "") || (surname != "") || (name != ""))
            {
                if (inn != "") lst0 = (from v in dbRWZ.Requests where (v.IdentificationNumber == inn) select v).ToList();
                if (nRequest != "") lst0 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
                if (surname != "") lst0 = (from v in dbRWZ.Requests where (v.Surname == surname) select v).ToList();
                if (name != "") lst0 = (from v in dbRWZ.Requests where (v.CustomerName == name) select v).ToList();
                if (inn != "") lst0 = (from v in dbRWZ.Requests where (v.IdentificationNumber == inn) select v).ToList();
                if (creditID != "") lst0 = (from v in dbRWZ.Requests where (v.CreditID == Convert.ToInt32(creditID)) select v).ToList();
            }
            else lst0 = (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            lst0 = (from v in lst0 where (v.AgentRoleID == agentRoleID) select v).ToList();

            //var lst0 = ((nRequest != "") || (inn != "") || (creditID != "") || (surname != "") || (name != "")) ? (from v in dbRWZ.Requests select v).ToList() : (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            //var lst1 = (inn == "") ? (from v in lst0 where ((v.AgentRoleID == agentRoleID) && (v.OrgID == orgID)) select v).ToList() : (from v in lst0 where ((v.IdentificationNumber == inn) && ((v.AgentRoleID == agentRoleID) && (v.OrgID == orgID))) select v).ToList();
            //var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            //var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            //var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            //var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();
            //var lst5 = (creditID == "") ? lst4 : (from v in lst4 where (v.CreditID == Convert.ToInt32(creditID)) select v).ToList();
            //var lst6 = (i0 == false) ? (from v in lst5 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst5;
            var lst6 = (i0 == false) ? (from v in lst0 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst0;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправлено") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Исправлено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Утверждено") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Выдано") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Принято") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отменено") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Отказано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "В обработке") select v).ToList() : lst15;
            var lst17 = (i11 == false) ? (from v in lst16 where (v.RequestStatus != "На выдаче") select v).ToList() : lst16;
            var lst18 = (i12 == false) ? (from v in lst17 where (v.RequestStatus != "К подписи") select v).ToList() : lst17;

            var lst19 = (k0 == false) ? (from v in lst18 where (v.Bussiness != 0) select v).ToList() : lst18;
            var lst20 = (k1 == false) ? (from v in lst19 where (v.Bussiness != 1) select v).ToList() : lst19;
            var lst21 = (k2 == false) ? (from v in lst20 where (v.Bussiness != 2) select v).ToList() : lst20;

            //var lst22 = (g1 == false) ? (from v in lst21 where (v.OrgID != 17) select v).ToList() : lst21;  //beeshop
            var lst23 = (g1 == false) ? (from v in lst21 where ((v.OrgID != 3) && (v.OrgID != 7)) select v).ToList() : lst21;  //Билайн
            var lst24 = (g2 == false) ? (from v in lst23 where (v.GroupID != 110) select v).ToList() : lst23; //нано

            var lst26 = (o0 == false) ? (from v in lst24 where (v.BranchID != 1027) select v).ToList() : lst24; //ЦФ
            var lst27 = (o1 == false) ? (from v in lst26 where (v.OfficeID != 1082) select v).ToList() : lst26; //Берекет
            var lst28 = (o2 == false) ? (from v in lst27 where ((v.OfficeID != 1134) && (v.OfficeID != 1052)) select v).ToList() : lst27; //Карабалта
            var lst29 = (o3 == false) ? (from v in lst28 where ((v.OfficeID != 1133) && (v.OfficeID != 1120)) select v).ToList() : lst28; //Талас
            var lst30 = (o4 == false) ? (from v in lst29 where (v.BranchID != 1016) select v).ToList() : lst29; //Балыкчы
            var lst31 = (o5 == false) ? (from v in lst30 where (v.BranchID != 1017) select v).ToList() : lst30; //Ош
            var lst32 = (o6 == false) ? (from v in lst31 where (v.BranchID != 1018) select v).ToList() : lst31; //Нарын
            var lst33 = (o7 == false) ? (from v in lst32 where (v.BranchID != 1019) select v).ToList() : lst32; //Токмок
            var lst34 = (o8 == false) ? (from v in lst33 where (v.BranchID != 1020) select v).ToList() : lst33; //Каракол
            var lst35 = (o9 == false) ? (from v in lst34 where (v.BranchID != 1023) select v).ToList() : lst34; //Жалалабад


            if (lst35.Count > 0)
            {
                foreach (var ech in lst35)
                {
                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst35.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;

                    if ((lst35.IndexOf(ech) >= l1) && (lst35.IndexOf(ech) <= l2))
                    if ((ech.StatusOB != "Выдан") && (ech.StatusOB != "Отказано"))
                    {
                        if (ech.GroupID != 110)
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
                                    //var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault(); //ОБ
                                    var usrs2 = dbRWZ.Users2s.Where(u => u.UserIDOB == hstat.UserID).ToList().FirstOrDefault(); //скоринг
                                    {
                                        if (hstat.StatusID == 3) { f = 3; }
                                        if (hstat.StatusID == 4) { f = 4; }
                                    }
                                    if ((f == 3) || (f == 5))
                                    {
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        if (reqs.RequestStatus == "К подписи")
                                        {
                                            reqs.RequestStatus = "На выдаче";
                                            dbRWZ.Requests.Context.SubmitChanges();

                                            /*RequestHistory*/
                                            RequestsHistory newItem = new RequestsHistory()
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
                                        }
                                        //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                    }
                                    if (f == 4)
                                    {
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        reqs.RequestStatus = "Отказано";
                                        dbRWZ.Requests.Context.SubmitChanges();
                                        /*причина отказа*/
                                        string RejectReason = "";
                                        try
                                        {
                                            RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                        }
                                        catch
                                        {
                                            RejectReason = "";
                                        }
                                        finally
                                        {

                                        }
                                        /*RequestHistory*/
                                        RequestsHistory newItem = new RequestsHistory()
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
            }
            return lst35;

        }

        public static List<Request> GetRequestsAllForExpert(int userID, int? agentRoleID, string nRequest, string inn, string creditID, string surname, string name, string date1, string date2, string connectionString, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, bool i11, bool i12, bool k0, bool k1, bool k2, bool g1, bool g2, int pageIndex, int pageSize, bool o0, bool o1, bool o2, bool o3, bool o4, bool o5, bool o6, bool o7, bool o8, bool o9)
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);

            List<Request> lst0 = new List<Request>();
            if ((nRequest != "") || (inn != "") || (creditID != "") || (surname != "") || (name != ""))
            {
                if (inn != "") lst0 = (from v in dbRWZ.Requests where (v.IdentificationNumber == inn) select v).ToList();
                if (nRequest != "") lst0 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
                if (surname != "") lst0 = (from v in dbRWZ.Requests where (v.Surname == surname) select v).ToList();
                if (name != "") lst0 = (from v in dbRWZ.Requests where (v.CustomerName == name) select v).ToList();
                if (inn != "") lst0 = (from v in dbRWZ.Requests where (v.IdentificationNumber == inn) select v).ToList();
                if (creditID != "") lst0 = (from v in dbRWZ.Requests where (v.CreditID == Convert.ToInt32(creditID)) select v).ToList();

            }
            else lst0 = (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy-MM-dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy-MM-dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy-MM-dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy-MM-dd", CultureInfo.InvariantCulture))) select v).ToList();
            lst0 = (from v in lst0 where ((v.RequestType == "MK")) select v).ToList();

            //var lst0 = ((nRequest != "") || (inn != "") || (creditID != "") || (surname != "") || (name != "")) ? (from v in dbRWZ.Requests select v).ToList() : (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            //var lst1 = (inn == "") ? (from v in lst0 where ((v.AgentRoleID == agentRoleID) || (v.AgentRoleID == 19)) select v).ToList() : (from v in lst0 where ((v.IdentificationNumber == inn) && ((v.AgentRoleID == agentRoleID) || (v.AgentRoleID == 19))) select v).ToList();
            //var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            //var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            //var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            //var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();
            //var lst5 = (creditID == "") ? lst4 : (from v in lst4 where (v.CreditID == Convert.ToInt32(creditID)) select v).ToList();
            //var lst6 = (i0 == false) ? (from v in lst5 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst5;
            var lst6 = (i0 == false) ? (from v in lst0 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst0;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправить") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Исправлено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Утверждено") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Выдано") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Принято") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отменено") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Отказано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "В обработке") select v).ToList() : lst15;
            var lst17 = (i11 == false) ? (from v in lst16 where (v.RequestStatus != "На выдаче") select v).ToList() : lst16;
            var lst18 = (i12 == false) ? (from v in lst17 where (v.RequestStatus != "К подписи") select v).ToList() : lst17;


            var lst19 = (k0 == false) ? (from v in lst18 where (v.Bussiness != 0) select v).ToList() : lst18;
            var lst20 = (k1 == false) ? (from v in lst19 where (v.Bussiness != 1) select v).ToList() : lst19;
            var lst21 = (k2 == false) ? (from v in lst20 where (v.Bussiness != 2) select v).ToList() : lst20;


            //var lst22 = (g1 == false) ? (from v in lst21 where (v.OrgID != 17) select v).ToList() : lst21;  //beeshop
            //var lst23 = (g1 == false) ? (from v in lst21 where ((v.OrgID != 3) && (v.OrgID != 7)) select v).ToList() : lst21;  //Билайн
            //var lst24 = (g2 == false) ? (from v in lst23 where (v.GroupID != 110) select v).ToList() : lst23; //нано
            var lst23 = (g1 == false) ? (from v in lst21 where (v.GroupID == 110) select v).ToList() : lst21;  //Билайн
            var lst24 = (g2 == false) ? (from v in lst23 where (v.GroupID != 110) select v).ToList() : lst23; //нано

            var lst26 = (o0 == false) ? (from v in lst24 where (v.BranchID != 5) select v).ToList() : lst24; //ОАО Капитал Банк
            var lst27 = (o1 == false) ? (from v in lst26 where (v.OfficeID != 15) select v).ToList() : lst26; //Ошский филиал ОАО Капитал Банк
            var lst28 = (o2 == false) ? (from v in lst27 where ((v.OfficeID != 1134) && (v.OfficeID != 1052)) select v).ToList() : lst27; //Карабалта
            var lst29 = (o3 == false) ? (from v in lst28 where ((v.OfficeID != 1133) && (v.OfficeID != 1120)) select v).ToList() : lst28; //Талас
            var lst30 = (o4 == false) ? (from v in lst29 where (v.BranchID != 16) select v).ToList() : lst29; //Кызыл-Кийский филиал ОАО Капитал Банк
            var lst31 = (o5 == false) ? (from v in lst30 where (v.BranchID != 17) select v).ToList() : lst30; //Джалал-Абадский филиал ОАО Капитал Банк
            var lst32 = (o6 == false) ? (from v in lst31 where (v.BranchID != 18) select v).ToList() : lst31; //Араванский филиал ОАО Капитал Банк
            var lst33 = (o7 == false) ? (from v in lst32 where (v.BranchID != 19) select v).ToList() : lst32; //Караколский филиал ОАО Капитал Банк
            var lst34 = (o8 == false) ? (from v in lst33 where (v.BranchID != 24) select v).ToList() : lst33; //Бишкекский филиал "Капитал Банк Центр"
            var lst35 = (o9 == false) ? (from v in lst34 where (v.BranchID != 25) select v).ToList() : lst34; //Таласский филиал ОАО "Капитал Банк"


            if (lst35.Count > 0)
            {
                foreach (var ech in lst35)
                {

                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst35.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;

                    if ((lst35.IndexOf(ech) >= l1) && (lst35.IndexOf(ech) <= l2))
                    if ((ech.StatusOB != "Выдан") && (ech.StatusOB != "Отказано"))
                    {
                        string st = "";
                        var historystatus = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                        if (historystatus.Count > 0)
                        {
                            var hstat = historystatus.Last();
                            if (hstat.StatusID == 0) { st = "В процессе"; }
                            if (hstat.StatusID == 1) { st = "На согласовании"; }
                            if (hstat.StatusID == 2) { st = "На утверждении"; }
                            if (hstat.StatusID == 3) { st = "На выдаче"; }
                            if (hstat.StatusID == 4) { st = "Отказано"; }
                            if (hstat.StatusID == 5) { st = "Выдан";  }
                            if (hstat.StatusID == 6) { st = "Закрыт"; }
                            if (hstat.StatusID == 7) { st = "Списан"; }
                            if (hstat.StatusID == 8) { st = "Предварительная выдача"; }
                            if (hstat.StatusID == 9) { st = "Инициация выдачи"; }
                            var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                            reqs.StatusOB = st;
                            if (hstat.StatusID == 5) reqs.RequestStatus = "Выдано";
                            if (hstat.StatusID == 2) reqs.RequestStatus = "Утверждено";
                            if (hstat.StatusID == 3) reqs.RequestStatus = "На выдаче";
                            if (hstat.StatusID == 4) reqs.RequestStatus = "Отказано";
                            dbRWZ.Requests.Context.SubmitChanges();
                        }

                        if (ech.GroupID != 110)
                        {
                            if ((ech.RequestStatus != "Выдано") && (ech.RequestStatus != "На выдаче") && (ech.RequestStatus != "Отменено") && (ech.RequestStatus != "Отказано") && (ech.RequestStatus != "Принято"))
                            {
                                int f = 0;
                                CreditController ctx = new CreditController();
                                string usernameAgent = ech.AgentUsername;
                                string fullnameAgent = ech.AgentFirstName;
                                string fullnameCustomer = ech.Surname + " " + ech.CustomerName + " " + ech.Otchestvo;
                                int reqID = ech.RequestID;
                                //var historystatus2 = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                                //foreach (var hstat in historystatus)
                                if (historystatus.Count > 0)
                                {
                                    var hstat = historystatus.Last();
                                    //var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault(); //ОБ
                                    var usrs2 = dbRWZ.Users2s.Where(u => u.UserIDOB == hstat.UserID).ToList().FirstOrDefault(); //скоринг
                                    {
                                        if (hstat.StatusID == 3) { f = 3; }
                                        if (hstat.StatusID == 4) { f = 4; }
                                        if (hstat.StatusID == 5) { f = 5; }
                                    }
                                    if (f == 3) 
                                    {
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        if ((reqs.RequestStatus == "Утверждено") || (reqs.RequestStatus == "К подписи"))
                                        {
                                            reqs.RequestStatus = "На выдаче";
                                            dbRWZ.Requests.Context.SubmitChanges();

                                            /*RequestHistory*/
                                            RequestsHistory newItem = new RequestsHistory()
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
                                        }
                                        //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                    }
                                    if (f == 5)
                                    {
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        if ((reqs.RequestStatus == "На выдаче") && (reqs.TypeOfIssue == 0))
                                        {
                                            reqs.RequestStatus = "Выдано";
                                            dbRWZ.Requests.Context.SubmitChanges();

                                            /*RequestHistory*/
                                            RequestsHistory newItem = new RequestsHistory()
                                            {
                                                AgentID = (usrs2 != null) ? usrs2.UserID : 0,
                                                CreditID = hstat.CreditID,
                                                // CustomerID = Convert.ToInt32(hfCustomerID.Value),
                                                StatusDate = hstat.OperationDate,
                                                Status = "Выдано",
                                                note = "",
                                                RequestID = ech.RequestID
                                            };
                                            ctx.ItemRequestHistoriesAddItem(newItem);
                                        }
                                        //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                    }

                                    if (f == 4)
                                    {
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        reqs.RequestStatus = "Отказано";
                                        dbRWZ.Requests.Context.SubmitChanges();
                                        /*причина отказа*/
                                        string RejectReason = "";
                                        try
                                        {
                                            RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                        }
                                        catch
                                        {
                                            RejectReason = "";
                                        }
                                        finally
                                        {

                                        }
                                        /*RequestHistory*/
                                        RequestsHistory newItem = new RequestsHistory()
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
            }
            return lst35;

        }



        public static List<Request> GetRequestsForExpert(int userID, int? agentRoleID, string nRequest, string inn, string creditID, string surname, string name, string date1, string date2, string connectionString, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, bool i11, bool i12, bool k0, bool k1, bool k2, bool g1, bool g2, int pageIndex, int pageSize, bool o0, bool o1, bool o2, bool o3, bool o4, bool o5, bool o6, bool o7, bool o8, bool o9)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            int branchID = 5;
            int officeID = dbRWZ.Users2s.Where(r => r.UserID == userID).FirstOrDefault().OfficeID;
            branchID = dbR.Offices.Where(r => r.ID == officeID).FirstOrDefault().BranchID;

            List<Request> lst0 = new List<Request>();
            if ((nRequest != "") || (inn != "") || (creditID != "") || (surname != "") || (name != ""))
            {
                if (inn != "") lst0 = (from v in dbRWZ.Requests where (v.IdentificationNumber == inn) select v).ToList();
                if (nRequest != "") lst0 = (from v in dbRWZ.Requests where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
                if (surname != "") lst0 = (from v in dbRWZ.Requests where (v.Surname == surname) select v).ToList();
                if (name != "") lst0 = (from v in dbRWZ.Requests where (v.CustomerName == name) select v).ToList();
                if (inn != "") lst0 = (from v in dbRWZ.Requests where (v.IdentificationNumber == inn) select v).ToList();
                if (creditID != "") lst0 = (from v in dbRWZ.Requests where (v.CreditID == Convert.ToInt32(creditID)) select v).ToList();

            }
            //else lst0 = (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            else lst0 = (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == Convert.ToDateTime(date1) || (v.RequestDate.Value.Date > Convert.ToDateTime(date1)) && (v.RequestDate.Value.Date < Convert.ToDateTime(date2)) || (v.RequestDate.Value.Date == Convert.ToDateTime(date2)))) select v).ToList();
            lst0 = (from v in lst0 where ((v.RequestType == "MK") && (v.BranchID == branchID)) select v).ToList();

            //var lst0 = ((nRequest != "") || (inn != "") || (creditID != "") || (surname != "") || (name != "")) ? (from v in dbRWZ.Requests select v).ToList() : (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            //var lst1 = (inn == "") ? (from v in lst0 where (((v.AgentRoleID == agentRoleID) || (v.AgentRoleID == 19)) && (v.BranchID == branchID)) select v).ToList() : (from v in lst0 where ((v.IdentificationNumber == inn) && ((v.AgentRoleID == agentRoleID) || (v.AgentRoleID == 19)) && (v.BranchID == branchID)) select v).ToList();
            //var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            //var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            //var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            //var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();
            //var lst5 = (creditID == "") ? lst4 : (from v in lst4 where (v.CreditID == Convert.ToInt32(creditID)) select v).ToList();
            //var lst6 = (i0 == false) ? (from v in lst5 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst5;
            var lst6 = (i0 == false) ? (from v in lst0 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst0;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправить") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Исправлено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Утверждено") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Выдано") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Принято") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отменено") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Отказано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "В обработке") select v).ToList() : lst15;
            var lst17 = (i11 == false) ? (from v in lst16 where (v.RequestStatus != "На выдаче") select v).ToList() : lst16;
            var lst18 = (i12 == false) ? (from v in lst17 where (v.RequestStatus != "К подписи") select v).ToList() : lst17;


            var lst19 = (k0 == false) ? (from v in lst18 where (v.Bussiness != 0) select v).ToList() : lst18;
            var lst20 = (k1 == false) ? (from v in lst19 where (v.Bussiness != 1) select v).ToList() : lst19;
            var lst21 = (k2 == false) ? (from v in lst20 where (v.Bussiness != 2) select v).ToList() : lst20;

            //var lst22 = (g1 == false) ? (from v in lst21 where (v.OrgID != 17) select v).ToList() : lst21;  //beeshop
            var lst23 = (g1 == false) ? (from v in lst21 where ((v.OrgID != 3) && (v.OrgID != 7)) select v).ToList() : lst21;  //Билайн
            var lst24 = (g2 == false) ? (from v in lst23 where (v.GroupID != 110) select v).ToList() : lst23; //нано

            var lst26 = (o0 == false) ? (from v in lst24 where (v.BranchID != 1027) select v).ToList() : lst24; //ЦФ
            var lst27 = (o1 == false) ? (from v in lst26 where (v.OfficeID != 1082) select v).ToList() : lst26; //Берекет
            var lst28 = (o2 == false) ? (from v in lst27 where ((v.OfficeID != 1134) && (v.OfficeID != 1052)) select v).ToList() : lst27; //Карабалта
            var lst29 = (o3 == false) ? (from v in lst28 where ((v.OfficeID != 1133) && (v.OfficeID != 1120)) select v).ToList() : lst28; //Талас
            var lst30 = (o4 == false) ? (from v in lst29 where (v.BranchID != 1016) select v).ToList() : lst29; //Балыкчы
            var lst31 = (o5 == false) ? (from v in lst30 where (v.BranchID != 1017) select v).ToList() : lst30; //Ош
            var lst32 = (o6 == false) ? (from v in lst31 where (v.BranchID != 1018) select v).ToList() : lst31; //Нарын
            var lst33 = (o7 == false) ? (from v in lst32 where (v.BranchID != 1019) select v).ToList() : lst32; //Токмок
            var lst34 = (o8 == false) ? (from v in lst33 where (v.BranchID != 1020) select v).ToList() : lst33; //Каракол
            var lst35 = (o9 == false) ? (from v in lst34 where (v.BranchID != 1023) select v).ToList() : lst34; //Жалалабад

            if (lst35.Count > 0)
            {
                foreach (var ech in lst35)
                {
                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst35.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;

                    if ((lst35.IndexOf(ech) >= l1) && (lst35.IndexOf(ech) <= l2))
                    if ((ech.StatusOB != "Выдан") && (ech.StatusOB != "Отказано"))
                    {

                        string st = "";
                        var historystatus = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                        if (historystatus.Count > 0)
                        {
                            var hstat = historystatus.Last();
                            if (hstat.StatusID == 0) { st = "В процессе"; }
                            if (hstat.StatusID == 1) { st = "На согласовании"; }
                            if (hstat.StatusID == 2) { st = "На утверждении"; }
                            if (hstat.StatusID == 3) { st = "На выдаче"; }
                            if (hstat.StatusID == 4) { st = "Отказано"; }
                            if (hstat.StatusID == 5) { st = "Выдан"; }
                            if (hstat.StatusID == 6) { st = "Закрыт"; }
                            if (hstat.StatusID == 7) { st = "Списан"; }
                            if (hstat.StatusID == 8) { st = "Предварительная выдача"; }
                            if (hstat.StatusID == 9) { st = "Инициация выдачи"; }
                            var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                            reqs.StatusOB = st;
                                if (hstat.StatusID == 5) reqs.RequestStatus = "Выдано";
                                if (hstat.StatusID == 2) reqs.RequestStatus = "Утверждено";
                                if (hstat.StatusID == 3) reqs.RequestStatus = "На выдаче";
                                if (hstat.StatusID == 4) reqs.RequestStatus = "Отказано";
                            dbRWZ.Requests.Context.SubmitChanges();
                                
                        }

                        if (ech.GroupID != 110)
                        {
                            if ((ech.RequestStatus != "Выдано") && (ech.RequestStatus != "На выдаче") && (ech.RequestStatus != "Отменено") && (ech.RequestStatus != "Отказано") && (ech.RequestStatus != "Принято"))
                            {
                                int f = 0;

                                CreditController ctx = new CreditController();
                                string usernameAgent = ech.AgentUsername;
                                string fullnameAgent = ech.AgentFirstName;
                                string fullnameCustomer = ech.Surname + " " + ech.CustomerName + " " + ech.Otchestvo;
                                int reqID = ech.RequestID;
                                //var historystatus2 = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                                //foreach (var hstat in historystatus)
                                if (historystatus.Count > 0)
                                {
                                    var hstat = historystatus.Last();
                                    //var usrs = dbR.Users.Where(u => u.UserID == hstat.UserID).ToList().FirstOrDefault(); //ОБ
                                    var usrs2 = dbRWZ.Users2s.Where(u => u.UserIDOB == hstat.UserID).ToList().FirstOrDefault(); //скоринг
                                    {
                                        if (hstat.StatusID == 3) { f = 3; }
                                        if (hstat.StatusID == 4) { f = 4; }
                                    }
                                    if (f == 3)
                                    {
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        if (reqs.RequestStatus == "К подписи")
                                        {
                                            reqs.RequestStatus = "На выдаче";
                                            dbRWZ.Requests.Context.SubmitChanges();


                                            /*RequestHistory*/
                                            RequestsHistory newItem = new RequestsHistory()
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
                                        }
                                        //AgentView.SendMailTo2("На выдаче", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); ////агентам и админам
                                    }
                                    if (f == 4)
                                    {
                                        var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                        reqs.RequestStatus = "Отказано";
                                        dbRWZ.Requests.Context.SubmitChanges();
                                        /*причина отказа*/
                                        string RejectReason = "";
                                        try
                                        {
                                            RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                        }
                                        catch
                                        {
                                            RejectReason = "";
                                        }
                                        finally
                                        {

                                        }
                                        /*RequestHistory*/
                                        RequestsHistory newItem = new RequestsHistory()
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
            }
            return lst35;

        }




        public static List<Request> GetRequestsAllForExpert(int userID, int? agentRoleID, string nRequest, string inn, string surname, string name, string date1, string date2, string connectionString, bool i0, bool i1, bool i2, bool i3, bool i4, bool i5, bool i6, bool i7, bool i8, bool i9, bool i10, bool i11, bool i12, bool k0, bool k1, bool k2, bool g1, bool g2, bool g3, int pageIndex, int pageSize)
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var lst0 = (from v in dbRWZ.Requests where ((v.RequestDate.Value.Date == DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date > DateTime.ParseExact(date1, "yyyy.MM.dd", CultureInfo.InvariantCulture)) && (v.RequestDate.Value.Date < DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture)) || (v.RequestDate.Value.Date == DateTime.ParseExact(date2, "yyyy.MM.dd", CultureInfo.InvariantCulture))) select v).ToList();
            var lst1 = (inn == "") ? (from v in lst0 where (v.AgentRoleID == agentRoleID) select v).ToList() : (from v in lst0 where ((v.IdentificationNumber == inn) && (v.AgentRoleID == agentRoleID)) select v).ToList();
            var lst1_5 = (nRequest == "") ? lst1 : (from v in lst1 where (v.RequestID == Convert.ToInt32(nRequest)) select v).ToList();
            var lst2 = (surname == "") ? lst1_5 : (from v in lst1_5 where (v.Surname == surname) select v).ToList();
            var lst3 = (name == "") ? lst2 : (from v in lst2 where (v.CustomerName == name) select v).ToList();
            var lst4 = (inn == "") ? lst3 : (from v in lst3 where (v.IdentificationNumber == inn) select v).ToList();

            var lst6 = (i0 == false) ? (from v in lst4 where (v.RequestStatus != "Новая заявка") select v).ToList() : lst4;
            var lst7 = (i1 == false) ? (from v in lst6 where (v.RequestStatus != "Исправить") select v).ToList() : lst6;
            var lst8 = (i2 == false) ? (from v in lst7 where (v.RequestStatus != "Исправлено") select v).ToList() : lst7;
            var lst9 = (i3 == false) ? (from v in lst8 where (v.RequestStatus != "Подтверждено") select v).ToList() : lst8;
            var lst10 = (i4 == false) ? (from v in lst9 where (v.RequestStatus != "Утверждено") select v).ToList() : lst9;
            var lst11 = (i5 == false) ? (from v in lst10 where (v.RequestStatus != "Выдано") select v).ToList() : lst10;
            var lst12 = (i6 == false) ? (from v in lst11 where (v.RequestStatus != "Принято") select v).ToList() : lst11;
            var lst13 = (i7 == false) ? (from v in lst12 where (v.RequestStatus != "Не подтверждено") select v).ToList() : lst12;
            var lst14 = (i8 == false) ? (from v in lst13 where (v.RequestStatus != "Отменено") select v).ToList() : lst13;
            var lst15 = (i9 == false) ? (from v in lst14 where (v.RequestStatus != "Отказано") select v).ToList() : lst14;
            var lst16 = (i10 == false) ? (from v in lst15 where (v.RequestStatus != "В обработке") select v).ToList() : lst15;
            var lst17 = (i11 == false) ? (from v in lst16 where (v.RequestStatus != "На выдаче") select v).ToList() : lst16;
            var lst18 = (i12 == false) ? (from v in lst17 where (v.RequestStatus != "К подписи") select v).ToList() : lst17;


            var lst19 = (k0 == false) ? (from v in lst18 where (v.Bussiness != 0) select v).ToList() : lst18;
            var lst20 = (k1 == false) ? (from v in lst19 where (v.Bussiness != 1) select v).ToList() : lst19;
            var lst21 = (k2 == false) ? (from v in lst20 where (v.Bussiness != 2) select v).ToList() : lst20;

            //var lst18 = (k0 == false) ? (from v in lst17 where (v.Bussiness == 1) select v).ToList() : lst17;
            //var lst19 = (k1 == false) ? (from v in lst18 where (v.Bussiness == 0) select v).ToList() : lst18;
            //var lst20 = (k2 == false) ? (from v in lst19 where (v.Bussiness == 2) select v).ToList() : lst19;


            var lst22 = (g1 == false) ? (from v in lst21 where (v.GroupID != 56) select v).ToList() : lst21;  //beeshop
            var lst23 = (g2 == false) ? (from v in lst22 where (v.GroupID != 100) select v).ToList() : lst22;  //Билайн
            var lst24 = (g3 == false) ? (from v in lst23 where (v.GroupID != 110) select v).ToList() : lst23; //нано

            if (lst24.Count > 0)
            {
                foreach (var ech in lst24)
                {

                    int l1 = ((pageIndex + 1) * pageSize - pageSize);
                    int l2 = ((pageIndex + 1) * pageSize - 1);
                    int n = lst24.Count;  //gvRequests.Rows.Count;
                    if (l2 > n) l2 = n - 1;

                    if ((lst24.IndexOf(ech) >= l1) && (lst24.IndexOf(ech) <= l2))
                    {
                        string st = "";
                        var historystatus = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
                        if (historystatus.Count > 0)
                        {
                            var hstat = historystatus.Last();
                            if (hstat.StatusID == 0) { st = "В процессе"; }
                            if (hstat.StatusID == 1) { st = "На согласовании"; }
                            if (hstat.StatusID == 2) { st = "На утверждении"; }
                            if (hstat.StatusID == 3) { st = "На выдаче"; }
                            if (hstat.StatusID == 4) { st = "Отказано"; }
                            if (hstat.StatusID == 5) { st = "Выдан"; }
                            if (hstat.StatusID == 6) { st = "Закрыт"; }
                            if (hstat.StatusID == 7) { st = "Списан"; }
                            if (hstat.StatusID == 8) { st = "Предварительная выдача"; }
                            if (hstat.StatusID == 9) { st = "Инициация выдачи"; }
                            var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                            reqs.StatusOB = st;
                            dbRWZ.Requests.Context.SubmitChanges();
                        }

                        if ((ech.RequestStatus != "Выдано") && (ech.RequestStatus != "На выдаче") && (ech.RequestStatus != "Отменено") && (ech.RequestStatus != "Отказано") && (ech.RequestStatus != "Принято"))
                        {
                            int f = 0;
                            CreditController ctx = new CreditController();
                            string usernameAgent = ech.AgentUsername;
                            string fullnameAgent = ech.AgentFirstName;
                            string fullnameCustomer = ech.Surname + " " + ech.CustomerName + " " + ech.Otchestvo;
                            int reqID = ech.RequestID;
                            //var historystatus2 = (from v in dbR.HistoriesStatuses where ech.CreditID == v.CreditID orderby v.OperationDate select v).ToList();
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
                                if ((f == 3) || (f == 5))
                                {
                                    var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "На выдаче";
                                    dbRWZ.Requests.Context.SubmitChanges();

                                    /*RequestHistory*/
                                    RequestsHistory newItem = new RequestsHistory()
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
                                    var reqs = dbRWZ.Requests.Where(r => r.RequestID == ech.RequestID).FirstOrDefault();
                                    reqs.RequestStatus = "Отказано";
                                    dbRWZ.Requests.Context.SubmitChanges();
                                    /*причина отказа*/
                                    string RejectReason = "";
                                    try
                                    {
                                        RejectReason = dbR.RejectedHistoriesReasons.Where(r => r.CreditID == ech.CreditID).FirstOrDefault().RejectReason;
                                    }
                                    catch
                                    {
                                        RejectReason = "";
                                    }
                                    finally
                                    {

                                    }
                                    /*RequestHistory*/
                                    RequestsHistory newItem = new RequestsHistory()
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
                                    // AgentView.SendMailTo2("Отказано", true, true, false, connectionString, userID, reqID, fullnameAgent, usernameAgent, fullnameCustomer); //агентам и админам
                                }
                            }
                        }
                    }
                }
            }
            return lst24;

        }



        public static List<Request> GetRequests(int userID)
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var lst = (from v in dbRWZ.Requests where ((v.AgentID == userID)) select v).ToList();
            return lst;
        }

        public int ItemRequestAddItem(Request newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.Requests.InsertOnSubmit(newItem);
            dbRWZ.Requests.Context.SubmitChanges();
            return (newItem.RequestID);
        }
        public void ItemRequestFilesAddItem(RequestsFile newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.RequestsFiles.InsertOnSubmit(newItem);
            dbRWZ.RequestsFiles.Context.SubmitChanges();
        }
        public void ItemRequestProductAddItem(RequestsProduct newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.RequestsProducts.InsertOnSubmit(newItem);
            dbRWZ.RequestsProducts.Context.SubmitChanges();
        }

        public void ItemProductAddItem(Product newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.Products.InsertOnSubmit(newItem);
            dbRWZ.Products.Context.SubmitChanges();
        }

        public void DeleteProduct(int itemid)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.RequestsProducts.DeleteAllOnSubmit(from v in dbRWZ.RequestsProducts where (v.ProductID == itemid) select v);
            dbRWZ.RequestsProducts.Context.SubmitChanges();
        }

        public void DeleteRequestsFiles(int itemid)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.RequestsFiles.DeleteAllOnSubmit(from v in dbRWZ.RequestsFiles where (v.ID == itemid) select v);
            dbRWZ.RequestsFiles.Context.SubmitChanges();
        }

        public Request GetRequestByCreditID(int itemid)
        {
            Request itm = new Request();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            itm = dbRWZ.Requests.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }

        public Request GetRequestByRequestID(int reqid)
        {
            Request itm = new Request();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            itm = dbRWZ.Requests.Where(g => g.RequestID == reqid).FirstOrDefault();
            return itm;
        }


        public void RequestUpd(Request item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var lst = dbRWZ.Requests.Where(r => r.CreditID == item.CreditID).FirstOrDefault();

            lst.CreditPurpose = item.CreditPurpose;
            lst.CreditProduct = item.CreditProduct;
            lst.RequestPeriod = item.RequestPeriod;
            lst.ProductPrice = item.ProductPrice;
            lst.AmountDownPayment = item.AmountDownPayment;
            lst.RequestSumm = item.RequestSumm;
            lst.RequestRate = item.RequestRate;
            lst.RequestGrantComission = item.RequestGrantComission;
            lst.ActualDate = item.ActualDate;
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
            //editRequest.CreditID = CreditsHistoriesID;
            lst.AverageMonthSalary = item.AverageMonthSalary;
            lst.SumMonthSalary = item.SumMonthSalary;
            lst.CountMonthSalary = item.CountMonthSalary;
            lst.Revenue = item.Revenue;
            lst.CountWorkDay = item.CountWorkDay;
            lst.СostPrice = item.СostPrice;
            lst.Overhead = item.Overhead;
            lst.FamilyExpenses = item.FamilyExpenses;
            lst.Bussiness = item.Bussiness;
            lst.OtherLoans = item.OtherLoans;
            lst.IsEmployer = item.IsEmployer;
            lst.BusinessComment = item.BusinessComment;
            lst.AdditionalIncome = item.AdditionalIncome;
            lst.MinRevenue = item.MinRevenue;
            lst.MaxRevenue = item.MaxRevenue;
            lst.RevenueAgro = item.RevenueAgro;
            lst.RevenueMilk = item.RevenueMilk;
            lst.OverheadAgro = item.OverheadAgro;
            lst.AddOverheadAgro = item.AddOverheadAgro;
            lst.MaritalStatus = item.MaritalStatus;
            lst.OrganizationINN = item.OrganizationINN;
            lst.RevenueMilkProd = item.RevenueMilkProd;
            lst.isNoPledge = item.isNoPledge;
            lst.isGuarantor = item.isGuarantor;
            lst.isPledger = item.isPledger;
            lst.GuarantorName = item.GuarantorName;
            lst.GuarantorSurname = item.GuarantorSurname;
            lst.GuarantorOtchestvo = item.GuarantorOtchestvo;
            lst.GuarantorIdentificationNumber = item.IdentificationNumber;
            lst.PledgerName = item.PledgerName;
            lst.PledgerSurname = item.PledgerSurname;
            lst.PledgerOtchestvo = item.PledgerOtchestvo;
            lst.PledgerIdentificationNumber = item.PledgerIdentificationNumber;
            lst.CardNumber = item.CardNumber;
            lst.TypeOfIssue = item.TypeOfIssue;
            lst.MortrageTypeID = item.MortrageTypeID;
            lst.KindOfAgriculture = item.KindOfAgriculture;
            lst.Bulls = item.Bulls;
            lst.BairyCows = item.BairyCows;
            lst.Sheeps = item.Sheeps;
            lst.Horse = item.Horse;
            lst.ExperienceAnimals = item.ExperienceAnimals;
            dbRWZ.Requests.Context.SubmitChanges();
        }

        public RequestsProduct GetRequestsProductById(int itemid)
        {
            RequestsProduct itm = new RequestsProduct();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            itm = dbRWZ.RequestsProducts.Where(g => g.ProductID == itemid).FirstOrDefault();
            return itm;
        }

        public void RequestsProductsUpd(RequestsProduct item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);

            var ctx = dbRWZ.RequestsProducts.Where(r => r.ProductID == item.ProductID).FirstOrDefault();
            ctx.ProductMark = item.ProductMark;
            ctx.ProductSerial = item.ProductSerial;
            ctx.ProductImei = item.ProductImei;
            ctx.ProductImei2 = item.ProductImei2;
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

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);

            dbRWZ.RequestsProductsDels.InsertOnSubmit(newItem);
            dbRWZ.RequestsProductsDels.Context.SubmitChanges();
        }


        /********************/
        public void ItemJournalAdd(JournalNanoBeeline newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.JournalNanoBeeline.InsertOnSubmit(newItem);
            dbRWZ.JournalNanoBeeline.Context.SubmitChanges();
        }

        public void ItemJournalUpd(JournalNanoBeeline item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            var itm = dbRWZ.JournalNanoBeeline.Where(r => r.ID == item.ID).FirstOrDefault();
            itm.Status = item.Status;
            dbRWZ.JournalNanoBeeline.Context.SubmitChanges();
        }

        /****************/
        public void ItemGuaranteeAddItem(Guarantee newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.Guarantees.InsertOnSubmit(newItem);
            dbRWZ.Guarantees.Context.SubmitChanges();
        }

        public void ItemDeleteGuarantees(int itemid)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            dbRWZ.Guarantees.DeleteAllOnSubmit(from v in dbRWZ.Guarantees where (v.ID == itemid) select v);
            dbRWZ.Guarantees.Context.SubmitChanges();
        }

        public Guarantee GetGuaranteeById(int itemid)
        {
            Guarantee itm = new Guarantee();
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            itm = dbRWZ.Guarantees.Where(g => g.ID == itemid).FirstOrDefault();
            return itm;
        }
        public void GuaranteeUpd(Guarantee item)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);

            var ctx = dbRWZ.Guarantees.Where(r => r.ID == item.ID).FirstOrDefault();
            ctx.Name = item.Name;
            ctx.Count = item.Count;
            ctx.Address = item.Address;
            ctx.Description = item.Description;
            ctx.AssessedPrice = item.AssessedPrice;
            ctx.MarketPrice = item.MarketPrice;
            ctx.Coefficient = item.Coefficient;
            dbRWZ.Guarantees.Context.SubmitChanges();
        }

        //public void CardItemRequestFilesAddItem(CardRequestsFile newItem)
        //{
        //    dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
        //    dbRWZ.CardRequestsFiles.InsertOnSubmit(newItem);
        //    dbRWZ.CardRequestsFiles.Context.SubmitChanges();
        //    //return(newItem.ID);
        //}

    }
}