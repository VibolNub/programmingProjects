using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace VibolNubApp
{
    internal static class Program
    {
        public static string active_user = "" ;
        public static SqlConnection cnn;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            cnn = new SqlConnection();
            //cnn.ConnectionString = System.IO.File.ReadAllText("cnn.txt");
            cnn.ConnectionString = "server=localhost; database=vibolcsharp; user id=sa; password=123456";
            cnn.Open();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());
        }
    }
}
