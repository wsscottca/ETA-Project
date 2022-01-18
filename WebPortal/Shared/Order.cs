using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Shared
{
    public class Order
    {
        public int number { get; set; }
        public string orderDate { get; set; }
        internal bool orderAlloc { get; set; }
        public List<Item> items { get; set; }
        public Order()
        {
            number = 0;
            orderDate = "";
            items = new List<Item> { };
        }

        public Order(int number, string orderDate, bool orderAlloc, List<Item> items)
        {
            this.number = number;
            this.orderDate = orderDate;
            this.orderAlloc = orderAlloc;
            this.items = items;

            Console.WriteLine("Order " + number + " created successfully.");
        }

        public int GetOrderNumber() { return number; }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public List<Item> GetItems()
        {
            return items;
        }
        public string GetDate() { return orderDate; }
    }
}
