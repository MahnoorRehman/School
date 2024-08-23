using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models.Entities
{
  public class Students
  {
    [Key]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Please Enter First Name")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Please Enter Last Name")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please Enter Date of Birth")]
    public DateOnly DoB { get; set; }

    [Required(ErrorMessage = "Please Enter Date of Birth")]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please enter the student's phone number.")]
    [MinLength(11, ErrorMessage = "Please Enter Valid Phone Number."), MaxLength(11)]
    [Phone]
    public string Phone { get; set; }
    [StringLength(50)]

    public string? City { get; set; }
    [Required(ErrorMessage = "Please Choose Class")]
    public string Classid { get; set; }

    [StringLength(25)]
    [Required(ErrorMessage = "Please Choose Gender")]
    public string Gender { get; set; }

    //[NotMapped]
    //public SelectList StudentClasses { get; set; }
    public virtual ICollection<StudentSubject> StudentSubject { get; set; }
  }
}
