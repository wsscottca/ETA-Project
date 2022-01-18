using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Shared
{
    public class Customer
    {
        public string number { get; set; }
        public List<Order> orders { get; set; }

        public Customer()
        {
            number = "";
            orders = new List<Order> { };
        }
        public Customer(string number, List<Order> orders)
        {
            this.number = number;
            this.orders = orders;
            Console.WriteLine("Customer " + number + " created successfully.");
        }

        public string GetNumber()
        {
            return number;
        }
        public List<Order> GetOrders()
        {
            return orders;
        }
        public Order GetOrder(int number)
        {
            return orders.Find(order => order.number == number);
        }
        public void AddOrder(Order order)
        {
            orders.Add(order);
        }
    }
}
