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
    public partial class Bill_Details : Form
    {
        public Bill_Details()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True");
        public void finddetail()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select [Order_Detail].[Od_id],[Order_Detail].[Order I.D],[Order_Detail].[Product I.D],Product.[Product Name],Product.Category,[Order_Detail].[Price],[Order_Detail].[Quantity],[Order_Detail].[Total] from [Order_Detail] inner join Product on [Order_Detail].[Product I.D]=Product.[Product I.d] where [Order I.D] = @id", con);
                cmd.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                if (dataTable.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dataTable;
                }
                else
                {
                    dataGridView1.DataSource = dataTable;
                    MessageBox.Show("No data found..", "Missing Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            finddetail();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }
}
