using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models.Entities
{
  //[PrimaryKey(nameof(TeacherId), nameof(SubjectId))]

  public class Teacher
  {
    [Key]
    [Required]
    public int TeacherId { get; set; }

    [Required(ErrorMessage = "Please Enter Teacher Name.")]
    [StringLength(50)]
    public string TeacherName { get; set; }

    [Required (ErrorMessage = "Please Select Subject")]
    public int SubjectId { get; set; }

    [ForeignKey("SubjectId")]
    
    public virtual Subjects? Subject { get; set; }

  }

}
