using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {

            CalculatorTest();
        }

        private static void CalculatorTest()
        {
            Patient patient = new Patient
            {
                Birthday = DateTime.Parse("1950-04-01"),
                FirstName = "Ryszard",
                Gender = Gender.Male,
            };

            Visit visit = new Visit
            {
                Patient = patient,
                VisitDate = DateTime.Now,
                Duration = TimeSpan.FromHours(1),
                UnitPrice = 150
            };

            CalculatorBase calculator1 = new GenderCalculator(0.9m);
            CalculatorBase calculator2 = new AgeCalculator();

            calculator2.Calculate(visit);

            Console.WriteLine(visit.DiscountedAmount);



        }
    }


    public class Patient
    {
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public Gender? Gender { get; set; }

        public int Age => (int) DateTime.Today.Subtract(Birthday).TotalDays / 365;

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

    public class AgeCalculator : CalculatorBase
    {
        protected override bool CanDiscount(Visit visit)
        {
            return visit.Patient.Age > 60;
        }

        protected override decimal Discount(Visit visit)
        {
            return visit.Amount;
        }
    }

    public class GenderCalculator : CalculatorBase
    {
        private decimal ratio;

        public GenderCalculator(decimal ratio)
        {
            this.ratio = ratio;
        }

        protected override bool CanDiscount(Visit visit)
        {
            return visit.Patient.Gender == Gender.Female;
        }

        protected override decimal Discount(Visit visit)
        {
            return visit.Amount * ratio;
        }
    }

    public abstract class CalculatorBase
    {
        public void Calculate(Visit visit)
        {
            if (CanDiscount(visit))
            {
                visit.Discount = Discount(visit);
                visit.DiscountName = this.ToString();
            }
        }

        protected abstract bool CanDiscount(Visit visit);
        protected abstract decimal Discount(Visit visit);
    }


    /*
    public class AgeCalculator : Calculator
    {
        public override decimal Calculate(Visit visit)
        {
            if (visit.Patient.Age > 60)
            {
                return 0;
            }
            else
            {
                return (decimal)visit.Duration.TotalHours * visit.UnitPrice;
            }
        }
    }

    public class GenderCalculator : Calculator
    {
        private decimal ratio;

        public GenderCalculator(decimal ratio)
        {
            this.ratio = ratio;
        }

        public override decimal Calculate(Visit visit)
        {
            if (visit.Patient.Gender == Gender.Female)
            {
                return (decimal)visit.Duration.TotalHours * visit.UnitPrice * ratio;
            }
    
            else
            {
                return (decimal)visit.Duration.TotalHours * visit.UnitPrice;
            }
        }
    }

    public abstract class Calculator
    {
        public abstract decimal Calculate(Visit visit);
    }


    */
}
