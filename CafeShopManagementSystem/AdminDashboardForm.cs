using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace CafeShopManagementSystem
{
    public partial class AdminDashboardForm : UserControl
    {

        static string conn = ConfigurationManager.ConnectionStrings["myDatabaseConnection"].ConnectionString;
        SqlConnection connect = new SqlConnection(conn);

        //SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hasit\OneDrive\Documents\car.mdf;Integrated Security=True;Connect Timeout=30");

        public AdminDashboardForm()
        {
            InitializeComponent();
            //btnDisplayCounts_Click(this, EventArgs.Empty);

            displayTotalCashier();
            displayTotalCustomers();
            displayTodaysIncome();
            displayTotalIncome();
        }

        //public void refreshData()
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke((MethodInvoker)refreshData);
        //        return;
        //    }
        //    displayTotalCashier();
        //    displayTotalCustomers();
        //    displayTotalIncome();
        //    displayTodaysIncome();
        //}

        public void displayTotalCashier()
        {
            if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT COUNT(id) FROM users WHERE role = @role AND status = @status";

                    using(SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@role", "Cashier");
                        cmd.Parameters.AddWithValue("@status", "Active");

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            //MessageBox.Show("Total cashiers: " + count);
                            dashboard_TC.Text = count.ToString();
                        }

                        reader.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTotalCustomers()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT COUNT(id) FROM customers";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            dashboard_TCust.Text = count.ToString();
                            Console.WriteLine("Customer Count: " +count);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTodaysIncome()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT sum(total_price) FROM customers WHERE DATE = @date";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        DateTime today = DateTime.Today;
                        string getToday = today.ToString("yyyy-MM-dd");

                        cmd.Parameters.AddWithValue("@date", getToday);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            object value = reader[0];

                            if (value != DBNull.Value)
                            {
                                float count = Convert.ToSingle(reader[0]);
                                dashboard_TI.Text = "$" + count.ToString("0.00");
                            }
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayTotalIncome()
        {

            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT SUM(total_price) FROM customers";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int count = Convert.ToInt32(reader[0]);
                            dashboard_TIn.Text = "$" + count.ToString("0.00");
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed connection: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        //--------------------------------------
        private int getTotalCashier()
        {
            string selectData = "SELECT COUNT(id) FROM users WHERE role = @role AND status = @status";

            using (SqlCommand cmd = new SqlCommand(selectData, connect))
            {
                cmd.Parameters.AddWithValue("@role", "Cashier");
                cmd.Parameters.AddWithValue("@status", "Active");

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int getTotalCustomers()
        {
            string selectData = "SELECT COUNT(id) FROM customers";

            using (SqlCommand cmd = new SqlCommand(selectData, connect))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private float getTodaysIncome()
        {
            string selectData = "SELECT SUM(total_price) FROM customers WHERE DATE = @date";

            using (SqlCommand cmd = new SqlCommand(selectData, connect))
            {
                cmd.Parameters.AddWithValue("@date", DateTime.Today.ToString("yyyy-MM-dd"));

                object value = cmd.ExecuteScalar();

                if (value != DBNull.Value)
                {
                    return Convert.ToSingle(value);
                }
                else
                {
                    return 0;
                }
            }
        }

        private float getTotalIncome()
        {
            string selectData = "SELECT SUM(total_price) FROM customers";

            using (SqlCommand cmd = new SqlCommand(selectData, connect))
            {
                object value = cmd.ExecuteScalar();

                if (value != DBNull.Value)
                {
                    return Convert.ToSingle(value);
                }
                else
                {
                    return 0;
                }
            }
        }

        //private void btnDisplayCounts_Click(object sender, EventArgs e)
        //{
        //    int totalCashier = 0;
        //    int totalCustomers = 0;
        //    float todaysIncome = 0;
        //    float totalIncome = 0;

        //    if (connect.State == ConnectionState.Closed)
        //    {
        //        connect.Open();

        //        totalCashier = getTotalCashier();
        //        totalCustomers = getTotalCustomers();
        //        todaysIncome = getTodaysIncome();
        //        totalIncome = getTotalIncome();

        //        connect.Close();
        //    }

        //    string message = "Total Cashiers: " + totalCashier + Environment.NewLine +
        //                     "Total Customers: " + totalCustomers + Environment.NewLine +
        //                     "Today's Income: $" + todaysIncome.ToString("0.00") + Environment.NewLine +
        //                     "Total Income: $" + totalIncome.ToString("0.00");

        //    MessageBox.Show(message, "Count Values", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}
    }

}
