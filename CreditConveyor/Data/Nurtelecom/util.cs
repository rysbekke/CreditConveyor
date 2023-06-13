using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace СreditСonveyor.Data.Nurtelecom
{
    public class util
    {
        ///////////////////////////////////////////////////////////////////////
        public static void update_user_password(int us_id, string unencypted)
        {
            Random random = new Random();
            int salt = random.Next(10000, 99999);

            //string encrypted = util.encrypt_string_using_MD5(unencypted + Convert.ToString(salt));

            string sql = "update users set us_password = N'$en', us_salt = $salt where us_id = $id";

            //sql = sql.Replace("$en", encrypted);
            sql = sql.Replace("$salt", Convert.ToString(salt));
            sql = sql.Replace("$id", Convert.ToString(us_id));

            СreditСonveyor.Data.Nurtelecom.dbutil.execute_nonquery(sql);
        }

        ///////////////////////////////////////////////////////////////////////
        public SqlDataReader getRequests(int us_id, string unencypted)
        {

            string sql = @"update users set us_most_recent_login_datetime = getdate() where us_id = $us";
            sql = sql.Replace("$us", Convert.ToString(us_id));
            SqlDataReader sdr = dbutil.execute_reader(sql, 0);
            return sdr;
        }
        ///////////////////////////////////////////////////////////////////////
    }
}