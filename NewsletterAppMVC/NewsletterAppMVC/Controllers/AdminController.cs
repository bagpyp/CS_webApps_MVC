using NewsletterAppMVC.Models;
using NewsletterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsletterAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            ////ADO.NET flown BEGIN, replaced below using Entity Framework (EF)
            //
            ////init list of signups*
            //var signups = new List<NewsletterSignUp>();
            ////string to feed reader signups
            //string queryString =
            //    @"
            //        SELECT * FROM SignUps
            //    ";

            ////resource mgmt
            //using (var connection = new SqlConnection(_connectionString))
            //{
            //    var command = new SqlCommand(queryString, connection);
            //    connection.Open();
            //    //init a reader object to loop through records
            //    var reader = command.ExecuteReader();

            //    //enter loop
            //    while (reader.Read())
            //    {
            //        //*init a signup to add to list "up 2 levels"
            //        var signup = new NewsletterSignUp()
            //        {
            //            Id = Convert.ToInt32(reader["Id"]),
            //            FirstName = reader["FirstName"].ToString(),
            //            LastName = reader["LastName"].ToString(),
            //            EmailAddress = reader["EmailAddress"].ToString(),
                          //only included SSN to represent why to use view models, removed later
            //            SocialSecurityNumber = reader["SocialSecurityNumber"].ToString()
            //        };
            //        signups.Add(signup);
            //    }
            //    connection.Close();
            //}
            //
            ////ADO.NET flow END
            using (var db = new NewsletterEntities())
            {
                //daaaang!  look at how much easier that is with EF

                ////one way to filter using lambda
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList();

                //using LINQ
                var signups =
                    (
                        from c in db.SignUps
                        where c.Removed == null
                        select c
                    ).ToList();

                var signupVms = new List<SignUpVm>();
                foreach (var signup in signups)
                {
                    var signupVm = new SignUpVm()
                    {
                        Id = signup.Id,
                        FirstName = signup.FirstName,
                        LastName = signup.LastName,
                        EmailAddress = signup.EmailAddress
                    };
                    signupVms.Add(signupVm);
                }
                return View(signupVms);
            }
        }
        public ActionResult Unsubscribe(int id)
        {
            using (var db = new NewsletterEntities())
            {
                var signup = db.SignUps.Find(id);
                signup.Removed = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}