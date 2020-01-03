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

                customer.Estimate = 50m;
                //Gets customerAge
                int customerAge = DateTime.Now.Year - Convert.ToDateTime(customer.BirthDate).Year;
                //If the user is under 25, add $25 to the monthly total.
                if (customerAge < 25 && customerAge > 17)
                {
                    customer.Estimate += 25m;
                }
                //If the user is under 18, add $100 to the monthly total.
                else if (customerAge < 18)
                {
                    customer.Estimate += 100m;
                }
                //If the user is over 100, add $25 to the monthly total.
                else if (customerAge > 100)
                {
                    customer.Estimate += 25m;
                }
                //If the car's year is before 2000, add $25 to the monthly total.
                if (customer.CarYear < 2000)
                {
                    customer.Estimate += 25m;
                }
                //If the car's year is after 2015, add $25 to the monthly total.
                else if (customer.CarYear > 2015)
                {
                    customer.Estimate += 25;
                }
                //If the car's Make is a Porsche, add $25 to the price.
                if (customer.CarMake.ToLower() == "porsche")
                {
                    customer.Estimate += 25m;
                }
                //If the car's Make is a Porsche and its model is a 911 Carrera, add an additional $25 to the price.
                if (customer.CarMake.ToLower() == "porsche" && customer.CarModel.ToLower() == "911")
                {
                    customer.Estimate += 25m;
                }
                //Add $10 to the monthly total for every speeding ticket the user has.
                for (int i = 0; i < customer.Tickets; i++)
                {
                    customer.Estimate += 10m;
                }
                //If the user has ever had a DUI, add 25 % to the total.
                if (customer.DUI == "true")
                {
                    customer.Estimate *= 1.25m;
                }
                //If it's full coverage, add 50% to the total.
                if (customer.Coverage == "full")
                {
                    customer.Estimate *= 1.5m;
                }

                




                //adding and saving new class object to database
                db.Customers.Add(customer);
                db.SaveChanges();
                ViewBag.Message = customer;

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