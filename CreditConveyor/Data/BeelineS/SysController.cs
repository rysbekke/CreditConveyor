using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Zamat;

namespace СreditСonveyor.Data.BeelineS
{
    public class SysController
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();

        public void CustomerAddItem(Customer newItem)
        {

            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            dbW.Customers.InsertOnSubmit(newItem);
            dbW.Customers.Context.SubmitChanges();
        }

        public Customer CustomerGetItem(int CustomerID)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Customer item = new Customer();
            item = (from v in dbR.Customers where v.CustomerID == CustomerID select v).FirstOrDefault();
            return item;
        }

        public int CustomerAddItem2(Customer newItem)
        {
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbW.Customers.InsertOnSubmit(newItem);
            dbW.Customers.Context.SubmitChanges();
            return newItem.CustomerID;
        }



        public void CustomerUpdItem(Customer item)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);

            var lst = dbW.Customers.Where(r => r.CustomerID == item.CustomerID).FirstOrDefault();
            if (item.DocumentSeries != null) lst.DocumentSeries = item.DocumentSeries;
            if (item.DocumentNo != null) lst.DocumentNo = item.DocumentNo;
            if (item.IssueDate != null) lst.IssueDate = item.IssueDate;
            if (item.IssueAuthority != null) lst.IssueAuthority = item.IssueAuthority;
            if (item.Sex != null) lst.Sex = item.Sex;
            if (item.DateOfBirth != null) lst.DateOfBirth = item.DateOfBirth;
            if (item.RegistrationStreet != null) lst.RegistrationStreet = item.RegistrationStreet;
            if (item.RegistrationHouse != null) lst.RegistrationHouse = item.RegistrationHouse;
            if (item.RegistrationFlat != null) lst.RegistrationFlat = item.RegistrationFlat;
            if (item.ResidenceStreet != null) lst.ResidenceStreet = item.ResidenceStreet;
            if (item.ResidenceHouse != null) lst.ResidenceHouse = item.ResidenceHouse;
            if (item.ResidenceFlat != null) lst.ResidenceFlat = item.ResidenceFlat;
            if (item.WorkSalary != null) lst.WorkSalary = item.WorkSalary;
            dbW.Customers.Context.SubmitChanges();
        }

        /**/
        public Currency CurrencyGetItem(int? CurrencyID)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);
            Currency item = new Currency();

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            item = (from v in dbR.Currencies where v.CurrencyID == CurrencyID select v).FirstOrDefault();
            return item;
        }
        /**/
        public Office OficcesGetItem(int OfficceID)
        {
            //dbdataDataContext db = new dbdataDataContext(connectionString);

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Office item = new Office();
            item = (from v in dbR.Offices where v.ID == OfficceID select v).FirstOrDefault();
            return item;
        }
        /**/
        public Branch BranchGetItem(int BranchID)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Branch item = new Branch();
            item = (from v in dbR.Branches where v.ID == BranchID select v).FirstOrDefault();
            return item;
        }
        /**/
        public City CityGetItem(int? CityID)
        {
            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            City item = new City();
            item = (from v in dbR.Cities where v.CityID == CityID select v).FirstOrDefault();
            return item;
        }
        /*****/
        public RequestsUsersRole RequestsUsersRoleGetItem(int UserID)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            RequestsUsersRole item = new RequestsUsersRole();
            item = (from v in dbRWZ.RequestsUsersRoles where v.UserID == UserID select v).FirstOrDefault();
            return item;
        }
        /*****/
        public Users2 UsersGetItem(int UserID)
        {
            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringZ);
            Users2 item = new Users2();
            item = (from v in dbRWZ.Users2s where v.UserID == UserID select v).FirstOrDefault();
            return item;
        }

    }
}