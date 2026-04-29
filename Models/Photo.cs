using System.ComponentModel.DataAnnotations;
namespace PhotoStudio.Models;

public class Photo
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    [MaxLength(255)]
    public string FileName { get; set; }

    [MaxLength(500)]
    public string FilePath { get; set; }

    public DateTime UploadDate { get; set; }
    
    public bool IsSelected { get; set; }
}