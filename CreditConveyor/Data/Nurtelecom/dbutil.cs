using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace СreditСonveyor.Data.Nurtelecom
{
    public class dbutil
    {
        ///////////////////////////////////////////////////////////////////////
        public static SqlDataReader execute_reader(string sql, CommandBehavior behavior)
        {
            //if (Util.get_setting("LogSqlEnabled", "1") == "1")
            //{
            //    Util.write_to_log("sql=\n" + sql);
            //}

            SqlConnection conn = get_sqlconnection();
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return cmd.ExecuteReader(behavior | CommandBehavior.CloseConnection);
                }
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        ///////////////////////////////////////////////////////////////////////
        //public static SqlDataReader execute_reader2(string sql, CommandBehavior behavior)
        //{
        //    //if (Util.get_setting("LogSqlEnabled", "1") == "1")
        //    //{
        //    //    Util.write_to_log("sql=\n" + sql);
        //    //}

        //    SqlConnection conn = get_sqlconnection();
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sql, conn))
        //        {
        //            //return cmd.ExecuteReader(behavior | CommandBehavior.CloseConnection);
        //            sqldat
        //            using (SqlDataReader reader = cmd.ExecuteReader(behavior | CommandBehavior.CloseConnection))
        //            {
        //                //List<Request> customers = reader.AutoMap<CustomerDTO>().ToList();
        //                List<Request> customers = reader.Select<Request>(Request.FromDataReader)
        //                             .ToList();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        conn.Close();
        //        throw;
        //    }
        //}


        //using (DataReader reader = ...)
        //{    
        //    List<CustomerDTO> customers = reader.AutoMap<CustomerDTO>().ToList();
        //}
        ///////////////////////////////////////////////////////////////////////
        public static void execute_nonquery(string sql)
        {

            //if (Util.get_setting("LogSqlEnabled", "1") == "1")
            //{
            //    Util.write_to_log("sql=\n" + sql);
            //}

            using (SqlConnection conn = get_sqlconnection())
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close(); // redundant, but just to be clear
            }
        }

        ///////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////
        public static SqlConnection get_sqlconnection()
        {

            //string connection_string = Util.get_setting("ConnectionString", "MISSING CONNECTION STRING");
            string connection_string = ConfigurationManager.ConnectionStrings["DosCredobankConnectionStringOBW"].ToString();
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();
            return conn;
        }

        ///////////////////////////////////////////////////////////////////////
    }
}