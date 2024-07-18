using Microsoft.AspNetCore.Mvc;
using MyFinancePal.Models;
using MyFinancePal.Services;

namespace MyFinancePal.Controllers
{
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet]
        public async Task<List<Course>> Get() => await _coursesService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Course>> Get(string id)
        {
            var course = await _coursesService.GetAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            return course;
        }


        [HttpGet("{courseName}")]

        public async Task<List<Student>> GetStudents(string courseName)
            => await _coursesService.GetStudentsAsync(courseName);
    }
}
