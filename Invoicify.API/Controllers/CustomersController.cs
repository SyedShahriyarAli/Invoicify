using Invoicify.API.Data;
using Invoicify.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Invoicify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer cannot be null");

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer cannot be null");

            var existingCustomer = _context.Customers.Find(id);

            if (existingCustomer == null)
                return NotFound();

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Contact = customer.Contact;
            existingCustomer.UpdatedAt = DateTime.Now;

            _context.Customers.Update(existingCustomer);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
