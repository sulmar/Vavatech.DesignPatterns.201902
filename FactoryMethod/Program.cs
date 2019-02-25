using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Wybierz typ wizyty (P)rywatna, (N)FZ");

            VisitType visitType = VisitType.Hybrid;

            // Bed practice
            //Visit visit = null;

            //switch(visitType)
            //{
            //    case VisitType.NFZ: visit = new NFZVisit(); break;
            //    case VisitType.Private: visit = new PrivateVisit(); break;
            //}

            Visit visit = VisitFactory.Create(visitType);

            decimal amount = visit.Calculate();

            Console.WriteLine($"Koszt wizyty {amount}");
            // BadPracticeTest();

        }

        private static void BadPracticeTest()
        {
            Medium medium = Medium.Sms;


            if (medium == Medium.Email)
            {
                Console.WriteLine("Send email");
            }
            else if (medium == Medium.Sms)
            {
                Console.WriteLine("Send sms");
            }
            else if (medium == Medium.Tweet)
            {
                Console.WriteLine("Send tweet");
            }

            switch (medium)
            {
                case Medium.Email: Console.WriteLine("Send email"); break;
                case Medium.Tweet: Console.WriteLine("Send tweet"); break;
                case Medium.Sms: Console.WriteLine("Send sms"); break;
            }

            Console.WriteLine("Dziekujemy za wyslanie.");
        }
    }

    enum Medium
    {
        Email,
        Sms,
        Tweet
    }
}
