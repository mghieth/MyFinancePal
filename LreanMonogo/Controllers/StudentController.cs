using LearnMongo.Models;
using LearnMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearnMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentsServices;

        public StudentController(IStudentService studentsServices) =>
            _studentsServices = studentsServices;


        [HttpGet]
        public async Task<List<Student>> Get() =>
            await _studentsServices.GetAsync();

        [HttpGet("{id:length(24)}")]

        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await _studentsServices.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student newsStudent)
        {
            await _studentsServices.CreateAsync(newsStudent);

            return CreatedAtAction(nameof(Get), new {id = newsStudent.Id}, newsStudent);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Student updateStudent)
        {
            var student = await _studentsServices.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            updateStudent.Id = student.Id;
             await _studentsServices.UpdateAsync(id, updateStudent);

             return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remove(string id)
        {
            var student = await _studentsServices.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            await _studentsServices.RemoveAsync(id);

            return NoContent();
        }
    }
}
