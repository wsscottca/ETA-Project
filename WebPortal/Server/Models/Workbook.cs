using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Shared;

namespace WebPortal.Server.Models
{
    public class Workbook
    {
        private ExcelWorksheet worksheet;
        private string path;
        public Workbook(string path)
        {
            this.path = path;
        }
        public Workbook()
        {
            this.path = "Data/open_orders.xlsx";
        }

        private ExcelWorksheet GetWorksheet(string path)
        {
            return worksheet;
        }

        public void InitWorkbook()
        {
            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(fileInfo);
            worksheet = package.Workbook.Worksheets.FirstOrDefault();
        }

        public List<Customer> MapOrdersFromWorkbook()
        {
            int rows = worksheet.Dimension.Rows;
            int columns = worksheet.Dimension.Columns;

            List<Customer> customers = new List<Customer> { };

            Customer customer = null;
            Order order = null;
            Item item = null;
            string customerNumber;
            int orderNumber = 0;
            string orderDate;
            string sku;
            string model;
            string description;
            int quantity = 0;
            bool invIssue;
            string eta;
            bool orderAlloc;

            for (int i = 2; i <= rows; i++)
            {
                // Map the cells
                customerNumber = worksheet.Cells[i, 1].Value.ToString();
                if (int.TryParse(worksheet.Cells[i, 2].Value.ToString(), out int result))
                {
                    orderNumber = Convert.ToInt32(worksheet.Cells[i, 2].Value.ToString());
                }
                sku = worksheet.Cells[i, 3].Value.ToString();
                model = worksheet.Cells[i, 4].Value.ToString();
                description = worksheet.Cells[i, 5].Value.ToString();
                if (description != null && description != "NULL" && description != "" && description.Length > 10)
                {
                    description = description.Substring(0, description.Length - 6);
                }
                if (int.TryParse(worksheet.Cells[i, 6].Value.ToString(), out int result1))
                {
                    quantity = Convert.ToInt32(worksheet.Cells[i, 6].Value.ToString());
                }

                // Change String to bool for inventory issue and order allocation
                if (worksheet.Cells[i, 7].Value.ToString() == "Y")
                {
                    invIssue = true;
                }
                else
                {
                    invIssue = false;
                }
                if (invIssue)
                {
                    eta = worksheet.Cells[i, 8].Value.ToString();
                    if (eta != null && eta != "NULL" && eta != "" && eta.Length > 10)
                    {
                        eta = eta.Substring(0, eta.IndexOf(' '));
                    }
                    else
                    {
                        eta = "No ETA available from Vendor.";
                    }
                }
                else
                {
                    eta = "In Stock";
                }
                orderDate = worksheet.Cells[i, 9].Value.ToString();
                orderDate = orderDate.Substring(0, orderDate.IndexOf(' '));
                if (worksheet.Cells[i, 10].Value.ToString() == "Y")
                {
                    orderAlloc = true;
                }
                else
                {
                    orderAlloc = false;
                }

                // If we're not working on the same customer as the previous line, if customer already exists (is in the list of existing customers) grab that customer, otherwise create new customer
                if (customer == null || customerNumber != customer.GetNumber())
                {
                    customer = customers.Where(c => c.GetNumber() == customerNumber).FirstOrDefault();
                    if (customer == null)
                    {
                        customer = new Customer(customerNumber, new List<Order> { });
                        customers.Add(customer);
                    }
                    else
                    {
                        Console.WriteLine("Customer " + customer.GetNumber() + " found successfully.");
                    }
                }


                // If we're not working on the same order as the previous line, if order already exists for customer then grab it from the list of the customer's orders. Otherwise create a new order for the customer.
                if (order == null || orderNumber != order.GetOrderNumber())
                {
                    order = customer.GetOrders().Where(o => o.GetOrderNumber() == orderNumber).FirstOrDefault();
                    if (order == null)
                    {
                        order = new Order(orderNumber, orderDate, orderAlloc, new List<Item> { });
                    }
                    else
                    {
                        Console.WriteLine("Order " + order.GetOrderNumber() + " found successfully.");
                    }
                    customer.AddOrder(order);
                }

                // Create an item and then push it to the order's items
                item = new Item(sku, model, description, quantity, invIssue, eta);
                order.AddItem(item);
            }
            return customers;
        }
    }
}
