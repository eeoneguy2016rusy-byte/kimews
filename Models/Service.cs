using System.ComponentModel.DataAnnotations;
namespace PhotoStudio.Models;

public class Service
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string ServiceName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int DurationMin { get; set; }
}
