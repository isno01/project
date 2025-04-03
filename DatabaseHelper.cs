using MySql.Data.MySqlClient;
using System;

public class DatabaseHelper
{
    private string connectionString = "Server=localhost;Database=StudentInfoDB;User=root;Password=;";

    // Insert a new student record into the database
    public void InsertStudent(string firstName, string lastName, string middleName, int houseNo, string brgyName, string municipality, string province, string region, string country, string birthdate, int age, string studContactNo, string emailAddress, string guardianFirstName, string guardianLastName, string hobbies, string nickname, int courseId, int yearId)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var query = "INSERT INTO StudentRecordTB (firstName, lastName, middleName, houseNo, brgyName, municipality, province, region, country, birthdate, age, studContactNo, emailAddress, guardianFirstName, guardianLastName, hobbies, nickname, courseId, yearId) " +
                        "VALUES (@firstName, @lastName, @middleName, @houseNo, @brgyName, @municipality, @province, @region, @country, @birthdate, @age, @studContactNo, @studEmail, @guardianFirstName, @guardianLastName, @hobbies, @nickname, @courseId, @yearId)";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@middleName", middleName);
                cmd.Parameters.AddWithValue("@houseNo", houseNo);
                cmd.Parameters.AddWithValue("@brgyName", brgyName);
                cmd.Parameters.AddWithValue("@municipality", municipality);
                cmd.Parameters.AddWithValue("@province", province);
                cmd.Parameters.AddWithValue("@region", region);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@birthdate", birthdate);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@studContactNo", studContactNo);
                cmd.Parameters.AddWithValue("@studEmail", emailAddress);
                cmd.Parameters.AddWithValue("@guardianFirstName", guardianFirstName);
                cmd.Parameters.AddWithValue("@guardianLastName", guardianLastName);
                cmd.Parameters.AddWithValue("@hobbies", hobbies);
                cmd.Parameters.AddWithValue("@nickname", nickname);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                cmd.Parameters.AddWithValue("@yearId", yearId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // Fetch all students from the database
    public void ShowStudents()
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var query = "SELECT * FROM StudentRecordTB";
            using (var cmd = new MySqlCommand(query, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["studentId"]}, Name: {reader["firstName"]} {reader["lastName"]}, Course: {reader["courseId"]}, Year: {reader["yearId"]}");
                    }
                }
            }
        }
    }
}
