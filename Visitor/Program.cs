using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {

            Patient patient = new Patient
            {
                FirstName = "Ryszard",
            };

            Visit visit = new Visit
            {
                Patient = patient,
                VisitDate = DateTime.Now,
                Duration = TimeSpan.FromHours(1),
                TotalAmount = 150,
                Medicals = new List<Product>
                {
                    new Product { Name = "Medical 1"},
                    new Product { Name = "Medical 2"},
                    new Product { Name = "Medical 3"}
                }
            };

            IVisitor visitor = new HtmlVisitor();

            VisitHelper.Build(visitor, visit);

            Console.WriteLine(visitor.Output);


        }
    }


    public interface IVisitor
    {
        void Visit(Visit visit);
        void Visit(Patient patient);
        void Visit(Product product);

        string Output { get; }
    }

    public class AsciVisitor : IVisitor
    {
        public string Output => throw new NotImplementedException();

        public void Visit(Visit visit)
        {
            throw new NotImplementedException();
        }

        public void Visit(Patient patient)
        {
            throw new NotImplementedException();
        }

        public void Visit(Product product)
        {
            throw new NotImplementedException();
        }
    }

    public class HtmlVisitor : IVisitor
    {
        public string Output => builder.ToString();

        private StringBuilder builder = new StringBuilder();

        public void Visit(Visit visit)
        {
            builder.AppendLine("<b>Id = {visit.Id}</b>");
        }

        public void Visit(Patient patient)
        {
            builder.AppendLine($"<i>{patient.FirstName} {patient.LastName}");
        }

        public void Visit(Product product)
        {
            builder.AppendLine($"{product.Name}");
        }
    }

    public abstract class DocumentPart
    {
        public string Text { get; private set; }
        public abstract void Accept(IVisitor visitor);
    }


    // Model

    public abstract class Base
    {

    }


    public class Product : Base
    {
        public string Name { get; set; }

    }


    public class VisitHelper
    {
        public static string Build(IVisitor visitor, Visit visit)
        {
            visitor.Visit(visit);

            visitor.Visit(visit.Patient);

            foreach (var product in visit.Medicals)
            {
                visitor.Visit(product);
            }

            return visitor.Output;
        }
    }

    public class Visit : Base
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public Patient Patient { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal TotalAmount { get; set; }

        public IList<Product> Medicals { get; set; }

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
