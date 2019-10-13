using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                Console.WriteLine("Adding new athletes...");

                var athlete = new Athlete
                {
                    FirstName = "Usain",
                    LastName = "Bolt",
                    RegistrationDate = DateTime.Parse(DateTime.Today.ToString())
                };

                context.Athletes.Add(athlete);

                var athlete1 = new Athlete
                {
                    FirstName = "Jesse",
                    LastName = "Owens",
                    RegistrationDate = DateTime.Parse(DateTime.Today.ToString())
                };

                context.Athletes.Add(athlete1);
                context.SaveChanges();

                var athletes = (from a in context.Athletes
                                orderby a.FirstName
                                select a).ToList<Athlete>();

                Console.WriteLine("Retrieve all athlete from the database:");

                foreach (var ath in athletes)
                {
                    string name = ath.FirstName + " " + ath.LastName;
                    Console.WriteLine("ID: {0}, Name: {1}", ath.ID, name);
                }

                Console.ReadLine();
            }
        }
    }
}
