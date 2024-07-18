using LearnMongo.Models;
using LearnMongo.Resource;
using LearnMongo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnMongo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentsServices;

        public StudentController(IStudentService studentsServices) =>
            _studentsServices = studentsServices;


        [HttpGet]
       
        public async Task<List<StudentResource>> Get() =>
            await _studentsServices.GetAsync();

        [HttpGet("{id:length(24)}")]

        public async Task<ActionResult<StudentResource>> Get(string id)
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

           // return CreatedAtAction(nameof(Get), new {id = newsStudent.Id}, newsStudent);

           return Ok(await _studentsServices.GetAsync());
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

             return Ok(await _studentsServices.GetAsync());
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

            return Ok(await _studentsServices.GetAsync());
        }
    }
}
