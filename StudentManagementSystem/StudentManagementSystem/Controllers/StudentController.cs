using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private string _connectionString = 
            @"
                Data Source=DESKTOP-HVJJI34\SQLEXPRESS;
                Initial Catalog = School; Integrated Security = True; 
                Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            ";

        // GET: Student
        public ActionResult Index()
        {
            string queryString = "SELECT * FROM Students";
            var students = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var student = new Student()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString()
                    };
                    students.Add(student);
                }
                connection.Close();
            }
            return View(students);
        }

        // SET: Student
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Student student)
        {
            string queryString =
                @"
                    INSERT INTO 
                        Students (FirstName, LastName)
                    VALUES 
                        (@FirstName, @LastName)
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                command.Parameters.Add("@LastName", SqlDbType.VarChar);

                command.Parameters["@FirstName"].Value = student.FirstName;
                command.Parameters["@LastName"].Value = student.LastName;

                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            string queryString =
                @"
                    SELECT * FROM Students WHERE Id = @id
                ";
            Student student = new Student();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                //add @id as a parameter to our sql query
                command.Parameters.Add("@id", SqlDbType.Int);
                //set it's value to the parameter of the Details function
                command.Parameters["@id"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    student.Id = Convert.ToInt32(reader["Id"]);
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                }

                connection.Close();
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            string queryString = 
                @"
                    UPDATE Students
                    SET
                        FirstName= @FirstName,
                        LastName = @LastName
                    WHERE
                        Id = @Id;
                ";
            Student student = new Student();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(queryString, connection);

                //add @id as a parameter to our sql query
                command.Parameters.Add("@id", SqlDbType.Int);
                //set it's value to the parameter of the Details function
                command.Parameters["@id"].Value = id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    student.Id = Convert.ToInt32(reader["Id"]);
                    student.FirstName = reader["FirstName"].ToString();
                    student.LastName = reader["LastName"].ToString();
                }

                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}