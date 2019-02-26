using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class MyContext
    {
        public IList<Visit> Visits { get;  }
        public IList<Patient> Patients { get; set; }

        public void SaveChanges()
        {

        }
    }
}
