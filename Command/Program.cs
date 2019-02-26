using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {

            Patient patient = new Patient
            {
                FirstName = "Marcin",
                Pesel = "74329472389742"
            };


            Visit visit = new Visit()
            {
                Patient = patient
            };

            //ICommand command = new AddPatientCommand(patient);

            //if (command.CanExecute())
            //{
            //    command.Execute();
            //}


            IList<ICommand> commands = new List<ICommand>();

            commands.Add(new AddPatientCommand(patient));
            commands.Add(new PrintCommand(visit));

            foreach (ICommand command in commands)
            {
                if (command.CanExecute())
                {
                    Task.Run(()=>command.Execute());
                }
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

        public void Print()
        {
            Console.WriteLine($"Printing  {VisitDate}...");
        }

        public void Save()
        {
            Console.WriteLine($"Saving {VisitDate}");
        }

        public bool CanSave()
        {
            return !string.IsNullOrEmpty(Patient.FirstName)
                 && !string.IsNullOrEmpty(Patient.Pesel);
        }

        public bool CanPrint()
        {
            return Amount > 0;
        }


        public void AddPatient()
        {

        }

    }

    public interface ICommand
    {
        void Execute();
        bool CanExecute();
    }


    public class PrintCommand : ICommand
    {
        private readonly Visit visit;

        public PrintCommand(Visit visit)
        {
            this.visit = visit;
        }

        public bool CanExecute()
        {
            return visit.Amount > 0;
        }

        public void Execute()
        {
            Console.WriteLine($"Printing {visit.VisitDate}...");
        }
    }

    public class AddPatientCommand : ICommand
    {
        private readonly Patient patient;

        public AddPatientCommand(Patient patient)
        {
            this.patient = patient;
        }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(patient.FirstName)
                 && !string.IsNullOrEmpty(patient.Pesel);
        }

        public void Execute()
        {
            Console.WriteLine($"Saving  {patient}...");
        }
    }
}
