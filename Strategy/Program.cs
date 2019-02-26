using Strategy.Better;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Tester.Test();


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

            IStrategy strategy = new AgeStrategy(60);

            Calculator calculator = new Calculator(strategy);

            calculator.Calculate(visit);
        }
    }

    public interface IStrategy
    {
        bool CanDiscount(Visit visit);
        decimal Discount(Visit visit);
    }


    public class AgeStrategy : IStrategy
    {
        private int age;

        public AgeStrategy(int age)
        {
            this.age = age;
        }

        public bool CanDiscount(Visit visit)
        {
            return visit.Patient.Age > age;
        }

        public decimal Discount(Visit visit)
        {
            return visit.Amount;
        }
    }

    public class GenderStrategy : IStrategy
    {
        private decimal ratio;
        private Gender gender;

        public GenderStrategy(decimal ratio, Gender gender)
        {
            this.ratio = ratio;
        }

        public bool CanDiscount(Visit visit)
        {
            return visit.Patient.Gender == gender;
        }

        public decimal Discount(Visit visit)
        {
            return visit.Amount * ratio;
        }
    }

    public class Calculator
    {
        private IStrategy strategy;

        public Calculator(IStrategy strategy)
        {
            this.strategy = strategy;
        }


        public void Calculate(Visit visit)
        {
            if (strategy.CanDiscount(visit))
            {
                visit.Discount = strategy.Discount(visit);
                visit.DiscountName = this.ToString();
            }
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
