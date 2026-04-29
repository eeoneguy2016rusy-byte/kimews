using System.ComponentModel.DataAnnotations;

namespace PhotoStudio.Models;

public class LoginViewModel
{
    public string Username { get; set; } = string.Empty; // Это наш Email
    public string Password { get; set; } = string.Empty; // Это наше ФИО
}