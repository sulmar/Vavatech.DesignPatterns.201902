using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer("Marcin", "Sulecki");

            Customer copyCustomer = (Customer) customer.Clone();

            copyCustomer.FirstName = "Bartek";


            Order order = new Order(1, "ZAM 001", customer)
            {
                DeliveryDate = DateTime.Now.AddDays(14)
            };


            Order copyOrder = new Order(2, "ZAM 002", order.Customer)
            {
                CreateDate = order.CreateDate,
            };

           
            

        }
    }

    public abstract class Base
    {

    }

    public class Order : Base
    {
        protected Order()
        {

        }

        private Order(int id)
        {
            Id = id;
            CreateDate = DateTime.Now;
        }

        public Order(int id, string orderNumber, Customer customer)
            : this(id)
        {
           
            OrderNumber = orderNumber;
            Customer = customer;
        }


        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public Customer Customer { get; set; }

        public DateTime? DeliveryDate { get; set; }

        
    }

  

    public class Customer : Base, ICloneable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRemoved { get; set; }


        protected Customer()
        {

        }

        public Customer(string firstName, string lastName)
            : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public object Clone()
        {
            //Customer copyCustomer = new Customer
            //{
            //    Id = this.Id,
            //    FirstName = this.FirstName,
            //    LastName = this.LastName
            //};

            return MemberwiseClone();

            //return copyCustomer;
        }
    }
}
