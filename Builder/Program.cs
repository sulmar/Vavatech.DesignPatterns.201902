using CSharpVerbalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Phone.Instance
                .From("555-666-777")
                .To("555-777-322")
                .WithSubject("Design Pattern")
                .Call();


            //Phone.Instance
            //    .To("555-432-445")
            //    .From("555-556-333")
                    
                    
            //        .WithSubject("Wzorce projektowe")
            //        .Call();



            // RegexTest();



            // ReportTest();

            // Test(ref visit);

            // StringBuilderTest();
        }


        private static void 
            RegexTest()
        {
            // PM> Install-Package VerbalExpressions-official

            // http[s]://www.domain.com

            VerbalExpressions expression = new VerbalExpressions()
                .StartOfLine()
                .Then("http")
                .Maybe("s")
                .AnythingBut(" ")
                .EndOfLine();

            Regex regex = expression.ToRegex();

            string url = "http://www.vavatech.pl:8080";

            bool isValid = expression.Test(url);
        }

        private static void ReportTest()
        {
            Patient patient = new Patient
            {
                Id = 1,
                FirstName = "Marcin",
                LastName = "Sulecki"
            };

            Visit visit = new Visit
            {
                Patient = patient,
                TotalAmount = 150,
                Duration = TimeSpan.FromHours(1)
            };

            BuilderBase builder = new HtmlBuilder(visit);

            builder.AddHeader();
            builder.AddContent();
            builder.AddFooter();

            string result = builder.Build();

            Console.WriteLine(result);
        }

        private static void StringBuilderTest()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("First line");
            builder.AppendLine("Hello");
            builder.AppendLine("Hello");


            var result = builder.ToString();
        }

        public static void Test(ref Visit visit)
        {
            visit = new Visit() { Id = 100 };
        }
    }


   


    public abstract class BuilderBase
    {
        public abstract void AddHeader();
        public abstract void AddContent();
        public abstract void AddFooter();

        public abstract string Build();
    }


    public class HtmlBuilder : BuilderBase
    {
        private readonly Visit visit;
        private string view;

        public HtmlBuilder(Visit visit)
        {
            this.visit = visit;

            view = "<html>";
        }

        public override void AddHeader()
        {
            view += $"<title>Wizyta Id: {visit.Id}</title>";
        }


        public override void AddContent()
        {
            view += $"<b>Czas trwania: {visit.Duration}</b>";
        }

        public override void AddFooter()
        {
            view += $"-------------------";
        }

        public override string Build()
        {
            return view + "</html>";
        }


    }
}
