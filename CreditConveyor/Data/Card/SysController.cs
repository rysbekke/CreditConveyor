using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Zamat;

namespace СreditСonveyor.Data.Card
{
    
    public class SysController
    {
        static string connectionStringR = ConfigurationManager.ConnectionStrings["ConnectionStringOBR"].ToString();
        static string connectionStringW = ConfigurationManager.ConnectionStrings["ConnectionStringOBW"].ToString();
        static string connectionStringRWZ = ConfigurationManager.ConnectionStrings["ConnectionStringZ"].ToString();

        public void CustomerAddItem(Customer newItem)
        {

            dbdataDataContext dbW = new dbdataDataContext(connectionStringW);
            dbW.Customers.InsertOnSubmit(newItem);
            dbW.Customers.Context.SubmitChanges();
        }

        public Customer CustomerGetItem(int CustomerID)
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Customer item = new Customer();
            item = (from v in dbR.Customers where v.CustomerID == CustomerID select v).FirstOrDefault();
            return item;
        }

        public void CustomerUpdItem(Customer item)
        {

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
            if ((item.Codename != null) || (item.Codename != "")) lst.Codename = item.Codename;
            if ((item.EMail != null) || (item.EMail != "")) lst.EMail = item.EMail;
            dbW.Customers.Context.SubmitChanges();
        }

        /**/
        public Currency CurrencyGetItem(int? CurrencyID)
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Currency item = new Currency();
            item = (from v in dbR.Currencies where v.CurrencyID == CurrencyID select v).FirstOrDefault();
            return item;
        }
        /**/
        public Office OficcesGetItem(int OfficceID)
        {

            dbdataDataContext dbR = new dbdataDataContext(connectionStringR);
            Office item = new Office();
            item = (from v in dbR.Offices where v.ID == OfficceID select v).FirstOrDefault();
            return item;
        }
        /**/
        public Branch BranchGetItem(int BranchID)
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            Branch item = new Branch();
            item = (from v in dbRWZ.Branches where v.ID == BranchID select v).FirstOrDefault();
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

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            RequestsUsersRole item = new RequestsUsersRole();
            item = (from v in dbRWZ.RequestsUsersRoles where v.UserID == UserID select v).FirstOrDefault();
            return item;
        }
        /*****/
        public Users2 UsersGetItem(int UserID)
        {

            dbdataDataContext dbRWZ = new dbdataDataContext(connectionStringRWZ);
            Users2 item = new Users2();
            item = (from v in dbRWZ.Users2s where v.UserID == UserID select v).FirstOrDefault();
            return item;
        }


    }
}