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
namespace Doctor_Appointment_Booking_System
{
    public partial class patients : Form
    {
        public patients()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            DisplayPat();
            PId.KeyPress += PId_KeyPress;
            PPhone.KeyPress += PPhone_KeyPress;
            PName.KeyPress += PName_KeyPress;
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");
        
        private void DisplayPat()
        {

            Con.Open();
            string Query = "Select * From PatientTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            Con.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
                
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                PUser.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                PName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                PId.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                PDob.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                PGen.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                PPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                PPass.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                if (PUser.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
        }

        int Key = 0;
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (PUser.Text.Length < 5)
            {
                MessageBox.Show("Username should be at least 5 characters long.");
            }
            else if (PPass.Text.Length < 8)
            {
                MessageBox.Show("Password should be at least 8 characters long.");
            }
            else if (PPhone.Text.Length != 10)
            {
                MessageBox.Show("Phone number should be exactly 10 digits long.");
            }
            else
            {
                try
                {
                    Con.Open();

                    // Check if username already exists
                    SqlCommand checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM PatientTb1 WHERE PatUsername = @PUser", Con);
                    checkUserCmd.Parameters.AddWithValue("@PUser", PUser.Text);
                    int userCount = (int)checkUserCmd.ExecuteScalar();
                    if (userCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose a different one.");
                        PUser.Clear(); // Clear the username textbox
                        return;
                    }

                    // Check if ID number already exists
                    SqlCommand checkIdCmd = new SqlCommand("SELECT COUNT(*) FROM PatientTb1 WHERE PatIDNo = @PId", Con);
                    checkIdCmd.Parameters.AddWithValue("@PId", PId.Text);
                    int idCount = (int)checkIdCmd.ExecuteScalar();
                    if (idCount > 0)
                    {
                        MessageBox.Show("ID number already exists.");
                        PId.Clear(); // Clear the ID number textbox
                        return;
                    }

                    // Check if phone number already exists
                    SqlCommand checkPhoneCmd = new SqlCommand("SELECT COUNT(*) FROM PatientTb1 WHERE PatPhone = @PPhone", Con);
                    checkPhoneCmd.Parameters.AddWithValue("@PPhone", PPhone.Text);
                    int phoneCount = (int)checkPhoneCmd.ExecuteScalar();
                    if (phoneCount > 0)
                    {
                        MessageBox.Show("Phone number already exists.");
                        PPhone.Clear(); // Clear the phone number textbox
                        return;
                    }

                    // Check if password already exists
                    SqlCommand checkPassCmd = new SqlCommand("SELECT COUNT(*) FROM PatientTb1 WHERE PatPass = @PPass", Con);
                    checkPassCmd.Parameters.AddWithValue("@PPass", PPass.Text);
                    int passCount = (int)checkPassCmd.ExecuteScalar();
                    if (passCount > 0)
                    {
                        MessageBox.Show("Password already exists. Please choose a different one.");
                        PPass.Clear(); // Clear the password textbox
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("INSERT INTO PatientTb1(PatUsername, PatName, PatIDNo, PatDOB, PatGen, PatPhone, PatPass) VALUES(@PUser, @PName, @PId, @PDob, @PGen, @PPhone, @PPass)", Con);
                    cmd.Parameters.AddWithValue("@PUser", PUser.Text);
                    cmd.Parameters.AddWithValue("@PName", PName.Text);
                    cmd.Parameters.AddWithValue("@PId", PId.Text);
                    cmd.Parameters.AddWithValue("@PDob", PDob.Text);
                    cmd.Parameters.AddWithValue("@PGen", PGen.Text);
                    cmd.Parameters.AddWithValue("@PPhone", PPhone.Text);
                    cmd.Parameters.AddWithValue("@PPass", PPass.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Added");
                    Con.Close();

                    // Clear text fields after successful registration
                    PUser.Clear();
                    PPass.Clear();
                    PPhone.Clear();
                    PId.Clear();
                    PName.Clear();
                    DateTime defaultDob = DateTime.Today.AddYears(-18);
                    PGen.SelectedItem = null;
                    DisplayPat();
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

        private void Delbtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select Patient");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from PatientTb1 where PatID=@PKey", Con);
                    
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Deleted");
                    Con.Close();
                    DisplayPat();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Updbtn_Click(object sender, EventArgs e)
        {
            if (PUser.Text.Length < 5)
            {
                MessageBox.Show("Username should be at least 5 characters long.");
            }
            else if (PPass.Text.Length < 8)
            {
                MessageBox.Show("Password should be at least 8 characters long.");
            }
            else if (PPhone.Text.Length != 10)
            {
                MessageBox.Show("Phone number should be exactly 10 digits long.");
            }
            else if (PUser.Text == "" || PName.Text == "" || PId.Text == "" || PDob.Text == "" || PGen.Text == "" || PPhone.Text == "" || PPass.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();

                    // Check if username already exists
                    SqlCommand checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM PatientTb1 WHERE PatUsername = @PUser AND PatID != @PKey", Con);
                    checkUserCmd.Parameters.AddWithValue("@PUser", PUser.Text);
                    checkUserCmd.Parameters.AddWithValue("@PKey", Key);
                    int userCount = (int)checkUserCmd.ExecuteScalar();
                    if (userCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose a different one.");
                        PUser.Clear(); // Clear the username textbox
                        return;
                    }

                    // Check if ID number already exists
                    SqlCommand checkIdCmd = new SqlCommand("SELECT COUNT(*) FROM PatientTb1 WHERE PatIDNo = @PId AND PatID != @PKey", Con);
                    checkIdCmd.Parameters.AddWithValue("@PId", PId.Text);
                    checkIdCmd.Parameters.AddWithValue("@PKey", Key);
                    int idCount = (int)checkIdCmd.ExecuteScalar();
                    if (idCount > 0)
                    {
                        MessageBox.Show("ID number already exists.");
                        PId.Clear(); // Clear the ID number textbox
                        return;
                    }

                    // Check if phone number already exists
                    SqlCommand checkPhoneCmd = new SqlCommand("SELECT COUNT(*) FROM PatientTb1 WHERE PatPhone = @PPhone AND PatID != @PKey", Con);
                    checkPhoneCmd.Parameters.AddWithValue("@PPhone", PPhone.Text);
                    checkPhoneCmd.Parameters.AddWithValue("@PKey", Key);
                    int phoneCount = (int)checkPhoneCmd.ExecuteScalar();
                    if (phoneCount > 0)
                    {
                        MessageBox.Show("Phone number already exists.");
                        PPhone.Clear(); // Clear the phone number textbox
                        return;
                    }

                    // Check if password already exists
                    SqlCommand checkPassCmd = new SqlCommand("SELECT COUNT(*) FROM PatientTb1 WHERE PatPass = @PPass AND PatID != @PKey", Con);
                    checkPassCmd.Parameters.AddWithValue("@PPass", PPass.Text);
                    checkPassCmd.Parameters.AddWithValue("@PKey", Key);
                    int passCount = (int)checkPassCmd.ExecuteScalar();
                    if (passCount > 0)
                    {
                        MessageBox.Show("Password already exists. Please choose a different one.");
                        PPass.Clear(); // Clear the password textbox
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("update PatientTb1 set PatUsername=@PUser,PatName=@PName,PatIDNo=@PId,PatDOB=@PDob,PatGen=@PGen,PatPhone=@PPhone,PatPass=@PPass where PatID=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PUser", PUser.Text);
                    cmd.Parameters.AddWithValue("@PName", PName.Text);
                    cmd.Parameters.AddWithValue("@PId", PId.Text);
                    cmd.Parameters.AddWithValue("@PDob", PDob.Text);
                    cmd.Parameters.AddWithValue("@PGen", PGen.Text);
                    cmd.Parameters.AddWithValue("@PPhone", PPhone.Text);
                    cmd.Parameters.AddWithValue("@PPass", PPass.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Updated");
                    Con.Close();
                    DisplayPat();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void PId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters like backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void PPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters like backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }
        private void PName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, space, and control characters like backspace
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void patients_Load(object sender, EventArgs e)
        {

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

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            doctors obj = new doctors();
            obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            doctors obj = new doctors();
            obj.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Doctorp2 obj = new Doctorp2();
            obj.Show();
            this.Hide();
        }

        private void label100_Click(object sender, EventArgs e)
        {
            Doctorp2 obj = new Doctorp2();
            obj.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Reports obj = new Reports();
            obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Reports obj = new Reports();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox3.Text.Trim(); // Get the search term from the TextBox and remove leading/trailing spaces

            // If the search term is empty, display all rows in the DataGridView
            if (string.IsNullOrEmpty(searchTerm))
            {
                DisplayPat();
                return;
            }

            // Filter the DataTable based on the search term
            DataTable filteredTable = ((DataTable)dataGridView1.DataSource).Clone(); // Clone the DataTable structure
            foreach (DataRow row in ((DataTable)dataGridView1.DataSource).Rows)
            {
                // Check if the Doctor's Name column contains the search term (case-insensitive)
                if (row["PatName"].ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    filteredTable.ImportRow(row); // Add the row to the filtered DataTable
                }
            }

            // Update the DataGridView with the filtered data
            dataGridView1.DataSource = filteredTable;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ListViewForm loginForm = new ListViewForm();
            loginForm.Show();
            this.Hide(); // Hide 
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide(); // Hide 
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
