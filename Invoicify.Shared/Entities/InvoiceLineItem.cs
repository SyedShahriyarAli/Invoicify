#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace Invoicify.Shared.Entities
{
    public class InvoiceLineItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Total => UnitPrice * Quantity;

        public Invoice Invoice { get; set; }
        public Product Product { get; set; }

        [NotMapped]
        public string ProductName { get; set; }
    }
}
