using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School.Data;
using School.Models;
using School.Models.Entities;
using System.Diagnostics;

namespace School.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly SchoolDbContext _context;

    public HomeController(ILogger<HomeController> logger, SchoolDbContext context)
    {
      _logger = logger;
      this._context = context;
    }

    public async Task<IActionResult> Index()
    {


      var dataPoints =  _context.StudentSubject
        .GroupBy(ss => ss.SubjectId)
        .Select(group => new
        {
          label = group.First().Subjects.SubjectName,
          y = group.Count()
        }).ToList();
      ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
