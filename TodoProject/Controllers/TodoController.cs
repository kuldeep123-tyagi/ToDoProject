using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoProject.Model;
using TodoProject.Repos;

namespace TodoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/todo?pageNumber=1&pageSize=10
        [HttpGet]
        public IActionResult GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var todos = _todoService.GetAll()
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize);

            return Ok(todos);
        }

        // GET: api/todo/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var todo = _todoService.GetById(id);
            if (todo == null)
                return NotFound("Todo item not found.");

            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem todo)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            var createdTodo = _todoService.Create(todo);
            return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
        }

        // PUT: api/todo/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] TodoItem todo)
        {
            if (!ModelState.IsValid || id != todo.Id)
                return BadRequest("Invalid data.");

            var updated = _todoService.Update(todo);
            if (!updated)
                return NotFound("Todo item not found.");

            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = _todoService.Delete(id);
            if (!deleted)
                return NotFound("Todo item not found.");

            return NoContent();
        }
    }
}
