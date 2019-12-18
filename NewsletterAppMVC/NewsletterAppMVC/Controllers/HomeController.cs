using NewsletterAppMVC.Models;
using NewsletterAppMVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NewsletterAppMVC.Controllers
{
    public class HomeController : Controller
    {
        ////was using this conenction string for ADO.NET flow, no need with EF
        //private readonly string _connectionString =
        //    @"
        //        Data Source=(localdb)\MSSQLLocalDB;
        //        Initial Catalog = Newsletter; 
        //        Integrated Security = True; 
        //        Connect Timeout = 30; 
        //        Encrypt=False;
        //        TrustServerCertificate=False;
        //        ApplicationIntent=ReadWrite;
        //        MultiSubnetFailover=False
        //    ";

        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress)
        {
            //very simple server validation
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                //EF flow
                using (var db = new NewsletterEntities())
                {
                    var signup = new SignUp()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        EmailAddress = emailAddress
                    };

                    db.SignUps.Add(signup);
                    db.SaveChanges();
                }
                    ////ADO.NET flow BEGIN (replaced above by Entity Framework (EF))

                    //string queryString =
                    //    @"
                    //        INSERT INTO SignUps 
                    //            (FirstName, LastName, EmailAddress)
                    //        VALUES
                    //            (@FirstName, @LastName, @EmailAddress)
                    //    ";

                    //using (var connection = new SqlConnection(_connectionString))
                    //{
                    //    var command = new SqlCommand(queryString, connection);

                    //    //adding parameters to command
                    //    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    //    command.Parameters["@FirstName"].Value = firstName;
                    //    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    //    command.Parameters["@LastName"].Value = lastName;
                    //    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                    //    command.Parameters["@EmailAddress"].Value = emailAddress;

                    //    connection.Open();
                    //    command.ExecuteNonQuery();
                    //    connection.Close();

                    //}

                    ////ADO.NET flow END

                    return View("Success");
            }
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}