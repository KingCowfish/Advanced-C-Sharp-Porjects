using InsuranceMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly string connectionString = @"Data Source=DESKTOP-191KR0A\SQLEXPRESS;Initial Catalog=Insurance;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetQuote()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Quote(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, string carMake, string carModel, int carYear,
            int ticket, int dUI, bool coverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(carMake)
                || string.IsNullOrEmpty(carModel))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                double r = 50;

                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > today.AddYears(-age)) age--;

                if ((age) < 25 && (age) > 18)
                {
                     r = r + 25;
                }
                else if((age) < 18)
                {
                    r = r + 100;
                }
                else if((age) > 100)
                {
                    r = r + 25;
                }

                if (carYear < 2000)
                {
                    r = r + 25;
                }
                else if(carYear > 2015)
                {
                    r = r + 25;
                }

                if (carMake == "Porsche" && carModel != "911 Carrera")
                {
                    r = r + 25;
                }
                else if(carMake == "Porsche" && carModel == "911 Carrera")
                {
                    r = r + 50;
                }

                r = r + (10 * ticket);

                if (dUI > 0)
                {
                    r = r + (r * .25);
                }

                if (!coverage)
                {
                    r = r + (r * .50);
                }

                double rate = r;

                string queryString = @"INSERT INTO Quotes (FirstName, LastName, EmailAddress, DateOfBirth, CarMake, CarModel, CarYear, Ticket, DUI, Coverage, Rate) VALUES
                                        (@FirstName, @LastName, @EmailAddress, @DateOfBirth, @CarMake, @CarModel, @CarYear, @Ticket, @DUI, @Coverage, @Rate)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                    command.Parameters.Add("@DateOfBirth", SqlDbType.Date);
                    command.Parameters.Add("@CarMake", SqlDbType.VarChar);
                    command.Parameters.Add("@CarModel", SqlDbType.VarChar);
                    command.Parameters.Add("@CarYear", SqlDbType.Int);
                    command.Parameters.Add("@Ticket", SqlDbType.Int);
                    command.Parameters.Add("@DUI", SqlDbType.Int);
                    command.Parameters.Add("@Coverage", SqlDbType.Bit);
                    command.Parameters.Add("@Rate", SqlDbType.Money);

                    command.Parameters["@FirstName"].Value = firstName;
                    command.Parameters["@LastName"].Value = lastName;
                    command.Parameters["@EmailAddress"].Value = emailAddress;
                    command.Parameters["@DateOfBirth"].Value = dateOfBirth;
                    command.Parameters["@CarMake"].Value = carMake;
                    command.Parameters["@CarModel"].Value = carModel;
                    command.Parameters["@CarYear"].Value = carYear;
                    command.Parameters["@Ticket"].Value = ticket;
                    command.Parameters["@DUI"].Value = dUI;
                    command.Parameters["@Coverage"].Value = coverage;
                    command.Parameters["@Rate"].Value = rate;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return View("Success");
            }
        }

        public ActionResult Admin()
        {
            string queryString = @"SELECT Id, FirstName, LastName, EmailAddress, Rate from Quotes";
            List<Applicant> applicants = new List<Applicant>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    var applicant = new Applicant();
                    applicant.Id = Convert.ToInt32(reader["Id"]);
                    applicant.FirstName = reader["FirstName"].ToString();
                    applicant.LastName = reader["LastName"].ToString();
                    applicant.EmailAddress = reader["EmailAddress"].ToString();
                    applicant.Rate = Convert.ToInt32(reader["Rate"]);
                    applicants.Add(applicant);

                }
            }


            return View();
        }
    }
}