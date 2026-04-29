namespace PhotoStudio.Models;

public class Photographer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; }
    public int ExperienceYears { get; set; }
}