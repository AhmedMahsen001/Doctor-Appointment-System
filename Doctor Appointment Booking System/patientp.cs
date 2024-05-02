using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doctor_Appointment_Booking_System
{
    public partial class patientp : Form
    {
        private string loggedInUsername;
        private string loggedInPassword;
        public patientp(string username, string password)
        {
            InitializeComponent();
            loggedInUsername = username;
            loggedInPassword = password;
            DisplayPatientData(loggedInUsername, loggedInPassword);
            dataGridView1.CellClick += dataGridView1_CellClick;
            
        }
        
        public void DisplayPatientData(string username, string password)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    Con.Open();

                    string Query = "SELECT * FROM PatientTb1 WHERE PatUsername=@Username AND PatPass=@Password";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");

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
            else if (PUser.Text == "" || PName.Text == "" || textBox6.Text == "" || PDob.Text == "" || PGen.Text == "" || PPhone.Text == "" || PPass.Text == "")
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
                    checkIdCmd.Parameters.AddWithValue("@PId", textBox6.Text);
                    checkIdCmd.Parameters.AddWithValue("@PKey", Key);
                    int idCount = (int)checkIdCmd.ExecuteScalar();
                    if (idCount > 0)
                    {
                        MessageBox.Show("ID number already exists.");
                        textBox6.Clear(); // Clear the ID number textbox
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

                    

                    SqlCommand cmd = new SqlCommand("update PatientTb1 set PatUsername=@PUser,PatName=@PName,PatIDNo=@PId,PatDOB=@PDob,PatGen=@PGen,PatPhone=@PPhone,PatPass=@PPass where PatID=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PUser", PUser.Text);
                    cmd.Parameters.AddWithValue("@PName", PName.Text);
                    cmd.Parameters.AddWithValue("@PId", textBox6.Text);
                    cmd.Parameters.AddWithValue("@PDob", PDob.Text);
                    cmd.Parameters.AddWithValue("@PGen", PGen.Text);
                    cmd.Parameters.AddWithValue("@PPhone", PPhone.Text);
                    cmd.Parameters.AddWithValue("@PPass", PPass.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show("Patient Updated");
                    DisplayPatientData(loggedInUsername, loggedInPassword);
                    
                    Con.Close();
                    

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {

                groupBox1.Text = "";
                groupBox1.Text = "                                          Patient Details\n\n" + "Login Details  " +
                    "     " + "\n**********************************************" + "\n\n    " +
                    "Username:  " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "\n\n\n    Password:  " + dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() +
                     "                  \n\n\nPersonal Details  " +
                    "     " + "\n**********************************************" + "\n\n    " +
                    "Patient Name:  " + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "\n\n   ID Number:  " + dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + "\n\n   DOB:  " + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() +
                    "\n\n   Gender:  " + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() + "\n\n   Phone Number:  " + dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();










                PUser.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                PName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                PDob.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                PGen.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                PPhone.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                PPass.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

                PUser.ReadOnly = true;
                PPass.ReadOnly = true;

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
       

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide(); // Hide 
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
           
        }

        private void label11_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Doctorp homeForm = new Doctorp(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Doctorp homeForm = new Doctorp(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Appointmentsbook homeForm = new Appointmentsbook(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void label100_Click(object sender, EventArgs e)
        {
            Appointmentsbook homeForm = new Appointmentsbook(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Feedback homeForm = new Feedback(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Feedback homeForm = new Feedback(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            patpresc homeForm = new patpresc(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }
    }

}
