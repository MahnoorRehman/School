using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models.Entities
{
  public class StudentClass
  {
    [Key]
    public int ClassId { get; set; }
    [Required(ErrorMessage = "Please Enter Class Name")]
    [RegularExpression(@"^Level\s\d+$", ErrorMessage = "Class name must be in the format 'Level 1' ")]
    public string ClassName { get; set; }

  }
}
