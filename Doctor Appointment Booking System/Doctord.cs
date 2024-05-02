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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Doctor_Appointment_Booking_System
{
    public partial class Doctord : Form
    {
        private string loggedInUsername;
        private string loggedInPassword;
        public Doctord(string username, string password)
        {
            InitializeComponent();
            loggedInUsername = username;
            loggedInPassword = password;
            DisplayDoctorData(loggedInUsername, loggedInPassword);
            dataGridView2.CellClick += dataGridView2_CellClick;
        }

        public void DisplayDoctorData(string username, string password)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    Con.Open();

                    string Query = "SELECT * FROM DoctorTb1 WHERE DocUsername=@Username AND DocPass=@Password";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");

        int Key = 0;

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (DUser.Text.Length < 5)
            {
                MessageBox.Show("Username should be at least 5 characters long.");
            }
            else if (DPass.Text.Length < 8)
            {
                MessageBox.Show("Password should be at least 8 characters long.");
            }
            else if (PPhone.Text.Length != 10)
            {
                MessageBox.Show("Phone number should be exactly 10 digits long.");
            }
            else if (DUser.Text == "" || PSpec.Text == "" || PName.Text == "" || PId.Text == "" || PDob.Text == "" || PGen.Text == "" || PPhone.Text == "" || DPass.Text == "")
            {
                MessageBox.Show("Missing Info");
            }
            else
            {
                try
                {
                    Con.Open();

                   
                    



                    SqlCommand cmd = new SqlCommand("update DoctorTb1 set DocUsername=@DUser,DocSpec=@PSpec,DocName=@PName,DocIDNo=@PId,DocDOB=@PDob,DocGen=@PGen,DocPhone=@PPhone,DocPass=@DPass where DocID=@DKey", Con);
                    cmd.Parameters.AddWithValue("@DUser", DUser.Text);
                    cmd.Parameters.AddWithValue("@PSpec", PSpec.Text);
                    cmd.Parameters.AddWithValue("@PName", PName.Text);
                    cmd.Parameters.AddWithValue("@PId", PId.Text);
                    cmd.Parameters.AddWithValue("@PDob", PDob.Text);
                    cmd.Parameters.AddWithValue("@PGen", PGen.Text);
                    cmd.Parameters.AddWithValue("@PPhone", PPhone.Text);
                    cmd.Parameters.AddWithValue("@DPass", DPass.Text);
                    cmd.Parameters.AddWithValue("@DKey", Key);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Doctor Updated");
                    DisplayDoctorData(loggedInUsername, loggedInPassword);

                    Con.Close();


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0 && e.RowIndex < dataGridView2.Rows.Count)
            {

                groupBox1.Text = "";
                groupBox1.Text = "                                          Doctor Details\n\n" + "Login Details  " +
                    "     " + "\n**********************************************" + "\n\n    " +
                    "Username:  " + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "\n\n\n    Password:  " + dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString() +
                     "                  \n\n\nPersonal Details  " +
                    "     " + "\n**********************************************" + "\n\n    " +
                    "Doctor Name  :" + dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString() + "\n\n   ID Number:  " + dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString() + "\n\n   DOB:  " + dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString() +
                    "\n\n   Gender  :" + dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString() + "\n\n   Phone Number  :" + dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();










                DUser.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                PSpec.Text= dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                PName.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                PId.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                PDob.Text = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                PGen.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                PPhone.Text = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
                DPass.Text = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();

                DUser.ReadOnly = true;
                DPass.ReadOnly = true;

                if (DUser.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            patientd doctorForm = new patientd(loggedInUsername, loggedInPassword);
            doctorForm.Show();
            this.Hide();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Appointmentsadd doctorForm = new Appointmentsadd(loggedInUsername, loggedInPassword);
            doctorForm.Show();
            this.Hide();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Presc doctorForm = new Presc(loggedInUsername, loggedInPassword);
            doctorForm.Show();
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

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
