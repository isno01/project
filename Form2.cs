using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentManagementSystem
{
    public class StudentPage_Individual : Form
    {
        private string connectionString = "server=localhost; database=StudentInfoDB; user=root;";

        private int studentId;

        private Label lblFullName, lblAddress, lblContact, lblCourse, lblGuardian, lblHobbies;
        private TextBox txtFullName, txtAddress, txtContact, txtCourse, txtGuardian, txtHobbies;
        private Button btnClose;

        public StudentPage_Individual(int studentId)
        {
            this.studentId = studentId;
            InitializeForm();
            LoadStudentDetails();
        }

        private void InitializeForm()
        {
            this.Text = "Student Details";
            this.Size = new System.Drawing.Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Labels
            lblFullName = new Label { Text = "Full Name:", Location = new System.Drawing.Point(20, 20) };
            lblAddress = new Label { Text = "Address:", Location = new System.Drawing.Point(20, 60) };
            lblContact = new Label { Text = "Contact No.:", Location = new System.Drawing.Point(20, 100) };
            lblCourse = new Label { Text = "Course:", Location = new System.Drawing.Point(20, 140) };
            lblGuardian = new Label { Text = "Guardian:", Location = new System.Drawing.Point(20, 180) };
            lblHobbies = new Label { Text = "Hobbies:", Location = new System.Drawing.Point(20, 220) };

            // Textboxes (Read-Only)
            txtFullName = new TextBox { Location = new System.Drawing.Point(120, 20), Width = 250, ReadOnly = true };
            txtAddress = new TextBox { Location = new System.Drawing.Point(120, 60), Width = 250, ReadOnly = true };
            txtContact = new TextBox { Location = new System.Drawing.Point(120, 100), Width = 250, ReadOnly = true };
            txtCourse = new TextBox { Location = new System.Drawing.Point(120, 140), Width = 250, ReadOnly = true };
            txtGuardian = new TextBox { Location = new System.Drawing.Point(120, 180), Width = 250, ReadOnly = true };
            txtHobbies = new TextBox { Location = new System.Drawing.Point(120, 220), Width = 250, ReadOnly = true };

            // Close Button
            btnClose = new Button { Text = "Close", Location = new System.Drawing.Point(150, 270), Width = 100 };
            btnClose.Click += (sender, e) => this.Close();

            // Add Controls
            this.Controls.Add(lblFullName); this.Controls.Add(txtFullName);
            this.Controls.Add(lblAddress); this.Controls.Add(txtAddress);
            this.Controls.Add(lblContact); this.Controls.Add(txtContact);
            this.Controls.Add(lblCourse); this.Controls.Add(txtCourse);
            this.Controls.Add(lblGuardian); this.Controls.Add(txtGuardian);
            this.Controls.Add(lblHobbies); this.Controls.Add(txtHobbies);
            this.Controls.Add(btnClose);
        }

        private void LoadStudentDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT studentId, 
                               CONCAT(firstName, ' ', middleName, ' ', lastName) AS fullName, 
                               CONCAT(houseNo, ' ', brgyName, ', ', municipality, ', ', province, ', ', region, ', ', country) AS fullAddress,
                               studContactNo, 
                               emailAddress, 
                               CONCAT(guardianFirstName, ' ', guardianLastName) AS guardian, 
                               hobbies,
                               (SELECT courseName FROM CourseTB WHERE CourseTB.courseId = StudentRecordTB.courseId) AS course 
                        FROM StudentRecordTB 
                        WHERE studentId = @studentId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@studentId", studentId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtFullName.Text = reader["fullName"].ToString();
                            txtAddress.Text = reader["fullAddress"].ToString();
                            txtContact.Text = reader["studContactNo"].ToString();
                            txtCourse.Text = reader["course"].ToString();
                            txtGuardian.Text = reader["guardian"].ToString();
                            txtHobbies.Text = reader["hobbies"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No student found with the provided ID.");
                        }
                    }
                }
                catch (MySqlException sqlEx)
                {
                    MessageBox.Show($"Database error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }  // <-- This closing brace was missing
    }
}
