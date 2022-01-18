using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Server.Models;
using WebPortal.Server.Storage;
using WebPortal.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebPortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;
        public OrderStatusController(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _customerRepository.GetAll()
                .OrderBy(customer => customer.number);
        }
    }
}
