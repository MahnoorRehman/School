using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models.Entities
{
  public class StudentSubject
  {
    [Required]
    public int StudentId { get; set; }
    [ForeignKey("StudentId")]
    public Students Students { get; set; }
    [Required]
    public int SubjectId { get; set; }
    [ForeignKey("SubjectId")]
    public Subjects Subjects { get; set; }
  }
}
