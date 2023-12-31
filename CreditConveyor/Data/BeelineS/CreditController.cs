﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Zamat;

namespace СreditСonveyor.Data.BeelineS
{
    public class CreditController
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();


        public int HistoriesAddItem(History newItem)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.Histories.InsertOnSubmit(newItem);
            dbW.Histories.Context.SubmitChanges();
            return (newItem.CreditID);
        }

        public void HistoriesUpdate(History item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.Histories.InsertOnSubmit(item);
            dbW.Histories.Context.SubmitChanges();
        }
        public History HistoriesGetItem(int CreditID)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);

            History item = new History();
            item = (from v in dbR.Histories where v.CreditID == CreditID select v).FirstOrDefault();
            return item;
        }
        public History GetHistoryByCreditID(int itemid)
        {
            History itm = new History();
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            itm = dbR.Histories.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }
        public void HistoryUpd(History item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            var lst = dbW.Histories.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            lst.RequestPeriod = item.RequestPeriod;
            lst.RequestRate = item.RequestRate;
            lst.ProductID = item.ProductID;
            lst.ApprovedPeriod = item.ApprovedPeriod;
            lst.ApprovedRate = item.ApprovedRate;
            lst.PartnerCompanyID = item.PartnerCompanyID;
            dbW.Histories.Context.SubmitChanges();
        }
        /**************************************************************************/

        /*Credits.HistoriesCustomer*/

        public void HistoriesCustomerAddItem(HistoriesCustomer newItem)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.HistoriesCustomers.InsertOnSubmit(newItem);
            dbW.HistoriesCustomers.Context.SubmitChanges();

        }
        public HistoriesCustomer HistoriesCustomerGetItem(int CreditID)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            HistoriesCustomer item = new HistoriesCustomer();
            item = (from v in dbR.HistoriesCustomers where v.CreditID == CreditID select v).FirstOrDefault();
            return item;
        }
        public HistoriesCustomer GetHistoriesCustomerByCreditID(int itemid)
        {
            HistoriesCustomer itm = new HistoriesCustomer();

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            itm = dbR.HistoriesCustomers.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }

        public void HistoriesCustomerUpd(HistoriesCustomer item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            var lst = dbW.HistoriesCustomers.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            lst.CreditPurpose = item.CreditPurpose;
            lst.RequestSumm = item.RequestSumm;
            lst.CustomerID = item.CustomerID;
            lst.ApprovedSumm = item.ApprovedSumm;
            dbW.HistoriesCustomers.Context.SubmitChanges();
        }


        /***********************************************************/
        public int GuarantorGetItem(int CreditID)
        {
            //dbdataDataContext dbR = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Guarantor item = new Guarantor();
            item = (from v in dbR.Guarantors where v.CreditID == CreditID select v).FirstOrDefault();
            if (item != null) { return item.CustomerID; }
            else { return -1; }
        }

        /*************************************************************/
        public void HistoriesEarlyPaymentComissionAddItem(HistoriesEarlyPaymentComission newItem)
        {
            //dbdataDataContext dbW = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.HistoriesEarlyPaymentComissions.InsertOnSubmit(newItem);
            dbW.HistoriesEarlyPaymentComissions.Context.SubmitChanges();
        }
        public HistoriesEarlyPaymentComission GetHistoriesEarlyPaymentComissionByCreditID(int itemid)
        {
            HistoriesEarlyPaymentComission itm = new HistoriesEarlyPaymentComission();
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            itm = dbR.HistoriesEarlyPaymentComissions.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }
        public void HistoriesEarlyPaymentComissionUpd(HistoriesEarlyPaymentComission item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            var lst = dbW.HistoriesEarlyPaymentComissions.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            lst.Period = item.Period;
            lst.RequestPartialComissionType = item.RequestPartialComissionType;
            lst.RequestFullPaymentComissionType = item.RequestFullPaymentComissionType;
            dbW.HistoriesCustomers.Context.SubmitChanges();
        }
        /*************************************************************/
        public void HistoriesOfficerAddItem(HistoriesOfficer newItem)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.HistoriesOfficers.InsertOnSubmit(newItem);
            dbW.HistoriesOfficers.Context.SubmitChanges();
        }
        public HistoriesOfficer GetHistoriesOfficerByCreditID(int itemid)
        {
            HistoriesOfficer itm = new HistoriesOfficer();
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            itm = dbR.HistoriesOfficers.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }
        public void HistoriesOfficerUpd(HistoriesOfficer item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            var lst = dbW.HistoriesOfficers.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            lst.OfficerTypeID = item.OfficerTypeID;
            lst.UserID = item.UserID;
            dbW.HistoriesCustomers.Context.SubmitChanges();
        }
        /*************************************************************/
        public void ItemIncomesStructuresAddItem(IncomesStructure newItem)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.IncomesStructures.InsertOnSubmit(newItem);
            dbW.IncomesStructures.Context.SubmitChanges();
        }

        public IncomesStructure GetIncomesStructureByCreditID(int itemid)
        {
            IncomesStructure itm = new IncomesStructure();
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            itm = dbR.IncomesStructures.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }
        public void IncomesStructuresUpd(IncomesStructure item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            var lst = dbW.IncomesStructures.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            lst.ActualDate = item.ActualDate;
            dbW.IncomesStructures.Context.SubmitChanges();
        }
        /*************************************************************/
        public void IncomesStructuresActualDateAddItem(IncomesStructuresActualDate newItem)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.IncomesStructuresActualDates.InsertOnSubmit(newItem);
            dbW.IncomesStructuresActualDates.Context.SubmitChanges();
        }
        public IncomesStructuresActualDate GetIncomesStructuresActualDateByCreditID(int itemid)
        {
            IncomesStructuresActualDate itm = new IncomesStructuresActualDate();
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            itm = dbR.IncomesStructuresActualDates.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }
        public void IncomesStructuresActualDateUpd(IncomesStructuresActualDate item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            var lst = dbW.IncomesStructuresActualDates.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            lst.ActualDate = item.ActualDate;
            dbW.IncomesStructuresActualDates.Context.SubmitChanges();
        }
        /*****************************************************************/

        public List<Graphic> GraphicsGetItem(int CreditID)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            var item = (from v in dbR.Graphics where (v.CreditID == CreditID) orderby v.PayDate select v).ToList();
            return item;
        }

        /*****************************************************************/
        public void ItemRequestHistoriesAddItem(RequestsHistories2 newItem)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);

            dbRWZ.RequestsHistories2s.InsertOnSubmit(newItem);
            dbRWZ.RequestsHistories2s.Context.SubmitChanges();
        }
        /*****************************************************************/
        public void ItemHistoriesStatuseAddItem(HistoriesStatuse newItem)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.HistoriesStatuses.InsertOnSubmit(newItem);
            dbW.HistoriesStatuses.Context.SubmitChanges();
        }

        public List<HistoriesStatuse> ItemHistoriesStatuseGetItem(int CreditID)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            var item = (from v in dbR.HistoriesStatuses where (v.CreditID == CreditID) orderby v.StatusID select v).ToList();
            return item;
        }
        /*****************************************************************/
        public void PartnerHistoriesAddItem(PartnersHistory newItem)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbW.PartnersHistories.InsertOnSubmit(newItem);
            dbW.PartnersHistories.Context.SubmitChanges();
        }
        /*****************************************************************/
        public void PartnerHistoriesUpd(PartnersHistory item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            var lst = dbW.PartnersHistories.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            lst.SumV = item.SumV;
            dbW.PartnersHistories.Context.SubmitChanges();
        }
        /*****************************************************************/
        /*****************************************************************/
        public void PartnerHistoriesUpdItem(PartnersHistory item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            var lst = dbW.PartnersHistories.Where(r => r.CreditID == item.CreditID).FirstOrDefault();
            lst.SumV = item.SumV;
            dbW.PartnersHistories.Context.SubmitChanges();
        }
        /*****************************************************************/
        /*****************************************************************/
        public PartnersHistory GetPartnerHistoryByCreditID(int itemid)
        {
            PartnersHistory itm = new PartnersHistory();
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            itm = dbR.PartnersHistories.Where(g => g.CreditID == itemid).FirstOrDefault();
            return itm;
        }



    }
}