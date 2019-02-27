using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            Patient patient = new Patient
            {
                FirstName = "Marcin",
                LastName = "Sulecki"
            };

            patient.BeginEdit();

            patient.FirstName = "Bartek";

            patient.CancelEdit();

            Console.WriteLine(patient.FirstName);
        }
    }


    public class Patient : IEditableObject, ICloneable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        private Patient copy;

        public void BeginEdit()
        {
            copy = (Patient) this.Clone();
        }

        public void CancelEdit()
        {
            this.FirstName = copy.FirstName;
            this.LastName = copy.LastName;
        }

        public void EndEdit()
        {
            copy = null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
