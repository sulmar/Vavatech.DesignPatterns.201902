using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{


    #region Factory Pattern

    public abstract class Product
    {
    }

    public class ConcreteProductA : Product
    {
    }

    public class ConcreteProductB : Product
    {
    }

    public abstract class Factory
    {
        public abstract Product Create();
    }
    

    public class ConcreteFactory : Factory
    {
        public override Product Create()
        {
            return new ConcreteProductA();
        }
    }


    #endregion


    class VisitFactory
    {
        public static Visit Create(VisitType visitType)
        {
            Visit visit = null;

            switch (visitType)
            {
                case VisitType.NFZ: visit = new NFZVisit(); break;
                case VisitType.Private: visit = new PrivateVisit(); break;

                default: throw new NotSupportedException($"{nameof(visitType)}: {visitType}");
            }

            return visit;


        }
    }


    enum VisitType
    {
        Private,

        NFZ,

        Hybrid
    }

    abstract class Visit
    {
        public DateTime CreateDate { get; set; }
        public TimeSpan Duration { get; set; }

        public Visit()
        {
            Duration = TimeSpan.FromMinutes(10);
        }

        public abstract decimal Calculate();
    }

    class NFZVisit : Visit
    {
        public override decimal Calculate()
        {
            return 0;
        }
    }

    class PrivateVisit : Visit
    {
        private decimal amountPerHour;

        public override decimal Calculate()
        {
            return Duration.Hours * amountPerHour;
        }
    }
}
