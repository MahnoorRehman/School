﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models.Entities;

namespace School.Controllers
{

  public class StudentController : Controller
  {
    private readonly SchoolDbContext _context;

    public StudentController(SchoolDbContext context)
    {
      this._context = context;
    }
    public IActionResult Index()
    {
      var students = _context.Students.ToList();
      return View(students);
    }
    public IActionResult Add()
    {
      //var stud = new Students();
      //var classes = _context.StudentClass.ToList();
      //stud.StudentClasses= new SelectList(classes, "ClassId", "ClassName");
      var classes = _context.StudentClass.ToList();
      ViewBag.Classes = new SelectList(classes, "ClassId", "ClassName");
      var gen = _context.Gender.ToList();
      ViewBag.Gen = new SelectList(gen, "GenderId", "GenderName");

      var subjects = _context.Subjects.ToList();
      ViewBag.Subjects = new SelectList(subjects, "SubjectId", "SubjectName");


      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(Students add_student, int[] StudentSubject)
    {

      if (ModelState.IsValid)
      {
        await _context.Students.AddAsync(add_student);
        await _context.SaveChangesAsync();

        if (StudentSubject != null && StudentSubject.Length > 0)
        {
          // Create StudentSubject entries
          var studentSubjects = StudentSubject.Select(subjectId => new StudentSubject
          {
            StudentId = add_student.StudentId,
            SubjectId = subjectId
          }).ToList();

          // Add entries to the StudentSubject table
          await _context.StudentSubject.AddRangeAsync(studentSubjects);
          await _context.SaveChangesAsync();
        }

        return RedirectToAction("StudentList", "Student");
      }
      else
      {
        var classes = _context.StudentClass.ToList();
        ViewBag.Classes = new SelectList(classes, "ClassId", "ClassName");

        var gen = _context.Gender.ToList();
        ViewBag.Gen = new SelectList(gen, "GenderId", "GenderName");

        var subjects = _context.Subjects.ToList();
        ViewBag.Subjects = new SelectList(subjects, "SubjectId", "SubjectName");

        return View(add_student);
      }

    }
    [HttpGet]
    public async Task<IActionResult> StudentList()
    {
      var students = await _context.Students.ToListAsync();

      return View(students);
    }
    [HttpGet]
    public async Task<IActionResult> EditStudent(int id)
    {
      var classes = _context.StudentClass.ToList();
      ViewBag.Classes = new SelectList(classes, "ClassId", "ClassName");

      var gen = _context.Gender.ToList();
      ViewBag.Gen = new SelectList(gen, "GenderId", "GenderName");

      var subjects = _context.Subjects.ToList();
      ViewBag.Subjects = new SelectList(subjects, "SubjectId", "SubjectName");

      var stuId = await _context.Students
        .Include(s => s.StudentSubject)
        .FirstOrDefaultAsync(s => s.StudentId == id);

      // Prepare a list of selected subjects
      var selectedSubjects = stuId.StudentSubject.Select(ss => ss.SubjectId).ToList();
      ViewBag.SelectedSubjects = selectedSubjects;

      stuId.Classid = stuId.Classid.Trim();
      return View(stuId);
    }
    [HttpPost]
    public async Task<IActionResult> EditStudent(Students students, int[] StudentSubject)
    {

      var stu = await _context.Students.Include(s=> s.StudentSubject)
        .FirstOrDefaultAsync(x => x.StudentId == students.StudentId);
      if (stu is not null)
      {
        stu.FirstName = students.FirstName;
        stu.LastName = students.LastName;
        stu.DoB = students.DoB;
        stu.Email = students.Email;
        stu.Phone = students.Phone;
        stu.Gender = students.Gender;
        stu.Classid = students.Classid;
        await _context.SaveChangesAsync();

        // Handle Student Subjects
        if (StudentSubject != null && StudentSubject.Length > 0)
        {
          // Get the existing subject IDs for the student
          var existingSubjectIds = stu.StudentSubject.Select(ss => ss.SubjectId).ToList();

          // Determine which subjects to remove
          var subjectsToRemove = existingSubjectIds
              .Where(existingId => !StudentSubject.Contains(existingId))
              .ToList();

          // Determine which subjects to add
          var subjectsToAdd = StudentSubject
              .Where(subjectId => !existingSubjectIds.Contains(subjectId))
              .Select(subjectId => new StudentSubject
              {
                StudentId = students.StudentId,
                SubjectId = subjectId
              }).ToList();

          // Remove old subjects
          if (subjectsToRemove.Count > 0)
          {
            var subjectsToRemoveEntities = stu.StudentSubject
                .Where(sss => subjectsToRemove.Contains(sss.SubjectId))
                .ToList();

            _context.StudentSubject.RemoveRange(subjectsToRemoveEntities);
          }

          // Add new subjects
          if (subjectsToAdd.Count > 0)
          {
            _context.StudentSubject.AddRange(subjectsToAdd);
          }

          await _context.SaveChangesAsync();
        }
      }


      return RedirectToAction("StudentList", "Student");
    }
    [HttpGet]
    public async Task<IActionResult> DeleteStudent(int id)
    {
      var del_student = await _context.Students.FindAsync(id);
      if (del_student is not null)
      {
        _context.Students.Remove(del_student);
        await _context.SaveChangesAsync();
      }
      return RedirectToAction("StudentList", "Student");
    }
  }
}
