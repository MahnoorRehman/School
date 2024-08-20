using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using School.Models.Entities;

namespace School.Controllers
{
  public class TeacherController : Controller
  {
    private readonly SchoolDbContext _context;
    public TeacherController(SchoolDbContext context)
    {
      this._context = context;
    }
    [HttpGet]
    public async Task<IActionResult> TeacherList()
    {
      var teach = await _context.Teacher.Include(t => t.Subject).ToListAsync();
      var viewModel = new TeacherViewModel
      {
        TeacherList = teach,
        //Subjects = await _context.Subjects.ToListAsync()

      };
      return View(viewModel);
    }
    public async Task<IActionResult> AddTeacher()
    {
      var subjects = await _context.Subjects.ToListAsync();
      var viewModel = new TeacherViewModel
      {
        Teacher = new Teacher(),
        Subjects = subjects
      };
      return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> AddTeacher(TeacherViewModel viewModel)
    {
      if (ModelState.IsValid)
      {
        _context.Teacher.Add(viewModel.Teacher);
        await _context.SaveChangesAsync();
        return RedirectToAction("TeacherList");
      }

      viewModel.Subjects = await _context.Subjects.ToListAsync();
      return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> EditTeacher(int teacherId)
    {
      var teacher = await _context.Teacher.FindAsync(teacherId);
      if (teacher == null)
      {
        return NotFound();
      }
      var vModel = new TeacherViewModel
      {
        Teacher = teacher,
        Subjects = await _context.Subjects.ToListAsync()
      };
      return View(vModel);

    }
    [HttpPost]
    public async Task<IActionResult> EditTeacher(TeacherViewModel t)
    {
      var teacher = await _context.Teacher.FindAsync(t.Teacher.TeacherId);
      if (teacher is not null)
      {
        teacher.TeacherName = t.Teacher.TeacherName;
        teacher.SubjectId = t.Teacher.SubjectId;
        await _context.SaveChangesAsync();
      }
      return RedirectToAction("TeacherList");
    }
    [HttpPost]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
      var teacher =await _context.Teacher.FindAsync(id);
      if(teacher is not null)
      {
        _context.Teacher.Remove(teacher);
        await _context.SaveChangesAsync();

      }
      return RedirectToAction("TeacherList");

    }
  }
}
