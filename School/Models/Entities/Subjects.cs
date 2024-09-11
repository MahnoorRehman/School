using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models.Entities
{
  public class Subjects
  {
    [Key]
    public int SubjectId { get; set; }

    [Required(ErrorMessage = "Please Enter Subject Name")]
    [StringLength(50)]
    public string SubjectName { get; set; }
    public int? Credits { get; set; }
    public virtual ICollection<Teacher>? teacher { get; set; }
    public virtual ICollection<StudentSubject>? StudentSubject { get; set; }
  }
}
