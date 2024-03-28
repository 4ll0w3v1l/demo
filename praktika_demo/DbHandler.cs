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
        private string server;
        private string database;
        private string connString;
        public SqlConnection conn;
        public SqlDataAdapter a;
        public DataTable dt;
        private SqlCommandBuilder commandBuilder;
        private int UserId;
        private int userAccess;
        public DbHandler(int uId, int userAccess) 
        {
            this.userAccess = userAccess;
            this.UserId = uId;
            this.server = "DESKTOP-GJ3MKIR\\SQLEXPRESS";
            this.database = "praktika";
            this.connString = $"Server={server};Database={database};Integrated Security=True;";
            this.conn = new SqlConnection(this.connString);

            if (this.conn.State == ConnectionState.Closed)
            {
                this.conn.Open();
            }
        }

        public List<object> login(string l, string p)
        {
            string comm = $"SELECT id, access FROM Users WHERE login = '{l}' AND password = '{p}'";
            SqlCommand req = new SqlCommand(comm, this.conn);
            SqlDataReader reader = req.ExecuteReader();

            List<object> userData = new List<object>();
            if (reader.Read())
            {
                
                this.UserId = reader.GetInt32(0);
                this.userAccess = reader.GetInt32(1);

                userData.Add(this.UserId);
                userData.Add(this.userAccess);

                return userData;
            }
            else { return userData; }
        }

        public DataTable GetDataTable()
        {
            this.dt = new DataTable();
            string comm = "";
            if (false)
            {
                comm = $"SELECT * FROM products WHERE [Исполнитель] = {UserId}";
            }

            else {
                comm = $"SELECT * FROM products";
            }

            SqlCommand req = new SqlCommand(comm, this.conn);
            this.a = new SqlDataAdapter(req);
            this.commandBuilder = new SqlCommandBuilder(a);
            this.a.Fill(this.dt);
            return this.dt;
        }

        public void Update()
        {
            try
            {
                if (this.userAccess < 1)
                { 
                    this.a.Update(this.dt);
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
