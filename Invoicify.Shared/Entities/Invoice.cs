#nullable disable

using Invoicify.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicify.Shared.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public double TotalAmount { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public Customer Customer { get; set; }

        public ICollection<InvoiceLineItem> LineItems { get; set; } = new HashSet<InvoiceLineItem>();

        [NotMapped]
        public string CustomerName { get; set; }
    }
}
