using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models.Entities;

namespace School.Controllers
{
  public class SubjectController : Controller
  {
    private readonly SchoolDbContext _context;
    public SubjectController(SchoolDbContext context)
    {
      this._context = context;
    }
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult AddSubjects()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> AddSubjects(Subjects subject)
    {
      if (ModelState.IsValid)
      {
        await _context.Subjects.AddAsync(subject);
        await _context.SaveChangesAsync();
        return RedirectToAction("SubjectList");
      }
      else
      {
        return View(subject);
      }
    }
    [HttpGet]
    public async Task<IActionResult> SubjectList(Subjects subject)
    {
      var subj = await _context.Subjects.ToListAsync();
      return View(subj);
    }

    [HttpGet]
    public async Task<IActionResult> EditSubject(int subid)
    {
      var sub = await _context.Subjects.FindAsync(subid);
      return View(sub);

    }
    [HttpPost]
    public async Task<IActionResult> EditSubject(Subjects subject)
    {
      var subj = await _context.Subjects.FindAsync(subject.SubjectId);
      if (subj is not null)
      {
        subj.SubjectName = subject.SubjectName;
        subj.Credits = subject.Credits;
        await _context.SaveChangesAsync();
      }
      return RedirectToAction("SubjectList");

    }
    [HttpPost]
    public async Task<IActionResult> DeleteSubject(int id)
    {
      var sub = await _context.Subjects.FindAsync(id);
      if (sub is not null)
      {
        _context.Subjects.Remove(sub);
        await _context.SaveChangesAsync();
      }
      return RedirectToAction("SubjectList");
    }
  }
}
