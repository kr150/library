using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace library
{
    static class Program
    {
        public static SqlConnection connection;
        public static SqlCommand command;
        //private static SqlDataAdapter da;
        //private static System.Data.DataSet ds;
        public static SqlDataReader thisSqlDataReader;
        public static string connString = "Data Source=LAPTOP-VP6H47AQ;Initial Catalog=library;Integrated Security=true";//用windows身份登录

        //ExecuteNonQuery执行语句
        [STAThread]
        static void Main()
        {
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            connection = new SqlConnection(connString);
            connection.Open();
            command = connection.CreateCommand();
            //为指定的command对象执行DataReader
            //thisSqlDataReader = command.ExecuteReader();
            
            Application.Run(new Form1());
        }
    }
}
