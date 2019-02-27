using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public abstract class Base
    {

    }
         
    public class Visit  : Base
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public Patient Patient { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal TotalAmount { get; set; }

        public Visit()
        {
            VisitDate = DateTime.Today;
        }
    }

    public class Patient : Base
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
