using System.ComponentModel.DataAnnotations;

namespace School.Models.Entities
{
  public class Gender
  {
    [Required]
    [Key]
    public int GenderId { get; set; }
    [Required]
    [StringLength(25)]
    public string GenderName { get; set; } = null!;
  }
}
