using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace praktika_demo
{
    internal class DbHandler
    {
        private string connString;
        public SqlConnection conn;
        public SqlDataAdapter a;
        public DataTable dt;
        private SqlCommandBuilder commandBuilder;
        private int UserId;
        private int userAccess;
        public DbHandler(int uId, int userAccess) 
        {
            userAccess = userAccess;
            UserId = uId;
            connString = $"Data Source=!!!!!!!!!!!!;Initial Catalog=!!!!!!!!!!;Integrated Security=True;";
            conn = new SqlConnection(connString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public List<object> login(string l, string p)
        {
            //here
            string comm = $"SELECT id, access FROM Users WHERE login = '{l}' AND password = '{p}'";
            SqlCommand req = new SqlCommand(comm, conn);
            SqlDataReader reader = req.ExecuteReader();

            List<object> userData = new List<object>();
            if (reader.Read())
            {  
                UserId = reader.GetInt32(0);
                userAccess = reader.GetInt32(1);

                userData.Add(UserId);
                userData.Add(userAccess);

                return userData;
            }
            else { return userData; }
        }

        public DataTable GetDataTable(int accessLevel)
        {
            dt = new DataTable();
            string comm = "";
            if (accessLevel>0)
            //here
            {
                comm = $"SELECT * FROM products WHERE [Исполнитель] = {UserId}";
            }

            else {
                comm = $"SELECT * FROM products";
            }

            SqlCommand req = new SqlCommand(comm, conn);
            a = new SqlDataAdapter(req);
            commandBuilder = new SqlCommandBuilder(a);
            a.Fill(dt);
            return dt;
        }

        public void Update()
        {
            try
            {
                if (userAccess < 1)
                { 
                    a.Update(dt);
                }
                else
                {
                    MessageBox.Show("Недостаточно прав");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Произошла ошибка: {e.ToString()}");
            }
        }
    }
}
