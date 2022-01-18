using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Server.Models;
using WebPortal.Server.Storage;
using WebPortal.Shared;

namespace WebPortal.Server
{
    public static class LoadData
    {
        public static void AddCustomerRepository(this IServiceCollection services)
        {
            Workbook workbook = new Workbook();
            workbook.InitWorkbook();
            List<Customer> customers = workbook.MapOrdersFromWorkbook();

            var customerRepository = new MemoryRepository<Customer>();

            foreach (Customer customer in customers) {
                customerRepository.Add(customer);
            }

            services.AddSingleton<IRepository<Customer>>(customerRepository);
        }
    }
}
