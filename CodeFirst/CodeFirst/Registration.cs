using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class Registration
    {
        public int RegistrationID { get; set; }
        public int EventID { get; set; }
        public int AthleteID { get; set; }
        public Place? Place { get; set; }

        public virtual Event Event { get; set; }
        public virtual Athlete Athlete { get; set; }
    }
}
