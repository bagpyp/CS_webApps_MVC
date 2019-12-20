using CarInsuranceQuote.Models;
using System;
using System.Web.Mvc;

namespace CarInsuranceQuote.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //passing in user input values
        public ActionResult Estimate(string firstName, string lastName, string email, DateTime birthdate, int carYear,
                                     string carMake, string carModel, string dui, int tickets, string coverage)
        {
            //accessing database
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                //instantiating a class object and assigning it properties based on user input
                var customer = new Customer
                {
                    Id = 1,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    BirthDate = birthdate,
                    CarYear = carYear,
                    CarMake = carMake,
                    CarModel = carModel,
                    DUI = dui,
                    Tickets = tickets,
                    Coverage = coverage
                };

                //adding and saving new class object to database
                db.Customers.Add(customer);
                db.SaveChanges();

                //had to try and catch a validation error from the database
                //had accidentally set DUI data type to varchar(3) ("yes" or "no")
                //and then later routed it to "true" and "false" lol!
                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                //{
                //    Exception raise = dbEx;
                //    foreach (var validationErrors in dbEx.EntityValidationErrors)
                //    {
                //        foreach (var validationError in validationErrors.ValidationErrors)
                //        {
                //            string message = string.Format("{0}:{1}",
                //                validationErrors.Entry.Entity.ToString(),
                //                validationError.ErrorMessage);
                //            // raise a new exception nesting
                //            // the current instance as InnerException
                //            raise = new InvalidOperationException(message, raise);
                //        }
                //    }
                //    throw raise;
                //}
                
            }

            return View("Success");
        }
    }
}