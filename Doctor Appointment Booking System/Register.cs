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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            PId.KeyPress += PId_KeyPress;
            PPhone.KeyPress += PPhone_KeyPress;
            PName.KeyPress += PName_KeyPress;
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(PUser.Text.Length < 5)
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
                    MessageBox.Show("Registration Successful");
                    Con.Close();

                    // Clear text fields after successful registration
                    PUser.Clear();
                    PPass.Clear();
                    PPhone.Clear();
                    PId.Clear();
                    PName.Clear();
                    DateTime defaultDob = DateTime.Today.AddYears(-18);
                    PGen.SelectedItem = null;


                    // Redirect to login form
                    Login loginForm = new Login();
                    loginForm.Show();
                    this.Hide(); // Hide the current registration form
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
