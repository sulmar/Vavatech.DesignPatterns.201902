using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            IRadio radio = new HyteraAdapter();

            radio.Call("555-566-788");

          //  Hytera radio = new Hytera();

            //if ()

            //radio.Init();

            //if ()
            //radio.Call("555-566-788");

            //if
            //radio.Release();
        }
    }

    public interface IRadio
    {
        void Call(string recipient);
    }

    public class MotorolaAdapter : IRadio
    {
        // Adaptee
        private Motorola radio;

        public void Call(string recipient)
        {
            radio.Init();
            radio.Call(recipient);
            radio.Release();
        }
    }

    public class HyteraAdapter : IRadio
    {
        private Hytera radio;

        public void Call(string recipient)
        {
            radio.Start();
            radio.Ring(recipient);
        }
    }

    public class Hytera
    {
        public int Volume { get; set; }

        public void Start()
        {
            Volume = 50;
        }

        public void Ring(string to)
        {
            Console.WriteLine($"Calling to {to}");
        }
    }


    public class Motorola
    {
        private bool powerOn = false;

        public void Init()
        {
            powerOn = true;
        }

        public void Call(string to)
        {
            if (powerOn)
            {
                Console.WriteLine($"Calling {to}");
            }

        }

        public void Release()
        {
            powerOn = false;
        }
    }
}
