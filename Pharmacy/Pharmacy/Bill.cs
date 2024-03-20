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

namespace Pharmacy
{
    public partial class Bill : Form
    {
        public Bill()
        {
            InitializeComponent();
            comboShow();
            comboShowEmp();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True");
        public decimal total;
        public void back() 
        {
            DialogResult dialogResult = MessageBox.Show("Do You Want to Close this Form.This Will Clear All the Data of the Current Form...", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
            }
        }

        public void comboShow()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select [Product I.D] from Product", con);
                SqlDataReader sdr = cmd.ExecuteReader();
                
                while (sdr.Read())
                {
                    comboBox1.Items.Add(sdr["Product I.D"]);
                }
                sdr.Close();
                

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        
        public void comboShowEmp()
        {
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select [Employee I.D] from Employee where Designation = 'Admin' ", con);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                while (sdr1.Read())
                {
                    comboBox2.Items.Add(sdr1["Employee I.D"]);
                }
                sdr1.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        

        public void addItem()
        {

            if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text)||
                string.IsNullOrWhiteSpace(textBox6.Text)
                )  
                
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }
            DataTable dt = (DataTable)dataGridView1.DataSource;

            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("Product I.D");
                dt.Columns.Add("Product Name");
                dt.Columns.Add("Category");
                dt.Columns.Add("Price");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Total");
            }
            DataRow dr = dt.NewRow();
            dr["Product I.D"] = comboBox1.Text;
            dr["Product Name"] = textBox2.Text;
            dr["Category"] = textBox6.Text;
            dr["Price"] = textBox4.Text;
            dr["Quantity"] = textBox1.Text;
            dr["Total"] = int.Parse(textBox1.Text) * float.Parse(textBox4.Text);
            dt.Rows.Add(dr);

            dataGridView1.DataSource = dt;
            totalAmount();

        }

        public void totalAmount()
        {
            total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                decimal value;
                if (decimal.TryParse(row.Cells["Total"].Value.ToString(), out value))
                {
                    total += value;
                }
            }

            label4.Text = total.ToString();

        }

        public void delete()
        {
            int rowIndex;
            if (!int.TryParse(textBox5.Text, out rowIndex))
            {
                MessageBox.Show("Please enter a valid row index.");
                return;
            }
            rowIndex -= 1;

            if (rowIndex < 0 || rowIndex >= dataGridView1.Rows.Count)
            {
                MessageBox.Show("Invalid row index.");
                return;
            }

            dataGridView1.Rows.RemoveAt(rowIndex);
            totalAmount();
            textBox5.Clear();

        }

        public void Insert()
        {
            
                try
                {
                    con.Open();
                    string status = "Pending";
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Order] ([Order date], [Company I.D], [Employee I.D], Status, Amount) VALUES (@OrderDate, @CompanyID, @EmployeeID, @Status, @Price); SELECT SCOPE_IDENTITY();", con);
                    cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CompanyID", int.Parse(textBox3.Text));
                    cmd.Parameters.AddWithValue("@EmployeeID", int.Parse(comboBox2.Text));
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Price", total);
                    int orderID = Convert.ToInt32(cmd.ExecuteScalar());



                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        int productID = Convert.ToInt32(row.Cells["Product I.D"].Value);
                        //string productName = row.Cells["Product Name"].Value.ToString();
                        decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                        int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);


                        SqlCommand cmd2 = new SqlCommand("INSERT INTO [Order_Detail] ([Order I.D], [Product I.d], Price, Quantity) VALUES (@OrderID, @ProductID, @Price, @Quantity)", con);
                        cmd2.Parameters.AddWithValue("@OrderID", orderID);
                        cmd2.Parameters.AddWithValue("@ProductID", productID);
                        cmd2.Parameters.AddWithValue("@Price", price);
                        cmd2.Parameters.AddWithValue("@Quantity", quantity);
                        cmd2.ExecuteNonQuery();
                    }
                    con.Close();
                    MessageBox.Show("Insert Successfully...!!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            back();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addItem();
            textBox1.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT [Product Name], [T.P], [Company I.D],Category FROM [Product] WHERE [Product I.D] = @id", con);
                cmd.Parameters.AddWithValue("@id", comboBox1.SelectedItem);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    textBox2.Text = sdr["Product Name"].ToString();
                    textBox4.Text = sdr["T.P"].ToString();
                    textBox3.Text = sdr["Company I.D"].ToString();
                    textBox6.Text = sdr["Category"].ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) { MessageBox.Show("Please Enter an Employee I.D"); }
            else if (dataGridView1.Rows.Count == 0) { MessageBox.Show("Please Enter Any Product"); }
            else
            {
                Insert();
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            delete();
        }
    }
}
