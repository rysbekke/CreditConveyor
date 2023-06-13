using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace Zamat.Repository
{
    public class UserRepository
    {
        //public List<User> GetUsers()
        //{
        //    List<User> users = new List<User>();

        //    string sql = "SELECT * FROM public.\"User\"";
        //    //using (SqlConnection conn = new SqlConnection(Storage.ConnectionString))
        //    using (NpgsqlConnection conn = new NpgsqlConnection(Storage.ConnectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Невозможно установить соединение с базой данных!", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return users;
        //        }
        //        NpgsqlCommand command = new NpgsqlCommand(sql, conn);
        //        using (NpgsqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                users.Add(new User
        //                {
        //                    Id = (int)reader[0],
        //                    Login = reader[1].ToString(),
        //                    Password = reader[2].ToString(),
        //                    UserFIO = reader[3].ToString(),
        //                    Address = reader[4].ToString(),
        //                    Phone = reader[5].ToString(),
        //                    Access = reader[6].ToString()
        //                });
        //            }
        //        }
        //    }

        //    return users;
        //}

        //public void Delete(User user)
        //{
        //    string sql = "DELETE FROM Users WHERE Id=" + user.Id;
        //    using (NpgsqlConnection conn = new NpgsqlConnection(Storage.ConnectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Невозможно установить соединение с базой данных!", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        NpgsqlCommand command = new NpgsqlCommand(sql, conn);
        //        command.ExecuteScalar();
        //    }
        //}

        //public void AddOrUpdate(User user)
        //{
        //    if (GetUsers().Any(u => u.Id == user.Id))
        //    {
        //        string sql = "UPDATE Users SET " +
        //                     "Login='" + user.Login +
        //                     "',Password='" + user.Password +
        //                     "',UserFIO='" + user.UserFIO +
        //                     "',Address='" + user.Address +
        //                     "',Phone='" + user.Phone +
        //                     "',Access='" + user.Access +
        //                     "' WHERE Id=" + user.Id;
        //        using (NpgsqlConnection conn = new NpgsqlConnection(Storage.ConnectionString))
        //        {
        //            try
        //            {
        //                conn.Open();
        //            }
        //            catch
        //            {
        //                MessageBox.Show("Невозможно установить соединение с базой данных!", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }
        //            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
        //            command.ExecuteScalar();
        //        }
        //    }
        //    else
        //    {
        //        string sql = "INSERT INTO Users (Login, Password, UserFIO, Address, Phone, Access) Values ('" +
        //                     user.Login + "','" +
        //                     user.Password + "','" +
        //                     user.UserFIO + "','" +
        //                     user.Address + "','" +
        //                     user.Password + "','" +
        //                     user.Access + "')";
        //        using (NpgsqlConnection conn = new NpgsqlConnection(Storage.ConnectionString))
        //        {
        //            try
        //            {
        //                conn.Open();
        //            }
        //            catch
        //            {
        //                MessageBox.Show("Невозможно установить соединение с базой данных!", "Ошибка подключения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return;
        //            }
        //            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
        //            command.ExecuteScalar();
        //        }
        //    }
        //}
    }
}