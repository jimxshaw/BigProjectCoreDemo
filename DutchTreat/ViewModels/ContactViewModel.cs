using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.ViewModels
{
  public class ContactViewModel
  {
    [Required]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters")]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Subject { get; set; }

    [Required]
    [MaxLength(500, ErrorMessage = "Message must be fewer than 500 characters")]
    public string Message { get; set; }


  }
}
