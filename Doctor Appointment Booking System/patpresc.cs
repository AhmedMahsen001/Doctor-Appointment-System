using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Doctor_Appointment_Booking_System
{
    public partial class patpresc : Form
    {
        private string loggedInUsername;
        private string loggedInPassword;
        public patpresc(string username, string password)
        {
            InitializeComponent();
            DisplayPresc();
            dataGridView1.CellClick += dataGridView1_CellClick;
            loggedInUsername = username;
            loggedInPassword = password;
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayPresc()
        {


            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    Con.Open();
                    string Query = "Select * From PrescriptionTb1";
                    SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                    SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if a row is clicked, not the header or empty space
            {
                string bullet = "\u2022"; // Unicode character for bullet

                groupBox1.Text = "\r\n                    \n" + "\r\nPrescription\n\n" + "****************************************************\n" + DateTime.Today.Date + "\n\n" +
                        bullet + " Doctor: " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "                                " +
                        bullet + " Patient: " + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "\n\n\n" + "                                " +
                        bullet + "Symptoms: " + dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + "\n\n\n" +
                        bullet + "Medication: " + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() + "\n\n\n" +
                        bullet + "Cost:Ksh " + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            }




        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
           patientp homeForm = new patientp(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox9_Click(object sender, EventArgs e)
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Feedback homeForm = new Feedback(loggedInUsername, loggedInPassword);
            homeForm.Show();
            this.Hide();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Logout Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide(); // Hide 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
