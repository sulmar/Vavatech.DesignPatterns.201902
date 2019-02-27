using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public interface ICall
    {
        void Call();
    }

    public interface IFrom
    {
        ITo From(string number);
    }

    public interface ITo
    {
        ISubject To(string number);
    }

    public interface ISubject : ICall
    {
        ICall WithSubject(string subject);
    }


    public class Phone : IFrom, ITo, ISubject
    {
        private string _from;
        private string _to;
        private string _subject;

        public static IFrom Instance => new Phone();

        public void Call()
        {
            if (string.IsNullOrEmpty(_subject))
            {
                Console.WriteLine($"Calling from {_from} to {_to}");
            }
            else
            {
                Console.WriteLine($"Calling from {_from} to {_to} in subject {_subject}");
            }
        }

        public ITo From(string number)
        {
            _from = number;

            return this;
        }

        public ISubject To(string number)
        {
            _to = number;

            return this;
        }

        public ICall WithSubject(string subject)
        {
            _subject = subject;

            return this;
        }
    }


    //public class Phone
    //{
    //    private string _from;
    //    private string _to;
    //    private string _subject;public static Phone Instance = new Phone();

    //    

    //    public Phone From(string from)
    //    {
    //        _from = from;

    //        return this;
    //    }

    //    public Phone To(string to)
    //    {
    //        _to = to;

    //        return this;
    //    }

    //    public Phone WithSubject(string subject)
    //    {
    //        _subject = subject;

    //        return this;
    //    }

    //    public void Call()
    //    {
    //        if (string.IsNullOrEmpty(_subject))
    //        {
    //            Console.WriteLine($"Calling from {_from} to {_to}");
    //        }
    //        else
    //        {
    //            Console.WriteLine($"Calling from {_from} to {_to} in subject {_subject}");
    //        }


    //    }


    //}
}
