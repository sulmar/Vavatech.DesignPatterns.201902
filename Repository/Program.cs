using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class Program
    {
        static void Main(string[] args)
        {

            IPatientRepository patientRepository = new DbPatientRepository();

            var patients = patientRepository.Get();



            //MyContext context = new MyContext();

            //var patients = context.Patients.ToList();

        }
    }



    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender? Gender { get; set; }
        public string Pesel { get; set; }

        public int Age => (int)DateTime.Today.Subtract(Birthday).TotalDays / 365;

    }


    public enum Gender
    {
        Female,
        Male
    }


    public class Visit
    {
        public DateTime VisitDate { get; set; }
        public Patient Patient { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Amount => (decimal)this.Duration.TotalHours * this.UnitPrice;

        public decimal DiscountedAmount => this.Amount - this.Discount;

        public decimal Discount { get; set; }
        public string DiscountName { get; set; }

    }
}
