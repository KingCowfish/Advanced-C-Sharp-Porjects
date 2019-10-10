using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceMVC.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public int CarYear { get; set; }
        public int Ticket { get; set; }
        public int DUI { get; set; }
        public bool Coverage { get; set; }
        public int Rate { get; set; }
    }
}