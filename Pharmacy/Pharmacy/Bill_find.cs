using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Pharmacy.Form2;
using System.Data.SqlClient;

namespace Pharmacy
{
    public partial class Bill_find : Form
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;
        private string query;
        DataTable billData;
        DataTable customerData;
        DataTable empData;
        DataTable productData;
        public Bill_find()
        {
            InitializeComponent();
            this.billData = new DataTable();
            this.customerData = new DataTable();
            this.empData = new DataTable();
            this.productData = new DataTable();
            string connectionString = "Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        private void Bill_find_Load(object sender, EventArgs e)
        {
            loadFormData();
        }

        private void loadFormData()
        {

            try
            {
                billData.Clear();
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                connection.Open();
                string billQuery = "SELECT * FROM Bill";
                command.CommandText = billQuery;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(this.billData);
                dataGridView1.DataSource = billData;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bill data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private static string billID = string.Empty;
        private static string billdetailID = string.Empty;
        private static string billAmount = string.Empty;
        private void deleteRow()
        {
            try
            {
                string billdetailID = "0";
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    billdetailID = row.Cells["[Bd_id]"].Value.ToString();
                    billID = row.Cells["[Bill I.D]"].Value.ToString();
                    billAmount = row.Cells["Total"].Value.ToString();
                    totalBillAmount = row.Cells["Amount"].Value.ToString();

                    connection.Open();
                    command.Parameters.Clear();
                    string billDetailquery = @"delete from [Bill_Detail] where [Bd_id] = " + billdetailID;
                    command.CommandText = billDetailquery;
                    command.ExecuteNonQuery();
                    connection.Close();

                    if (totalBillAmount != string.Empty && billAmount != string.Empty)
                        updatedAmount = decimal.Parse(totalBillAmount) - decimal.Parse(billAmount);
                    else
                        updatedAmount = 0;
                    recordUpdate();

                    MessageBox.Show("Record Deleted Successfully");
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }

        private static string totalBillAmount = string.Empty;
        private static decimal updatedAmount = 0;
        private void recordUpdate()
        {
            connection.Open();
            command.Parameters.Clear();
            string billQuery = @"Update Bill set Amount= " + updatedAmount + " where [Bill I.D] = " + billID;
            command.CommandText = billQuery;
            command.ExecuteNonQuery();
            connection.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //deleteRow();
            //loadFormData();
            deletefacts();
        }
        string billrefID = string.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            billrefID = textBox1.Text;
            try
            {
                billData.Clear();
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                connection.Open();
                string billQuery = "SELECT * FROM Bill where [Bill I.D] = " + billrefID;
                command.CommandText = billQuery;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(this.billData);
                dataGridView1.DataSource = billData;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading bill data: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            fr2.Show();
            this.Hide();
                
        }
        public void deletefacts()
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Please Provide I.D for Delete operation", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand command1 = new SqlCommand("DELETE FROM [Bill] WHERE [Bill I.D] = @b_id", connection);
                    command1.Parameters.AddWithValue("@b_id", int.Parse(textBox1.Text));
                    int workdone = command1.ExecuteNonQuery();
                    if (workdone > 0)
                    {
                        MessageBox.Show("Bill deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Bill not found.");
                    }
                    connection.Close();
                    loadFormData();
                    textBox1.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            loadFormData();
        }
    }
}
