using Microsoft.AspNetCore.Mvc.Rendering;
using School.Models.Entities;

namespace School.Models
{
  public class TeacherViewModel
  {
    public Teacher Teacher { get; set; }
    public List<Teacher>? TeacherList { get; set; }
    public List<Subjects>? Subjects { get; set; }
  }
}
