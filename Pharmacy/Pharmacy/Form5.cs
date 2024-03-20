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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            checkloadgrid();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H3TPEQS;Initial Catalog=db2;Integrated Security=True");
        public void checkloadgrid()
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select A.[Product_Id],A.[Product_Name],A.[Price],A.Stock,A.[Company_Id],B.[Company_Name] from Product A inner join Company_table B on A.[Company_Id]=B.[Company_Id]", con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Product");
                dataGridView1.DataSource = ds.Tables["Product"].DefaultView;
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

        public void insert_check() 
        {
            try 
            {
                con.Open();
                SqlCommand ins_com = new SqlCommand("SELECT COUNT(*) FROM Company_table WHERE Company_Id = @Cid", con);
                ins_com.Parameters.AddWithValue("@Cid", int.Parse(textBox4.Text));
                int count = (int)ins_com.ExecuteScalar();
                
                if (count > 0) 
                {
                    
                    SqlCommand com = new SqlCommand("Insert into Product (Product_Name,Price,Stock,Company_Id) Values (@pname,@price,@stock,@C_id)", con);
                    com.Parameters.AddWithValue("@pname",textBox2.Text);
                    com.Parameters.AddWithValue("@price",float.Parse(textBox3.Text));
                    com.Parameters.AddWithValue("@C_id",int.Parse(textBox4.Text));
                    com.Parameters.AddWithValue("@stock", int.Parse(textBox5.Text));
                    com.ExecuteNonQuery();
                    MessageBox.Show("Insert Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else 
                {
                    MessageBox.Show("Company Is not Registered...!!","Alert");
                    return;
                }
                con.Close();
                checkloadgrid();
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

        public void delete_C_Check() 
        {
            if (textBox4.TextLength == 0)
            {
                MessageBox.Show("Please Provide I.D for Delete Operation...!!", "ALert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                try 
                {
                    con.Open();
                    SqlCommand cmd_d = new SqlCommand("Delete from Company_table where Company_Id=@cid",con);
                    cmd_d.Parameters.AddWithValue("@cid",int.Parse(textBox4.Text));
                    int workdone_d = cmd_d.ExecuteNonQuery();
                    if (workdone_d > 0)
                    {
                        MessageBox.Show("Delete Successfully...!!","Delete");
                    }
                    else 
                    {
                        MessageBox.Show("Company Not Found...!!","Not Found");
                    }
                    con.Close();
                    checkloadgrid();
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
        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insert_check();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            delete_C_Check();
        }
    }
}
