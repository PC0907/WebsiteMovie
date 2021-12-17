using DotNetAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;
using System.Data.SQLite;
using DotNetAssignment.Database;



namespace DotNetAssignment.Services
{
    public class UserServices
    {
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool RegisterUser(User user)
        {
            bool valid = false;
            DatabaseCon con = new DatabaseCon();
            try
            {
                string mail = Convert.ToString(user.mail);
                string pass = Convert.ToString(user.hashpassword);
                string hashed = ComputeSha256Hash(pass);
                string username = Convert.ToString(user.username);
                con.OpenConnection();

                string query = "INSERT INTO user ('username', 'hashpassword', 'mail') VALUES(@username, @hashpassword, @mail)";

                SQLiteCommand comm = new SQLiteCommand(query, con.myConnection);
                comm.Parameters.AddWithValue("@username", username);
                comm.Parameters.AddWithValue("@hashpassword", hashed);
                comm.Parameters.AddWithValue("@mail", mail);
                var res = comm.ExecuteNonQuery();
                valid = true;
            }
            catch (Exception) {
               Console.WriteLine("This account already exists");
            }
            finally
            {
                con.CloseConnetion();
            }
            return valid;

        }
        public static bool LoginUser(User user)
        {
            bool valid = false;
            DatabaseCon con = new DatabaseCon();
            try
            {
                string mail = Convert.ToString(user.mail);
                string pass = Convert.ToString(user.hashpassword);
                string hashed = ComputeSha256Hash(pass);
                con.OpenConnection();
                string query = "SELECT * FROM user WHERE mail=@mail";
                SQLiteCommand myCommand = new SQLiteCommand(query, con.myConnection);
                myCommand.Parameters.AddWithValue("@mail", mail);
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {

                    while (result.Read())
                    {
                        string pass_db = Convert.ToString(result["hashpassword"]);
                        if (pass_db == hashed)
                        {
                            valid = true;
                        }
                    }
                }
            }

            catch (Exception)
            {
                Console.WriteLine("Some error occurred");
            }
            finally
            {
                con.CloseConnetion();
            }
            return valid;
        }
    
        public static async Task<User>GetUser(string username)
        {
            DatabaseCon con = new DatabaseCon();
            User user = new User();
            try
            {
                con.OpenConnection();
                string query = "SELECT * FROM user WHERE username=@username";
                SQLiteCommand myCommand = new SQLiteCommand(query, con.myConnection);
                myCommand.Parameters.AddWithValue("@username", username);
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        await Task.Delay(500);
                        user.username = result["username"].ToString();
                        user.mail = result["mail"].ToString();

                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Some error occurred");
            }
            finally
            {
                con.CloseConnetion();
            }
            return user;
        }

    }
}
