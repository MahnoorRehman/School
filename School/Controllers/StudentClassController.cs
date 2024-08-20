using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models.Entities;

namespace School.Controllers
{
  public class StudentClassController : Controller
  {
    private readonly SchoolDbContext _Context;
    public StudentClassController(SchoolDbContext context)
    {
      this._Context = context;
    }
    public IActionResult Add()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(StudentClass student_Class)
    {
      //var stuClass = new StudentClass
      //{
      //  ClassId = student_Class.ClassId,
      //  ClassName = student_Class.ClassName,

      //};
      //check existing class
      var existingClass = await _Context.StudentClass
                        .FirstOrDefaultAsync(c => c.ClassName == student_Class.ClassName);
      if(existingClass != null)
      {
        ModelState.AddModelError("ClassName", "Class Name Already Exists");
        return View(student_Class);
      }

      if (ModelState.IsValid)
      {
        await _Context.StudentClass.AddAsync(student_Class);
        await _Context.SaveChangesAsync();
        ModelState.Clear();
        return RedirectToAction("ClassList");
      }
      else
      {
        return View(student_Class);
      }


    }
    [HttpGet]
    public async Task<IActionResult> ClassList()
    {
      var studentClass = await _Context.StudentClass.ToListAsync();
      return View(studentClass);
    }
    [HttpGet]
    public async Task<IActionResult> EditClass(int classId)
    {
      var clsId = await _Context.StudentClass.FindAsync(classId);
      return View(clsId);

    }
    [HttpPost]
    public async Task<IActionResult> EditClass(StudentClass studentClass)
    {
      var stuClass = await _Context.StudentClass.FindAsync(studentClass.ClassId);
      if (stuClass is not null)
      {
        stuClass.ClassName = studentClass.ClassName;
        await _Context.SaveChangesAsync();
      }
      return RedirectToAction("ClassList", "StudentClass");

    }

    [HttpGet]
    public async Task<IActionResult> DeleteClass(int classId)
    {
      var del_class = await _Context.StudentClass.FindAsync(classId);
      if (del_class is not null)
      {
        _Context.StudentClass.Remove(del_class);
        await _Context.SaveChangesAsync();

      }
      return RedirectToAction("ClassList", "StudentClass");
    }
  }
}
