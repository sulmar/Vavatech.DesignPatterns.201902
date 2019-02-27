using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            TempObservable source = new TempObservable();

            MyObserver observer1 = new MyObserver("Marcin");

            using (IDisposable subsciption = source.Subscribe(observer1))
            {

            }

        }

    }

    public class MyObserver : IObserver<float>
    {
        private readonly string name;

        public MyObserver(string name)
        {
            this.name = name;
        }

        public void OnCompleted()
        {
            Console.WriteLine($"[{name}] koniec nadawania");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"[{name}] blad odczytu");
        }

        public void OnNext(float value)
        {
            Console.WriteLine($"[{name}] {value}");
        }
    }

    public class TempObservable : IObservable<float>
    {
        public IDisposable Subscribe(IObserver<float> observer)
        {
            observer.OnNext(10);
            observer.OnNext(20);
            observer.OnNext(30);
            observer.OnNext(40);
            observer.OnNext(50);

            observer.OnCompleted();

            return null;
        }
    }
}
