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
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Doctor_Appointment_Booking_System
{
    public partial class doctors : Form
    {
        public doctors()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            DisplayDoc();
            dIDno.KeyPress += dIDno_KeyPress;
            dPhone.KeyPress += dPhone_KeyPress;
            dName.KeyPress += dName_KeyPress;

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayDoc()
        {

            Con.Open();
            string Query = "Select * From DoctorTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            Con.Close();

        }
        private void doctors_Load(object sender, EventArgs e)
        {

        }

        private void dAdd_Click(object sender, EventArgs e)
        {
            if (dUser.Text.Length < 5)
            {
                MessageBox.Show("Username should be at least 5 characters long.");
            }
            else if (dPass.Text.Length < 8)
            {
                MessageBox.Show("Password should be at least 8 characters long.");
            }
            else if (dPhone.Text.Length != 10)
            {
                MessageBox.Show("Phone number should be exactly 10 digits long.");
            }
            else
            {
                try
                {
                    Con.Open();

                    // Check if username already exists
                    SqlCommand checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorTb1 WHERE DocUsername = @dUser", Con);
                    checkUserCmd.Parameters.AddWithValue("@dUser", dUser.Text);
                    int userCount = (int)checkUserCmd.ExecuteScalar();
                    if (userCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose a different one.");
                        dUser.Clear(); // Clear the username textbox
                        return;
                    }

                    // Check if ID number already exists
                    SqlCommand checkIdCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorTb1 WHERE DocIDNo = @dIDno", Con);
                    checkIdCmd.Parameters.AddWithValue("@dIDno", dIDno.Text);
                    int idCount = (int)checkIdCmd.ExecuteScalar();
                    if (idCount > 0)
                    {
                        MessageBox.Show("ID number already exists.");
                        dIDno.Clear(); // Clear the ID number textbox
                        return;
                    }

                    // Check if phone number already exists
                    SqlCommand checkPhoneCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorTb1 WHERE DocPhone = @dPhone", Con);
                    checkPhoneCmd.Parameters.AddWithValue("@dPhone", dPhone.Text);
                    int phoneCount = (int)checkPhoneCmd.ExecuteScalar();
                    if (phoneCount > 0)
                    {
                        MessageBox.Show("Phone number already exists.");
                        dPhone.Clear(); // Clear the phone number textbox
                        return;
                    }

                    // Check if password already exists
                    SqlCommand checkPassCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorTb1 WHERE DocPass = @dPass", Con);
                    checkPassCmd.Parameters.AddWithValue("@dPass", dPass.Text);
                    int passCount = (int)checkPassCmd.ExecuteScalar();
                    if (passCount > 0)
                    {
                        MessageBox.Show("Password already exists. Please choose a different one.");
                        dPass.Clear(); // Clear the password textbox
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("insert into DoctorTb1(DocUsername,DocSpec,DocName,DocIDNo,DocDOB,DocGen,DocPhone,DocPass)values(@dUser,@dSpec,@dName,@dIDno,@dDOB,@dGen,@dPhone,@dPass)", Con);
                    cmd.Parameters.AddWithValue("@dUser", dUser.Text);
                    cmd.Parameters.AddWithValue("@dSpec", dSpec.Text);
                    cmd.Parameters.AddWithValue("@dName", dName.Text);
                    cmd.Parameters.AddWithValue("@dIDno", dIDno.Text);
                    cmd.Parameters.AddWithValue("@dDOB", dDob.Text);
                    cmd.Parameters.AddWithValue("@dGen", dGen.Text);
                    cmd.Parameters.AddWithValue("@dPhone", dPhone.Text);
                    cmd.Parameters.AddWithValue("@dPass", dPass.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Added");
                    Con.Close();

                    // Clear text fields after successful registration
                    dUser.Clear();
                    dPass.Clear();
                    dPhone.Clear();
                    dSpec.SelectedItem = null;
                    dName.Clear();
                    dIDno.Clear();
                    DateTime defaultDOB = DateTime.Today.AddYears(-18);
                    dGen.SelectedItem = null;
                    DisplayDoc();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    // Ensure connection is closed even if an exception occurs
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }

        }

        private void dDel_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Doctor");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from DoctorTb1 where DocID=@DKey", Con);

                    cmd.Parameters.AddWithValue("@DKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Deleted");
                    Con.Close();
                    DisplayDoc();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void dUpd_Click(object sender, EventArgs e)
        {
            if (dUser.Text.Length < 5)
            {
                MessageBox.Show("Username should be at least 5 characters long.");
            }
            else if (dPass.Text.Length < 8)
            {
                MessageBox.Show("Password should be at least 8 characters long.");
            }
            else if (dPhone.Text.Length != 10)
            {
                MessageBox.Show("Phone number should be exactly 10 digits long.");
            }
            else if (dUser.Text == "" || dSpec.Text == "" || dName.Text == "" || dIDno.Text == "" || dDob.Text == "" || dGen.Text == "" || dPhone.Text == "" || dPass.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();

                    // Check if username already exists
                    SqlCommand checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorTb1 WHERE DocUsername = @dUser AND DocID != @DKey", Con);
                    checkUserCmd.Parameters.AddWithValue("@dUser", dUser.Text);
                    checkUserCmd.Parameters.AddWithValue("@DKey", Key);
                    int userCount = (int)checkUserCmd.ExecuteScalar();
                    if (userCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose a different one.");
                        dUser.Clear(); // Clear the username textbox
                        return;
                    }

                    // Check if ID number already exists
                    SqlCommand checkIdCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorTb1 WHERE DocIDNo = @dIDno AND DocID != @DKey", Con);
                    checkIdCmd.Parameters.AddWithValue("@dIDno", dIDno.Text);
                    checkIdCmd.Parameters.AddWithValue("@DKey", Key);
                    int idCount = (int)checkIdCmd.ExecuteScalar();
                    if (idCount > 0)
                    {
                        MessageBox.Show("ID number already exists.");
                        dIDno.Clear(); // Clear the ID number textbox
                        return;
                    }

                    // Check if phone number already exists
                    SqlCommand checkPhoneCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorTb1 WHERE DocPhone = @dPhone AND DocID != @DKey", Con);
                    checkPhoneCmd.Parameters.AddWithValue("@dPhone", dPhone.Text);
                    checkPhoneCmd.Parameters.AddWithValue("@DKey", Key);
                    int phoneCount = (int)checkPhoneCmd.ExecuteScalar();
                    if (phoneCount > 0)
                    {
                        MessageBox.Show("Phone number already exists.");
                        dPhone.Clear(); // Clear the phone number textbox
                        return;
                    }

                    // Check if password already exists
                    SqlCommand checkPassCmd = new SqlCommand("SELECT COUNT(*) FROM DoctorTb1 WHERE DocPass = @dPass AND DocID != @DKey", Con);
                    checkPassCmd.Parameters.AddWithValue("@dPass", dPass.Text);
                    checkPassCmd.Parameters.AddWithValue("@DKey", Key);
                    int passCount = (int)checkPassCmd.ExecuteScalar();
                    if (passCount > 0)
                    {
                        MessageBox.Show("Password already exists. Please choose a different one.");
                        dPass.Clear(); // Clear the password textbox
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("update DoctorTb1 set DocUsername=@dUser,DocSpec=@dSpec,DocName=@dName,DocIDNo=@dIDno,DocDOB=@dDOB,DocGen=@dGen,DocPhone=@dPhone,DocPass=@dPass where DocID=@DKey", Con);
                    cmd.Parameters.AddWithValue("@dUser", dUser.Text);
                    cmd.Parameters.AddWithValue("@dSpec", dSpec.Text);
                    cmd.Parameters.AddWithValue("@dName", dName.Text);
                    cmd.Parameters.AddWithValue("@dIDno", dIDno.Text);
                    cmd.Parameters.AddWithValue("@dDOB", dDob.Text);
                    cmd.Parameters.AddWithValue("@dGen", dGen.Text);
                    cmd.Parameters.AddWithValue("@dPhone", dPhone.Text);
                    cmd.Parameters.AddWithValue("@dPass", dPass.Text);
                    cmd.Parameters.AddWithValue("@DKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Updated");
                    Con.Close();
                    DisplayDoc();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    // Ensure connection is closed even if an exception occurs
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
        }
        int Key = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex >= 0 && e.RowIndex<dataGridView1.Rows.Count)
            {
                dUser.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                dSpec.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                dName.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                dIDno.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                dDob.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                dGen.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                dPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                dPass.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                if (dUser.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

         private void dIDno_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters like backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void dPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters like backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }
        private void dName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, space, and control characters like backspace
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                DisplayDoc();
                return;
            }


            DataTable filteredTable = ((DataTable)dataGridView1.DataSource).Clone();
            foreach (DataRow row in ((DataTable)dataGridView1.DataSource).Rows)
            {

                if (row["DocName"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row);
                }
                else if (row["DocUsername"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row);
                }
            }


            dataGridView1.DataSource = filteredTable;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
           patients obj = new patients();
            obj.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
           Doctorp2 obj = new Doctorp2();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ListViewForm obj = new ListViewForm();
            obj.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Reports obj = new Reports();
            obj.Show();
            this.Hide();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
