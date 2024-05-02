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
using System.Collections;
namespace Doctor_Appointment_Booking_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\DoctorAppointmentDb.mdf;Integrated Security=True;Connect Timeout=30");

        public static string Role;
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (comboBoxdrop.SelectedIndex == -1)
            {
                MessageBox.Show("Select Your Position");
            }
            else if (comboBoxdrop.SelectedIndex == 0)
            {
                if (textBoxuser.Text == "" || textBoxpass.Text == "")
                {
                    MessageBox.Show("Enter Both Admin name and Password");
                }
                else if (textBoxuser.Text == "Admin" && textBoxpass.Text == "Password")
                {
                    Role = "Admin";
                    MessageBox.Show("Admin Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Homes obj = new Homes();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Admin Name And Password");
                }
            }
            else if (comboBoxdrop.SelectedIndex == 1)
            {
                if (textBoxuser.Text == "" || textBoxpass.Text == "")
                {
                    MessageBox.Show("Enter Both Doctor name and Password");
                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from DoctorTb1 where DocUsername='" + textBoxuser.Text + "' and DocPass='" + textBoxpass.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Role = "Doctor";
                        MessageBox.Show("Doctor Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Doctord doctorForm = new Doctord(textBoxuser.Text, textBoxpass.Text);
                        doctorForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Doctor Not Found");
                    }
                    Con.Close();
                }
            }
            else
            {
                if (textBoxuser.Text == "" || textBoxpass.Text == "")
                {
                    MessageBox.Show("Enter Both Patient name and Password");
                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from PatientTb1 where PatUsername='" + textBoxuser.Text + "' and PatPass='" + textBoxpass.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Role = "Patient";
                        MessageBox.Show("Patient Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        patientp homeForm = new patientp(textBoxuser.Text, textBoxpass.Text);
                        homeForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Patient Not Found");
                    }
                    Con.Close();
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }
    }
}
