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
    public partial class Bill_Detail_User : Form
    {
        public Bill_Detail_User()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=Pharmacy;Integrated Security=True");
        public void finddetail()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select [Bill_Detail].[Bd_id],[Bill_Detail].[Bill I.D],[Bill_Detail].[Product I.D],Product.[Product Name],Product.Category,[Bill_Detail].[Price],[Bill_Detail].[Quantity],[Bill_Detail].[Total] from [Bill_Detail] inner join Product on [Bill_Detail].[Product I.D]=Product.[Product I.d] where [Bill I.D] = @id", con);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            finddetail();
        }
    }
}
