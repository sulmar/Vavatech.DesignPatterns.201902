using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {

            Lamp lamp = new Lamp();

            Console.WriteLine(lamp.Graph);

            Console.WriteLine(lamp.Status);

            lamp.PushDown();

            Console.WriteLine(lamp.Status);

            lamp.PushDown();

            Console.WriteLine(lamp.Status);

            lamp.PushDown();

            Console.WriteLine(lamp.Status);


            lamp.PushDown();

            Console.WriteLine(lamp.Status);


            //if (lamp.Status == LampStatus.Off)
            //{
            //    lamp.Status = LampStatus.On;

            //    Console.WriteLine("Pamietaj o wyl. swiatla");
            //}
        }
    }


    public class Lamp
    {
        // public LampStatus Status { get; set; }

        // PM> Install-Package stateless

        private StateMachine<LampStatus, Trigger> machine;


        public LampStatus Status => machine.State;


        // public void PushUp() => machine.Fire(Trigger.PushUp);
        public void PushDown() => machine.Fire(Trigger.PushDown);

        // public bool CanPushUp => machine.CanFire(Trigger.PushUp);
        public bool CanPushDown => machine.CanFire(Trigger.PushDown);

        // http://www.webgraphviz.com/
        public string Graph => Stateless.Graph.UmlDotGraph.Format(machine.GetInfo());


        private bool IsHappyHours =>
            DateTime.Now.TimeOfDay >= TimeSpan.FromHours(14)
            && DateTime.Now.TimeOfDay <= TimeSpan.FromHours(15);


        private Timer timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds);

        public Lamp()
        {
            timer.Enabled = false;
            timer.AutoReset = false;

            timer.Elapsed += (s, e) => machine.Fire(Trigger.Elapsed);

            machine = new StateMachine<LampStatus, Trigger>(LampStatus.Off);

            machine.Configure(LampStatus.Off)
                .Permit(Trigger.PushDown, LampStatus.On);

            machine.Configure(LampStatus.On)
                .OnEntry(() => Console.WriteLine("Pamietaj o wyl. swiatla"), "Powitanie")
                .OnEntry(()=> timer.Start())
                .Permit(Trigger.Elapsed, LampStatus.Off)
                //.Permit(Trigger.PushDown, LampStatus.Blinking)
                .PermitIf(Trigger.PushDown, LampStatus.Blinking, ()=>IsHappyHours)
                .PermitIf(Trigger.PushDown, LampStatus.Off, () => !IsHappyHours)

                ;

            machine.Configure(LampStatus.Blinking)
                .Permit(Trigger.PushDown, LampStatus.Off)
                .OnExit(() => Console.WriteLine("dzieki za wyl. swiatla"), "Podziekowanie");

            ;


            


            // diagnostyka

            machine.OnTransitioned(t => Console.WriteLine($"[{t.Trigger}] {t.Source} -> {t.Destination}"));

        }




    }

    public enum Trigger
    {
      //  PushUp,
        PushDown,
        Elapsed
    }

    public enum LampStatus
    {
        On,
        Blinking,
        Off,
        
    }
}
