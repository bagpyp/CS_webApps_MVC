using CarInsuranceQuote.Models;
using CarInsuranceQuote.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceQuote.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //accessing database
            using (CarInsuranceEntities db = new CarInsuranceEntities())
            {
                //creating a list of db records and storing as 'customers'
                var customers = db.Customers;
                //creating a list of ViewModel objects
                var customerVms = new List<CustomerVm>();

                foreach (var customer in customers)
                {
                    //creating a ViewModel object and assigning chosen properties to be passed to the view
                    var customerVm = new CustomerVm();
                    customerVm.FirstName = customer.FirstName;
                    customerVm.LastName = customer.LastName;
                    customerVm.Email = customer.Email;
                    customerVm.Estimate = Convert.ToDecimal(customer.Estimate);

                    //moved this logic to home controller

                    ////Start with a base of $50 / month.
                    //customerVm.Estimate = 50m;
                    ////Gets customerAge
                    //int customerAge = DateTime.Now.Year - Convert.ToDateTime(customer.BirthDate).Year;
                    ////If the user is under 25, add $25 to the monthly total.
                    //if (customerAge < 25 && customerAge > 17)
                    //{
                    //    customerVm.Estimate += 25m;
                    //}
                    ////If the user is under 18, add $100 to the monthly total.
                    //else if (customerAge < 18)
                    //{
                    //    customerVm.Estimate += 100m;
                    //}
                    ////If the user is over 100, add $25 to the monthly total.
                    //else if (customerAge > 100)
                    //{
                    //    customerVm.Estimate += 25m;
                    //}
                    ////If the car's year is before 2000, add $25 to the monthly total.
                    //if (customer.CarYear < 2000)
                    //{
                    //    customerVm.Estimate += 25m;
                    //}
                    ////If the car's year is after 2015, add $25 to the monthly total.
                    //else if (customer.CarYear > 2015)
                    //{
                    //    customerVm.Estimate += 25;
                    //}
                    ////If the car's Make is a Porsche, add $25 to the price.
                    //if (customer.CarMake.ToLower() == "porsche")
                    //{
                    //    customerVm.Estimate += 25m;
                    //}
                    ////If the car's Make is a Porsche and its model is a 911 Carrera, add an additional $25 to the price.
                    //if (customer.CarMake.ToLower() == "porsche" && customer.CarModel.ToLower() == "911")
                    //{
                    //    customerVm.Estimate += 25m;
                    //}
                    ////Add $10 to the monthly total for every speeding ticket the user has.
                    //for (int i = 0; i < customer.Tickets; i++)
                    //{
                    //    customerVm.Estimate += 10m;
                    //}
                    ////If the user has ever had a DUI, add 25 % to the total.
                    //if (customer.DUI == "true")
                    //{
                    //    customerVm.Estimate *= 1.25m;
                    //}
                    ////If it's full coverage, add 50% to the total.
                    //if (customer.Coverage == "full")
                    //{
                    //    customerVm.Estimate *= 1.5m;
                    //}



                    //adding new ViewModel object to list
                    customerVms.Add(customerVm);
                }
                //passing ViewModel list to view
                return View(customerVms);
            }
        }
    }
}