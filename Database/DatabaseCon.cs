using System;
using System.Data.SQLite;
using System.IO;

namespace DotNetAssignment.Database
{
    public class DatabaseCon
    {

        public SQLiteConnection myConnection;


        public DatabaseCon()
        {
            myConnection = new SQLiteConnection("Data Source = ./Database/DataBase.sqlite3");
            if (!File.Exists("./Database/DataBase.sqlite3"))
            {
                SQLiteConnection.CreateFile("DataBase.sqlite3");
                Console.WriteLine("file has been created!");
            }
            else
            {
                Console.WriteLine("file is already there!");
            }

        }
        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }
        public void CloseConnetion()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
