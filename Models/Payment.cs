using System.ComponentModel.DataAnnotations;
namespace PhotoStudio.Models;

public class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public DateTime PaymentDate { get; set; }
    
    public decimal Amount { get; set; }

    [MaxLength(30)]
    public string PaymentType { get; set; } // Например: Карта, Наличные
}