using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy.Better
{

    public class Tester
    {
        public static void Test()
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

            ICanDiscountStrategy canDiscountStrategy = new AgeCanDiscountStrategy(60);
            // IDiscountStrategy discountStrategy = new FixedDiscountStrategy(10);

            canDiscountStrategy = new HappyHoursDiscountStrategy(TimeSpan.FromHours(8.5), TimeSpan.FromHours(17));

            IDiscountStrategy discountStrategy = new PercentageDiscountStrategy(0.2m);

           

            Calculator calculator = new Calculator(canDiscountStrategy, discountStrategy);

            calculator.Calculate(visit);


        }
    }


    public interface ICanDiscountStrategy
    {
        bool CanDiscount(Visit visit);        
    }

    public interface IDiscountStrategy
    {
        decimal Discount(Visit visit);
    }


    public class HappyHoursDiscountStrategy : ICanDiscountStrategy
    {
        private readonly TimeSpan From;
        private readonly TimeSpan To;
            
        public HappyHoursDiscountStrategy(TimeSpan from, TimeSpan to)
        {
            From = from;
            To = to;
        }

        public bool CanDiscount(Visit visit)
        {
            return DateTime.Now.TimeOfDay >= From && DateTime.Now.TimeOfDay <= To;
        }
    }

    public class AgeCanDiscountStrategy : ICanDiscountStrategy
    {
        private byte age;

        public AgeCanDiscountStrategy(byte age)
        {
            this.age = age;
        }

        public bool CanDiscount(Visit visit)
        {
            return visit.Patient.Age > age;
        }
    }

    public class GenderCanDiscountStrategy : ICanDiscountStrategy
    {
        private Gender? gender;

        public GenderCanDiscountStrategy(Gender gender)
        {
            this.gender = gender;
        }

        public bool CanDiscount(Visit visit)
        {
            return visit.Patient.Gender == gender;
        }
    }


    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private decimal ratio;

        public PercentageDiscountStrategy(decimal ratio)
        {
            this.ratio = ratio;
        }

        public decimal Discount(Visit visit)
        {
            return visit.Amount * ratio;
        }
    }

    public class FixedDiscountStrategy : IDiscountStrategy
    {
        private decimal discountAmount;

        public FixedDiscountStrategy(decimal discountAmount)
        {
            this.discountAmount = discountAmount;
        }

        public decimal Discount(Visit visit)
        {
            return visit.Amount - discountAmount;
        }
    }

    public class Calculator
    {
        private ICanDiscountStrategy canDiscountStrategy;
        private IDiscountStrategy discountStrategy;

        public Calculator(ICanDiscountStrategy canDiscountStrategy, IDiscountStrategy discountStrategy)
        {
            this.canDiscountStrategy = canDiscountStrategy;
            this.discountStrategy = discountStrategy;
        }

        public void Calculate(Visit visit)
        {
            if (canDiscountStrategy.CanDiscount(visit))
            {
                visit.Discount = discountStrategy.Discount(visit);
                visit.DiscountName = this.ToString();
            }
        }
    }
}
