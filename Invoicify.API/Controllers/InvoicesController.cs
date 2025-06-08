using Invoicify.API.Data;
using Invoicify.Shared.Entities;
using Invoicify.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invoicify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly DataContext _context;

        public InvoicesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetInvoices(DateTime fromDate, DateTime toDate)
        {
            var invoices = _context.Invoices
                .Where(x => x.InvoiceDate >= fromDate && x.InvoiceDate <= toDate)
                .Select(x => new Invoice
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    CreatedAt = x.CreatedAt,
                    InvoiceDate = x.InvoiceDate,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    UpdatedAt = x.UpdatedAt,
                })
                .OrderByDescending(x => x.InvoiceDate)
                .ToList();

            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public IActionResult GetInvoice(int id)
        {
            var invoice = _context.Invoices
                    .Where(x => x.Id == id)
                    .Include(x => x.LineItems)
                        .ThenInclude(x => x.Product)
                    .Include(x => x.Customer)
                    .FirstOrDefault();

            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpPost]
        public IActionResult CreateInvoice(Invoice invoice)
        {
            if (invoice == null)
                return BadRequest("Invoice cannot be null");

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }

        [HttpGet("{id}/UpdateStatus")]
        public IActionResult UpdateStatus(int id, InvoiceStatus status)
        {
            var existingInvoice = _context.Invoices.Find(id);

            if (existingInvoice == null)
                return NotFound();

            existingInvoice.Status = status;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
