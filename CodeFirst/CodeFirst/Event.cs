using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    public class Event
    {
        public int EventID { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
