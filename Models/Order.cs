using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Required]
    public DateTime SessionDate { get; set; }

    [Required]
    public string Status { get; set; } = "Новый";

    public decimal TotalAmount { get; set; }

    public string? Notes { get; set; }

    [Required]
    public int ClientId { get; set; }
    public Client? Client { get; set; }

    [Required]
    public int PhotographerId { get; set; }
    public Photographer? Photographer { get; set; }
    
    [Required]
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
}